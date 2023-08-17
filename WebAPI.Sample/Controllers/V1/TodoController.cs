using Microsoft.AspNetCore.Mvc;
using WebApi.Bugeto.Models.Services;
using WebAPI.Sample.Models.Dtos;

namespace WebAPI.Sample.Controllers.V1
{

    [ApiVersion("1", Deprecated = true)]
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodoRepository _todoRepository;
        public TodoController(TodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }
        // GET: api/<TodoController>
        [HttpGet]
        public virtual IActionResult Get()
        {
            var todo = _todoRepository.GetAll().Select(p => new TodoItemDto
            {
                Id = p.Id,
                InsertTime = p.InsertTime,
                Text = p.Text,
                Links = new List<Link>
                {
                   new Link
                   {
                       Href = Url.Action(nameof(Get), "Todo", new {p.Id}, Request.Scheme),
                       Rel = "Read",
                       HttpVerb ="Get",
                       Action = "Get",
                   },
                   new Link
                   {
                       Href = Url.Action(nameof(Put), "Todo", new {p.Id}, Request.Scheme),
                       Rel = "Update",
                       HttpVerb ="Put",
                       Action = "Edit",
                   },
                   new Link
                   {
                       Href = Url.Action(nameof(Delete), "Todo", new {p.Id}, Request.Scheme),
                       Rel = "Delete",
                       HttpVerb ="Delete",
                       Action = "Delete",
                   },
                }
            }).ToList();

            return Ok(todo);
        }

        // GET api/<TodoController>/5
        [HttpGet("{id}")]
        public virtual IActionResult Get(int id)
        {
            var todo = _todoRepository.Get(id);
            var apiModel = new TodoItemDto
            {
                Id = todo.Id,
                InsertTime = todo.InsertTime,
                Text = todo.Text,
            };
            return Ok(apiModel);
        }

        // POST api/<TodoController>
        [HttpPost]
        public virtual IActionResult Post([FromBody] string value)
        {
            var todoDto = new AddToDoDto
            {
                Todo = new TodoDto
                {
                    Text = value
                },

            };
            var result = _todoRepository.Add(todoDto);
            //ادرس دسترسی به دیتای اضافه شده در هدر سوگر قرار می گیرد
            string url = Url.Action(nameof(Get), "Todo", new { result.Todo.Id }, Request.Scheme);
            return Created(url, result);
        }

        // PUT api/<TodoController>/5
        [HttpPut]
        public virtual IActionResult Put([FromBody] EditToDoDto value)
        {
            var result = _todoRepository.Edit(value);
            if (result) return Ok(result);
            return NotFound();
        }

        // DELETE api/<TodoController>/5
        [HttpDelete("{id}")]
        public virtual IActionResult Delete(int id)
        {
            _todoRepository.Delete(id);
            return Ok();
        }
    }
}
