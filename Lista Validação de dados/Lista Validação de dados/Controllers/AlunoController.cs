namespace Lista_Validação_de_dados.Controllers
{
    using Lista_Validação_de_dados.DTOs;
    using Lista_Validação_de_dados.Models;
    using Lista_Validação_de_dados.Persistence;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController : ControllerBase
    {
        private readonly IAlunoRepository _repo;
        public AlunoController(IAlunoRepository repo) => _repo = repo;

        [HttpGet]
        public ActionResult<IEnumerable<Aluno>> GetAll() => Ok(_repo.GetAll());

        [HttpGet("{ra}")]
        public ActionResult<Aluno> GetByRa(string ra)
        {
            var a = _repo.GetByRa(ra);
            if (a == null) return NotFound();
            return Ok(a);
        }

        [HttpPost]
        public ActionResult Create([FromBody] AlunoCreateDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var aluno = new Aluno
            {
                Ra = dto.Ra.Trim().ToUpperInvariant(),
                Nome = dto.Nome.Trim(),
                Email = dto.Email.Trim(),
                Cpf = dto.Cpf,
                Ativo = dto.Ativo
            };

            try
            {
                _repo.Add(aluno);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }

            return CreatedAtAction(nameof(GetByRa), new { ra = aluno.Ra }, aluno);
        }

        [HttpPut("{ra}")]
        public ActionResult Update(string ra, [FromBody] AlunoUpdateDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var aluno = new Aluno
            {
                Nome = dto.Nome.Trim(),
                Email = dto.Email.Trim(),
                Cpf = dto.Cpf,
                Ativo = dto.Ativo
            };

            var updated = _repo.Update(ra, aluno);
            if (!updated) return NotFound();
            return NoContent();
        }

        [HttpDelete("{ra}")]
        public ActionResult Delete(string ra)
        {
            var removed = _repo.Remove(ra);
            if (!removed) return NotFound();
            return NoContent();
        }
    }
}
}

