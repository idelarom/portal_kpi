using System;
using System.Web.UI;

namespace presentation
{
    public class Alert
    {
        /// <summary>
        /// Alerta SweetAlert. Parametros: Mensaje, Titulo, Contexto
        /// </summary>
        /// <param name="Mensaje"></param>
        /// <param name="Titulo"></param>
        /// <param name="mypage"></param>
        public static void ShowAlert(String Mensaje, String Titulo, Page mypage)
        {
            Mensaje = Mensaje.Replace("'", "");
            char cr = (char)13;
            char lf = (char)10;
            char tab = (char)9;
            Mensaje = Mensaje.Replace(cr.ToString(), "");
            Mensaje = Mensaje.Replace(lf.ToString(), "");
            Mensaje = Mensaje.Replace(tab.ToString(), "");

            ScriptManager.RegisterClientScriptBlock(mypage, mypage.GetType(), "alertMessage", "swal('" + Titulo + "', '" + Mensaje + "', 'success')", true);
        }

        /// <summary>
        /// Alerta Error SweetAlert. Parametros: Mensaje, Contexto
        /// </summary>
        /// <param name="Mensaje"></param>
        /// <param name="mypage"></param>
        public static void ShowAlertError(String Mensaje, Page mypage)
        {
            Mensaje = Mensaje.Replace("'", "");
            char cr = (char)13;
            char lf = (char)10;
            char tab = (char)9;
            Mensaje = Mensaje.Replace(cr.ToString(), "");
            Mensaje = Mensaje.Replace(lf.ToString(), "");
            Mensaje = Mensaje.Replace(tab.ToString(), "");

            ScriptManager.RegisterClientScriptBlock(mypage, mypage.GetType(), "showalert",
                "sweetAlert({    title: 'Mensaje del Sistema', text: '" + Mensaje + "', type: 'info', allowEscapeKey:false});", true);
            ScriptManager.RegisterClientScriptBlock(mypage, mypage.GetType(), "sounderror", "PlaySound('sounds/error.wav');", true);
        }

        public static void ShowAlertAutoCloseTimer(String Title, String Mensaje, String Tiempo, bool botonConfirmar, String img, Page mypage)
        {
            Mensaje = Mensaje.Replace("'", "");
            char cr = (char)13;
            char lf = (char)10;
            char tab = (char)9;
            Mensaje = Mensaje.Replace(cr.ToString(), "");
            Mensaje = Mensaje.Replace(lf.ToString(), "");
            Mensaje = Mensaje.Replace(tab.ToString(), "");
            string Str_Atrib = string.Format("title: '{0}', text: '{1}', timer: {2}, showConfirmButton: {3}, imageUrl: '{4}'",
                Title, Mensaje, Tiempo, botonConfirmar.ToString().ToLower(), img);
            string Str_js = "swal({" + Str_Atrib + "});";
            ScriptManager.RegisterClientScriptBlock(mypage, mypage.GetType(), "showalert", Str_js, true);
            ScriptManager.RegisterClientScriptBlock(mypage, mypage.GetType(), "sounderror", "PlaySound('sounds/info.wav');", true);
        }

        /// <summary>
        /// Alerta Info SweetAlert. Parametros: Mensaje,Titulo, Contexto
        /// </summary>
        /// <param name="Mensaje"></param>
        /// <param name="mypage"></param>
        public static void ShowAlertInfo(String Mensaje, String Title, Page mypage)
        {
            Mensaje = Mensaje.Replace("'", "");
            char cr = (char)13;
            char lf = (char)10;
            char tab = (char)9;
            Mensaje = Mensaje.Replace(cr.ToString(), "");
            Mensaje = Mensaje.Replace(lf.ToString(), "");
            Mensaje = Mensaje.Replace(tab.ToString(), "");
            ScriptManager.RegisterClientScriptBlock(mypage, mypage.GetType(), "showalert",
                "sweetAlert({    title: '" + Title + "', text: '" + Mensaje + "', type: 'info', allowEscapeKey:false});", true);
            ScriptManager.RegisterClientScriptBlock(mypage, mypage.GetType(), "sounderror", "PlaySound('sounds/info.wav');", true);
        }

