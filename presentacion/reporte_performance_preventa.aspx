<%@ Page Title="Preventa" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="reporte_performance_preventa.aspx.cs" Inherits="presentacion.reporte_performance_preventa" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://code.highcharts.com/highcharts.js"></script>
    <script src="https://code.highcharts.com/modules/exporting.js"></script>

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

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <div class="box box-danger box-solid">
                <div class="box-header with-border">
                    <h3 class="box-title">Compromisos</h3>

                    <div class="box-tools pull-right">
                        <button type="button" class="btn btn-box-tool" data-widget="collapse">
                            <i class="fa fa-minus"></i>
                        </button>
                    </div>
                </div>
                <div class="box-body" style="">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="box box-danger">
                                <div class="box-header with-border">
                                    <h3 class="box-title">Cumplimiento de compromisos</h3>
                                </div>
                                <div class="box-body">
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div id="cumpli_compromisos" style="min-width: 200px; height: 400px; max-width: 600px; margin: 0 auto">
                                            </div>

                                        </div>
                                        <div class="col-lg-12">
                                            <div class="table-responsive">
                                                <table class="dvv table no-margin table-condensed">
                                                    <thead>
                                                        <tr style="font-size: 11px;">
                                                            <th style="min-width: 50px; text-align: left;" scope="col">Login</th>
                                                            <th style="min-width: 180px; text-align: left;" scope="col">Ingeniero</th>
                                                            <th style="min-width: 80px; text-align: center;" scope="col">Terminados a tiempo</th>
                                                            <th style="min-width: 80px; text-align: center;" scope="col">Terminados fuera de tiempo</th>
                                                            <th style="min-width: 80px; text-align: center;" scope="col">No terminados dentro de tiempo</th>
                                                            <th style="min-width: 80px; text-align: center;" scope="col">No terminados fuera de tiempo</th>
                                                            <th style="min-width: 80px; text-align: center;" scope="col">Total de compromisos</th>
                                                            <th style="min-width: 60px; text-align: center;" scope="col">% eficiencia</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <asp:Repeater ID="repeater_bonos" runat="server">
                                                            <ItemTemplate>
                                                                <tr style="font-size: 11px">
                                                                    <td><%# Eval("CC") %></td>
                                                                    <td><%# Eval("CC") %></td>
                                                                    <td><%# Eval("CC") %></td>
                                                                    <td><%# Eval("CC") %></td>
                                                                    <td><%# Eval("CC") %></td>
                                                                    <td><%# Eval("CC") %></td>
                                                                    <td><%# Eval("CC") %></td>
                                                                    <td><%# Eval("CC") %></td>

                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="overlay" id="load_cumpli_compromisos" runat="server">
                                    <i class="fa fa-refresh fa-spin"></i>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
