using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Registro.Models;
using Newtonsoft.Json;

namespace Registro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistroController : ControllerBase
    {
        DynamicParameters param = new DynamicParameters();
       
        

        DateTime Dia = DateTime.Now;
     
      
        [HttpPost("HoraInicio")]
        public IActionResult HoraInicio(UsuarioModel usuario)
        {


                //registro.HoraInicio = Dia.TimeOfDay.Hours.ToString();
                param.Add("@HoraInicio", Dia.ToString("HH:mm"));
                param.Add("@Dia",Dia.ToString());
                param.Add("@UsuarioID", usuario.UsuarioID);
                Data.Executar("HoraInicio", param);
            

            return Ok(  JsonConvert.SerializeObject( Dia.ToString("HH:mm")));

        }

        [HttpPost("SaidaAlmoco")]
        public IActionResult SaidaAlmoco(UsuarioModel usuario)
        {
            param.Add("VoltaAlmoco", null);
            param.Add("HoraSaida", null);
            param.Add("@SaidaAlmoco", Dia.ToString("HH:mm"));
            param.Add("@Dia", Dia.ToString());
            param.Add("@UsuarioID", usuario.UsuarioID);
            Data.Executar("AtualizarRegistro", param);

            return Ok(JsonConvert.SerializeObject(Dia.ToString("HH:mm")));

        }

        [HttpPost("VoltaAlmoco")]
        public IActionResult VoltaAlmoco(UsuarioModel usuario)
        {
            param.Add("VoltaAlmoco", Dia.ToString("HH:mm"));
            param.Add("HoraSaida", null);
            param.Add("@SaidaAlmoco",null);
            param.Add("@Dia", Dia.ToString());
            param.Add("@UsuarioID", usuario.UsuarioID);
            Data.Executar("AtualizarRegistro", param);

            return Ok(JsonConvert.SerializeObject(Dia.ToString("HH:mm")));

        }

        [HttpPost("HoraSaida")]
        public IActionResult HoraSaida(UsuarioModel usuario)
        {
            param.Add("VoltaAlmoco", null);
            param.Add("HoraSaida", Dia.ToString("HH:mm"));
            param.Add("@SaidaAlmoco", null);
            param.Add("@Dia", Dia.ToString());
            param.Add("@UsuarioID", usuario.UsuarioID);
            Data.Executar("AtualizarRegistro", param);

            return Ok(JsonConvert.SerializeObject(Dia.ToString("HH:mm")));

        }



        [HttpGet("Buscar/{id}/{DataInicio}/{DataTermino}")]
        public IActionResult BuscarPeloID(int id, DateTime DataInicio, DateTime DataTermino)
        {
            var zero = false;
            var al = false;
            DynamicParameters param = new DynamicParameters();
            
            param.Add("@DataInicio", DataInicio.ToString("dd/MM/yyyy"));
            param.Add("@DataTermino", DataTermino.ToString("dd/MM/yyyy"));
            param.Add("@UsuarioID", id);
          
            var user = Data.Listar<UsuarioModel>("BuscarRegistro", param).FirstOrDefault<UsuarioModel>();

            if (user.HoraInicio == null)
            {
                user.HoraInicio = "00:00";
                zero = true;
            }

            if (user.SaidaAlmoco == null)
            {
                user.SaidaAlmoco = "00:00";
            }

            if (user.VoltaAlmoco == null)
            {
                user.VoltaAlmoco = "00:00";
                al = true;
            }

            if (user.HoraSaida == null)
            {
                user.HoraSaida = "00:00";
            }
            //var pPeriodo = (TimeSpan.Parse(user.SaidaAlmoco) - TimeSpan.Parse(user.HoraInicio));
            //var SPeriodo = (TimeSpan.Parse(user.HoraSaida) - TimeSpan.Parse(user.VoltaAlmoco));
            //var hTotal = pPeriodo + SPeriodo;

            if (zero == true)
            {

                user.HorasTotal = "00:00:00";
            }
            else if(al == true )
            {
                user.HorasTotal = (TimeSpan.Parse(user.SaidaAlmoco) - TimeSpan.Parse(user.HoraInicio)).ToString();
            }

            else
            {
                user.HorasTotal = (TimeSpan.Parse(user.SaidaAlmoco) - TimeSpan.Parse(user.HoraInicio)
                    + TimeSpan.Parse(user.HoraSaida) - TimeSpan.Parse(user.VoltaAlmoco)).ToString();
            }



            return Ok(user);

        }


    }
}