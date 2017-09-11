<%@ Page Title="" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="mantenimiento.aspx.cs" Inherits="presentacion.mantenimiento" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">


<style type="text/css">
/*body{
    text-align: center;
    background: #0085cf;
    font-family: sans-serif;
    font-weight: 100;
}*/
h1{
 color: #fff;
 font-weight: 100;
 font-size: 40px;
 margin: 40px 0px 20px;
}

#reloj{
 font-family: sans-serif;
 color: #fff;
 display: inline-block;
 font-weight: 100;
 text-align: center;
 font-size: 20px;
}

#reloj > div{
    padding: 10px;
    border-radius: 3px;
    background: #DE3230;
     /*#076fa9;*/
    display: inline-block;

}

#reloj div > span{
    padding: 15px;
    border-radius: 3px;
    background: rgba(57, 68, 84, 0.27);
    display: inline-block;
}
.texto{
    padding-top: 5px;
    font-size: 10px;
}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <div class="error-page">
                <div class="error-content">
                </div>
                <!-- /.error-content -->
            </div>
           <%-- <h1>Tiempo estimado de mantenimiento </h1>--%>
            <center>
            <div id="reloj">
              <div>
                <span class="dias"></span>
                <div class="texto">Días</div> 
              </div>
              <div>
                <span class="horas"></span>
                <div class="texto">Horas</div>
              </div>
              <div>
                <span class="minutos"></span>
                <div class="texto">Minutos</div>
              </div>
              <div>
                <span class="segundos"></span>
                <div class="texto">Segundos</div>
              </div>
            </div>
                </center>
            <img class=" img-responsive" src="img/mant.jpg" style="margin-left: auto; margin-right: auto; display: block;max-height:580px" />
        </div>
    </div>
    <asp:HiddenField ID="hdfDias" runat="server" />
    <asp:HiddenField ID="hdfHoras" runat="server" />
    <asp:HiddenField ID="hdfMinutos" runat="server" />
    <asp:HiddenField ID="hdfSegundos" runat="server" />
    <asp:HiddenField ID="hdfFixTime" runat="server" />
 <script>
function getTimeRemaining(endtime) {
  var t = Date.parse(endtime) - Date.parse(new Date());
  var segundos = Math.floor((t / 1000) % 60);
  var minutos = Math.floor((t / 1000 / 60) % 60);
  var horas = Math.floor((t / (1000 * 60 * 60)) % 24);
  var dias = Math.floor(t / (1000 * 60 * 60 * 24));
  return {
    'total': t,
    'dias': dias,
    'horas': horas,
    'minutos': minutos,
    'segundos': segundos
  };
}

function initializeReloj(id, endtime) {
  var reloj = document.getElementById(id);
  var diaSpan = reloj.querySelector('.dias');
  var horaSpan = reloj.querySelector('.horas');
  var minutoSpan = reloj.querySelector('.minutos');
  var segundoSpan = reloj.querySelector('.segundos');

  function updateReloj() {
    var t = getTimeRemaining(endtime);

    diaSpan.innerHTML = t.dias;
    horaSpan.innerHTML = ('0' + t.horas).slice(-2);
    minutoSpan.innerHTML = ('0' + t.minutos).slice(-2);
    segundoSpan.innerHTML = ('0' + t.segundos).slice(-2);

    if (t.total <= 0) {
      clearInterval(timeinterval);
    }
  }

  updateReloj();
  var timeinterval = setInterval(updateReloj, 1000);
}
//var Dias = document.getElementById('<%= hdfDias.ClientID %>').value;//document.getElementById('hdfFixTime');
//var Horas = document.getElementById('<%= hdfHoras.ClientID %>').value;
//var Minutos = document.getElementById('<%= hdfMinutos.ClientID %>').value;
//var Segundos = document.getElementById('<%= hdfSegundos.ClientID %>').value;

var FixTime = document.getElementById('<%= hdfFixTime.ClientID %>').value;
        //var deadline = new Date(Date.parse(new Date()) + 15 * 24 * 60 * 60 * 1000);
     //var deadline = new Date(Date.parse(new Date()) + Dias * Horas * Minutos * Segundos * 1000);
     var deadline = new Date(Date.parse(FixTime));
initializeReloj('reloj', deadline);
</script>
</asp:Content>
