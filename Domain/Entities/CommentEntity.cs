using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Domain.Entities
{
    public class CommentEntity : BaseEntity
    {
        public CommentEntity()
        {
           SubComments = new List<CommentEntity>();
        }

        public string Content { get; set; }
        public int? ParentId { get; set; }
        public CommentEntity Parent { get; set; }
        public int UserId { get; set; }
        public UserEntity User { get; set; }
        public ICollection<DishCommentEntity> DishComments { get; set; }
        public ICollection<CommentEntity> SubComments { get; set; }
    }
}
