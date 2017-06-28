<%@ Page Title="Configuracion Dashboard" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="configuracion_dashboard.aspx.cs" Inherits="presentacion.configuracion_dashboard" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
           function ConfirmEmpleadoProyectoModal(msg) {
            if (confirm(msg)) {
                $("#<%= lnkcargando.ClientID%>").show();
                $("#<%= lnkguardar.ClientID%>").hide();
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
            <h4 class="page-header">Configuración de Dashboard y Widgets
                <small> Puede configurar los widgets que quiera visualizar y el orden en el que los quiera visualizar</small>
            </h4>

        </div>
        
        <div class="col-lg-12">
            <asp:UpdatePanel ID="UPDA" runat="server">
                <ContentTemplate>
                    <div class="box box-danger">
                        <div class="box-header ui-sortable-handle" style="cursor: move;">
                            <i class="fa fa-window-restore" aria-hidden="true"></i>
                            <h3 class="box-title">Widgets</h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <div class="row">
                                <div class="col-lg-6 col-md-6 col-sm-12">
                                    <h5><strong><i class="fa fa-list" aria-hidden="true"></i>&nbsp;Widgets disponibles para mi perfil</strong></h5>
                                    <telerik:RadListBox RenderMode="Lightweight" runat="server" ID="rdl_widgets_permitidos" Height="300px"
                                        Width="100%" AllowTransferDuplicates="false" AllowTransferOnDoubleClick="true"
                                        AllowTransfer="true" TransferToID="rdl_widgets_actuales" TransferMode="Copy"
                                        ButtonSettings-AreaWidth="35px" Skin="Bootstrap" EnableDragAndDrop="true">                                      
                                    </telerik:RadListBox>
                                </div>
                                <div class="col-lg-6 col-md-6 col-sm-12">

                                    <h5><strong><i class="fa fa-list" aria-hidden="true"></i>&nbsp;Mis Widgets</strong></h5>
                                    <telerik:RadListBox RenderMode="Lightweight" AllowDelete="true" AllowReorder="true" runat="server" ID="rdl_widgets_actuales" Height="300px" Width="100%"
                                        ButtonSettings-AreaWidth="35px" Skin="Bootstrap" SelectionMode="Multiple"  EnableDragAndDrop="true">
                                          
                                    </telerik:RadListBox>
                                </div>
                            </div>

                        </div>
                        <div class="box-footer clearfix no-border">
                              <asp:LinkButton OnClientClick="return false;" ID="lnkcargando" CssClass="pull-right btn btn-primary btn-flat" runat="server" Style="display: none;">
                                            <i class="fa fa-refresh fa-spin fa-fw"></i>
                                            <span class="sr-only">Loading...</span>&nbsp;Guardando
                                </asp:LinkButton>
                                <asp:LinkButton ID="lnkguardar" CssClass="pull-right btn btn-primary btn-flat" OnClick="lnkguardar_Click"
                                    OnClientClick="return ConfirmEmpleadoProyectoModal('¿Desea Guardar esta Configuración?');" runat="server">
                                            <i class="fa fa-floppy-o" aria-hidden="true"></i>&nbsp;Guardar
                                </asp:LinkButton>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <asp:HiddenField ID="hdfusuario" runat="server" />
</asp:Content>