        /// <summary>
        /// Alerta Info SweetAlert. Parametros: Mensaje,Titulo, Contexto
        /// </summary>
        /// <param name="Mensaje"></param>
        /// <param name="mypage"></param>
        public static void ShowAlertConfirm(String Mensaje, String Title, String URL, Page mypage)
        {
            Mensaje = Mensaje.Replace("'", "");
            char cr = (char)13;
            char lf = (char)10;
            char tab = (char)9;
            Mensaje = Mensaje.Replace(cr.ToString(), "");
            Mensaje = Mensaje.Replace(lf.ToString(), "");
            Mensaje = Mensaje.Replace(tab.ToString(), "");
            ScriptManager.RegisterClientScriptBlock(mypage, mypage.GetType(), "showalert",
                "swal({   title: '" + Title + "',   text: '" + Mensaje + "', allowEscapeKey:false,   type: 'warning',   showCancelButton: true,   confirmButtonColor: '#DD6B55',   confirmButtonText: 'Si, Deseo Revisarlo',   closeOnConfirm: false }, function(){  location.href = '" + URL + "';});", true);
        }

        /// <summary>
        /// Alerta Gift y Redireccion
        /// </summary>
        /// <param name="Mensaje"></param>
        /// <param name="mypage"></param>
        public static void ShowGiftMessage(String Mensaje, String Title, String URL, String IMG, String Timer, String MensajeOK, Page mypage)
        {
            Mensaje = Mensaje.Replace("'", "");
            char cr = (char)13;
            char lf = (char)10;
            char tab = (char)9;
            Mensaje = Mensaje.Replace(cr.ToString(), "");
            Mensaje = Mensaje.Replace(lf.ToString(), "");
            Mensaje = Mensaje.Replace(tab.ToString(), "");
            ScriptManager.RegisterClientScriptBlock(mypage, mypage.GetType(), "showalert",
                "swal({   title: '" + Title + "',   text: '" + Mensaje + "', allowEscapeKey:false,imageUrl: '" + IMG + "',   timer: " + Timer + ",   showConfirmButton: false }, function(){swal({  title: 'Mensaje del Sistema', allowEscapeKey:false,text: '" + MensajeOK + "',type: 'success',showCancelButton: false,confirmButtonColor: '#428bca',confirmButtonText: 'Aceptar',showLoaderOnConfirm: true,closeOnConfirm: false }, function () {location.href = '" + URL + "';});});", true);
        }

        /// <summary>
        /// Alerta Gift y Redireccion
        /// </summary>
        /// <param name="Mensaje"></param>
        /// <param name="mypage"></param>
        public static void ShowGiftMessageCloseWidnows(String Mensaje, String Title, String IMG, String Timer, String MensajeOK, Page mypage)
        {
            Mensaje = Mensaje.Replace("'", "");
            char cr = (char)13;
            char lf = (char)10;
            char tab = (char)9;
            Mensaje = Mensaje.Replace(cr.ToString(), "");
            Mensaje = Mensaje.Replace(lf.ToString(), "");
            Mensaje = Mensaje.Replace(tab.ToString(), "");
            ScriptManager.RegisterClientScriptBlock(mypage, mypage.GetType(), "showalert",
                "swal({   title: '" + Title + "',   text: '" + Mensaje + "', allowEscapeKey:false,imageUrl: '" + IMG + "',   timer: " + Timer + ",   showConfirmButton: false }, function(){swal({  title: 'Mensaje del Sistema',text: '" + MensajeOK + "',type: 'success', allowEscapeKey:false,showCancelButton: false,confirmButtonColor: '#428bca',confirmButtonText: 'Aceptar',closeOnConfirm: false }, function () { window.close();});});", true);
        }

