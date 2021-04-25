using CapaDatos.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CDPersona
    {
        public bool Create(Persona persona, out string Msj)
        {
            try
            {
                using (CursoMVCEntities contexto = new CursoMVCEntities())
                {
                    contexto.Persona.Add(persona);
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

        public bool Edit(Persona persona, out string Msj)
        {
            try
            {
                using (CursoMVCEntities contexto = new CursoMVCEntities())
                {
                    contexto.Entry(persona).State = System.Data.Entity.EntityState.Modified;
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

        public Persona GetById(int IdPersona)
        {
            using (CursoMVCEntities contexto = new CursoMVCEntities())
            {
                //        Persona _persona;
                //        _persona = contexto.Persona.Find(IdPersona);
                //        _persona = contexto.Persona.Where(x => x.IdPersona == IdPersona).FirstOrDefault();
                return contexto.Persona.Find(IdPersona);
            }
        }
    }
}
