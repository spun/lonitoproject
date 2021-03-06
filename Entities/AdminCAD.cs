﻿using System;
using System.Collections.Generic;
using System.Linq;
using Auxiliares;
using System.Text;
using System.Data.SqlClient;
using Entities;
using System.Data;
using System.Drawing;
using System.IO;

namespace Entities
{
    public class AdminCAD
    {
        public AdminCAD()
        {   
        }

        #region Variables

        #endregion

        #region Consultas BD

        // Realiza la Sql de la busqueda y devuelve el Dataset del resultado.
        public DataSet Busqueda(string quebusco, string loquebusco)
        {
            BD bd = new BD();
            SqlConnection c = bd.Connect();
            DataSet bdvirtual = new DataSet();

            try
            {
                string sql = "";

                if(quebusco == "TM")
                    sql = "select ID,NIF,Usuario,Nombre,Apellidos,Mail from Administrador WHERE TfnoFijo LIKE '%" + loquebusco + "%' OR TfnoMovil LIKE '%" + loquebusco + "%'";
                else
                    sql = "select ID,NIF,Usuario,Nombre,Apellidos,Mail from Administrador WHERE " + quebusco + " LIKE '%" + loquebusco + "%'";

                System.Diagnostics.Debug.Write(sql);
                
                SqlDataAdapter da = new SqlDataAdapter(sql, c);
                da.Fill(bdvirtual, "Administrador");
            }
            catch
            {

            }
            finally
            {
                c.Close();
            }

            return bdvirtual;
        }

        // Comprueba que el Nick del usuario no exista. Devuelve True si existe
        public bool CompruebaExistenciaUsuario(string nuevo)
        {
            bool existe = false;
            int valor; 
            
            BD bd = new BD();
            SqlConnection c = bd.Connect();

            try
            {
                c.Open();

                SqlCommand com = new SqlCommand("select count(*) ex from Administrador where Usuario='"+ nuevo +"'" , c);
                SqlDataReader dr = com.ExecuteReader();

                dr.Read();
                
                valor = Convert.ToInt16(dr[0]);

                if (valor != 0)
                    existe = true;

                dr.Close();
            }
            catch
            {}
            finally
            {
                c.Close();
            }
            
            return existe;
        }

        // Devuelve true si existe el DNI en la Base de Datos.
        public bool CompruebaExistenciaDNI(string dni)
        {
            bool existe = false;
            int valor;

            BD bd = new BD();
            SqlConnection c = bd.Connect();

            try
            {
                c.Open();

                SqlCommand com = new SqlCommand("select count(*) ex from Administrador where NIF='" + dni + "'", c);
                SqlDataReader dr = com.ExecuteReader();

                dr.Read();

                valor = Convert.ToInt16(dr[0]);

                if (valor != 0)
                    existe = true;

                dr.Close();
            }
            catch
            { }
            finally
            {
                c.Close();
            }

            return existe;
        }

        // Utilizado para comprobar el pass en la BD de un usuario.
        public bool CompruebaPass(string pass, int id)
        {
            bool concuerda = false;

            BD bd = new BD();
            SqlConnection c = bd.Connect();

            try
            {
                c.Open();

                SqlCommand com = new SqlCommand("select Pass from Administrador where ID='" + id + "'", c);
                SqlDataReader dr = com.ExecuteReader();

                dr.Read();

                string passCod = SHA1helper.Compute(pass);

                if (passCod == dr[0].ToString())
                    concuerda = true;

                dr.Close();
            }
            catch
            { }
            finally
            {
                c.Close();
            }

            return concuerda;
        }

        // Obtienes el ID de un administrador con el nick pasado por parametro
        public int ObtieneID(string usuario)
        {
            int id = -1;

            BD bd = new BD();
            SqlConnection c = bd.Connect();

            try
            {
                c.Open();

                SqlCommand com = new SqlCommand("select ID from Administrador where Usuario='" + usuario + "'", c);
                SqlDataReader dr = com.ExecuteReader();

                dr.Read();

                id = Convert.ToInt16(dr[0]);

                dr.Close();
            }
            catch
            { }
            finally
            {
                c.Close();
            }

            return id;
        }

