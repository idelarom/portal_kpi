using System;
using System.Web.UI;

namespace presentacion
{
    public class Toast
    {
        public static string config = "toastr.options = {"
                + "'closeButton': true,"
                + "'debug': false,"
                + "'newestOnTop': true,"
                + "'progressBar': true,"
                + "'positionClass': 'toast-top-full-width',"
                + "'preventDuplicates': true,"
                + "'onclick': null,"
                + "'showDuration': '300',"
                + "'hideDuration': '1000',"
                + "'timeOut': '3000',"
                + "'extendedTimeOut': '1000',"
                + "'showEasing': 'swing',"
                + "'hideEasing': 'linear',"
                + "'showMethod': 'fadeIn',"
                + "'hideMethod': 'fadeOut'"
                + "};";
        public static void Success(String Mensaje, String Titulo, Page mypage)
        {
            Mensaje = Mensaje.Replace("'", "");
            char cr = (char)13;
            char lf = (char)10;
            char tab = (char)9;
            Mensaje = Mensaje.Replace(cr.ToString(), "");
            Mensaje = Mensaje.Replace(lf.ToString(), "");
            Mensaje = Mensaje.Replace(tab.ToString(), "");

            ScriptManager.RegisterClientScriptBlock(mypage, mypage.GetType(), Guid.NewGuid().ToString(), config + "toastr.success('" + Mensaje + "','" + Titulo + "');", true);
        }
        public static void Info(String Mensaje, String Titulo, Page mypage)
        {
            Mensaje = Mensaje.Replace("'", "");
            char cr = (char)13;
            char lf = (char)10;
            char tab = (char)9;
            Mensaje = Mensaje.Replace(cr.ToString(), "");
            Mensaje = Mensaje.Replace(lf.ToString(), "");
            Mensaje = Mensaje.Replace(tab.ToString(), "");

            ScriptManager.RegisterClientScriptBlock(mypage, mypage.GetType(), Guid.NewGuid().ToString(), config + "toastr.info('" + Mensaje + "','" + Titulo + "');", true);
        }
        public static void Error(String Mensaje, Page mypage)
        {
            Mensaje = Mensaje.Replace("'", "");
            char cr = (char)13;
            char lf = (char)10;
            char tab = (char)9;
            Mensaje = Mensaje.Replace(cr.ToString(), "");
            Mensaje = Mensaje.Replace(lf.ToString(), "");
            Mensaje = Mensaje.Replace(tab.ToString(), "");

            ScriptManager.RegisterClientScriptBlock(mypage, mypage.GetType(), Guid.NewGuid().ToString(), config + "toastr.error('" + Mensaje + "','Mensaje del Sistema');", true);
        }
        public static void Warning(String Mensaje, String Titulo, Page mypage)
        {
            Mensaje = Mensaje.Replace("'", "");
            char cr = (char)13;
            char lf = (char)10;
            char tab = (char)9;
            Mensaje = Mensaje.Replace(cr.ToString(), "");
            Mensaje = Mensaje.Replace(lf.ToString(), "");
            Mensaje = Mensaje.Replace(tab.ToString(), "");

            ScriptManager.RegisterClientScriptBlock(mypage, mypage.GetType(), Guid.NewGuid().ToString(), config + "toastr.warning('" + Mensaje + "','" + Titulo + "');", true);
        }
    }
}