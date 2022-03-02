using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ingenio.BO.Ingenio;
using System.IO;
using System.Diagnostics;

namespace Ingenio.DAL.Configuracion
{

    public class ConfiguracionDAL
    {
        // IngenioEntities db;
        public ConfiguracionDAL()
        {

        }
        //Obtener los estados de fundacion y de cofinal 
        public ICollection<Estados> getEstados(int id, bool NoticiaCofiFunda)
        {

            using (var db = new IngenioEntities())
            {
                db.Database.CommandTimeout = 2;
                UsuariosRoles rol = ( from r in db.UsuariosRoles where r.Id_Usuario == id && r.Id_Rol == 1 select r ).FirstOrDefault();
                List<Estados> estados = new List<Estados>();
                if (rol == null)
                {

                    estados = ( from p in db.Estados where NoticiaCofiFunda == false ? p.NoticiaCofiFunda == false : p.NoticiaCofiFunda == true select p ).ToList();
                }
                else
                {
                    if (rol.Id_Rol == 1) //si es admin
                    {
                        estados = ( from p in db.Estados
                                    where
                                    //p.Id_Usuario == id &&
                                    ( NoticiaCofiFunda == false ? p.NoticiaCofiFunda == false : p.NoticiaCofiFunda == true )
                                    select p ).ToList();
                        
                    }
                }
                return estados;
            }

        }

        //metodo crear noticcias para cofinal y fundacion
        public bool CreateNoticia(Estados estados)
        {
            using (var db = new IngenioEntities())
            {

                try
                {
                    Usuarios usuario = ( from usu in db.Usuarios where usu.Id == estados.Id_Usuario select usu ).FirstOrDefault();
                    if (usuario == null)
                    {
                        throw new Excepciones( "Error ASO-01", "No existe el usuario !!" );
                    }
                    Estados estado = ( from est in db.Estados where est.Id == estados.Id select est ).FirstOrDefault();
                    if (estado != null)
                    {
                        throw new Excepciones( "Error PEC-IDE-01", "La noticia ya existe!!" );
                    }

                    //perCarg.registro = ultReg + 1;
                    db.Estados.Add( estados );
                    return db.SaveChanges() == 1 ? true : false;
                }

                catch (Exception e)
                {

                    Trace.WriteLine( "Controlador ConfiguracionDAL, funcion createnoticia  " + e.Message.ToString(), "Error " + DateTime.Now );
                    return false;
                }
            }
        }
        // metiodi pra previsualisar la noticia de fundacion
        public Estados getEstadoIndexFundacion(string Id)
        {
            using (var db = new IngenioEntities())
            {
                try
                {
                    Estados estado = ( from e in db.Estados
                                       where
                                       e.Url == Id && e.NoticiaCofiFunda == true && e.Activo == true
                                       select e ).FirstOrDefault();

                    if (estado == null)
                    {
                        throw new Excepciones( "POR-EDI-001", "No existe la noticia", "No se encontro la noticia" );
                    }

                    return estado;
                }
                catch (Exception e)
                {

                    Trace.WriteLine( "Controlador ConfiguracionDAL, funcion getEstadoIndexFundacion  " + e.Message.ToString(), "Error " + DateTime.Now );
                    return null;
                }
            }
        }

        //metodo para previsualisar la noticia cofinal
        public Estados getEstadoIndexCofinal(string Id)
        {
            using (var db = new IngenioEntities())
            {
                try
                {
                    Estados estado = ( from e in db.Estados
                                       where
                                       e.Url == Id && e.NoticiaCofiFunda == false && e.Activo == true
                                       select e ).FirstOrDefault();

                    if (estado == null)
                    {
                        throw new Excepciones( "POR-EDI-001", "No existe la noticia", "No se encontro la noticia" );
                    }

                    return estado;
                }
                catch (Exception e)
                {

                    Trace.WriteLine( "Controlador ConfiguracionDAL, funcion getEstadoIndexCofinal  " + e.Message.ToString(), "Error " + DateTime.Now );
                    return null;
                }
            }
        }

        //Para mostrar los slider en el home de la fundacion
        public ICollection<Sliders> getSlidersHomeFundacion()
        {
            using (var db = new IngenioEntities())
            {
                ICollection<Sliders> sliders = ( from p in db.Sliders where p.Activo == true && p.Id > 6 select p ).ToList();
                return sliders;
            }
        }

        //para mostrar las noticias en el home de la fundacion
        public ICollection<Estados> getEstadoHomeFudacion()
        {
            using (var db = new IngenioEntities())
            {
                ICollection<Estados> estados = ( from p in db.Estados where p.Activo == true && p.NoticiaCofiFunda == true select p ).ToList();
                return estados;
            }
        }

        //para mostra el sliders en el home
        public ICollection<Sliders> getSlidersHome()
        {
            using (var db = new IngenioEntities())
            {
                ICollection<Sliders> sliders = ( from p in db.Sliders where p.Activo == true && p.Id <= 6 select p ).ToList();
                return sliders;
            }
        }

        public ICollection<Galeria> GetImganes(int id_Usuario)
        {
            using (var db = new IngenioEntities())
            {
                ICollection<Galeria> galerias = ( from g in db.Galeria where g.Id_Usuario == id_Usuario select g ).ToList();
                return galerias;
            }
        }

        public bool DeleteGaleria(int id_Usuario, string src)
        {
            using (var db = new IngenioEntities())
            {
                try
                {
                    Galeria galeria = ( from g in db.Galeria where g.Id_Usuario == id_Usuario && g.RutaImagen == src select g ).FirstOrDefault();
                    db.Galeria.Remove( galeria );
                    return db.SaveChanges() == 1 ? true : false;
                }
                catch (Exception e)
                {
                    Trace.WriteLine( "Controlador ConfiguracionDAL, funcion DeleteGaleria  " + e.Message.ToString(), "Error " + DateTime.Now );
                    return false;
                }
            }
        }

