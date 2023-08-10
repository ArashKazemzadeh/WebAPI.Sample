namespace WebAPI.Sample.Models.Dtos;

public class AddToDoDto
{
    public TodoDto Todo { get; set; }
    public List<int> Categories { get; set; }
}