using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Sample.Models.Dtos;
using WebAPI.Sample.Models.Services;

namespace WebAPI.Sample.Controllers.V2
{
    [ApiVersion("2")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class CategoriesController : V1.CategoriesController
    {
        private readonly CategoryRepository _categoryRepository;
        public CategoriesController(CategoryRepository categoryRepository)
            : base(categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public override  IActionResult Get()
        {
            return Ok(_categoryRepository.GetAll());
        }

        [HttpGet("{Id}")]
        public override IActionResult Get(int Id)
        {
            return Ok(_categoryRepository.Find(Id));
        }
        [HttpPut]

        public override IActionResult Put(CategoryDto categoryDto)
        {
            return Ok(_categoryRepository.Edit(categoryDto));
        }

        [HttpPost]
        public override IActionResult Post(string Name)
        {
            var result = _categoryRepository.AddCategory(Name);
            return Created(Url.Action(nameof(Get), "Categories", new { Id = result }, Request.Scheme), true);

        }

        [HttpDelete]
        public override IActionResult Delete(int Id)
        {
            return Ok(_categoryRepository.Delete(Id));
        }
    }
}

