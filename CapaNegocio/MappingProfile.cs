using AutoMapper;
using CapaDatos.EntityFramework;
using Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<PersonaDTO, Persona>()
                .ForMember(destino => destino.IdUsuarioCreo, origen => origen.Condition(src => src.IdPersona == 0))
                .ForMember(destino => destino.FechaCreo, origen => origen.Condition(src => src.IdPersona == 0))
                .ForMember(destino => destino.IdUsuarioModifico, origen => origen.Condition(src => src.IdPersona > 0))
                .ForMember(destino => destino.FechaModifico, origen => origen.Condition(src => src.IdPersona > 0));
            CreateMap<Persona, PersonaDTO>();

            CreateMap<UsuarioDTO, Usuario>()
                .ForMember(destino => destino.Persona, origen => origen.Ignore());
            CreateMap<Usuario, UsuarioDTO>()
                .ForMember(destino => destino.Contrasena, origen => origen.Ignore());

        }
    }
}
