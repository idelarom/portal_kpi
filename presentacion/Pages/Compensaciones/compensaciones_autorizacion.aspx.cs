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
    public partial class compensaciones_autorizacion : System.Web.UI.Page
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
                    cbBonds_Types.Items.Insert(0, new ListItem("Todas", "0"));
                    cbBonds_Types_SelectedIndexChanged(null,null);
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar los tipos de bonos: " + ex.ToString(), this);
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
                if (Convert.ToInt32(Session["id_profile"]) == (int)enumerators.profiles_compensations.administrador)//administrador
                {
                    id_immediate_boss = null;
                }
                BonosCOM bonos = new BonosCOM();
                int? id_tipo_bono = null;
                if (Convert.ToInt32(this.cbBonds_Types.SelectedValue) > 0) { id_tipo_bono = Convert.ToInt32(this.cbBonds_Types.SelectedValue); }
                return bonos.sp_GetRequests_Bonds(id_tipo_bono,
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
                file.date_attach = DateTime.Now;
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
                int id_request = Convert.ToInt32(hdnIdRequestBond.Value == "" ? "0" : hdnIdRequestBond.Value);
                hdnIdRequestBond.Value = id_request.ToString();
                BonosCOM bonos = new BonosCOM();
                List<files_requests_bonds> list = new List<files_requests_bonds>();
                list = bonos.get_files(id_request);
                Session[hdfguid.Value + "list_documentos"] = list;
                repeater_archivos.DataSource = list;
                repeater_archivos.DataBind();
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar archivos: " + ex.Message, this);
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
                DataSet ds = bonos.sp_Update_Request_Bond(bono);
                System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                 "AlertGO('El bono del empleado ha sido "+(bono.id_request_status == (int)enumerators.bonds_status.authorization?"autorizado":"rechazado")+" con éxito.', 'compensaciones_autorizacion.aspx');", true);
            }
            catch (Exception ex)
            {
                Toast.Error("Error al procesar solicitud: " + ex.Message, this);
            }
        }

        private string GetMonthName(int month_number)
        {
            string month_name = string.Empty;
            switch (month_number)
            {
                case 1:
                    month_name = "Enero";
                    break;
                case 2:
                    month_name = "Febrero";
                    break;
                case 3:
                    month_name = "Marzo";
                    break;
                case 4:
                    month_name = "Abril";
                    break;
                case 5:
                    month_name = "Mayo";
                    break;
                case 6:
                    month_name = "Junio";
                    break;
                case 7:
                    month_name = "Julio";
                    break;
                case 8:
                    month_name = "Agosto";
                    break;
                case 9:
                    month_name = "Septiembre";
                    break;
                case 10:
                    month_name = "Octubre";
                    break;
                case 11:
                    month_name = "Noviembre";
                    break;
                case 12:
                    month_name = "Diciembre";
                    break;
            }
            return month_name;
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

        /// <summary>
        /// Desbloquea el hilo actual
        /// </summary>
        private void UnBlockUI()
        {

            load2.Style["display"] = "none";
            ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "UnBlockUI();", true);
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string usuario = Session["usuario"] as string;

                AsyncUpload1.FileUploaded += new Telerik.Web.UI.FileUploadedEventHandler(AsyncUpload1_FileUploaded);
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
                CargarTiposBonos(usuario.ToUpper());
            }

        }
        
        protected void cbBonds_Types_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LoadBondsRequests();
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar solicitud: " + ex.Message, this);
            }
            finally
            {
                InitTables();
                 UnBlockUI();
            }
        }

        protected void btnviewrequest_Click(object sender, EventArgs e)
        {
            try
            {
                int id_request = Convert.ToInt32(hdnIdRequestBond.Value == "" ? "0" : hdnIdRequestBond.Value);
                if (id_request > 0)
                {
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
                UnBlockUI();
            }
        }

        protected void lnkdescargas_Click(object sender, EventArgs e)
        {
            try
            {
                string filename = hdfpath.Value;
                RutasArchivosCOM rutas = new RutasArchivosCOM();
                DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~/"));//path localDateTime localDate = DateTime.Now;
                string path_local = dirInfo + "files/documents/temp/";
                string server = @rutas.path(1);
                int id_request = Convert.ToInt32(hdnIdRequestBond.Value == "" ? "0" : hdnIdRequestBond.Value);
                string path = id_request > 0 ? @server + id_request.ToString() + "\\" + filename : path_local + filename;
                if (File.Exists(@path))
                {
                    //Response.ContentType = "doc/docx";
                    //Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(@path));
                    //Response.TransmitFile(@path);
                    //Response.End();
                    // Limpiamos la salida
                    Response.Clear();
                    // Con esto le decimos al browser que la salida sera descargable
                    Response.ContentType = "application/octet-stream";
                    // esta linea es opcional, en donde podemos cambiar el nombre del fichero a descargar (para que sea diferente al original)
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(@path));
                    // Escribimos el fichero a enviar
                    Response.WriteFile(@path);
                    // volcamos el stream
                    Response.Flush();
                    // Enviamos todo el encabezado ahora
                    Response.End();
                }
                else
                {
                    ModalShow("#modal_archivos");
                    Toast.Error("No es encuentra el documento especificado en la ruta: " + path, this);
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al descargar documento: " + ex.Message, this);
            }
            finally
            {
                InitTables();
                UnBlockUI();
            }
        }
        
        protected void AsyncUpload1_FileUploaded(object sender, Telerik.Web.UI.FileUploadedEventArgs e)
        {
            try
            {
                //GUARDAMOS LOS RESULTADOS DE LA ACTIVIDAD
                int r = AsyncUpload1.UploadedFiles.Count;
                int no_ = 0;

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
                        if (!Directory.Exists(@directory2))
                        {
                            Directory.CreateDirectory(@directory2);
                        }
                        files_requests_bonds file = new files_requests_bonds();
                        file.path = @directory2 + name.Trim().Replace(@"\", "/");
                        file.file_name = name.Trim();
                        file.size = Convert.ToDecimal(e.File.ContentLength);
                        file.content_type = e.File.ContentType;
                        string source = file.path;
                        string path = @directory2 + file.file_name;
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
                    else
                    {
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

        protected void lnkdeletefile_Click(object sender, EventArgs e)
        {
            try
            {
                int id_request = Convert.ToInt32(hdnIdRequestBond.Value == "" ? "0" : hdnIdRequestBond.Value);
              
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
                this.trInformationImplementations.Visible = false;
                this.txtPMTrackerNumberImplementations.Text = "";
                this.txtNumberHoursImplementations.Text = "";
                this.txtProjectNameImplementations.Text = "";
                this.txtCustomerNameImplementations.Text = "";
                this.trInformationSupport.Visible = false;
                this.txtMonthSupport.Text = "";
                this.txtWeekSupport.Text = "";
                this.txtYearSupport.Text = "";
                BonosCOM bonos = new BonosCOM();
                DataSet ds = bonos.sp_GetRequests_Bonds(null,null, id_bonds);
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

                        if (Convert.ToBoolean(dsConfigurations.Tables[0].Rows[0]["folio_pmtracker_required"]))
                        {
                            this.trInformationImplementations.Visible = true;
                            this.txtPMTrackerNumberImplementations.Text = dsConfigurations.Tables[0].Rows[0]["folio_pmtracker"].ToString();
                            this.txtNumberHoursImplementations.Text = dsConfigurations.Tables[0].Rows[0]["number_hours"].ToString();
                            DataSet dsPMTracker = bonos.sp_GetConnextProjectsForFolio(dsConfigurations.Tables[0].Rows[0]["folio_pmtracker"].ToString());
                            if ((dsPMTracker != null))
                            {
                                this.txtProjectNameImplementations.Text = dsPMTracker.Tables[0].Rows[0]["nombre_proyecto"].ToString();
                                this.txtCustomerNameImplementations.Text = dsPMTracker.Tables[0].Rows[0]["nombre_cliente"].ToString();
                            }
                        }
                        else
                        {
                            this.trInformationImplementations.Visible = false;
                        }

                        if (Convert.ToBoolean(dsConfigurations.Tables[0].Rows[0]["week_detail_required"]))
                        {
                            this.trInformationSupport.Visible = true;
                            this.txtMonthSupport.Text = GetMonthName(Convert.ToInt32(bono["month"]));
                            this.txtWeekSupport.Text = bono["week"].ToString();
                            this.txtYearSupport.Text = bono["year"].ToString();
                        }
                        else
                        {
                            this.trInformationSupport.Visible = false;
                        }
                        txtAuthorizationAmount_TextChanged(null,null);
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
                InitTables();
            }
        }

        protected void txtAuthorizationAmount_TextChanged(object sender, EventArgs e)
        {
            bool correct = true;
            int id_tipo_bono = Convert.ToInt32(hdnid_tipo_bono.Value);
            int id_request = Convert.ToInt32(hdnIdRequestBond.Value);
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
                ds = bonos.sp_GetEmployeesCompensationsForEmployeeNumberAndBondType(
                    no_, id_tipo_bono, Convert.ToDateTime(this.txtPeriodDateOf.SelectedDate), id_request);
                decimal percentage = (Convert.ToDecimal(ds.Tables[0].Rows[0]["percentage_extra"]) / Convert.ToDecimal(100));
                decimal amountValidate = (Convert.ToDecimal(ds.Tables[0].Rows[0]["amount"]) * percentage);
                decimal authorization_total_bonds = Convert.ToDecimal(ds.Tables[1].Rows[0]["total_bonos"]);
                decimal authorization_total_amount = Convert.ToDecimal(ds.Tables[1].Rows[0]["total_monto"]);
                hdnMontoOriginal.Value = amountValidate.ToString();
                hdnauthorization_total_bonds.Value = authorization_total_bonds.ToString();
                hdnauthorization_total_amount.Value = authorization_total_amount.ToString();

                txtAuthorizationAmount.Text = txtAuthorizationAmount.Text == "" ? "0.00" : txtAuthorizationAmount.Text;
                decimal monto_actual1 = Convert.ToDecimal(txtAuthorizationAmount.Text == "" ? "0.00" : txtAuthorizationAmount.Text.Replace("$", ""));
                //'codigo agregado por RCA
                decimal monto_roiginal = (string.IsNullOrEmpty(hdnMontoOriginal.Value) ? 0 : Convert.ToDecimal(hdnMontoOriginal.Value));

                decimal monto_total_autorizado = (string.IsNullOrEmpty(hdnauthorization_total_amount.Value) ? 0 : Convert.ToDecimal(hdnauthorization_total_amount.Value));
                decimal Direfencia = (monto_roiginal - monto_total_autorizado);

                if (id_tipo_bono == 1)
                {
                    
                    if (Direfencia <= 0)
                    {
                        correct = false;
                        txtAuthorizationAmount.Text = Convert.ToDecimal("0.00").ToString("C2");
                        Toast.Error("Ya se le ha otorgado el monto completo de  " + monto_roiginal.ToString("C2") + " correspondiente a este periodo.", this);

                        txtAuthorizationAmount.BorderStyle = BorderStyle.Solid;
                        txtAuthorizationAmount.BorderColor = System.Drawing.Color.Red;
                    }
                    else if (Direfencia >= 1 & Direfencia < monto_roiginal & monto_actual1 > Direfencia)
                    {
                        correct = false;
                        txtAuthorizationAmount.Text = Direfencia.ToString("C2");// Direfencia.ToString("C2");
                        Toast.Info("Solo tiene un monto disponible " + Direfencia.ToString("C2") + " de un total de " + monto_roiginal.ToString("C2"), "Mensaje del sistema.", this);

                        txtAuthorizationAmount.BorderStyle = BorderStyle.Solid;
                        txtAuthorizationAmount.BorderColor = System.Drawing.Color.Green;
                    }
                }
                if (id_tipo_bono == 1)
                {
                    decimal monto_actual = Convert.ToDecimal(txtAuthorizationAmount.Text.Replace("$", ""));

                    if (monto_actual > monto_roiginal)
                    {
                        correct = false;
                        float price = float.Parse(monto_roiginal.ToString());
                        txtAuthorizationAmount.Text = Convert.ToDecimal("0.00").ToString("C2");// string.Format("{0:C}", price);
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
                    if ((value_MOUNT <= 0))
                    {
                        txtAuthorizationAmount.Text = Convert.ToDecimal("0.00").ToString("C2");
                        correct = false;
                        Toast.Error("El monto debe ser mayor a $ 0.00", this);
                        txtAuthorizationAmount.BorderStyle = BorderStyle.Solid;
                        txtAuthorizationAmount.BorderColor = System.Drawing.Color.Red;
                    }
                }

                correct = txtAuthorizationAmount.BorderColor == System.Drawing.Color.Green;
                amount_correct.Visible = correct;
                amount_error.Visible = !correct;
            }
            amount_load.Style["display"] = "none";
            InitTables();
        }

        protected void lnkautorizar_Click(object sender, EventArgs e)
        {
            try
            {
                txtAuthorizationAmount.BorderColor = System.Drawing.Color.Green;

                amount_correct.Visible = true;
                amount_error.Visible = false;
                int id_bonds = Convert.ToInt32(hdnIdRequestBond.Value==""?"0":hdnIdRequestBond.Value);
                decimal amount = Convert.ToDecimal(txtAuthorizationAmount.Text==""?"0":txtAuthorizationAmount.Text.Replace("$","").Replace(",",""));
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
                else {
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
    }
}