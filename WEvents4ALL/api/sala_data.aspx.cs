﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using Entities;
using System.Data;

namespace WEvents4ALL.api
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        public string sJson = "";
        protected void Page_Load(object sender, EventArgs e)
        {            
            SalasEN salaEn = new SalasEN();
            DataSet salaRecuperar = new DataSet();
            VentasEN ventEN = new VentasEN();
            DataSet datosVentas = new DataSet();

            try
            {
                string idEspectaculo = Request.QueryString["id"];
                salaRecuperar = salaEn.RecuperarSalaEspectaculo(idEspectaculo);

                int nSecciones = Convert.ToInt16(salaRecuperar.Tables[0].Rows.Count);

                Dictionary<string, object> dict = new Dictionary<string, object>();
                dict.Add("numSecciones", nSecciones);

                object[] secciones = new object[nSecciones];

                int contSeccion = 0;
                foreach (DataRow seccion in salaRecuperar.Tables[0].Rows)
                {
                    Dictionary<string, object> seccData = new Dictionary<string, object>();
                    seccData.Add("filas", seccion["NumFilas"].ToString());
                    seccData.Add("columnas", seccion["NumColumnas"].ToString());

                    secciones.SetValue(seccData, contSeccion);
                    contSeccion++;
                }
                dict.Add("secciones", secciones);

                datosVentas = ventEN.getVentasEspectaculoId(idEspectaculo);
                int nVentas = Convert.ToInt16(datosVentas.Tables[0].Rows.Count);

                object[] ventas = new object[nVentas];
                int contVentas = 0;
                foreach (DataRow venta in datosVentas.Tables[0].Rows)
                {
                    ventas.SetValue(venta["NumAsiento"].ToString(), contVentas);
                    contVentas++;
                }
                dict.Add("ventas", ventas);

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                sJson = serializer.Serialize((object)dict);


            }
            catch (Exception ex)
            {
                sJson = "error";
            }
        }
    }
}