using negocio.Componentes;
using negocio.Componentes.Compensaciones;
using System;
using System.Data;
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
            LoadInitalYears();
            LoadInitialMonths();
            //this.ClientScript.RegisterStartupScript(this.GetType(), "execute ClearFields", "<script language='javascript'>ClearFields();</script>");
            //this.txtAuthorizationAmount.Text = "";
            //this.txtComments.Text = "";
            //this.txtCustomerNameImplementations.Text = "";
            //if (!this.tblMonthSelect.Visible)
            //{
            //    this.txtPeriodDateOf.Text = "";
            //    this.txtPeriodDateTo.Text = "";
            //}
            //this.txtEmployeeName.Text = "";
            //this.txtNumberHoursImplementations.Text = "";
            //this.txtPMTrackerNumberImplementations.Text = "";
            //this.txtProjectNameImplementations.Text = "";
            //this.hdnEmployeeNumber.Value = "";
            //this.hdnAuthorizationAmount.Value = "";
            //this.hdnFilesRequeried.Value = "";
            //this.hdnFinalizeYear.Value = "";
            //this.hdnIdPeriodicity.Value = "";
            //this.hdnMonto.Value = "";
            //this.hdnSubio.Value = "";
            //this.hdnLogin.Value = "";
            //this.cPeriodDateOf.SelectedDate = null;
            //this.cPeriodDateTo.SelectedDate = null;
            //this.hdnCC_Cargo.Value = string.Empty;
            //this.txtCC_Cargo.Text = string.Empty;
            //this.txtCC_Emp.Text = string.Empty;

            //hdnMontoOriginal.Value = "";
            //hdnauthorization_total_bonds.Value = "";
            //hdnauthorization_total_amount.Value = "";
        }

        private DataTable informacion_empleado(int no_)
        {
            try
            {
                DataSet ds = listado_empleados();
                DataTable dt_empleados = new DataTable();
                if (ds.Tables.Count > 0)
                {
                    DataView dv_empleados = ds.Tables[0].DefaultView;
                    dv_empleados.RowFilter = "num_empleado = " + no_.ToString() + "";
                   
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
                Boolean ver_Todos_los_empleados = Convert.ToBoolean(Session["ver_Todos_los_empleados"]);

                DataSet ds = listado_empleados();
                DataTable dt_empleados = new DataTable();
                if (filtro != "")
                {
                    DataView dv_empleados = ds.Tables[0].DefaultView;
                    dv_empleados.RowFilter = "nombre like '%" + filtro + "%'";
                    if (dv_empleados.ToTable().Rows.Count <= 0)
                    {
                        dv_empleados.RowFilter = "usuario like '%" + filtro + "%'";
                    }
                    dt_empleados = dv_empleados.ToTable();
                }
                else
                {
                    dt_empleados = ds.Tables[0];
                }
                ddlempleado.DataValueField = "num_empleado";
                ddlempleado.DataTextField = "nombre";
                ddlempleado.DataSource = dt_empleados;
                ddlempleado.DataBind();
                ddlempleado.Items.Insert(0,new ListItem("--Seleccione un empleado--","0"));
                if (!ver_Todos_los_empleados)
                {
                    ddlempleado.SelectedValue = num_empleado.ToString();
                    //lnkagregartodos_Click(null, null);
                }

                ddlempleado.Enabled = ver_Todos_los_empleados;
                div_filtro_empleados.Visible = ver_Todos_los_empleados;
                if (dt_empleados.Rows.Count == 1)
                {
                    ddlempleado.SelectedIndex = 1;
                    ddlempleado_SelectedIndexChanged(null,null);
                }
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

        #endregion Funciones
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string usuario = Session["usuario"] as string;
                CargarTiposBonos(usuario.ToUpper());
                ClearFields();
                CargarEmpleados("");
                hdnMontoOriginal.Value = "";
                hdnauthorization_total_bonds.Value = "";
                hdnauthorization_total_amount.Value = "";
            }
        }

        protected void lnksearch_Click(object sender, EventArgs e)
        {
            string filter = txtfilterempleado.Text;
            try
            {
                if (filter.Length == 0 || filter.Length > 3)
                {
                    CargarEmpleados(filter);
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

        protected void ddlempleado_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int no_ = Convert.ToInt32(ddlempleado.SelectedValue);
                if (no_ > 0)
                {
                    DataTable dtempleado = informacion_empleado(no_);
                    if (dtempleado.Rows.Count > 0)
                    {
                        DataRow empleado = dtempleado.Rows[0];
                        txtCC_Cargo.Text = empleado["cc"].ToString();
                        txtCC_Emp.Text = empleado["cc"].ToString();
                        CargarInformacionBonoEmpleado(no_);
                    }
                    else {

                        Toast.Error("Error al cargar información del empleado.", this);
                    }
                }
                else {
                    Toast.Error("Seleccione una opción valida.", this);
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar información del empleado: " + ex.Message, this);
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
                    Toast.Error("El monto no puede exceder a " + string.Format("{0:C}", price),this);
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

        protected void cbInitialMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int months_toadd = 1;
                DateTime fecha_inicial = Convert.ToDateTime(cbInitialYear.SelectedValue+"-"+(Convert.ToInt32(cbInitialMonth.SelectedValue).ToString("00")) +"-01");
                DateTime fecha_inicial_B = fecha_inicial.AddMonths(months_toadd);
                DateTime fecha_final = fecha_inicial_B.AddMonths(1).AddDays(-1);
                txtPeriodDateOf.SelectedDate = fecha_inicial;
                txtPeriodDateTo.SelectedDate = fecha_final;
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

                    //if (this.trWeek.Visible)
                    //{
                    //    SelectedWeek(selectedDate, string.Empty, string.Empty);
                    //    if (Session["SelectedDate"] != null)
                    //    {
                    //        IFormatProvider culture = new System.Globalization.CultureInfo("es-MX", true);
                    //        selectedDate = DateTime.Parse(Session["SelectedDate"].ToString(), culture, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                    //    }
                    //    SetScheduledDate();
                    //}
                    //else
                    //{
                    //    LoadBondsRequests();
                    //}
                }


            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar solicitud: " + ex.Message, this);
            }
        }

        protected void lnksearchcc_Click(object sender, EventArgs e)
        {
        }

    }
}