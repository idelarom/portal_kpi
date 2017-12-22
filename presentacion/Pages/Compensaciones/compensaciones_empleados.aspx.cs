using datos;
using datos.Model;
using negocio.Componentes.Compensaciones;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion.Pages.Compensaciones
{
    public partial class compensaciones_empleados : System.Web.UI.Page
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

        protected void Page_Init(object sender, EventArgs e)
        {

            ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), " BlockUI();", true);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string usuario = Session["usuario"] as string;
                hdfguid.Value = Guid.NewGuid().ToString();
                BonosCOM bonos = new BonosCOM();
                DataTable dtValidateUser =  bonos.sp_Validate_User(usuario).Tables[0];
                if (dtValidateUser.Rows.Count > 0)
                {
                    Session["id_profile"] = Convert.ToInt32(dtValidateUser.Rows[0]["id_profile"]);
                    Session["employee_number"] = Convert.ToInt32(dtValidateUser.Rows[0]["employee_number"]);
                }
                else
                {
                    Session["id_profile"] = 0;
                    Session["employee_number"] = Convert.ToInt32(dtValidateUser.Rows[0]["employee_number"]);
                }
                Cargar_empleados();
            }
        }

        /// <summary>
        /// Desbloquea el hilo actual
        /// </summary>
        private void UnBlockUI()
        {
            load.Style["display"] = "none";
            load2.Style["display"] = "none";
            load3.Style["display"] = "none";
            ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "UnBlockUI();", true);
        }

        private void InitTables()
        {
            ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "Init('#table_empleadosCompensaciones');", true);
            ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "Init('#table_empleados');", true);
            ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "Init('#table_cc');", true);
        }

        private DataTable Cargar_Empleados_compensaciones(int? NumEmpleado, string NomEmpleado, int TipoConsulta)
        {

            CompensacionesEmpleadosCOM Empleados = new CompensacionesEmpleadosCOM();
            DataSet ds = Empleados.Sps_ConsultaEmpleadosNAVISION(NumEmpleado, NomEmpleado, TipoConsulta);
            DataTable dt = ds.Tables[0];
            //repeat_employees_compensations.DataSource = ds.Tables[0];
            //repeat_employees_compensations.DataBind();

            return dt;
        }

        private void Cargar_empleados()
        {
            DataTable dt = Cargar_Empleados_compensaciones(null, null, 1);
            repeat_employees_compensations.DataSource = dt;
            repeat_employees_compensations.DataBind();

            ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(), " BlockUI();", true);
            InitTables();
        }

        private void LoadBondsTypes()
        {
            CompensacionesEmpleadosCOM Bonos = new CompensacionesEmpleadosCOM();
            DataSet ds_tipo_bonos = Bonos.sp_GetBondsTypesEnabled();
            DataTable dt_tipo_bonos = new DataTable();
            dt_tipo_bonos = ds_tipo_bonos.Tables[0];
            repeater_Bonds_Definition.DataSource = dt_tipo_bonos;
            repeater_Bonds_Definition.DataBind();
        }

        private void deletetotable(string tipo)
        {
            try
            { 
                if ((ViewState["checked_auto"] != null))
                {
                    DataTable dtd = new DataTable();
                    dtd = ViewState["checked_auto"] as DataTable;
                    foreach (DataRow row in dtd.Rows)
                    {
                        if (row["tipo"].ToString().Trim() == tipo.Trim())
                        {
                            row.Delete();
                            break; // TODO: might not be correct. Was : Exit For
                        }
                    }
                    ViewState["checked_auto"] = dtd;
                }


            }
            catch (Exception ex)
            {
            }
        }

        private void addtotable(string tipo)
        {
            try
            {
                if (ViewState["checked_auto"] == null)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("tipo");
                    ViewState["checked_auto"] = dt;
                }
                DataTable dtd = new DataTable();
                dtd =  ViewState["checked_auto"] as DataTable;
                DataRow row = dtd.NewRow();
                row["tipo"] = tipo;
                dtd.Rows.Add(row);
                ViewState["checked_auto"] = dtd;

            }
            catch (Exception ex)
            {
            }
        }

        private Boolean ExistInTable(string tipo)
        {
            try
            {
                DataTable dtd = new DataTable();
                dtd = ViewState["checked_auto"] as DataTable;
                DataView dv = new DataView();
                dv = dtd.DefaultView;
                dv.RowFilter = "tipo = '" + tipo.Trim() + "'";
                return (dv.ToTable().Rows.Count > 0 ? true : false);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private void LimpiarControlesSolicitudBonos()
        {
            txtfilterempleado.Text = string.Empty;
            txtNumEmpleado.Text = string.Empty;
            txtJefe.Text = string.Empty;
            txtCC.Text = string.Empty;
            txtUser.Text = string.Empty;
            txtEmail.Text = string.Empty;
        }

        private void LoadBondsAutomaticTypes()
        {
            CompensacionesEmpleadosCOM Bonos = new CompensacionesEmpleadosCOM();
            DataTable dt_tipo_bonos = Bonos.sp_GetAllBondsAutomaticType().Tables[0];
            ddlBondsType.DataSource = dt_tipo_bonos;
            ddlBondsType.DataValueField = "IdBonds";
            ddlBondsType.DataTextField = "NameBonds";
            ddlBondsType.DataBind();
            ddlBondsType.Items.Insert(0, "Seleccione un bono");
            ddlBondsType.Visible = true;
        }

        private void LoadPeriodicityTypes()
        {
            CompensacionesEmpleadosCOM Periodicidad = new CompensacionesEmpleadosCOM();
            DataTable dt_Periodicidad = Periodicidad.sp_GetAllPeriodicityEnabled().Tables[0];
            ddlPeriodicidad.DataSource = dt_Periodicidad;
            ddlPeriodicidad.DataValueField = "id_periodicity";
            ddlPeriodicidad.DataTextField = "name";
            ddlPeriodicidad.DataBind();
            ddlPeriodicidad.Items.Insert(0, "Seleccione un periodo");
            ddlPeriodicidad.Visible = true;
            ddlPeriodicidad.SelectedIndex = 1;
        }
        private void LoadMesAno()
        {

            DataTable dsMeses = new DataTable("meses");
            //Añadimos las columnas codigo y descripcion
            dsMeses.Columns.Add("codigo", typeof(int));
            dsMeses.Columns.Add("descripcion", typeof(string));

            //Añadimos los meses al dataset
            dsMeses.Rows.Add(new object[] { 1, "Enero" });
            dsMeses.Rows.Add(new object[] { 2, "Febrero" });
            dsMeses.Rows.Add(new object[] { 3, "Marzo" });
            dsMeses.Rows.Add(new object[] { 4, "Abril" });
            dsMeses.Rows.Add(new object[] { 5, "Mayo" });
            dsMeses.Rows.Add(new object[] { 6, "Junio" });
            dsMeses.Rows.Add(new object[] { 7, "Julio" });
            dsMeses.Rows.Add(new object[] { 8, "Agosto" });
            dsMeses.Rows.Add(new object[] { 9, "Septiembre" });
            dsMeses.Rows.Add(new object[] { 10, "Octubre" });
            dsMeses.Rows.Add(new object[] { 11, "Noviembre" });
            dsMeses.Rows.Add(new object[] { 12, "Diciembre" });
            dsMeses.AcceptChanges();

            //Establecemos la datatable como la fuente de datos de nuestro combo.
            ddlMesInicial.DataSource = dsMeses;
            ddlMesInicial.DataValueField = "codigo";
            ddlMesInicial.DataTextField = "descripcion";
            ddlMesInicial.DataBind();
            ddlMesInicial.Items.Insert(0, "Seleccione un mes");
            ddlMesInicial.Visible = true;

            ddlMesFinal.DataSource = dsMeses;
            ddlMesFinal.DataValueField = "codigo";
            ddlMesFinal.DataTextField = "descripcion";
            ddlMesFinal.DataBind();
            ddlMesFinal.Items.Insert(0, "Seleccione un mes");
            ddlMesFinal.Visible = true;

            int i;
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("AÑO");
            for (i = DateTime.Now.Year; i <= DateTime.Now.Year + 10; i++)
            {
                DataRow dr = dt.NewRow();
                dr["ID"] = i.ToString();
                dr["AÑO"] = i.ToString();
                dt.Rows.Add(dr);
            }

            ddlAnoInicial.DataSource = dt;
            ddlAnoInicial.DataValueField = "ID";
            ddlAnoInicial.DataTextField = "AÑO";
            ddlAnoInicial.DataBind();
            ddlAnoInicial.Items.Insert(0, "Seleccione un año");
            ddlAnoInicial.Visible = true;

            ddlAnoFinal.DataSource = dt;
            ddlAnoFinal.DataValueField = "ID";
            ddlAnoFinal.DataTextField = "AÑO";
            ddlAnoFinal.DataBind();
            ddlAnoFinal.Items.Insert(0, "Seleccione un año");
            ddlAnoFinal.Visible = true;

            int DiaActual = DateTime.Now.Day;
            int MesActual = DateTime.Now.Month;
            int AñoActual = DateTime.Now.Year;

            if (DiaActual <= 6)
            {
                ddlMesInicial.SelectedValue = MesActual.ToString();
                ddlAnoInicial.SelectedValue = AñoActual.ToString();
                ddlMesFinal.SelectedValue = MesActual.ToString();
                ddlAnoFinal.SelectedValue = AñoActual.ToString();
            }
            else
            {
                if (MesActual == 12)
                {
                    ddlMesInicial.SelectedValue = "1";// (MesActual + (MesActual == 12 ? 1 : 0)).ToString();
                    ddlAnoInicial.SelectedValue = (AñoActual + 1).ToString();
                    ddlMesFinal.SelectedValue = "1";// (MesActual + (MesActual == 12 ? 1 : 0)).ToString();
                    ddlAnoFinal.SelectedValue = (AñoActual + 1).ToString();
                }
                else
                {
                    ddlMesInicial.SelectedValue = (MesActual + (MesActual < 12 ? 1 : 0)).ToString();
                    ddlAnoInicial.SelectedValue = AñoActual.ToString();
                    ddlMesFinal.SelectedValue = (MesActual + (MesActual < 12 ? 1 : 0)).ToString();
                    ddlAnoFinal.SelectedValue = AñoActual.ToString();
                }
            }

        }
        private void LoadOcurrencias()
        {
            int i;
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Ocurrencia");
            for (i = 1; i <= 12; i++)
            {
                DataRow dr = dt.NewRow();
                dr["ID"] = i.ToString();
                dr["Ocurrencia"] = i.ToString();
                dt.Rows.Add(dr);
            }
            ddlOcurrencias.DataSource = dt;
            ddlOcurrencias.DataValueField = "ID";
            ddlOcurrencias.DataTextField = "Ocurrencia";
            ddlOcurrencias.DataBind();
            ddlOcurrencias.Items.Insert(0, "Seleccione una ocurrencia");
            ddlOcurrencias.Visible = true;
        }

        private void loadCostCenter()
        {
            CompensacionesEmpleadosCOM CC = new CompensacionesEmpleadosCOM();
            DataSet ds_CC = CC.sp_getAll_cost_center();
            DataTable dt_CC = new DataTable();
            dt_CC = ds_CC.Tables[0];
            repeater_cc.DataSource = dt_CC;
            repeater_cc.DataBind();
        }
        
        private void LimpiarControlesSolicitudBonosAutomaticos()
        {
            LoadBondsAutomaticTypes();
            LoadPeriodicityTypes();
            LoadOcurrencias();
            LoadMesAno();
            txtNumEmpleadoAuto.Text = "";
            txtNombEmpleadoAuto.Text = "";
            txtMonto.Text = "";
            txtCCAuto.Text = "";
            txtCCCAuto.Text = "";
            txtComtarios.Text = "";
            txtcomentarioscancela.Text = "";
            hdfComandAutomaticos.Value = "";
            div_comentscancel.Visible = false;
            lnkcancelarSol.Visible = false;


        }

        private void InsertAndUpdate_Compensations(bool isNew)
        {
            try
            {
                bool isOcurredError = false;
                int index;
                DataSet dsCompensations = new DataSet();
                CompensacionesEmpleadosCOM Empleados = new CompensacionesEmpleadosCOM();
                dsCompensations = Empleados.sp_Delete_Employee_Compensations(Convert.ToInt32(this.txtNumEmpleado.Text.Trim()));

                if (dsCompensations.Tables.Count == 0)
                {
                    //for (index = 0; index < this.gridBondsDefinition.RowsInViewState.Count; index++)
                    //{
                    foreach (RepeaterItem item in repeater_Bonds_Definition.Items)
                    {
                        //WebControls.CheckBox chkSelected;
                        //chkSelected = ((WebControls.CheckBox)this.gridBondsDefinition.RowsInViewState(index).Cells(0).FindControl("chkSelected"));
                        CheckBox chkSelected = (CheckBox)item.FindControl("chkSelected");
                        Label lblamount_required = (Label)item.FindControl("lblamount_required");
                        Label lblperiodicity_required = (Label)item.FindControl("lblperiodicity_required");
                        Label lblid_bond_type = (Label)item.FindControl("lblid_bond_type");

                        employees_compensations employee_compensations = new employees_compensations();
                        employee_compensations.employee_number = Convert.ToInt32(this.txtNumEmpleado.Text);
                        employee_compensations.id_bond_type = Convert.ToInt32(lblid_bond_type.Text); //this.gridBondsDefinition.Rows[index].Cells(1).Text;

                        if (Convert.ToBoolean(lblamount_required.Text) == true)
                        { //Si el monto es requerido
                          // Obout.Interface.OboutTextBox txtAmount;                            
                          //txtAmount = ((Obout.Interface.OboutTextBox)this.gridBondsDefinition.RowsInViewState(index).Cells(3).FindControl("txtAmount"));
                            TextBox txtAmount = (TextBox)item.FindControl("txtAmount");
                            employee_compensations.amount = Convert.ToDecimal(txtAmount.Text == "" ? "0" : txtAmount.Text);

                            //Obout.Interface.OboutTextBox txtporcentaje;
                            //txtporcentaje = ((Obout.Interface.OboutTextBox)this.gridBondsDefinition.RowsInViewState(index).Cells(4).FindControl("txtporcentaje"));
                            TextBox txtporcentaje = (TextBox)item.FindControl("txtporcentaje");
                            employee_compensations.percentage_extra = Convert.ToDecimal(txtporcentaje.Text);
                        }
                        else
                        {
                            employee_compensations.amount = 0;
                            employee_compensations.percentage_extra = null;
                        }

                        if (Convert.ToBoolean(lblperiodicity_required.Text) == true)
                        { //Si la periodicidad es requerida
                            //Obout.Interface.OboutDropDownList ddlPeriodicity;
                            //ddlPeriodicity = ((Obout.Interface.OboutDropDownList)this.gridBondsDefinition.RowsInViewState(index).Cells(6).FindControl("ddlPeriodicity"));
                            DropDownList ddlPeriodicity = (DropDownList)item.FindControl("ddlPeriodicity");
                            employee_compensations.id_periodicity = Convert.ToInt32(ddlPeriodicity.SelectedValue);
                        }
                        else
                        {
                            employee_compensations.id_periodicity = null;
                        }
                        employee_compensations.created_by = Session["usuario"] as string;
                        employee_compensations.modified_by = Session["usuario"] as string;

                        if (chkSelected.Checked == true & Convert.ToInt32(lblid_bond_type.Text) != 5)
                        {
                            dsCompensations = Empleados.sp_Insert_Employee_Compensations(employee_compensations);
                            if (dsCompensations.Tables.Count != 0)
                            {                                
                                Toast.Error("Ocurrio un error al intentar guardar las compensaciones del empleado.", this);
                                isOcurredError = true;
                                break;
                            }
                        }
                        else if (chkSelected.Checked == false & Convert.ToInt32(lblid_bond_type.Text) != 5)
                        {
                            Empleados.sp_actualizar_log_inactivo(employee_compensations);
                        }

                    }
                }
                else
                {
                    Toast.Error("Ocurrio un error al intentar guardar las compensaciones del empleado.", this);
                    isOcurredError = true;
                }

                if (isOcurredError == false)
                {
                     Toast.Success("La información del empleado ha sido guardado exitosamente.", "Mensaje del sistema", this);
                    LimpiarControlesSolicitudBonos();
                    Cargar_empleados();
                    this.tblemployees.Visible = true;
                    this.tblInformationEmployeeBonds.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar los tipos de bonos: " + ex.ToString(), this);
            }
        }

        private string ConvertToHtmlFile(DataTable targetTable)
        {
            string myHtmlFile = "";

            if (targetTable == null)
            {
                throw new System.ArgumentNullException("targetTable");
                //Continue.
            }
            else
            {
            }

            //Get a worker object.
           StringBuilder myBuilder = new StringBuilder();
            //Open tags and write the top portion.
            myBuilder.Append("<html>");
            myBuilder.Append("<head>");
            //myBuilder.Append("<title>")
            //myBuilder.Append("Page-")
            //myBuilder.Append(Guid.NewGuid().ToString())
            //myBuilder.Append("</title>")
            myBuilder.Append("</head>");
            myBuilder.Append("<body style='font-size: 10pt;  font-family: Arial, Helvetica, sans-serif;'>");
            //myBuilder.Append("<table border='2px' cellpadding='4' cellspacing='0' bordercolor='#333333' ")
            myBuilder.Append("<table cellspacing='0' cellpadding='4' style='font-size: 9pt;  font-family: Arial, Helvetica, sans-serif;' rules='all' bordercolor='#333333' border='1'>");
            //myBuilder.Append("style='border: solid 2px Black; font-size: x-small;'>")

            //Add the headings row.

            myBuilder.Append("<tr align='left' valign='top' style='font-size: 10pt;  font-family: Arial, Helvetica, sans-serif;color:#FFF' bgcolor='#2E2EFE'>");

            foreach (DataColumn myColumn in targetTable.Columns)
            {
                myBuilder.Append("<td align='left' valign='top'><b>");
                myBuilder.Append(myColumn.ColumnName);
                myBuilder.Append("</b></td>");
            }

            myBuilder.Append("</tr>");

            //Add the data rows.
            foreach (DataRow myRow in targetTable.Rows)
            {
                myBuilder.Append("<tr align='left' valign='top'>");

                foreach (DataColumn myColumn in targetTable.Columns)
                {
                    myBuilder.Append("<td align='left' valign='top'>");
                    myBuilder.Append(myRow[myColumn.ColumnName].ToString());
                    myBuilder.Append("</td>");
                }
                myBuilder.Append("</tr>");
            }

            //Close tags.
            myBuilder.Append("</table>");
            myBuilder.Append("</body>");
            myBuilder.Append("</html>");

            //Get the string for return.
            myHtmlFile = myBuilder.ToString();

            return myHtmlFile;
        }

        protected void lnksearch_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = Cargar_Empleados_compensaciones(null, "", 3);
                repeater_empleados.DataSource = dt;
                repeater_empleados.DataBind();
                ModalShow("#modal_empleados");
            }
            catch (Exception ex)
            {
                Toast.Error("Error al filtrar empleados: " + ex.Message, this);
            }
            finally
            {
                InitTables();
                UnBlockUI();
            }
        }

        protected void btnConfiguracion_Click(object sender, EventArgs e)
        {
            CompensacionesEmpleadosCOM Empleados = new CompensacionesEmpleadosCOM();
            DataSet ds = Empleados.Sps_ConsultaEmpleadosNAVISION(Convert.ToInt32(hdfnumEmpleado.Value), null, 3);
            DataTable dt = ds.Tables[0];
            txtfilterempleado.Text  = dt.Rows[0]["NomFull"].ToString();
            txtNumEmpleado.Text = dt.Rows[0]["NumEmpleado"].ToString();
            txtJefe.Text = dt.Rows[0]["JefeInmediato"].ToString();
            txtCC.Text = dt.Rows[0]["CC"].ToString() + "-" + dt.Rows[0]["CCNom"].ToString();
            txtUser.Text = dt.Rows[0]["Login"].ToString();
            txtEmail.Text = dt.Rows[0]["Email"].ToString();
            Boolean status = Convert.ToBoolean(dt.Rows[0]["Enabled"]);
            if (status == true)
            {
                chkActivo.Text = "Activo";
                chkActivo.Checked = true;
            }
            else
            {
                chkActivo.Text = "Inactivo";
                chkActivo.Checked = false;
            }
            LoadBondsTypes();
            this.tblemployees.Visible = false;
            this.tblInformationEmployeeBonds.Visible = true;
            UnBlockUI();
        }

        protected void repeater_Bonds_Definition_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            int num = 1;
            DataRowView dbr = (DataRowView)e.Item.DataItem;

            string bond_type = DataBinder.Eval(dbr, "name").ToString();

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                System.Web.UI.WebControls.DropDownList ddlPeriodicity = new System.Web.UI.WebControls.DropDownList();
                System.Web.UI.WebControls.TextBox txtAmount = new System.Web.UI.WebControls.TextBox();
                System.Web.UI.WebControls.CheckBox chkSelected = new System.Web.UI.WebControls.CheckBox();
                System.Web.UI.WebControls.TextBox txtPorcentaje = new System.Web.UI.WebControls.TextBox();
                System.Web.UI.WebControls.TextBox txtAmountMax = new System.Web.UI.WebControls.TextBox();
                Literal lbtnAuto = new Literal();

                System.Web.UI.WebControls.Label lblid_bond_type = new System.Web.UI.WebControls.Label();
                System.Web.UI.WebControls.Label lblamount_required = new System.Web.UI.WebControls.Label();
                System.Web.UI.WebControls.Label lblperiodicity_required = new System.Web.UI.WebControls.Label();

                chkSelected = (CheckBox)e.Item.FindControl("chkSelected");//e.Row.Cells(0).FindControl("chkSelected");
                ddlPeriodicity = (DropDownList)e.Item.FindControl("ddlPeriodicity");// e.Row.Cells(6).FindControl("ddlPeriodicity");
                txtAmount = (TextBox)e.Item.FindControl("txtAmount");// e.Row.Cells(3).FindControl("txtAmount");
                txtPorcentaje = (TextBox)e.Item.FindControl("txtPorcentaje");// e.Row.Cells(4).FindControl("txtPorcentaje");
                txtAmountMax = (TextBox)e.Item.FindControl("txtAmountMax");// e.Row.Cells(5).FindControl("txtAmountMax");
                lbtnAuto = (Literal)e.Item.FindControl("lbtnAuto");// e.Row.Cells(2).FindControl("lbtnAuto");
                //txtPorcentaje.Text = "100";

                lblid_bond_type = (Label)e.Item.FindControl("lblid_bond_type");
                lblamount_required = (Label)e.Item.FindControl("lblamount_required");
                lblperiodicity_required = (Label)e.Item.FindControl("lblperiodicity_required");

                if (!object.ReferenceEquals(txtNumEmpleado.Text, ""))
                {
                    // Data.DataTable dtEmployeeCompensation = default(Data.DataTable);
                    DataTable dtEmployeeCompensation = new DataTable();
                    CompensacionesEmpleadosCOM Empleados = new CompensacionesEmpleadosCOM();
                    dtEmployeeCompensation = Empleados.sp_GetEmployees_CompensationsForEmployeeNumber(Convert.ToInt32(txtNumEmpleado.Text)).Tables[0];
                    //'AQUI ES DONDE SE QUEDO

                    foreach (DataRow row in dtEmployeeCompensation.Rows)
                    {
                        int id_bond_type = Convert.ToInt32(DataBinder.Eval(dbr, "id_bond_type"));
                        lblid_bond_type.Text = id_bond_type.ToString();

                        if (row["id_bond_type"].ToString() == id_bond_type.ToString())// e.Row.Cells(1).Text)
                        {
                            chkSelected.Checked = true;
                            if (Convert.ToBoolean(row["amount_required"]) == true)
                            {
                                txtAmount.Text = row["amount"].ToString();
                                txtPorcentaje.Text = Convert.ToInt32(row["percentage_extra"]).ToString();
                                txtAmountMax.Text = Convert.ToDecimal((Convert.ToInt32(row["amount"]) * Convert.ToInt32(row["percentage_extra"]) / 100)).ToString();
                            }
                            if (Convert.ToBoolean(row["periodicity_required"]) == true)
                            {
                                ddlPeriodicity.SelectedValue = Convert.ToInt32(row["id_periodicity"]).ToString();
                            }
                        }
                    }
                }
                if (ViewState["checked_auto"] != null && lblid_bond_type.Text == "5")
                {
                    chkSelected.Checked = ExistInTable(bond_type);
                }

                if (bond_type == "Automático")
                {

                    lbtnAuto.Text = "Automático";
                    if (!string.IsNullOrEmpty(txtNumEmpleado.Text))
                    {
                        CompensacionesEmpleadosCOM Empleados = new CompensacionesEmpleadosCOM();
                        DataTable DT = Empleados.sp_GetAllBondsAtomaticForEmployee(Convert.ToInt32(txtNumEmpleado.Text)).Tables[0];
                        int CantBonds = 0;
                        foreach (DataRow row in DT.Rows)
                        {
                            CantBonds = Convert.ToInt32(row["NumberOfAutomaticBonds"]);
                        }
                        if (CantBonds > 0)
                        {
                            lbtnAuto.Text = "<a onclick='OpenBondsRequisitionsAutomatic();'>Automático</a>";
                            chkSelected.Checked = true;
                            hdfCantBondsAuto.Value = CantBonds.ToString();
                        }
                        else
                        {
                            lbtnAuto.Text = "Automático";
                            hdfCantBondsAuto.Value = "0";
                        }
                    }
                }
                else
                {
                    lbtnAuto.Text = bond_type;
                }
                Boolean amount_required = Convert.ToBoolean(DataBinder.Eval(dbr, "amount_required"));
                Boolean periodicity_required = Convert.ToBoolean(DataBinder.Eval(dbr, "periodicity_required"));
                lblamount_required.Text = amount_required.ToString();
                lblperiodicity_required.Text = periodicity_required.ToString();

                if (amount_required == true)
                {
                    txtAmount.Visible = true;
                    txtPorcentaje.Visible = true;
                    txtAmountMax.Visible = true;
                    txtAmountMax.Enabled = false;                   

                }
                else
                {
                    txtAmount.Visible = false;
                    txtPorcentaje.Visible = false;
                    txtAmountMax.Visible = false;
                }
                if (periodicity_required == true)
                {
                    PeriodicidadCOM periodicity = new PeriodicidadCOM();
                    ddlPeriodicity.DataSource = periodicity.SelectAll();
                    ddlPeriodicity.DataValueField = "id_periodicity";
                    ddlPeriodicity.DataTextField = "name";
                    ddlPeriodicity.DataBind();
                    ddlPeriodicity.Visible = true;
                }
                else
                {
                    ddlPeriodicity.Visible = false;
                }

                num = num + 1;
            }
        }

        protected void txtPorcentaje_TextChanged(object sender, EventArgs e)
        {
            TextBox txtPorcentajeSender = sender as TextBox;

            decimal monto = 0;
            decimal percentage = 0;
            decimal percentageValidate = 0;
            decimal amountValidate = 0;

            foreach (RepeaterItem item in repeater_Bonds_Definition.Items)
            {
                TextBox txtAmount = (TextBox)item.FindControl("txtAmount");
                TextBox txtPorcentaje = (TextBox)item.FindControl("txtPorcentaje");
                TextBox txtAmountMax = (TextBox)item.FindControl("txtAmountMax");
                if (txtPorcentaje == txtPorcentajeSender)
                {
                    monto = Convert.ToDecimal(txtAmount.Text);
                    percentage = Convert.ToDecimal(txtPorcentaje.Text);
                    percentageValidate = (Convert.ToDecimal(txtPorcentaje.Text) / Convert.ToDecimal(100));
                    amountValidate = (monto * percentageValidate);
                    txtAmountMax.Text = amountValidate.ToString();
                    break;
                }
            }
            if (monto <= 0)
            {
                Toast.Info("El monto debe ser mayor a cero", "Aviso del sistema", this);
            }
            else if (percentage < 100)
            {
                Toast.Info("El porcentaje extra debe de ser mayor o igual al 100%", "Aviso del sistema", this);
            }

            UnBlockUI();
        }

        protected void btnValidachinchkSelected_Click(object sender, EventArgs e)
        {
            string EmployeeNumber = txtNumEmpleado.Text.Trim();
            foreach (RepeaterItem item in repeater_Bonds_Definition.Items)
            {
                CheckBox chkSelected = (CheckBox)item.FindControl("chkSelected");
                Literal lbtnAuto = (Literal)item.FindControl("lbtnAuto");
                string Bono = lbtnAuto.Text.Replace("<a onclick='OpenBondsRequisitionsAutomatic();'>", "").Replace("</a>", "");
                deletetotable(Bono);
                if (chkSelected.Checked)
                {
                    addtotable(Bono);
                }              
                if (Bono == "Automático")
                {
                    string usuario = Session["usuario"] as string;
                    //TipoBonosCOM Bonos = new TipoBonosCOM();
                    BonosCOM bonos = new BonosCOM();
                    DataTable dt = new DataTable();                   
                    dt = bonos.tipos_bonos(true, usuario).Tables[0]; //Bonos.SelectAll();
                    if (dt.Rows[4]["id_bond_type"].ToString().Contains("5"))
                    {
                        if (chkSelected.Checked == true & Convert.ToUInt32(hdfCantBondsAuto.Value) <= 0)
                        {
                            LoadBondsAutomaticTypes();
                            LoadPeriodicityTypes();
                            LoadOcurrencias();
                            LoadMesAno();
                            DataTable DT;
                            CompensacionesEmpleadosCOM Datos = new CompensacionesEmpleadosCOM();
                            DT = Datos.sp_OptenerDatostoRequestsBondsAutomatic(Convert.ToInt32(txtNumEmpleado.Text.Trim()), null).Tables[0];
                            foreach (DataRow row in DT.Rows)
                            {
                                if (Convert.ToString(row["Empleado"]) == "EL EMPLEADO NO EXISTE O ESTA DADO DE BAJA")
                                {
                                    Toast.Info("El empleado que acaba de seleccionar no existe o esta dado de baja, favor verifique su selección ", "Informacion del sistema", this);

                                }
                                else
                                {
                                    txtNombEmpleadoAuto.Text = Convert.ToString(row["Empleado"]);
                                    txtNumEmpleadoAuto.Text = Convert.ToString(row["No_"]);
                                    txtCCAuto.Text = Convert.ToString(row["Centro de Costos"]);
                                    txtCCCAuto.Text = Convert.ToString(row["Centro de Costos"]);
                                    txtRCCC.Text = Convert.ToString(row["ResponsableCC"]);
                                    repeater_solicitud_auto.DataSource = Datos.sp_GetRequests_Bonds_Automatic_Type(Convert.ToInt32(row["No_"].ToString().Trim())).Tables[0];
                                    repeater_solicitud_auto.DataBind();
                                    hdfComandAutomaticos.Value = "Solicitar";
                                    this.tblemployees.Visible = false;
                                    this.tblInformationEmployeeBonds.Visible = false;
                                    this.tblInformationEmployeeBondsAuto.Visible = true;
                                }
                            }
                            break;
                        }
                        else if (chkSelected.Checked == false & Convert.ToUInt32(hdfCantBondsAuto.Value) > 0)
                        {
                            LoadBondsTypes();
                            Toast.Info("El empleado ya tiene uno o varios Bonos automaticos dados de alta, por lo tanto la casilla no puede ser deseleccionada", "Aviso del sistema", this);
                            break;
                        }
                    }
                    else
                    {
                        Toast.Warning("No tienes permisos para solicitar el tipo de bono automatico, favor comunicate con el area de sistemas correspondiente", "Alerta del sistema", this);
                    }
                }
            }
            UnBlockUI();
        }

        protected void lnknuevoEmpleado_Click(object sender, EventArgs e)
        {
            LoadBondsTypes();
            this.tblemployees.Visible = false;
            this.tblInformationEmployeeBonds.Visible = true;
            UnBlockUI();
        }

        protected void lnkguardarconfigbond_Click(object sender, EventArgs e)
        {
            try
            { 
                bool verror = false;
                string vmensaje_error = "";
                foreach (RepeaterItem item in repeater_Bonds_Definition.Items)
                {
                    CheckBox chkSelected = (CheckBox)item.FindControl("chkSelected");
                    TextBox txtAmount = (TextBox)item.FindControl("txtAmount");
                    Label lblamount_required = (Label)item.FindControl("lblamount_required");
                    Label lblperiodicity_required = (Label)item.FindControl("lblperiodicity_required");
                    TextBox txtporcentaje = (TextBox)item.FindControl("txtporcentaje");
                    Label lblid_bond_type = (Label)item.FindControl("lblid_bond_type");

                    if (chkSelected.Checked == true & Convert.ToBoolean(lblamount_required.Text) == true)
                    {
                        if (txtAmount.Text == "")
                        {
                            verror = true;
                            vmensaje_error = "Por Favor Ingrese el Monto";
                            break;
                        }

                        if (Convert.ToDecimal(txtAmount.Text) < 0)
                        {
                            verror = true;
                            vmensaje_error = "El Monto debe ser mayor a 0";
                            break;
                        }
                        if (txtAmount.Text == "")
                        {
                            vmensaje_error = "Por Favor Ingrese el monto";
                        }

                        if (txtporcentaje.Text == "")
                        {
                            verror = true;
                            vmensaje_error = "Por Favor Ingrese el Porcentaje";
                            break;
                        }

                        if (Convert.ToDecimal(txtporcentaje.Text) < 100)
                        {
                            verror = true;
                            vmensaje_error = "El Porcentaje debe ser mayor a 100";
                            break;
                        }

                    }
                }

                if (verror)
                {
                    LoadBondsTypes();                 
                    Toast.Error(vmensaje_error, this);
                }
                else
                {
                    string EmployeeNumber = txtNumEmpleado.Text.Trim();
                    if (this.lnkguardarconfigbond.Text == "Guardar")
                    {
                        InsertAndUpdate_Compensations(true);
                    }
                    else
                    {
                        InsertAndUpdate_Compensations(false);
                    }
                }
        }
        catch(Exception ex){
                Toast.Error("Error al guardar solicitud: " + ex.Message, this);
            }
            UnBlockUI();
        }

        protected void lnkcancelarconfigbond_Click(object sender, EventArgs e)
        {
            Response.Redirect("compensaciones_empleados.aspx");
        }

        protected void lnkcancelar_Click(object sender, EventArgs e)
        {
            LimpiarControlesSolicitudBonosAutomaticos();
            this.tblemployees.Visible = false;
            this.tblInformationEmployeeBonds.Visible = true;
            this.tblInformationEmployeeBondsAuto.Visible = false;            
           UnBlockUI();
            btnConfiguracion_Click(null, null);
        }

        protected void btnConfiguracionAuto_Click(object sender, EventArgs e)
        {           
            LoadBondsAutomaticTypes();
            LoadPeriodicityTypes();
            LoadOcurrencias();
            LoadMesAno();
            DataTable DT;
            CompensacionesEmpleadosCOM Datos = new CompensacionesEmpleadosCOM();
            DT = Datos.sp_OptenerDatostoRequestsBondsAutomatic(Convert.ToInt32(txtNumEmpleado.Text.Trim()), null).Tables[0];
            foreach (DataRow row in DT.Rows)
            {
                if (Convert.ToString(row["Empleado"]) == "EL EMPLEADO NO EXISTE O ESTA DADO DE BAJA")
                {
                    Toast.Info("El empleado que acaba de seleccionar no existe o esta dado de baja, favor verifique su selección ", "Informacion del sistema", this);

                }
                else
                {
                    txtNombEmpleadoAuto.Text = Convert.ToString(row["Empleado"]);
                    txtNumEmpleadoAuto.Text = Convert.ToString(row["No_"]);
                    txtCCAuto.Text = Convert.ToString(row["Centro de Costos"]);
                    txtCCCAuto.Text = Convert.ToString(row["Centro de Costos"]);
                    txtRCCC.Text = Convert.ToString(row["ResponsableCC"]);
                    repeater_solicitud_auto.DataSource = Datos.sp_GetRequests_Bonds_Automatic_Type(Convert.ToInt32(row["No_"].ToString().Trim())).Tables[0];
                    repeater_solicitud_auto.DataBind();
                    this.tblemployees.Visible = false;
                    hdfComandAutomaticos.Value = "Solicitar";
                    this.tblInformationEmployeeBonds.Visible = false;
                    this.tblInformationEmployeeBondsAuto.Visible = true;
                }
            }
            UnBlockUI();
        }

        protected void lnksearchCCC_Click(object sender, EventArgs e)
        {
            try
            {
                loadCostCenter();
                ModalShow("#modal_CC");
            }
            catch (Exception ex)
            {
                Toast.Error("Error al filtrar centros de costos: " + ex.Message, this);
            }
            finally
            {
                InitTables();
                UnBlockUI();
            }
        }

        protected void btneventgrid_Click(object sender, EventArgs e)
        {
            string Id_request_bond = hdfId_request_bond_automatic.Value;
            string bond_name = hdfbond_name.Value;
            string IdBonds = hdfIdBonds.Value;
            string Monto = hdfMonto.Value;
            string Periodo = hdfPeriodo.Value;
            string Ocurrencias = hdfOcurrencias.Value;
            string OcurrenciasPend = hdfOcurrenciasPend.Value;
            string CC_Cargo = hdfCC_Cargo.Value;
            string Estatus = hdfEstatus.Value;
            string Comentarios = hdfComentarios.Value;
            string Fecha_Inicial = hdfFecha_Inicial.Value;
            string Fecha_Final = hdfFecha_Final.Value;

            if (Estatus!= "SOLICITADA")
            {
                Toast.Warning("No se puede edidar la solicitud " + Id_request_bond + " porque no tiene estatus de Solicitada ", "Mensaje del sistema", this);
            }
            else
            {
                ddlBondsType.SelectedValue = IdBonds;
                ddlPeriodicidad.SelectedItem.Text = Periodo;
                txtMonto.Text = Monto;
                ddlOcurrencias.SelectedValue = Ocurrencias;
                ddlPeriodicidad.SelectedItem.Text = Periodo;
                txtComtarios.Text = Comentarios;
                String[] PartFecha_Inicial = Fecha_Inicial.Split('-');
                ddlMesInicial.SelectedValue = Convert.ToInt32(PartFecha_Inicial[1]).ToString();
                ddlAnoInicial.SelectedValue = Convert.ToInt32(PartFecha_Inicial[0]).ToString();
                String[] PartFecha_Final = Fecha_Final.Split('-');
                ddlMesFinal.SelectedValue = Convert.ToInt32(PartFecha_Final[1]).ToString();
                ddlAnoFinal.SelectedValue = Convert.ToInt32(PartFecha_Final[0]).ToString();
                txtCCCAuto.Text = hdfCC_Cargo.Value;
                hdfFechaFinalAuto.Value = Fecha_Final;
                string[] part_CCC = hdfCC_Cargo.Value.Split('-');
                CompensacionesEmpleadosCOM CC = new CompensacionesEmpleadosCOM();
                DataSet ds_CC = CC.sp_getAll_cost_center();
                DataTable dt_CC = new DataTable();
                dt_CC = ds_CC.Tables[0];

                foreach (DataRow row in dt_CC.Rows)
                {
                    if (row["CC"].ToString()== part_CCC[0])
                    {
                        txtRCCC.Text = row["ResponsableCC"].ToString();
                    }
                }

                hdfComandAutomaticos.Value = "Actualizar";

            }

            UnBlockUI();
        }

        protected void btneliminar_Click(object sender, EventArgs e)
        {
            string Id_request_bond = hdfId_request_bond_automatic.Value;
            string bond_name = hdfbond_name.Value;
            string IdBonds = hdfIdBonds.Value;
            string Monto = hdfMonto.Value;
            string Periodo = hdfPeriodo.Value;
            string Ocurrencias = hdfOcurrencias.Value;
            string OcurrenciasPend = hdfOcurrenciasPend.Value;
            string CC_Cargo = hdfCC_Cargo.Value;
            string Estatus = hdfEstatus.Value;
            string Comentarios = hdfComentarios.Value;
            string Fecha_Inicial = hdfFecha_Inicial.Value;
            string Fecha_Final = hdfFecha_Final.Value;

            ddlBondsType.SelectedValue = IdBonds;
            ddlPeriodicidad.SelectedItem.Text = Periodo;
            txtMonto.Text = Monto;
            ddlOcurrencias.SelectedValue = Ocurrencias;
            ddlPeriodicidad.SelectedItem.Text = Periodo;
            txtComtarios.Text = Comentarios;
            String[] PartFecha_Inicial = Fecha_Inicial.Split('-');
            ddlMesInicial.SelectedValue = Convert.ToInt32(PartFecha_Inicial[1]).ToString();
            ddlAnoInicial.SelectedValue = Convert.ToInt32(PartFecha_Inicial[0]).ToString();
            String[] PartFecha_Final = Fecha_Final.Split('-');
            ddlMesFinal.SelectedValue = Convert.ToInt32(PartFecha_Final[1]).ToString();
            ddlAnoFinal.SelectedValue = Convert.ToInt32(PartFecha_Final[0]).ToString();
            txtCCCAuto.Text = hdfCC_Cargo.Value;
            hdfFechaFinalAuto.Value = Fecha_Final;
            string[] part_CCC = hdfCC_Cargo.Value.Split('-');
            CompensacionesEmpleadosCOM CC = new CompensacionesEmpleadosCOM();
            DataSet ds_CC = CC.sp_getAll_cost_center();
            DataTable dt_CC = new DataTable();
            dt_CC = ds_CC.Tables[0];

            foreach (DataRow row in dt_CC.Rows)
            {
                if (row["CC"].ToString() == part_CCC[0])
                {
                    txtRCCC.Text = row["ResponsableCC"].ToString();
                }
            }
            if (Estatus == "SOLICITADA" || Estatus == "AUTORIZADA")
            {
                if (div_comentscancel.Visible==false)
                {
                    lnkcancelarSol.Visible = true;
                    div_comentscancel.Visible = true;
                }               
            }
            else
            {
                Toast.Warning("No se puede mostrar el boton de cancelar por que la solicitud " + Id_request_bond + " no tiene alguno de los estatus requeridos para cancelar que son Solicitada y Autorizada", "", this);
            }
            UnBlockUI();
        }

        protected void ddlOcurrencias_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlMesInicial.SelectedIndex != 0 & ddlAnoInicial.SelectedIndex != 0 & ddlOcurrencias.SelectedIndex != 0)
            {
                int Mes_Inicial = Convert.ToInt32(ddlMesInicial.SelectedValue);

                string FechaInicial = "01/" + Mes_Inicial + "/" + ddlAnoInicial.SelectedValue;
                string FechaFinal = "";
                int Ocurencias = Convert.ToInt32(ddlOcurrencias.SelectedValue);

                var Fecha_calculada = new DateTime(Convert.ToInt32(ddlAnoInicial.SelectedValue), Mes_Inicial, 1);

                if (ddlPeriodicidad.SelectedItem.Text == "Mensual")
                {
                    Ocurencias = Ocurencias - 1;
                }
                else if (ddlPeriodicidad.SelectedItem.Text == "Trimestral")
                {
                    Ocurencias = (Ocurencias * 3) - 1;
                }
                else if (ddlPeriodicidad.SelectedItem.Text == "Semestral")
                {
                    Ocurencias = (Ocurencias * 6) - 1;
                }
                else if (ddlPeriodicidad.SelectedItem.Text == "Anual")
                {
                    Ocurencias = (Ocurencias * 12) - 1;
                }
                else if (ddlPeriodicidad.SelectedItem.Text == "Bimestral")
                {
                    Ocurencias = (Ocurencias * 2) - 1;
                }
                
                for (int ctr = 0; ctr <= Ocurencias; ctr++)
                {
                    if (ctr == Ocurencias)
                    {
                        FechaFinal = Fecha_calculada.AddMonths(ctr).ToString("dd/MM/yyyy");
                    }
                }
                String[] PartFecha = FechaFinal.Split('/');

                int diafinal = DateTime.DaysInMonth(Convert.ToInt32(PartFecha[2].ToString()), Convert.ToInt32(PartFecha[1].ToString()));
                FechaFinal = diafinal.ToString() + "/" + PartFecha[1].ToString() + "/" + PartFecha[2].ToString();

                ddlMesFinal.SelectedValue = Convert.ToInt32(PartFecha[1]).ToString();
                ddlAnoFinal.SelectedValue = Convert.ToInt32(PartFecha[2]).ToString();
                hdfFechaFinalAuto.Value = FechaFinal;
            }
            UnBlockUI();
        }

        protected void lnksolicitaryguardar_Click(object sender, EventArgs e)
        {
            if (ddlBondsType.SelectedValue == "Seleccione un bono")
            {
                Toast.Warning("Favor de seleccionar un tipo de bono", "Dato requerido", this);
            }
            else if (Convert.ToDecimal(txtMonto.Text.Replace("$", "").Replace(",", "") == "" ? "0" : txtMonto.Text.Replace("$", "").Replace(",", "")) <=0 )                
            {
                Toast.Warning("El monto deve ser mayor a cero", "Dato requerido", this);
            }
            else if (ddlOcurrencias.SelectedValue == "Seleccione una ocurrencia")
            {
                Toast.Warning("Favor de seleccionar un numero de ocurrencias", "Dato requerido", this);
            }
            else
            {
                string FechaInicial = "01/" + ddlMesInicial.SelectedValue + "/" + ddlAnoInicial.SelectedValue;
                string FechaFinal = hdfFechaFinalAuto.Value;

                CompensacionesEmpleadosCOM BondAutomatic = new CompensacionesEmpleadosCOM();
                requests_bonds_Automatic requests_bonds_Automatic = new requests_bonds_Automatic();
                requests_bonds_Automatic.Id_request_bond_automatic = Convert.ToInt32(hdfId_request_bond_automatic.Value);
                requests_bonds_Automatic.Employee_number = Convert.ToInt32(txtNumEmpleadoAuto.Text);
                requests_bonds_Automatic.Employee = txtNombEmpleadoAuto.Text;
                string[] Partcc = txtCCAuto.Text.Split('-');
                requests_bonds_Automatic.CC_Empleado = Convert.ToInt32(Partcc[0]);
                requests_bonds_Automatic.IdBonds = Convert.ToInt32(ddlBondsType.SelectedValue);
                requests_bonds_Automatic.NameBonds = ddlBondsType.SelectedItem.Text;
                requests_bonds_Automatic.Id_Periodicity = Convert.ToInt32(ddlPeriodicidad.SelectedValue);
                requests_bonds_Automatic.Occurrences = Convert.ToInt32(ddlOcurrencias.SelectedValue);
                string[] Partccc = txtCCCAuto.Text.Split('-');
                requests_bonds_Automatic.CC_Cargo = Convert.ToInt32(Partccc[0]);
                requests_bonds_Automatic.InitialDate = Convert.ToDateTime(FechaInicial);
                requests_bonds_Automatic.FinalDate = Convert.ToDateTime(FechaFinal);
                requests_bonds_Automatic.observations = txtComtarios.Text;
                requests_bonds_Automatic.PendingOccurrences = Convert.ToInt32(ddlOcurrencias.SelectedValue);
                requests_bonds_Automatic.status = 1;
                requests_bonds_Automatic.Amount = Convert.ToDecimal(txtMonto.Text.Replace("$", "").Replace(",", ""));
                requests_bonds_Automatic.Created_by = Session["usuario"] as string;
                requests_bonds_Automatic.Update_by = Session["usuario"] as string;
                CompensacionesEmpleadosCOM Bonos_automaticos = new CompensacionesEmpleadosCOM();
                if (hdfComandAutomaticos.Value == "Solicitar")
                {
                    try
                    {
                        DataSet ds = new DataSet();
                        ds = Bonos_automaticos.sp_InsertResquestBondsAutomatic(requests_bonds_Automatic);

                        DataTable dt = new DataTable();
                        dt = ds.Tables[0];
                        string Body = "";
                        string vmensaje_error = dt.Rows[0]["mensaje"].ToString();
                        if (vmensaje_error == "")
                        {
                            DataTable DTNotification = new DataTable();
                            DTNotification.Columns.Add("Dato");
                            DTNotification.Columns.Add("Descripcion");

                            DataRow rowNumE = DTNotification.NewRow();
                            rowNumE["Dato"] = "Numero de empleado:";
                            rowNumE["Descripcion"] = txtNumEmpleadoAuto.Text;
                            DTNotification.Rows.Add(rowNumE);

                            DataRow rowEmp = DTNotification.NewRow();
                            rowEmp["Dato"] = "Empleado:";
                            rowEmp["Descripcion"] = txtNombEmpleadoAuto.Text;
                            DTNotification.Rows.Add(rowEmp);

                            DataRow rowBono = DTNotification.NewRow();
                            rowBono["Dato"] = "Bono Automatico:";
                            rowBono["Descripcion"] = ddlBondsType.SelectedItem.Text;
                            DTNotification.Rows.Add(rowBono);

                            DataRow rowMon = DTNotification.NewRow();
                            rowMon["Dato"] = "Monto:";
                            rowMon["Descripcion"] = "$" + txtMonto.Text;
                            DTNotification.Rows.Add(rowMon);

                            DataRow rowPer = DTNotification.NewRow();
                            rowPer["Dato"] = "Periodicidad:";
                            rowPer["Descripcion"] = ddlPeriodicidad.SelectedItem.Text;
                            DTNotification.Rows.Add(rowPer);

                            DataRow rowOcu = DTNotification.NewRow();
                            rowOcu["Dato"] = "Ocurrencias:";
                            rowOcu["Descripcion"] = ddlOcurrencias.SelectedItem.Text;
                            DTNotification.Rows.Add(rowOcu);

                            DataRow rowFE = DTNotification.NewRow();
                            rowFE["Dato"] = "Fecha Emision:";
                            rowFE["Descripcion"] = FechaInicial;
                            DTNotification.Rows.Add(rowFE);

                            DataRow rowFV = DTNotification.NewRow();
                            rowFV["Dato"] = "Fecha Vencimiento:";
                            rowFV["Descripcion"] = FechaFinal;
                            DTNotification.Rows.Add(rowFV);

                            DataRow rowObs = DTNotification.NewRow();
                            rowObs["Dato"] = "Comentarios:";
                            rowObs["Descripcion"] = txtComtarios.Text;
                            DTNotification.Rows.Add(rowObs);

                            Body = "Se realizó el alta de la solicitud para un bono automático </br></br>" + "\r\n" + ConvertToHtmlFile(DTNotification);
                            Bonos_automaticos.sp_NotificacionBondAutomatic(Body, 1, Session["usuario"] as string, txtNumEmpleadoAuto.Text.Trim());                            
                            repeater_solicitud_auto.DataSource = Bonos_automaticos.sp_GetRequests_Bonds_Automatic_Type(Convert.ToInt32(txtNumEmpleadoAuto.Text.Trim())).Tables[0];
                            repeater_solicitud_auto.DataBind();
                            LimpiarControlesSolicitudBonosAutomaticos();
                            Toast.Success("Se realizó correctamente el alta de la solicitud", "Alta de solicitud", this);
                        }
                        else
                        {
                            Toast.Error(vmensaje_error, this);
                        }
                    }
                    catch (Exception ex)
                    {

                        Toast.Error("Error al solicitar bono automatico: " + ex.Message, this);
                    }
                }
                else if (hdfComandAutomaticos.Value == "Actualizar")
                {
                    try
                    {
                        DataSet ds = new DataSet();
                        ds = Bonos_automaticos.sp_UpdateResquestBondsAutomatic(requests_bonds_Automatic);

                        DataTable dt = new DataTable();
                        dt = ds.Tables[0];
                        string Body = "";
                        string vmensaje_error = dt.Rows[0]["mensaje"].ToString();
                        if (vmensaje_error == "")
                        {
                            DataTable DTNotification = new DataTable();
                            DTNotification.Columns.Add("Dato");
                            DTNotification.Columns.Add("Descripcion");

                            DataRow rowNumE = DTNotification.NewRow();
                            rowNumE["Dato"] = "Numero de empleado:";
                            rowNumE["Descripcion"] = txtNumEmpleadoAuto.Text;
                            DTNotification.Rows.Add(rowNumE);

                            DataRow rowEmp = DTNotification.NewRow();
                            rowEmp["Dato"] = "Empleado:";
                            rowEmp["Descripcion"] = txtNombEmpleadoAuto.Text;
                            DTNotification.Rows.Add(rowEmp);

                            DataRow rowBono = DTNotification.NewRow();
                            rowBono["Dato"] = "Bono Automatico:";
                            rowBono["Descripcion"] = ddlBondsType.SelectedItem.Text;
                            DTNotification.Rows.Add(rowBono);

                            DataRow rowMon = DTNotification.NewRow();
                            rowMon["Dato"] = "Monto:";
                            rowMon["Descripcion"] = "$" + txtMonto.Text;
                            DTNotification.Rows.Add(rowMon);

                            DataRow rowPer = DTNotification.NewRow();
                            rowPer["Dato"] = "Periodicidad:";
                            rowPer["Descripcion"] = ddlPeriodicidad.SelectedItem.Text;
                            DTNotification.Rows.Add(rowPer);

                            DataRow rowOcu = DTNotification.NewRow();
                            rowOcu["Dato"] = "Ocurrencias:";
                            rowOcu["Descripcion"] = ddlOcurrencias.SelectedItem.Text;
                            DTNotification.Rows.Add(rowOcu);

                            DataRow rowFE = DTNotification.NewRow();
                            rowFE["Dato"] = "Fecha Emision:";
                            rowFE["Descripcion"] = FechaInicial;
                            DTNotification.Rows.Add(rowFE);

                            DataRow rowFV = DTNotification.NewRow();
                            rowFV["Dato"] = "Fecha Vencimiento:";
                            rowFV["Descripcion"] = FechaFinal;
                            DTNotification.Rows.Add(rowFV);

                            DataRow rowObs = DTNotification.NewRow();
                            rowObs["Dato"] = "Comentarios:";
                            rowObs["Descripcion"] = txtComtarios.Text;
                            DTNotification.Rows.Add(rowObs);

                            Body = "Se realizó la modificacion del bono automático </br></br>" + "\r\n" + ConvertToHtmlFile(DTNotification);
                            Bonos_automaticos.sp_NotificacionBondAutomatic(Body, 2, Session["usuario"] as string, txtNumEmpleadoAuto.Text.Trim());                            
                            repeater_solicitud_auto.DataSource = Bonos_automaticos.sp_GetRequests_Bonds_Automatic_Type(Convert.ToInt32(txtNumEmpleadoAuto.Text.Trim())).Tables[0];
                            repeater_solicitud_auto.DataBind();
                            LimpiarControlesSolicitudBonosAutomaticos();
                            Toast.Success("Se realizó correctamente la actualizacion de la solicitud", "Alta de solicitud", this);
                        }
                     }
                    catch (Exception ex)
                    {
                        Toast.Error("Error al solicitar bono automatico: " + ex.Message, this);
                    }
                }
            }

            UnBlockUI();
        }

        protected void txtMonto_TextChanged(object sender, EventArgs e)
        {
            decimal no_ = Convert.ToDecimal(txtMonto.Text == "" ? "0" : txtMonto.Text);
            if (no_ > 0)
            {
                txtMonto.Text = no_.ToString("C");
                txtMonto.BorderColor = System.Drawing.Color.Green;
            }
            else
            {
                txtMonto.BorderColor = System.Drawing.Color.Red;
                Toast.Warning("El Monto debe ser mayor a cero ", "Mensaje del sistema", this);
            }

            UnBlockUI();
        }

        protected void lnkcancelarSol_Click(object sender, EventArgs e)
        {
            if (txtcomentarioscancela.Text == "")
            {
                Toast.Warning("Ingrese Comentarios de Cancelación", "Dato requerido", this);
            }
            else
            {
                string Id_request_bond = hdfId_request_bond_automatic.Value;
                string bond_name = hdfbond_name.Value;
                string IdBonds = hdfIdBonds.Value;
                string Monto = hdfMonto.Value;
                string Periodo = hdfPeriodo.Value;
                string Ocurrencias = hdfOcurrencias.Value;
                string OcurrenciasPend = hdfOcurrenciasPend.Value;
                string CC_Cargo = hdfCC_Cargo.Value;
                string Estatus = hdfEstatus.Value;
                string Comentarios = hdfComentarios.Value;
                string Fecha_Inicial = hdfFecha_Inicial.Value;
                string Fecha_Final = hdfFecha_Final.Value;
                CompensacionesEmpleadosCOM Bonos_automaticos = new CompensacionesEmpleadosCOM();
                if (Estatus == "SOLICITADA" || Estatus == "AUTORIZADA")
                {
                    requests_bonds_Automatic requests_bonds_Automatic = new requests_bonds_Automatic();
                    requests_bonds_Automatic.Id_request_bond_automatic = Convert.ToInt32(hdfId_request_bond_automatic.Value);
                    requests_bonds_Automatic.Employee_number = Convert.ToInt32(txtNumEmpleadoAuto.Text);
                    requests_bonds_Automatic.UpdateDelete_By = Session["usuario"] as string;
                    requests_bonds_Automatic.Delete_comments = txtcomentarioscancela.Text;

                    try
                    {
                        Bonos_automaticos.sp_UpdateDeleteRequestBondsAutomatic(requests_bonds_Automatic);

                        string Body = "";
                        DataTable DTNotification = new DataTable();
                        DTNotification.Columns.Add("Dato");
                        DTNotification.Columns.Add("Descripcion");

                        DataRow rowNumE = DTNotification.NewRow();
                        rowNumE["Dato"] = "Numero de empleado:";
                        rowNumE["Descripcion"] = txtNumEmpleadoAuto.Text;
                        DTNotification.Rows.Add(rowNumE);

                        DataRow rowEmp = DTNotification.NewRow();
                        rowEmp["Dato"] = "Empleado:";
                        rowEmp["Descripcion"] = txtNombEmpleadoAuto.Text;
                        DTNotification.Rows.Add(rowEmp);

                        DataRow rowBono = DTNotification.NewRow();
                        rowBono["Dato"] = "Bono Automatico:";
                        rowBono["Descripcion"] = ddlBondsType.SelectedItem.Text;
                        DTNotification.Rows.Add(rowBono);

                        DataRow rowMon = DTNotification.NewRow();
                        rowMon["Dato"] = "Monto:";
                        rowMon["Descripcion"] = "$" + txtMonto.Text;
                        DTNotification.Rows.Add(rowMon);

                        DataRow rowPer = DTNotification.NewRow();
                        rowPer["Dato"] = "Periodicidad:";
                        rowPer["Descripcion"] = ddlPeriodicidad.SelectedItem.Text;
                        DTNotification.Rows.Add(rowPer);

                        DataRow rowOcu = DTNotification.NewRow();
                        rowOcu["Dato"] = "Ocurrencias:";
                        rowOcu["Descripcion"] = ddlOcurrencias.SelectedItem.Text;
                        DTNotification.Rows.Add(rowOcu);

                        DataRow rowFE = DTNotification.NewRow();
                        rowFE["Dato"] = "Fecha Emision:";
                        rowFE["Descripcion"] = Fecha_Inicial;
                        DTNotification.Rows.Add(rowFE);

                        DataRow rowFV = DTNotification.NewRow();
                        rowFV["Dato"] = "Fecha Vencimiento:";
                        rowFV["Descripcion"] = Fecha_Final;
                        DTNotification.Rows.Add(rowFV);

                        DataRow rowObs = DTNotification.NewRow();
                        rowObs["Dato"] = "Comentarios:";
                        rowObs["Descripcion"] = txtcomentarioscancela.Text;
                        DTNotification.Rows.Add(rowObs);

                        Body = "Se realizó la cancelación de la solicitud del bono automático </br></br>" + "\r\n" + ConvertToHtmlFile(DTNotification);
                        Bonos_automaticos.sp_NotificacionBondAutomatic(Body, 3, Session["usuario"] as string, txtNumEmpleadoAuto.Text.Trim());
                        repeater_solicitud_auto.DataSource = Bonos_automaticos.sp_GetRequests_Bonds_Automatic_Type(Convert.ToInt32(txtNumEmpleadoAuto.Text.Trim())).Tables[0];
                        repeater_solicitud_auto.DataBind();
                        LimpiarControlesSolicitudBonosAutomaticos();
                        Toast.Success("Se realizó correctamente el cancelación de la solicitud", "Alta de solicitud", this);
                    }
                    catch (Exception ex)
                    {

                        Toast.Error("Error al cancelar bono automatico: " + ex.Message, this);
                    }
                }
                else
                {
                    Toast.Warning("No se puede cancelar la solicitud porque no tiene el estatus de Solicitada o Autorizada ", "Aviso del sistema    ", this);
                }
            }
            UnBlockUI();
        }

        protected void btnDetalle_Click(object sender, EventArgs e)
        {
            int Id_request_bond_automatic = Convert.ToInt32(hdfId_request_bond_automatic.Value);
            CompensacionesEmpleadosCOM Detalle = new CompensacionesEmpleadosCOM();
            DataTable dt = Detalle.sp_GetRequests_Bonds_Automatic_Type_Detalle(Id_request_bond_automatic).Tables[0];
            repeater_Detalle_sol.DataSource = dt;
            repeater_Detalle_sol.DataBind();
            ModalShow("#modal_Detalle_sol");
            UnBlockUI();

        }
    }
}