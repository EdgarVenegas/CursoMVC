using CapaDatos.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CDUsuario
    {
        public bool Create(Usuario usuario, out string Msj)
        {
            try
            {
                using (CursoMVCEntities contexto = new CursoMVCEntities())
                {
                    contexto.Usuario.Add(usuario);
                    contexto.SaveChanges();
                }
                Msj = "";
                return true;
            }
            catch (Exception ex)
            {
                Msj = ex.Message;
                return false;
            }
        }

        public bool Edit(Usuario usuario, out string Msj)
        {
            try
            {
                using (CursoMVCEntities contexto = new CursoMVCEntities())
                {
                    contexto.Entry(usuario).State = System.Data.Entity.EntityState.Modified;
                    contexto.SaveChanges();
                }
                Msj = "";
                return true;
            }
            catch (Exception ex)
            {
                Msj = ex.Message;
                return false;
            }
        }

        public Usuario GetById(int IdUsuario)
        {
            using (CursoMVCEntities contexto = new CursoMVCEntities())
            {
                return contexto.Usuario.Find(IdUsuario);
            }
        }

        public Usuario GetByCorreo(string correo)
        {
            using (CursoMVCEntities contexto = new CursoMVCEntities())
            {
                return contexto.Usuario.Where(x => x.Persona.Email.Equals(correo)).FirstOrDefault();
            }
        }

        public bool ExistsCorreo(string correo)
        {
            using (CursoMVCEntities contexto = new CursoMVCEntities())
            {
                return contexto.Usuario.Any(x => x.Persona.Email == correo);
            }
        }

        public bool ExistsUsername(string username)
        {
            using (CursoMVCEntities contexto = new CursoMVCEntities())
            {
                return contexto.Usuario.Any(x => x.Username == username);
            }
        }

        public List<Usuario> GetAll()
        {
            using (CursoMVCEntities contexto = new CursoMVCEntities())
            {
                //No tomar en cuenta las relaciones.
                //contexto.Configuration.LazyLoadingEnabled = false;
                return contexto.Usuario.OrderBy(x => x.Persona.APaterno).ThenBy(x => x.Persona.Nombre).ToList();
            }
        }

        public DataTable getActives(string conexion)
        {
            try
            {
                DataTable tabla = new DataTable();
                using (SqlConnection con = new SqlConnection(conexion))
                {
                    using (SqlCommand cmd = new SqlCommand("spGetActivos", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        //Agregar parametros al SP:
                        //cmd.Parameters.Add("@Parametro", SqlDbType.Int).Value = 1;

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            con.Open();
                            adapter.Fill(tabla);
                            con.Close();
                        }
                    }
                }

                return tabla;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
