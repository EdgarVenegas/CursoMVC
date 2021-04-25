using AutoMapper;
using CapaDatos;
using CapaDatos.EntityFramework;
using Modelos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    class CNUsuario
    {
        private IMapper _imapper;
        public CNUsuario()
        {
            MapperConfiguration mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            _imapper = mapperConfig.CreateMapper();
        }

        public bool Create(UsuarioDTO usuarioDTO, out string Msj)
        {
            try
            {

                CDPersona _cdPersona = new CDPersona();
                CDUsuario _cdUsuario = new CDUsuario();

                //Primera validacion: Que sea un correo unico
                if (_cdUsuario.ExistsCorreo(usuarioDTO.Persona.Email))
                    throw new Exception("El correo electrónico ya está ocupado.");
                //Segundavalidacion: Que sea un correo unico
                if (_cdUsuario.ExistsUsername(usuarioDTO.Username))
                    throw new Exception("El nombre de usario ya está ocupado.");

                //Mapper: Nuevo
                //var PersonaEjemplo = _imapper.Map<PersonaDTO, Persona>(usuarioDTO.Persona);

                //Persona _persona = new Persona();
                //_persona.AMaterno = usuarioDTO.Persona.AMaterno;
                //_persona.APaterno = usuarioDTO.Persona.APaterno;
                //_persona.Email = usuarioDTO.Persona.Email;
                //_persona.FechaCreo = DateTime.Now;
                //_persona.FechaNacimiento = usuarioDTO.Persona.FechaNacimiento;
                //_persona.IdUsuarioCreo = usuarioDTO.IdUsuarioAuditoria;
                //_persona.Nombre = usuarioDTO.Persona.Nombre;
                //_persona.Telefono = usuarioDTO.Persona.Telefono;

                Persona persona = new Persona
                {
                    AMaterno = usuarioDTO.Persona.AMaterno,
                    APaterno = usuarioDTO.Persona.APaterno,
                    Email = usuarioDTO.Persona.Email,
                    FechaCreo = DateTime.Now,
                    FechaNacimiento = usuarioDTO.Persona.FechaNacimiento,
                    IdUsuarioCreo = usuarioDTO.IdUsuarioAuditoria,
                    Nombre = usuarioDTO.Persona.Nombre,
                    Telefono = usuarioDTO.Persona.Telefono
                };
                if (!_cdPersona.Create(persona, out string MsjPersona))
                    throw new Exception(MsjPersona);


                //Usuario _usuario = new Usuario();
                //_usuario.Bloqueado = false;
                //_usuario.Contrasena = usuarioDTO.Contrasena;
                //_usuario.Estatus = true;
                //_usuario.FechaCreo = DateTime.Now;
                //_usuario.IdPersona = persona.IdPersona;
                //_usuario.IntentosBloqueo = 0;
                //_usuario.IdUsuarioCreo = usuarioDTO.IdUsuarioAuditoria;
                //_usuario.Username = usuarioDTO.Username;                

                Usuario _usuario = new Usuario
                {
                    Bloqueado = false,
                    Contrasena = usuarioDTO.Contrasena,
                    Estatus = true,
                    FechaCreo = DateTime.Now,
                    IdPersona = persona.IdPersona,
                    IntentosBloqueo = 0,
                    IdUsuarioCreo = usuarioDTO.IdUsuarioAuditoria,
                    Username = usuarioDTO.Username
                };
                if (!_cdUsuario.Create(_usuario, out string MsjUsuario))
                    throw new Exception(MsjUsuario);

                Msj = "Datos creados con éxito";
                return true;

            }
            catch (Exception ex)
            {
                Msj = ex.Message;
                return false;
            }
        }

        public bool Edit(UsuarioDTO usuarioDTO, out string Msj)
        {

            try
            {
                CDPersona _cdPersona = new CDPersona();
                CDUsuario _cdUsuario = new CDUsuario();

                // Existente
                var _persona = _cdPersona.GetById(usuarioDTO.IdPersona);
                if (!_cdPersona.Edit(_imapper.Map(usuarioDTO.Persona, _persona), out string MsjPersona))
                    throw new Exception(MsjPersona);

                var _usuario = _cdUsuario.GetById(usuarioDTO.IdUsuario);
                if (!_cdUsuario.Edit(_imapper.Map(usuarioDTO, _usuario), out string MsjUsuario))
                    throw new Exception(MsjUsuario);

                Msj = "Datos editados con éxito";
                return true;

            }
            catch (Exception ex)
            {
                Msj = ex.Message;
                return false;
            }

        }

        public bool setBloqueo(int IdUsuario, int IdUsuarioAuditoria, out string Msj)
        {
            try
            {
                CDUsuario _cdUsuario = new CDUsuario();

                var _usuario = _cdUsuario.GetById(IdUsuario);
                _usuario.Bloqueado = !_usuario.Bloqueado.Value;
                _usuario.IntentosBloqueo = 0;
                _usuario.IdUsuarioModifico = IdUsuarioAuditoria;
                _usuario.FechaModifico = DateTime.Now;
                if (!_cdUsuario.Edit(_usuario, out string MsjUsuario))
                    throw new Exception(MsjUsuario);

                Msj = "Datos actualizados con éxito";
                return true;

            }
            catch (Exception ex)
            {
                Msj = ex.Message;
                return false;
            }
        }

        public bool setEstatus(int IdUsuario, int IdUsuarioAuditoria, out string Msj)
        {
            try
            {
                CDUsuario _cdUsuario = new CDUsuario();

                var _usuario = _cdUsuario.GetById(IdUsuario);
                _usuario.Estatus = !_usuario.Estatus.Value;
                _usuario.IdUsuarioModifico = IdUsuarioAuditoria;
                _usuario.FechaModifico = DateTime.Now;
                if (!_cdUsuario.Edit(_usuario, out string MsjUsuario))
                    throw new Exception(MsjUsuario);

                Msj = "Datos actualizads con éxito";
                return true;

            }
            catch (Exception ex)
            {
                Msj = ex.Message;
                return false;
            }
        }

        public List<UsuarioDTO> getAll()
        {
            return _imapper.Map<List<Usuario>, List<UsuarioDTO>>(new CDUsuario().GetAll());
        }

        public UsuarioDTO Login(string Correo, string Contrasena, out string Msj)
        {
            try
            {
                CDUsuario _cdUsuario = new CDUsuario();

                // Primera validacion: Que sea un correo unico. 
                if (!_cdUsuario.ExistsCorreo(Correo))
                    throw new Exception("El correo electrónico no existe.");
                var usuario = _cdUsuario.GetByCorreo(Correo);
                if (usuario.Bloqueado.Value)
                    throw new Exception("El usuario se encuentra bloqueado, contacte a su administrador.");

                //Segunda validacion: Contraseña
                if (!usuario.Contrasena.Equals(Contrasena))
                {
                    usuario.IntentosBloqueo += 1;
                    _cdUsuario.Edit(usuario, out string MsjIntentos);
                    throw new Exception(usuario.Bloqueado.Value ? "Tu cuenta ha sido bloqueada por multiples inicios de sesión fallidos." : "Contraseña incorrecta.");
                }

                Msj = "";
                return _imapper.Map<Usuario, UsuarioDTO>(usuario);

            }
            catch (Exception ex)
            {
                Msj = ex.Message;
                return null;
            }
        }

        public UsuarioDTO GetById(int IdUsuario)
        {
            return _imapper.Map<Usuario, UsuarioDTO>(new CDUsuario().GetById(IdUsuario));
        }

        public List<UsuarioDTO> getActives(string conexion)
        {
            List<UsuarioDTO> lstUsuarios = new List<UsuarioDTO>();
            DataTable tabla = new CDUsuario().getActives(conexion);

            foreach (DataRow row in tabla.Rows)
            {
                lstUsuarios.Add(new UsuarioDTO
                {
                    Bloqueado = (bool)row["Bloqueado"],
                    Estatus = (bool)row["Estatus"],
                    IdUsuario = Convert.ToInt32(row["IdUsaurio"]),
                    Username = row["Username"].ToString(),
                    Persona = new PersonaDTO
                    {
                        AMaterno = row["AMaterno"].ToString(),
                        APaterno = row["APaterno"].ToString(),
                        Email = row["Email"].ToString(),
                        Nombre = row["Nombre"].ToString(),
                        FechaNacimiento = Convert.ToDateTime(row["FechaNacimiento"]),
                        Telefono = row["Telefono"].ToString()
                    }

                });
            }

            return _imapper.Map<List<Usuario>, List<UsuarioDTO>>(new CDUsuario().GetAll());
        }
    }
}
