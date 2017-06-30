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
            return false;
          }
        function onDoubleClic(sender, e) {            
             var myHidden = document.getElementById('<%= hdfid_widget.ClientID %>');

            myHidden.value = e.get_item().get_value();
            document.getElementById('<%= btnview_html.ClientID%>').click();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <h4 class="page-header">Configuración de Dashboard
                <small>Puede configurar los widgets que quiera visualizar y el orden en el que los quiera visualizar</small>
            </h4>

        </div>
       
        <div class="col-lg-12">
            <asp:UpdatePanel ID="UPDA" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="rdl_widgets_permitidos" EventName="Transferred" />
                </Triggers>
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
                                    <telerik:RadListBox RenderMode="Lightweight" runat="server" ID="rdl_widgets_permitidos" Height="200px"
                                        Width="100%" AllowTransferDuplicates="false"
                                        AllowTransfer="true" TransferToID="rdl_widgets_actuales" TransferMode="Move"
                                        ButtonSettings-AreaWidth="35px" Skin="Bootstrap" EnableDragAndDrop="true"
                                           OnClientItemDoubleClicking="onDoubleClic">
                                    </telerik:RadListBox>
                                </div>
                                <div class="col-lg-6 col-md-6 col-sm-12">

                                    <h5><strong><i class="fa fa-list" aria-hidden="true"></i>&nbsp;Mis Widgets</strong></h5>
                                    <telerik:RadListBox RenderMode="Lightweight" AllowDelete="true" AllowReorder="true" runat="server"
                                        ID="rdl_widgets_actuales" Height="200px" Width="100%"
                                        ButtonSettings-AreaWidth="35px" Skin="Bootstrap" SelectionMode="Multiple" EnableDragAndDrop="true"
                                         OnClientItemDoubleClicking="onDoubleClic">
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

      <div class="modal fade bs-example-modal-lg" tabindex="-1" id="myModal" role="dialog" aria-labelledby="mySmallModalLabel" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg" role="document">
            <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnview_html" EventName="Click" />
                </Triggers>
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">×</span></button>
                            <h4 class="modal-title">Widgets</h4>
                        </div>
                        <div class="modal-body">
                            <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                        </div>
                        <div class="modal-footer ">
                            <button type="button" class="btn btn-default pull-left" data-dismiss="modal">Cerrar</button>
                       
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <asp:Button ID="btnview_html" runat="server" Text="Button" OnClick="btnview_html_Click" />
    <asp:HiddenField ID="hdfid_widget" runat="server" />
    <asp:HiddenField ID="hdfusuario" runat="server" />
</asp:Content>
