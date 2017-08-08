using Microsoft.Exchange.WebServices.Autodiscover;
using Microsoft.Exchange.WebServices.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace PruebasUnitarias
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

        public string GetToAddress(Item item)
        {
            string toAddress = string.Empty;
            Object valHeaders = null;
            item.Load(psPropSet);

            if (item.TryGetProperty(PR_TRANSPORT_MESSAGE_HEADERS, out valHeaders))
            {
                Regex regex = new Regex(@"To:.*<(.+)>");
                Match match = regex.Match(valHeaders.ToString());
                if (match.Groups.Count == 2)
                    toAddress = match.Groups[1].Value;
            }
            return toAddress;
        }


        public string Initialize(string userId, string password)
        {
            try
            {
                Console.WriteLine("Init");
                service = new ExchangeService(ExchangeVersion.Exchange2007_SP1);
                credentials = new WebCredentials(userId, password);
                service.Credentials = credentials;
                service.Url = new Uri("https://mail.migesa.com.mx/ews/exchange.asmx");
                // Initialize values for the start and end times, and the number of appointments to retrieve.
                DateTime startDate = DateTime.Now;
                DateTime endDate = startDate.AddDays(30);
                const int NUM_APPTS = 5;

                // Initialize the calendar folder object with only the folder ID. 
                CalendarFolder calendar = CalendarFolder.Bind(service, WellKnownFolderName.Calendar, new PropertySet());

                // Set the start and end time and number of appointments to retrieve.
                CalendarView cView = new CalendarView(startDate, endDate, NUM_APPTS);

                // Limit the properties returned to the appointment's subject, start time, and end time.
                cView.PropertySet = new PropertySet(AppointmentSchema.Subject, AppointmentSchema.Start, AppointmentSchema.End);

                // Retrieve a collection of appointments by using the calendar view.
                FindItemsResults<Appointment> appointments = calendar.FindAppointments(cView);
                string value = "";

                foreach (Appointment a in appointments)
                {
                    DateTime fecha_inicio = a.Start;
                    DateTime fecha_fin = a.End;
                    string subject = a.Subject.ToString();
                    string location = a.Location.ToString();
                    string day_ = fecha_inicio.Day.ToString();
                    string month_ = fecha_inicio.ToString();
                    string year_ = fecha_inicio.Year.ToString();
                    string minutes_ = (fecha_inicio.Minute).ToString();
                    string hours = fecha_inicio.Hour.ToString();
                    string day_f = fecha_inicio.Day.ToString();
                    string month_f = fecha_inicio.ToString();
                    string year_f= fecha_inicio.Year.ToString();
                    string minutes_f = (fecha_inicio.Minute).ToString();
                    string hours_f = fecha_inicio.Hour.ToString();
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
                                "backgroundColor: '#f56954'," +
                                "borderColor: '#f56954'," +
                                "allday:false," +
                                "className:0" +
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

        public FindItemsResults<Item> RetrieveInbox(int items)
        {
            if (exchangeInitialized)
            {
                return service.FindItems(
                                        WellKnownFolderName.Inbox,
                                        new ItemView(10));
            }
            return null;
        }

        public EmailMessage RetrieveEmail(ItemId itemId)
        {
            if (exchangeInitialized)
            {
                return Microsoft.Exchange.WebServices.Data.EmailMessage.Bind(service, itemId);

            }
            return null;
        }

        private void SetupAutoDiscover(string userid)
        {
            // Create an instance of the AutodiscoverService.
            autodiscoverService = new
            Microsoft.Exchange.WebServices.Autodiscover.AutodiscoverService();
            // Enable tracing.
            //autodiscoverService.TraceEnabled = true;
            // Set the credentials.
            autodiscoverService.Credentials = credentials;
            // Prevent the AutodiscoverService from looking in the local Active Directory
            // for the Exchange Web Services Services SCP.
            autodiscoverService.EnableScpLookup = false;
            // Specify a redirection URL validation callback that returns true for valid URLs.
            autodiscoverService.RedirectionUrlValidationCallback = RedirectionUrlValidationCallback;

            // Get the Exchange Web Services URL for the user's mailbox.
            GetUserSettingsResponse response =
                autodiscoverService.GetUserSettings(
                    userid,
                    UserSettingName.ExternalEwsUrl);

            // Extract the Exchange Web Services URL from the response.
            var externalEwsUrl = new
            Uri(response.Settings[UserSettingName.ExternalEwsUrl].ToString());
            // Set the URL of the ExchangeService object.
            service.Url = externalEwsUrl;
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
