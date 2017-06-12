using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text;

namespace negocio
{
    /// <summary>
    /// EN ESTA CLASE SE AGREGAN TODAS LAS FUNCIONES QUE PUEDAN UTILIZARSE DE MANERA GLOBAL, SE DEBE AGREGAS EL CONTEXTO DE LA PAGINA C#(THIS), VB(ME)
    /// </summary>
    public class funciones
    {
        public static string NombreMes(string strOutput)
        {
            strOutput = strOutput.Replace("Monday", "Lunes");
            strOutput = strOutput.Replace("Tuesday", "Martes");
            strOutput = strOutput.Replace("Wednesday", "Miércoles");
            strOutput = strOutput.Replace("Thursday", "Jueves");
            strOutput = strOutput.Replace("Friday", "Viernes");
            strOutput = strOutput.Replace("Saturday", "Sábado");
            strOutput = strOutput.Replace("Sunday", "Domingo");
            strOutput = strOutput.Replace("January", "Enero");
            strOutput = strOutput.Replace("February", "Febrero");
            strOutput = strOutput.Replace("March", "Marzo");
            strOutput = strOutput.Replace("April", "Abril");
            strOutput = strOutput.Replace("May", "Mayo");
            strOutput = strOutput.Replace("June", "Junio");
            strOutput = strOutput.Replace("July", "Julio");
            strOutput = strOutput.Replace("August", "Agosto");
            strOutput = strOutput.Replace("September", "Septiembre");
            strOutput = strOutput.Replace("October", "Octubre");
            strOutput = strOutput.Replace("November", "Noviembre");
            strOutput = strOutput.Replace("December", "Diciembre");
            return strOutput;
        }

        private String FormatNumber(decimal valor, int decimales)
        {
            string total_decimale = "";
            for (int i = 0; i < decimales; i++)
            {
                total_decimale = total_decimale + "#";
            }

            return valor.ToString("#,###." + total_decimale);
        }

        /// <summary>
        /// Devuelve meses
        /// </summary>
        /// <returns></returns>
        public static DataTable meses()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("value", typeof(Int32));
            dt.Columns.Add("name");
            for (int i = 1; i <= 12; i++)
            {
                DataRow row = dt.NewRow();
                string fullMonthName = new DateTime(2015, i, 1).ToString("MMMM", CultureInfo.CreateSpecificCulture("es")).ToUpper();
                row["value"] = i;
                row["name"] = fullMonthName;
                dt.Rows.Add(row);
            }
            DataView dv = dt.DefaultView;
            dv.Sort = "value";
            return dv.ToTable();
        }

        public static DataTable años()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("value");
            dt.Columns.Add("name");

