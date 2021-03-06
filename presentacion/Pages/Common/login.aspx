﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Common/Login.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="presentacion.Pages.Common.login" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../../dist/js/push_notification.js"></script>

    <style type="text/css">
        .login-box, .register-box {
            width: 100%;
            max-width: 360px;
            margin: 10px auto;
        }

        .login-logo, .register-logo {
            font-size: 35px;
            text-align: center;
            margin-bottom: 25px;
            font-weight: 300;
            padding-left: 5px;
        }

        .sweet-alert p {
            color: #797979;
            font-size: 14px;
            text-align: center;
            font-weight: 300;
            position: relative;
            text-align: inherit;
            float: none;
            margin: 0;
            padding: 0;
            line-height: normal;
        }

        .sweet-alert h2 {
            color: #575757;
            font-size: 20px;
            text-align: center;
            font-weight: 600;
            text-transform: none;
            position: relative;
            margin: 25px 0;
            padding: 0;
            line-height: 40px;
            display: block;
        }

        .sweet-alert button {
            background-color: #8CD4F5;
            color: white;
            border: none;
            box-shadow: none;
            font-size: 17px;
            font-weight: 500;
            -webkit-border-radius: 4px;
            border-radius: 5px;
            padding: 10px 20px;
            margin: 20px 5px 0 5px;
            cursor: pointer;
        }
    </style>
    <script type="text/javascript">
          function ConfirmwidgetProyectoModal() {
            $("#<%= lnkcargando.ClientID%>").show();
            $("#<%= lnkguardar.ClientID%>").hide();
            return true;
          }

        function ConfirmMinutaModal() {
            GetInfoDevice();
            $("#<%= lnkiniciandosession.ClientID%>").show();
            $("#<%= btniniciar.ClientID%>").hide();
            return true;
        }
        function GetInfoDevice() {

            var client = new ClientJS(); // Create A New Client Object

            var browser = client.getBrowser(); // Get Browser
            var OS = client.getOS(); // Get OS Version
            var osVersion = client.getOSVersion(); // Get OS Version
            var isMobile = client.isMobile(); // Check For Mobile
            var isMobileAndroid = client.isMobileAndroid(); // Check For Mobile Android
            var isMobileWindows = client.isMobileWindows(); // Check For Mobile Windows
            var isMobileBlackBerry = client.isMobileBlackBerry(); // Check For Mobile Blackberry
            var isMobileIOS = client.isMobileIOS(); // Check For Mobile iOS
            var isIphone = client.isIphone(); // Check For iPhone
            var isIpad = client.isIpad(); // Check For iPad
            var isIpod = client.isIpod(); // Check For iPod
            var deviceVendor = client.getDeviceVendor(); // Get Device Vendor
            var fingerprint = client.getFingerprint(); // Get Client's Fingerprint

            var device = "";
            if (isMobile) {
                if (isMobileAndroid) {
                    device = "Movil";
                } else if (isMobileWindows) {
                    device = "Windows Phone";
                } else if (isMobileBlackBerry) {
                    device = "Black Berry";
                } else if (isMobileIOS) {
                    if (isIphone) {
                        device = "iPhone";
                    } else if (isIpod) {
                        device = "iPod";
                    } else if (isIpad) {
                        device = "iPad";
                    }
                }
            } else {
                device = "Computadora";
            }
            //console.log("Dispositivo: " + device);
            //console.log("Navegador: " + browser);
            //console.log("OS: " + OS);
            //console.log("OS_Version: " + osVersion);
            
            $('#<%= hdfmodel.ClientID%>').val(deviceVendor);
            $('#<%= hdffinger.ClientID%>').val(fingerprint);
            $('#<%= hdfdevice.ClientID%>').val(device);
            $('#<%= hdfos.ClientID%>').val(OS);
            $('#<%= hdfosversion.ClientID%>').val(osVersion);
            $('#<%= hdfbrowser.ClientID%>').val(browser);

        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            //if (location.protocol != 'https:' && location.hostname != "localhost") {
            //    location.href = 'https:' + window.location.href.substring(window.location.protocol.length);
            //} else 
                getLocation();
                initGeolocation();
           // }
        });

        function initGeolocation() {
            $.getJSON('https://ipinfo.io', function (response) {
                var ip = response.ip;
                var region = (response.city == "" ? "" : response.city + ", ") + (response.region == "" ? "" : response.region + ", ") + response.country;
                var lat = response.loc.split(',')[0];
                var lon = response.loc.split(',')[1];
                var proveedor = response.org;
                if ($('#<%= hdflatitud.ClientID%>').val() == "") {
                    $('#<%= hdflatitud.ClientID%>').val(lat);
                    $('#<%= hdflongitud.ClientID%>').val(lon);
                }
                $('#<%= hdfip.ClientID%>').val(ip);
                $('#<%= hdfregion.ClientID%>').val(region);
                $('#<%= hdfproveedor.ClientID%>').val(proveedor);
            });
        }

        
        function getLocation() {
            var options = {
                enableHighAccuracy: true,
                timeout: 6000,
                maximumAge: 0
            };

            navigator.geolocation.getCurrentPosition(success, error, options);

            function success(position) {
                var coordenadas = position.coords;
                var lat = coordenadas.latitude;
                var lon = coordenadas.longitude;
                $('#<%= hdflatitud.ClientID%>').val(lat);
                $('#<%= hdflongitud.ClientID%>').val(lon);
            };

            function error(error) {
                if (location.protocol != 'https:' && location.hostname != "localhost") {
                     swal({
                        title: "No sabemos donde estas :(",
                        text: "Para utilizar todas las herramientas del portal, debes permitir detectar tu ubicación. Es posible que tu navegador no sea compatible con esta funcionalidad.",

                        imageUrl: "../../img/local.png",
                        showCancelButton: true,
                        confirmButtonColor: "#DD6B55",
                        confirmButtonText: "Recargar",
                        cancelButtonText: "Cerrar",
                        closeOnConfirm: true,
                        closeOnCancel: true
                    },
                    function (isConfirm) {
                        if (isConfirm) {
                            location.reload();
                        }
                    });
                 }
            };
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="form-group has-feedback" id="div_dominio" runat="server" visible="false">
                 
                <telerik:RadTextBox ID="rtxtdominio" runat="server" Text="migesa.net" CssClass="form-control" Width="100%" Skin="Bootstrap" placeholder="Dominio"></telerik:RadTextBox>
                <span class="glyphicon glyphicon-cloud form-control-feedback"></span>
            </div>
            <div class="form-group has-feedback">
                <telerik:RadTextBox ID="rtxtusuario" runat="server" CssClass="form-control" Width="100%" Skin="Bootstrap" placeholder="Usuario"></telerik:RadTextBox>
                <span class="glyphicon glyphicon-user form-control-feedback"></span>
            </div>
            <div class="form-group has-feedback">
                <telerik:RadTextBox ID="rtxtcontra" runat="server" CssClass="form-control" Width="100%" Skin="Bootstrap" TextMode="Password"
                    placeholder="Contraseña">
                </telerik:RadTextBox>

                <span class="glyphicon glyphicon-lock form-control-feedback"></span>
            </div>
            <div class="row">
                <div class="col-xs-12" style="text-align: right;">
                    <asp:LinkButton ID="lnkiniciandosession" CssClass="btn btn-danger btn-block" runat="server" OnClientClick="return false;" Style="display: none;">
                          <i class="fa fa-refresh fa-spin fa-fw"></i>
                                            <span class="sr-only">Loading...</span>&nbsp;Iniciando sesión</asp:LinkButton>
                    <asp:Button ID="btniniciar" runat="server" Text="Iniciar sesión" CssClass="btn btn-danger"
                        OnClick="btniniciar_Click" OnClientClick="return ConfirmMinutaModal();" />
                </div>

                <div class="col-xs-12" runat="server" id="div_cambiodomiinio" visible="true" style="font-size:12px">
                    <br />
                    <p>
                        ¿No puedes ingresar? <asp:LinkButton ID="lnkcambiardominio" Visible="false" runat="server" OnClick="lnkcambiardominio_Click">
                        Puedes cambiar el dominio de inicio
                    </asp:LinkButton>
                        <asp:LinkButton ID="lnkrecuperarcontraseña" runat="server" OnClick="lnkrecuperarcontraseña_Click">
                         Recuperar contraseña
                    </asp:LinkButton>
                    </p>
                </div>

                <div class="col-xs-12" runat="server" id="div_portalclientes" visible="false">
                    <br />
                    <p>
                        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="lnkcambiardominio_Click">
                        Puedes Cambiar el Dominio de Inicio
                        </asp:LinkButton>
                    </p>
                </div>
                <!-- /.col -->
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <br />
                    <p style="font-size: 10px; text-align: justify;">
                        Advertencia: El uso de este sistema es limitado a usuarios autorizados. Este sistema es propiedad privada de Migesa y puede ser usado sólo por aquellos individuos autorizados por Migesa. El uso que se le dé a este sistema puede ser monitoreado de acuerdo a las políticas de Migesa.
                    </p>
                </div>
            </div>
            <asp:HiddenField ID="hdfip" runat="server" />
            <asp:HiddenField ID="hdfregion" runat="server" />
            <asp:HiddenField ID="hdflatitud" runat="server" />
            <asp:HiddenField ID="hdflongitud" runat="server" />
            <asp:HiddenField ID="hdfproveedor" runat="server" />
            <asp:HiddenField ID="hdfdevice" runat="server" />
            <asp:HiddenField ID="hdfos" runat="server" />
            <asp:HiddenField ID="hdfosversion" runat="server" />
            <asp:HiddenField ID="hdfmodel" runat="server" />
            <asp:HiddenField ID="hdfbrowser" runat="server" />
            <asp:HiddenField ID="hdffinger" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    
    <div class="modal fade  bs-example-modal-lg" id="modal_recuperar_contraseña" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog  modal-lg">
            <asp:UpdatePanel ID="dedeed" runat="server" >
                <Triggers> 
                    <asp:AsyncPostBackTrigger ControlID="lnkrecuperarcontraseña" EventName="Click" />
                </Triggers>
                <ContentTemplate>
                    
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Recuperar contraseña</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            Le enviaremos la información al correo electrónico relacionado a su cuenta. Si es empleado de la empresa,
                            el correo es el utilizado internamente, en caso de ser usuario externo, deberá ser el que proporcionó para su cuenta.
                            </div>
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            <h5><strong><i class="fa fa-edge" aria-hidden="true"></i>&nbsp;Correo electrónico</strong></h5>
                            <asp:TextBox ID="txtcorreo" TextMode="Email" CssClass=" form-control" runat="server"></asp:TextBox>
                        </div>

                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default pull-left" data-dismiss="modal">Cerrar</button>
                    <asp:LinkButton OnClientClick="return false;" ID="lnkcargando" CssClass="btn btn-primary btn-flat" runat="server" Style="display: none;">
                                            <i class="fa fa-refresh fa-spin fa-fw"></i>
                                            <span class="sr-only">Loading...</span>&nbsp;Enviando
                    </asp:LinkButton>
                    <asp:LinkButton ID="lnkguardar" CssClass="btn btn-primary btn-flat"
                        OnClientClick="return ConfirmwidgetProyectoModal();" OnClick="lnkguardar_Click" runat="server">
                                            <i class="fa fa-share" aria-hidden="true"></i>&nbsp;Recuperar contraseña
                    </asp:LinkButton>
                </div>
            </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
</asp:Content>