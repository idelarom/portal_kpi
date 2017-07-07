<%@ Page Title="" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="localizador.aspx.cs" Inherits="presentacion.localizador" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src='https://api.mapbox.com/mapbox.js/v2.4.0/mapbox.js'></script>
    <link href='https://api.mapbox.com/mapbox.js/v2.4.0/mapbox.css' rel='stylesheet' />
    <script type="text/javascript">

        function ver(lat, lon) {
            $("#mapcanvas").height($(window).height() - 250);
            
            L.mapbox.accessToken = 'pk.eyJ1IjoicHJvZ3JhbWFkb3IzIiwiYSI6ImNpc2hwN3JoNjAwNXczM3BpZ3ZocmUwamMifQ.Qpennd5geuMVwKlgUBb69w';
            var map = L.mapbox.map('mapcanvas', 'mapbox.streets')
                .setView([lat, lon], 15);
            var marker = L.marker([lat, lon], {
                icon: L.mapbox.marker.icon({
                    'marker-color': '#f86767'
                })
            });
            marker.addTo(map);
        }
        function cerrar() {
            window.close();
            return false;
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="mapcanvas" style="width: 100%;"></div>
     <div class="col-lg-12 col-sm-12">
        <h5><strong><i class="fa fa-desktop" aria-hidden="true"></i>&nbsp;Descripción</strong></h5>
        <telerik:RadTextBox ID="rtxtdesc" Width="100%" runat="server" Skin="Bootstrap" ReadOnly="True" Rows="4" TextMode="MultiLine"></telerik:RadTextBox>
    </div>
</asp:Content>
