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
        function ViewDetailscumplimiento_compromisos(login, num, tipo) {
            alert(num);
            if (num > 0) {
                var nombre = document.getElementById('<%= hdfingenierofiltrar.ClientID %>');
                var tipo_ = document.getElementById('<%= hdftipocompromisosfiltrar.ClientID %>');
                nombre.value = login;
                tipo_.value = tipo;
                document.getElementById('<%= lnkviewcumpli_compromisos_detalles.ClientID%>').click();

            }
            return false;
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">

        <div class="col-lg-12" id="div_reporte" runat="server" visible="true">
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
                                                            <th style="min-width: 180px; text-align: left;" scope="col">Ingeniero</th>
                                                            <th style="min-width: 80px; text-align: center;" scope="col">Terminados a tiempo</th>
                                                            <th style="min-width: 80px; text-align: center;" scope="col">Terminados fuera de tiempo</th>
                                                            <th style="min-width: 80px; text-align: center;" scope="col">No terminados dentro de tiempo</th>
                                                            <th style="min-width: 80px; text-align: center;" scope="col">No terminados fuera de tiempo</th>
                                                            <th style="min-width: 80px; text-align: center;" scope="col">Total de compromisos</th>
                                                            <th style="min-width: 60px; text-align: center;" scope="col">% Eficiencia</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <asp:Repeater ID="repeater_cumplimiento_compromisos" runat="server">
                                                            <ItemTemplate>
                                                                <tr style="font-size: 11px">
                                                                    <td><%# Eval("Ingeniero") %></td>
                                                                    <td style="text-align: center;">
                                                                        <a style="cursor: pointer;" onclick='<%# "return ViewDetailscumplimiento_compromisos("+@"""" + Eval("Login")+@""""+","+Eval("Terminados a Tiempo")+","+@"""Terminados a Tiempo"""+");" %>'>
                                                                            <%# Eval("Terminados a Tiempo") %>
                                                                        </a>
                                                                    </td>
                                                                    <td style="text-align: center;">
                                                                        <a style="cursor: pointer;" onclick='<%# "return ViewDetailscumplimiento_compromisos("+@"""" + Eval("Login")+@""""+","+Eval("Terminados Fuera de Tiempo")+","+@"""Terminados Fuera de Tiempo"""+");" %>'>
                                                                            <%# Eval("Terminados Fuera de Tiempo") %>
                                                                        </a>
                                                                    </td>
                                                                    <td style="text-align: center;">
                                                                        <a style="cursor: pointer;" onclick='<%# "return ViewDetailscumplimiento_compromisos("+@"""" + Eval("Login")+@""""+","+Eval("No Terminados Dentro de Tiempo")+","+@"""No Terminados Dentro de Tiempo"""+");" %>'>
                                                                            <%# Eval("No Terminados Dentro de Tiempo") %>
                                                                        </a>
                                                                    </td>
                                                                    <td style="text-align: center;">
                                                                        <a style="cursor: pointer;" onclick='<%# "return ViewDetailscumplimiento_compromisos("+@"""" + Eval("Login")+@""""+","+Eval("No Terminados Fuera de Tiempo")+","+@"""No Terminados Fuera de Tiempo"""+");" %>'>
                                                                            <%# Eval("No Terminados Fuera de Tiempo") %>
                                                                        </a>
                                                                    </td>
                                                                    <td style="text-align: center;">
                                                                        <a style="cursor: pointer;" onclick='<%# "return ViewDetailscumplimiento_compromisos("+@"""" + Eval("Login")+@""""+","+Eval("Total de compromisos")+","+@""""""+");" %>'>
                                                                            <%# Eval("Total de compromisos") %>
                                                                        </a>
                                                                    </td>
                                                                    <td style="text-align: center;">
                                                                        <%# Eval("Porcentaje de eficiencia") %>
                                                                    </td>

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
    <div class="modal fade bs-example-modal-lg" tabindex="-1" id="mymodalcumplimientos_compromisos" role="dialog" aria-labelledby="mySmallModalLabel" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg" role="document">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="lnkviewcumpli_compromisos_detalles" EventName="Click" />
                </Triggers>
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">×</span></button>
                            <h4 class="modal-title">Compromisos</h4>
                        </div>
                        <div class="modal-body" runat="server">
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="table-responsive">
                                        <table class="table table-resposinve table-bordered table-condensed">
                                            <thead>
                                                <tr>
                                                    <th scope="col">N. Oport</th>
                                                    <th scope="col">Cliente</th>
                                                    <th scope="col">Creado Por</th>
                                                    <th scope="col">Descripcion</th>
                                                    <th scope="col">Tipo</th>
                                                    <th scope="col">Tecnologia</th>
                                                    <th scope="col">Clasificador</th>
                                                    <th scope="col">Asignado A</th>
                                                    <th scope="col">Estatus</th>
                                                    <th scope="col">Horas</th>
                                                    <th scope="col">Prioridad</th>
                                                    <th scope="col">F Creacion</th>
                                                    <th scope="col">F Inicio</th>
                                                    <th scope="col">F Asignado</th>
                                                    <th scope="col">F Comp Ini</th>
                                                    <th scope="col">F Comp Final</th>
                                                    <th scope="col">F Terminado</th>
                                                    <th scope="col">En Asignar</th>
                                                    <th scope="col">Diferencia</th>
                                                    <th scope="col">Iniciar</th>
                                                    <th scope="col">Semana</th>
                                                    <th scope="col">Usuario Cierra</th>
                                                    <th scope="col">Fecha Cierre</th>
                                                    <th scope="col">Calificacion</th>
                                                    <th scope="col">Re Apertura</th>
                                                    <th scope="col">Re Open</th>
                                                    <th scope="col">cumple</th>
                                                    <th scope="col">Dif Dias Venta</th>
                                                    <th scope="col">Dif Dias Practica</th>
                                                    <th scope="col">Dif Dias Asignacion</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="repeater_cumplimiento_compromisos_detalles" runat="server">
                                                    <ItemTemplate>
                                                        <tr style="font-size: 11px">
                                                            <td><%# Eval("NumOport") %></td>
                                                            <td><%# Eval("Cliente") %></td>
                                                            <td><%# Eval("NomCreadoPor") %></td>
                                                            <td><%# Eval("DescComp") %></td>
                                                            <td><%# Eval("TipoComp") %></td>
                                                            <td><%# Eval("DescTecnologia") %></td>
                                                            <td><%# Eval("DescClasificador") %></td>
                                                            <td><%# Eval("NomAsignadoA") %></td>
                                                            <td><%# Eval("DescEstatus") %></td>
                                                            <td><%# Eval("Horas") %></td>
                                                            <td><%# Eval("DescPrioridad") %></td>
                                                            <td><%# Eval("FechaCreacion") %></td>
                                                            <td><%# Eval("FechaInicio") %></td>
                                                            <td><%# Eval("FechaAsignado") %></td>
                                                            <td><%# Eval("FechaCompIni") %></td>
                                                            <td><%# Eval("FechaCompFinal") %></td>
                                                            <td><%# Eval("FechaTerminado") %></td>
                                                            <td><%# Eval("EnAsignar") %></td>
                                                            <td><%# Eval("Diferencia") %></td>
                                                            <td><%# Eval("Iniciar") %></td>
                                                            <td><%# Eval("Semana") %></td>
                                                            <td><%# Eval("UsuarioCierra") %></td>
                                                            <td><%# Eval("FechaCierre") %></td>
                                                            <td><%# Eval("Calificacion") %></td>
                                                            <td><%# Eval("ReApertura") %></td>
                                                            <td><%# Eval("ReOpen") %></td>
                                                            <td><%# Eval("cumple") %></td>
                                                            <td><%# Eval("DifDiasVenta") %></td>
                                                            <td><%# Eval("DifDiasPractica") %></td>
                                                            <td><%# Eval("DifDiasAsignacion") %></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer ">
                            <button type="button" class="btn btn-default pull-left" data-dismiss="modal">Cerrar</button>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <asp:HiddenField ID="hdfingenierofiltrar" runat="server" />
    <asp:HiddenField ID="hdfnumcompromisosfiltrar" runat="server" />
    <asp:HiddenField ID="hdftipocompromisosfiltrar" runat="server" />
    <asp:Button ID="lnkviewcumpli_compromisos_detalles" OnClick="lnkviewcumpli_compromisos_detalles_Click" runat="server" style="display:none"/>
</asp:Content>
