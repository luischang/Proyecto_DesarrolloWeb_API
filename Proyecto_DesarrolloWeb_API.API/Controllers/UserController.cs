using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_DesarrolloWeb_API.API.Models;
using Proyecto_DesarrolloWeb_API.Dominio.Core.Entities;
using Proyecto_DesarrolloWeb_API.Dominio.Infrastructure.Data;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Proyecto_DesarrolloWeb_API.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [AllowAnonymous]
        [HttpGet]
        [Route("get/{email}")]
        public async Task<IActionResult> GetOne(string email)
        {
            using var data = new PetRescueDBContext();
            ResponseCustom response = null;
            var usuario = await data.User.Where(x => x.Email == email).FirstOrDefaultAsync();
            if (usuario == null)
            {
                response = new ResponseCustom()
                {
                    Code = 400,
                    Msg = ""
                };
            }
            response = new ResponseCustom()
            {
                Code = 200,
                Msg = "Usuario encontrado",
                Data = usuario
            };
            return Ok(response);
            //return Ok(usuario);
        }
        [AllowAnonymous]
        [HttpGet]
        [Route("GetUsers/{type}")]
        public async Task<IActionResult> GetUsers(string type = "all")
        {
            /*
             type = "all"
             type = "aprobados"
             type = "desaprobados"
             */
            using var data = new PetRescueDBContext();
            ResponseCustom response = null;
            object usuario = null;
            if (type == "all")
            {
                usuario = await data.User.ToListAsync();
            }
            else if (type == "aprobados")
            {
                usuario = await data.User.Where(x => x.Enabled == 1).ToListAsync();
            }
            else if (type == "desaprobados")
            {
                usuario = await data.User.Where(x => x.Enabled == 0).ToListAsync();
            }
            if (usuario == null)
            {
                response = new ResponseCustom()
                {
                    Code = 400,
                    Msg = ""
                };
            }
            response = new ResponseCustom()
            {
                Code = 200,
                Msg = "Usuario encontrado",
                Data = usuario
            };
            return Ok(response);
            //return Ok(usuario);
        }

        [AllowAnonymous]
        [HttpPost("update")]
        public async Task<IActionResult> Register([FromBody] User usuario)
        {
            ResponseCustom responseCustom = null;
            if (usuario.Id == 0)
            {
                responseCustom = new ResponseCustom()
                {
                    Code = 400,
                    Msg = "Error Falta ID",
                    Data = usuario
                };
                return Ok(responseCustom);
            }
            if (usuario.Email == null)
            {
                responseCustom = new ResponseCustom()
                {
                    Code = 400,
                    Msg = "Error Faltan datos",
                    Data = usuario
                };
                return Ok(responseCustom);
            }
            using var data = new PetRescueDBContext();
            data.User.Update(usuario);
            await data.SaveChangesAsync();
            responseCustom = new ResponseCustom()
            {
                Code = 200,
                Msg = "Usuario Actualizado",
                Data = usuario
            };
            return Ok(responseCustom);

/*
            var usuarioTemp = await data.User.Where(x => (x.Email == usuario.Email)).FirstOrDefaultAsync();
            if (usuarioTemp != null)
            {
                responseCustom = new ResponseCustom()
                {
                    Code = 402,
                    Msg = "Usuario Existente"
                };
                return Ok(responseCustom);
            }
            data.User.Add(usuario);
            await data.SaveChangesAsync();
            var response = new ResponseCustom()
            {
                Code = 200,
                Msg = "Usuario Creado",
                Data = usuario
            };
            return Ok(response);*/
        }

        [AllowAnonymous]
        [HttpPost("AprobarUser")]
        public async Task<IActionResult> AprobarUser([FromBody] User usuario)
        {
            ResponseCustom responseCustom = null;
            if (usuario.Id == 0)
            {
                responseCustom = new ResponseCustom()
                {
                    Code = 400,
                    Msg = "Error Falta ID",
                    Data = usuario
                };
                return Ok(responseCustom);
            }
            if (usuario.Email == null)
            {
                responseCustom = new ResponseCustom()
                {
                    Code = 400,
                    Msg = "Error Faltan datos",
                    Data = usuario
                };
                return Ok(responseCustom);
            }
            using var data = new PetRescueDBContext();

            var usuario2 = await data.User.Where(x => x.Email == usuario.Email).FirstOrDefaultAsync();
            usuario2.Enabled = 1;
            usuario2.Admin = 0;
            data.User.Update(usuario2);
            await data.SaveChangesAsync();
            responseCustom = new ResponseCustom()
            {
                Code = 200,
                Msg = "Usuario Aprobado",
                Data = usuario
            };
            return Ok(responseCustom);
        }
    }
}
