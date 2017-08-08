using Microsoft.Exchange.WebServices.Autodiscover;
using Microsoft.Exchange.WebServices.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using negocio.Entidades;
using System.Text.RegularExpressions;
using negocio.Componentes;
using System.Data;
using datos.Model;

namespace presentacion
{
    public class EWSHelper
    {
        ExchangeService service = null;
        Microsoft.Exchange.WebServices.Autodiscover.AutodiscoverService autodiscoverService;
        public bool exchangeInitialized = false;
        WebCredentials credentials;
        ExtendedPropertyDefinition PR_TRANSPORT_MESSAGE_HEADERS = new ExtendedPropertyDefinition(0x007D, MapiPropertyType.String);
        public PropertySet psPropSet;

        public EWSHelper()
        {
            psPropSet = new PropertySet(BasePropertySet.FirstClassProperties) { PR_TRANSPORT_MESSAGE_HEADERS, ItemSchema.MimeContent };
        }

        /// <summary>
        /// Regresa un array en forma de string con los appointmen de un usuario por fecha
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public string GetArrayCalendar(string userId, string password, DateTime startDate, DateTime endDate)
        {
            try
            {
                Console.WriteLine("Init");
                service = new ExchangeService(ExchangeVersion.Exchange2013_SP1);
                credentials = new WebCredentials(userId, password);
                service.Credentials = credentials;
                service.Url = new Uri("https://mail.migesa.com.mx/ews/exchange.asmx");
                const int NUM_APPTS = 5;

                // Initialize the calendar folder object with only the folder ID. 
                CalendarFolder calendar = CalendarFolder.Bind(service, WellKnownFolderName.Calendar, new PropertySet());

                // Set the start and end time and number of appointments to retrieve.
                CalendarView cView = new CalendarView(startDate, endDate);

                // Limit the properties returned to the appointment's subject, start time, and end time.
                cView.PropertySet = new PropertySet(AppointmentSchema.Subject, AppointmentSchema.Start, AppointmentSchema.End);

                // Retrieve a collection of appointments by using the calendar view.
                FindItemsResults<Microsoft.Exchange.WebServices.Data.Appointment> appointments = calendar.FindAppointments(cView);
                string value = "";
                foreach (Microsoft.Exchange.WebServices.Data.Appointment a in appointments)
                {
                    DateTime fecha_inicio = a.Start;
                    DateTime fecha_fin = a.End;
                    string subject = a.Subject == null ? "" : a.Subject.ToString();
                    string day_ = fecha_inicio.Day.ToString();
                    string month_ = fecha_inicio.Month.ToString();
                    string year_ = fecha_inicio.Year.ToString();
                    string minutes_ = (fecha_inicio.Minute).ToString();
                    string hours = fecha_inicio.Hour.ToString();
                    string day_f = fecha_fin.Day.ToString();
                    string month_f = fecha_fin.Month.ToString();
                    string year_f = fecha_fin.Year.ToString();
                    string minutes_f = (fecha_fin.Minute).ToString();
                    string hours_f = fecha_fin.Hour.ToString();
                    month_ = month_.Length == 1 ? "0" + month_ : month_;
                    day_ = day_.Length == 1 ? "0" + day_ : day_;
                    hours = hours.Length == 1 ? "0" + hours : hours;
                    minutes_ = minutes_.Length == 1 ? "0" + minutes_ : minutes_;
                    month_f = month_f.Length == 1 ? "0" + month_f : month_f;
                    day_f = day_f.Length == 1 ? "0" + day_f : day_f;
                    hours_f = hours_f.Length == 1 ? "0" + hours_f : hours_f;
                    minutes_f = minutes_f.Length == 1 ? "0" + minutes_f : minutes_f;
                    value = value + "  {title: '" + subject + "'," +
                                "start: '" + year_ + "-" + month_ + "-" + day_ + "T" + hours + ":" + minutes_ + ":00'," +
                                "end:'" + year_f + "-" + month_f + "-" + day_f + "T" + hours_f + ":" + minutes_f + ":00'," +
                                "backgroundColor: '#1976d2'," +
                                "borderColor: '#1976d2'," +
                                "allday:false," +
                                "id:0" +
                                "},";
                }
                return value;
            }
            catch (Exception ex)
            {
                exchangeInitialized = false;
                return "";
            }
        }
        
