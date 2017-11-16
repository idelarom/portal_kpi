<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/MP/Global.Master" AutoEventWireup="true" CodeBehind="catalogo_permisos_usuarios_bonos.aspx.cs" Inherits="presentacion.Pages.Catalogs.catalogo_permisos_usuarios_bonos" %>
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
        function ConfirmEntregableDelete(login) {
            if (confirm('¿Desea eliminar este usuario?')) {
                var hdfusuario = document.getElementById('<%= hdflogin.ClientID %>');
                hdfusuario.value = login;
                document.getElementById('<%= btneliminar.ClientID%>').click();
                return true;
            } else {
                return false;
            }
        }
        function EditarClick(login) {
            var hdfusuario = document.getElementById('<%= hdflogin.ClientID %>');
            hdfusuario.value = login;
            document.getElementById('<%= btneventgrid.ClientID%>').click();
            return false;
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
        function ChangedTextLoad2() {
            $("#<%= imgloadempleado.ClientID%>").show();
            $("#<%= lblbemp.ClientID%>").show();
            return true;
         }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-lg-12">
          <h4 class="page-header">Catálogo Permisos de usuario</h4>          
        </div>
    </div>
    <div class="row">
        <div class="col-lg-12">
            <div class="box box-danger">
                <div class="box-body"><asp:LinkButton ID="lnknuevoPermiso" CssClass="btn btn-primary btn-flat" runat="server" OnClick="lnknuevoPermiso_Click">
                Nuevo Permiso &nbsp;<i class="fa fa-plus" aria-hidden="true"></i>
            </asp:LinkButton>
                    <div class="table-responsive">
                        <table class="dvv table no-margin table-condensed">
                            <thead>
                                <tr style="font-size: 12px;">
                                    <th style="width: 20px; text-align: center;" scope="col"></th>
                                    <th style="width: 20px; text-align: center;" scope="col"></th>
                                    <th style=" min-width:80px; text-align: left;" scope="col">Uruario de red</th>
                                    <th style=" min-width:80px; text-align: left;" scope="col">Empleado</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="repeat_permissions_users_bonds_types" runat="server">
                                    <ItemTemplate>
                                        <tr style="font-size: 12px">
                                            <td style="text-align: center;">
                                                <a style="cursor: pointer;"
                                                    onclick='<%# "return EditarClick("+@""""+Eval("login") + @"""" +");" %>'>
                                                    <i class="fa fa-pencil fa-2x" aria-hidden="true"></i>
                                                </a>
                                            </td>
                                            <td style="text-align: center;">
                                                <a style="cursor: pointer;"
                                                    onclick='<%# "return ConfirmEntregableDelete("+@""""+Eval("login") + @"""" +");" %>'>
                                                    <i class="fa fa-trash fa-2x" aria-hidden="true"></i>
                                                </a>
                                            </td>
                                            <td style="text-align: left;"><%# Eval("login") %></td>
                                             <td style="text-align: left;"><%# Eval("empleado") %></td>
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
       <div class="modal fade bs-example-modal-lg" tabindex="-1" id="ModalPermisosBonos" role="dialog" aria-labelledby="mySmallModalLabel" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg" role="document">
            <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="lnknuevoPermiso" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btneventgrid" EventName="Click" />
                    <asp:PostBackTrigger ControlID="lnkguardar" />
                </Triggers>
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">×</span></button>
                            <h4 class="modal-title">Detalles delos permisos</h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                 <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                                    <h5><strong><i class="fa fa-play-circle-o" aria-hidden="true"></i>&nbsp;Tipos de bonos a solicitar</strong></h5>
                                    <telerik:RadComboBox RenderMode="Lightweight" ID="ddlBonosSolicitud" Width="100%" MaxLength="250" runat="server"
                                        CheckBoxes="True" Skin="Bootstrap">
                                    </telerik:RadComboBox>
                                </div>
                                 <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                                    <h5><strong><i class="fa fa-play-circle-o" aria-hidden="true"></i>&nbsp;Tipos de bonos a autorizar</strong></h5>
                                    <telerik:RadComboBox RenderMode="Lightweight" ID="ddlBonosAutorizacion" Width="100%" MaxLength="250" runat="server"
                                        CheckBoxes="True" Skin="Bootstrap">
                                    </telerik:RadComboBox>
                                </div>
                                 <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                                     <asp:CheckBox ID="chkCC" runat="server" Text="Filtro CC" />
                                 </div>                                 
                            </div>
                            <div class="row">
                                <div class="col-lg-6 col-md-6 col-sm-12 col-xs-122">
                                    <h5><strong><i class="fa fa-users" aria-hidden="true"></i>&nbsp;Empleado</strong>
                                        &nbsp; 
                                    </h5>
                                    <div class="input-group input-group-sm" runat="server" id="div_filtro_empleados">
                                        <asp:TextBox
                                            onfocus="this.select();" ID="txtfilterempleado" CssClass=" form-control"
                                            placeholder="Ingrese un filtro" runat="server"></asp:TextBox>
                                        <span class="input-group-btn">
                                            <asp:LinkButton ID="lnksearch" CssClass="btn btn-primary btn-flat"
                                                OnClientClick="return ChangedTextLoad2();" OnClick="lnksearch_Click" runat="server">
                                                <i class="fa fa-search" aria-hidden="true"></i>
                                            </asp:LinkButton>
                                        </span>
                                    </div>
                                    <asp:Image ID="imgloadempleado" Style="display: none;" ImageUrl="~/img/load.gif" runat="server" />
                                    <label id="lblbemp" runat="server" style="display: none; color: #1565c0">Buscando Empleados</label>
                                    <asp:DropDownList Visible="true" ID="ddlempleado_a_consultar" CssClass="form-control"
                                        AutoPostBack="true" runat="server">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="modal-footer ">
                            <button type="button" class="btn btn-default pull-left" data-dismiss="modal">Cerrar</button>
                            <asp:LinkButton OnClientClick="return false;" ID="lnkcargando" CssClass="btn btn-primary btn-flat" runat="server" Style="display: none;">
                                            <i class="fa fa-refresh fa-spin fa-fw"></i>
                                            <span class="sr-only">Loading...</span>&nbsp;Guardando...
                            </asp:LinkButton>
                            <asp:LinkButton ID="lnkguardar" CssClass="btn btn-primary btn-flat" OnClick="lnkguardar_Click"
                                OnClientClick="return ConfirmwidgetProyectoModal('¿Desea Guardar este usuario?');" runat="server">
                                            <i class="fa fa-floppy-o" aria-hidden="true"></i>&nbsp;Guardar
                            </asp:LinkButton>
                        </div>
                </ContentTemplate>
                </asp:UpdatePanel>
            </div>
           </div>  
    <asp:Button ID="btneventgrid" OnClick="btneventgrid_Click" runat="server" Text="Button" Style="display: none;" />
    <asp:Button ID="btneliminar" OnClick="btneliminar_Click" runat="server" Text="Button" Style="display: none;" />
     <asp:HiddenField ID="hdfcommand" runat="server" />
     <asp:HiddenField ID="hdflogin" runat="server" />
</asp:Content>