        public bool CreateRutaGaleria(Galeria galeria)
        {
            using (var db = new IngenioEntities())
            {
                db.Galeria.Add( galeria );
                return db.SaveChanges() == 1 ? true : false;
            }
        }

        //metodo para publicar en el home cofinal
        public ICollection<Estados> getEstadoHome()
        {
            using (var db = new IngenioEntities())
            {
                try
                {
                    ICollection<Estados> estados = ( from p in db.Estados where p.Activo == true && p.NoticiaCofiFunda == false select p ).ToList();
                    return estados;
                }
                catch (Exception e)
                {

                    throw;
                }
            }
        }
        //metodo usado para la configuracion de la notica
        public Estados getEstado(int id, int id_user, bool NoticiaCofiFunda)
        {
            using (var db = new IngenioEntities())
            {
                try
                {
                    UsuariosRoles rol = ( from r in db.UsuariosRoles where r.Id_Usuario == id_user && r.Id_Rol == 1 select r ).FirstOrDefault();
                    Estados estado;
                    if (rol != null)
                    {
                        if (NoticiaCofiFunda)
                        {
                            estado = ( from e in db.Estados
                                       where
                                       e.Id == id && e.NoticiaCofiFunda == true
                                       select e ).FirstOrDefault();
                        }
                        else
                        {

                            estado = ( from e in db.Estados
                                       where
                                       e.Id == id && e.NoticiaCofiFunda == false
                                       select e ).FirstOrDefault();
                        }
                    }
                    else
                    {
                        if (NoticiaCofiFunda)
                        {
                            estado = ( from e in db.Estados
                                       where
                                      e.NoticiaCofiFunda == true && e.Id == id && e.Id_Usuario == id_user
                                       select e ).FirstOrDefault();

                        }
                        else
                        {
                            estado = ( from e in db.Estados
                                       where
                                       e.NoticiaCofiFunda == false && e.Id == id && e.Id_Usuario == id_user
                                       select e ).FirstOrDefault();
                        }

                    }
                    if (estado == null)
                    {
                        throw new Excepciones( "POR-EDI-001", "No existe la noticia", "No se encontro la noticia" );
                    }

                    return estado;
                }
                catch (Exception e)
                {
                    Trace.WriteLine( "Controlador ConfiguracionDAL, funcion getEstado  " + e.Message.ToString(), "Error " + DateTime.Now );
                    return null;
                }
            }
        }

        //subir el slider
        public bool UpdateSlider(Sliders d, bool eliminar)
        {
            using (var db = new IngenioEntities())
            {
                try
                {
                    Sliders sl = db.Sliders.Find( d.Id );
                    sl.Descripcion = d.Descripcion;
                    sl.Titulo = d.Titulo;
                    sl.Url = d.Url;
                    if (eliminar == true)
                    {
                        sl.Imagen = d.Imagen == null ? d.Imagen : sl.Imagen;
                    }
                    else
                    {
                        sl.Imagen = d.Imagen != null ? d.Imagen : sl.Imagen;
                    }
                    sl.Posicion = d.Posicion;
                    sl.Activo = d.Activo;
                    int res = db.SaveChanges();
                    return res == 1;
                }
                catch (Exception e)
                {
                    Trace.WriteLine( "Controlador ConfiguracionDAL, funcion UpdateSlider  " + e.Message.ToString(), "Error " + DateTime.Now );
                    return false;
                }
            }
        }

        //obtener el slider
        public Sliders GetItem(int id)
        {
            using (var db = new IngenioEntities())
            {
                return db.Sliders.Find( id );
            }
        }

        //obtener la lista de los sliders aplica para fundacion y confinal
        public ICollection<Sliders> getSliders(bool TipoSlider)
        {
            using (var db = new IngenioEntities())
            {
                ICollection<Sliders> sliders = ( from p in db.Sliders where TipoSlider ? p.Id > 6 : p.Id <= 6 select p ).ToList();
                return sliders;
            }
        }

        //editar las noticias de cofinal y de fundacion
        public bool EditNoticia(Estados estados)
        {
            using (var db = new IngenioEntities())
            {
                try
                {
                    Estados estado = db.Estados.Find( estados.Id );
                    estado.Titulo = estados.Titulo;
                    estado.Html = estados.Html;
                    estado.Fecha = estados.Fecha;
                    estado.FechaInicio = estados.FechaInicio;
                    estado.FechaFin = estados.FechaFin;
                    estado.Activo = estados.Activo;
                    estado.Url = estados.Url;
                    estado.Imagen = estados.Imagen == null ? estado.Imagen : estados.Imagen;
                    int res = db.SaveChanges();
                    return res == 1 ? true : false;
                }
                catch (Exception e)
                {

                    Trace.WriteLine( "Controlador ConfiguracionDAL, funcion EditNoticia  " + e.Message.ToString(), "Error " + DateTime.Now );
                    return false;
                }
            }
        }

        //Elinar la noticia de cofinal y de fundacion
        public bool DeleteEstado(int id)
        {
            using (var db = new IngenioEntities())
            {
                try
                {
                    Estados estado = db.Estados.Find( id );
                    db.Estados.Remove( estado );
                    return db.SaveChanges() == 1 ? true : false;
                }
                catch (Exception e)
                {
                    Trace.WriteLine( "Controlador ConfiguracionDAL, funcion DeleteEstado  " + e.Message.ToString(), "Error " + DateTime.Now );
                    return false;
                }
            }
        }
    }
}
