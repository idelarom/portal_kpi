<%@ Page Title="Dashboard Proyectos" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="proyectos_dashboard.aspx.cs" Inherits="presentacion.proyectos_dashboard" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function GoRiesgos() {
            document.getElementById('<%= lnkgo_riesgos.ClientID%>').click();
            return true;
        }
         function ConfirmwidgetProyectoModal(msg) {
            if (confirm(msg)) {
                $("#<%= LinkButton2.ClientID%>").show();
                $("#<%= lnkguardarhistorial.ClientID%>").hide();
                return true;
            } else {
                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <h4 class="page-header">
                <i class="fa fa-cubes" aria-hidden="true"></i>&nbsp;<asp:Label ID="lblproyect" runat="server" Text="Mi Proyecto"></asp:Label></h4>
        </div>
        <div class="col-lg-12">
            <div class="box box-danger">
                <div class="box-header with-border">
                    <h3 class="box-title">Información general</h3>

                    <div class="box-tools pull-right">
                        <button type="button" class="btn btn-box-tool" data-widget="collapse">
                            <i class="fa fa-minus"></i>
                        </button>
                    </div>
                </div>
                <div class="box-body" style="">
                    <div class="row">
                        <div class="col-lg-12 col-md-12 col-sm-12  col-xs-12">
                            <h5><strong><i class="fa fa-user" aria-hidden="true"></i>&nbsp;Tecnología</strong></h5>
                            <p class="text-muted">
                                <asp:Label ID="lbltecnologia" runat="server" Text="Label"></asp:Label>
                            </p>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-4">
                            <h5><strong><i class="fa fa-briefcase" aria-hidden="true"></i>&nbsp;Estatus</strong></h5>
                            <p class="text-muted">
                                <asp:Label ID="lblestatus" runat="server" Text="Label"></asp:Label>
                            </p>
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-4  col-xs-8">
                            <h5><strong><i class="fa fa-calendar-o" aria-hidden="true"></i>&nbsp;Periodo de evaluación</strong></h5>
                            <p class="text-muted">
                                <asp:Label ID="lblperiodo" runat="server" Text="Label"></asp:Label>
                            </p>
                        </div>
                        <div class="col-lg-5 col-md-5 col-sm-5  col-xs-12">
                            <h5><strong><i class="fa fa-user" aria-hidden="true"></i>&nbsp;Administrador del proyecto</strong></h5>
                            <p class="text-muted">
                                <asp:Label ID="lblempleado" runat="server" Text="Label"></asp:Label>
                            </p>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-3  col-xs-12">
                            <h5><strong><i class="fa fa-tasks" aria-hidden="true"></i>&nbsp;CPED</strong></h5>
                            <p class="text-muted">
                                <asp:Label ID="lblcped" runat="server" Text="Label"></asp:Label>
                            </p>
                        </div>
                        <div class="col-lg-6 col-md-6 col-sm-6  col-xs-12">
                            <h5><strong><i class="fa fa-money" aria-hidden="true"></i>&nbsp;Monto</strong></h5>
                            <p class="text-muted">
                                <asp:Label ID="lblmonto" runat="server" Text="Label"></asp:Label>
                            </p>
                        </div>
                    </div>
                </div>
                <div class=" box-footer">
                    <asp:LinkButton ID="lnkterminarproyecto" OnClick="lnkterminarproyecto_Click" CssClass="btn btn-danger btn-flat pull-right" runat="server">
                        Cerrar proyecto&nbsp;<i class="fa fa-handshake-o" aria-hidden="true"></i>
                    </asp:LinkButton>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
            <!-- small box -->
            <div class="small-box bg-red" onclick="return GoRiesgos();" style="cursor: pointer;">
                <div class="inner">
                    <h3>
                        <asp:Label ID="lblnumriesgos" runat="server" Text="0"></asp:Label></h3>

                    <p>Riesgos abiertos</p>
                </div>
                <div class="icon">
                    <i class="ion ion-android-warning"></i>
                </div>
                <asp:LinkButton ID="lnkgo_riesgos" OnClick="lnkgo_riesgos_Click" runat="server" CssClass="small-box-footer">
                  
              Evaluaciones&nbsp;<i class="fa fa-arrow-circle-right"></i>
                </asp:LinkButton>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hdfid_proyecto" runat="server" />

    <div class="modal fade bs-example-modal-lg" tabindex="-1" id="modal_terminacion" role="dialog" aria-labelledby="mySmallModalLabel" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg" role="document">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="lnkterminarproyecto" EventName="Click" />
                </Triggers>
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">×</span></button>
                            <h4 class="modal-title">Cierre de proyecto</h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <h5><strong><i class="fa fa-file-archive-o" aria-hidden="true"></i>&nbsp;Documento de cierre</strong></h5>
                                    <telerik:RadAsyncUpload RenderMode="Lightweight" ID="AsyncUpload1" runat="server"
                                        OnFileUploaded="AsyncUpload1_FileUploaded" PostbackTriggers="lnkguardarhistorial"
                                        MaxFileSize="2097152" Width="100%"
                                        AutoAddFileInputs="false" Localization-Select="Seleccionar" Skin="Bootstrap" />
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default pull-left" data-dismiss="modal">Cerrar</button>
                            <asp:LinkButton OnClientClick="return false;" ID="LinkButton2" CssClass="btn btn-primary btn-flat" runat="server" Style="display: none;">
                                            <i class="fa fa-refresh fa-spin fa-fw"></i>
                                            <span class="sr-only">Loading...</span>&nbsp;Terminando
                            </asp:LinkButton>
                            <asp:LinkButton ID="lnkguardarhistorial"
                                OnClientClick="return ConfirmwidgetProyectoModal('¿Desea terminar este proyecto?');"
                                OnClick="lnkguardarhistorial_Click" CssClass="btn btn-primary btn-flat pull-right" runat="server">
                                            Terminar proyecto
                            </asp:LinkButton>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
