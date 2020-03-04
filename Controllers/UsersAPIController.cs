using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using CasaEventos.Data;
using Microsoft.AspNetCore.Identity;
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
            var users = _context.Users.Select(u => new
            {
                u.Id,
                u.Email,
                User.FindFirst("FullName").Value
            })
                .ToList();
            return Ok(users);
        }


        // /// <summary>
        // /// Buscar por ID.
        // /// </summary>
        // [HttpGet("{id}")]
        // public IActionResult GetUser(string email)
        // {
        //     if (id != 0)
        //     {
        //         try
        //         {
        //             IdentityUser user = _context.Users.Select(
        //                 us => new UserTemp
        //                 {
        //                     Id = us.Id,
        //                     Email = us.Email
        //                 }).First(user => user.Email == email);
        //             return Ok(new { user });

        //         }
        //         catch (Exception)
        //         {
        //             Response.StatusCode = 404;
        //             return new ObjectResult("Usuário não encontrado.");
        //         }
        //     }
        //     else
        //     {
        //         Response.StatusCode = 404;
        //         return new ObjectResult("Não existe usuário cadastrado");
        //     }
        // }


        public class UserTemp
        {
            public int Id { get; set; }
            [Required(ErrorMessage = "Campo Email é obrigatório")]
            [EmailAddress]
            public string Email { get; set; }

            [Required(ErrorMessage = "Campo de Senha é obrigatório")]
            [StringLength(100, ErrorMessage = "A {0} precisa ter pelo menos {2} e no maximo {1} caracteres.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Compare("Password", ErrorMessage = "As senhas precisam ser iguais.")]
            public string ConfirmPassword { get; set; }
            public string Nome { get; set; }
        }
    }
}