using Educational_Victoria.Interfaces.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Educational_Victoria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserSubjesctAccessController : ControllerBase
    {
        private readonly IUserSubjectAccessService _service;

        public UserSubjesctAccessController(IUserSubjectAccessService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> GrantAccess(int userId, int subjectId)
        {
            var access = await _service.GrantAccessAsync(userId, subjectId);
            return Ok(access);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetSubjectsByUser(int userId)
        {
            var subjects = await _service.GetSubjectsByUserAsync(userId);
            return Ok(subjects);
        }



        [HttpDelete]
        public async Task<IActionResult> RevokeAccess(int userId, int subjectId)
        {
            var result = await _service.RevokeAccessAsync(userId, subjectId);
            if (!result) return NotFound("Acesso não encontrado.");
            return Ok("Acesso revogado.");
        }
    }
}