        /// <summary>
        /// Alerta Gift y Redireccion
        /// </summary>
        /// <param name="Mensaje"></param>
        /// <param name="mypage"></param>
        public static void ShowGiftSimple(String Mensaje, String Title, String IMG, String Timer, Page mypage)
        {
            Mensaje = Mensaje.Replace("'", "");
            char cr = (char)13;
            char lf = (char)10;
            char tab = (char)9;
            Mensaje = Mensaje.Replace(cr.ToString(), "");
            Mensaje = Mensaje.Replace(lf.ToString(), "");
            Mensaje = Mensaje.Replace(tab.ToString(), "");
            ScriptManager.RegisterClientScriptBlock(mypage, mypage.GetType(), "showalert",
                "swal({   title: '" + Title + "',   text: '" + Mensaje + "', allowEscapeKey:false,imageUrl: '" + IMG + "',   timer: " + Timer + ",   showConfirmButton: false });", true);
        }

        /// <summary>
        /// Alerta Gift y Redireccion
        /// </summary>
        /// <param name="Mensaje"></param>
        /// <param name="mypage"></param>
        public static void ShowGift(String Mensaje, String Title, String IMG, String Timer, String MensajeOK, Page mypage)
        {
            Mensaje = Mensaje.Replace("'", "");
            char cr = (char)13;
            char lf = (char)10;
            char tab = (char)9;
            Mensaje = Mensaje.Replace(cr.ToString(), "");
            Mensaje = Mensaje.Replace(lf.ToString(), "");
            Mensaje = Mensaje.Replace(tab.ToString(), "");
            ScriptManager.RegisterClientScriptBlock(mypage, mypage.GetType(), "showalert",
                "swal({   title: '" + Title + "',   text: '" + Mensaje + "', allowEscapeKey:false,imageUrl: '" + IMG + "',   timer: " + Timer + ",   showConfirmButton: false }, function(){swal({  title: 'Mensaje del Sistema', allowEscapeKey:false,text: '" + MensajeOK + "',type: 'success',showCancelButton: false,confirmButtonColor: '#428bca',confirmButtonText: 'Aceptar',closeOnConfirm: false });});", true);
        }

        /// <summary>
        /// Alerta Gift y Redireccion
        /// </summary>
        /// <param name="Mensaje"></param>
        /// <param name="mypage"></param>
        public static void ShowGiftRedirect(String Mensaje, String Title, String IMG, String Timer, String URL, Page mypage)
        {
            Mensaje = Mensaje.Replace("'", "");
            char cr = (char)13;
            char lf = (char)10;
            char tab = (char)9;
            Mensaje = Mensaje.Replace(cr.ToString(), "");
            Mensaje = Mensaje.Replace(lf.ToString(), "");
            Mensaje = Mensaje.Replace(tab.ToString(), "");
            ScriptManager.RegisterClientScriptBlock(mypage, mypage.GetType(), "showalert",
                "swal({   title: '" + Title + "',   text: '" + Mensaje + "', allowEscapeKey:false,imageUrl: '" + IMG + "',   timer: " + Timer + ",   showConfirmButton: false }, function () {location.href = '" + URL + "';});", true);
        }

        /// <summary>
        /// Alerta Gift y cIERRA PESTAÑA
        /// </summary>
        /// <param name="Mensaje"></param>
        /// <param name="mypage"></param>
        public static void ShowGiftCloseWindows(String Mensaje, String Title, String IMG, String Timer, Page mypage)
        {
            Mensaje = Mensaje.Replace("'", "");
            char cr = (char)13;
            char lf = (char)10;
            char tab = (char)9;
            Mensaje = Mensaje.Replace(cr.ToString(), "");
            Mensaje = Mensaje.Replace(lf.ToString(), "");
            Mensaje = Mensaje.Replace(tab.ToString(), "");
            ScriptManager.RegisterClientScriptBlock(mypage, mypage.GetType(), "showalert",
                "swal({   title: '" + Title + "',   text: '" + Mensaje + "', allowEscapeKey:false,imageUrl: '" + IMG + "',   timer: " + Timer + ",   showConfirmButton: false }, function () {window.close();});", true);
        }
    }
}