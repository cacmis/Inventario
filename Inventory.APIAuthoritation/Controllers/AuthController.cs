using Inventory.Entities;
using Inventory.DTOs.Auth;
using Inventory.Persistence.Interfaces;
using Inventory.APIAuthoritation.Services.Interfaces;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.APIAuthoritation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        private readonly ITokenService _tokenService;
        public AuthController(IAuthRepository authRepository, ITokenService tokenService)
        {
            _authRepository = authRepository;
            _tokenService = tokenService;
        }
        
        // api/Auth/register
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserToRegisterDto userToRegisterDto)
        {
            if(await _authRepository.UserExists(userToRegisterDto.Email.ToLower()))
                return BadRequest($"Ya existe un usuario registrado con este correo: {userToRegisterDto.Email}");

            var userToCreate = new User {
                Name = userToRegisterDto.Name,
                Email = userToRegisterDto.Email.ToLower(),
                Phone = userToRegisterDto.Phone,
                DateCreated = DateTime.Now,
                Active = true
            };

            var userCreated = await _authRepository.Register(userToCreate, userToRegisterDto.Password);

            UserToListDto userToReturn = new(userCreated.Id, userCreated.Email, userCreated.Name, userCreated.Phone, "");

            return Ok(userToReturn);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserToLoginDto userToLoginDto)
        {
            var userFromRepo = await _authRepository.Login(userToLoginDto.Email.ToLower(), userToLoginDto.Password);
            if (userFromRepo is null)
                return Unauthorized();
            // aun no existe el token por que no lo hemos implemantado
            var token = _tokenService.CreateToken(userFromRepo);
            UserToListDto userToReturn = new(userFromRepo.Id, userFromRepo.Email, userFromRepo.Name, userFromRepo.Phone, token);
            return Ok(userToReturn);
        }

    }
}