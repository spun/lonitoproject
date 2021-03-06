﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities;
using System.Data;
using System.Drawing;

namespace Entities
{
    public class ClientesEN
    {
        #region variables

        private ClientesCAD cad_cliente;

        //private int id;
        private string nif;
        private string nombre;
        private string apellidos;
        private string pais;
        private string provincia;
        private string localidad;
        private string domicilio;
        private string cp; // codigo postal
        private string telefono;
        private string movil;
        private string mail;
        private string estadoCivil;
        private Image foto;
        private int sexo; // 0 - hombre, 1 - mujer
        private DateTime fecha;
        private string nick;
        private string pass;


        #endregion

        #region Estructuras Set y Get

        public string DNI
        {
            get { return nif; }
            set { nif = value; }
        }

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public string Apellidos
        {
            get { return apellidos; }
            set { apellidos = value; }
        }

        public string Pais
        {
            get { return pais; }
            set { pais = value; }
        }

        public string Provincia
        {
            get { return provincia; }
            set { provincia = value; }
        }

        public string Localidad
        {
            get { return localidad; }
            set { localidad = value; }
        }

        public string Domicilio
        {
            get { return domicilio; }
            set { domicilio = value; }
        }

        public string CP
        {
            get { return cp; }
            set { cp = value; }
        }

        public string Telefono
        {
            get { return telefono; }
            set { telefono = value; }
        }

        public string Movil
        {
            get { return movil; }
            set { movil = value; }
        }

        public string Mail
        {
            get { return mail; }
            set { mail = value; }
        }

        public string EC
        {
            get { return estadoCivil; }
            set { estadoCivil = value; }
        }

        public int Sexo
        {
            get { return sexo; }
            set { sexo = value; }
        }

        public Image Foto
        {
            get { return foto; }
            set { foto = value; }
        }

        public DateTime Fecha
        {
            get { return fecha; }
            set { fecha = value; }
        }

        public string Nick
        {
            get { return nick; }
            set { nick = value; }
        }

        public string Password
        {
            get { return pass; }
            set { pass = value; }
        }

        #endregion

        #region Constructores

        public ClientesEN()
        {
            nif = nombre = apellidos = pais = provincia = localidad = domicilio = cp = telefono = movil = mail = estadoCivil = nick = pass = string.Empty;

            sexo = -1;

            fecha = DateTime.Parse("01/01/2100");

            //foto = Events4ALL.Properties.Resources.foto_usuario_defectojpg;

            foto = null;

            cad_cliente = new ClientesCAD();

        }
        

        public ClientesEN(string dni_c, string nombre_c, string apellidos_c, string pais_c, string provincia_c, string localidad_c, string domicilio_c, string cp_c, string telefono_c, string movil_c, string mail_c,
                          string estadoCivil_c, Image foto_c, int sexo_c, DateTime fecha_c, string nick_c, string pass_c)
        {
            nif = dni_c;
            nombre = nombre_c;
            apellidos = apellidos_c;
            pais = pais_c;
            provincia = provincia_c;
            localidad = localidad_c;
            domicilio = domicilio_c;
            cp = cp_c;
            telefono = telefono_c;
            movil = movil_c;
            mail = mail_c;
            estadoCivil = estadoCivil_c;
            foto = foto_c;
            sexo = sexo_c;
            fecha = fecha_c;
            nick = nick_c;
            pass = pass_c;

            cad_cliente = new ClientesCAD();
    
        }

        #endregion



        #region Interactuando con CAD


        public bool InsertarCliente()
        {
            bool error = false;
            bool dni = false;

            if (cad_cliente.ExisteCliente(this.DNI))
            {
                dni = true;
            }

            if (dni == false)
            {
                error = cad_cliente.InsertarCliente(this);
            }

            return error;

        }

        public bool ActualizarCliente()
        {
            bool error = false;

            error = cad_cliente.ActualizarCliente(this);

            return error;

        }

        public DataSet getClientes()
        {
            return cad_cliente.getClientes();
        }

        public DataSet BusquedaCliente(string campo, string datoAbuscar)
        {
            return cad_cliente.BusquedaCliente(campo, datoAbuscar);
        }


        public DataSet getClienteByNif(string nif)
        {
            return cad_cliente.getClienteByNif(nif);
        }

        public bool ExisteCliente(string nif)
        {
            return cad_cliente.ExisteCliente(nif);
        }

        public Image ObtieneImagen(string nif)
        {
            return cad_cliente.devuelveImagen(nif);
        }

        public void BorrarCliente(string nif)
        {
            cad_cliente = new ClientesCAD();
            bool borrado = false;

            borrado = cad_cliente.BorraCliente(nif);

        }

        public bool CompruebaExistenciaNif(string nif)
        {
            return cad_cliente.ExisteCliente(nif);
        }

        public DataSet ExisteUsuarioNickPass(string nick, string pass)
        {
            return cad_cliente.ExisteUsuarioNickPass(nick, pass);
        }


        #endregion

        // Obtiene un usuario a partir de su de de cliente.
        public DataSet ObtenerUsuarioPorID(string id)
        {
            ClientesCAD cliCAD = new ClientesCAD();
            return cliCAD.ObtenerUsuarioPorID(id);
        }

        // Obtiene una puntuacion de un cliente en un espectaculo.
        public int getPuntuacionEsp(string cliId, string espId)
        {
            ClientesCAD cliCAD = new ClientesCAD();
            return cliCAD.getPuntuacionEsp(cliId, espId);
        }

        #region Made In Lirio 

        public bool ActualizaCliente(ClientesEN cliente, int id)
        {
            return cad_cliente.ActualizaCliente(cliente,id);
        }

        public bool VerificaPassCliente(string pass, int id)
        {
            return cad_cliente.VerificaPassCliente(pass,id);
        }

        #endregion
    }
}
