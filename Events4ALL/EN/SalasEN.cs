﻿using System;
using Events4ALL.CAD;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Events4ALL.EN
{
    class SalasEN
    {
        #region variables
            private int numSala;
            private string tipoSala;
            private int numSecciones;
            private string descripcion;
            private int aforo;
            private int estadoSala;
            private int[][] secciones;
        #endregion

        #region set y get
            public int NumSala
            {
                get { return numSala; }
                set { numSala = value; }
            }
            public int[][] Secciones
            {
                get { return secciones; }
                set { secciones = value; }
            }
            public int EstadoSala
            {
                get { return estadoSala; }
                set { estadoSala = value; }
            }
            public int Aforo
            {
                get { return aforo; }
                set {aforo=value;}
            }
            public int NumSecciones
            {
                get { return numSecciones; }
                set { numSecciones = value; }
            }
            public string TipoSala
            {
                get { return tipoSala; }
                set { tipoSala = value; }
            }
            public string Descripcion
            {
                get { return descripcion; }
                set { descripcion = value; }
            }
        #endregion

            public SalasEN()
            {
                
            }

            public SalasEN(int numSala_c,string tipoSala_c,int numSecciones_c,string descripcion_c,int aforo_c,int[][] secciones_c)
            {
                numSala = numSala_c;
                tipoSala = tipoSala_c;
                numSecciones = numSecciones_c;
                descripcion = descripcion_c;
                aforo = aforo_c;
                estadoSala = 1; //1 quiere decir que la sala esta libre
                secciones = secciones_c;
            }

            public bool InsertarEn()
            {
                SalasCAD sala = new SalasCAD();
                if (sala.InsertarSala(this) == true)
                    return true;
                else
                    return false;
            }

            public void UpdateSala()
            {
                SalasCAD sala = new SalasCAD();
                sala.UpdateCAD(this);
            }
            
            public int acutalizarId()
            {
                int id = 0;
                SalasCAD sala = new SalasCAD();
                id=sala.SacarIdSala();

                return id;
            }

            public DataSet SalasPorTipo(String tipo)
            {
                SalasCAD sala = new SalasCAD();
                DataSet salasTipo = sala.SalasPorTipo(tipo);
                return salasTipo;
            }
            public DataSet SalaSelect(int idSala, String tipo, int aforo_min, int aforo_max, int estado)
            {
                SalasCAD sala = new SalasCAD();
                DataSet selectSala = sala.SalaSelectCAD(idSala,tipo,aforo_min,aforo_max,estado);
                return selectSala;
            }

            public DataSet RecuperarSala(string id)
            {
                SalasCAD salaCad = new SalasCAD();
                return salaCad.SacarSala(id);
            }
    }
}
