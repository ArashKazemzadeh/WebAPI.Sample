using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Sample.Models.Dtos;
using WebAPI.Sample.Models.Services;

namespace WebAPI.Sample.Controllers.V1
{
    [ApiVersion("1", Deprecated = true)]
    [Route("api/[controller]")]
    //[Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly CategoryRepository _categoryRepository;
        public CategoriesController(CategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public virtual IActionResult Get()
        {
            return Ok(_categoryRepository.GetAll());
        }

        [HttpGet("{Id}")]
        public virtual IActionResult Get(int Id)
        {
            return Ok(_categoryRepository.Find(Id));
        }
        [HttpPut]

        public virtual IActionResult Put(CategoryDto categoryDto)
        {
            return Ok(_categoryRepository.Edit(categoryDto));
        }

        [HttpPost]
        public virtual IActionResult Post(string Name)
        {
            var result = _categoryRepository.AddCategory(Name);
            return Created(Url.Action(nameof(Get), "Categories", new { Id = result }, Request.Scheme), true);

        }

        [HttpDelete]
        public virtual IActionResult Delete(int Id)
        {
            return Ok(_categoryRepository.Delete(Id));
        }
    }
}

