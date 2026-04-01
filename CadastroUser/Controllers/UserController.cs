using CadastroUser.Data;
using CadastroUser.Models.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CadastroUser.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private AppDbContext _context;
        public UserController(AppDbContext dbContext)
        {
            _context = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserModel>>> ListarUsers()
        {
            var users = await _context.Usuarios.ToListAsync();
            return Ok(users);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<UserModel>> ObterUserId(int id)
        {
            var user = await _context.Usuarios.FindAsync(id);
            if (user == null)
            {
                return NotFound("Usuário não foi encontrado");
            }

            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<UserModel>> Cadastrar(UserModel usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return Ok(usuario);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EditarUser(int id, UserModel usuario)
        {
            try
            {
                var existe = await _context.Usuarios.AnyAsync(u => u.Id == id);

                if (!existe)
                {
                    return NotFound("Id do usuário não corresponde ao id da URL");
                }

                _context.Usuarios.Update(usuario);
                await _context.SaveChangesAsync();
                return Ok(usuario);
            }
            catch(DbUpdateConcurrencyException)
            {
                return BadRequest("Erro ao atualizar");
            }
            
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> ApagarUser(int id)
        {
            var user = _context.Usuarios.Find(id);

            if (user == null)
            {
                return NotFound("Usuário não encontrado");
            }

            _context.Usuarios.Remove(user);
            await _context.SaveChangesAsync();
            return Ok("Usuário apagado com sucesso");
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginRequest login)
        {
            var user = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == login.Email);

            if (user == null || user.Senha != login.Senha)
            {
                return Unauthorized("E-mail ou senha incorretos.");
            }

            return Ok(new { message = "Login realizado com sucesso!", user = user.Nome });
        }

        public class LoginRequest
        {
            public string Email { get; set; }
            public string Senha { get; set; }
        }

    }
}
