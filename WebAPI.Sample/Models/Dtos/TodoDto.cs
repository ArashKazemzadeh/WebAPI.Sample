namespace WebAPI.Sample.Models.Dtos
{
    public class TodoDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime InsertTime { get; set; }
        public bool IsRemoved { get; set; }
    }
}
