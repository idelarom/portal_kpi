<%@ Page Title="Recursos" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="proyectos_recursos.aspx.cs" Inherits="presentacion.proyectos_recursos" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">


        $(document).ready(function () {
            Init('.dvv');
        });
        function Init(value) {
            if ($.fn.dataTable.isDataTable(value)) {
                table = $(value).DataTable();
            }
            else {
                $(value).DataTable({
                    "paging": true,
                    "pageLength": 10,
                    "lengthChange": false,
                    "searching": true,
                    "ordering": true,
                    "info": true,
                    "autoWidth": false,
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

        }
        
        function ConfirmEmpleadoProyectoModal(msg) {
            if (confirm(msg)) {
                $("#<%= lnkcargarempleados.ClientID%>").show();
                $("#<%= lnkguardarempleado.ClientID%>").hide();
                return true;
            } else {
                return false;
            }
        }
        function ChangedTextLoad() {
            $("#<%= imgloadempleados.ClientID%>").show();
            $("#<%= lblbe.ClientID%>").show();
            return true;
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
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <h5 class="page-header">Administración de recursos(empleados)</h5>
        </div>
        <div class="col-lg-12">
            <div class="box box-danger">
                <div class="box-body with-border" style="">
                    <div class="row">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            <h5><strong><i class="fa fa-cubes" aria-hidden="true"></i>&nbsp;Proyecto</strong>
                                &nbsp;<asp:LinkButton ID="lnkdashboard" CssClass="btn btn-danger btn-xs btn-flat"
                                    OnClick="lnkdashboard_Click" runat="server">Dashboard</asp:LinkButton>
                            </h5>
                            <p class="text-muted">
                                <asp:Label ID="lblproyect" runat="server" Text="nombre del proyecto"></asp:Label>
                            </p>
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-12  col-xs-12">
                            <h5><strong><i class="fa fa-user" aria-hidden="true"></i>&nbsp;Tecnología(s)</strong></h5>
                            <p class="text-muted">
                                <asp:Label ID="lbltecnologia" runat="server" Text="Label"></asp:Label>
                            </p>
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-6  col-xs-12">
                            <h5><strong><i class="fa fa-tasks" aria-hidden="true"></i>&nbsp;CPED</strong></h5>
                            <p class="text-muted">
                                <asp:Label ID="lblcped" runat="server" Text="Label"></asp:Label>
                            </p>
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-6  col-xs-12">
                            <h5><strong><i class="fa fa-money" aria-hidden="true"></i>&nbsp;Monto</strong></h5>
                            <p class="text-muted">
                                <asp:Label ID="lblmonto" runat="server" Text="Label"></asp:Label>
                            </p>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="col-lg-12">
            <div class="box box-danger">
                <div class="box-body with-border" style="">
                    <div class="row">
                        <div class="col-lg-12">
                            <asp:LinkButton OnClick="lnkagregarempleadoaproyecto_Click" ID="lnkagregarempleadoaproyecto" CssClass="btn btn-danger btn-flat btn-sm" runat="server">
                                        Agregar recurso&nbsp;<i class="fa fa-plus" aria-hidden="true"></i>
                            </asp:LinkButton>
                            <div class="table table-responsive">
                                <table class="dvv table table-responsive table-bordered table-condensed">
                                    <thead>
                                        <tr>
                                            <th style="min-width: 20px"></th>
                                            <th style="min-width: 80px">Usuario</th>
                                            <th style="min-width: 250px">Empleado</th>
                                            <th style="min-width: 40px; text-align: center;">PM</th>
                                            <th style="min-width: 100px">Agregado por</th>
                                            <th style="min-width: 150px">Agregado desde</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="repeat_proyectos_empleados" runat="server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td style="text-align: center;">
                                                        <asp:LinkButton OnClick="lnkeliminarempleadoproyecto_Click" ID="lnkeliminarempleadoproyecto" runat="server" CommandName="Delete"
                                                             CssClass="btn btn-danger btn-flat" OnClientClick="return ConfirmEntregableDelete('¿Desea Eliminar este recurso del proyecto?');"
                                                            CommandArgument='<%# Eval("id_proyectoe") %>' Visible='<%# !Eval("usuario").ToString().ToUpper().Equals(Eval("usuario_resp").ToString().ToUpper()) %>'>
                                                                     <i class="fa fa-trash" aria-hidden="true"></i>
                                                        </asp:LinkButton>
                                                    </td>
                                                    <td style="text-align: left;"><%# Eval("usuario").ToString().ToUpper() %></td>
                                                    <td style="text-align: left;"><%# System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(Eval("nombre").ToString().ToLower()) %></td>
                                                    <td style="text-align: center;">
                                                        <asp:CheckBox ID="CheckBox1"
                                                            Checked='<%# Convert.ToBoolean(Eval("administrador_proyecto"))  %>'
                                                            CssClass=" form-control" Enabled="false" runat="server" />
                                                    </td>
                                                    <td style="text-align: left;"><%# Eval("usuario_registro").ToString().ToUpper() %></td>
                                                    <td style="text-align: left;"><%# Convert.ToDateTime(Eval("fecha_registro")).ToString("dd MMMM yyyy", System.Globalization.CultureInfo.CreateSpecificCulture("es-MX")) %></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </div>



    <div class="modal fade bs-example-modal-lg" tabindex="-1" id="myModalEmpleados" role="dialog" aria-labelledby="mySmallModalLabel" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg" role="document">
            <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnbuscarempleado" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="lnkagregarempleadoaproyecto" EventName="Click" />
                </Triggers>
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">×</span></button>
                            <h4 class="modal-title">Empleados</h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-lg-4 col-md-6 col-sm-6 col-xs-12">
                                    <h5><strong><i class="fa fa-user" aria-hidden="true"></i>&nbsp;Seleccione uno o mas empleados</strong></h5>
                                    <div style="text-align: left;" class="input-group input-group-sm">
                                        <asp:TextBox ID="txtbuscarempleadoproyecto" CssClass="form-control" placeholder="Buscar"
                                            runat="server"></asp:TextBox>
                                        <span class="input-group-btn">
                                            <asp:LinkButton ID="btnbuscarempleado" CssClass="btn btn-primary btn-flat" runat="server" OnClientClick="ChangedTextLoad();" OnClick="btnbuscarempleado_Click">
                                                <i class="fa fa-search" aria-hidden="true"></i>
                                            </asp:LinkButton>
                                        </span>
                                    </div>

                                    <asp:Image ID="imgloadempleados" Style="display: none;" ImageUrl="~/img/load.gif" runat="server" />
                                    <label id="lblbe" runat="server" style="display: none; color: #1565c0">Buscando Empleados</label>
                                </div>
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">

                                    <div style="height: 200px; overflow: scroll;">

                                        <telerik:RadListBox RenderMode="Lightweight" runat="server" ID="rdlempleadosproyecto" Style="font-size: 11px" Width="100%"
                                            Skin="Bootstrap" SelectionMode="Multiple">
                                        </telerik:RadListBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer ">
                            <button type="button" class="btn btn-default pull-left" data-dismiss="modal">Cerrar</button>
                            <asp:LinkButton OnClientClick="return false;" ID="lnkcargarempleados" CssClass="btn btn-primary btn-flat" runat="server" Style="display: none;">
                                            <i class="fa fa-refresh fa-spin fa-fw"></i>
                                            <span class="sr-only">Loading...</span>&nbsp;Guardando Configuración...
                            </asp:LinkButton>
                            <asp:LinkButton ID="lnkguardarempleado" CssClass="btn btn-primary btn-flat" OnClick="lnkguardarempleado_Click"
                                OnClientClick="return ConfirmEmpleadoProyectoModal('¿Desea Guardar esta configuración del Proyecto?');" runat="server">
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
