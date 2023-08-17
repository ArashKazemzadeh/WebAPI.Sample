namespace WebAPI.Sample.Models.Dtos
{
    public class TodoItemDto
    {

        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime InsertTime { get; set; }
        public List<Link> Links { get; set; }
    }
}
