﻿using System;
using System.Collections.Generic;
using System.Linq;
using Events4ALL.Auxiliares;
using System.Text;
using System.Data.SqlClient;
using Events4ALL.EN;
using System.Data;

namespace Events4ALL.CAD
{
    class AdminCAD
    {
        public AdminCAD()
        {   
        }

        #region Variables

        #endregion

        #region Consultas BD

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

        // devuelve true si Existe
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

                if (pass == dr[0].ToString())
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

        // pues eso...
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

        #endregion

        #region Iteraccion BD

        public bool InsertarAdmin(AdminEN nuevo)
        {
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

                string sql1 = "INSERT INTO Administrador ";
                string tabla1 = "(Nombre, Apellidos, Usuario, Pass, NIF, ";
                string tabla2 = "FechaNac, Poblacion, Provincia, Pais, ";
                string tabla3 = "Direccion, TfnoFijo, TfnoMovil, Mail, ";
                string tabla4 = "Foto, Estado, Sexo, CP)";
                string sql2 = " VALUES ('";
                string valores1 = nuevo.Nombre + comilla + nuevo.Apellidos + comilla + nuevo.Nick + comilla + nuevo.Pass+comilla+ nuevo.DNI+comilla;
                string valores2 = fecha + comilla + nuevo.Localidad + comilla + nuevo.Provincia + comilla + nuevo.Pais + comilla;
                string valores3 = nuevo.Domicilio + comilla + nuevo.Telefono + comilla + nuevo.Movil + comilla + nuevo.Mail + comilla;
                string valores4 = nuevo.Foto + comilla + nuevo.EC + "'," + nuevo.Sexo +",'"+ nuevo.CP+"'";
                string sql3 = ")";

                string sql = sql1 + tabla1 + tabla2 + tabla3 + tabla4 + sql2 + valores1 + valores2 + valores3 + valores4 + sql3;

                System.Diagnostics.Debug.Write(sql);
                
                SqlCommand com = new SqlCommand(sql, c);
                com.ExecuteNonQuery();

                error = true;
            }
            catch
            { 
            
            }
            finally
            { 
                c.Close();
            }
            return error;
        }

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

        public bool ActualizarAdmin(int id, string dni, string nombre, string apellidos, string pais, string provincia,
                                    string localidad, string domicilio, string cp, string telefono, string movil,
                                    string mail, string ec, string foto, int sexo, string nick, string pass, DateTime fecha,
                                    bool upDNI, bool upNombre, bool upApellido,  bool upPaisbool, bool upProvincia,
                                    bool upLocalidad, bool upDomicilio, bool upCP, bool upTelefono, 
                                    bool upMovil, bool upMail, bool upEC, bool upFoto, bool upSexo, bool upNick, bool upPass, 
                                    bool upFecha)
        {
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
                    if (primero)
                        sql = sql + ", TfnoFijo = '" + telefono + "'";
                    else
                        sql = sql + "TfnoFijo = '" + telefono + "'";

                    primero = true;
                }

                if (upMovil)
                {
                    if (primero)
                        sql = sql + ", TfnoMovil = '" + movil + "'";
                    else
                        sql = sql + "TfnoMovil = '" + movil + "'";

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

                 //if(upFoto)

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
                        sql = sql + ", Pass = '" + pass + "'";
                    else
                        sql = sql + "Pass = '" + pass + "'";

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
                com.ExecuteNonQuery();

                error = true;
            }
            catch
            {

            }
            finally
            {
                c.Close();
            }

            return error;
        }

        public DataSet getAdminByNick(string nick)
        {
            DataSet ds = new DataSet();
            BD bd = new BD();
            SqlConnection c = bd.Connect();

            try
            {
                c.Open();
                SqlDataAdapter da = new SqlDataAdapter("select * from Administrador where Usuario='"+nick+"'", c);
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
    }
}
