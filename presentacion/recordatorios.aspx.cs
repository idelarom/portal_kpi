using negocio.Componentes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class recordatorios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LlenarCalendario(Session["usuario"] as string);
            }
        }

        private void LlenarCalendario(string usuario)
        {
            try
            {
                RecordatoriosCOM recordatorio = new RecordatoriosCOM();
                DataTable dt_days = recordatorio.Get(usuario.ToUpper());
                foreach (DataRow row in dt_days.Rows)
                {
                    DateTime datevalue = Convert.ToDateTime(row["fecha"]);
                    int day = datevalue.Day;
                    int month = datevalue.Month;
                    int year = datevalue.Year;

                    Telerik.Web.UI.RadCalendarDay NewDay = new Telerik.Web.UI.RadCalendarDay(rdcalendar);
                    NewDay.Date = datevalue;// new DateTime(year, month, day);
                    NewDay.Repeatable = Telerik.Web.UI.Calendar.RecurringEvents.DayAndMonth;
                    NewDay.ToolTip = row["titulo"].ToString();
                    NewDay.ItemStyle.CssClass = "day-style";
                    rdcalendar.SpecialDays.Add(NewDay);
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar las fechas en el calendario: "+ex.Message,this);
            }
        }

        protected void rdcalendar_SelectionChanged(object sender, Telerik.Web.UI.Calendar.SelectedDatesEventArgs e)
        {
            DateTime date_selected = rdcalendar.SelectedDate;
            string value = "";
        }
    }
}