using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dapper;
using Registro.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;

namespace Registro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {


       
        //private UserManager<usuario> _userManager;
        //private SignInManager<usuario> _singInManager;
        //private readonly Autenticacao _appSettings;

        //public UsuarioController(UserManager<usuario> userManager, SignInManager<usuario> signInManager, IOptions<Autenticacao> appSettings)
        //{
        //    _userManager = userManager;
        //    _singInManager = signInManager;
        //    _appSettings = appSettings.Value;
        //}


        [HttpGet]
        public ActionResult Listar()
        {
            return Ok(Data.Listar<UsuarioModel>("ListarUsuarios"));
        }

        [HttpGet("{id}")]
        public IActionResult AdicionarEditar (int id = 0)
        {
            if (id == 0)
                return NotFound();
            else
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@UsuarioID", id);
                return Ok(Data.Listar<UsuarioModel>("SelecionarUsuario", param).FirstOrDefault<UsuarioModel>());
            }
        }

        [HttpPost]
        public IActionResult AdicionarEditar(UsuarioModel usu)
        {
         
            
                DynamicParameters param = new DynamicParameters();
                param.Add("@UsuarioID", usu.UsuarioID);
                param.Add("@Nome", usu.Nome);
                param.Add("@Senha", usu.Senha);
                param.Add("@ConfirmarSenha", usu.ConfirmarSenha);

            Data.Executar("AdicionarEditarUsuario", param);
            return Ok();
            
        }


        [HttpGet("deletar/{id}")]
        public IActionResult Deletar(int id = 0)
        {
          
                DynamicParameters param = new DynamicParameters();
                param.Add("@UsuarioID", id);
                Data.Executar("DeletarUsuario", param);
                return Ok();
            
        }



        [HttpPost("Login")]
        [ProducesResponseType(200, Type = typeof(JwtSecurityTokenHandler))]
        [ProducesResponseType(400)]
        public IActionResult Login([FromBody] UsuarioModel userParam )
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@Nome", userParam.Nome);
            param.Add("@Senha", userParam.Senha);

            
            var user = Data.Listar<UsuarioModel>("Login", param).FirstOrDefault<UsuarioModel>();  

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

          

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MySecretKey010203"));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokenOptions = new JwtSecurityToken(
                claims: new List<Claim> {
                new Claim(ClaimTypes.Name, user.Nome),
             
                },
                expires: DateTime.Now.AddDays(2),
                signingCredentials: signinCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return Ok(new { Token = tokenString , Usuario = user.Nome, Id = user.UsuarioID, data = DateTime.Now.ToShortDateString()});

        }


        [HttpGet("Buscar/{nome}")]
        public IActionResult BuscarPeloNome(string nome)
        {
                DynamicParameters param = new DynamicParameters();
                param.Add("@Nome", nome);
                return Ok(Data.Listar<UsuarioModel>("BuscarPeloNome", param).FirstOrDefault<UsuarioModel>());
            
        }

     

    }
}