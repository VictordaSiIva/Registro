using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Registro.Models
{
    public class UsuarioModel
    {
        public int UsuarioID { get; set; }

        public string Nome { get; set; }

        public string Senha { get; set; }

        public string ConfirmarSenha { get; set; }

        public int RegistroID { get; set; }

        public string HoraInicio { get; set; }

        public string SaidaAlmoco { get; set; }
        public string VoltaAlmoco { get; set; }
        public string HoraSaida { get; set; }

        public string HorasTotal { get; set; }
        public string Dia { get; set; }



    }
}
