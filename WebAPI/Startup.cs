﻿using Application.Commands.User;
using Application.Interfaces;
using Application.Queries.User;
using EFDataAccess;
using Implementation.AutoMapper;
using Implementation.Commands.User;
using Implementation.Services.Queriess.User;
using Microsoft.EntityFrameworkCore;
using System.Text;
using WebAPI.Email;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;
using WebAPI.Hashing;
using System.Reflection;
using Application.Commands;
using Implementation.Services.Commands;
using Application.Queries;
using Implementation.Services.Queriess;

namespace WebAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddCors();
            services.AddAutoMapper(typeof(ConfigurationMapping));

            services.AddControllers();

            //  services.AddDbContext<DBContext>();

            services.AddDbContext<DBContext>(
                options => options.UseSqlServer(
                    this.Configuration.GetConnectionString("DBConnection")
                    )
                );

            services.AddSwaggerGen(c => { //<-- NOTE 'Add' instead of 'Configure'
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "FoodyAPI",
                    Version = "v1"
                });
            });


            services.AddTransient<IAddUserCommand, EFAddUserCommand>();
            services.AddTransient<IUpdateUserCommand, EFUpdateUserCommand>();
            services.AddTransient<IDeleteUserCommand, EFDeleteUserCommand>();
            services.AddTransient<IGetUserQuery, EFGetUserQuery>();
            services.AddTransient<IGetUsersQuery, EFGetUsersQuery>();

            services.AddTransient<IAddIngredientCommand, EFAddIngredientCommand>();
            services.AddTransient<IUpdateIngredientCommand, EFUpdateIngredientCommand>();
            services.AddTransient<IDeleteIngedientCommand, EFDeleteIntergrientCommand>();
            services.AddTransient<IGetIngredientQuery, EFGetIngredientQuery>();
            services.AddTransient<IGetIngredientsQuery, EFGetIngredientsQuery>();

            services.AddTransient<IAddDishTypeCommand, EFAddDishTypeCommand>();
            services.AddTransient<IUpdateDishTypeCommand, EFUpdateDishTypeCommand>();
            services.AddTransient<IDeleteDishTypeCommand, EFDeleteDishTypeCommand>();
            services.AddTransient<IGetDishTypeQuery, EFGetDishTypeQuery>();
            services.AddTransient<IGetDishTypesQuery, EFGetDishTypesQuery>();

            services.AddSingleton
                <IPasswordHashing, HashingPassword>((service) => new HashingPassword(new RNGCryptoServiceProvider()));

            var section = Configuration.GetSection("Email");

            var sender = new SmtpEmailSender(section["host"], Int32.Parse(section["port"]), section["fromaddress"], section["password"]);

            services.AddSingleton<IEmailSender>(sender);

            string key = Configuration.GetSection("JwtKey").Value;

            byte[] keyBytes = Encoding.ASCII.GetBytes(key);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };
            });

            //services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            //services.AddTransient<IRegisterUserCommand, EFRegisterUserCommand>();
            //services.AddTransient<IAuthUserCommand, EFAuthUserCommand>();

            //var key = Configuration.GetSection("Encryption")["key"];
            //var enc = new Encryption(key);
            //services.AddSingleton(enc);


            //services.AddTransient(s => {
            //    var http = s.GetRequiredService<IHttpContextAccessor>();
            //    var value = http.HttpContext.Request.Headers["Authorization"].ToString();
            //    var encryption = s.GetRequiredService<Encryption>();

            //    try
            //    {
            //        var decodedString = encryption.DecryptString(value);
            //        decodedString = decodedString.Replace("\t", "");
            //        var user = JsonConvert.DeserializeObject<LoggedUser>(decodedString);
            //        user.IsLogged = true;
            //        return user;
            //    }
            //    catch (Exception)
            //    {
            //        return new LoggedUser
            //        {
            //            IsLogged = false
            //        };
            //    }
            //});



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseCors(policy =>
                {
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                    policy.AllowAnyOrigin();
                });
            }
            else
            {
                app.UseHsts();
            }


            app.UseHttpsRedirection();

            app.UseRouting();

         //   app.UseAuthorization();
            app.UseStaticFiles();

            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "FoodyAPI V1");
            });

             app.UseEndpoints(endpoints =>
             {
                 endpoints.MapControllers();
             });
        }
    }
}
