
using AutoMapper;
using Inventory.DTOs.Category;
using Inventory.Entities;
using Inventory.Persistence.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Dtos= Inventory.DTOs.Category;

namespace Inventory.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository=categoryRepository;
            _mapper=mapper;
        }

       [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryRepository.GetAllAsync();
            var categoriesDto= _mapper.Map<List<Dtos.CategoryToListDto>>(categories);
            return Ok(categoriesDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            var categoryDto = _mapper.Map<Dtos.CategoryToListDto>(category);
            return Ok(categoryDto);
        }
        
        [HttpPost]
        public async Task<IActionResult> Post(Dtos.CategoryToCreateDto categoryToCreateDto)
        {
            // var categoryToCreate = new Category
            // {
            //     Name = categoryToCreateDto.Name,
            //     Description = categoryToCreateDto.Description,
            //     CreatedAt = DateTime.Now
            // };
            var categoryToCreate = _mapper.Map<Category>(categoryToCreateDto);
            categoryToCreate.CreatedAt = DateTime.Now;
            var categoryCreated = await _categoryRepository.AddAsync(categoryToCreate);

            // var categoryCreatedDto = new Dtos.CategoryToListDto
            // {
            //     Id=categoryCreated.Id,
            //     Name = categoryCreated.Name,
            //     Description= categoryCreated.Description,
            //     CreatedAt = categoryCreated.CreatedAt,
            //     UpdatedAt = categoryCreated.UpdatedAt
            // };
            var categoryCreatedDto = _mapper.Map<Dtos.CategoryToListDto>(categoryCreated);
            return Ok(categoryCreatedDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Dtos.CategoryToEditDto categoryToEditDto)
        {
            if(id != categoryToEditDto.Id)
                return BadRequest("Error en los datos de entrada");

            var categoryToUpdate = await _categoryRepository.GetByIdAsync(id);
            if(categoryToUpdate is null)
                return BadRequest("Id no encontrado");
            
            _mapper.Map(categoryToEditDto,categoryToUpdate);

            categoryToUpdate.UpdatedAt = DateTime.Now;

            var updated = await _categoryRepository.UpdateAsync(id,categoryToUpdate);

            if(!updated )
                return NoContent();
            
            var category = await _categoryRepository.GetByIdAsync(id);
            var categoryDto = _mapper.Map<Dtos.CategoryToListDto>(category);

           return Ok(categoryDto);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
             var categoryToDelete  = await _categoryRepository.GetByIdAsync(id);

             if(categoryToDelete is null)
                 return NotFound("Registro no encontradop");

            var deleted = await _categoryRepository.DeleteAsync(categoryToDelete);

            if(!deleted)
                return Ok("Registro no borrado. Favor de consultar el log para mas detalle");

            return Ok("Registro borrado correctamente");
        }

    }
}