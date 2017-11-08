using negocio.Componentes;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Globalization;
using System.Text;
using System.Web.UI;

namespace presentacion.Pages.Common
{
    public partial class inicio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string usuario = Session["usuario"] as string;
                hdf_usuario.Value =  usuario.ToUpper().ToString();
                hdf_numempleado.Value = Convert.ToInt32(Session["num_empleado"]).ToString();
                hdf_ver_Todos_empleados.Value = Convert.ToBoolean(Session["ver_Todos_los_empleados"]).ToString();
                CargarOrdenDivs();
                IniciarCalendario();
            }
        }

        [System.Web.Services.WebMethod]
        public static String getDivs(string usuario)
        {
            try
            {
                UsuariosCOM usuarios = new UsuariosCOM();
                DataTable dt = usuarios.sp_usuario_widgets(usuario).Tables[0];
                string value = JsonConvert.SerializeObject(dt);
                return value;
            }
            catch (Exception)
            {
                return "";
            }
        }

        private void CargarOrdenDivs()
        {
            try
            {
                EmpleadosCOM componente = new EmpleadosCOM();
                DataSet ds = componente.sp_order_widgets(Convert.ToString(Session["usuario"]));
                DataTable dt = ds.Tables[0];
                string value = dt.Rows[0]["order"].ToString().Trim();
                if (value != "")
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("<script type='text/javascript'>");
                    sb.Append("$(document).ready(function () {");
                    sb.Append(value);
                    sb.Append("});");
                    sb.Append("</script>");
                    ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), sb.ToString(), false);
                }
            }
            catch (Exception ex)
            {
                Toast.Error(ex.Message, this);
            }
        }

        public StringBuilder InicializarCalendario()
        {
            try
            {
                string usuario = Session["usuario"] as string;
                DateTime fecha = Convert.ToDateTime(DateTime.Now);
                RecordatoriosCOM recordatorios = new RecordatoriosCOM();
                DataTable dt = recordatorios.Get(usuario);
                string eventos = "";
                foreach (DataRow row in dt.Rows)
                {
                    row["fecha_end"] = row["fecha_end"] == DBNull.Value ? Convert.ToDateTime(row["fecha"]).AddMinutes(30) : row["fecha_end"];
                    string color = Convert.ToBoolean(row["appointment"]) ? "#1565c0 " : "#f56954";
                    string day_ = Convert.ToDateTime(row["fecha"]).Day.ToString();
                    string month_ = (Convert.ToDateTime(row["fecha"]).Month).ToString();
                    string year_ = Convert.ToDateTime(row["fecha"]).Year.ToString();
                    string minutes_ = (Convert.ToDateTime(row["fecha"]).Minute).ToString();
                    string minutes_finish = (Convert.ToDateTime(row["fecha_end"]).Minute).ToString();
                    string hours = Convert.ToDateTime(row["fecha"]).Hour.ToString();
                    string hours_ = Convert.ToDateTime(row["fecha_end"]).Hour.ToString();
                    month_ = month_.Length == 1 ? "0" + month_ : month_;
                    day_ = day_.Length == 1 ? "0" + day_ : day_;
                    hours = hours.Length == 1 ? "0" + hours : hours;
                    minutes_ = minutes_.Length == 1 ? "0" + minutes_ : minutes_;
                    minutes_finish = minutes_finish.Length == 1 ? "0" + minutes_finish : minutes_finish;
                    eventos = eventos +
                                "{title: '" + row["titulo"].ToString() + "'," +
                                "start: '" + year_ + "-" + month_ + "-" + day_ + "T" + hours + ":" + minutes_ + ":00'," +
                                "end:'" + year_ + "-" + month_ + "-" + day_ + "T" + hours + ":" + minutes_finish + ":00'," +
                                "backgroundColor:'" + color + "'," +
                                "borderColor: '" + color + "'," +
                                "allday:false," +
                                "id:" + row["id_recordatorio"].ToString() + "," +
                                "organizador:'" + (row["organizer"] == null ? "" : row["organizer"].ToString()) + "'," +
                                "ubicacion:'" + (row["location"] == null ? "" : row["location"].ToString()) + "'," +
                                "descripcion:'" + (row["descripcion"] == null ? "" : row["descripcion"].ToString().Replace(Environment.NewLine, " ")) + "'," +
                                "organizador_mail:'" + (row["organizer_address"] == null ? "" : row["organizer_address"].ToString()) + "'," +
                                "Isappointment:" + Convert.ToBoolean(row["appointment"]).ToString().ToLower() + "," +
                                "start_:'" + Convert.ToDateTime(row["fecha"]).ToString("dddd dd MMMM, yyyy hh:mm tt", CultureInfo.CreateSpecificCulture("es-MX")) + "'," +
                                "end_:'" + Convert.ToDateTime(row["fecha_end"]).ToString("dddd dd MMMM, yyyy hh:mm tt", CultureInfo.CreateSpecificCulture("es-MX")) + "'" +

                                "},";
                }
                eventos = eventos.Length > 1 ? eventos.Substring(0, eventos.Length - 1) : eventos;
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
                                    "     locale: 'es',height: 300," +
                                    "     header: {" +
                                    "         left: 'prev,next today'," +
                                    "         center: 'title'," +
                                    "         right: 'month,agendaWeek,agendaDay'" +
                                    "     }," +
                                    "eventClick: function (calEvent, jsEvent, view) {" +
                                    "      var Isappointment = calEvent.Isappointment;" +
                                    "      $('#div_fecha_fin').hide();" +
                                    "      $('#div_organizador').hide();" +
                                    "      if (calEvent.Isappointment)" +
                                    "      {                " +
                                    "          $('#div_fecha_fin').show();" +
                                    "          $('#div_organizador').show();" +
                                    "          $('#ContentPlaceHolder1_rtxtlugar').val(calEvent.ubicacion);" +
                                    "          $('#ContentPlaceHolder1_rtxtorganizador').val(calEvent.organizador);" +
                                    "          $('#ContentPlaceHolder1_rtxtcorreorganizador').val(calEvent.organizador_mail);" +
                                    "          $('#ContentPlaceHolder1_txtfechafin').val(calEvent.end_);" +
                                    "      }" +
                                    "      $('#ContentPlaceHolder1_txtfechainicio').val(calEvent.start_);" +
                                    "      $('#ContentPlaceHolder1_rtxttitulo').val(calEvent.title);" +
                                    "      $('#ContentPlaceHolder1_rtxtdescripcion').val(calEvent.descripcion);" +
                                    "      ModalShow('#modal_evento');" +
                                    "  }," +
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
                                    " $('#calendar').fullCalendar('gotoDate', '" + year + "-" + month + "-" + day + "');" +
                                " });");
                sb.Append("</script>");
                return sb;
            }
            catch (Exception ex)
            {
                Toast.Error("Error al generar calendario: " + ex.Message, this);
                return new StringBuilder();
            }
        }

        private void IniciarCalendario()
        {
            ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), InicializarCalendario().ToString(), false);
        }

        private void ModalShow(string modalname)
        {
            System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                             "ModalShow('" + modalname + "');", true);
        }
    }
}