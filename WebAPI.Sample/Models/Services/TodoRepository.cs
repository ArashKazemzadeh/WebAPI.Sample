using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi.Bugeto.Models.Contexts;
using WebApi.Bugeto.Models.Entities;
using WebAPI.Sample.Models.Dtos;

namespace WebApi.Bugeto.Models.Services
{
    public class TodoRepository
    {
        private readonly DataBaseContext _context;
        public TodoRepository(DataBaseContext context)
        {
            _context = context;
        }

        public List<TodoDto> GetAll()
        {
            return _context.ToDos.Select(p => new TodoDto
            {
                Id = p.Id,
                InsertTime = p.InsertTime,
                IsRemoved = p.IsRemoved,
                Text = p.Text
            }).ToList();
        }
        public TodoDto Get(int Id)
        {
            var todo = _context.ToDos.SingleOrDefault(p => p.Id == Id);
            return new TodoDto()
            {
                Id = todo.Id,
                InsertTime = todo.InsertTime,
                IsRemoved = todo.IsRemoved,
                Text = todo.Text,
            };
        }

        public AddToDoDto Add(AddToDoDto todo)
        {
            ToDo newToDo = new ToDo()
            {
                Id = todo.Todo.Id,
                InsertTime = DateTime.Now,
                IsRemoved = false,
                Text = todo.Todo.Text,

            };
            foreach (var item in todo.Categories)
            {
                var category = _context.Categories.SingleOrDefault(p => p.Id == item);
                newToDo.Categories.Add(category);
            }
            _context.ToDos.Add(newToDo);
            _context.SaveChanges();
            return new AddToDoDto
            {
                Todo = new TodoDto
                {
                    Id = newToDo.Id,
                    InsertTime = newToDo.InsertTime,
                    IsRemoved = newToDo.IsRemoved,
                    Text = newToDo.Text,
                }
                  ,
                Categories = todo.Categories
            };
        }

        public void Delete(int id)
        {
          var todo=  _context.ToDos.SingleOrDefault(t => t.Id == id);
          todo.IsRemoved = true;
            _context.SaveChanges();
        }

        public bool Edit(EditToDoDto edit)
        {
            var todo = _context.ToDos.SingleOrDefault(p => p.Id == edit.Id);
            todo.Text = edit.Text;
            _context.SaveChanges();
            return true;
        }

    }


    
}
