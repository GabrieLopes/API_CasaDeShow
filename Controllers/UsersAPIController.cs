using System;
using System.Linq;
using CasaEventos.Data;
using Microsoft.AspNetCore.Mvc;

namespace CasaEventos.Controllers
{
    [Route("api/v1/users")]
    [ApiController]
    public class UsersAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public UsersAPIController(ApplicationDbContext context)
        {
            this._context = context;
        }
        /// <summary>
        /// Listar todos os usuários.
        /// </summary>
        [HttpGet]
        public IActionResult GetUsers()
        {
            if (_context.Users.Count() > 0)
            {
                var users = _context.Users.Select(u => new
                {
                    u.Id,
                    u.Email,
                    User.FindFirst("FullName").Value
                })
                    .ToList();
                return Ok(users);
            }
            else
            {
                Response.StatusCode = 404;
                return new ObjectResult("Não existem usuários cadastrados.");
            }
        }


        /// <summary>
        /// Buscar por e-mail.
        /// </summary>
        [HttpGet("{email}")]
        public IActionResult GetUser(string email)
        {
            try
            {
                var user = _context.Users.Select(
                    x => new UserTemp
                    {
                        id = x.Id,
                        email = x.Email,
                        nome = User.FindFirst("FullName").Value
                    }).First(user => user.email == email);
                return Ok(new { user });

            }
            catch (Exception)
            {
                Response.StatusCode = 404;
                return new ObjectResult("Usuário não encontrado, favor inserir um e-mail válido.");
            }
        }


        public class UserTemp
        {
            public string id { get; set; }
            public string email { get; set; }

            public string nome { get; set; }
        }
    }
}