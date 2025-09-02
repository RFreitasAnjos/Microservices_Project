using Educational_Victoria.DTOs.UsersDto;
using Educational_Victoria.Interfaces.IServices;
using Educational_Victoria.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Educational_Victoria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        /// <summary>
        /// Retorna todos os usuários.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<User>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllAsync()
        {
            var users = await _usersService.GetAllAsync();
            if (users == null || users.Count == 0)
                return NotFound("Nenhum usuário encontrado");

            return Ok(users);
        }

        /// <summary>
        /// Retorna um usuário pelo ID.
        /// </summary>
        /// <param name="id">ID do usuário</param>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserByIdAsync(int id)
        {
            var user = await _usersService.GetByIdAsync(id);
            if (user == null)
                return NotFound("Usuário não encontrado");

            return Ok(user);
        }

        /// <summary>
        /// Cria um novo usuário.
        /// </summary>
        /// <param name="createUserDto">Dados do usuário</param>
        [HttpPost]
        [ProducesResponseType(typeof(User), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto createUserDto)
        {
            var newUser = await _usersService.CreateAsync(createUserDto);
            // Retorna 201 Created com Location apontando para GetUserByIdAsync
            return Ok(newUser);
        }

        /// <summary>
        /// Atualiza um usuário existente.
        /// </summary>
        /// <param name="id">ID do usuário</param>
        /// <param name="updateUserDto">Novos dados do usuário</param>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserDto updateUserDto)
        {
            var updatedUser = await _usersService.UpdateAsync(id, updateUserDto);
            return Ok(updatedUser);
        }

        /// <summary>
        /// Deleta um usuário pelo ID.
        /// </summary>
        /// <param name="id">ID do usuário</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var deletedUser = await _usersService.DeleteAsync(id);
            if (deletedUser == null)
                return NotFound("Usuário não encontrado");

            return Ok(deletedUser);
        }
    }
}
