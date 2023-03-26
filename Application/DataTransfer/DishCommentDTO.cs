namespace Application.DataTransfer
{
    public class DishCommentDTO : BaseDTO
    {
        public DishCommentDTO()
        {
            CommentIds = new List<int>();
        }

        public List<int> CommentIds { get; set; }
        public int DishId { get; set; } 
        public int CommentId { get; set; }
        public DishDTO Dish { get; set; }
        public CommentDTO Comment { get; set; }
    }
}
