using negocio.Componentes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class reporte_performance_comercial : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarReporte("JVB", DateTime.Now.Year);
            }
        }

        private DataSet GetNuevasOport(string vendedor)
        {
            try
            {
                DataSet ds = new DataSet();
                PerformanceComercial performance = new PerformanceComercial();
                ds = performance.PerformanceComercial_NuevasOP_NUEVO(vendedor);
                return ds;
            }
            catch (Exception)
            {
                return new DataSet();
            }
        }
        private DataSet GetINventarioOportu(string vendedor)
        {
            try
            {
                DataSet ds = new DataSet();
                PerformanceComercial performance = new PerformanceComercial();
                ds = performance.PerformanceComercial_InventarioOP_NUEVO(vendedor);
                return ds;
            }
            catch (Exception)
            {
                return new DataSet();
            }
        }
        private void CargarReporte(string vendedor, int year)
        {
            try
            {
                //nuevas oportunidades
                DataSet ds = GetNuevasOport(vendedor);
                lblañoanterior.Text = (year - 1).ToString();
                lblañoactual.Text = year.ToString();
                lblañoactual2.Text = (year - 1).ToString();
                lblañoanterior2.Text = year.ToString();
                if (ds != null)
                {
                    DataTable dt_totales_nuevas_oportunidades = ds.Tables[0];
                    DataTable dt_detalles_dt_totales_nuevas_oportunidades = ds.Tables[1];

                    repeat_nuevas_oportunidades.DataSource = dt_detalles_dt_totales_nuevas_oportunidades;
                    repeat_nuevas_oportunidades.DataBind();
                    repeater_totales_nuevas_oportunidades.DataSource = dt_totales_nuevas_oportunidades;
                    repeater_totales_nuevas_oportunidades.DataBind();
                }

                //Inventario de oportunidades
                DataSet ds_inp = GetINventarioOportu(vendedor);

                if (ds_inp != null)
                {
                    DataTable dt_totales_nuevas_oportunidades = ds_inp.Tables[0];
                    DataTable dt_detalles_dt_totales_nuevas_oportunidades = ds_inp.Tables[1];
                    DataTable dt_detalles_dt_inv_oportunidades = ds_inp.Tables[2];

                    DataRow dr_totales = dt_totales_nuevas_oportunidades.Rows[0];
                    DataRow dr_totales_añoantes = dt_detalles_dt_totales_nuevas_oportunidades.Rows[0];
                    DataRow dr_totales_añoanterior = dt_detalles_dt_totales_nuevas_oportunidades.Rows[1];
                    DataRow dr_totales_añoactual = dt_detalles_dt_totales_nuevas_oportunidades.Rows[2];


                    DataRow drq1 = dt_detalles_dt_inv_oportunidades.Rows[0];
                    DataRow drq2 = dt_detalles_dt_inv_oportunidades.Rows[1];
                    DataRow drq3 = dt_detalles_dt_inv_oportunidades.Rows[2];
                    DataRow drq4 = dt_detalles_dt_inv_oportunidades.Rows[3];

                    string script = "";
                    script = script + 
                                "<tr>"+
                                    "<td>Anteriores a " + (year - 2).ToString() +"</td> " +
                                    "<td align='center'>"+ dr_totales_añoantes["numop"].ToString()+ "</td> "+
                                    "<td align='right'>" + funciones.ValueMoneyMil(Convert.ToDecimal(dr_totales_añoantes["montoori"]),0) + "</td> " +
                                    "<td align='right'>" + funciones.ValueMoneyMil(Convert.ToDecimal(dr_totales_añoantes["margen"]), 0) + "</td>"+
                                    "<td align='center'>Q1</td>" +
                                    "<td align='center'>" + drq1["numop"].ToString() + " </td> " +
                                    "<td align='right'>" + funciones.ValueMoneyMil(Convert.ToDecimal(drq1["montoori"]), 0) + "</td> " +
                                    "<td align='right'>" + funciones.ValueMoneyMil(Convert.ToDecimal(drq1["margen"]), 0) + "</td> " +
                                    "<td align='center'>" + drq1["numop2"].ToString() + "</td> " +
                                    "<td align='right'>" + funciones.ValueMoneyMil(Convert.ToDecimal(drq1["montoori2"]), 0) + "</td> " +
                                    "<td align='right'>" + funciones.ValueMoneyMil(Convert.ToDecimal(drq1["margen2"]), 0) + "</td>"+
                                " </tr>";
                    script = script +
                             "<tr>" +
                                 "<td>" + (year - 1).ToString() + "</td> " +
                                 "<td align='center'>" + dr_totales_añoanterior["numop"].ToString() + "</td> " +
                                 "<td align='right'>" + funciones.ValueMoneyMil(Convert.ToDecimal(dr_totales_añoanterior["montoori"]), 0) + "</td> " +
                                 "<td align='right'>" + funciones.ValueMoneyMil(Convert.ToDecimal(dr_totales_añoanterior["margen"]), 0) + "</td>" +
                                 "<td align='center'>Q2</td>" +
                                 "<td align='center'>" + drq2["numop"].ToString() + " </td> " +
                                 "<td align='right'>" + funciones.ValueMoneyMil(Convert.ToDecimal(drq2["montoori"]), 0) + "</td> " +
                                 "<td align='right'>" + funciones.ValueMoneyMil(Convert.ToDecimal(drq2["margen"]), 0) + "</td> " +
                                 "<td align='center'>" + drq2["numop2"].ToString() + "</td> " +
                                 "<td align='right'>" + funciones.ValueMoneyMil(Convert.ToDecimal(drq2["montoori2"]), 0) + "</td> " +
                                 "<td align='right'>" + funciones.ValueMoneyMil(Convert.ToDecimal(drq2["margen2"]), 0) + "</td>" +
                             " </tr>";
                    script = script +
                             "<tr>" +
                                 "<td>" + (year).ToString() + "</td> " +
                                 "<td align='center'>" + dr_totales_añoactual["numop"].ToString() + "</td> " +
                                 "<td align='right'>" + funciones.ValueMoneyMil(Convert.ToDecimal(dr_totales_añoactual["montoori"]), 0) + "</td> " +
                                 "<td align='right'>" + funciones.ValueMoneyMil(Convert.ToDecimal(dr_totales_añoactual["margen"]), 0) + "</td>" +
                                 "<td align='center'>Q3</td>" +
                                 "<td align='center'>" + drq3["numop"].ToString() + " </td> " +
                                 "<td align='right'>" + funciones.ValueMoneyMil(Convert.ToDecimal(drq3["montoori"]), 0) + "</td> " +
                                 "<td align='right'>" + funciones.ValueMoneyMil(Convert.ToDecimal(drq3["margen"]), 0) + "</td> " +
                                 "<td align='center'>" + drq3["numop2"].ToString() + "</td> " +
                                 "<td align='right'>" + funciones.ValueMoneyMil(Convert.ToDecimal(drq3["montoori2"]), 0) + "</td> " +
                                 "<td align='right'>" + funciones.ValueMoneyMil(Convert.ToDecimal(drq3["margen2"]), 0) + "</td>" +
                             " </tr>";
                    script = script +
                           "<tr>" +
                               "<td><b>Total</b></td> " +
                               "<td align='center'><b>" + dr_totales["numop"].ToString() + "</b></td> " +
                               "<td align='right'><b>" + funciones.ValueMoneyMil(Convert.ToDecimal(dr_totales["montoori"]), 0) + "</b></td> " +
                               "<td align='right'><b>" + funciones.ValueMoneyMil(Convert.ToDecimal(dr_totales["margen"]), 0) + "</b></td>" +
                               "<td align='center'>Q4</td>" +
                               "<td align='center'>" + drq4["numop"].ToString() + " </td> " +
                               "<td align='right'>" + funciones.ValueMoneyMil(Convert.ToDecimal(drq4["montoori"]), 0) + "</td> " +
                               "<td align='right'>" + funciones.ValueMoneyMil(Convert.ToDecimal(drq4["margen"]), 0) + "</td> " +
                               "<td align='center'>" + drq4["numop2"].ToString() + "</td> " +
                               "<td align='right'>" + funciones.ValueMoneyMil(Convert.ToDecimal(drq4["montoori2"]), 0) + "</td> " +
                               "<td align='right'>" + funciones.ValueMoneyMil(Convert.ToDecimal(drq4["margen2"]), 0) + "</td>" +
                           " </tr>";
                    PlaceHolder1.Controls.Add(new Literal {  Text = script});
                }

            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar reporte: "+ex.Message,this);
            }

        }
    }
}