        /// <summary>
        /// Cancela un Appointment
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="id"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public string CancelAppointment(string userId, string password,string id, string message)
        {
            try
            {
                Console.WriteLine("Init");
                service = new ExchangeService(ExchangeVersion.Exchange2013_SP1);
                credentials = new WebCredentials(userId, password);
                service.Credentials = credentials;
                service.Url = new Uri("https://mail.migesa.com.mx/ews/exchange.asmx");

                DateTime startDate = DateTime.Now.AddMonths(-6);
                DateTime endDate = DateTime.Now.AddMonths(6);
                // Initialize the calendar folder object with only the folder ID. 
                CalendarFolder calendar = CalendarFolder.Bind(service, WellKnownFolderName.Calendar, new PropertySet());

                // Set the start and end time and number of appointments to retrieve.
                CalendarView cView = new CalendarView(startDate, endDate);

                // Limit the properties returned to the appointment's subject, start time, and end time.
                cView.PropertySet = new PropertySet(AppointmentSchema.Subject, AppointmentSchema.Start, AppointmentSchema.End);

                // Retrieve a collection of appointments by using the calendar view.
                FindItemsResults<Microsoft.Exchange.WebServices.Data.Appointment> appointments = calendar.FindAppointments(cView);
                negocio.Entidades.Appointment appoint = null;
                foreach (Microsoft.Exchange.WebServices.Data.Appointment a in appointments)
                {
                    a.Load();
                    if (a.Id.ToString() == id)
                    {
                        a.CancelMeeting(message);
                    }
                }
                return "";
            }
            catch (Exception ex)
            {
                exchangeInitialized = false;
                return ex.Message;
            }
        }

        /// <summary>
        /// Cancela un Appointment
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="id"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public string AcceptAppointment(string userId, string password, string id)
        {
            try
            {
                Console.WriteLine("Init");
                service = new ExchangeService(ExchangeVersion.Exchange2013_SP1);
                credentials = new WebCredentials(userId, password);
                service.Credentials = credentials;
                service.Url = new Uri("https://mail.migesa.com.mx/ews/exchange.asmx");

                DateTime startDate = DateTime.Now.AddMonths(-6);
                DateTime endDate = DateTime.Now.AddMonths(6);
                // Initialize the calendar folder object with only the folder ID. 
                CalendarFolder calendar = CalendarFolder.Bind(service, WellKnownFolderName.Calendar, new PropertySet());

                // Set the start and end time and number of appointments to retrieve.
                CalendarView cView = new CalendarView(startDate, endDate);

                // Limit the properties returned to the appointment's subject, start time, and end time.
                cView.PropertySet = new PropertySet(AppointmentSchema.Subject, AppointmentSchema.Start, AppointmentSchema.End);

                // Retrieve a collection of appointments by using the calendar view.
                FindItemsResults<Microsoft.Exchange.WebServices.Data.Appointment> appointments = calendar.FindAppointments(cView);
                negocio.Entidades.Appointment appoint = null;
                foreach (Microsoft.Exchange.WebServices.Data.Appointment a in appointments)
                {
                    a.Load();
                    if (a.Id.ToString() == id)
                    {
                        a.Accept(true);
                    }
                }
                return "";
            }
            catch (Exception ex)
            {
                exchangeInitialized = false;
                return ex.Message;
            }
        }

        /// <summary>
        /// Rechaza un Appointment
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="id"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public string DeclineAppointment(string userId, string password, string id, string mess)
        {
            try
            {
                Console.WriteLine("Init");
                service = new ExchangeService(ExchangeVersion.Exchange2013_SP1);
                credentials = new WebCredentials(userId, password);
                service.Credentials = credentials;
                service.Url = new Uri("https://mail.migesa.com.mx/ews/exchange.asmx");

                DateTime startDate = DateTime.Now.AddMonths(-6);
                DateTime endDate = DateTime.Now.AddMonths(6);
                // Initialize the calendar folder object with only the folder ID. 
                CalendarFolder calendar = CalendarFolder.Bind(service, WellKnownFolderName.Calendar, new PropertySet());

                // Set the start and end time and number of appointments to retrieve.
                CalendarView cView = new CalendarView(startDate, endDate);

                // Limit the properties returned to the appointment's subject, start time, and end time.
                cView.PropertySet = new PropertySet(AppointmentSchema.Subject, AppointmentSchema.Start, AppointmentSchema.End);

                // Retrieve a collection of appointments by using the calendar view.
                FindItemsResults<Microsoft.Exchange.WebServices.Data.Appointment> appointments = calendar.FindAppointments(cView);
                negocio.Entidades.Appointment appoint = null;
                foreach (Microsoft.Exchange.WebServices.Data.Appointment a in appointments)
                {
                    a.Load();
                    if (a.Id.ToString() == id)
                    {
                        a.Decline(true);
                    }
                }
                return "";
            }
            catch (Exception ex)
            {
                exchangeInitialized = false;
                return ex.Message;
            }
        }

