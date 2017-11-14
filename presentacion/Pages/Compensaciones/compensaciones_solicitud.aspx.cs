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
    public partial class compensaciones_solicitud : System.Web.UI.Page
    {
        #region Funciones

        /// <summary>
        /// Carga los tipos de bonos
        /// </summary>
        /// <param name="usuario"></param>
        private void CargarTiposBonos(string usuario)
        {
            try
            {
                BonosCOM bonos = new BonosCOM();
                DataSet ds = bonos.tipos_bonos(true, usuario);
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    cbBonds_Types.DataValueField = "id_bond_type";
                    cbBonds_Types.DataTextField = "name";
                    cbBonds_Types.DataSource = dt;
                    cbBonds_Types.DataBind();
                    cbBonds_Types.Items.Insert(0, new ListItem("--Seleccione un tipo de bono--", "0"));
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar los tipos de bonos: " + ex.ToString(), this);
            }
        }

        /// <summary>
        /// Limpia los controles
        /// </summary>
        private void ClearFields()
        {
            hdndesc_cc.Value = "";
            hdnfolio.Value = "";
            hdnproyecto.Value = "";
            hdncliente.Value = "";
            Session[hdfguid.Value + "list_documentos"] = null;
            //this.ClientScript.RegisterStartupScript(this.GetType(), "execute ClearFields", "<script language='javascript'>ClearFields();</script>");
            this.txtAuthorizationAmount.Text = "";
            txtfilterempleado.Text = "";
            this.txtComments.Text = "";
            hdnEmployeeNumber.Value = "";
            this.txtCustomerNameImplementations.Text = "";
            if (!this.tblMonthSelect.Visible)
            {
                this.txtPeriodDateOf.SelectedDate = null;
                this.txtPeriodDateTo.SelectedDate = null;
            }
            trFinalizeMonth.Visible = false;
            lblTitleMonth.Text = "Mes:";
            this.txtNumberHoursImplementations.Text = "";
            this.txtPMTrackerNumberImplementations.Text = "";
            this.txtProjectNameImplementations.Text = "";
            this.hdnAuthorizationAmount.Value = "";
            this.hdnFilesRequeried.Value = "";
            this.hdnFinalizeYear.Value = "";
            this.hdnIdPeriodicity.Value = "";
            this.hdnMonto.Value = "";
            this.hdnSubio.Value = "";
            this.txtPeriodDateOf.SelectedDate = null;
            this.txtPeriodDateTo.SelectedDate = null;
            this.hdnCC_Cargo.Value = string.Empty;
            this.txtCC_Cargo.Text = string.Empty;
            this.txtCC_Emp.Text = string.Empty;

            hdnMontoOriginal.Value = "";
            hdnauthorization_total_bonds.Value = "";
            hdnauthorization_total_amount.Value = "";

            LoadInitalYears();
            LoadInitialMonths();
            cbInitialMonth_SelectedIndexChanged(null,null);

        }

        private DataTable informacion_empleado(int no_)
        {
            try
            {
                BonosCOM bonos = new BonosCOM();
                DataSet ds = bonos.sp_GetEmployeeForBondType(Convert.ToInt32(cbBonds_Types.SelectedValue));
                DataTable dt_empleados = new DataTable();
                if (ds.Tables.Count > 0)
                {
                    DataView dv_empleados = ds.Tables[0].DefaultView;
                    dv_empleados.RowFilter = "employee_number = " + no_.ToString() + "";
                   
                    dt_empleados = dv_empleados.ToTable();
                }
                return dt_empleados;
            }
            catch (Exception)
            {
                return new DataTable();
            }
        }

        private DataSet listado_empleados()
        {
            try
            {
                int NumJefe = Convert.ToInt32(Session["NumJefe"]);
                int num_empleado = Convert.ToInt32(Session["num_empleado"]);
                Boolean ver_Todos_los_empleados = Convert.ToBoolean(Session["ver_Todos_los_empleados"]);
                if (funciones.Permisos(Session["usuario"] as string, 4))
                {
                    ver_Todos_los_empleados = true;
                }
                EmpleadosCOM empleados = new EmpleadosCOM();
                bool no_activos = false;
                DataSet ds = empleados.sp_listado_empleados(num_empleado, ver_Todos_los_empleados, no_activos);
                return ds;
            }
            catch (Exception ex)
            {
                Toast.Error("Error al iniciar busqueda de empleados: " + ex.Message, this);
                return new DataSet();
            }
        }

        /// <summary>
        /// arga el listado de empleados
        /// </summary>
        /// <param name="filtro"></param>
        protected void CargarEmpleados(string filtro)
        {
            try
            {
                int num_empleado = Convert.ToInt32(Session["num_empleado"]);

                BonosCOM bonos = new BonosCOM();
                DataSet ds = bonos.sp_GetEmployeeForBondType(Convert.ToInt32(cbBonds_Types.SelectedValue));
                DataTable dt_empleados = ds.Tables[0];
                repeater_empleados.DataSource = dt_empleados;
                repeater_empleados.DataBind();
                ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "InitPagging('#table_empleados');", true);
            }
            catch (Exception ex)
            {
                Toast.Error("Error al iniciar busqueda de empleados: " + ex.Message, this);
            }
            finally
            {
            }
        }
        
        private void LoadInitalYears()
        {

            DataTable TableYears = funciones.años();

            this.cbInitialYear.DataSource = TableYears;
            this.cbInitialYear.DataTextField = "name";
            this.cbInitialYear.DataValueField = "value";
            this.cbInitialYear.DataBind();

            this.cbInitialYear.SelectedValue = System.DateTime.Today.Year.ToString();
            if (DateTime.Now.Month != 1)
            {
                this.cbInitialYear.SelectedValue = System.DateTime.Today.Year.ToString();
            }
            else
            {
                this.cbInitialYear.SelectedValue = (System.DateTime.Today.Year-1).ToString();
            }
        }

        private void LoadInitialMonths()
        {
            DataTable TableMonths = funciones.meses();

            this.cbInitialMonth.DataSource = TableMonths;
            this.cbInitialMonth.DataTextField = "name";
            this.cbInitialMonth.DataValueField = "value";
            this.cbInitialMonth.DataBind();
            if (DateTime.Now.Month != 1)
            {
                this.cbInitialMonth.SelectedValue = DateTime.Now.Month.ToString();
            }
            else
            {
                this.cbInitialMonth.SelectedValue = "12";
                this.cbInitialYear.SelectedValue = (DateTime.Now.Year-1).ToString();
            }
        }
        private void LoadFinalizeYears()
        {

            DataTable TableYears = funciones.años();

            this.cbFinalizeYear.DataSource = TableYears;
            this.cbFinalizeYear.DataTextField = "name";
            this.cbFinalizeYear.DataValueField = "value";
            this.cbFinalizeYear.DataBind();

            this.cbFinalizeYear.SelectedValue = System.DateTime.Today.Year.ToString();
            if (DateTime.Now.Month > 1)
            {
                if (string.IsNullOrEmpty(this.cbFinalizeYear.SelectedValue))
                {
                    this.cbFinalizeYear.SelectedValue = DateTime.Now.Year.ToString();
                }
            }
            else
            {
                if (string.IsNullOrEmpty(this.cbFinalizeYear.SelectedValue))
                {
                    this.cbFinalizeYear.SelectedValue = Convert.ToInt32(DateTime.Now.Year - 1).ToString();
                }
            }

        }

        private void LoadFinalizeMonths()
        {
            DataTable TableMonths = funciones.meses();

            this.cbFinalizeMonth.DataSource = TableMonths;
            this.cbFinalizeMonth.DataTextField = "name";
            this.cbFinalizeMonth.DataValueField = "value";
            this.cbFinalizeMonth.DataBind();
            if (DateTime.Now.Month > 1)
            {
                this.cbFinalizeMonth.SelectedValue = DateTime.Now.Month.ToString();
            }
            else
            {
                this.cbFinalizeMonth.SelectedValue = "12";
                this.cbFinalizeMonth.SelectedValue = (DateTime.Now.Year - 1).ToString();
            }

        }

        // calcula la fecha inicial a partir de la fecha final, con respecto al tipo de periodo de bono
        private void SelectedMonthAndYear()
        {
            if (this.hdnIdPeriodicity.Value == ((int)enumerators.Periodicity.quarterly).ToString())
            {
                // si es trimaestral

                // valida  si el mes es enero o febrero
                //Si es principo de año (enero, febrero)
                if (this.cbFinalizeMonth.SelectedValue == ((int)enumerators.Months.January).ToString() |
                    this.cbFinalizeMonth.SelectedValue == ((int)enumerators.Months.February).ToString())
                {
                    //si es enero, sino febrero
                    if (this.cbFinalizeMonth.SelectedValue == ((int)enumerators.Months.January).ToString())
                    {
                        // el mes sera noviembre y el año se le resta 1
                        this.cbInitialMonth.SelectedValue = ((int)enumerators.Months.November).ToString();
                        this.cbInitialYear.SelectedValue = (Convert.ToInt32(this.cbFinalizeYear.SelectedValue) - 1).ToString();
                    }
                    else
                    {
                        // el mes sera diciembre y el año se le resta 1
                        this.cbInitialMonth.SelectedValue = ((int)enumerators.Months.December).ToString();
                        this.cbInitialYear.SelectedValue = (Convert.ToInt32(this.cbFinalizeYear.SelectedValue) - 1).ToString();
                    }
                }
                else
                {
                    // si no es enero o febrero, se restan dos meses del mes final
                    this.cbInitialMonth.SelectedValue = (Convert.ToInt32(this.cbFinalizeMonth.SelectedValue) - 2).ToString();
                    this.cbInitialYear.SelectedValue = this.cbFinalizeYear.SelectedValue;
                }
            }

            if (this.hdnIdPeriodicity.Value == ((int)enumerators.Periodicity.semiannual).ToString())
            {
                // si el bono es semestral

                // valida si el mes final es enero, febrero, marzo abril o mayo
                if (this.cbFinalizeMonth.SelectedValue == ((int)enumerators.Months.January).ToString() | 
                    this.cbFinalizeMonth.SelectedValue == ((int)enumerators.Months.February).ToString() |
                    this.cbFinalizeMonth.SelectedValue == ((int)enumerators.Months.March).ToString() | 
                    this.cbFinalizeMonth.SelectedValue == ((int)enumerators.Months.April).ToString() |
                    this.cbFinalizeMonth.SelectedValue == ((int)enumerators.Months.May).ToString())
                {
                    //Enero
                    if (this.cbFinalizeMonth.SelectedValue == ((int)enumerators.Months.January).ToString())
                    {
                        this.cbInitialMonth.SelectedValue = ((int)enumerators.Months.August).ToString();
                        this.cbInitialYear.SelectedValue = (Convert.ToInt32(this.cbFinalizeYear.SelectedValue) - 1).ToString();
                    }
                    //febrero
                    if (this.cbFinalizeMonth.SelectedValue == ((int)enumerators.Months.February).ToString())
                    {
                        this.cbInitialMonth.SelectedValue = ((int)enumerators.Months.September).ToString();
                        this.cbInitialYear.SelectedValue = (Convert.ToInt32(this.cbFinalizeYear.SelectedValue) - 1).ToString();
                    }

                    //marzo
                    if (this.cbFinalizeMonth.SelectedValue == ((int)enumerators.Months.March).ToString())
                    {
                        this.cbInitialMonth.SelectedValue = ((int)enumerators.Months.October).ToString();
                        this.cbInitialYear.SelectedValue = (Convert.ToInt32(this.cbFinalizeYear.SelectedValue) - 1).ToString();
                    }

                    // abril
                    if (this.cbFinalizeMonth.SelectedValue == ((int)enumerators.Months.April).ToString())
                    {
                        this.cbInitialMonth.SelectedValue = ((int)enumerators.Months.November).ToString();
                        this.cbInitialYear.SelectedValue = (Convert.ToInt32(this.cbFinalizeYear.SelectedValue) - 1).ToString();
                    }

                    // mayo
                    if (this.cbFinalizeMonth.SelectedValue == ((int)enumerators.Months.May).ToString())
                    {
                        this.cbInitialMonth.SelectedValue = ((int)enumerators.Months.December).ToString();
                        this.cbInitialYear.SelectedValue = (Convert.ToInt32(this.cbFinalizeYear.SelectedValue) - 1).ToString();
                    }
                }
                else
                {
                    this.cbInitialMonth.SelectedValue = (Convert.ToInt32(this.cbFinalizeMonth.SelectedValue) - 5).ToString();
                    // cualquier otro mes le resta 5 al mes final
                    this.cbInitialYear.SelectedValue = this.cbFinalizeYear.SelectedValue;
                }
            }

            if (this.hdnIdPeriodicity.Value == ((int)enumerators.Periodicity.bimestral).ToString())
            {
                // es bimestral
                //Si es principo de año (enero, febrero)
                if (this.cbFinalizeMonth.SelectedValue == ((int)enumerators.Months.January).ToString())
                {
                    //si es enero, sino febrero
                    if (this.cbFinalizeMonth.SelectedValue == ((int)enumerators.Months.January).ToString())
                    {
                        this.cbInitialMonth.SelectedValue = ((int)enumerators.Months.December).ToString();
                        this.cbInitialYear.SelectedValue = (Convert.ToInt32(this.cbFinalizeYear.SelectedValue) - 1).ToString();
                    }
                }
                else
                {
                    this.cbInitialMonth.SelectedValue = (Convert.ToInt32(this.cbFinalizeMonth.SelectedValue) - 1).ToString();
                    this.cbInitialYear.SelectedValue = this.cbFinalizeYear.SelectedValue;
                }
            }

            if (this.hdnIdPeriodicity.Value == ((int)enumerators.Periodicity.monthly).ToString())
            {
                // es mensual
                this.cbInitialMonth.SelectedValue = (Convert.ToInt32(this.cbFinalizeMonth.SelectedValue) - 1).ToString();
                this.cbInitialYear.SelectedValue = this.cbFinalizeYear.SelectedValue;
            }

            if (this.hdnIdPeriodicity.Value == ((int)enumerators.Periodicity.annual).ToString())
            {
                // es anual
                this.cbInitialMonth.SelectedValue = this.cbFinalizeMonth.SelectedValue;
                this.cbInitialYear.SelectedValue = (Convert.ToInt32(this.cbFinalizeYear.SelectedValue) - 1).ToString();
            }
            string date_initial = this.cbInitialYear.SelectedValue +"-"+ this.cbInitialMonth.SelectedValue.PadLeft(2, '0') +"-01";
            string date_final = "";
            if (this.cbFinalizeYear.SelectedValue != "")
            {
                date_final = this.cbFinalizeYear.SelectedValue + "-" + this.cbFinalizeMonth.SelectedValue.PadLeft(2, '0') + "-" + System.DateTime.DaysInMonth(Convert.ToInt32(this.cbFinalizeYear.SelectedValue), Convert.ToInt32(this.cbFinalizeMonth.SelectedValue)).ToString().PadLeft(2, '0');
            }
            this.txtPeriodDateOf.SelectedDate = Convert.ToDateTime(date_initial);

            if (date_final == "")
            {

                this.txtPeriodDateTo.SelectedDate = Convert.ToDateTime(date_initial).AddMonths(1).AddDays(-1);
            }
            else {
                this.txtPeriodDateTo.SelectedDate = Convert.ToDateTime(date_final);
            }

        }


        private void CargarInformacionBonoEmpleado(int no_)
        {
            try
            {

              DataSet ds = new DataSet();
                DataSet dsEmpleado = new DataSet();
                hdnMontoOriginal.Value = "";
                BonosCOM bonos = new BonosCOM();
                EmpleadosCOM empleados = new EmpleadosCOM();
                //Obtiene bonos del empleado por tipo de bono
                ds = bonos.sp_GetEmployeesCompensationsForEmployeeNumberAndBondType(no_, Convert.ToInt32(this.cbBonds_Types.SelectedValue), Convert.ToDateTime(this.txtPeriodDateOf.SelectedDate));

                dsEmpleado = empleados.sp_GetEmployeeEnabledForEmployeeNumber(no_, 1);

                if (dsEmpleado.Tables[0].Rows.Count > 0)
                {
                    if ((hdnCC_Cargo.Value != string.Empty))
                    {
                        if (hdnCC_Cargo.Value != txtCC_Cargo.Text)
                        {
                            txtCC_Cargo.Text = hdnCC_Cargo.Value;
                        }
                    }
                }

                if (ds.Tables[0].Rows.Count > 0)
                {
                    //obtiene el periodo para el bono y si requiere archivos 
                    this.hdnIdPeriodicity.Value = ds.Tables[0].Rows[0]["id_periodicity"].ToString();
                    this.hdnFilesRequeried.Value = ds.Tables[0].Rows[0]["files_required"].ToString();

                    // aun no se para que es esto... :S
                    if (string.IsNullOrEmpty(hdnSubio.Value))
                    {
                        hdnSubio.Value = "NO-" + no_.ToString();
                    }
                    if (!hdnSubio.Value.ToString().Contains(no_.ToString()))
                    {
                        hdnSubio.Value = "NO-" + no_.ToString();
                    }
                    
                    if (this.tblMonthSelect.Visible & ds.Tables[0].Rows[0]["id_periodicity"].ToString() != ((int)enumerators.Periodicity.monthly).ToString())
                    {
                        // si el periodo del bono es diferente de mensual se calculan el inicio y fin del periodo del bono
                        this.lblTitleMonth.Text = "Mes Inicial:";
                        this.trFinalizeMonth.Visible = true;
                        //If Me.txtAuthorizationAmount.Text = "" Then
                        //calcula la fecha final
                        LoadFinalizeMonths();
                        // calcula la fecha inicial
                        LoadFinalizeYears();
                        //End If

                        // se almacena la fecha final en sus respectivos campos de texto con en el formato correspondiente
                        this.txtPeriodDateTo.SelectedDate = Convert.ToDateTime(System.DateTime.DaysInMonth(Convert.ToInt32(this.cbFinalizeYear.SelectedValue), Convert.ToInt32(this.cbFinalizeMonth.SelectedValue)) + "/" + this.cbFinalizeMonth.SelectedValue.PadLeft(2, '0') + "/" + this.cbFinalizeYear.SelectedValue);
                        // calcula la fecha inicial a partil de la fecha final con respecto al tipo de bono utilizado
                        SelectedMonthAndYear();
                        // se almacena la fecha inicial en sus respectivos campos de texto con en el formato correspondiente
                        this.txtPeriodDateOf.SelectedDate = Convert.ToDateTime("01/" + this.cbInitialMonth.SelectedValue.PadLeft(2, '0') + "/" + this.cbInitialYear.SelectedValue);

                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(this.txtAuthorizationAmount.Text) & ds.Tables[0].Rows[0]["id_periodicity"].ToString() != ((int)enumerators.Periodicity.monthly).ToString()
                            & this.cbBonds_Types.SelectedValue != "2" & this.cbBonds_Types.SelectedValue != "3")
                        {
                            this.txtPeriodDateOf.SelectedDate = Convert.ToDateTime("01/" + this.cbFinalizeMonth.SelectedValue.PadLeft(2, '0') + "/" + this.cbFinalizeYear.SelectedValue);
                            LoadInitialMonths();
                        }
                        this.lblTitleMonth.Text = "Mes:";
                        this.trFinalizeMonth.Visible = false;
                    }
                    this.hdnValidateAmount.Value = string.Empty;
                    if (this.txtAuthorizationAmount.Attributes["readonly"] != "true")
                    {
                        if (Convert.ToDecimal(ds.Tables[0].Rows[0]["amount"]) > 0)
                        {
                            decimal percentage = (Convert.ToDecimal(ds.Tables[0].Rows[0]["percentage_extra"]) / Convert.ToDecimal(100));
                            decimal amountValidate = (Convert.ToDecimal(ds.Tables[0].Rows[0]["amount"]) * percentage);
                            decimal authorization_total_bonds =Convert.ToDecimal(ds.Tables[1].Rows[0]["total_bonos"]);
                            decimal authorization_total_amount = Convert.ToDecimal(ds.Tables[1].Rows[0]["total_monto"]);
                            hdnMontoOriginal.Value = amountValidate.ToString();
                            hdnauthorization_total_bonds.Value = authorization_total_bonds.ToString();
                            hdnauthorization_total_amount.Value = authorization_total_amount.ToString();
                            txtAuthorizationAmount.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["amount"]).ToString();
                            hdnValidateAmount.Value = Convert.ToDecimal(ds.Tables[0].Rows[0]["amount"]).ToString();
                        }
                    }
                    LoadInitalYears();
                    LoadFinalizeYears();
                    txtAuthorizationAmount_TextChanged(null,null);
                }


            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar información del bono: "+ex.Message,this);
            }
        }


        /// <summary>
        /// Selecciona la semana del dia seleccionado
        /// </summary>
        /// <param name="selectedDate"></param>
        /// <param name="toolTip"></param>
        /// <param name="cssClass"></param>
        private void SelectedWeek(System.DateTime selectedDate, string toolTip, string cssClass)
        {
            try
            {
                BonosCOM bonos = new BonosCOM();
                DataSet ds = bonos.sp_GetBondsTypesEnabledAndID(Convert.ToInt32(cbBonds_Types.SelectedValue));
                this.hdnAuthorizationAmount.Value = ds.Tables[0].Rows[0]["amount"].ToString();
                this.txtAuthorizationAmount.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["amount"]).ToString("C2");

                DataTable TableJobDays = new DataTable();
                TableJobDays.Columns.Add("id");
                TableJobDays.Columns.Add("date");
                TableJobDays.Columns.Add("amount");

                DateTime first_date = funciones.FirstDateInWeek(selectedDate, DayOfWeek.Monday);

                for (int i = 0; i < 7; i++)
                {
                    DataRow new_row = TableJobDays.NewRow();
                    new_row["id"] = i+1;
                    DateTime fecha = first_date.AddDays(i);
                    new_row["date"] = fecha;
                    new_row["amount"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[0]["amount"]) / Convert.ToDecimal(7), 2);
                    TableJobDays.Rows.Add(new_row);
                }
                txtPeriodDateOf.SelectedDate = first_date;
                txtPeriodDateTo.SelectedDate = first_date.AddDays(6);

                this.repeater_fechas_Soporte.DataSource = TableJobDays;
                this.repeater_fechas_Soporte.DataBind();
            }
            catch (Exception ex)
            {
                Toast.Error(ex.Message,this);
            }
        }
        
        /// <summary>
        /// Calcula el total del monto autorizado para soporte
        /// </summary>
        private void CalcularMontoTotalAutorizadoSoporte()
        {
            try
            {
                decimal total = 0;
                foreach (RepeaterItem item in repeater_fechas_Soporte.Items)
                {
                    CheckBox cbx = item.FindControl("cbx_checkday") as CheckBox;
                    if (cbx.Checked)
                    {
                        decimal value = Convert.ToDecimal(cbx.Attributes["amount"].ToString());
                        total = total + value;
                    }
                }
                total = Math.Round(total,0);
                lblmonto_total_autorizadp.Text = total.ToString("C2");
                txtAuthorizationAmount.Text = total.ToString("C2");
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar información de soporte: " + ex.Message, this);
            }
        }

        /// <summary>
        /// Guarda temporalmente un documento
        /// </summary>
        /// <param name="path"></param>
        /// <param name="file_name"></param>
        /// <param name="content_type"></param>
        public void AgregarDocumento(string path, string file_name, string content_type)
        {
            try
            {
                if (Session[hdfguid.Value + "list_documentos"] == null)
                {
                    List<files_requests_bonds> list_fr = new List<files_requests_bonds>();
                    Session[hdfguid.Value + "list_documentos"] = list_fr;
                }
                List<files_requests_bonds> list = Session[hdfguid.Value + "list_documentos"] as List<files_requests_bonds>;
                files_requests_bonds file = new files_requests_bonds();
                file.path = path;
                file.file_name = file_name;
                file.content_type = content_type;
                list.Add(file);
                Session[hdfguid.Value + "list_documentos"] = list;
            }
            catch (Exception ex)
            {
                Toast.Error("Error al guardar archivo: " + ex.Message, this);
            }

        }

        /// <summary>
        /// Elimina un documento
        /// </summary>
        /// <param name="file_name"></param>
        public void EliminarDocumento(string file_name)
        {
            try
            {
                if (Session[hdfguid.Value + "list_documentos"] == null)
                {
                    List<files_requests_bonds> list_fr = new List<files_requests_bonds>();
                    Session[hdfguid.Value + "list_documentos"] = list_fr;
                }
                List<files_requests_bonds> list = Session[hdfguid.Value + "list_documentos"] as List<files_requests_bonds>;

                foreach (files_requests_bonds file in list)
                {
                    if (file.file_name.ToString().Trim().Equals(file_name.Trim()))
                    {
                        list.Remove(file);
                        break;
                    }
                }
                Session[hdfguid.Value + "list_documentos"] = list;
            }
            catch (Exception ex)
            {
                Toast.Error("Error al guardar archivo: " + ex.Message, this);
            }

        }

        public void CargarTablaArchivos()
        {
            try
            {
                if (Session[hdfguid.Value + "list_documentos"] == null)
                {
                    List<files_requests_bonds> list_fr = new List<files_requests_bonds>();
                    Session[hdfguid.Value + "list_documentos"] = list_fr;
                }
                List<files_requests_bonds> list = Session[hdfguid.Value + "list_documentos"] as List<files_requests_bonds>;
                repeater_archivos.DataSource = list;
                repeater_archivos.DataBind();
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar archivos: " + ex.Message, this);
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

        #endregion Funciones

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string usuario = Session["usuario"] as string;
                hdfguid.Value = Guid.NewGuid().ToString();
                AsyncUpload1.FileUploaded += new Telerik.Web.UI.FileUploadedEventHandler(AsyncUpload1_FileUploaded);
                CargarTiposBonos(usuario.ToUpper());
                ClearFields();
                hdnMontoOriginal.Value = "";
                hdnauthorization_total_bonds.Value = "";
                hdnauthorization_total_amount.Value = "";
            }
        }

        protected void lnksearch_Click(object sender, EventArgs e)
        {
            try
            {
                CargarEmpleados("");
                ModalShow("#modal_empleados");
            }
            catch (Exception ex)
            {
                Toast.Error("Error al filtrar empleados: " + ex.Message, this);
            }
            finally
            {
                load.Style["display"] = "none";
            }
        }
        
        protected void lnksearchpmTracker_Click(object sender, EventArgs e)
        {
            try
            {
                string filter = "";
                BonosCOM bonos = new BonosCOM();
                DataSet ds = bonos.sp_GetConnextProjectsForLogin(Session["usuario"] as string, filter);
                DataTable dt = ds.Tables[0];
                repeat_proyectos.DataSource = dt;
                repeat_proyectos.DataBind();
                ModalShow("#modal_proyectos");
                ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "InitPagging('#table_proyectos');", true);

                ModalShow("#modal_proyectos");
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar proyectos: " + ex.Message, this);
            }
            finally {
                load.Style["display"] = "none";
            }
        }

        protected void txtAuthorizationAmount_TextChanged(object sender, EventArgs e)
        {
            bool correct = true;
            int no_ = Convert.ToInt32(hdnEmployeeNumber.Value==""?"0": hdnEmployeeNumber.Value);
            if (no_ > 0)
            {
                txtAuthorizationAmount.Text = txtAuthorizationAmount.Text == "" ? "0.00" : txtAuthorizationAmount.Text;
                if (cbBonds_Types.SelectedValue == "1")
                {
                    decimal monto_roiginal = (string.IsNullOrEmpty(hdnMontoOriginal.Value) ? 0 : Convert.ToDecimal(hdnMontoOriginal.Value));
                    decimal monto_actual = Convert.ToDecimal(txtAuthorizationAmount.Text.Replace("$", ""));

                    if (monto_actual > monto_roiginal)
                    {
                        correct = false;
                        float price = float.Parse(monto_roiginal.ToString());
                        txtAuthorizationAmount.Text = string.Format("{0:C}", price);
                        Toast.Error("El monto no puede exceder a " + string.Format("{0:C}", price), this);
                    }
                }
                decimal monto_actual1 = Convert.ToDecimal(txtAuthorizationAmount.Text.Replace("$", ""));

                string text = monto_actual1.ToString();
                if (correct)
                {
                    float price = float.Parse(text);
                    txtAuthorizationAmount.Text = string.Format("{0:C}", price);
                    decimal value_MOUNT = default(decimal);
                    value_MOUNT = Convert.ToDecimal(price);
                    if ((value_MOUNT < 0))
                    {
                        Toast.Error("El monto debe ser mayor a $ 0.00", this);
                    }
                }
            }
            else
            {
                txtAuthorizationAmount.Text = "";
                Toast.Info("Seleccione un empleado","Mensaje del sistema", this);
            }
          
        }

        protected void cbInitialMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SelectedMonthAndYear();
            }
            catch (Exception ex)
            {
                Toast.Error("Error al calcular fechas: " + ex.Message, this);
            }
        }

        protected void cbBonds_Types_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(cbBonds_Types.SelectedValue) > 0)
                {
                    ClearFields();

                    this.tblInformationRequisitions.Visible = true;

                    this.hdnIdTypeBonds.Value = this.cbBonds_Types.SelectedValue;
                    BonosCOM bonos = new BonosCOM();
                    DataSet ds = bonos.sp_GetBondsTypesEnabledAndID(Convert.ToInt32(cbBonds_Types.SelectedValue));
                    this.trWeek.Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["week_detail_required"]);
                    this.trProjectName.Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["project_name_required"]);
                    this.trNumberHours.Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["number_hours_required"]);
                    this.trFolioPMTracker.Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["folio_pmtracker_required"]);
                    this.trCustomerName.Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["customer_name_required"]);
                    this.trGridRequisitions.Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["grid_requisitions_required"]);
                    txtCC_Cargo.ReadOnly = true;
                    txtCC_Emp.ReadOnly = true;
                    txtCustomerNameImplementations.ReadOnly = true;
                    txtProjectNameImplementations.ReadOnly = true;
                    txtPMTrackerNumberImplementations.ReadOnly = true;
                    if (Convert.ToBoolean(ds.Tables[0].Rows[0]["authorization_amount_capture"]))
                    {
                        this.txtAuthorizationAmount.Attributes.Remove("readonly");
                    }
                    else
                    {
                        this.txtAuthorizationAmount.Attributes.Add("readonly", "true");
                    }
                    this.txtPeriodDateOf.Enabled = Convert.ToBoolean(ds.Tables[0].Rows[0]["period_date_of_capture"]);
                    this.txtPeriodDateTo.Enabled = Convert.ToBoolean(ds.Tables[0].Rows[0]["period_date_of_capture"]);


                    if (Convert.ToBoolean(ds.Tables[0].Rows[0]["month_select"]))
                    {
                        this.tblMonthSelect.Visible = true;
                        this.txtPeriodDateOf.Enabled = false;
                        this.txtPeriodDateTo.Enabled = false;
                        this.trFinalizeMonth.Visible = false;
                        LoadInitialMonths();
                        LoadInitalYears();
                        cbInitialMonth_SelectedIndexChanged(null, null);
                    }
                    else
                    {
                        this.tblMonthSelect.Visible = false;
                        this.txtPeriodDateOf.Enabled = true;
                        this.txtPeriodDateTo.Enabled = true;
                    }

                    this.txtPeriodDateOf.Attributes.Add("readonly", "true");
                    this.txtPeriodDateTo.Attributes.Add("readonly", "true");

                    if (this.trWeek.Visible)
                    {
                        calDateSupport.SelectedDate = DateTime.Now;
                        calDateSupport_SelectedDateChanged(null,null);
                    }
                    else
                    {
                        //LoadBondsRequests();
                    }
                }


            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar solicitud: " + ex.Message, this);
            }
        }

        protected void lnksearchcc_Click(object sender, EventArgs e)
        {
            try
            {
                string filter = "";
                BonosCOM bonos = new BonosCOM();
                DataSet ds = bonos.sp_Get_Tbl_Estructura_CC("","");
                DataTable dt = ds.Tables[0];
                repetaer_cc.DataSource = dt;
                repetaer_cc.DataBind();
                ModalShow("#modal_cc");
                ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "InitPagging('#table_cc');", true);

                ModalShow("#modal_cc");
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar centro de costos: " + ex.Message, this);
            }
            finally
            {
                load.Style["display"] = "none";
            }

        }

        protected void calDateSupport_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            ClearFields();
            DateTime selectedDate = Convert.ToDateTime(this.calDateSupport.SelectedDate);
            SelectedWeek(selectedDate, string.Empty, string.Empty);
            CalcularMontoTotalAutorizadoSoporte();
        }

        protected void cbx_checkday_CheckedChanged(object sender, EventArgs e)
        {
            CalcularMontoTotalAutorizadoSoporte();
        }

        protected void btncargarempleado_Click(object sender, EventArgs e)
        {
            try
            {
                int no_ = Convert.ToInt32(hdnEmployeeNumber.Value == "" ? "0" : hdnEmployeeNumber.Value);
                if (no_ > 0)
                {
                    DataTable dtempleado = informacion_empleado(no_);
                    if (dtempleado.Rows.Count > 0)
                    {
                        DataRow empleado = dtempleado.Rows[0];
                        txtCC_Cargo.Text = empleado["cc_name"].ToString();
                        txtCC_Emp.Text = empleado["cc_name"].ToString();
                        CargarInformacionBonoEmpleado(no_);
                        hdnEmployeeNumber.Value = no_.ToString();
                        txtfilterempleado.Text = empleado["full_name"].ToString();
                    }
                    else
                    {
                        Toast.Error("Error al cargar información del empleado.", this);
                    }
                }
                else
                {
                    Toast.Error("Ocurrio un error al seleccionar empleado, intentelo nuevamente.", this);
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar información del empleado: " + ex.Message, this);
            }
            finally {
                load.Style["display"] = "none";
            }
        }

        protected void lnkcancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("compensaciones_solicitud.aspx");
        }

        protected void lnkadjuntarfiles_Click(object sender, EventArgs e)
        {
            try
            {
                int no_ = Convert.ToInt32(hdnEmployeeNumber.Value == "" ? "0" : hdnEmployeeNumber.Value);

                if (no_ == 0)
                {
                    Toast.Error("Seleccione un empleado.", this);
                }
                else {

                    CargarTablaArchivos();
                    ModalShow("#modal_archivos");
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar documentos: " + ex.Message, this);
            }
            finally
            {
                load.Style["display"] = "none";
            }
        }

        protected void lnkguardaresultados_Click(object sender, EventArgs e)
        {
            try
            {
                int r = AsyncUpload1.UploadedFiles.Count;
                if (r == 0)
                {
                    Toast.Error("Error al guardar documento: Seleccione un documento.", this);
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al guardar resultado: " + ex.Message, this);
            }
            finally
            {
                //InicializarTablas();
            }

        }

        protected void AsyncUpload1_FileUploaded(object sender, Telerik.Web.UI.FileUploadedEventArgs e)
        {
            try
            {
                //GUARDAMOS LOS RESULTADOS DE LA ACTIVIDAD
                int r = AsyncUpload1.UploadedFiles.Count;
                int no_ = Convert.ToInt32(hdnEmployeeNumber.Value == "" ? "0" : hdnEmployeeNumber.Value);
                
                if (no_ == 0)
                {
                    Toast.Error("Seleccione un empleado.", this);
                }
                else
                {
                    string name = "";
                    DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~/"));//path localDateTime localDate = DateTime.Now;
                    string path_local = "files/documents/temp/";
                    string directory = dirInfo.ToString() + path_local;
                    if (!Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                    }
                    
                    name = Path.GetFileNameWithoutExtension(e.File.FileName) + Path.GetExtension(e.File.FileName);
                    e.File.SaveAs(directory + name.Trim());
                    AgregarDocumento(directory + name.Trim(),name.Trim(),e.File.ContentType);
                    CargarTablaArchivos();
                    Toast.Success("Documento guardado correctamente.","Mensaje del sistema",this);
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al guardar documento: " + ex.Message, this);
            }
            finally
            {
            }
        }

        protected void lnkdeletefile_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = sender as LinkButton;
                string file = lnk.Attributes["file_name"].ToString();
                EliminarDocumento(file);
                CargarTablaArchivos();
                Toast.Success("Documento eliminado correctamente.", "Mensaje del sistema", this);

            }
            catch (Exception ex)
            {
                Toast.Error("Error al eliminar documento: " + ex.Message, this);
            }
            finally {
                ModalShow("#modal_archivos");
            }
        }

        protected void lnkdownloadfile_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = sender as LinkButton;
                string path = lnk.Attributes["path"].ToString();
                if (File.Exists(path))
                {
                    Response.ContentType = "doc/docx";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(path));
                    Response.TransmitFile(path);
                    Response.End();
                }
                else
                {
                    Toast.Error("No es encuentra el documento especificado", this);
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al descargar documento: " + ex.Message, this);
            }
        }

        protected void lnkcc_Click(object sender, EventArgs e)
        {
            LinkButton lnk = sender as LinkButton;
            string desc_cc = hdndesc_cc.Value;
            string cc = hdnCC_Cargo.Value;
            txtCC_Cargo.Text = desc_cc;
            hdnCC_Cargo.Value = cc;
            //ModalClose("#modal_cc");
        }

        protected void lnkproyecto_Click(object sender, EventArgs e)
        {
            LinkButton lnk = sender as LinkButton;
            txtPMTrackerNumberImplementations.Text = hdnfolio.Value;
            txtProjectNameImplementations.Text = hdnproyecto.Value;
            txtCustomerNameImplementations.Text = hdncliente.Value;
            //ModalClose("#modal_proyectos");

        }
    }
}