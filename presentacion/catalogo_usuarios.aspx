<%@ Page Title="Catalogo Usuarios" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="catalogo_usuarios.aspx.cs" Inherits="presentacion.catalogo_usuarios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function Init() {
            $('.dvv').DataTable({
                "paging": true,
                "lengthChange": true,
                "searching": true,
                "ordering": true,
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
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-lg-12">
            
        </div>
    </div>
    <div class="row">
        <div class="col-lg-12">
            <div class="box box-primary">
                <div class="box-body">
                    <div class="table-responsive">
                        <table class="dvv table no-margin table-condensed">
                            <thead>
                                <tr style="font-size: 11px;">
                                    <th style="text-align: left;" scope="col">Nombre</th>
                                    <th style="min-width: 70px; text-align: left;" scope="col">No. Empleado</th>
                                    <th style="min-width: 80px; text-align: center;" scope="col">Usuario</th>
                                    <th style="min-width: 65px; text-align: center;" scope="col">Perfil</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="repeater_bonos" runat="server">
                                    <ItemTemplate>
                                        <tr style="font-size: 11px">

                                            <td style="text-align: center;"><%# Eval("Nombre") %></td>
                                            <td style="text-align: center;"><%# Eval("No_") %></td>
                                            <td style="text-align: center;"><%# Eval("Usuario") %></td>
                                            <td style="text-align: center;"><%# Eval("Perfil") %></td>

                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                    </div>
                </div>
                <div class=" box-footer">
                    <asp:LinkButton ID="lnkgenerarexcel" CssClass="btn btn-success btn-flat" runat="server">
                        <i class="fa fa-file-excel-o" aria-hidden="true"></i>&nbsp;Exportar a Excel
                    </asp:LinkButton>
                </div>
            </div>

        </div>
    </div>
</asp:Content>
