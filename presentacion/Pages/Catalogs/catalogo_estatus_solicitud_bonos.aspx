﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/MP/Global.Master" AutoEventWireup="true" CodeBehind="catalogo_estatus_solicitud_bonos.aspx.cs" Inherits="presentacion.Pages.Catalogs.catalogo_estatus_solicitud_bonos" %>
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
        function ConfirmEntregableDelete(id_permiso) {
            if (confirm('¿Desea eliminar este estatus?')) {
                var hdfusuario = document.getElementById('<%= hdfid_request_status.ClientID %>');
                hdfusuario.value = id_permiso;
                document.getElementById('<%= btneliminar.ClientID%>').click();
                return true;
            } else {
                return false;
            }
        }
        function EditarClick(id_permiso) {
            var hdfusuario = document.getElementById('<%= hdfid_request_status.ClientID %>');
            hdfusuario.value = id_permiso;
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
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-lg-12">
          <h4 class="page-header">Catálogo estatus de la solicitud de bonos</h4>          
        </div>
    </div>
    <div class="row">
        <div class="col-lg-12">
            <div class="box box-danger">
                <div class="box-body"><asp:LinkButton ID="lnknuevoestatus" CssClass="btn btn-primary btn-flat" runat="server" OnClick="lnknuevoestatus_Click">
                Nuevo Estatus de la solicitud&nbsp;<i class="fa fa-plus" aria-hidden="true"></i>
            </asp:LinkButton>
                    <div class="table-responsive">
                        <table class="dvv table no-margin table-condensed">
                            <thead>
                                <tr style="font-size: 12px;">
                                    <th style="width: 20px; text-align: center;" scope="col"></th>
                                    <th style="width: 20px; text-align: center;" scope="col"></th>
                                    <th style=" min-width:80px; text-align: left;" scope="col">tipo estatus</th>
                                    <th style=" min-width:80px; text-align: left;" scope="col">Descripcion</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="repeat_requests_status" runat="server">
                                    <ItemTemplate>
                                        <tr style="font-size: 12px">
                                            <td style="text-align: center;">
                                                <a style="cursor: pointer;"
                                                    onclick='<%# "return EditarClick("+Eval("id_request_status")+");" %>'>
                                                    <i class="fa fa-pencil fa-2x" aria-hidden="true"></i>
                                                </a>
                                            </td>
                                            <td style="text-align: center;">
                                                <a style="cursor: pointer;"
                                                    onclick='<%# "return ConfirmEntregableDelete("+Eval("id_request_status")+");" %>'>
                                                    <i class="fa fa-trash fa-2x" aria-hidden="true"></i>
                                                </a>
                                            </td>
                                            <td style="text-align: left;"><%# Eval("name") %></td>
                                            <td style="text-align: left;"><%# Eval("description") %></td>
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
       <div class="modal fade bs-example-modal-lg" tabindex="-1" id="ModalEstatusSolicitud" role="dialog" aria-labelledby="mySmallModalLabel" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg" role="document">
            <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="lnknuevoestatus" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btneventgrid" EventName="Click" />
                    <asp:PostBackTrigger ControlID="lnkguardar" />
                </Triggers>
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">×</span></button>
                            <h4 class="modal-title">Detalles del estatus</h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-lg-12 col-md-12 col-sm-12">
                                    <h5><strong><i class="fa fa-bars" aria-hidden="true"></i>&nbsp;Nombre del estatus</strong></h5>
                                    <asp:TextBox ID="txtestatus" MaxLength="250" CssClass=" form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-lg-12 col-md-12 col-sm-12">
                                    <h5><strong><i class="fa fa-bars" aria-hidden="true"></i>&nbsp;Descripcion</strong></h5>
                                    <asp:TextBox ID="txtdescripcion" MaxLength="250" CssClass=" form-control" runat="server"></asp:TextBox>
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
                                OnClientClick="return ConfirmwidgetProyectoModal('¿Desea Guardar este estatus?');" runat="server">
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
     <asp:HiddenField ID="hdfid_request_status" runat="server" />
</asp:Content>

