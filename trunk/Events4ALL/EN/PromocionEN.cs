﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Events4ALL.CAD;
using System.Data;
using System.Collections;

namespace Events4ALL.EN
{
    class PromocionEN
    {

        #region variables
        private int idPromocion;
        private string nombre;
        private int descuento;
        PromocionCAD proCAD;
        #endregion

        #region gets/sets
        public int IdPromocion
        {
            get { return idPromocion; }
            set { idPromocion = value; }
        }

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public int Descuento
        {
            get { return descuento; }
            set { descuento = value; }
        }
        #endregion

        #region constructor por defecto
        public PromocionEN()
        {
            proCAD = new PromocionCAD();
        }
        #endregion

        #region constructor
        public PromocionEN(int id, string nom, int des)
        {
            idPromocion = id;
            nombre = nom;
            descuento = des;
        }
        #endregion

        public DataSet ObtenerEspectaculos()
        {

            DataSet dsPromo;
            dsPromo = proCAD.ObtenerEspectaculos();
            return dsPromo;
        }
    }
}
