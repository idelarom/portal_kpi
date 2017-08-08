using datos.Model;
using negocio.Componentes;
using negocio.Entidades;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class recordatorios : System.Web.UI.Page
    {
        [WebMethod]
        public static String GetRecordsToday(string user)
        {
            try
            {
                RecordatoriosCOM recordatorios = new RecordatoriosCOM();
                DataTable dt = recordatorios.SelectToday(user);
                return JsonConvert.SerializeObject(dt);
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject("");
            }
        }

        [WebMethod]
        public static String GetRecords(string user)
        {
            try
            {
                RecordatoriosCOM recordatorios = new RecordatoriosCOM();
                DataTable dt = recordatorios.Get(user);
                List<Event> eventos = new List<Event>(); 
                foreach (DataRow row in dt.Rows)
                {
                    string color = Convert.ToBoolean(row["appointment"]) ? "#1565c0 " : "#f56954";
                    string day_ = Convert.ToDateTime(row["fecha"]).Day.ToString();
                    string month_ = (Convert.ToDateTime(row["fecha"]).Month).ToString();
                    string year_ = Convert.ToDateTime(row["fecha"]).Year.ToString();
                    string minutes_ = (Convert.ToDateTime(row["fecha"]).Minute).ToString();
                    string minutes_finish = (Convert.ToDateTime(row["fecha"]).Minute + 30).ToString();
                    string hours = Convert.ToDateTime(row["fecha"]).Hour.ToString();
                    month_ = month_.Length == 1 ? "0" + month_ : month_;
                    day_ = day_.Length == 1 ? "0" + day_ : day_;
                    hours = hours.Length == 1 ? "0" + hours : hours;
                    minutes_ = minutes_.Length == 1 ? "0" + minutes_ : minutes_;
                    minutes_finish = minutes_finish.Length == 1 ? "0" + minutes_finish : minutes_finish;
                    eventos.Add(new Event(
                        row["titulo"].ToString(), 
                        year_ + "-" + month_ + "-" + day_ + "T" + hours + ":" + minutes_ + ":00",
                        year_ + "-" + month_ + "-" + day_ + "T" + hours + ":" + minutes_finish + ":00",
                        color,
                        color,
                        row["organizer"] == null ?"": row["organizer"].ToString(),
                        row["location"] == null ? "" : row["location"].ToString(),
                        row["descripcion"] == null ? "" : row["descripcion"].ToString(),
                        row["organizer_address"] == null ? "" : row["organizer_address"].ToString()));
                }
                string ret = JsonConvert.SerializeObject(eventos);
                return ret;
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject("");
            }
        }
        public StringBuilder InicializarCalendario()
        {
            try
            {

                string usuario = Session["usuario"] as string;
                DateTime fecha = Convert.ToDateTime(txtfecha.Text);
                RecordatoriosCOM recordatorios = new RecordatoriosCOM();
                DataTable dt = recordatorios.Get(usuario);
                string eventos = "";
                foreach (DataRow row in dt.Rows)
                {
                    string color = Convert.ToBoolean(row["appointment"]) ? "#1565c0 " : "#f56954";
                    string day_ = Convert.ToDateTime(row["fecha"]).Day.ToString();
                    string month_ = (Convert.ToDateTime(row["fecha"]).Month).ToString();
                    string year_ = Convert.ToDateTime(row["fecha"]).Year.ToString();
                    string minutes_ = (Convert.ToDateTime(row["fecha"]).Minute).ToString();
                    string minutes_finish = (Convert.ToDateTime(row["fecha"]).Minute + 30).ToString();
                    string hours = Convert.ToDateTime(row["fecha"]).Hour.ToString();
                    month_ = month_.Length == 1 ? "0" + month_ : month_;
                    day_ = day_.Length == 1 ? "0" + day_ : day_;
                    hours = hours.Length == 1 ? "0" + hours : hours;
                    minutes_ = minutes_.Length == 1 ? "0" + minutes_ : minutes_;
                    minutes_finish = minutes_finish.Length == 1 ? "0" + minutes_finish : minutes_finish;
                    eventos =eventos + "  {title: '"+ row["titulo"] .ToString()+ "'," +
                                "start: '"+year_+"-"+month_+"-"+day_+"T"+hours+":"+minutes_+":00'," +
                                "end:'" + year_ + "-" + month_ + "-" + day_ + "T" + hours + ":" + minutes_finish + ":00'," +
                                "backgroundColor:'" + color + "'," +
                                "borderColor: '"+ color + "',"+
                                "allday:false,"+
                                "id:"+row["id_recordatorio"].ToString()+
                                "},";
                }
                eventos = eventos.Substring(0, eventos.Length-1);
                string day = fecha.Day.ToString();
                string month = fecha.Month.ToString();
                string year = fecha.Year.ToString();
                month = month.Length == 1 ? "0" + month : month;
                day = day.Length == 1 ? "0" + day : day;
                StringBuilder sb = new StringBuilder();
                sb.Append("<script type='text/javascript'>");
                sb.Append(""
                            + " $(function () {" +
                                    " $('#calendar').fullCalendar({" +
                                    "     locale: 'es'," +
                                    "     dayClick: function(date, jsEvent, view) {" +
                                    "         $('#"+ hdffecha.ClientID+"').val(date.format());" +
                                    "         document.getElementById('"+btncalendar.ClientID+"').click();" +
                                    "     }," +
                                    "     eventClick: function(calEvent, jsEvent, view) {" +
                                    "         $('#" + hdffecha.ClientID + "').val(calEvent.start.format());" +
                                    "         return EditRecordatorios(calEvent.id); }," +
                                    "     header: {" +
                                    "         left: 'prev,next today'," +
                                    "         center: 'title'," +
                                    "         right: 'month,agendaWeek,agendaDay'" +
                                    "     }," +
                                    "     buttonText: {" +
                                    "         today: 'Hoy'," +
                                    "         month: 'Mes'," +
                                    "         week: 'Sem'," +
                                    "         day: 'Dia'" +
                                    "     }," +
                                    "     events: [" + eventos + "]," +
                                    "     editable: false," +
                                    "     droppable: false" +                                   
                                    " });" +
                                    " $('#calendar').fullCalendar('gotoDate', '"+year+"-"+month+"-"+day+"');"+
                                " });");
                sb.Append("</script>");
                return sb;
            }
            catch (Exception ex)
            {
                Toast.Error("Error al generar calendario: "+ex.Message,this);
                return new StringBuilder();
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string usuario = Session["usuario"] as string;
               // SincronizarCalendario();
                ListaRecordatorios(usuario, DateTime.Now.AddHours(1));
                IniciarCalendario();
            }
        }

        /// <summary>
        /// Sincroniza el calendario de outlock con el calendario del portal
        /// </summary>
        private void SincronizarCalendario()
        {
            try
            {
                string usuario = Session["usuario"] as string;
                string password = Session["contraseña"] as string;
                string mail = Session["mail"] as string;
                string mail_user = usuario + mail.Replace(mail.Split('@')[0], "");
                EWSHelper calendar = new EWSHelper();
                calendar.GetAllCalendar(mail_user, password);
            }
            catch (Exception ex)
            {
                Toast.Error("Error al sincroniizar calendario: " + ex.Message, this);
            }
        }
        private void IniciarCalendario()
        {
            ScriptManager.RegisterClientScriptBlock(this,GetType(),Guid.NewGuid().ToString(),InicializarCalendario().ToString(),false);
        }
        private void ListaRecordatorios(string usuario, DateTime fecha)
        {
            try
            {
                hdffecha.Value = fecha.ToString("yyyy-MM-dd"); 
                txtfecha.Text = fecha.ToString("yyyy-MM-dd");
                txtfecharec.SelectedDate = fecha;
                txtfechafin.SelectedDate = fecha;
                RecordatoriosCOM recordatorio = new RecordatoriosCOM();
                DataTable dt_days = recordatorio.Select(usuario.ToUpper(), fecha);
                repeat_list_rec.DataSource = dt_days;
                repeat_list_rec.DataBind();

                String fcha = Convert.ToDateTime(fecha).ToString("dddd dd MMMM, yyyy", CultureInfo.CreateSpecificCulture("es-MX")).ToLower();
                String fecha_r = fcha.ToLower();
                lblfechaselected.Text = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(fcha);

            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar los recordatorios en el calendario: " + ex.Message, this);
            }
        }       
       
        private void LimpiarControles()
        {
            rtxtcorreorganizador.Text = "";
            rtxtorganizador.Text = "";
            
            txtid_recordatorio.Text = "";
            rtxtdescripcion.Text = "";
            rtxttitulo.Text = "";
        }
        protected void btncalendar_Click(object sender, EventArgs e)
        {
            string f = hdffecha.Value;
            if (f != "")
            {
                DateTime fecha = Convert.ToDateTime(f);
                string usuario = Session["usuario"] as string;
                ListaRecordatorios(usuario, fecha);
                IniciarCalendario();
            }
        }

        protected void lnkaddapointment_Click(object sender, EventArgs e)
        {
            string f = hdffecha.Value;
            if (f != "")
            {
                div_fecha_fin.Visible = true;
                div_organizador.Visible = true;
                DateTime fecha = Convert.ToDateTime(f);
                string usuario = Session["usuario"] as string;
                ListaRecordatorios(usuario, fecha);
                IniciarCalendario();
                LimpiarControles();
                ModalShow("#myModal");
            }
        }
        protected void lnkaddrecordatorio_Click(object sender, EventArgs e)
        {
            string f = hdffecha.Value;
            if (f != "")
            {
                div_fecha_fin.Visible = false;
                div_organizador.Visible = false;
                DateTime fecha = Convert.ToDateTime(f);
                string usuario = Session["usuario"] as string;
                ListaRecordatorios(usuario, fecha);
                IniciarCalendario();
                LimpiarControles();
                ModalShow("#myModal");
            }
        }

        protected void lnkguardar_Click(object sender, EventArgs e)
        {
            string titulo = rtxttitulo.Text;
            string descripcion = rtxtdescripcion.Text;
            string orga = rtxtorganizador.Text;
            string orga_mail = rtxtcorreorganizador.Text;
            string usuario = Session["usuario"] as string;
            if (titulo == "")
            {
                Toast.Error("El campo TITULO es obligatorio.", this);
            }
            else if (txtfecharec.SelectedDate == null)
            {
                Toast.Error("El campo FECHA Y HORA es obligatorio.", this);
            }
            else
            {
                DateTime fecha = Convert.ToDateTime(txtfecharec.SelectedDate);
                DateTime fechaf = Convert.ToDateTime(txtfechafin.SelectedDate);
                if (txtid_recordatorio.Text == "")
                {
                    Guardar(titulo, descripcion, fecha, usuario);
                }
                else { Editar(Convert.ToInt32(txtid_recordatorio.Text), titulo, descripcion, fecha, usuario,orga,orga_mail,fechaf); }
            }
        }

        private void Guardar(string titulo, string descripcion, DateTime fecha, string usuario)
        {
            try
            {
                datos.Model.recordatorios entidad = new datos.Model.recordatorios();
                entidad.titulo = titulo;
                entidad.descripcion = descripcion;
                entidad.fecha = fecha;
                entidad.usuario = usuario;
                entidad.usuario_creacion = usuario;
                RecordatoriosCOM recordatorio = new RecordatoriosCOM();
                string vmensaje = recordatorio.Agregar(entidad, new List<recordatorios_usuarios_adicionales>());
                if (vmensaje == "")
                {
                    ModalClose("#myModal");
                    usuario = Session["usuario"] as string;
                    ListaRecordatorios(usuario, Convert.ToDateTime(hdffecha.Value));
                    LimpiarControles();
                    IniciarCalendario();
                    Toast.Success("Recordatorio guardado correctamente.", "Mensaje del sistema", this);
                }
                else
                {
                    Toast.Error("Error al guardar recordatorio: " + vmensaje, this);
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al guardar recordatorio: " + ex.Message, this);
            }
            finally
            {
                lnkguardar.Visible = true;
                lnkcargando.Style["display"] = "none";
            }
        }

        private void Editar(int id_recordatorio, string titulo, string descripcion, DateTime fecha, string usuario,  
            string organ, string organ_mail, DateTime fecha_end)
        {
            try
            {
                datos.Model.recordatorios entidad = new datos.Model.recordatorios();
                entidad.titulo = titulo;
                entidad.id_recordatorio = id_recordatorio;
                entidad.descripcion = descripcion;
                entidad.fecha = fecha;
                entidad.organizer = organ;
                entidad.organizer_address = organ_mail;
                entidad.usuario = usuario;
                entidad.fecha_end = fecha_end;
                entidad.usuario_creacion = usuario;
                RecordatoriosCOM recordatorio = new RecordatoriosCOM();
                string vmensaje = recordatorio.Editar(entidad, new List<recordatorios_usuarios_adicionales>());
                if (vmensaje == "")
                {
                    ModalClose("#myModal");
                    usuario = Session["usuario"] as string;
                    ListaRecordatorios(usuario, Convert.ToDateTime(hdffecha.Value));
                    LimpiarControles();
                    IniciarCalendario();
                    Toast.Success("Recordatorio editado correctamente.", "Mensaje del sistema", this);
                }
                else
                {
                    Toast.Error("Error al editado recordatorio: " + vmensaje, this);
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al editar recordatorio: " + ex.Message, this);
            }
            finally
            {
                lnkguardar.Visible = true;
                lnkcargando.Style["display"] = "none";
            }
        }

        private void Eliminar(int id_recordatorio, string motivo, string usuario)
        {
            try
            {
                datos.Model.recordatorios entidad = new datos.Model.recordatorios();
                entidad.id_recordatorio = id_recordatorio;
                entidad.comentarios_borrado = motivo;
                entidad.usuario_borrado = usuario;
                RecordatoriosCOM recordatorio = new RecordatoriosCOM();
                string vmensaje = "";
                DataTable dt_days = recordatorio.Get(usuario.ToUpper());
                DataView dv = dt_days.DefaultView;
                dv.RowFilter = "id_recordatorio = " + id_recordatorio + "";
                if (dv.ToTable().Rows.Count > 0)
                {
                    DataRow row = dv.ToTable().Rows[0];
                    bool isAppointment = Convert.ToBoolean(row["appointment"]);
                    if (isAppointment)
                    {
                        String password = Session["contraseña"] as string;
                        string username = Session["usuario"] as string;
                        string mail = Session["mail"] as string;
                        string mail_user = username + mail.Replace(mail.Split('@')[0], "");
                        string id = row["key"].ToString();
                        EWSHelper appointments = new EWSHelper();
                        vmensaje = appointments.CancelAppointment(mail_user,password,id,motivo);
                    }
                }
                if (vmensaje == "")
                {
                    vmensaje = recordatorio.Eliminar(entidad);
                }

                if (vmensaje == "")
                {
                    ModalClose("#myModal");
                    usuario = Session["usuario"] as string;
                    ListaRecordatorios(usuario, Convert.ToDateTime(hdffecha.Value));
                    LimpiarControles();
                    IniciarCalendario();
                    Toast.Success("Recordatorio eliminado correctamente.", "Mensaje del sistema", this);
                }
                else
                {
                    Toast.Error("Error al eliminar recordatorio: " + vmensaje, this);
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al eliminar recordatorio: " + ex.Message, this);
            }
        }

        protected void lnkeliminar_Click(object sender, EventArgs e)
        {
            string motivos = hdfmotivos.Value;
            LinkButton lnk = sender as LinkButton;
            string usuario = Session["usuario"] as string;
            int id = Convert.ToInt32(lnk.CommandArgument);
            Eliminar(id, motivos, usuario);
        }

        protected void btnedit_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime date_selected = Convert.ToDateTime(hdffecha.Value);
                string usuario = Session["usuario"] as string;
                txtid_recordatorio.Text = hdfid_rec.Value;
                ListaRecordatorios(usuario, date_selected);
                RecordatoriosCOM recordatorio = new RecordatoriosCOM();
                DataTable dt_days = recordatorio.Get(usuario.ToUpper());
                DataView dv = dt_days.DefaultView;
                int id_recordatorio = Convert.ToInt32(hdfid_rec.Value == "" ? "0" : hdfid_rec.Value);
                dv.RowFilter = "id_recordatorio = " + id_recordatorio + "";
                if (dv.ToTable().Rows.Count > 0)
                {
                    DataRow row = dv.ToTable().Rows[0];
                    rtxttitulo.Text = row["titulo"].ToString();
                    rtxtdescripcion.Text = row["descripcion"].ToString();
                    txtfecharec.SelectedDate = Convert.ToDateTime(row["fecha"]);
                    IniciarCalendario();
                    Boolean isAppointment = Convert.ToBoolean(row["appointment"]);
                    div_organizador.Visible = isAppointment;
                    div_fecha_fin.Visible = isAppointment;
                    if (isAppointment)
                    {
                        rtxtlugar.Text = row["location"].ToString();
                        txtfechafin.SelectedDate = Convert.ToDateTime(row["fecha_end"]);
                        rtxtorganizador.Text = row["organizer"].ToString();
                        rtxtcorreorganizador.Text = row["organizer_address"].ToString();
                    }
                    ModalShow("#myModal");                   
                }
                else
                {
                    Toast.Error("Error al cargar recordatorio: No se encuentra ningun recordatorio", this);
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar recordatorio: " + ex.Message, this);
            }
        }

        protected void lnkcommandevent_Click(object sender, EventArgs e)
        {
            try
            {
                string motivo = hdfmotivos.Value;
                LinkButton lnk = sender as LinkButton;
                string comand = lnk.CommandName.ToLower();
                string usuario = Session["usuario"] as string;
                int id_recordatorio = Convert.ToInt32(lnk.CommandArgument);
                datos.Model.recordatorios entidad = new datos.Model.recordatorios();
                entidad.id_recordatorio = id_recordatorio;
                entidad.comentarios_borrado = motivo;
                entidad.usuario_borrado = usuario;
                RecordatoriosCOM recordatorio = new RecordatoriosCOM();
                string vmensaje = "";
                DataTable dt_days = recordatorio.Get(usuario.ToUpper());
                DataView dv = dt_days.DefaultView;
                dv.RowFilter = "id_recordatorio = " + id_recordatorio + "";
                if (dv.ToTable().Rows.Count > 0)
                {
                    DataRow row = dv.ToTable().Rows[0];
                    bool isAppointment = Convert.ToBoolean(row["appointment"]);
                    if (isAppointment)
                    {
                        String password = Session["contraseña"] as string;
                        string username = Session["usuario"] as string;
                        string mail = Session["mail"] as string;
                        string mail_user = username + mail.Replace(mail.Split('@')[0], "");
                        string id = row["key"].ToString();
                        EWSHelper appointments = new EWSHelper();
                        vmensaje = comand == "aceptar" ? appointments.AcceptAppointment(mail_user, password, id): appointments.DeclineAppointment(mail_user, password, id,motivo);
                    }
                }
                if (vmensaje == "")
                {
                    ModalClose("#myModal");
                    usuario = Session["usuario"] as string;
                    ListaRecordatorios(usuario, Convert.ToDateTime(hdffecha.Value));
                    LimpiarControles();
                    IniciarCalendario();
                    Toast.Success("Recordatorio respondido correctamente.", "Mensaje del sistema", this);
                }
                else
                {
                    Toast.Error("Error al responder recordatorio: " + vmensaje, this);
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al responder recordatorio: " + ex.Message, this);
            }
        }

    }
}