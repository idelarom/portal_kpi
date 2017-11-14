using datos;
using negocio.Componentes;
using presentacion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    /// <summary>
    /// EN ESTA CLASE SE AGREGAN TODAS LAS FUNCIONES QUE PUEDAN UTILIZARSE DE MANERA GLOBAL, SE DEBE AGREGAS EL CONTEXTO DE LA PAGINA C#(THIS), VB(ME)
    /// </summary>
    public class funciones
    {

        public static bool emailIsValid(string email)
        {
            string expresion;
            expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(email, expresion))
            {
                if (Regex.Replace(email, expresion, string.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public static DateTime FirstDateInWeek(DateTime dt, DayOfWeek weekStartDay)
        {
            DateTime dt2 = new DateTime();
            while (dt.DayOfWeek != weekStartDay)
                dt = dt.AddDays(-1);
            return dt;
        }
        public static Boolean Permisos(string usuario, int id_permiso)
        {
            try
            {
                UsuariosCOM usuarioP = new UsuariosCOM();
                Boolean permiso = usuarioP.ExistPermission(usuario, id_permiso);
                return permiso;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static string FirstCharToUpper(string input)
        {
            if (String.IsNullOrEmpty(input))
                throw new ArgumentException("ARGH!");
            return input.First().ToString().ToUpper() + String.Join("", input.Skip(1));
        }

        public static String ValueMoneyMil(decimal money, int numberofdecimals)
        {
            string value = "";
            money = money / 1000;

            decimal d = Math.Round(money, numberofdecimals);
            value = d.ToString("C2");
            value = value + " K";
            value = value.Replace("$","$ ");
            return value;
        }
        public static DataTable ToDataTable<T>(T entity) where T : class
        {
            var properties = typeof(T).GetProperties();
            var table = new DataTable();

            foreach (var property in properties)
            {
                table.Columns.Add(property.Name, property.PropertyType);
            }

            table.Rows.Add(properties.Select(p => p.GetValue(entity, null)).ToArray());
            return table;
        }

        /// <summary>
        /// Devuelve el rango de fecha del trimestre de la fecha que se asigne.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static List<DateTime> RangoFechasTrimestre(DateTime date)
        {
            List<DateTime> list = new List<DateTime>();
            try
            {
                int month = date.Month;
                int year = date.Year;                
                if (month >= 3 && month <= 5)
                {
                    list.Add(Convert.ToDateTime(year.ToString()+"-03-01"));
                    list.Add(Convert.ToDateTime(year.ToString() + "-05-31"));
                }
                else if (month >= 6 && month <= 8)
                {
                    list.Add(Convert.ToDateTime(year.ToString() + "-06-01"));
                    list.Add(Convert.ToDateTime(year.ToString() + "-08-31"));
                }
                else if (month >= 9 && month <= 11)
                {
                    list.Add(Convert.ToDateTime(year.ToString() + "-09-01"));
                    list.Add(Convert.ToDateTime(year.ToString() + "-11-30"));
                }
                else
                {
                    list.Add(Convert.ToDateTime(year.ToString() + "-12-01"));
                    list.Add(Convert.ToDateTime((year+1).ToString() + "-02-28"));
                }
                return list;
            }
            catch (Exception ex)
            {
                return list;
            }

        }

        /// <summary>
        /// Devuelve una tabla con los trimestres del 2014 al 2020
        /// </summary>
        /// <returns></returns>
        public static DataTable tabla_trimestres()
        {
            DataTable dt = new DataTable();
            try
            {
                dt.Columns.Add("trimestre");
                dt.Columns.Add("fecha");
                for (int year = 2014; year < 2020; year++)
                {
                    DataRow nrow1 = dt.NewRow();
                    DataRow nrow2 = dt.NewRow();
                    DataRow nrow3 = dt.NewRow();
                    DataRow nrow4 = dt.NewRow();
                    nrow1["trimestre"] = "marzo " + year.ToString();
                    nrow1["fecha"] = Convert.ToDateTime(year.ToString() + "-03-01");
                    nrow2["trimestre"] = "junio " + year.ToString();
                    nrow2["fecha"] = Convert.ToDateTime(year.ToString() + "-06-01");
                    nrow3["trimestre"] = "septiembre " + year.ToString();
                    nrow3["fecha"] = Convert.ToDateTime(year.ToString() + "-09-01");
                    nrow4["trimestre"] = "diciembre " + year.ToString();
                    nrow4["fecha"] = Convert.ToDateTime(year.ToString() + "-12-01");
                    dt.Rows.Add(nrow1);
                    dt.Rows.Add(nrow2);
                    dt.Rows.Add(nrow3);
                    dt.Rows.Add(nrow4);
                }
                return dt;
            }
            catch (Exception)
            {
                return dt;
            }
        }
   
        public static String SplitLastIndex(string value, char separator)
        {
            if (value.Split(' ').Length > 1)
            {
                int idx = value.LastIndexOf(separator);
                value = value.Replace(value.Substring(idx + 1), "");
            }
            return value;
        }

        /// <summary>
        /// Regresa el encabezado de tipo ContentType de una extension
        /// </summary>
        /// <param name="extension"></param>
        /// <returns></returns>
        public static String ContentType(string extension)
        {
            try
            {
                string contenttype = "";
                switch (extension.ToLower())
                {
                    case ".doc":
                        contenttype = "application/vnd.ms-word";
                        break;

                    case ".docx":
                        contenttype = "application/vnd.ms-word";
                        break;

                    case ".xls":
                        contenttype = "application/vnd.ms-excel";
                        break;

                    case ".xlsx":
                        contenttype = "application/vnd.ms-excel";
                        break;

                    case ".jpg":
                        contenttype = "image/jpg";
                        break;

                    case ".jpeg":
                        contenttype = "image/jpg";
                        break;

                    case ".png":
                        contenttype = "image/png";
                        break;

                    case ".gif":
                        contenttype = "image/gif";
                        break;

                    case ".pdf":
                        contenttype = "application/pdf";
                        break;
                }
                return contenttype;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

  

        public static Boolean Format(DataTable dt)
        {
            bool isEnglish = false;
            foreach (DataRow row in dt.Rows)
            {
                string COMIENZO = RetunrFirmatDate(row["COMIENZO"].ToString(),false);
                string FIN = RetunrFirmatDate(row["FIN"].ToString(),false);
                string value = COMIENZO.Split('/')[1];
                string value2 = FIN.Split('/')[1];
                if (Convert.ToInt32(value) > 12) {
                    isEnglish = true;
                    break;
                }
                if (Convert.ToInt32(value2) > 12)
                {
                    isEnglish = true;
                    break;
                }
            }
            return isEnglish;
        }

        public static String RetunrFirmatDate(string datestr, bool isEnglish)
        {
            try
            {
                datestr = datestr.Replace("lun ", "");
                datestr = datestr.Replace("mar ", "");
                datestr = datestr.Replace("mié ", "");
                datestr = datestr.Replace("jue ", "");
                datestr = datestr.Replace("vie ", "");
                datestr = datestr.Replace("sab ", "");
                datestr = datestr.Replace("dom ", "");
                datestr = datestr.Replace("mon ", "");
                datestr = datestr.Replace("tue ", "");
                datestr = datestr.Replace("wed ", "");
                datestr = datestr.Replace("thu ", "");
                datestr = datestr.Replace("fri ", "");
                datestr = datestr.Replace("sat ", "");
                datestr = datestr.Replace("sun ", "");
                datestr = datestr.Replace("monday ", "");
                datestr = datestr.Replace("tuesday ", "");
                datestr = datestr.Replace("wednesday ", "");
                datestr = datestr.Replace("thursday ", "");
                datestr = datestr.Replace("friday ", "");
                datestr = datestr.Replace("saturday ", "");
                datestr = datestr.Replace("sunday ", "");
                if (isEnglish)
                {
                    string[] cadena = datestr.Split('/');
                    string nee_date = cadena[1] + "/" + cadena[0] + "/" + cadena[2];
                    datestr = nee_date;
                }

                string year_today = datestr.Substring(datestr.Length - 3);
                string caracter = year_today.Substring(0, 1);
                if (caracter == "/")
                {
                    string year = year_today;
                    year_today = year_today.Replace("/", "");
                    year_today = "/20" + year_today;
                    datestr = datestr.Replace(year, year_today);
                }
                return datestr;
            }
            catch (Exception ex)
            {

                return "";
            }
        }

        /// <summary>
        /// Regresa un Archivo CSV en DataTable
        /// </summary>
        /// <param name="path"></param>
        /// <param name="isFirstRowHeader"></param>
        /// <returns></returns>
        public static DataTable GetDataTableFromCsv(string path, bool isFirstRowHeader)
        {
            string CSVFilePathName = @path;
            string[] Lines;
            string[] Fields;
            using (Stream stream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(path))
            using (StreamReader reader2 = new StreamReader(path, Encoding.Default))
            {
                // Would prefer string[] result = reader.ReadAllLines();
                Lines = reader2.ReadToEnd().Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            }
            Fields = Lines[0].Split(new char[] { ';' });
            int Cols = Fields.GetLength(0);
            DataTable dt = new DataTable();

            //1st row must be column names; force lower case to ensure matching later on.
            for (int i = 0; i < Cols; i++)
            {
                string value = Fields[i].ToLower();
                dt.Columns.Add(value, typeof(string));
            }
            DataRow Row;
            for (int i = 1; i < Lines.GetLength(0); i++)
            {
                Fields = Lines[i].Split(new char[] { ';' });
                Row = dt.NewRow();
                for (int f = 0; f < Cols; f++)
                {
                    string value = Fields[f];
                    Row[f] = value;
                }
                dt.Rows.Add(Row);
            }
            return dt;
        }

        /// <summary>
        /// Sube archivos a ruta
        /// </summary>
        /// <param name="ruta"></param>
        /// <returns></returns>
        public static bool UploadFile(FileUpload FileUPL, String ruta, Page page)
        {
            try
            {
                FileUPL.PostedFile.SaveAs(ruta);
                return true;
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.Message, page);
                return false;
            }
        }

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

            for (int i = (DateTime.Now.Year-1); i <= (DateTime.Today.Year+1); i++)
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
            html.Append("<table id='" + table_name + "'  class='dt table table-responsive table-bordered- table-condensed' style='font-size:10px;font-family: Verdana;'>");
            html.Append("<thead>");
            html.Append("<tr>");
            foreach (DataColumn columna in dt.Columns)
            {
                html.Append("<th style='text-align:left;background-color: #f5f5f5 ;padding: 10px;'>");
                html.Append(columna.ColumnName.ToUpper());
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
                    html.Append("<td  style='text-align:left;background-color: #f5f5f5 ;padding: 10px;'>");
                    html.Append(row[columna.ColumnName].ToString());
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
            cadena = cadena == null ? "" : cadena;
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