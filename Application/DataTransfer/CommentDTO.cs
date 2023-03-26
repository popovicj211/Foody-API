namespace Application.DataTransfer
{
    public class CommentDTO : BaseDTO
    {
        public CommentDTO()
        {
            SubComments = new List<CommentDTO>();
        }

        public string Content { get; set; }
        public int? ParentId { get; set; }
        public CommentDTO Parent { get; set; }
        public int UserId { get; set; }
        public UserDTO User { get; set; }
        public List<DishCommentDTO> DishComments { get; set; }
        public List<CommentDTO> SubComments { get; set; }
    }
}
