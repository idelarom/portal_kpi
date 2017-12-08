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
    public partial class compensaciones_autorizacion_atomaticos : System.Web.UI.Page
    {
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

        /// <summary>
        /// Desbloquea el hilo actual
        /// </summary>
        private void UnBlockUI()
        {

            load2.Style["display"] = "none";
            ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "UnBlockUI();", true);
        }

        /// <summary>
        /// Carga el paginado a todas las tablas
        /// </summary>
        private void InitTables()
        {
            ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "Init('#table_bonos');", true);
        }

        /// <summary>
        /// Carga el listado de bonos
        /// </summary>
        private void LoadBondsRequests()
        {
            try
            {
                DataSet ds = LoadBondsRequestsData();
                if (ds.Tables.Count > 0)
                {
                    ViewState[hdfguid.Value + "-dt_reporte"] = ds.Tables[0];
                    gridBondsRequisitions.DataSource = ds.Tables[0];
                    gridBondsRequisitions.DataBind();
                }
                else
                {
                    ViewState[hdfguid.Value + "-dt_reporte"] = null;
                    gridBondsRequisitions.DataSource = null;
                    gridBondsRequisitions.DataBind();
                }

                ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "Init('#table_bonos');", true);
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar historial de bonos. ", this);
            }
        }

        private DataSet LoadBondsRequestsData()
        {
            try
            {
                int? id_immediate_boss = null;
                id_immediate_boss = Convert.ToInt32(Session["num_empleado"]);
                if (Convert.ToInt32(Session["id_profile"]) == (int)enumerators.profiles_compensations.administrador)//administrador
                {
                    id_immediate_boss = null;
                }
                BonosCOM bonos = new BonosCOM();
                int? id_tipo_bono = 5;  //null;
                //if (Convert.ToInt32(this.cbBonds_Types.SelectedValue) > 0) { id_tipo_bono = Convert.ToInt32(this.cbBonds_Types.SelectedValue); }
                return bonos.sp_GetRequests_Bonds_Automatics(id_tipo_bono,
                    (int)enumerators.bonds_status.request,
                    null,
                    null,
                    id_immediate_boss);
            }
            catch (Exception ex)
            {
                return new DataSet();
            }
        }

        /// <summary>
        /// Procesa la solicitud de un bono
        /// </summary>
        /// <param name="bono"></param>
        private void ProcessBond(requests_bonds bono)
        {
            try
            {
                BonosCOM bonos = new BonosCOM();
                DataSet ds = bonos.sp_Update_Request_Bond_Automatic(bono);
                string vmensaje = "";
                if (ds.Tables[0].Columns.Contains("mensaje"))
                {
                    vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                }
                if (vmensaje == "")
                {
                    System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                  "AlertGO('El bono del empleado ha sido " + (bono.id_request_status == (int)enumerators.bonds_status.authorization ? "autorizado" : "rechazado") + " con éxito.', 'compensaciones_autorizacion_atomaticos.aspx');", true);
                }
                else
                {
                    Toast.Error("Error al procesar solicitud : " + vmensaje, this);
                }                  
            }
            catch (Exception ex)
            {
                Toast.Error("Error al procesar solicitud: " + ex.Message, this);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string usuario = Session["usuario"] as string;

                //AsyncUpload1.FileUploaded += new Telerik.Web.UI.FileUploadedEventHandler(AsyncUpload1_FileUploaded);
                hdfguid.Value = Guid.NewGuid().ToString();
                BonosCOM bonos = new BonosCOM();
                DataTable dtValidateUser = bonos.sp_Validate_User(usuario).Tables[0];
                if (dtValidateUser.Rows.Count > 0)
                {
                    Session["id_profile"] = Convert.ToInt32(dtValidateUser.Rows[0]["id_profile"]);
                }
                else
                {
                    Session["id_profile"] = 0;
                }
                //CargarTiposBonos(usuario.ToUpper());
               ViewState[hdfguid.Value + "-dt_reporte"] = null;
                LoadBondsRequests();
            }
        }

        protected void btnprocessrequest_Click(object sender, EventArgs e)
        {
            try
            {
                int id_bonds = Convert.ToInt32(hdnIdRequestBond.Value);
                int id_tipo_bono = Convert.ToInt32(hdnid_tipo_bono.Value);
                txtRequisitionNumber.Text = "";
                txtBondType.Text = "";
                txtEmployeeName.Text = "";
                hdnEmployeeNumber.Value = "";
                txtCC_Cargo.Text = "";
                tdAuthorizationComments.Text = "";
                txtAuthorizationAmount.BorderColor = System.Drawing.Color.Green;
                amount_correct.Visible = true;
                amount_error.Visible = false;
                txtCC_Emp.Text = "";
                txtComments.Text = "";
                txtCC_Cargo.Text = "";
                txtPeriodDateOf.SelectedDate = null;
                txtPeriodDateTo.SelectedDate = null;
                txtAuthorizationAmount.Text = "";
                BonosCOM bonos = new BonosCOM();
                DataSet ds = LoadBondsRequestsData();//bonos.sp_GetRequests_Bonds_Automatics(null, null, id_bonds);
                DataSet dsConfigurations = bonos.sp_GetBondsTypesEnabledAndID(id_tipo_bono);
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow bono = ds.Tables[0].Rows[0];
                        txtRequisitionNumber.Text = id_bonds.ToString();
                        txtRequisitionDate.Text = Convert.ToDateTime(bono["created"]).ToString("dd MMMM, yyyy", System.Globalization.CultureInfo.CreateSpecificCulture("es-MX"));
                        txtBondType.Text = bono["bond_name"].ToString();
                        txtEmployeeName.Text = bono["full_name"].ToString();
                        hdnEmployeeNumber.Value = bono["employee_number"].ToString();
                        txtCC_Cargo.Text = bono["CC_Cargo"].ToString();
                        txtCC_Emp.Text = bono["CC_Empleado"].ToString();
                        txtComments.Text = bono["requisition_comments"].ToString();
                        txtPeriodDateOf.SelectedDate = Convert.ToDateTime(bono["period_date_of"]);
                        txtPeriodDateTo.SelectedDate = Convert.ToDateTime(bono["period_date_to"]);
                        txtAuthorizationAmount.Text = Convert.ToDecimal(bono["authorization_amount"]).ToString("C2");
                        ModalShow("#modal_bonos");
                    }
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar información del bono: " + ex.Message, this);
            }
            finally
            {
                UnBlockUI();
                //InitTables();
            }
        }

        protected void lnkAutorizartodos_Click(object sender, EventArgs e)
        {
            txtCommentstodos.Text = "";
            ModalShow("#modal_bonos_todos");
        }

        protected void lnkautorizar_Click(object sender, EventArgs e)
        {
            try
            {
                txtAuthorizationAmount.BorderColor = System.Drawing.Color.Green;

                amount_correct.Visible = true;
                amount_error.Visible = false;
                int id_bonds = Convert.ToInt32(hdnIdRequestBond.Value == "" ? "0" : hdnIdRequestBond.Value);
                decimal amount = Convert.ToDecimal(txtAuthorizationAmount.Text == "" ? "0" : txtAuthorizationAmount.Text.Replace("$", "").Replace(",", ""));
                if (id_bonds == 0)
                {
                    Toast.Error("Error al procesar solicitud", this);
                }
                else if (tdAuthorizationComments.Text == "")
                {
                    Toast.Error("Error al procesar solicitud: Ingrese comentarios de autorización", this);
                }
                else if (amount == 0)
                {
                    txtAuthorizationAmount.BorderStyle = BorderStyle.Solid;
                    txtAuthorizationAmount.BorderColor = System.Drawing.Color.Red;
                    amount_correct.Visible = false;
                    amount_error.Visible = true;
                    Toast.Error("Error al procesar solicitud: Ingrese un monto valido.", this);
                }
                else
                {
                    requests_bonds bono = new requests_bonds();
                    bono.id_request_bond = id_bonds;
                    bono.authorization_amount = amount;
                    bono.modified_by = Session["usuario"] as string;
                    bono.authorization_comments = tdAuthorizationComments.Text;
                    bono.id_request_status = (int)enumerators.bonds_status.authorization;
                    ProcessBond(bono);
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al procesar solicitud: " + ex.Message, this);
            }
            finally
            {
                InitTables();
                load_modal.Style["display"] = "none";
            }
        }

        protected void lnkrechazarsolicitud_Click(object sender, EventArgs e)
        {
            try
            {
                txtAuthorizationAmount.BorderColor = System.Drawing.Color.Green;

                amount_correct.Visible = true;
                amount_error.Visible = false;
                int id_bonds = Convert.ToInt32(hdnIdRequestBond.Value == "" ? "0" : hdnIdRequestBond.Value);
                decimal amount = Convert.ToDecimal(txtAuthorizationAmount.Text == "" ? "0" : txtAuthorizationAmount.Text.Replace("$", "").Replace(",", ""));
                if (id_bonds == 0)
                {
                    Toast.Error("Error al procesar solicitud", this);
                }
                else if (tdAuthorizationComments.Text == "")
                {
                    Toast.Error("Error al procesar solicitud: Ingrese comentarios de autorización", this);
                }
                else if (amount == 0)
                {
                    txtAuthorizationAmount.BorderStyle = BorderStyle.Solid;
                    txtAuthorizationAmount.BorderColor = System.Drawing.Color.Red;
                    amount_correct.Visible = false;
                    amount_error.Visible = true;
                    Toast.Error("Error al procesar solicitud: Ingrese un monto valido.", this);
                }
                else
                {
                    requests_bonds bono = new requests_bonds();
                    bono.id_request_bond = id_bonds;
                    bono.authorization_amount = amount;
                    bono.modified_by = Session["usuario"] as string;
                    bono.authorization_comments = tdAuthorizationComments.Text;
                    bono.id_request_status = (int)enumerators.bonds_status.not_approved;
                    ProcessBond(bono);
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al procesar solicitud: " + ex.Message, this);
            }
            finally
            {
                InitTables();
                load_modal.Style["display"] = "none";
            }
        }

        protected void lnkrechazartodos_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState[hdfguid.Value + "-dt_reporte"] != null)
                {
                    DataTable dt = new DataTable();
                    dt = ViewState[hdfguid.Value + "-dt_reporte"] as DataTable;

                    foreach (DataRow row in dt.Rows)
                    {
                        string id_bonds = row["id"].ToString();
                        string amount = row["amount"].ToString();

                        requests_bonds bono = new requests_bonds();
                        bono.id_request_bond = Convert.ToInt32(id_bonds);
                        bono.authorization_amount = Convert.ToDecimal(amount);
                        bono.modified_by = Session["usuario"] as string;
                        bono.authorization_comments = tdAuthorizationComments.Text;
                        bono.id_request_status = (int)enumerators.bonds_status.not_approved;
                        ProcessBond(bono);
                    }
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al procesar solicitud: " + ex.Message, this);
            }
            finally
            {
                InitTables();
                load_modal.Style["display"] = "none";
            }
        }

        protected void lnkautorizartodoss_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState[hdfguid.Value + "-dt_reporte"] != null)
                {
                    DataTable dt = new DataTable();
                    dt = ViewState[hdfguid.Value + "-dt_reporte"] as DataTable;

                    foreach (DataRow row in dt.Rows)
                    {
                        string id_bonds = row["id"].ToString();
                        string amount = row["amount"].ToString();

                        requests_bonds bono = new requests_bonds();
                        bono.id_request_bond = Convert.ToInt32(id_bonds);
                        bono.authorization_amount = Convert.ToDecimal(amount);
                        bono.modified_by = Session["usuario"] as string;
                        bono.authorization_comments = tdAuthorizationComments.Text;
                        bono.id_request_status = (int)enumerators.bonds_status.authorization;
                        ProcessBond(bono);
                    }
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al procesar solicitud: " + ex.Message, this);
            }
            finally
            {
                InitTables();
                load_modal.Style["display"] = "none";
            }
        }
    }
}