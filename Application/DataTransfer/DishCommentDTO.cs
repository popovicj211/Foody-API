using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DataTransfer
{
    public class DishCommentDTO : BaseDTO
    {
        /*  public DishCommentDTO()
          {
              Genres = new List<string>();
          }*/
       // public List<string> Genres { get; set; }
        public int DishId { get; set; } 
        public int CommentId { get; set; }
        public DishDTO Dish { get; set; }
        public CommentDTO Comment { get; set; }
    }
}