            for (int i = 2000; i <= DateTime.Today.Year; i++)
            {
                DataRow row = dt.NewRow();
                row["value"] = i;
                row["name"] = i.ToString().Trim();
                dt.Rows.Add(row);
            }
            DataView dv = dt.DefaultView;
            dv.Sort = "value";
            return dv.ToTable();
        }

        public static DataTable TopDataRow(DataTable dt, int count)
        {
            DataTable dtn = dt.Clone();
            int i = 0;
            foreach (DataRow row in dt.Rows)
            {
                if (i < count)
                {
                    dtn.ImportRow(row);
                    i++;
                }
                if (i > count)
                    break;
            }
            return dtn;
        }

        /// <summary>
        /// Regresa el filtro de una tabla
        /// </summary>
        /// <param name="idc_pregunta"></param>
        public static DataTable FiltrarDataTable(DataTable dt, string query)
        {
            DataView dv = dt.DefaultView;
            dv.RowFilter = query;
            return dv.ToTable();
        }

        /// <summary>
        /// Traduce una fecha al español (BETHA)
        /// </summary>
        /// <returns></returns>
        public static string TradFecha(string fecha)
        {
            string val = "";
            val = fecha.Replace("January", "Enero");
            val = fecha.Replace("February", "Fecbrero");
            val = fecha.Replace("March ", "Marzo");
            val = fecha.Replace("April", "Abril");
            val = fecha.Replace("May", "Mayp");
            val = fecha.Replace("June", "Junio");
            val = fecha.Replace("July", "Julio");
            val = fecha.Replace("August", "Agosto");
            val = fecha.Replace("September", "Septiembre");
            val = fecha.Replace("October", "Octubre");
            val = fecha.Replace("November", "Noviembre");
            val = fecha.Replace("December", "Diciembre");
            val = fecha.Replace(",", "");
            val = fecha.Replace(".", "");
            return val;
        }

        public static string Redondeo_Dos_Decimales(decimal valor)
        {
            try
            {
                valor = Math.Round(valor, 2);
                return valor.ToString("#.##");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool isNumeric(string s)
        {
            decimal i = 0;
            return decimal.TryParse(s, out i);
        }

        public static string ContenidoArchivo(string ruta)
        {
            string value = "";
            try
            {
                if (File.Exists(ruta))
                {
                    StreamReader file = new StreamReader(ruta);
                    value = file.ReadToEnd();
                    file.Close();
                }
                return value;
            }
            catch (Exception)
            {
                return "";
            }
        }

        /// <summary>
        /// Convierte un Objeto tipo DataTable en FORMATO HTML
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="table_name"></param>
        /// <returns></returns>
        public static StringBuilder TableDinamic(DataTable dt, string table_name)
        {
            StringBuilder html = new StringBuilder();
            html.Append("<table id='" + table_name + "'  class='dt table table-responsive table-bordered- table-condensed'>");
            html.Append("<thead>");
            html.Append("<tr>");
            foreach (DataColumn columna in dt.Columns)
            {
                html.Append("<th>");
                html.Append(columna.ColumnName);
                html.Append("</th>");
            }
            html.Append("</tr>");
            html.Append("</thead>");
            html.Append("<tbody>");
            foreach (DataRow row in dt.Rows)
            {
                html.Append("<tr>");
                foreach (DataColumn columna in dt.Columns)
                {
                    html.Append("<td>");
                    html.Append(row[columna.ColumnName]);
                    html.Append("</td>");
                }
                html.Append("</tr>");
            }
            html.Append("</tbody>");
            html.Append("</table>");
            return html;
        }

        /// <summary>
        /// Convierte el formato string(HH:MM) a minutos en entero
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int ReturnMinutesfromString(string value)
        {
            string hour = "";
            foreach (char val in value)
            {
                if (val != ':')
                {
                    hour = hour + val;
                }
                else
                {
                    value = value.Replace(hour + val.ToString(), "");
                    break;
                }
            }
            return Convert.ToInt32(value) + (Convert.ToInt32(hour) * 60);
        }

        /// <summary>
        /// Regresa un TRUE si la fecha esta dentro de un horario laboral valido, en caso contrario regresa un FALSE
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static Boolean FechaCorrecta(DateTime time)
        {
            bool correcta = true;
            int minutes = ((time.Hour) * 60) + time.Minute;
            if (time.DayOfWeek == DayOfWeek.Sunday)
            {
                correcta = false;
            }
            if (time.DayOfWeek == DayOfWeek.Saturday)
            {
                if (minutes >= 781 || minutes <= 541)
                {
                    correcta = false;
                }
            }
            if (time.DayOfWeek != DayOfWeek.Saturday && time.DayOfWeek != DayOfWeek.Sunday)
            {
                if (minutes >= 1081 || minutes <= 541)
                {
                    correcta = false;
                }
            }
            return correcta;
        }

        public static string de64aTexto(string cadena)
        {
            string enviar;
            var base641 = cadena;
            var data = Convert.FromBase64String(base641);
            enviar = Encoding.UTF8.GetString(data);
            return enviar;
        }

        public static string deTextoa64(string cadena)
        {
            string enviar;
            var bytes = Encoding.UTF8.GetBytes(cadena);
            enviar = Convert.ToBase64String(bytes);
            return enviar;
        }
    }
}