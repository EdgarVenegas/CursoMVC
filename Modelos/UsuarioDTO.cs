using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos
{
    public class UsuarioDTO
    {
        public int IdUsuario { get; set; }
        public int IdPersona { get; set; }
        public string Username { get; set; }
        public string Contrasena { get; set; }
        public Nullable<bool> Estatus { get; set; }
        public Nullable<byte> IntentosBloqueo { get; set; }
        public Nullable<bool> Bloqueado { get; set; }
        public int IdUsuarioAuditoria { get; set; }

        public PersonaDTO Persona { get; set; }
    }
}
