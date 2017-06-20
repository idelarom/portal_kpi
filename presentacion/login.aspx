<%@ Page Title="" Language="C#" MasterPageFile="~/Login.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="presentacion.login" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .login-box, .register-box {
            width: 360px;
            margin: 10px auto;
        }

        .login-logo, .register-logo {
            font-size: 35px;
            text-align: center;
            margin-bottom: 25px;
            font-weight: 300;
            padding-left: 5px;
        }
    </style>
    <script type="text/javascript">
        function ConfirmMinutaModal() {
            $("#<%= lnkiniciandosession.ClientID%>").show();
            $("#<%= btniniciar.ClientID%>").hide();
            return true;
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
                                            <span class="sr-only">Loading...</span>&nbsp;Iniciando Sesión</asp:LinkButton>
                    <asp:Button ID="btniniciar" runat="server" Text="Iniciar Sesión" CssClass="btn btn-danger" OnClick="btniniciar_Click" OnClientClick="return ConfirmMinutaModal();" />
                </div>

                <div class="col-xs-12" runat="server" id="div_cambiodomiinio" visible="true">
                    <br />
                    <p>
                        ¿No puedes Iniciar Sesión?
                    <asp:LinkButton ID="lnkcambiardominio" runat="server" OnClick="lnkcambiardominio_Click">
                        Puedes Cambiar el Dominio de Inicio
                    </asp:LinkButton>
                    </p>
                </div>

                <div class="col-xs-12" runat="server" id="div_portalclientes" visible="false">
                    <br />
                    <p>
                        ¿No puedes Iniciar Sesión?
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>