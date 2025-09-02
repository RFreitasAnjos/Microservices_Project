using Educational_Victoria.DTOs.SubjectDto;
using Educational_Victoria.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Educational_Victoria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class SubjectsController : ControllerBase
    {
        private readonly ISubjectService _subjectService;

        public SubjectsController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        /// <summary>
        /// Retorna todos os materiais disponíveis.
        /// </summary>
        /// <returns>Lista de materiais.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllAsync()
        {
            var subjects = await _subjectService.GetAllAsync();
            return Ok(subjects);
        }

        /// <summary>
        /// Busca um material pelo ID.
        /// </summary>
        /// <param name="id">ID do material.</param>
        /// <returns>O material encontrado ou NotFound.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSubjectByIdAsync([FromRoute] int id)
        {
            var subject = await _subjectService.GetByIdAsync(id);
            if (subject == null)
                return NotFound("Material não encontrado.");
            return Ok(subject);
        }

        /// <summary>
        /// Cria um novo material.
        /// </summary>
        /// <param name="createSubjectDto">Dados do material a ser criado.</param>
        /// <returns>O material criado.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateSubject([FromBody] CreateSubjectDto createSubjectDto)
        {
            var newSubject = await _subjectService.CreateAsync(createSubjectDto);
            return Ok(newSubject);
        }

        /// <summary>
        /// Atualiza um material existente.
        /// </summary>
        /// <param name="id">ID do material.</param>
        /// <param name="updateSubjectDto">Novos dados do material.</param>
        /// <returns>O material atualizado.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateSubject([FromRoute] int id, [FromBody] UpdateSubjectDto updateSubjectDto)
        {
            var updatedSubject = await _subjectService.UpdateAsync(id, updateSubjectDto);
            if (updatedSubject == null)
                return NotFound("Material não encontrado para atualização.");
            return Ok(updatedSubject);
        }

        /// <summary>
        /// Remove um material pelo ID.
        /// </summary>
        /// <param name="id">ID do material.</param>
        /// <returns>Confirmação da exclusão.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteSubject([FromRoute] int id)
        {
            var deletedSubject = await _subjectService.DeleteAsync(id);
            if (deletedSubject == null)
                return NotFound("Material não encontrado para exclusão.");
            return Ok(deletedSubject);
        }
    }
}
