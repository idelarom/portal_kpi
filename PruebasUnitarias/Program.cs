using Clockwork;
using negocio.Componentes;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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


        private void Run()
        {
            try
            {
                UsuariosCOM usuarios = new UsuariosCOM();
                DataTable dt = usuarios.sp_prueba_files().Tables[0];
                int files_not_Foudn = 0;
                Console.WriteLine("Archivos no encontrados:");
                foreach (DataRow row in dt.Rows)
                {
                    string path = @"\\mts-web01\d$\Appweb\Compensaciones\Pages\Bonds\" + row["path"].ToString() + row["file_name"].ToString();
                    if (!File.Exists(path))
                    {
                        files_not_Foudn++;
                        Console.WriteLine( "Solicitud: " +row["id_request_bond"].ToString()+ "    Solicitante: " + row["solicitante"].ToString() + "       Fecha de Solicitud: " + row["date_attach"].ToString());
                      //  Console.WriteLine(path);
                    }
                }

                Console.WriteLine("Total de Archivos no encontrados: "+ files_not_Foudn.ToString());
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        static void Main(string[] args)
        {
            try
            {

                Program sms = new Program();
                Console.WriteLine("Cargando...");
                sms.Run();
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
