<%@ Page Title="Configuración" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="configuracion.aspx.cs" Inherits="presentacion.configuracion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        a:hover, a:active, a:focus {
            outline: none;
            text-decoration: none;
            color: black;
        }
        a {
            color: black;
        }
        .fa-title {
            width:20px;
        }
    </style>
    <script type="text/javascript">
        function ConfirmEmpleadoProyectoModal(msg) {
            if (confirm(msg)) {
                $("#<%= lnkcargandoguardarnombre.ClientID%>").show();
                $("#<%= lnkguardarnombre.ClientID%>").hide();
                return true;
            } else {
                return false;
            }
        }
        function Loadimagenuser(msg) {
            if (confirm(msg)) {
                $("#<%= lnkloadimagen.ClientID%>").show();
                $("#<%= lnksubirimagen.ClientID%>").hide();
                return true;
            } else {
                return false;
            }
        }
        function LoadSincronizacion(msg) {
            if (confirm(msg)) {
                $("#<%= lnkloadsinc.ClientID%>").show();
                $("#<%= lnksincronizar.ClientID%>").hide();
                return true;
            } else {
                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="row">
    <div class="col-lg-12"><h4 class="page-header">Configuración de mi cuenta</h4></div>
        <div class="col-lg-12">
            <div class="box box-solid">
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="box-group" id="configuracion">
                        <div class="panel box box-danger">
                            <div class="box-header with-border">
                                <h4 class="box-title">
                                    <a data-toggle="collapse" data-parent="#configuracion" href="#div_general" aria-expanded="false" class="collapsed">
                                        <i class="fa fa-user-circle fa-title" aria-hidden="true" ></i>&nbsp;General
                                    </a>
                                </h4>
                            </div>
                            <div id="div_general" class="panel-collapse collapse" aria-expanded="false" style="height: 0px;">
                                <div class="box-body">
                                    <div class="row">
                                        <div class="col-lg-6 col-xs-12">
                                            <h5><i class="fa fa-user" aria-hidden="true"></i>&nbsp;Puede cambiar su nombre en el portal.</h5>
                                            <div class="input-group">
                                                <asp:TextBox ID="txtnombre" onfocus="this.select();" CssClass="form-control" runat="server"></asp:TextBox>
                                                <span class="input-group-btn">
                                                    <asp:LinkButton OnClientClick="return false;" ID="lnkcargandoguardarnombre" CssClass="btn btn-danger btn-flat" runat="server" Style="display: none;">
                                                            <i class="fa fa-refresh fa-spin fa-fw"></i>
                                                            <span class="sr-only">Loading...</span>&nbsp;Guardando
                                                    </asp:LinkButton>
                                                    <asp:LinkButton ID="lnkguardarnombre" CssClass="btn btn-danger btn-flat" OnClick="lnkguardarnombre_Click"
                                                        OnClientClick="return ConfirmEmpleadoProyectoModal('¿Desea guardar esta configuración?');" runat="server">
                                                            <i class="fa fa-floppy-o" aria-hidden="true"></i>&nbsp;Guardar
                                                    </asp:LinkButton>
                                                </span>
                                            </div>
                                        </div>
                                        <div class="col-lg-6 col-xs-12">
                                            <h5><i class="fa fa-address-card-o" aria-hidden="true"></i>&nbsp;Puede cambiar la imagen de usuario</h5>
                                            <div class="input-group">
                                                <asp:FileUpload ID="fupfotoperfil" CssClass="form-control" onchange="return ValidateUF(this,2);" runat="server" />
                                                <span class="input-group-btn">
                                                    <asp:LinkButton OnClientClick="return false;" ID="lnkloadimagen" CssClass="btn btn-primary btn-flat" runat="server" Style="display: none;">
                                                            <i class="fa fa-refresh fa-spin fa-fw"></i>
                                                            <span class="sr-only">Loading...</span>&nbsp;Guardando
                                                    </asp:LinkButton>
                                                    <asp:LinkButton ID="lnksubirimagen" CssClass="btn btn-primary btn-flat" OnClick="lnksubirimagen_Click"
                                                        OnClientClick="return Loadimagenuser('¿Desea guardar esta imagen como foto de usuario?');" runat="server">
                                                            <i class="fa fa-floppy-o" aria-hidden="true"></i>&nbsp;Guardar
                                                    </asp:LinkButton>
                                                </span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="panel box box-danger">
                            <div class="box-header with-border">
                                <h4 class="box-title">
                                    <a data-toggle="collapse" data-parent="#configuracion" href="#div_seguridad" aria-expanded="false" class="collapsed">
                                       <i class="fa fa-unlock-alt   fa-title" aria-hidden="true"></i>&nbsp;Seguridad e inicio de sesión
                                    </a>
                                </h4>
                            </div>
                            <div id="div_seguridad" class="panel-collapse collapse" aria-expanded="false" style="height: 0px;">
                                <div class="box-body">
                                   <asp:UpdatePanel ID="up_seguridad" runat ="server">
                                       <ContentTemplate>
                                           <div class="row">
                                               <div class="col-lg-12">
                                                   <a class="btn btn-danger btn-flat" href="catalogo_sesiones_usuario.aspx">
                                                       <i class="fa fa-id-badge" aria-hidden="true"></i>&nbsp;Ver historial de inicios de sesión
                                                   </a>
                                               </div>
                                               <div class="col-lg-12">
                                                   <blockquote>
                                                       <p><i class="fa fa-bell" aria-hidden="true"></i>&nbsp;Recibir alertas sobre inicios de sesión no reconocidos.</p>
                                                       <small><cite title="Source Title">Te avisaremos si alguien inicia sesión desde un dispositivo o navegador que no usas con frecuencia.</cite>
                                                           <br />
                                                           <asp:CheckBox AutoPostBack="true" OnCheckedChanged="cbxalerta_inicio_sesion_CheckedChanged" ID="cbxalerta_inicio_sesion" Text="Recibir alertas" runat="server" />
                                                       </small>
                                                   </blockquote>
                                               </div>
                                           </div>
                                       </ContentTemplate>
                                   </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                        <div class="panel box box-danger">

                            <div class="box-header with-border">
                                <h4 class="box-title">
                                    <a data-toggle="collapse" data-parent="#configuracion" href="#div_record" aria-expanded="false" class="collapsed">
                                        <i class="fa fa-users  fa-title" aria-hidden="true"></i>&nbsp;Reuniones y recordatorios
                                    </a>
                                </h4>
                            </div>
                            <div id="div_record" class="panel-collapse collapse" aria-expanded="false" style="height: 0px;">
                                <div class="box-body">
                                    <asp:UpdatePanel ID="up_sinc" runat="server">
                                        <ContentTemplate>
                                            <div class="row">
                                                <div class="col-lg-12" style="display:none;">
                                                    <asp:LinkButton ID="lnkloadsinc" runat="server" CssClass="btn btn-primary btn-flat"
                                                        OnClientClick="return false;" Style="display: none;">
                                                           <i class="fa fa-refresh fa-spin fa-fw"></i>
                                                        <span class="sr-only">Loading...</span>&nbsp;Sincronizando</asp:LinkButton>
                                                    <asp:LinkButton ID="lnksincronizar" runat="server" CssClass="btn btn-primary btn-flat"
                                                        OnClientClick="return LoadSincronizacion('¿Desea sincronizar con el servidor Exchange de Migesa?');" OnClick="lnksincronizar_Click">
                                                            <i class="fa fa-refresh"></i>&nbsp;Sincronizar con Exchange </asp:LinkButton>
                                                 <br />
                                                           <asp:CheckBox AutoPostBack="true" OnCheckedChanged="cbxsincronizacion_CheckedChanged" ID="cbxsincronizacion" 
                                                               Text="Sincronizar al iniciar sesión" runat="server" />
                                                     <p class="text-red">Activar esta opción podria hacer que el inicio de sesión se demore unos segundos más.</p>
                                                </div>
                                               <div class="col-lg-12">
                                                   <blockquote>
                                                       <p><i class="fa fa-bell" aria-hidden="true"></i>&nbsp;Recibir alertas sobre reuninones y recordatorios.</p>
                                                       <small><cite title="Source Title">Podras recibir alertas hasta 15 minutos antes de que la reunion o recordatorio comienzen.</cite>
                                                           <br />
                                                           <asp:CheckBox AutoPostBack="true" OnCheckedChanged="cbxrecordatorios_CheckedChanged" ID="cbxrecordatorios" 
                                                               Text="Recibir alertas" runat="server" />
                                                       </small>
                                                   </blockquote>
                                               </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                    </div>
                    
                </div>
                <!-- /.box-body -->
            </div>
            <!-- /.box -->
        </div>
</div>
</asp:Content>