        // Devuelve todos los admins de la BD en un DataSet.
        public DataSet getAdmins()
        {
            BD bd = new BD();
            SqlConnection c = bd.Connect();
            DataSet bdvirtual = new DataSet();

            try
            {
                string sql = "select ID,NIF,Usuario,Nombre,Apellidos,Mail from Administrador";
                SqlDataAdapter da = new SqlDataAdapter(sql, c);
                da.Fill(bdvirtual, "Administrador");
            }
            catch
            {

            }
            finally
            {
                c.Close();
            }

            return bdvirtual;
        }

        // Devuelve los datos del Adminatrados de la ID pasada por parametro.
        public DataSet getAdmin(int id)
        {
            BD bd = new BD();
            SqlConnection c = bd.Connect();
            DataSet bdvirtual = new DataSet();

            try
            {
                string sql = "SELECT * FROM Administrador WHERE ID='" + id + "'";
                SqlDataAdapter da = new SqlDataAdapter(sql, c);
                da.Fill(bdvirtual, "Administrador");
            }
            catch(Exception ex)
            {
                throw (ex);
            }
            finally
            {
                c.Close();
            }

            return bdvirtual;
        }

        // Devuelve la imagen del Administrador de la ID pasada por parametro.
        public Image ObtieneImagen(int id)
        {
            byte[] bImage = new byte[0];
            Image im = null;
            BD bd = new BD();
            SqlConnection c = bd.Connect();
            try
            {
                c.Open();
                SqlCommand cmd = new SqlCommand("select * from Administrador where ID='" + id + "'", c);
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();

                bImage = (byte[])dr["Foto"];

                if (bImage != null)
                {
                    MemoryStream ms = new MemoryStream(bImage);
                    im = Image.FromStream(ms, true, true);
                }
            }
            catch
            {
            }
            finally
            {
                c.Close();
            }
            return im;
        }

        // Devuelve los datos en forma de DataSet del administrador que contenga como nombre de usuario el nick pasado
        // como parametro.
        public DataSet getAdminByNick(string nick)
        {
            DataSet ds = new DataSet();
            BD bd = new BD();
            SqlConnection c = bd.Connect();

            try
            {
                c.Open();
                SqlDataAdapter da = new SqlDataAdapter("select * from Administrador where Usuario='" + nick + "'", c);
                da.Fill(ds, "Condicion");
            }
            catch
            {
            }
            finally
            {
                c.Close();
            }
            return ds;
        }

        #endregion

        #region Iteraccion BD

