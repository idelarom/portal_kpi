using Clockwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PruebasUnitarias
{
    class Program
    {

        public string Send()
        {
            try
            {

                Clockwork.API api = new API("d09befc38459ecb67e7096a78767ee0de581bb6d ");
                SMSResult result = api.Send(
                    new SMS
                    {
                        To = "8120983011",
                        Message = "Hello World"
                    });

                if (result.Success)
                {
                    return "OK";
                }
                else { return "ERROR"; }

            }
            catch (APIException ex)
            {
                return ex.Message;
            }
            catch (WebException ex)
            {
                return ex.Message;
            }
            catch (ArgumentException ex)
            {
                return ex.Message;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
        static void Main(string[] args)
        {
            try
            {

                Program sms = new Program();
                Console.WriteLine("Enviando...");
                string result = sms.Send();
                Console.WriteLine(result);
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }
    }
}
