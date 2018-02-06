using negocio.Componentes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion.Pages.Reports
{
    public partial class reporte_performance_comercial : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
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


        private DataSet GetBateo(string vendedor)
        {
            try
            {
                DataSet ds = new DataSet();
                PerformanceComercial performance = new PerformanceComercial();
                ds = performance.PerformanceComercial_CierreOP_NUEVO(vendedor);
                return ds;
            }
            catch (Exception)
            {
                return new DataSet();
            }
        }


        private DataSet GetCumplimientodeCuota(string vendedor, int año_anterior)
        {
            try
            {
                DataSet ds = new DataSet();
                PerformanceComercial performance = new PerformanceComercial();
                ds = performance.NAVISION_VtaCtoMar_VenDLLS_nuevo(vendedor, año_anterior);
                return ds;
            }
            catch (Exception)
            {
                return new DataSet();
            }
        }

        private DataSet GetVisitas(string vendedor, int año_anterior)
        {
            try
            {
                DataSet ds = new DataSet();
                PerformanceComercial performance = new PerformanceComercial();
                ds = performance.PerformanceComercial_Visitas(vendedor, año_anterior);
                return ds;
            }
            catch (Exception)
            {
                return new DataSet();
            }
        }

        private DataSet Getsuficiencia(string vendedor)
        {
            try
            {
                DataSet ds = new DataSet();
                PerformanceComercial performance = new PerformanceComercial();
                ds = performance.PerformanceComercial_SuficienciaOP_nuevo(vendedor);
                return ds;
            }
            catch (Exception)
            {
                return new DataSet();
            }
        }

        private DataSet GetClientes(string vendedor)
        {
            try
            {
                DataSet ds = new DataSet();
                PerformanceComercial performance = new PerformanceComercial();
                ds = performance.PerformanceComercial_DetalleInventarioOP(vendedor);
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
                lblañoanterior3.Text = (year - 1).ToString();
                lblañoactual3.Text = year.ToString();
                lblañoactual4.Text = year.ToString();
                lblañoactual6.Text = year.ToString();
                lblañoanterior3.Text = (year - 1).ToString();
                lblañoanterior4.Text = (year - 1).ToString();
                lblañoanterior5.Text = (year - 1).ToString();
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


                // % de bateo

                DataSet ds_bateo = GetBateo(vendedor);
                if (ds_bateo != null)
                {
                    repeater_baetos_Detalles.DataSource = ds_bateo.Tables[0];
                    repeater_baetos_Detalles.DataBind();
                    REPEATER_TOTAL_BATEO.DataSource = ds_bateo.Tables[1];
                    REPEATER_TOTAL_BATEO.DataBind();
                    repetaer_promedio_bateo.DataSource = ds_bateo.Tables[2];
                    repetaer_promedio_bateo.DataBind();
                }

                //CUMPLIMIENTO CUOTA
                DataSet ds_cuota = GetCumplimientodeCuota(vendedor, year - 1);
                if (ds_cuota != null)
                {
                    repeater_año_anterior_cumpli_cuota.DataSource = ds_cuota.Tables[0];
                    repeater_año_anterior_cumpli_cuota.DataBind();
                    repeater_año_anterior_cumpli_cuota_total.DataSource = ds_cuota.Tables[1];
                    repeater_año_anterior_cumpli_cuota_total.DataBind();
                    repeater_año_actual_cumpli_cuota.DataSource = ds_cuota.Tables[2];
                    repeater_año_actual_cumpli_cuota.DataBind();
                    repeater_año_actual_cumpli_cuota_total.DataSource = ds_cuota.Tables[3];
                    repeater_año_actual_cumpli_cuota_total.DataBind();
                }


                //VISITAS
                DataSet ds_visitas1 = GetVisitas(vendedor, year - 1);
                DataSet ds_visitas2 = GetVisitas(vendedor, year);
                if (ds_visitas1 != null && ds_visitas2 != null)
                {
                    //año anterior
                    repeater_visitas_añoanterior.DataSource = ds_visitas1.Tables[1];
                    repeater_visitas_añoanterior.DataBind();
                    repeater_visitas_añoanterior_total.DataSource = ds_visitas1.Tables[2];
                    repeater_visitas_añoanterior_total.DataBind();
                    //año actual
                    repeater_visitas_añoactual.DataSource = ds_visitas2.Tables[0];
                    repeater_visitas_añoactual.DataBind();
                    repeater_visitas_añoactual_total.DataSource = ds_visitas2.Tables[2];
                    repeater_visitas_añoactual_total.DataBind();

                }

                //suficiencia
                DataSet ds_sufi = Getsuficiencia(vendedor);
                if (ds_sufi != null)
                {
                    repeater_suficiencia.DataSource = ds_sufi.Tables[0];
                    repeater_suficiencia.DataBind();
                }


                //clientes
                DataSet ds_clients = GetClientes(vendedor);
                if (ds_clients != null)
                {
                    //año anterior
                    repeater_clientes.DataSource = ds_clients.Tables[0];
                    repeater_clientes.DataBind();
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar reporte: "+ex.Message,this);
            }

        }
        private void ModalShow(string modalname)
        {
            System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                             "ModalShow('" + modalname + "');", true);
        }

        private void ModalClose(string modalname)
        {
            System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                             "ModalCloseGlobal('" + modalname + "');", true);
        }
    
        protected void CargarDatosFiltros(string filtro)
        {
            try
            {

                int NumJefe = Convert.ToInt32(Session["NumJefe"]);
                int num_empleado = Convert.ToInt32(Session["num_empleado"]);
                Boolean ver_Todos_los_empleados = Convert.ToBoolean(Session["ver_Todos_los_empleados"]);
                EmpleadosCOM empleados = new EmpleadosCOM();
                bool no_activos = cbxnoactivo.Checked;
                DataSet ds = empleados.sp_listado_empleados(num_empleado, ver_Todos_los_empleados, no_activos);
                DataTable dt_empleados = new DataTable();
                if (filtro != "")
                {
                    DataView dv_empleados = ds.Tables[0].DefaultView;
                    dv_empleados.RowFilter = "nombre like '%" + filtro + "%'";
                    dt_empleados = dv_empleados.ToTable();
                }
                else
                {
                    dt_empleados = ds.Tables[0];
                }
                //DataView dv2 = dt_empleados.DefaultView;
                //dv2.RowFilter = "clave_ventas <> ''";
                DataTable dt_final = dt_empleados;
                ddlempleado_a_consultar.DataValueField = "clave_ventas";
                ddlempleado_a_consultar.DataTextField = "nombre";
                ddlempleado_a_consultar.DataSource = dt_final;
                ddlempleado_a_consultar.DataBind();
                ddlempleado_a_consultar.Enabled = ver_Todos_los_empleados;
            }
            catch (Exception ex)
            {
                Toast.Error("Error al iniciar modal de filtros: " + ex.Message, this);
            }
            finally
            {

                lnkfiltros.Visible = true;
                nkcargandofiltros.Style["display"] = "none";
            }
        }



        protected void lnkfiltros_Click(object sender, EventArgs e)
        {
            CargarDatosFiltros("");
            lnkfiltros.Visible = true;
            nkcargandofiltros.Style["display"] = "none";
            ModalShow("#myModal");
        }

        protected void lnksearch_Click(object sender, EventArgs e)
        {
            string filter = txtfilterempleado.Text;
            try
            {
                if (filter.Length == 0 || filter.Length > 3)
                {
                    CargarDatosFiltros(filter);
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al filtrar empleados: " + ex.Message, this);
            }
            finally
            {
                imgloadempleado.Style["display"] = "none";
                lblbemp.Style["display"] = "none";
            }
        }


        protected void lnkguardar_Click(object sender, EventArgs e)
        {
            try
            {
                string clave_empleado = ddlempleado_a_consultar.SelectedValue.ToString().ToUpper().Trim(); ;
                if (clave_empleado=="")
                {
                    div_reporte.Visible = false;
                    Toast.Error("Seleccione un empleado con Clave de venta, para poder realizar el reporte.", this);
                }
                else
                {
                    lblnombrempleado.Text = ddlempleado_a_consultar.SelectedItem.ToString();
                    int año = DateTime.Now.Year;
                    string Usr = Session["usuario"] as string;
                    CargarReporte(clave_empleado, año);
                    ModalClose("#myModal");
                    div_reporte.Visible = true;
                }
            }
            catch (Exception ex)
            {
                div_reporte.Visible = false;
                Toast.Error("Error al generar el reporte: " + ex.Message, this);
            }
            finally
            {
                lnkguardar.Visible = true; ;
                lnkcargando.Style["display"] = "none";
                div_modalbodyfiltros.Visible = true;
            }
        }
    }
}