        /// <summary>
        /// Inserta los meetings en una tabla sql
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public void GetAllCalendar(string userId, string password)
        {
            try
            {
                List<datos.Model.recordatorios> list = new List<datos.Model.recordatorios>();
                service = new ExchangeService(ExchangeVersion.Exchange2010);
                credentials = new WebCredentials(userId, password);
                service.Credentials = credentials;
                service.TraceEnabled = true;
                service.TraceFlags = TraceFlags.All;
                service.Url = new Uri("https://mail.migesa.com.mx/ews/exchange.asmx");
                service.AutodiscoverUrl(userId, RedirectionUrlValidationCallback);
                DateTime startDate = DateTime.Now.AddMonths(-6);
                DateTime endDate = DateTime.Now.AddMonths(3);
                // Initialize the calendar folder object with only the folder ID. 
                CalendarFolder calendar = CalendarFolder.Bind(service, WellKnownFolderName.Calendar, new PropertySet());

                // Set the start and end time and number of appointments to retrieve.
                CalendarView cView = new CalendarView(startDate.AddDays(-1), endDate.AddDays(1));

                // Limit the properties returned to the appointment's subject, start time, and end time.
                cView.PropertySet = new PropertySet(AppointmentSchema.Subject, AppointmentSchema.Start, AppointmentSchema.End);

                // Retrieve a collection of appointments by using the calendar view.
                FindItemsResults<Microsoft.Exchange.WebServices.Data.Appointment> appointments = calendar.FindAppointments(cView);    
                string username = (userId.Split('@')[0]);
                PropertySet itempropertyset = new PropertySet(BasePropertySet.FirstClassProperties);
                itempropertyset.RequestedBodyType = BodyType.Text;
                RecordatoriosCOM reco = new RecordatoriosCOM();
                int total_actual = reco.GetRecords(username);
                if (total_actual != appointments.Items.Count)
                {
                    foreach (Microsoft.Exchange.WebServices.Data.Appointment a in appointments)
                    {
                        RecordatoriosCOM recordatorios = new RecordatoriosCOM();
                        a.Load(itempropertyset);
                        string id = a.Id.ToString();
                        if (!recordatorios.ExistAppointment(username, id))
                        {
                            List<recordatorios_usuarios_adicionales> list_Ad = new List<recordatorios_usuarios_adicionales>();
                            string nbody = a.Body.Text == null ? "" : a.Body.Text.ToString();
                            datos.Model.recordatorios e = new datos.Model.recordatorios();
                            DateTime fecha_inicio = a.Start;
                            DateTime fecha_fin = a.End;
                            string subject = a.Subject == null ? "" : a.Subject.ToString();
                            string organizer = a.Organizer.Name == null ? "" : a.Organizer.Name.ToString();
                            string organizer_address = a.Organizer.Address == null ? "" : a.Organizer.Address.ToString();
                            string body = a.Body.Text == null ? "" : a.Body.Text.ToString();
                            string participantes = a.DisplayTo == null ? "" : a.DisplayTo.ToString();
                            string lugar = a.Location == null ? "" : a.Location.ToString();
                            if (participantes != "")
                            {
                                string[] participantes_array = participantes.Split(';');
                                foreach (string part in participantes_array)
                                {
                                    if (part.ToUpper() != organizer.ToUpper())
                                    {
                                        recordatorios_usuarios_adicionales rec = new recordatorios_usuarios_adicionales
                                        {
                                            nombre = part,
                                            activo = true
                                        };
                                        list_Ad.Add(rec);
                                    }
                                }
                            }
                            e.organizer = organizer;
                            e.organizer_address = organizer_address;
                            e.key_appointment_exchanged = id;
                            e.fecha = fecha_inicio;
                            e.fecha_end = fecha_fin;
                            e.titulo = subject;
                            e.usuario = username;
                            e.descripcion = body;
                            e.usuario_creacion = username;
                            e.location = lugar;
                            recordatorios.Agregar(e, list_Ad);
                        }
                    }

                }
              
            }
            catch (Microsoft.Exchange.WebServices.Data.ServiceObjectPropertyException obj)
            {
                exchangeInitialized = false;
            }
            catch (Exception ex)
            {
                exchangeInitialized = false;
            }
        }

        static bool RedirectionUrlValidationCallback(string redirectionUrl)
        {
            // The default for the validation callback is to reject the URL.
            bool result = false;

            Uri redirectionUri = new Uri(redirectionUrl);

            // Validate the contents of the redirection URL. In this simple validation
            // callback, the redirection URL is considered valid if it is using HTTPS
            // to encrypt the authentication credentials. 
            if (redirectionUri.Scheme == "https")
            {
                result = true;
            }

            return result;
        }
    }
}