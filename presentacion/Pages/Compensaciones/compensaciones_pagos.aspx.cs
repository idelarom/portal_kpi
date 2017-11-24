using ClosedXML.Excel;
using datos;
using negocio.Componentes;
using negocio.Componentes.Compensaciones;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion.Pages.Compensaciones
{
    public partial class compensaciones_pagos : System.Web.UI.Page
    {
        #region Funciones

        /// <summary>
        /// Carga los tipos de comentarios
        /// </summary>
        /// <param name="usuario"></param>
        private void CargarCommentarios(DropDownList ddl)
        {
            try
            {
                BonosCOM bonos = new BonosCOM();
                DataSet ds = bonos.sp_Get_Comments_Types_Payments();
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    ddl.DataTextField = "description";
                    ddl.DataValueField = "id_comment_type_payment";
                    ddl.DataSource = ds.Tables[0];
                    ddl.DataBind();
                    
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar los bonos: " + ex.ToString(), this);
            }
            finally
            {
                InitTables();
            }
        }

        /// <summary>
        /// Carga los tipos de bonos
        /// </summary>
        /// <param name="usuario"></param>
        private void CargarBonos()
        {
            try
            {
                BonosCOM bonos = new BonosCOM();
                DataSet ds = bonos.sp_GetRequests_Bonds_Payment();
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    gridBondsPayments.DataSource = ds.Tables[0];
                    gridBondsPayments.DataBind();
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar los bonos: " + ex.ToString(), this);
            }
            finally {
                InitTables();
            }
        }

        /// <summary>
        /// Carga los bonos por empleado
        /// </summary>
        /// <param name="usuario"></param>
        private void CargarBonosEmpleado(int num_employee)
        {
            try
            {
                BonosCOM bonos = new BonosCOM();
                DataSet ds = bonos.sp_GetRequests_Bonds_PaymentDatails();
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    DataView dv = dt.DefaultView;
                    dv.RowFilter = "employee_number = "+num_employee.ToString()+"";
                    DataTable dt_result = dv.ToTable();
                    repetaer_bonds_details.DataSource = dt_result;
                    repetaer_bonds_details.DataBind();
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar los bonos: " + ex.ToString(), this);
            }
            finally
            {
                InitTables();
            }
        }

        /// <summary>
        /// Regresa una tabla de los bonos para imprimir o exportar a excel
        /// </summary>
        /// <returns></returns>
        private DataTable table_view()
        {
            try
            {
                BonosCOM bonos = new BonosCOM();
                DataSet ds = bonos.sp_GetRequests_Bonds_Payment();
                DataTable dt_bonos = new DataTable();
                dt_bonos.Columns.Add("Numero de empleado");
                dt_bonos.Columns.Add("Empleado");
                dt_bonos.Columns.Add("Monto autorizado");
                dt_bonos.Columns.Add("Pagado");
                dt_bonos.Columns.Add("Por pagar");

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    DataRow nrow = dt_bonos.NewRow();
                    nrow["Numero de empleado"] = row["employee_number"].ToString();
                    nrow["Empleado"] = row["full_name"].ToString();
                    nrow["Monto autorizado"] = Convert.ToDecimal(row["authorization_amount"]).ToString("C2");
                    nrow["Pagado"] = Convert.ToDecimal(row["amount_paid"]).ToString("C2");
                    nrow["Por pagar"] = Convert.ToDecimal(row["outstanding_amount_paid"]).ToString("C2");
                    dt_bonos.Rows.Add(nrow);
                }
                return dt_bonos;
            }
            catch (Exception ex)
            {
                return new DataTable();
            }
        }

        /// <summary>
        /// Carga el paginado a todas las tablas
        /// </summary>
        private void InitTables()
        {
            ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "Init('#table_bonos');", true);
        }

        /// <summary>
        /// Desbloquea el hilo actual
        /// </summary>
        private void UnBlockUI()
        {
            ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "UnBlockUI();", true);
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

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarBonos();
            }
        }

        protected void cbxseleccionartodo_CheckedChanged(object sender, EventArgs e)
        {
            foreach (RepeaterItem item in gridBondsPayments.Items)
            {
                CheckBox cbx = item.FindControl("cbxseleccionar") as CheckBox;
                cbx.Checked = cbxseleccionartodo.Checked;
            }
            InitTables();
        }

        protected void gridBondsPayments_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            DropDownList ddl = e.Item.FindControl("ddlcomentarios") as DropDownList;
            CargarCommentarios(ddl);
        }

        protected void lnkprocesar_Click(object sender, EventArgs e)
        {
            try
            {
                BonosCOM bonos = new BonosCOM();
                string payd_by = Session["usuario"] as string;
                int total = 0;
                foreach (RepeaterItem item in gridBondsPayments.Items)
                {
                    CheckBox cbx = item.FindControl("cbxseleccionar") as CheckBox;
                    if (cbx.Checked)
                    {
                        total++;
                        DropDownList ddl = item.FindControl("ddlcomentarios") as DropDownList;
                        int employee_number = Convert.ToInt32(cbx.Attributes["employee_number"]);
                        int id_comment_type_payment = Convert.ToInt32(ddl.SelectedValue);
                        bonos.sp_Payment_Request_BondForEmployeeNumber(payd_by, employee_number, id_comment_type_payment);
                    }
                }

                if (total > 0)
                {

                    System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                     "AlertGO('Se han pagado un total de " + total.ToString() + " bonos correctamente.', 'compensaciones_pagos.aspx');", true);
                }
                else
                {
                    Toast.Error("Error al procesar los bonos: Seleccione al menos 1 bono para pagar.", this);
                }

            }
            catch (Exception ex)
            {
                Toast.Error("Error al procesar los bonos: " + ex.ToString(), this);
            }
            finally
            {
                InitTables();
                UnBlockUI();
            }
        }

        protected void lnkexportar_Click(object sender, EventArgs e)
        {
            try
            {

                DataTable dt_bonos = table_view();
                if (dt_bonos.Rows.Count > 0)
                {
                    Export Export = new Export();
                    //array de DataTables
                    List<DataTable> ListaTables = new List<DataTable>();
                    ListaTables.Add(dt_bonos);
                    //array de nombre de sheets
                    DateTime localDate = DateTime.Now;
                    string date = localDate.ToString();
                    date = date.Replace("/", "_");
                    date = date.Replace(":", "_");
                    date = date.Replace(".", "_");
                    date = date.Replace(" ", "_");
                    string[] Nombres = new string[] { "Reporte Bonos" };
                    string mensaje = Export.toExcel("Reporte Bonos", XLColor.White, XLColor.Black, 18, true, DateTime.Now.ToString(), XLColor.White,
                                           XLColor.Black, 10, ListaTables, XLColor.CelestialBlue, XLColor.White, Nombres, 1,
                                           "reporte_bonos_" + date + ".xlsx", Page.Response);
                    if (mensaje != "")
                    {
                        Toast.Error("Error al exportar el reporte a excel: " + mensaje, this);
                    }
                }
                else {

                    Toast.Info("No hay información para exportar","Mensaje del sistema", this);
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al generar excel: " + ex.ToString(), this);
            }
            finally
            {
                InitTables();
                UnBlockUI();
            }
        }

        protected void lnkimprimir_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Toast.Error("Error al generar vista de impresión: " + ex.ToString(), this);
            }
            finally
            {
                InitTables();
                UnBlockUI();
            }
        }

        protected void btnview_Click(object sender, EventArgs e)
        {
            try
            {
                int num_employee = Convert.ToInt32(hdf_numemployee.Value==""?"0":hdf_numemployee.Value);
                CargarBonosEmpleado(num_employee);
                ModalShow("#modal_detalles");
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar desgloce: " + ex.ToString(), this);
            }
            finally
            {
                InitTables();
                UnBlockUI();
            }
        }

        protected void btncheckedchanged_Click(object sender, EventArgs e)
        {
            foreach (RepeaterItem item in gridBondsPayments.Items)
            {
                CheckBox cbx = item.FindControl("cbxseleccionar") as CheckBox;
                cbx.Checked = cbxseleccionartodo.Checked;
            }
            InitTables();
            UnBlockUI();
        }

        protected void txtamount_topaid_TextChanged(object sender, EventArgs e)
        {
            TextBox txtamount_topaid = sender as TextBox;
            decimal max_amount = Convert.ToDecimal(txtamount_topaid.Attributes["max_amount"]);
            decimal amount = Convert.ToDecimal(txtamount_topaid.Text==""?"0":txtamount_topaid.Text);
            amount = amount > 0 ? amount : 0;
            if (amount == 0)
            {
                Toast.Error("El monto debe ser mayor a $ 0.00", this);
                txtamount_topaid.Text = max_amount.ToString("C2");

            }
            else if (amount > max_amount)
            {
                Toast.Error("El monto no puede ser mayor a " + max_amount.ToString("C2"), this);
                txtamount_topaid.Text = max_amount.ToString("C2");

            }
            else
            {
                txtamount_topaid.Text = amount.ToString("C2");
            }
        }

        protected void btnguardar_Click(object sender, EventArgs e)
        {
            try
            {
                Button btn = sender as Button;
                foreach (RepeaterItem item in repetaer_bonds_details.Items)
                {
                    Button btn_ = item.FindControl("btnguardar") as Button;
                    if (btn == btn_)
                    {
                        TextBox txtamount_topaid = item.FindControl("txtamount_topaid") as TextBox;
                        TextBox txtobservations = item.FindControl("txtobservations") as TextBox;
                        decimal amount = Convert.ToDecimal(txtamount_topaid.Text==""?"":txtamount_topaid.Text.Replace("$","").Replace(",",""));
                        if (amount == 0)
                        {
                            Toast.Error("El monto debe ser mayo a $ 0.00", this);
                            break;
                        }
                        else {
                            string paid_by = Session["usuario"] as string;
                            string observations = txtobservations.Text;
                            int id_request_bond = Convert.ToInt32(btn.Attributes["id_request_bond"]);
                            BonosCOM bonos = new BonosCOM();
                            bonos.sp_Payment_Resquest_BondForID(amount,observations,paid_by,id_request_bond);
                            Toast.Success("El pago parcial o total ha sido realizado exitosamente.", "Mensaje del sistema",this);
                            break;                              
                        }
                    }
                }
                int num_employee = Convert.ToInt32(btn.Attributes["num_employee"]);
                CargarBonos();
                CargarBonosEmpleado(num_employee);
                ModalShow("#modal_detalles");
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar desgloce: " + ex.ToString(), this);
            }
            finally
            {
                InitTables();
                UnBlockUI();
            }

        }
    }
}