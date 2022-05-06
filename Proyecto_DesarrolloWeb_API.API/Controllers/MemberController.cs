using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_DesarrolloWeb_API.API.Classes;
using Proyecto_DesarrolloWeb_API.API.Interfaces;
using Proyecto_DesarrolloWeb_API.API.Models;
using Proyecto_DesarrolloWeb_API.Dominio.Core.Entities;
using Proyecto_DesarrolloWeb_API.Dominio.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Proyecto_DesarrolloWeb_API.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IJwtAuth jwtAuth;
        private readonly List<Member> lstMember = new List<Member>()
        {
            new Member{Id=1, Name="Kirtesh" },
            new Member {Id=2, Name="Nitya" },
            new Member{Id=3, Name="pankaj"}
        };
        public MemberController(IJwtAuth jwtAuth)
        {
            this.jwtAuth = jwtAuth;
        }
        // GET: api/<MembersController>
        [AllowAnonymous]
        [HttpGet]
        public IEnumerable<Member> AllMembers()
        {
            return lstMember;
        }

        // GET: api/<MembersController>/GetOne/{email}
        [AllowAnonymous]
        [HttpGet]
        [Route("GetOne/{email}")]
        public async Task<IActionResult> GetOne(string email)
        {
            //string email = "carlos@gmail.com";
            using var data = new PetRescueDBContext();
            var usuario = await data.User.Where(x => x.Email == email).FirstOrDefaultAsync();
            return Ok(usuario);
        }

        // GET api/<MembersController>/5
        [Authorize]
        [HttpGet("{id}")]
        public Member MemberByid(int id)
        {
            return lstMember.Find(x => x.Id == id);
        }

        [AllowAnonymous]
        // POST api/<MembersController>
        [HttpPost("authentication")]
        public async Task<IActionResult> Authentication([FromBody] UserCredential userCredential)
        {
            //Old Code
            /*
            var token = jwtAuth.Authentication(userCredential.UserName, userCredential.Password);
            if (token == null)
                return Unauthorized();
            return Ok(token);
            */
            //New Code
            using var data = new PetRescueDBContext();
            var usuario = await data.User.Where(x => (x.Email == userCredential.UserName && x.Contra == userCredential.Password)).FirstOrDefaultAsync();
            if (usuario == null)
                return Unauthorized();
            if (usuario.Enabled == 0)
                return Unauthorized();
            //return Ok(usuario);
            var token = jwtAuth.Authentication(userCredential.UserName, userCredential.Password);
            if (token == null)
                return Unauthorized();

            var response = new ResponseCustom()
            {
                Code = 200,
                Msg = token,
                Data = usuario
            };
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User usuario)
        {
            if (usuario.Email == null)
            {
                var responseCustom = new ResponseCustom()
                {
                    Code = 400,
                    Msg = "Error Falta el correo",
                    Data = usuario
                };
                return Ok(responseCustom);
            }
            //return BadRequest("Error");
            using var data = new PetRescueDBContext();
            var usuarioTemp = await data.User.Where(x => (x.Email == usuario.Email)).FirstOrDefaultAsync();
            if (usuarioTemp != null)
            {
                var responseCustom = new ResponseCustom()
                {
                    Code = 402,
                    Msg = "Usuario Existente"
                };
                return Ok(responseCustom);
            }
            usuario.Enabled = 0;
            usuario.RegistrationDate = DateTime.Now;
            data.User.Add(usuario);
            await data.SaveChangesAsync();
            var response = new ResponseCustom()
            {
                Code = 200,
                Msg = "Usuario Creado",
                Data = usuario
            };
            return Ok(response);
        }


        [AllowAnonymous]
        [HttpPost("registrarAdopcion/{id}")]
        public async Task<IActionResult> RegistrarAdopcion(int id)
        {
            using var data = new PetRescueDBContext();
            var mascotaTemp = await data.Pet.Where(x => (x.Id == id)).FirstOrDefaultAsync();
            return Ok(mascotaTemp);
        }
    }
}
