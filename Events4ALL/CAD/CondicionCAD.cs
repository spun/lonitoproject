﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Events4ALL.Auxiliares;
using System.Windows.Forms;
using Events4ALL.EN;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace Events4ALL.CAD
{
    
    class CondicionCAD
    {
        private BD bd;
        private DataSet bdvirtual;
        private SqlConnection con;
        private SqlDataAdapter da;
        SqlCommandBuilder cbuilder;

        public CondicionCAD()
        {
            bd = new BD();
            bdvirtual = new DataSet();
            con = bd.Connect();
        }
        //DataTable tabla = new DataTable();

        public DataSet ObtenerTodas()
        {
            //BD bd = new BD();
            //DataSet bdvirtual = new DataSet();
            //SqlConnection con = bd.Connect();
            try
            {
                con.Open();
                da = new SqlDataAdapter("select * from Condicion", con);
                da.Fill(bdvirtual, "Condicion");
                //tabla = bdvirtual.Tables["Condicion"];
            }
            catch
            {
            }
            finally
            {
                con.Close();
            }
            return bdvirtual;
        }

        public void Save()
        {
            //BD bd = new BD();
            //DataSet bdvirtual = new DataSet();
            //SqlConnection con = bd.Connect();
            try
            {
                //SqlDataAdapter da = new SqlDataAdapter() ;
                cbuilder = new SqlCommandBuilder(da);
                da.Update(bdvirtual, "Condicion");
            }
            catch
            {
            }
            finally
            {
            }
        }
    }
}
