﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Events4ALL.CAD;
using Events4ALL.EN;
using System.Collections;
using Events4ALL.Auxiliares;

namespace Events4ALL
{
    public partial class Promociones : UserControl
    {
        public Promociones()
        {
            InitializeComponent();
        }

        private bool cantOtro;

        private void checkBox_MC_ActivarCond1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_MC_ActivarCond1.Checked == true)
            {
                textBox_MC_VC_Cantidad2.Enabled = true;
                textBox_MC_VC_Descuento2.Enabled = true;
                comboBox_MC_VC_Comparacion2.Enabled = true;
                comboBox_MC_VC_Tcondicion2.Enabled = true;
                radioButton_MC_TE2_Cine.Enabled = true;
                radioButton_MC_TE2_Concierto.Enabled = true;
                radioButton_MC_TE2_Teatro.Enabled = true;
                radioButton_MC_TE_Todos2.Enabled = true;
            }
            else
            {
                textBox_MC_VC_Cantidad2.Enabled = false;
                textBox_MC_VC_Descuento2.Enabled = false;
                comboBox_MC_VC_Comparacion2.Enabled = false;
                comboBox_MC_VC_Tcondicion2.Enabled = false;
                radioButton_MC_TE2_Cine.Enabled = false;
                radioButton_MC_TE2_Concierto.Enabled = false;
                radioButton_MC_TE2_Teatro.Enabled = false;
                radioButton_MC_TE_Todos2.Enabled = false;
            }
        }

        private void checkBox_MC_ActivarCond2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_MC_ActivarCond2.Checked == true)
            {
                textBox_MC_VC_Cantidad3.Enabled = true;
                textBox_MC_VC_Descuento3.Enabled = true;
                comboBox_MC_VC_Comparacion3.Enabled = true;
                comboBox_MC_VC_Tcondicion3.Enabled = true;
                radioButton_MC_TE3_Cine.Enabled = true;
                radioButton_MC_TE3_Concierto.Enabled = true;
                radioButton_MC_TE3_Teatro.Enabled = true;
                radioButton_MC_TE_Todos3.Enabled = true;
            }
            else
            {
                textBox_MC_VC_Cantidad3.Enabled = false;
                textBox_MC_VC_Descuento3.Enabled = false;
                comboBox_MC_VC_Comparacion3.Enabled = false;
                comboBox_MC_VC_Tcondicion3.Enabled = false;
                radioButton_MC_TE3_Cine.Enabled = false;
                radioButton_MC_TE3_Concierto.Enabled = false;
                radioButton_MC_TE3_Teatro.Enabled = false;
                radioButton_MC_TE_Todos3.Enabled = false;
            }
        }

        /*
        public bool isCaracterValido(Char c)
        {
            if ((c >= '0' && c <= '9') || (Char)Keys.Back==c)
            {
                return true;
            }
            return false;
        }
        
        
        private void textBox_PE_TOtroDesc_KeyPress(object sender, KeyPressEventArgs e)
        {
             if (!isCaracterValido (e.KeyChar))
             {
                e.Handled = true;
             }
        }

        private void textBox_MC_VC_Valor1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!isCaracterValido(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox_MC_VC_Valor2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!isCaracterValido(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox_MC_VC_Valor3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!isCaracterValido(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        */
        


        private void CompruebaCantidad(string num)
        {
            if (!Auxiliares.Validaciones.EsNumeroEntero(num))
            {
                errorProvider_PE_Otro.SetError(textBox_PE_otroDesc, "Debe ser un número entero");
                cantOtro = false;
            }
            else
            {
                errorProvider_PE_Otro.Clear();
                cantOtro = true;
            }
        }

        private void Promociones_Load(object sender, EventArgs e)
        {
            #region cargar datagridview
            DataSet promos;
            promos = (DataSet)CondicionCAD.Instancia.ObtenerTodas();
            dataGridView_MC_ListaPromosCond.DataSource = promos;
            dataGridView_MC_ListaPromosCond.DataMember = "Condicion";
            dataGridView_MC_ListaPromosCond.Columns[0].Visible = false;
            dataGridView_MC_ListaPromosCond.Columns[2].Visible = false;
            dataGridView_MC_ListaPromosCond.Columns[3].Visible = false;
            dataGridView_MC_ListaPromosCond.Columns[4].Visible = false;
            dataGridView_MC_ListaPromosCond.Columns[5].Visible = false;
            dataGridView_MC_ListaPromosCond.Columns[6].Visible = false;
            dataGridView_MC_ListaPromosCond.Columns[7].Visible = false;
            dataGridView_MC_ListaPromosCond.Columns[8].Visible = false;
            dataGridView_MC_ListaPromosCond.Columns[9].Visible = false;
            dataGridView_MC_ListaPromosCond.Columns[10].Visible = false;
            dataGridView_MC_ListaPromosCond.Columns[11].Visible = false;
            dataGridView_MC_ListaPromosCond.Columns[12].Visible = false;
            dataGridView_MC_ListaPromosCond.Columns[13].Visible = false;
            dataGridView_MC_ListaPromosCond.Columns[14].Visible = false;
            dataGridView_MC_ListaPromosCond.Columns[15].Visible = false;
            dataGridView_MC_ListaPromosCond.Columns[16].Visible = false;
            dataGridView_MC_ListaPromosCond.Columns[17].Visible = false;
            dataGridView_MC_ListaPromosCond.ReadOnly = true;
            #endregion

            #region cargar combobox
            ArrayList todosEsp = (ArrayList)PromocionCAD.Instancia.ObtenerEspectaculos();
            
            foreach (PromocionCAD.Lista obj in todosEsp)
            {
                comboBox_PE_espectaculo.Items.Add(obj.titulo.ToString());
            }
            #endregion
        }

        private void button_PE_Guardar_Click(object sender, EventArgs e)
        {
            if (radioButton_PE_otroDesc.Checked)
            {
                CompruebaCantidad(textBox_PE_otroDesc.ToString());
            }
        }

        private void button_MC_Guardar_Click(object sender, EventArgs e)
        {
            CompruebaCantidad(textBox_MC_VC_Cantidad1.ToString());
            CompruebaCantidad(textBox_MC_VC_Descuento1.ToString());
            if (checkBox_MC_ActivarCond1.Checked)
            {
                CompruebaCantidad(textBox_MC_VC_Cantidad2.ToString());
                CompruebaCantidad(textBox_MC_VC_Descuento2.ToString());
            }
            if (checkBox_MC_ActivarCond2.Checked)
            {
                CompruebaCantidad(textBox_MC_VC_Cantidad3.ToString());
                CompruebaCantidad(textBox_MC_VC_Descuento3.ToString());
            }
        }
    }
}
