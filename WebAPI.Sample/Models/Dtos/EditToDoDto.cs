namespace WebAPI.Sample.Models.Dtos;

public class EditToDoDto
{
    public int Id { get; set; }
    public string Text { get; set; }
    public List<int> Categories { get; set; }
}