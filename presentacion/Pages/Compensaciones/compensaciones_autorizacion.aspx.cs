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
                load2.Style["display"] = "none";
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
                load2.Style["display"] = "none";
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
                load2.Style["display"] = "none";
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
    }
}