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
            hdnIdRequestBond.Value = "";
            txtAuthorizationAmount.BorderColor = System.Drawing.Color.Silver;
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
            cbInitialMonth_SelectedIndexChanged(null, null);
        }

        /// <summary>
        /// Carga la informacion de un empleado
        /// </summary>
        /// <param name="no_"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Carga el listado de empleados
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

        /// <summary>
        /// Carga el combo de los años inicial
        /// </summary>
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
                this.cbInitialYear.SelectedValue = (System.DateTime.Today.Year - 1).ToString();
            }
        }

        /// <summary>
        /// Carga el combo de los meses inicial
        /// </summary>
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
                this.cbInitialYear.SelectedValue = (DateTime.Now.Year - 1).ToString();
            }
        }

        /// <summary>
        /// Carga el combo de los años final
        /// </summary>
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

        /// <summary>
        /// Carga el combo de los meses final
        /// </summary>
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

        /// <summary>
        /// Calcula la fecha inicial a partir de la fecha final, con respecto al tipo de periodo de bono
        /// </summary>
        private void SelectedMonthAndYear()
        {
            //if (this.hdnIdPeriodicity.Value == ((int)enumerators.Periodicity.quarterly).ToString())
            //{
            //    // si es trimaestral

            //    // valida  si el mes es enero o febrero
            //    //Si es principo de año (enero, febrero)
            //    if (this.cbFinalizeMonth.SelectedValue == ((int)enumerators.Months.January).ToString() |
            //        this.cbFinalizeMonth.SelectedValue == ((int)enumerators.Months.February).ToString())
            //    {
            //        //si es enero, sino febrero
            //        if (this.cbFinalizeMonth.SelectedValue == ((int)enumerators.Months.January).ToString())
            //        {
            //            // el mes sera noviembre y el año se le resta 1
            //            this.cbInitialMonth.SelectedValue = ((int)enumerators.Months.November).ToString();
            //            this.cbInitialYear.SelectedValue = (Convert.ToInt32(this.cbFinalizeYear.SelectedValue) - 1).ToString();
            //        }
            //        else
            //        {
            //            // el mes sera diciembre y el año se le resta 1
            //            this.cbInitialMonth.SelectedValue = ((int)enumerators.Months.December).ToString();
            //            this.cbInitialYear.SelectedValue = (Convert.ToInt32(this.cbFinalizeYear.SelectedValue) - 1).ToString();
            //        }
            //    }
            //    else
            //    {
            //        // si no es enero o febrero, se restan dos meses del mes final
            //        this.cbInitialMonth.SelectedValue = (Convert.ToInt32(this.cbFinalizeMonth.SelectedValue) - 2).ToString();
            //        this.cbInitialYear.SelectedValue = this.cbFinalizeYear.SelectedValue;
            //    }
            //}

            //if (this.hdnIdPeriodicity.Value == ((int)enumerators.Periodicity.semiannual).ToString())
            //{
            //    // si el bono es semestral

            //    // valida si el mes final es enero, febrero, marzo abril o mayo
            //    if (this.cbFinalizeMonth.SelectedValue == ((int)enumerators.Months.January).ToString() |
            //        this.cbFinalizeMonth.SelectedValue == ((int)enumerators.Months.February).ToString() |
            //        this.cbFinalizeMonth.SelectedValue == ((int)enumerators.Months.March).ToString() |
            //        this.cbFinalizeMonth.SelectedValue == ((int)enumerators.Months.April).ToString() |
            //        this.cbFinalizeMonth.SelectedValue == ((int)enumerators.Months.May).ToString())
            //    {
            //        //Enero
            //        if (this.cbFinalizeMonth.SelectedValue == ((int)enumerators.Months.January).ToString())
            //        {
            //            this.cbInitialMonth.SelectedValue = ((int)enumerators.Months.August).ToString();
            //            this.cbInitialYear.SelectedValue = (Convert.ToInt32(this.cbFinalizeYear.SelectedValue) - 1).ToString();
            //        }
            //        //febrero
            //        if (this.cbFinalizeMonth.SelectedValue == ((int)enumerators.Months.February).ToString())
            //        {
            //            this.cbInitialMonth.SelectedValue = ((int)enumerators.Months.September).ToString();
            //            this.cbInitialYear.SelectedValue = (Convert.ToInt32(this.cbFinalizeYear.SelectedValue) - 1).ToString();
            //        }

            //        //marzo
            //        if (this.cbFinalizeMonth.SelectedValue == ((int)enumerators.Months.March).ToString())
            //        {
            //            this.cbInitialMonth.SelectedValue = ((int)enumerators.Months.October).ToString();
            //            this.cbInitialYear.SelectedValue = (Convert.ToInt32(this.cbFinalizeYear.SelectedValue) - 1).ToString();
            //        }

            //        // abril
            //        if (this.cbFinalizeMonth.SelectedValue == ((int)enumerators.Months.April).ToString())
            //        {
            //            this.cbInitialMonth.SelectedValue = ((int)enumerators.Months.November).ToString();
            //            this.cbInitialYear.SelectedValue = (Convert.ToInt32(this.cbFinalizeYear.SelectedValue) - 1).ToString();
            //        }

            //        // mayo
            //        if (this.cbFinalizeMonth.SelectedValue == ((int)enumerators.Months.May).ToString())
            //        {
            //            this.cbInitialMonth.SelectedValue = ((int)enumerators.Months.December).ToString();
            //            this.cbInitialYear.SelectedValue = (Convert.ToInt32(this.cbFinalizeYear.SelectedValue) - 1).ToString();
            //        }
            //    }
            //    else
            //    {
            //        this.cbInitialMonth.SelectedValue = (Convert.ToInt32(this.cbFinalizeMonth.SelectedValue) - 5).ToString();
            //        // cualquier otro mes le resta 5 al mes final
            //        this.cbInitialYear.SelectedValue = this.cbFinalizeYear.SelectedValue;
            //    }
            //}

            //if (this.hdnIdPeriodicity.Value == ((int)enumerators.Periodicity.bimestral).ToString())
            //{
            //    // es bimestral
            //    //Si es principo de año (enero, febrero)
            //    if (this.cbFinalizeMonth.SelectedValue == ((int)enumerators.Months.January).ToString())
            //    {
            //        //si es enero, sino febrero
            //        if (this.cbFinalizeMonth.SelectedValue == ((int)enumerators.Months.January).ToString())
            //        {
            //            this.cbInitialMonth.SelectedValue = ((int)enumerators.Months.December).ToString();
            //            this.cbInitialYear.SelectedValue = (Convert.ToInt32(this.cbFinalizeYear.SelectedValue) - 1).ToString();
            //        }
            //    }
            //    else
            //    {
            //        this.cbInitialMonth.SelectedValue = (Convert.ToInt32(this.cbFinalizeMonth.SelectedValue) - 1).ToString();
            //        this.cbInitialYear.SelectedValue = this.cbFinalizeYear.SelectedValue;
            //    }
            //}

            //if (this.hdnIdPeriodicity.Value == ((int)enumerators.Periodicity.monthly).ToString())
            //{
            //    // es mensual
            //    this.cbInitialMonth.SelectedValue = (Convert.ToInt32(this.cbFinalizeMonth.SelectedValue)).ToString();
            //    this.cbInitialYear.SelectedValue = this.cbFinalizeYear.SelectedValue;
            //}

            //if (this.hdnIdPeriodicity.Value == ((int)enumerators.Periodicity.annual).ToString())
            //{
            //    // es anual
            //    this.cbInitialMonth.SelectedValue = this.cbFinalizeMonth.SelectedValue;
            //    this.cbInitialYear.SelectedValue = (Convert.ToInt32(this.cbFinalizeYear.SelectedValue) - 1).ToString();
            //}
            string date_initial = this.cbInitialYear.SelectedValue + "-" + this.cbInitialMonth.SelectedValue.PadLeft(2, '0') + "-01";
            string date_final = "";
            if (this.cbFinalizeYear.SelectedValue != "" && this.cbFinalizeMonth.SelectedValue != "")
            {
                date_final = this.cbFinalizeYear.SelectedValue + "-" + this.cbFinalizeMonth.SelectedValue.PadLeft(2, '0') + "-" + System.DateTime.DaysInMonth(Convert.ToInt32(this.cbFinalizeYear.SelectedValue), Convert.ToInt32(this.cbFinalizeMonth.SelectedValue)).ToString().PadLeft(2, '0');
            }
            this.txtPeriodDateOf.SelectedDate = Convert.ToDateTime(date_initial);

            if (date_final == "")
            {
                this.txtPeriodDateTo.SelectedDate = Convert.ToDateTime(date_initial).AddMonths(1).AddDays(-1);
            }
            else
            {
                this.txtPeriodDateTo.SelectedDate = Convert.ToDateTime(date_final);
            }
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
                    gridBondsRequisitions.DataSource = ds.Tables[0];
                    gridBondsRequisitions.DataBind();
                }
                else
                {
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
                if (Convert.ToInt32(Session["id_perfil"]) == 1)//administrador
                {
                    id_immediate_boss = null;
                }
                BonosCOM bonos = new BonosCOM();
                return bonos.sp_GetRequests_Bonds(Convert.ToInt32(this.cbBonds_Types.SelectedValue),
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
        /// Carga el paginado a todas las tablas
        /// </summary>
        private void InitTables()
        {
            ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "Init('#table_bonos');", true);
        }

        /// <summary>
        /// Carga la configuración de un bono por empleado
        /// </summary>
        /// <param name="no_"></param>
        private void CargarInformacionBonoEmpleado(int no_, bool event_changed_date)
        {
            try
            {
                hdnIdRequestBond.Value = "";
                Session[hdfguid.Value + "list_documentos"] = null;
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

                    hdnCC_Cargo.Value = dsEmpleado.Tables[0].Rows[0]["cc"].ToString();
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

                    this.hdnValidateAmount.Value = string.Empty;
                    if (!event_changed_date)
                    {
                        //si no es un evento de cambio de fechas omitimos recalcular las fechas para evitar un bucle
                        if (this.tblMonthSelect.Visible & ds.Tables[0].Rows[0]["id_periodicity"].ToString()
                            != ((int)enumerators.Periodicity.monthly).ToString())
                        {
                            // si el periodo del bono es diferente de mensual se calculan el inicio y fin del periodo del bono
                            this.lblTitleMonth.Text = "Mes Inicial:";
                            this.trFinalizeMonth.Visible = true;
                            //If Me.txtAuthorizationAmount.Text = "" Then
                            //calcula la fecha final
                            LoadFinalizeMonths();
                            // calcula la fecha inicial
                            LoadFinalizeYears();

                            LoadInitalYears();
                            LoadInitialMonths();
                            //End If
                            //cbInitialYear_SelectedIndexChanged(null, null);
                            //// se almacena la fecha final en sus respectivos campos de texto con en el formato correspondiente
                            //this.txtPeriodDateTo.SelectedDate = Convert.ToDateTime(System.DateTime.DaysInMonth(Convert.ToInt32(this.cbFinalizeYear.SelectedValue), Convert.ToInt32(this.cbFinalizeMonth.SelectedValue)) + "/" + this.cbFinalizeMonth.SelectedValue.PadLeft(2, '0') + "/" + this.cbFinalizeYear.SelectedValue);

                            //// calcula la fecha inicial a partil de la fecha final con respecto al tipo de bono utilizado
                            if (this.hdnIdPeriodicity.Value == ((int)enumerators.Periodicity.monthly).ToString() || this.hdnIdPeriodicity.Value == "")
                            {//si es mensual
                                cbInitialMonth.SelectedValue = (Convert.ToInt32(cbFinalizeMonth.SelectedValue)).ToString();
                            }
                            else if (this.hdnIdPeriodicity.Value == ((int)enumerators.Periodicity.bimestral).ToString())
                            {//si es bimestral
                                if ((Convert.ToInt32(cbFinalizeMonth.SelectedValue) == ((int)enumerators.Months.January)))
                                {
                                    //si es diciembre
                                    cbInitialMonth.SelectedValue = ((int)enumerators.Months.December).ToString();
                                    cbInitialYear.SelectedValue = (Convert.ToInt32(cbFinalizeYear.SelectedValue) - 1).ToString();
                                }
                                else
                                {
                                    cbInitialMonth.SelectedValue = (Convert.ToInt32(cbFinalizeMonth.SelectedValue) - 2).ToString();
                                    cbInitialYear.SelectedValue = (Convert.ToInt32(cbFinalizeYear.SelectedValue)).ToString();
                                }
                            }
                            else if (this.hdnIdPeriodicity.Value == ((int)enumerators.Periodicity.quarterly).ToString())
                            {//si es trimestral
                                if ((Convert.ToInt32(cbFinalizeMonth.SelectedValue) == ((int)enumerators.Months.February)))
                                {
                                    //si es Febrero
                                    cbInitialMonth.SelectedValue = ((int)enumerators.Months.December).ToString();
                                    cbInitialYear.SelectedValue = (Convert.ToInt32(cbFinalizeYear.SelectedValue) - 1).ToString();
                                }
                                else if ((Convert.ToInt32(cbFinalizeMonth.SelectedValue) == ((int)enumerators.Months.January)))
                                {
                                    //si es Enero
                                    cbInitialMonth.SelectedValue = ((int)enumerators.Months.November).ToString();
                                    cbInitialYear.SelectedValue = (Convert.ToInt32(cbFinalizeYear.SelectedValue) - 1).ToString();
                                }
                                else
                                {
                                    cbInitialMonth.SelectedValue = (Convert.ToInt32(cbFinalizeMonth.SelectedValue) - 2).ToString();
                                    cbInitialYear.SelectedValue = (Convert.ToInt32(cbFinalizeYear.SelectedValue)).ToString();
                                }
                            }
                            else if (this.hdnIdPeriodicity.Value == ((int)enumerators.Periodicity.quarterly).ToString())
                            {//si es semestral
                                if ((Convert.ToInt32(cbFinalizeMonth.SelectedValue) == ((int)enumerators.Months.May)))
                                {
                                    //si es Mayo
                                    cbInitialMonth.SelectedValue = ((int)enumerators.Months.December).ToString();
                                    cbInitialYear.SelectedValue = (Convert.ToInt32(cbFinalizeYear.SelectedValue) - 1).ToString();
                                }
                                else if ((Convert.ToInt32(cbFinalizeMonth.SelectedValue) == ((int)enumerators.Months.April)))
                                {
                                    //si es Abril
                                    cbInitialMonth.SelectedValue = ((int)enumerators.Months.November).ToString();
                                    cbInitialYear.SelectedValue = (Convert.ToInt32(cbFinalizeYear.SelectedValue) - 1).ToString();
                                }
                                else if ((Convert.ToInt32(cbFinalizeMonth.SelectedValue) == ((int)enumerators.Months.March)))
                                {
                                    //si es Marzo
                                    cbInitialMonth.SelectedValue = ((int)enumerators.Months.October).ToString();
                                    cbInitialYear.SelectedValue = (Convert.ToInt32(cbFinalizeYear.SelectedValue) - 1).ToString();
                                }
                                else if ((Convert.ToInt32(cbFinalizeMonth.SelectedValue) == ((int)enumerators.Months.February)))
                                {
                                    //si es Febrero
                                    cbInitialMonth.SelectedValue = ((int)enumerators.Months.September).ToString();
                                    cbInitialYear.SelectedValue = (Convert.ToInt32(cbFinalizeYear.SelectedValue) - 1).ToString();
                                }
                                else if ((Convert.ToInt32(cbFinalizeMonth.SelectedValue) == ((int)enumerators.Months.January)))
                                {
                                    //si es Enero
                                    cbInitialMonth.SelectedValue = ((int)enumerators.Months.August).ToString();
                                    cbInitialYear.SelectedValue = (Convert.ToInt32(cbFinalizeYear.SelectedValue) - 1).ToString();
                                }
                                else
                                {
                                    cbInitialMonth.SelectedValue = (Convert.ToInt32(cbFinalizeMonth.SelectedValue) - 6).ToString();
                                    cbInitialYear.SelectedValue = (Convert.ToInt32(cbFinalizeYear.SelectedValue)).ToString();
                                }
                            }
                            else if (this.hdnIdPeriodicity.Value == ((int)enumerators.Periodicity.quarterly).ToString())
                            {//si es anual
                                cbInitialYear.SelectedValue = (Convert.ToInt32(cbInitialYear.SelectedValue) - 1).ToString();
                            }
                            SelectedMonthAndYear();
                            //// se almacena la fecha inicial en sus respectivos campos de texto con en el formato correspondiente
                            //this.txtPeriodDateOf.SelectedDate = Convert.ToDateTime("01/" + this.cbInitialMonth.SelectedValue.PadLeft(2, '0') + "/" + this.cbInitialYear.SelectedValue);
                        }
                        else
                        {

                            LoadFinalizeMonths();
                            // calcula la fecha inicial
                            LoadFinalizeYears();

                            LoadInitalYears();
                            LoadInitialMonths();
                            SelectedMonthAndYear();
                            //End If
                            //cbInitialYear_SelectedIndexChanged(null, null);
                            this.lblTitleMonth.Text = "Mes:";
                            this.trFinalizeMonth.Visible = false;
                        }
                    }
                    if (this.txtAuthorizationAmount.Attributes["readonly"] != "true")
                    {
                        if (Convert.ToDecimal(ds.Tables[0].Rows[0]["amount"]) > 0)
                        {
                            decimal percentage = (Convert.ToDecimal(ds.Tables[0].Rows[0]["percentage_extra"]) / Convert.ToDecimal(100));
                            decimal amountValidate = (Convert.ToDecimal(ds.Tables[0].Rows[0]["amount"]) * percentage);
                            decimal authorization_total_bonds = Convert.ToDecimal(ds.Tables[1].Rows[0]["total_bonos"]);
                            decimal authorization_total_amount = Convert.ToDecimal(ds.Tables[1].Rows[0]["total_monto"]);
                            hdnMontoOriginal.Value = amountValidate.ToString();
                            hdnauthorization_total_bonds.Value = authorization_total_bonds.ToString();
                            hdnauthorization_total_amount.Value = authorization_total_amount.ToString();
                            txtAuthorizationAmount.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["amount"]).ToString("C2");
                            txtAuthorizationAmount_TextChanged(null,null);


                        }
                    }


                   
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar información del bono: " + ex.Message, this);
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
                    new_row["id"] = i + 1;
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
                Toast.Error(ex.Message, this);
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
                total = Math.Round(total, 0);
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
        public void AgregarDocumento(string path, string file_name, string content_type, decimal size)
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
                file.path = path.Replace(@"\", "/");
                file.file_name = file_name;
                file.size = size;
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

        /// <summary>
        /// Carga los archivos en el grid de documentos
        /// </summary>
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
                InitTables();
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
            finally
            {
                InitTables();
                load.Style["display"] = "none";
            }
        }

        protected void txtAuthorizationAmount_TextChanged(object sender, EventArgs e)
        {
            bool correct = true;
            txtAuthorizationAmount.BorderColor = System.Drawing.Color.Green;
            int no_ = Convert.ToInt32(hdnEmployeeNumber.Value == "" ? "0" : hdnEmployeeNumber.Value);
            if (no_ > 0)
            {
                DataSet ds = new DataSet();
                DataSet dsEmpleado = new DataSet();
                hdnMontoOriginal.Value = "";
                BonosCOM bonos = new BonosCOM();
                EmpleadosCOM empleados = new EmpleadosCOM();
                //Obtiene bonos del empleado por tipo de bono
                ds = bonos.sp_GetEmployeesCompensationsForEmployeeNumberAndBondType(no_, Convert.ToInt32(this.cbBonds_Types.SelectedValue), Convert.ToDateTime(this.txtPeriodDateOf.SelectedDate));
                decimal percentage = (Convert.ToDecimal(ds.Tables[0].Rows[0]["percentage_extra"]) / Convert.ToDecimal(100));
                decimal amountValidate = (Convert.ToDecimal(ds.Tables[0].Rows[0]["amount"]) * percentage);
                decimal authorization_total_bonds = Convert.ToDecimal(ds.Tables[1].Rows[0]["total_bonos"]);
                decimal authorization_total_amount = Convert.ToDecimal(ds.Tables[1].Rows[0]["total_monto"]);
                hdnMontoOriginal.Value = amountValidate.ToString();
                hdnauthorization_total_bonds.Value = authorization_total_bonds.ToString();
                hdnauthorization_total_amount.Value = authorization_total_amount.ToString();

                txtAuthorizationAmount.Text = txtAuthorizationAmount.Text == "" ? "0.00" : txtAuthorizationAmount.Text;
                decimal monto_actual1 = Convert.ToDecimal(txtAuthorizationAmount.Text == "" ? "0.00" : txtAuthorizationAmount.Text.Replace("$", ""));
                if (Convert.ToInt32(this.cbBonds_Types.SelectedValue) == 1)
                {
                    //'codigo agregado por RCA
                    decimal monto_roiginal = (string.IsNullOrEmpty(hdnMontoOriginal.Value) ? 0 : Convert.ToDecimal(hdnMontoOriginal.Value));

                    decimal monto_total_autorizado = (string.IsNullOrEmpty(hdnauthorization_total_amount.Value) ? 0 : Convert.ToDecimal(hdnauthorization_total_amount.Value));
                    decimal Direfencia = (monto_roiginal - monto_total_autorizado);

                    if (Direfencia <= 0)
                    {
                        correct = false;
                        txtAuthorizationAmount.Text = Convert.ToDecimal("0.00").ToString("C2");
                        Toast.Error("Ya se le ha otorgado el monto completo de  " + monto_roiginal.ToString("C2") + " correspondiente a este periodo.", this);
                        txtAuthorizationAmount.Focus();
                        txtAuthorizationAmount.BorderStyle = BorderStyle.Solid;
                        txtAuthorizationAmount.BorderColor = System.Drawing.Color.Red;
                    }
                    else if (Direfencia >= 1 & Direfencia < monto_roiginal & monto_actual1 > Direfencia)
                    {
                        correct = false;
                        txtAuthorizationAmount.Text = Direfencia.ToString("C2");
                        Toast.Info("Solo tiene un monto disponible " + Direfencia.ToString("C2") + " de un total de " + monto_roiginal.ToString("C2"),"Mensaje del sistema.", this);
                        txtAuthorizationAmount.Focus();
                        txtAuthorizationAmount.BorderStyle = BorderStyle.Solid;
                        txtAuthorizationAmount.BorderColor = System.Drawing.Color.Green;
                    }
                }
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
                        txtAuthorizationAmount.BorderStyle = BorderStyle.Solid;
                        txtAuthorizationAmount.BorderColor = System.Drawing.Color.Red;
                    }
                }

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
                        txtAuthorizationAmount.BorderStyle = BorderStyle.Solid;
                        txtAuthorizationAmount.BorderColor = System.Drawing.Color.Red;
                    }
                }
            }
            else
            {
                txtAuthorizationAmount.Text = "";
                Toast.Info("Seleccione un empleado", "Mensaje del sistema", this);
            }

            InitTables();
        }

        protected void cbInitialMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.hdnIdPeriodicity.Value == ((int)enumerators.Periodicity.monthly).ToString() || this.hdnIdPeriodicity.Value == "")
                {//si es mensual
                    cbFinalizeMonth.SelectedValue = (Convert.ToInt32(cbInitialMonth.SelectedValue)).ToString();
                    cbFinalizeYear.SelectedValue = (Convert.ToInt32(cbInitialYear.SelectedValue)).ToString();
                }
                else if (this.hdnIdPeriodicity.Value == ((int)enumerators.Periodicity.bimestral).ToString())
                {//si es bimestral
                    if ((Convert.ToInt32(cbInitialMonth.SelectedValue) == ((int)enumerators.Months.December)))
                    {
                        //si es diciembre
                        cbFinalizeMonth.SelectedValue = ((int)enumerators.Months.January).ToString();
                        cbFinalizeYear.SelectedValue = (Convert.ToInt32(cbInitialYear.SelectedValue) + 1).ToString();
                    }
                    else
                    {
                        cbFinalizeMonth.SelectedValue = (Convert.ToInt32(cbInitialMonth.SelectedValue) + 2).ToString();
                        cbFinalizeYear.SelectedValue = (Convert.ToInt32(cbInitialYear.SelectedValue)).ToString();
                    }
                }
                else if (this.hdnIdPeriodicity.Value == ((int)enumerators.Periodicity.quarterly).ToString())
                {//si es trimestral
                    if ((Convert.ToInt32(cbInitialMonth.SelectedValue) == ((int)enumerators.Months.December)))
                    {
                        //si es diciembre
                        cbFinalizeMonth.SelectedValue = ((int)enumerators.Months.February).ToString();
                        cbFinalizeYear.SelectedValue = (Convert.ToInt32(cbInitialYear.SelectedValue) + 1).ToString();
                    }
                    else if ((Convert.ToInt32(cbInitialMonth.SelectedValue) == ((int)enumerators.Months.November)))
                    {
                        //si es noviembre
                        cbFinalizeMonth.SelectedValue = ((int)enumerators.Months.January).ToString();
                        cbFinalizeYear.SelectedValue = (Convert.ToInt32(cbInitialYear.SelectedValue) + 1).ToString();
                    }
                    else
                    {
                        cbFinalizeMonth.SelectedValue = (Convert.ToInt32(cbInitialMonth.SelectedValue) + 2).ToString();
                        cbFinalizeYear.SelectedValue = (Convert.ToInt32(cbInitialYear.SelectedValue)).ToString();
                    }
                }
                else if (this.hdnIdPeriodicity.Value == ((int)enumerators.Periodicity.quarterly).ToString())
                {//si es semestral
                    if ((Convert.ToInt32(cbInitialMonth.SelectedValue) == ((int)enumerators.Months.December)))
                    {
                        //si es diciembre
                        cbFinalizeMonth.SelectedValue = ((int)enumerators.Months.May).ToString();
                        cbFinalizeYear.SelectedValue = (Convert.ToInt32(cbInitialYear.SelectedValue) + 1).ToString();
                    }
                    else if ((Convert.ToInt32(cbInitialMonth.SelectedValue) == ((int)enumerators.Months.November)))
                    {
                        //si es noviembre
                        cbFinalizeMonth.SelectedValue = ((int)enumerators.Months.April).ToString();
                        cbFinalizeYear.SelectedValue = (Convert.ToInt32(cbInitialYear.SelectedValue) + 1).ToString();
                    }
                    else if ((Convert.ToInt32(cbInitialMonth.SelectedValue) == ((int)enumerators.Months.October)))
                    {
                        //si es octubre
                        cbFinalizeMonth.SelectedValue = ((int)enumerators.Months.March).ToString();
                        cbFinalizeYear.SelectedValue = (Convert.ToInt32(cbInitialYear.SelectedValue) + 1).ToString();
                    }
                    else if ((Convert.ToInt32(cbInitialMonth.SelectedValue) == ((int)enumerators.Months.September)))
                    {
                        //si es septiembre
                        cbFinalizeMonth.SelectedValue = ((int)enumerators.Months.February).ToString();
                        cbFinalizeYear.SelectedValue = (Convert.ToInt32(cbInitialYear.SelectedValue) + 1).ToString();
                    }
                    else if ((Convert.ToInt32(cbInitialMonth.SelectedValue) == ((int)enumerators.Months.August)))
                    {
                        //si es agosto
                        cbFinalizeMonth.SelectedValue = ((int)enumerators.Months.January).ToString();
                        cbFinalizeYear.SelectedValue = (Convert.ToInt32(cbInitialYear.SelectedValue) + 1).ToString();
                    }
                    else
                    {
                        cbFinalizeMonth.SelectedValue = (Convert.ToInt32(cbInitialMonth.SelectedValue) + 6).ToString();
                        cbFinalizeYear.SelectedValue = (Convert.ToInt32(cbInitialYear.SelectedValue)).ToString();
                    }
                }
                else if (this.hdnIdPeriodicity.Value == ((int)enumerators.Periodicity.quarterly).ToString())
                {//si es anual
                    cbFinalizeYear.SelectedValue = (Convert.ToInt32(cbFinalizeYear.SelectedValue) + 1).ToString();
                }
                SelectedMonthAndYear();
                int no_ = Convert.ToInt32(hdnEmployeeNumber.Value == "" ? "0" : hdnEmployeeNumber.Value);
                if (no_ > 0)
                {
                    CargarInformacionBonoEmpleado(no_, true);
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al calcular fechas: " + ex.Message, this);
            }
            finally {

                InitTables();
            }
        }

        protected void cbInitialYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.hdnIdPeriodicity.Value == ((int)enumerators.Periodicity.monthly).ToString() || this.hdnIdPeriodicity.Value == "")
                {//si es mensual
                    cbInitialMonth.SelectedValue = (Convert.ToInt32(cbFinalizeMonth.SelectedValue)).ToString();
                }
                else if (this.hdnIdPeriodicity.Value == ((int)enumerators.Periodicity.bimestral).ToString())
                {//si es bimestral
                    if ((Convert.ToInt32(cbFinalizeMonth.SelectedValue) == ((int)enumerators.Months.January)))
                    {
                        //si es diciembre
                        cbInitialMonth.SelectedValue = ((int)enumerators.Months.December).ToString();
                        cbInitialYear.SelectedValue = (Convert.ToInt32(cbFinalizeYear.SelectedValue) - 1).ToString();
                    }
                    else
                    {
                        cbInitialMonth.SelectedValue = (Convert.ToInt32(cbFinalizeMonth.SelectedValue) - 2).ToString();
                        cbInitialYear.SelectedValue = (Convert.ToInt32(cbFinalizeYear.SelectedValue)).ToString();
                    }
                }
                else if (this.hdnIdPeriodicity.Value == ((int)enumerators.Periodicity.quarterly).ToString())
                {//si es trimestral
                    if ((Convert.ToInt32(cbFinalizeMonth.SelectedValue) == ((int)enumerators.Months.February)))
                    {
                        //si es Febrero
                        cbInitialMonth.SelectedValue = ((int)enumerators.Months.December).ToString();
                        cbInitialYear.SelectedValue = (Convert.ToInt32(cbFinalizeYear.SelectedValue) - 1).ToString();
                    }
                    else if ((Convert.ToInt32(cbFinalizeMonth.SelectedValue) == ((int)enumerators.Months.January)))
                    {
                        //si es Enero
                        cbInitialMonth.SelectedValue = ((int)enumerators.Months.November).ToString();
                        cbInitialYear.SelectedValue = (Convert.ToInt32(cbFinalizeYear.SelectedValue) - 1).ToString();
                    }
                    else
                    {
                        cbInitialMonth.SelectedValue = (Convert.ToInt32(cbFinalizeMonth.SelectedValue) - 2).ToString();
                        cbInitialYear.SelectedValue = (Convert.ToInt32(cbFinalizeYear.SelectedValue)).ToString();
                    }
                }
                else if (this.hdnIdPeriodicity.Value == ((int)enumerators.Periodicity.quarterly).ToString())
                {//si es semestral
                    if ((Convert.ToInt32(cbFinalizeMonth.SelectedValue) == ((int)enumerators.Months.May)))
                    {
                        //si es Mayo
                        cbInitialMonth.SelectedValue = ((int)enumerators.Months.December).ToString();
                        cbInitialYear.SelectedValue = (Convert.ToInt32(cbFinalizeYear.SelectedValue) - 1).ToString();
                    }
                    else if ((Convert.ToInt32(cbFinalizeMonth.SelectedValue) == ((int)enumerators.Months.April)))
                    {
                        //si es Abril
                        cbInitialMonth.SelectedValue = ((int)enumerators.Months.November).ToString();
                        cbInitialYear.SelectedValue = (Convert.ToInt32(cbFinalizeYear.SelectedValue) - 1).ToString();
                    }
                    else if ((Convert.ToInt32(cbFinalizeMonth.SelectedValue) == ((int)enumerators.Months.March)))
                    {
                        //si es Marzo
                        cbInitialMonth.SelectedValue = ((int)enumerators.Months.October).ToString();
                        cbInitialYear.SelectedValue = (Convert.ToInt32(cbFinalizeYear.SelectedValue) - 1).ToString();
                    }
                    else if ((Convert.ToInt32(cbFinalizeMonth.SelectedValue) == ((int)enumerators.Months.February)))
                    {
                        //si es Febrero
                        cbInitialMonth.SelectedValue = ((int)enumerators.Months.September).ToString();
                        cbInitialYear.SelectedValue = (Convert.ToInt32(cbFinalizeYear.SelectedValue) - 1).ToString();
                    }
                    else if ((Convert.ToInt32(cbFinalizeMonth.SelectedValue) == ((int)enumerators.Months.January)))
                    {
                        //si es Enero
                        cbInitialMonth.SelectedValue = ((int)enumerators.Months.August).ToString();
                        cbInitialYear.SelectedValue = (Convert.ToInt32(cbFinalizeYear.SelectedValue) - 1).ToString();
                    }
                    else
                    {
                        cbInitialMonth.SelectedValue = (Convert.ToInt32(cbFinalizeMonth.SelectedValue) - 6).ToString();
                        cbInitialYear.SelectedValue = (Convert.ToInt32(cbFinalizeYear.SelectedValue)).ToString();
                    }
                }
                else if (this.hdnIdPeriodicity.Value == ((int)enumerators.Periodicity.quarterly).ToString())
                {//si es anual
                    cbInitialYear.SelectedValue = (Convert.ToInt32(cbInitialYear.SelectedValue) - 1).ToString();
                }
                SelectedMonthAndYear();
                int no_ = Convert.ToInt32(hdnEmployeeNumber.Value == "" ? "0" : hdnEmployeeNumber.Value);
                if (no_ > 0)
                {
                    CargarInformacionBonoEmpleado(no_, true);
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al calcular fechas: " + ex.Message, this);
            }
            finally {

                InitTables();
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
                        LoadFinalizeMonths();
                        LoadFinalizeYears();
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
                        calDateSupport_SelectedDateChanged(null, null);
                    }
                    else
                    {
                        LoadBondsRequests();
                    }
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar solicitud: " + ex.Message, this);
            }
            finally
            {
                InitTables();
                load2.Style["display"] = "none";
            }
        }

        protected void lnksearchcc_Click(object sender, EventArgs e)
        {
            try
            {
                string filter = "";
                BonosCOM bonos = new BonosCOM();
                DataSet ds = bonos.sp_Get_Tbl_Estructura_CC("", "");
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
                InitTables();
                load.Style["display"] = "none";
            }
        }

        protected void calDateSupport_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            ClearFields();
            DateTime selectedDate = Convert.ToDateTime(this.calDateSupport.SelectedDate);
            SelectedWeek(selectedDate, string.Empty, string.Empty);
            CalcularMontoTotalAutorizadoSoporte();
            InitTables();
        }

        protected void cbx_checkday_CheckedChanged(object sender, EventArgs e)
        {
            CalcularMontoTotalAutorizadoSoporte();
            InitTables();
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
                        hdnEmployeeNumber.Value = no_.ToString();
                        LoadFinalizeMonths();
                        // calcula la fecha inicial
                        LoadFinalizeYears();

                        LoadInitalYears();
                        LoadInitialMonths();
                        CargarInformacionBonoEmpleado(no_,false);
                        txtCC_Cargo.Text = empleado["cc_name"].ToString();
                        txtCC_Emp.Text = empleado["cc_name"].ToString();

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
            finally
            {
                InitTables();
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
                else
                {
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
                InitTables();
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
                InitTables();
            }
        }

        protected void AsyncUpload1_FileUploaded(object sender, Telerik.Web.UI.FileUploadedEventArgs e)
        {
            try
            {
                //GUARDAMOS LOS RESULTADOS DE LA ACTIVIDAD
                int r = AsyncUpload1.UploadedFiles.Count;
                int no_ = Convert.ToInt32(hdnEmployeeNumber.Value == "" ? "0" : hdnEmployeeNumber.Value);

                int id_request = Convert.ToInt32(hdnIdRequestBond.Value == "" ? "0" : hdnIdRequestBond.Value);
                if (no_ == 0 && id_request == 0)
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
                   
                    if (id_request > 0)
                    {
                        //si hay un id de bono, quiere decir que es una actualizacion y agregamos en la base de datos
                        int id_request_bonds = id_request;
                        RutasArchivosCOM rutas = new RutasArchivosCOM();
                        string path_local2 = @rutas.path(1);
                        string directory2 = path_local2 + id_request_bonds.ToString() + @"\";
                        if (!Directory.Exists(directory2))
                        {
                            Directory.CreateDirectory(directory2);
                        }
                        files_requests_bonds file = new files_requests_bonds();
                        file.path = directory2 + name.Trim().Replace(@"\", "/");
                        file.file_name = name.Trim();
                        file.size = Convert.ToDecimal(e.File.ContentLength);
                        file.content_type = e.File.ContentType;
                        string source = file.path;
                        string path = directory2 + file.file_name;
                        file.login = Session["usuario"] as string;
                        file.path = "UploadedFiles/" + id_request_bonds.ToString() + "/";
                        file.id_request_bond = id_request_bonds;
                        BonosCOM bonos = new BonosCOM();
                        int id_file_request = bonos.AgregarArchivo(file);
                        if (id_file_request > 0)
                        {
                            e.File.SaveAs(path);
                            AgregarDocumento(path, file.file_name, file.content_type, Convert.ToDecimal(file.size));
                            CargarTablaArchivos();
                            Toast.Success("Documento guardado correctamente.", "Mensaje del sistema", this);
                        }
                        else
                        {
                            Toast.Error("Error al guardar archivo.", this);

                        }
                    }
                    else {
                        e.File.SaveAs(directory + name.Trim());
                        AgregarDocumento(directory + name.Trim(), name.Trim(), e.File.ContentType, Convert.ToDecimal(e.File.ContentLength));
                        CargarTablaArchivos();
                        Toast.Success("Documento guardado correctamente.", "Mensaje del sistema", this);
                    }
                   
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al guardar documento: " + ex.Message, this);
            }
            finally
            {
                InitTables();
            }
        }

        protected void lnkdeletefile_Click(object sender, EventArgs e)
        {
            try
            {
                int id_request = Convert.ToInt32(hdnIdRequestBond.Value == "" ? "0" : hdnIdRequestBond.Value);
                if (id_request == 0)
                {
                    LinkButton lnk = sender as LinkButton;
                    string file = lnk.Attributes["file_name"].ToString();
                    EliminarDocumento(file);
                    CargarTablaArchivos();
                    Toast.Success("Documento eliminado correctamente.", "Mensaje del sistema", this);
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al eliminar documento: " + ex.Message, this);
            }
            finally
            {
                InitTables();
                ModalShow("#modal_archivos");
            }
        }

        protected void lnkcc_Click(object sender, EventArgs e)
        {
            LinkButton lnk = sender as LinkButton;
            string desc_cc = hdndesc_cc.Value;
            string cc = hdnCC_Cargo.Value;
            txtCC_Cargo.Text = desc_cc;
            hdnCC_Cargo.Value = cc;
            load.Style["display"] = "none";
            InitTables();
        }

        protected void lnkproyecto_Click(object sender, EventArgs e)
        {
            LinkButton lnk = sender as LinkButton;
            txtPMTrackerNumberImplementations.Text = hdnfolio.Value;
            txtProjectNameImplementations.Text = hdnproyecto.Value;
            txtCustomerNameImplementations.Text = hdncliente.Value;
            load.Style["display"] = "none";
            InitTables();
        }

        protected void lnksolicitar_Click(object sender, EventArgs e)
        {
            try
            {
                string vmensaje = "";
                List<files_requests_bonds> list = Session[hdfguid.Value + "list_documentos"] == null ? new List<files_requests_bonds>() :
                                                    Session[hdfguid.Value + "list_documentos"] as List<files_requests_bonds>;
                string Texto = "NO-" + hdnEmployeeNumber.Value;

                int num_empleado = Convert.ToInt32(hdnEmployeeNumber.Value == "" ? "0" : hdnEmployeeNumber.Value);
                decimal monto_actual = txtAuthorizationAmount.Text == "" ? 0 : Convert.ToDecimal(txtAuthorizationAmount.Text.Replace("$", "").Replace(",", ""));
                DateTime? period_date_of = txtPeriodDateOf.SelectedDate;
                DateTime? period_date_to = txtPeriodDateTo.SelectedDate;
                string comentarios = txtComments.Text;
                string cc = hdnCC_Cargo.Value;
                string cc_cargo = hdnCC_Cargo.Value;

                bool number_hours_required = this.trNumberHours.Visible;
                bool folio_pmtracker_required = this.trFolioPMTracker.Visible;
                bool soporte = trWeek.Visible;

                int dia_soporte = calDateSupport.SelectedDate == null ? 0 : Convert.ToDateTime(calDateSupport.SelectedDate).Day;
                int mes_soporte = calDateSupport.SelectedDate == null ? 0 : Convert.ToDateTime(calDateSupport.SelectedDate).Month;
                int año_soporte = calDateSupport.SelectedDate == null ? 0 : Convert.ToDateTime(calDateSupport.SelectedDate).Year;

                string folio_pm = txtPMTrackerNumberImplementations.Text.Trim();
                decimal hours_pm = Convert.ToDecimal(txtNumberHoursImplementations.Text == "" ? "0" : txtNumberHoursImplementations.Text);
                //VALIDACINES
                if (num_empleado == 0)
                {
                    lnksearch_Click(null, null);
                    vmensaje = "Seleccione un empleado.";
                }
                else if (monto_actual == 0)
                {
                    vmensaje = "Ingrese un monto mayor a $ 0.00.";
                }
                else if (soporte && (dia_soporte == 0 || mes_soporte == 0 || año_soporte == 0))
                {
                    vmensaje = "Seleccione una semana para la fecha de soporte.";
                }
                else if (folio_pmtracker_required && folio_pm == "")
                {
                    lnkproyecto_Click(null, null);
                    vmensaje = "Seleccione un proyecto PM Tracker.";
                }
                else if (number_hours_required && hours_pm == 0)
                {
                    vmensaje = "Ingrese la cantidad de horas de implementación.";
                }
                else if (period_date_of == null || period_date_to == null)
                {
                    vmensaje = "Hay un problema con las fechas.";
                }
                else if (cc_cargo == "")
                {
                    vmensaje = "Seleccione un Centro de Costos valido.";
                }
                else if (comentarios == "")
                {
                    vmensaje = "Los comentarios son requeridos en la solicitud.";
                }
                else if (((hdnFilesRequeried.Value.Trim().ToUpper() == "TRUE") & (hdnSubio.Value == Texto.ToString())) && list.Count == 0)
                {
                    lnkadjuntarfiles_Click(null, null);
                    //Si no se ha cargado archivo y es requerido
                    vmensaje = "Para el Bono de " + this.cbBonds_Types.SelectedItem.Text + " es necesario adjuntar un archivo.";
                }

                //Se valida que el empleado tenga un monto por periodo valido
                if (vmensaje == "" && Convert.ToInt32(this.cbBonds_Types.SelectedValue) == 1)
                {
                    //'codigo agregado por RCA
                    decimal monto_roiginal = (string.IsNullOrEmpty(hdnMontoOriginal.Value) ? 0 : Convert.ToDecimal(hdnMontoOriginal.Value));

                    decimal monto_total_autorizado = (string.IsNullOrEmpty(hdnauthorization_total_amount.Value) ? 0 : Convert.ToDecimal(hdnauthorization_total_amount.Value));
                    decimal Direfencia = (monto_roiginal - monto_total_autorizado);

                    if (Direfencia <= 0)
                    {
                        vmensaje = "Ya se le ha otorgado el monto completo de  " + monto_roiginal.ToString("C2") + " correspondiente a este periodo.";
                    }
                    else if (Direfencia >= 1 & Direfencia < monto_roiginal & monto_actual > Direfencia)
                    {
                        vmensaje = "Solo tiene un monto disponible " + Direfencia.ToString("C2") + " de un total de " + monto_roiginal.ToString("C2");
                    }
                }
                if (vmensaje == "")
                {
                    requests_bonds request_bond = new requests_bonds();
                    request_bond.employee_number = num_empleado;
                    request_bond.period_date_of = period_date_of;
                    request_bond.period_date_to = period_date_to;
                    request_bond.authorization_amount = monto_actual;
                    request_bond.id_request_status = (int)enumerators.bonds_status.request;
                    request_bond.id_bond_type = Convert.ToInt32(cbBonds_Types.SelectedValue);
                    request_bond.requisition_comments = this.txtComments.Text.Trim();
                    request_bond.created_by = Session["usuario"] as string;
                    request_bond.CC_Empleado = Convert.ToInt32(cc);
                    request_bond.CC_Cargo = Convert.ToInt32(cc_cargo);
                    if (this.trWeek.Visible)
                    {
                        request_bond.week = dia_soporte;
                        request_bond.month = mes_soporte;
                        request_bond.year = año_soporte;
                        request_bond.selected_date = period_date_to;
                    }

                    if (folio_pmtracker_required)
                    {
                        request_bond.folio_pmtracker = folio_pm;
                    }
                    if (number_hours_required)
                    {
                        request_bond.number_hours = hours_pm;
                    }
                    BonosCOM bonos = new BonosCOM();
                    DataSet ds = bonos.Agregar(request_bond);
                    if (ds.Tables.Count == 0)
                    {
                        vmensaje = "Ocurrio un error al solicitar bono.";
                    }
                    else if (ds.Tables[0].Rows.Count == 0)
                    {
                        vmensaje = "Ocurrio un error al solicitar bono.";
                    }
                    else
                    {
                        int id_request_bonds = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
                        RutasArchivosCOM rutas = new RutasArchivosCOM();
                        string path_local = @rutas.path(1);
                        string directory = path_local + id_request_bonds.ToString() + @"\";
                        if (!Directory.Exists(directory))
                        {
                            Directory.CreateDirectory(directory);
                        }
                        foreach (files_requests_bonds file in list)
                        {
                            string source = file.path;
                            string path = directory + file.file_name;
                            file.login = Session["usuario"] as string;
                            file.path = "UploadedFiles/" + id_request_bonds.ToString() + "/";
                            file.id_request_bond = id_request_bonds;
                            int id_file_request = bonos.AgregarArchivo(file);
                            if (id_file_request > 0)
                            {
                                File.Copy(source, path, true);
                            }
                        }
                    }
                }

                //Si hay algun mensaje de error
                if (vmensaje != "")
                {
                    Toast.Error(vmensaje, this);
                }
                else
                {
                    System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                  "AlertGO('El bono del empleado ha sido solicitado correctamente.', 'compensaciones_solicitud.aspx');", true);
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al guardar solicitud: " + ex.Message, this);
            }
            finally
            {
                InitTables();
            }
        }

        protected void lnkdescargas_Click(object sender, EventArgs e)
        {
            try
            {
                string filename = hdfpath.Value;
                RutasArchivosCOM rutas = new RutasArchivosCOM();
                DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~/"));//path localDateTime localDate = DateTime.Now;
                string path_local = dirInfo+"files/documents/temp/";
                string server = @rutas.path(1);
                int id_request = Convert.ToInt32(hdnIdRequestBond.Value == "" ? "0" : hdnIdRequestBond.Value);
                string path = id_request > 0 ? server + id_request.ToString()+"/"+ filename : path_local + filename;
                if (File.Exists(path))
                {
                    Response.ContentType = "doc/docx";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(path));
                    Response.TransmitFile(path);
                    Response.End();
                }
                else
                {
                    ModalShow("#modal_archivos");
                    Toast.Error("No es encuentra el documento especificado", this);
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al descargar documento: " + ex.Message, this);
            }
            finally
            {
                InitTables();
                load.Style["display"] = "none";
            }
        }

        protected void btnviewrequest_Click(object sender, EventArgs e)
        {
            try
            {
                int id_request = Convert.ToInt32(hdnIdRequestBond.Value == "" ? "0" : hdnIdRequestBond.Value);
                if (id_request > 0)
                {
                    ClearFields();
                    hdnIdRequestBond.Value = id_request.ToString();
                    BonosCOM bonos = new BonosCOM();
                    List<files_requests_bonds> list = new List<files_requests_bonds>();
                    list = bonos.get_files(id_request);
                    Session[hdfguid.Value + "list_documentos"] = list;
                    CargarTablaArchivos();
                    ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "Init('#table_bonos');", true);
                    ModalShow("#modal_archivos");
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar información del bono: " + ex.Message.ToString(), this);
            }
            finally
            {
                InitTables();
                load3.Style["display"] = "none";
            }
        }
    }
}