        // Inserta el los datos del administrador pasado en el EN por parametro en la base de datos. Devuelve true 
        // o false en funcion de si se ha realizado la inserccion o no.
        public bool InsertarAdmin(AdminEN nuevo)
        {
            byte[] pic = null;
            if (nuevo.Foto != null)
            {
                MemoryStream tmpStream = new MemoryStream();
                nuevo.Foto.Save(tmpStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                tmpStream.Position = 0;
                pic = new byte[tmpStream.Length];
                tmpStream.Read(pic, 0, System.Convert.ToInt32(tmpStream.Length));
                pic = tmpStream.ToArray();
            }

            bool error = false;
            BD bd = new BD();
            SqlConnection c = bd.Connect();

            try
            {
                c.Open();

                string fecha = nuevo.Fecha.ToString();
                string anyo = "" + fecha[6] + fecha[7] + fecha[8] + fecha[9];
                string mes = "" + fecha[3] + fecha[4];
                string dia = "" + fecha[0] + fecha[1];
                fecha = anyo + '/' + mes + '/' + dia;

                string comilla = "', '";

                string tel1 = "";

                if (nuevo.Telefono[3] == ' ')
                {
                    for (int i = 0; i < nuevo.Telefono.Length; i++)
                    {
                        if (nuevo.Telefono[i] != ' ')
                            tel1 = tel1 + nuevo.Telefono[i];
                    }
                }
                else
                    tel1 = nuevo.Telefono;

                string tel2 = "";

                if (nuevo.Movil[3] == ' ')
                {
                    for (int i = 0; i < nuevo.Movil.Length; i++)
                    {
                        if (nuevo.Movil[i] != ' ')
                            tel2 = tel2 + nuevo.Movil[i];
                    }
                }
                else
                    tel2 = nuevo.Movil;

                string sql1 = "INSERT INTO Administrador ";
                string tabla1 = "(Nombre, Apellidos, Usuario, Pass, NIF, ";
                string tabla2 = "FechaNac, Poblacion, Provincia, Pais, ";
                string tabla3 = "Direccion, TfnoFijo, TfnoMovil, Mail, ";
                string tabla4 = "Estado, Sexo, CP";

                if (nuevo.Foto != null)
                    tabla4 = tabla4 + ", Foto)";
                else
                    tabla4 = tabla4 + ")";

                string sql2 = " VALUES ('";
                string valores1 = nuevo.Nombre + comilla + nuevo.Apellidos + comilla + nuevo.Nick + comilla + SHA1helper.Compute(nuevo.Pass) +comilla+ nuevo.DNI+comilla;
                string valores2 = fecha + comilla + nuevo.Localidad + comilla + nuevo.Provincia + comilla + nuevo.Pais + comilla;
                string valores3 = nuevo.Domicilio + comilla + tel1 + comilla + tel2 + comilla + nuevo.Mail + comilla;
                string valores4 = nuevo.EC + "'," + nuevo.Sexo +",'"+ nuevo.CP+"'";

                if (nuevo.Foto != null)
                    valores4 = valores4 + ", @pic";

                string sql3 = ")";

                string sql = sql1 + tabla1 + tabla2 + tabla3 + tabla4 + sql2 + valores1 + valores2 + valores3 + valores4 + sql3;

                System.Diagnostics.Debug.Write(sql);
                
                SqlCommand com = new SqlCommand(sql, c);

                if (nuevo.Foto != null)
                    com.Parameters.AddWithValue("@pic", pic);

                com.ExecuteNonQuery();

                error = true;
            }
            catch(Exception ex)
            {
                // Captura la condición general y la reenvía. 
                throw ex;
            }
            finally
            { 
                c.Close();
            }
            return error;
        }

        // Ejecuta la sentencia SQL de borrado del Admin pasado por ID. Devuelve true o false en funcion de si se ha 
        // podido realizar o no.
        public bool BorraAdmin(int id)
        {
            SqlConnection conn = null;
            BD bd = new BD();

            String comEspectaculo = "DELETE FROM Administrador WHERE ID = '" + id + "'";

            try
            {
                conn = bd.Connect();
                conn.Open();

                SqlCommand com = new SqlCommand(comEspectaculo, conn);
                return com.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                // Captura la condición general y la reenvía. 
                throw ex;
            }
            finally
            {
                if (conn != null) conn.Close(); // Se asegura de cerrar la conexión. 
            }        
        }

        // Actualiza los datos del administrador pasados por parametro. Y solo se actualizaran aquellos donde su booleano
        // este en true. Devuelve true o false en funcion de si se ha realizado la inserccion o no.
        public bool ActualizarAdmin(int id, string dni, string nombre, string apellidos, string pais, string provincia,
                                    string localidad, string domicilio, string cp, string telefono, string movil,
                                    string mail, string ec, Image foto, int sexo, string nick, string pass, DateTime fecha,
                                    bool upDNI, bool upNombre, bool upApellido,  bool upPaisbool, bool upProvincia,
                                    bool upLocalidad, bool upDomicilio, bool upCP, bool upTelefono, 
                                    bool upMovil, bool upMail, bool upEC, bool upFoto, bool upSexo, bool upNick, bool upPass, 
                                    bool upFecha)
        {
            byte[] pic = null;
            if (foto != null)
            {
                MemoryStream tmpStream = new MemoryStream();
                foto.Save(tmpStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                tmpStream.Position = 0;
                pic = new byte[tmpStream.Length];
                tmpStream.Read(pic, 0, System.Convert.ToInt32(tmpStream.Length));
                pic = tmpStream.ToArray();
            }

            bool error = false;
            BD bd = new BD();
            SqlConnection c = bd.Connect();

            try
            {
                c.Open();

                string sql = "UPDATE Administrador SET ";

                #region anidacion de datos

                bool primero = false;

                if (upNombre)
                {
                    sql = sql + "Nombre = '" + nombre + "'";
                    primero = true;
                }

                if (upDNI)
                {
                    if(primero)
                        sql = sql + ", NIF = '" + dni + "'";
                    else
                        sql = sql + "NIF = '" + dni + "'";

                    primero = true;
                }

                if (upApellido)
                {
                    if (primero)
                        sql = sql + ", Apellidos = '" + apellidos + "'";
                    else
                        sql = sql + "Apellidos = '" + apellidos + "'";

                    primero = true;
                }

                if (upPaisbool)
                {
                    if (primero)
                        sql = sql + ", Pais = '" + pais + "'";
                    else
                        sql = sql + "Pais = '" + pais + "'";

                    primero = true;
                }

                if (upProvincia)
                {
                    if (primero)
                        sql = sql + ", Provincia = '" + provincia + "'";
                    else
                        sql = sql + "Provincia = '" + provincia + "'";

                    primero = true;
                }

                if (upLocalidad)
                {
                    if (primero)
                        sql = sql + ", Poblacion = '" + localidad + "'";
                    else
                        sql = sql + "Poblacion = '" + localidad + "'";

                    primero = true;
                }

                if (upDomicilio)
                {
                    if (primero)
                        sql = sql + ", Direccion = '" + domicilio + "'";
                    else
                        sql = sql + "Direccion = '" + domicilio + "'";

                    primero = true;
                }

                if (upCP)
                {
                    if (primero)
                        sql = sql + ", CP = '" + cp + "'";
                    else
                        sql = sql + "CP = '" + cp + "'";

                    primero = true;
                }

                if (upTelefono)
                {
                    string tel1 = "";

                    if (telefono[3] == ' ')
                    {
                        for (int i = 0; i < telefono.Length; i++)
                        {
                            if (telefono[i] != ' ')
                                tel1 = tel1 + telefono[i];
                        }
                    }
                    else
                        tel1 = telefono;

                    if (primero)
                        sql = sql + ", TfnoFijo = '" + tel1 + "'";
                    else
                        sql = sql + "TfnoFijo = '" + tel1 + "'";

                    primero = true;

                    System.Diagnostics.Debug.Write(telefono[3] + " cochino");
                    System.Diagnostics.Debug.Write(tel1 + " cerdo");
                }

                if (upMovil)
                {
                    string tel2 = "";

                    if (movil[3] == ' ')
                    {
                        for (int i = 0; i < movil.Length; i++)
                        {
                            if (movil[i] != ' ')
                                tel2 = tel2 + movil[i];
                        }
                    }
                    else
                        tel2 = movil;
                    
                    if (primero)
                        sql = sql + ", TfnoMovil = '" + tel2 + "'";
                    else
                        sql = sql + "TfnoMovil = '" + tel2 + "'";

                    primero = true;
                }

                if (upMail)
                {
                    if (primero)
                        sql = sql + ", Mail = '" + mail + "'";
                    else
                        sql = sql + "Mail = '" + mail + "'";

                    primero = true;
                }

                if (upEC)
                {
                    if (primero)
                        sql = sql + ", Estado = '" + ec + "'";
                    else
                        sql = sql + "Estado = '" + ec + "'";

                    primero = true;
                }

                if (upFoto)
                {
                    if (primero)
                        sql = sql + ", Foto = @pic";
                    else
                        sql = sql + "Foto = @pic";

                    primero = true;
                }

                if (upSexo)
                {
                    if (primero)
                        sql = sql + ", Sexo = " + sexo;
                    else
                        sql = sql + "Sexo = " + sexo;

                    primero = true;
                }

                if (upNick)
                {
                    if (primero)
                        sql = sql + ", Usuario = '" + nick + "'";
                    else
                        sql = sql + "Usuario = '" + nick + "'";

                    primero = true;
                }

                if (upPass)
                {
                    if (primero)
                        sql = sql + ", Pass = '" + SHA1helper.Compute(pass) + "'";
                    else
                        sql = sql + "Pass = '" + SHA1helper.Compute(pass) + "'";

                    primero = true;
                }

                if (upFecha)
                {
                    string sfecha = fecha.ToString();
                    string anyo = "" + sfecha[6] + sfecha[7] + sfecha[8] + sfecha[9];
                    string mes = "" + sfecha[3] + sfecha[4];
                    string dia = "" + sfecha[0] + sfecha[1];
                    sfecha = anyo + '/' + mes + '/' + dia;

                    if (primero)
                        sql = sql + ", FechaNac = '" + sfecha + "'";
                    else
                        sql = sql + "FechaNac = '" + sfecha + "'";

                    primero = true;
                }
                #endregion

                sql = sql + " WHERE ID='" + id + "'";

                System.Diagnostics.Debug.Write(sql);

                SqlCommand com = new SqlCommand(sql, c);

                if (foto != null)
                    com.Parameters.AddWithValue("@pic", pic);

                com.ExecuteNonQuery();

                error = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                c.Close();
            }

            return error;
        }

        #endregion
    }
}
