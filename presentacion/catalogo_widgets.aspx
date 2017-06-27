<%@ Page Title="" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="catalogo_widgets.aspx.cs" Inherits="presentacion.catalogo_widgets" %>

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
<%-- 

          function ChangedTextLoad2() {
            $("#<%= imgloadempleado_.ClientID%>").show();
             $("#<%= lblbe2.ClientID%>").show();
             return true;
         }--%>
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div class="row">
        <div class="col-lg-12">
            <h4 class="page-header">Catálogo de Widgets</h4>
        </div>
        
        <div class="col-lg-12">
            <asp:LinkButton ID="lnknuevowidget" OnClick="lnknuevowidget_Click" CssClass="btn btn-primary btn-flat" runat="server">
                Nuevo widget&nbsp;<i class="fa fa-plus" aria-hidden="true"></i>
            </asp:LinkButton>
            <div class="table table-responsive">
                <telerik:RadGrid ID="grid_widgets" runat="server" Skin="Metro">
                    <MasterTableView AutoGenerateColumns="false" CssClass="dvv table table-responsive"
                        HeaderStyle-BackColor="White" HeaderStyle-ForeColor="Black"
                        Width="100%">
                        <Columns>
                            <telerik:GridTemplateColumn HeaderText="">
                                <HeaderStyle Width="50px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkcommand" runat="server" CommandName="Editar" 
                                        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "id_widget").ToString() %>'>
                                              <i class="fa fa-pencil fa-2x" aria-hidden="true"></i>
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="">
                                <HeaderStyle Width="50px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkcommand2" Visible="true"  runat="server" CommandName="Eliminar" 
                                        OnClientClick="return ConfirmEntregableDelete('¿Desea Eliminar este widget?')"
                                        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "id_widget").ToString() %>'>
                                         <i class="fa fa-trash fa-2x" aria-hidden="true"></i>
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>

                            <telerik:GridBoundColumn DataField="widget" HeaderText="widget" UniqueName="widget"
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
                    <asp:AsyncPostBackTrigger ControlID="lnknuevowidget" EventName="Click" />
                </Triggers>
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">×</span></button>
                            <h4 class="modal-title">Widgets</h4>
                        </div>
                        <div class="modal-body">
                            <div class="row" id="div_widget"  runat="server">
                                <div class="col-lg-12 col-sm-12">
                                    <h5><strong><i class="fa fa-cog" aria-hidden="true"></i>&nbsp;Nombre del widget</strong></h5>
                                    <telerik:RadTextBox ID="rtxtwidget" Width="100%" runat="server" Skin="Bootstrap"></telerik:RadTextBox>
                                </div>
                                 <div class="col-lg-12 col-sm-12">
                                    <h5><strong><i class="fa fa-paint-brush" aria-hidden="true"></i>&nbsp;Nombre de la clase del icono para el widget</strong></h5>
                                     <span><a onclick="window.open('http://ionicons.com/')" class="btn btn-info" role="button">Iconos para widgets</a></span>
                                    <telerik:RadTextBox ID="rtxticono" Width="100%" runat="server" Skin="Bootstrap"></telerik:RadTextBox>
                                </div>
                                
                            </div>
                            
                                <%--<div class="row" id="div_empleados" runat="server">

                                    <div class="col-lg-12 col-xs-12">
                                        <h5><strong><i class="fa fa-user" aria-hidden="true"></i>&nbsp;Seleccione los usuarios (empleados) a relacionar</strong></h5>
                                        <div style="text-align: left;" class="input-group input-group-sm">
                                            <asp:TextBox ID="txtbuscarempleado" CssClass="form-control" placeholder="Buscar" runat="server"></asp:TextBox>
                                            <span class="input-group-btn">
                                                <asp:LinkButton ID="btnbuscarempleado2" CssClass="btn btn-primary btn-flat" runat="server"
                                                    OnClientClick="return ChangedTextLoad2();" OnClick="btnbuscarempleado2_Click">
                                                <i class="fa fa-search" aria-hidden="true"></i>
                                                </asp:LinkButton>
                                            </span>
                                        </div>

                                        <asp:Image ID="imgloadempleado_" Style="display: none;" ImageUrl="~/img/load.gif" runat="server" />
                                        <label id="lblbe2" runat="server" style="display: none; color: #1565c0">Buscando Empleados</label>
                                    </div>

                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                        <div style="height: 150px; min-width:500px; overflow: scroll;">
                                            <asp:Repeater ID="rdllista_empleados" runat="server">
                                                <ItemTemplate>
                                                    <asp:UpdatePanel ID="jajaja" runat="server" UpdateMode="Always">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="mycheck" EventName="CheckedChanged" />
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <asp:CheckBox ID="mycheck" Text='<%# Eval("nombre_usuario").ToString()  %>'
                                                                ToolTip='<%# Eval("Usuario_Red").ToString()  %>' OnCheckedChanged="mycheck_CheckedChanged"
                                                                runat="server"
                                                                AutoPostBack="true"></asp:CheckBox>
                                                            <br />
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>
                                    </div>
                                </div>--%>
                            <asp:TextBox ID="txtid_widget" Visible="false" runat="server"></asp:TextBox>
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
                                OnClientClick="return ConfirmwidgetProyectoModal('¿Desea Guardar este widget?');" runat="server">
                                            <i class="fa fa-floppy-o" aria-hidden="true"></i>&nbsp;Guardar
                            </asp:LinkButton>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <asp:HiddenField ID="hdfmotivos" runat="server" />
</asp:Content>
