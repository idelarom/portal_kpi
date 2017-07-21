<%@ Page Title="" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="localizador.aspx.cs" Inherits="presentacion.localizador" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<%--    <script src='https://api.mapbox.com/mapbox.js/v2.4.0/mapbox.js'></script>
    <link href='https://api.mapbox.com/mapbox.js/v2.4.0/mapbox.css' rel='stylesheet' />--%>
    

    <script src="dist/js/gmap.js"></script>
    <script type="text/javascript">

        function ver(lat, lon) {
            $("#map").height($(window).height() - 250);
            var myLatlng = new google.maps.LatLng(lat, lon);
            var mapOptions = {
                zoom: 14,
                center: myLatlng
            }

            var map = new google.maps.Map(document.getElementById("map"), mapOptions);

            var marker = new google.maps.Marker({
                position: myLatlng,
                title: "Nuevo Dispositivo Detectado"
            });

            map.setTilt(45);
            marker.setMap(map);
 
        }
        function cerrar() {
            window.close();
            return false;
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="map" style="width: 100%;"></div>
     <div class="col-lg-12 col-sm-12">
        <h5><strong><i class="fa fa-desktop" aria-hidden="true"></i>&nbsp;Descripción</strong></h5>
        <telerik:RadTextBox ID="rtxtdesc" Width="100%" runat="server" Skin="Bootstrap" ReadOnly="True" Rows="4" TextMode="MultiLine"></telerik:RadTextBox>
    </div>
</asp:Content>
