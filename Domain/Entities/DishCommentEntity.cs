namespace Domain.Entities
{
    public class DishCommentEntity : BaseEntity
    {
        public int DishId { get; set; }
        public DishEntity Dish { get; set;}
        public int CommentId { get; set; }
        public CommentEntity Comment { get; set; }
    }
}
