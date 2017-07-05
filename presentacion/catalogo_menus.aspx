<%@ Page Title="" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="catalogo_menus.aspx.cs" Inherits="presentacion.catalogo_menus" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <script type="text/javascript">
        $(document).ready(function () {
            Init();
        });
        function Init() {
            $('.dvv').DataTable({
                "paging": true,
                "lengthChange": true,
                "searching": true,
                "ordering": false,
                "info": true,
                "autoWidth": true,
                "language": {
                    "sProcessing": "Procesando...",
                    "sLengthMenu": "Mostrar _MENU_ registros",
                    "sZeroRecords": "No se encontraron resultados",
                    "sEmptyTable": "Ningún dato disponible en esta tabla",
                    "sInfo": "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros",
                    "sInfoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros",
                    "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
                    "sInfoPostFix": "",
                    "sSearch": "Buscar:",
                    "sUrl": "",
                    "sInfoThousands": ",",
                    "sLoadingRecords": "Cargando...",
                    "oPaginate": {
                        "sFirst": "Primero",
                        "sLast": "Último",
                        "sNext": "Siguiente",
                        "sPrevious": "Anterior"
                    },
                    "oAria": {
                        "sSortAscending": ": Activar para ordenar la columna de manera ascendente",
                        "sSortDescending": ": Activar para ordenar la columna de manera descendente"
                    }
                }
            });
        }

        function ConfirmEntregableDelete(msg) {
            if (confirm(msg)) {
                return ReturnPrompMsg(msg);
            } else {
                return false;
            }
        }
         function ReturnPrompMsg() {
            var motivo = prompt("Motivo de Eliminación", "");
            if (motivo != null) {
                if (motivo != '') {
                    var myHidden = document.getElementById('<%= hdfmotivos.ClientID %>');
                    myHidden.value = motivo;
                    return true;
                } else {
                    alert('ES NECESARIO EL MOTIVO DE LA ELIMINACIÓN.');
                    ReturnPrompMsg();
                    return false;
                }
            } else {
                return false;
            }

         }

         function ConfirmwidgetProyectoModal(msg) {
            if (confirm(msg)) {
                $("#<%= lnkcargando.ClientID%>").show();
                $("#<%= lnkguardar.ClientID%>").hide();
                return true;
            } else {
                return false;
            }
          }

          function OpenModalEditGrid(id_menu, command) {
          var myHidden = document.getElementById('<%= hdfid_menu.ClientID %>');

             myHidden.value = id_menu;

              var commando = document.getElementById('<%= hdfcommand.ClientID %>');

            commando.value = command;
            document.getElementById('<%= btneventgrid.ClientID%>').click();
            return false;
         }

          function showContentSubmenu() {
              element = document.getElementById("Datossubmenu");
              check = document.getElementById('<%= Chkmenupadre.ClientID%>');
              if (check.checked) {
                  element.style.display = 'none';
              }
              else {
                  element.style.display = 'block';
              }
          }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <h4 class="page-header">Catálogo de menús</h4>
        </div>
        
        <div class="col-lg-12">
            <asp:LinkButton ID="lnknuevomenu" OnClick="lnknuevomenu_Click" CssClass="btn btn-primary btn-flat" runat="server">
                Nuevo menú&nbsp;<i class="fa fa-plus" aria-hidden="true"></i>
            </asp:LinkButton>
            <div class="table table-responsive">
                <telerik:RadGrid ID="grid_menus" runat="server" Skin="Metro">
                    <MasterTableView AutoGenerateColumns="false" CssClass="dvv table table-responsive"
                        HeaderStyle-BackColor="White" HeaderStyle-ForeColor="Black"
                        Width="100%">
                        <Columns>
                            <telerik:GridTemplateColumn HeaderText="">
                                <HeaderStyle Width="50px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <a style="cursor: pointer;" onclick='<%# "return OpenModalEditGrid("+DataBinder.Eval(Container.DataItem, "id_menu").ToString()+@",""" +"actualizar"+@""""+");" %>'>
                                        <i class="fa fa-pencil fa-2x" aria-hidden="true"></i>
                                    </a>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="">
                                <HeaderStyle Width="50px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                   <asp:LinkButton ID="lnkcommand2" Visible="true" runat="server" CommandName="Eliminar"
                                        OnClientClick="return ConfirmEntregableDelete('¿Desea Eliminar este widget?')" OnClick="lnkcommand2_Click"
                                        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "id_menu").ToString() %>'>
                                         <i class="fa fa-trash fa-2x" aria-hidden="true"></i>
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>

                            <telerik:GridBoundColumn DataField="name" HeaderText="NombreMenu" UniqueName="name"
                                Visible="true">
                            </telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </div>


        </div>
    </div>
    <div class="modal fade bs-example-modal-lg" tabindex="-1" id="myModal" role="dialog" aria-labelledby="mySmallModalLabel" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg" role="document">
            <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="lnknuevomenu" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btneventgrid" EventName="Click" />
                </Triggers>
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">×</span></button>
                            <h4 class="modal-title">Menus</h4>
                        </div>
                        <div class="modal-body">
                            <div class="row" id="div_menu"  runat="server">
                                <div class="col-lg-12 col-sm-12">
                                    <asp:CheckBox ID="Chkmenupadre"  runat="server" onchange="showContentSubmenu()" Text="Es menu principal" />
                                </div>
                                <div class="col-lg-12 col-sm-12">
                                    <h5><strong><i class="fa fa-bars" aria-hidden="true"></i>&nbsp;Nombre del menu</strong></h5>
                                    <telerik:RadTextBox ID="rtxtmenu" Width="100%" runat="server" Skin="Bootstrap"></telerik:RadTextBox>
                                </div>
                                <div class="col-lg-12 col-sm-12">
                                    <h5><strong><i class="fa fa-paint-brush" aria-hidden="true"></i>&nbsp;Nombre de la clase del icono para el menu</strong>
                                        
                                     <span><a onclick="window.open('http://fontawesome.io/icons/ ')" class="btn btn-info btn-sm" role="button">Iconos para menus</a></span>
                                    </h5>
                                    <telerik:RadTextBox ID="rtxticono" Width="100%" runat="server" Skin="Bootstrap"></telerik:RadTextBox>
                                </div>
                                <div id="Datossubmenu">
                                    <div class="col-lg-12 col-sm-12">
                                        <h5><strong><i class="fa fa-internet-explorer" aria-hidden="true"></i>&nbsp;Url</strong></h5>
                                        <telerik:RadTextBox ID="rtxtUrl" Width="100%" runat="server" Skin="Bootstrap"></telerik:RadTextBox>
                                    </div>
                                    <div class="col-lg-4 col-md-6 col-sm-12">
                                        <h5><strong><i class="fa fa-bars" aria-hidden="true"></i>&nbsp;Menu principal</strong></h5>
                                        <asp:DropDownList ID="ddlmenupadre" CssClass="form-control" runat="server"></asp:DropDownList>
                                    </div>
                                </div>
                                
                                <div class="col-lg-12 col-sm-12">
                                    <asp:CheckBox ID="cbxmantenimiento" runat="server"  Text="En mantenimiento" />
                                </div>
                            </div>
                            
                            <asp:TextBox ID="txtid_menu" Visible="false" runat="server"></asp:TextBox>
                        </div>
                        <div class="modal-footer ">
                            <div class="row" id="div_error" runat="server" visible="false" style="text-align: left;">
                                <div class="col-lg-12">
                                    <div class="alert alert-danger alert-dismissible">
                                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                        <asp:Label ID="lblerror" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <button type="button" class="btn btn-default pull-left" data-dismiss="modal">Cerrar</button>
                            <asp:LinkButton OnClientClick="return false;" ID="lnkcargando" CssClass="btn btn-primary btn-flat" runat="server" Style="display: none;">
                                            <i class="fa fa-refresh fa-spin fa-fw"></i>
                                            <span class="sr-only">Loading...</span>&nbsp;Guardando...
                            </asp:LinkButton>
                              <asp:LinkButton ID="lnkguardar" CssClass="btn btn-primary btn-flat" OnClick="lnkguardar_Click"
                                OnClientClick="return ConfirmwidgetProyectoModal('¿Desea Guardar este menu?');" runat="server">
                                            <i class="fa fa-floppy-o" aria-hidden="true"></i>&nbsp;Guardar
                            </asp:LinkButton>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <asp:Button ID="btneventgrid" OnClick="btneventgrid_Click" runat="server" Text="Button" Style="display: none;" />
    <asp:HiddenField ID="hdfmotivos" runat="server" />
     <asp:HiddenField ID="hdfcommand" runat="server" />
     <asp:HiddenField ID="hdfid_menu" runat="server" />
</asp:Content>
