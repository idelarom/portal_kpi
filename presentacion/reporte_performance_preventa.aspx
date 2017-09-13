<%@ Page Title="Preventa" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="reporte_performance_preventa.aspx.cs" Inherits="presentacion.reporte_performance_preventa" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://code.highcharts.com/highcharts.js"></script>
<script src="https://code.highcharts.com/modules/data.js"></script>
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
        
        function ConfirmwidgetProyectoModal() {
            $("#<%= div_modalbodyfiltros.ClientID%>").hide();
            $("#<%= lnkcargando.ClientID%>").show();
            $("#<%= lnkguardar.ClientID%>").hide();
            return true;
        }
        function Carganodfiltros() {
            $("#<%= nkcargandofiltros.ClientID%>").show();
            $("#<%= lnkfiltros.ClientID%>").hide();
            return true;
        }

        function ChanegdTextLoad() {
            var filter = $("#<%= txtfilterempleado.ClientID%>").val();
            if (filter.length == 0 || filter.length > 3) {
                return ChangedTextLoad2();
            } else {
                return true;
            }
        }

        function ChangedTextLoad2() {
            $("#<%= imgloadempleado.ClientID%>").show();
            $("#<%= lblbemp.ClientID%>").show();
            return true;
        }

        function ViewDetailsCumpCompro(ingeniero, tipo, num) {
            if (num > 0) {
                var nombre = document.getElementById('<%= hdfingeniero.ClientID %>');
                var tipo_ = document.getElementById('<%= hdftipocompromisos.ClientID %>');
                var tiempo_ = document.getElementById('<%= hdftiempo.ClientID %>');
                var tipo_tiempo_ = document.getElementById('<%= hdftipo_tiempo.ClientID %>');
                var año_ = document.getElementById('<%= hdfaño.ClientID %>');
                var mes_ = document.getElementById('<%= hdfmes.ClientID %>');
                año_.value = 0;
                mes_.value = 0;
                tiempo_.value = 0;
                tipo_tiempo_.value = 0;
                nombre.value = ingeniero;
                tipo_.value = tipo;
                document.getElementById('<%= btnfiltrocumcompro.ClientID%>').click();
            }
            return true;
        }
        function ViewDetailsTiemposCompromisos(tiempo, tipo_tiempo) {
            var nombre = document.getElementById('<%= hdfingeniero.ClientID %>');
            var tipo_ = document.getElementById('<%= hdftipocompromisos.ClientID %>');
            var tiempo_ = document.getElementById('<%= hdftiempo.ClientID %>');
            var tipo_tiempo_ = document.getElementById('<%= hdftipo_tiempo.ClientID %>');
            var año_ = document.getElementById('<%= hdfaño.ClientID %>');
            var mes_ = document.getElementById('<%= hdfmes.ClientID %>');
            año_.value = 0;
            mes_.value = 0;
            tipo_.value = "";
            nombre.value = "";
            if (tipo_tiempo.toLowerCase() == "asignación ingenieria") {
                tipo_tiempo_.value = 1;
            } else if (tipo_tiempo.toLowerCase() == "ventas") {
                tipo_tiempo_.value = 2;
            } else if (tipo_tiempo.toLowerCase() == "aplazamiento de ingenieria") {
                tipo_tiempo_.value = 3;
            }
            if (tiempo.toLowerCase() == "1 dia") {
                tiempo_.value = 1;
            } else if (tiempo.toLowerCase() == "2 dias") {
                tiempo_.value = 2;
            } else if (tiempo.toLowerCase() == "3 dias") {
                tiempo_.value = 3;
            } else if (tiempo.toLowerCase() == "mayor a 3 dias") {
                tiempo_.value = 4;
            } else if (tiempo.toLowerCase() == "mismo dia") {
                tiempo_.value = 5;
            }
            document.getElementById('<%= btnfiltrocumcompro.ClientID%>').click();
            return true;
        }
        
        function ViewDetailsBacklogCompromisos(año, mes) {
            var nombre = document.getElementById('<%= hdfingeniero.ClientID %>');
            var tipo_ = document.getElementById('<%= hdftipocompromisos.ClientID %>');
            var tiempo_ = document.getElementById('<%= hdftiempo.ClientID %>');
            var tipo_tiempo_ = document.getElementById('<%= hdftipo_tiempo.ClientID %>');
            var año_ = document.getElementById('<%= hdfaño.ClientID %>');
            var mes_ = document.getElementById('<%= hdfmes.ClientID %>');
            año_.value = año;
            mes = mes.toLowerCase();
            tipo_.value = "";
            nombre.value = "";
            tiempo_.value = 0;
            tipo_tiempo_.value = 0;
            if (mes == "enero") {
                mes_.value = 1;
            } else if (mes == "febrero") {
                mes_.value = 2;
            } else if (mes == "marzo") {
                mes_.value = 3;
            } else if (mes == "abril") {
                mes_.value = 4;
            } else if (mes == "mayo") {
                mes_.value = 5;
            } else if (mes == "junio") {
                mes_.value = 6;
            } else if (mes == "julio") {
                mes_.value = 7;
            } else if (mes == "agosto") {
                mes_.value = 8;
            } else if (mes == "septiembre") {
                mes_.value = 9;
            } else if (mes == "octubre") {
                mes_.value = 10;
            } else if (mes == "noviembre") {
                mes_.value = 11;
            } else if (mes == "diciembre") {
                mes_.value = 12;
            }
            document.getElementById('<%= btnfiltrocumcompro.ClientID%>').click();
            return true;
        }
        
        function ViewDetailsOportunidades(ingeniero, tipo_Filtro) {
            var nombre = document.getElementById('<%= hdfingeniero.ClientID %>');
            nombre.value = ingeniero;
            var tipo_filtro_ = document.getElementById('<%= hdftipofiltro_oportunidades.ClientID%>');
            tipo_filtro_.value = tipo_Filtro;
            document.getElementById('<%= btnfiltrooportunidades.ClientID%>').click();

            return true;
        }
        //declaramos los objetos de tipo load desde el inicio (load oscuro)
        var opts = {
            lines: 13 // The number of lines to draw
           , length: 28 // The length of each line
           , width: 14 // The line thickness
           , radius: 42 // The radius of the inner circle
           , scale: .8 // Scales overall size of the spinner
           , corners: 1 // Corner roundness (0..1)
           , color: '#000' // #rgb or #rrggbb or array of colors
           , opacity: 0.1 // Opacity of the lines
           , rotate: 0 // The rotation offset
           , direction: 1 // 1: clockwise, -1: counterclockwise
           , speed: 1 // Rounds per second
           , trail: 60 // Afterglow percentage
           , fps: 20 // Frames per second when using setTimeout() as a fallback for CSS
           , zIndex: 10 // The z-index (defaults to 2000000000)
           , className: 'spinner' // The CSS class to assign to the spinner
           , top: '45%' // Top position relative to parent
           , left: '50%' // Left position relative to parent
           , shadow: true // Whether to render a shadow
           , hwaccel: true // Whether to use hardware acceleration
           , position: 'absolute' // Element positioning
        };

        function ViewDetailsValorGanado(ingeniero) {
            var nombre = document.getElementById('<%= hdfingeniero.ClientID %>');
            nombre.value = ingeniero;
            document.getElementById('<%= btnviewvalor_ganado.ClientID%>').click();
            var fecha_inicial = document.getElementById('<%= rdpfechainicial.ClientID %>').value;
            var fecha_final = document.getElementById('<%= rdpfechafinal.ClientID %>').value;
            BindGrpahDetailsVG(ingeniero, fecha_inicial, fecha_final);
            return true;
        }

        function BindGrpahDetailsVG(ingeniero, fecha_inicial, fecha_final) {
            var estatus_array =  [];
            var montos = [];
            
            var call = $.ajax({
                url: 'reporte_performance_preventa.aspx/GetGenerarValorGanado',
                contentType: "application/json; charset=utf-8",
                type: "POST",
                dataType: "json",
                data: "{fecha_ini:'" + fecha_inicial + "',fecha_fin:'" + fecha_final + "',listado_empleados:'" + ingeniero + "'}",
                success: function (response) {
                    var bono = JSON.parse(response.d);
                    if (bono.length > 0) {
                        for (indice = 0; indice < bono.length; indice++) {
                            montos.push(bono[indice].ValorGanado);
                            estatus_array.push(bono[indice].Estatus);
                        }
                        var monto_maximo = bono[0].amount;
                        var chart = new Highcharts.Chart({
                            chart: {
                                renderTo: 'valor_ganados',
                                type: 'column'
                            },
                            title: {
                                text: 'Valor ganado'
                            },
                            xAxis: {
                                categories: estatus_array
                            },
                            yAxis: {
                                plotLines: [{
                                    value: monto_maximo,
                                    color: '#ff0000',
                                    width: 2,
                                    zIndex: 4,
                                    label: { text: 'Monto maximo' }
                                }],
                                title: { text: 'Bono de desempeño' }
                            },

                            tooltip: {
                                pointFormat: 'Valor ganado: $ <b>{point.y:.1f}</b>'
                            },
                            series: [{
                                name: 'Valor ganado',
                                colorByPoint: true,
                                data: montos
                            },
                            {
                                name: 'Bono maximo',
                                type: 'scatter',
                                marker: {
                                    enabled: false
                                },
                                data: [monto_maximo],
                                tooltip: {
                                    pointFormat: 'Monto maximo del bono trimestral: $ <b>{point.y:.1f}</b>'
                                },
                            }]
                        });
                        ModalShow('#modal_valor_ganado');
                    }
                },
                error: function (result, status, err) {
                    console.log("error", result.responseText);
                  
                }
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <asp:LinkButton OnClientClick="return false;" ID="nkcargandofiltros" CssClass="btn btn-primary btn-flat" runat="server" Style="display: none;">
                                <i class="fa fa-refresh fa-spin fa-fw"></i>
                                <span class="sr-only">Loading...</span>&nbsp;Cargando filtros
            </asp:LinkButton>
            <asp:LinkButton ID="lnkfiltros" CssClass="btn btn-primary btn-flat" OnClick="lnkfiltros_Click" runat="server"
                OnClientClick="return Carganodfiltros();">
                            <i class="fa fa-filter" aria-hidden="true"></i>&nbsp;Filtros
            </asp:LinkButton>
        </div>
    </div>

    <div class="row" id="div_reporte" runat="server" visible="false">
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

                                    <div class="box-tools pull-right">
                                        <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                            <i class="fa fa-minus"></i>
                                        </button>
                                    </div>
                                </div>
                                <div class="box-body">
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div id="cumpli_compromisos" style="min-width: 200px; height: 400px; max-width: 600px; margin: 0 auto">
                                            </div>

                                            <ul class="chart-legend clearfix" style="text-align: center;">
                                                <li><i class="fa fa-circle-o text-green"></i>&nbsp;Terminados a tiempo:&nbsp;<strong><asp:Label ID="lbltt" runat="server" Text="0"></asp:Label></strong></li>
                                                <li><i class="fa fa-circle-o text-yellow"></i>&nbsp;Terminados fuera de tiempo:&nbsp;<strong><asp:Label ID="lbltft" runat="server" Text="0"></asp:Label></strong></li>
                                                <li><i class="fa fa-circle-o text-blue"></i>&nbsp;No terminados dentro de tiempo:&nbsp;<strong><asp:Label ID="lblndt" runat="server" Text="0"></asp:Label></strong></li>
                                                <li><i class="fa fa-circle-o text-red"></i>&nbsp;No terminados fuera de tiempo:&nbsp;<strong><asp:Label ID="lblnft" runat="server" Text="0"></asp:Label></strong></li>
                                            </ul>

                                        </div>
                                        <div class="col-lg-12">
                                            <div class="table-responsive">
                                                <table class="dvv table table-responsive table-bordered table-condensed">
                                                    <thead>
                                                        <tr style="font-size: 11px;">
                                                            <th style="min-width: 200px; text-align: left;" scope="col">Ingeniero</th>
                                                            <th style="min-width: 100px; text-align: center;" scope="col">Terminados a tiempo</th>
                                                            <th style="min-width: 100px; text-align: center;" scope="col">Terminados fuera de tiempo</th>
                                                            <th style="min-width: 100px; text-align: center;" scope="col">No terminados dentro de tiempo</th>
                                                            <th style="min-width: 100px; text-align: center;" scope="col">No terminados fuera de tiempo</th>
                                                            <th style="min-width: 100px; text-align: center;" scope="col">Total de compromisos</th>
                                                            <th style="min-width: 60px; text-align: center;" scope="col">% Eficiencia</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <asp:Repeater ID="repeater_cumpli_compromisos" runat="server">
                                                            <ItemTemplate>
                                                                <tr style="font-size: 11px; height: 10px;">
                                                                    <td><%# Eval("Ingeniero") %></td>
                                                                    <td style="text-align: center;">
                                                                        <a class="btn btn-success btn-xs btn-flat" style="cursor: pointer; min-width: 70px; margin-bottom: 0px;" onclick='<%# "return ViewDetailsCumpCompro("+@"""" + Eval("Login")+@""""+@",""Terminados a Tiempo"","+ Eval("Terminados a Tiempo")+");" %>'>
                                                                            <%# Eval("Terminados a Tiempo") %>
                                                                        </a>

                                                                    </td>
                                                                    <td style="text-align: center;">
                                                                        <a class="btn btn-warning btn-xs btn-flat" style="cursor: pointer; min-width: 70px; margin-bottom: 0px;" onclick='<%# "return ViewDetailsCumpCompro("+@"""" + Eval("Login")+@""""+@",""Terminados Fuera de Tiempo"","+ Eval("Terminados Fuera de Tiempo")+");" %>'>
                                                                            <%# Eval("Terminados Fuera de Tiempo") %>
                                                                        </a>

                                                                    </td>
                                                                    <td style="text-align: center;">
                                                                        <a class="btn btn-primary btn-xs btn-flat" style="cursor: pointer; min-width: 70px; margin-bottom: 0px;" onclick='<%# "return ViewDetailsCumpCompro("+@"""" + Eval("Login")+@""""+@",""No Terminados Dentro de Tiempo"","+ Eval("No Terminados Dentro de Tiempo")+");" %>'>
                                                                            <%# Eval("No Terminados Dentro de Tiempo") %>
                                                                        </a>

                                                                    </td>
                                                                    <td style="text-align: center;">
                                                                        <a class="btn btn-danger btn-xs btn-flat" style="cursor: pointer; min-width: 70px; margin-bottom: 0px;" onclick='<%# "return ViewDetailsCumpCompro("+@"""" + Eval("Login")+@""""+@",""No Terminados Fuera de Tiempo"","+ Eval("No Terminados Fuera de Tiempo")+");" %>'>
                                                                            <%# Eval("No Terminados Fuera de Tiempo") %>
                                                                        </a>

                                                                    </td>
                                                                    <td style="text-align: center;">
                                                                        <a class="btn btn-default btn-xs btn-flat" style="cursor: pointer; min-width: 70px; margin-bottom: 0px;" onclick='<%# "return ViewDetailsCumpCompro("+@"""" + Eval("Login")+@""""+@","""","+ Eval("Total de compromisos")+");" %>'>
                                                                            <%# Eval("Total de compromisos") %>
                                                                        </a>

                                                                    </td>

                                                                    <td style="text-align: center;"><%# Eval("Porcentaje de eficiencia") %></td>
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
                        <div class="col-lg-12">
                            <div class="box box-danger">
                                <div class="box-header with-border">
                                    <h3 class="box-title">Tiempo de compromisos</h3>
                                    <div class="box-tools pull-right">
                                        <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                            <i class="fa fa-minus"></i>
                                        </button>
                                    </div>
                                </div>
                                <div class="box-body">
                                    <div class="row">
                                        <div class="col-lg-8 col-md-8 col-sm-7 col-xs-12">
                                            <asp:GridView ID="grid_tiempo_compromisos" Style="display: none;" runat="server"></asp:GridView>
                                            <div id="tiempo_compromisos" style="min-width: 200px; height: 400px; max-width: 900px; margin: 0 auto">
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-5 col-xs-12">
                                            <div class="box box-danger">
                                                <div class="box-header with-border">
                                                    <h5 class="box-title"></h5>
                                                    <div class="box-tools pull-right">
                                                        <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                                            <i class="fa fa-minus"></i>
                                                        </button>
                                                    </div>
                                                </div>
                                                <div class="box-body">
                                                    <table style="width: 100%">
                                                        <tr>
                                                            <td colspan="3">
                                                                <asp:Label ID="Label33" runat="server" Text="Asignación Ingenieria:" Font-Size="Small"
                                                                    Font-Bold="true"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 34%">
                                                                <asp:Label ID="Label34" runat="server" Text="Clasificación" Font-Size="X-Small"
                                                                    Font-Bold="true"></asp:Label>
                                                            </td>
                                                            <td style="width: 33%">
                                                                <asp:Label ID="Label35" runat="server" Text="No. Compr" Font-Size="X-Small"
                                                                    Font-Bold="true"></asp:Label>
                                                            </td>
                                                            <td style="width: 33%">
                                                                <asp:Label ID="Label36" runat="server" Text="%" Font-Size="X-Small" Font-Bold="true"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label3" runat="server" Text="Mismo día: " Font-Size="X-Small"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="txtAsignadaMismoDia" runat="server" Text=" 0" Font-Size="X-Small"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="txtPerc_AsignadaMismoDia" runat="server" Text=" 0 %" Font-Size="X-Small"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label6" runat="server" Text="1 día: " Font-Size="X-Small"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="txtAsignada1Dia" runat="server" Text=" 0" Font-Size="X-Small"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="txtPerc_Asignada1Dia" runat="server" Text=" 0 %" Font-Size="X-Small"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label9" runat="server" Text="2 días: " Font-Size="X-Small"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="txtAsignada2Dia" runat="server" Text="0" Font-Size="X-Small"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="txtPerc_Asignada2Dia" runat="server" Text=" 0 %" Font-Size="X-Small"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label12" runat="server" Text="3 días: " Font-Size="X-Small"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="txtAsignada3Dia" runat="server" Text=" 0" Font-Size="X-Small"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="txtPerc_Asignada3Dia" runat="server" Text=" 0 %" Font-Size="X-Small"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label15" runat="server" Text="Mayor a 3 días: " Font-Size="X-Small"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="txtAsignadaMayor3Dia" runat="server" Text=" 0" Font-Size="X-Small"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="txtPerc_AsignadaMayor3Dia" runat="server" Text=" 0 %" Font-Size="X-Small"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblNoAsignados" runat="server" Text="No Asignados: " Font-Size="X-Small"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="txtNoAsignados" runat="server" Text=" 0" Font-Size="X-Small"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="txtPerc_NoAsignados" runat="server" Text=" 0 %" Font-Size="X-Small"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <br />
                                                    <table style="width: 100%">
                                                        <tr>
                                                            <td colspan="3">
                                                                <asp:Label ID="Label37" runat="server" Text="Ventas:" Font-Size="Small" Font-Bold="true"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 34%">
                                                                <asp:Label ID="Label38" runat="server" Text="Clasificación" Font-Size="X-Small"
                                                                    Font-Bold="true"></asp:Label>
                                                            </td>
                                                            <td style="width: 33%">
                                                                <asp:Label ID="Label39" runat="server" Text="No. Compr" Font-Size="X-Small"
                                                                    Font-Bold="true"></asp:Label>
                                                            </td>
                                                            <td style="width: 33%">
                                                                <asp:Label ID="Label40" runat="server" Text="%" Font-Size="X-Small" Font-Bold="true"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblVentaMismoDia" runat="server" Text="Mismo día: " Font-Size="X-Small"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="txtVentaMismoDia" runat="server" Text=" 0" Font-Size="X-Small"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="txtPerc_VentaMismoDia" runat="server" Text=" 0 %" Font-Size="X-Small"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblVenta1Dia" runat="server" Text="1 día: " Font-Size="X-Small"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="txtVenta1Dia" runat="server" Text=" 0" Font-Size="X-Small"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="txtPerc_Venta1Dia" runat="server" Text=" 0 %" Font-Size="X-Small"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblVenta2Dia" runat="server" Text="2 días: " Font-Size="X-Small"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="txtVenta2Dia" runat="server" Text="0" Font-Size="X-Small"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="txtPerc_Venta2Dia" runat="server" Text=" 0 %" Font-Size="X-Small"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblVenta3Dia" runat="server" Text="3 días: " Font-Size="X-Small"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="txtVenta3Dia" runat="server" Text=" 0" Font-Size="X-Small"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="txtPerc_Venta3Dia" runat="server" Text=" 0 %" Font-Size="X-Small"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblVentaMayor3Dia" runat="server" Text="Mayor a 3 días: " Font-Size="X-Small"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="txtVentaMayor3Dia" runat="server" Text=" 0" Font-Size="X-Small"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="txtPerc_VentaMayor3Dia" runat="server" Text=" 0 %" Font-Size="X-Small"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <br />
                                                    <table style="width: 100%">
                                                        <tr>
                                                            <td colspan="3">
                                                                <asp:Label ID="Label41" runat="server" Text="Aplazamiento Ingenieria:" Font-Size="Small"
                                                                    Font-Bold="true"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 34%">
                                                                <asp:Label ID="Label42" runat="server" Text="Clasificación" Font-Size="X-Small"
                                                                    Font-Bold="true"></asp:Label>
                                                            </td>
                                                            <td style="width: 33%">
                                                                <asp:Label ID="Label43" runat="server" Text="No. Compr" Font-Size="X-Small"
                                                                    Font-Bold="true"></asp:Label>
                                                            </td>
                                                            <td style="width: 33%">
                                                                <asp:Label ID="Label44" runat="server" Text="%" Font-Size="X-Small" Font-Bold="true"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label18" runat="server" Text="Mismo día: " Font-Size="X-Small"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="txtAplazadasMismoDia" runat="server" Text=" 0" Font-Size="X-Small"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="txtPerc_AplazadasMismoDia" runat="server" Text=" 0 %" Font-Size="X-Small"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label21" runat="server" Text="1 día: " Font-Size="X-Small"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="txtAplazadas1Dia" runat="server" Text=" 0" Font-Size="X-Small"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="txtPerc_Aplazadas1Dia" runat="server" Text=" 0 %" Font-Size="X-Small"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label24" runat="server" Text="2 días: " Font-Size="X-Small"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="txtAplazadas2Dia" runat="server" Text=" 0" Font-Size="X-Small"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="txtPerc_Aplazadas2Dia" runat="server" Text=" 0 %" Font-Size="X-Small"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label27" runat="server" Text="3 días: " Font-Size="X-Small"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="txtAplazadas3Dia" runat="server" Text=" 0" Font-Size="X-Small"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="txtPerc_Aplazadas3Dia" runat="server" Text=" 0 %" Font-Size="X-Small"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label30" runat="server" Text="Mayor a 3 días: " Font-Size="X-Small"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="txtAplazadasMayor3Dia" runat="server" Text=" 0" Font-Size="X-Small"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="txtPerc_AplazadasMayor3Dia" runat="server" Text=" 0 %" Font-Size="X-Small"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblNoAsignados2" runat="server" Text="No Asignados: " Font-Size="X-Small"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="txtNoAsignados2" runat="server" Text=" 0" Font-Size="X-Small"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="txtPerc_NoAsignados2" runat="server" Text=" 0 %" Font-Size="X-Small"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>


                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-12">
                            <div class="box box-danger">
                                <div class="box-header with-border">
                                    <h3 class="box-title">Backlog compromisos</h3>
                                    <div class="box-tools pull-right">
                                        <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                            <i class="fa fa-minus"></i>
                                        </button>
                                    </div>
                                </div>
                                <div class="box-body">
                                    <div class="row">
                                        <div class="col-lg-8 col-md-8 col-sm-7 col-xs-12">
                                            <asp:GridView ID="grid_backlog_compromisos_actual" Style="display: none" runat="server"></asp:GridView>
                                            <div id="backlog_compromisos_actual" style="min-width: 200px; height: 400px; max-width: 900px; margin: 0 auto">
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-5 col-xs-12">
                                            <table style="width: 100%">
                                                <tr>
                                                    <td colspan="3">
                                                        <asp:Label ID="Label4" runat="server" Text="Distribucion:" Font-Size="Small" Font-Bold="true"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 34%">
                                                        <asp:Label ID="Label5" runat="server" Text="Clasificación" Font-Size="X-Small" Font-Bold="true"></asp:Label>
                                                    </td>
                                                    <td style="width: 33%">
                                                        <asp:Label ID="Label7" runat="server" Text="No. Compr" Font-Size="X-Small"
                                                            Font-Bold="true"></asp:Label>
                                                    </td>
                                                    <td style="width: 33%">
                                                        <asp:Label ID="Label8" runat="server" Text="%" Font-Size="X-Small" Font-Bold="true"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblEnero" runat="server" Text="Enero: " Font-Size="X-Small"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="txtEnero" runat="server" Text=" 0" Font-Size="X-Small"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="txtPerc_Enero" runat="server" Text=" 0 %" Font-Size="X-Small"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblFebrero" runat="server" Text="Febrero: " Font-Size="X-Small"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="txtFebrero" runat="server" Text=" 0" Font-Size="X-Small"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="txtPerc_Febrero" runat="server" Text=" 0 %" Font-Size="X-Small"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblMarzo" runat="server" Text="Marzo: " Font-Size="X-Small"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="txtMarzo" runat="server" Text=" 0" Font-Size="X-Small"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="txtPerc_Marzo" runat="server" Text=" 0 %" Font-Size="X-Small"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblAbril" runat="server" Text="Abril: " Font-Size="X-Small"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="txtAbril" runat="server" Text=" 0" Font-Size="X-Small"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="txtPerc_Abril" runat="server" Text=" 0 %" Font-Size="X-Small"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblMayo" runat="server" Text="Mayo: " Font-Size="X-Small"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="txtMayo" runat="server" Text=" 0" Font-Size="X-Small"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="txtPerc_Mayo" runat="server" Text=" 0 %" Font-Size="X-Small"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblJunio" runat="server" Text="Junio: " Font-Size="X-Small"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="txtJunio" runat="server" Text=" 0" Font-Size="X-Small"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="txtPerc_Junio" runat="server" Text=" 0 %" Font-Size="X-Small"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblJulio" runat="server" Text="Julio: " Font-Size="X-Small"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="txtJulio" runat="server" Text=" 0" Font-Size="X-Small"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="txtPerc_Julio" runat="server" Text=" 0 %" Font-Size="X-Small"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblAgosto" runat="server" Text="Agosto: " Font-Size="X-Small"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="txtAgosto" runat="server" Text=" 0" Font-Size="X-Small"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="txtPerc_Agosto" runat="server" Text=" 0%" Font-Size="X-Small"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblSeptiembre" runat="server" Text="Septiembre: " Font-Size="X-Small"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="txtSeptiembre" runat="server" Text=" 0" Font-Size="X-Small"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="txtPerc_Septiembre" runat="server" Text=" 0 %" Font-Size="X-Small"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblOctubre" runat="server" Text="Octubre: " Font-Size="X-Small"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="txtOctubre" runat="server" Text=" 0" Font-Size="X-Small"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="txtPerc_Octubre" runat="server" Text=" 0 %" Font-Size="X-Small"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblNoviembre" runat="server" Text="Noviembre: " Font-Size="X-Small"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="txtNoviembre" runat="server" Text=" 0" Font-Size="X-Small"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="txtPerc_Noviembre" runat="server" Text=" 0 %" Font-Size="X-Small"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblDiciembre" runat="server" Text="Diciembre: " Font-Size="X-Small"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="txtDiciembre" runat="server" Text=" 5" Font-Size="X-Small"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="txtPerc_Diciembre" runat="server" Text=" 8.34%" Font-Size="X-Small"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3" style="border-bottom: ridge"></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblTotal" runat="server" Text="Total: " Font-Size="X-Small"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="txtTotal" runat="server" Text=" 0" Font-Size="X-Small"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="txtperc_Total" runat="server" Text=" 0 %" Font-Size="X-Small"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>

                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-8 col-md-8 col-sm-7 col-xs-12">
                                            <asp:GridView ID="grid_backlog_compromisos_anterior" Style="display: none" runat="server"></asp:GridView>
                                            <div id="backlog_compromisos_anterior" style="min-width: 200px; height: 400px; max-width: 900px; margin: 0 auto">
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-5 col-xs-12">
                                            <table style="width: 100%">
                                                <tr>
                                                    <td colspan="3">
                                                        <asp:Label ID="Label14" runat="server" Text="Distribucion:" Font-Size="Small" Font-Bold="true"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 34%">
                                                        <asp:Label ID="Label16" runat="server" Text="Clasificación" Font-Size="X-Small"
                                                            Font-Bold="true"></asp:Label>
                                                    </td>
                                                    <td style="width: 33%">
                                                        <asp:Label ID="Label17" runat="server" Text="No. Compr" Font-Size="X-Small"
                                                            Font-Bold="true"></asp:Label>
                                                    </td>
                                                    <td style="width: 33%">
                                                        <asp:Label ID="Label19" runat="server" Text="%" Font-Size="X-Small" Font-Bold="true"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblEneroAnt" runat="server" Text="Enero: " Font-Size="X-Small"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="txtEneroAnt" runat="server" Text=" 0" Font-Size="X-Small"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="txtPerc_EneroAnt" runat="server" Text=" 0 %" Font-Size="X-Small"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblFebreroAnt" runat="server" Text="Febrero: " Font-Size="X-Small"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="txtFebreroAnt" runat="server" Text=" 0" Font-Size="X-Small"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="txtPerc_FebreroAnt" runat="server" Text=" 0 %" Font-Size="X-Small"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblMarzoAnt" runat="server" Text="Marzo: " Font-Size="X-Small"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="txtMarzoAnt" runat="server" Text=" 0" Font-Size="X-Small"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="txtPerc_MarzoAnt" runat="server" Text=" 0 %" Font-Size="X-Small"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblAbrilAnt" runat="server" Text="Abril: " Font-Size="X-Small"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="txtAbrilAnt" runat="server" Text=" 0" Font-Size="X-Small"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="txtPerc_AbrilAnt" runat="server" Text=" 0 %" Font-Size="X-Small"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblMayoAnt" runat="server" Text="Mayo: " Font-Size="X-Small"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="txtMayoAnt" runat="server" Text=" 0" Font-Size="X-Small"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="txtPerc_MayoAnt" runat="server" Text=" 0 %" Font-Size="X-Small"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblJunioAnt" runat="server" Text="Junio: " Font-Size="X-Small"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="txtJunioAnt" runat="server" Text=" 0" Font-Size="X-Small"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="txtPerc_JunioAnt" runat="server" Text=" 0 %" Font-Size="X-Small"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblJulioAnt" runat="server" Text="Julio: " Font-Size="X-Small"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="txtJulioAnt" runat="server" Text=" 0" Font-Size="X-Small"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="txtPerc_JulioAnt" runat="server" Text=" 0 %" Font-Size="X-Small"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblAgostoAnt" runat="server" Text="Agosto: " Font-Size="X-Small"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="txtAgostoAnt" runat="server" Text=" 0" Font-Size="X-Small"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="txtPerc_AgostoAnt" runat="server" Text=" 0 %" Font-Size="X-Small"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblSeptiembreAnt" runat="server" Text="Septiembre: " Font-Size="X-Small"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="txtSeptiembreAnt" runat="server" Text=" 0" Font-Size="X-Small"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="txtPerc_SeptiembreAnt" runat="server" Text=" 0 %" Font-Size="X-Small"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblOctubreAnt" runat="server" Text="Octubre: " Font-Size="X-Small"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="txtOctubreAnt" runat="server" Text=" 0" Font-Size="X-Small"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="txtPerc_OctubreAnt" runat="server" Text=" 0 %" Font-Size="X-Small"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblNoviembreAnt" runat="server" Text="Noviembre: " Font-Size="X-Small"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="txtNoviembreAnt" runat="server" Text=" 0" Font-Size="X-Small"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="txtPerc_NoviembreAnt" runat="server" Text=" 0 %" Font-Size="X-Small"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblDiciembreAnt" runat="server" Text="Diciembre: " Font-Size="X-Small"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="txtDiciembreAnt" runat="server" Text=" 0" Font-Size="X-Small"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="txtPerc_DiciembreAnt" runat="server" Text=" 0 %" Font-Size="X-Small"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3" style="border-bottom: groove"></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblTotalAnt" runat="server" Text="Total: " Font-Size="X-Small"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="txtTotalAnt" runat="server" Text=" 0" Font-Size="X-Small"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="txtperc_TotalAnt" runat="server" Text=" 0 %" Font-Size="X-Small"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-lg-12">
            <div class="box box-danger box-solid">
                <div class="box-header with-border">
                    <h3 class="box-title">Oportunidades</h3>
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
                                    <h3 class="box-title">Horas trabajadas por folio de oportunidad respecto al vendedor </h3>
                                    <div class="box-tools pull-right">
                                        <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                            <i class="fa fa-minus"></i>
                                        </button>
                                    </div>
                                </div>
                                <div class="box-body">
                                    <div class="row">
                                        <div class=" col-lg-12">
                                            <asp:GridView ID="grid_horas_trabajadas_oportunidades" Style="display: none;" runat="server"></asp:GridView>
                                            <div id="horas_trabajadas_oportunidades" style="min-width: 200px; height: 400px; max-width: 1200px; margin: 0 auto">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-12">
                            <div class="box box-danger">
                                <div class="box-header with-border">
                                    <h3 class="box-title">Horas trabajadas por folio de oportunidad respecto al vendedor </h3>
                                    <div class="box-tools pull-right">
                                        <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                            <i class="fa fa-minus"></i>
                                        </button>
                                    </div>
                                </div>
                                <div class="box-body">
                                    <div class="row">
                                        <div class=" col-lg-12">
                                            <div id="estatus_oportunidades" style="min-width: 200px; height: 400px; max-width: 600px; margin: 0 auto">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-12">
                            <div class="box box-danger">
                                <div class="box-header with-border">
                                    <h3 class="box-title">Grafica por horas de Ingenieros de preventa </h3>
                                    <div class="box-tools pull-right">
                                        <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                            <i class="fa fa-minus"></i>
                                        </button>
                                    </div>
                                </div>
                                <div class="box-body">
                                    <div class="row">
                                        <div class=" col-lg-12">
                                            <asp:GridView ID="grid_horas_trabajadas_oportunidades_ingeniero" Style="display: none;" runat="server"></asp:GridView>
                                            <div id="horas_trabajadas_oportunidades_ingeniero" style="min-width: 200px; height: 400px; max-width: 1200px; margin: 0 auto">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-12">
                            <div class="box box-danger">
                                <div class="box-header with-border">
                                    <h3 class="box-title">Valor ganado por ingeniero de preventa</h3>
                                    <div class="box-tools pull-right">
                                        <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                            <i class="fa fa-minus"></i>
                                        </button>
                                    </div>
                                </div>
                                <div class="box-body">
                                    <div class="row">

                                        <div class=" col-lg-12">
                                            <div class="table-responsive">
                                                <table class="dvv table table-responsive table-bordered table-condensed">
                                                    <thead>
                                                        <tr style="font-size: 11px;">
                                                            <th style="min-width: 200px; text-align: left;" scope="col">Ingeniero</th>
                                                            <th style="min-width: 40px; text-align: center;" scope="col">Ganadas</th>
                                                            <th style="min-width: 40px; text-align: center;" scope="col">Cancealadas</th>
                                                            <th style="min-width: 40px; text-align: center;" scope="col">Perdidas</th>
                                                            <th style="min-width: 40px; text-align: center;" scope="col">Abandonadas</th>
                                                            <th style="min-width: 40px; text-align: center;" scope="col">Abiertas</th>
                                                            <th style="min-width: 40px; text-align: center;" scope="col">Seguimiento</th>
                                                            <th style="min-width: 40px; text-align: center;" scope="col">En Espera</th>
                                                            <th style="min-width: 40px; text-align: center;" scope="col">Total</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <asp:Repeater ID="repeater_valor_ganado" runat="server">
                                                            <ItemTemplate>
                                                                <tr style="font-size: 11px; height: 10px;">
                                                                    <td>
                                                                        <a style="cursor: pointer;"
                                                                            onclick='<%# "return ViewDetailsValorGanado("+@"""" + Eval("Login")+@""""+@");" %>'>
                                                                            <%# Eval("Nombre") %>
                                                                        </a>
                                                                    </td>
                                                                    <td style="text-align: center;">
                                                                        <%# Eval("Ganada") %>
                                                                    </td>
                                                                    <td style="text-align: center;">
                                                                        <%# Eval("Cancelada") %>
                                                                    </td>
                                                                    <td style="text-align: center;">
                                                                        <%# Eval("Perdida") %>
                                                                    </td>
                                                                    <td style="text-align: center;">
                                                                        <%# Eval("Abandonada") %>
                                                                    </td>
                                                                    <td style="text-align: center;">
                                                                        <%# Eval("Abierta") %>
                                                                    </td>
                                                                    <td style="text-align: center;">
                                                                        <%# Eval("Seguimiento") %>
                                                                    </td>
                                                                    <td style="text-align: center;">
                                                                        <%# Eval("En espera") %>
                                                                    </td>
                                                                    <td style="text-align: center;">
                                                                        <%# Eval("Total de compromisos") %>
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
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>
    <div class="modal fade bs-example-modal-lg" tabindex="-1" id="myModal" role="dialog" aria-labelledby="mySmallModalLabel" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg" role="document">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                <Triggers>
                    <asp:PostBackTrigger ControlID="lnkguardar" />
                </Triggers>
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">×</span></button>
                            <h4 class="modal-title">Filtros</h4>
                        </div>
                        <div class="modal-body" id="div_modalbodyfiltros" runat="server">
                            <div class="row">
                                <div class="col-lg-12 col-xs-12">
                                    <h6><strong><i class="fa fa-users" aria-hidden="true"></i>&nbsp;Seleccione el empleado a consultar</strong>
                                        &nbsp; 
                                        <asp:CheckBox ID="cbxnoactivo" Text="Ver no Activos" Checked="true" runat="server" />
                                    </h6>
                                    <div class="input-group input-group-sm">
                                        <asp:TextBox
                                            onfocus="this.select();" ID="txtfilterempleado" CssClass=" form-control"
                                            placeholder="Ingrese un filtro(ejemplo:Nombre)" runat="server" OnTextChanged="txtfilterempleado_TextChanged"></asp:TextBox>
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
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlempleado_a_consultar_SelectedIndexChanged" runat="server">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                                    <h6><strong><i class="fa fa-calendar" aria-hidden="true"></i>&nbsp;Fecha Inicial</strong></h6>
                                    <telerik:RadDatePicker ID="rdpfechainicial" runat="server" Width="100%" Skin="Bootstrap"></telerik:RadDatePicker>
                                </div>
                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6" style="font-size: 10px;">
                                    <h6><strong><i class="fa fa-calendar" aria-hidden="true"></i>&nbsp;Fecha Final</strong></h6>
                                    <telerik:RadDatePicker ID="rdpfechafinal" runat="server" Width="100%" Skin="Bootstrap"></telerik:RadDatePicker>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                                    <br />
                                    <asp:LinkButton ID="lnkagregarseleccion" OnClick="lnkagregarseleccion_Click"
                                        CssClass="btn btn-primary btn-flat btn-xs" runat="server">
                                        Selección&nbsp;<i class="fa fa-plus" aria-hidden="true"></i>
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="lnkagregartodos" OnClick="lnkagregartodos_Click"
                                        CssClass="btn btn-primary btn-flat btn-xs" runat="server">
                                        Todos&nbsp;<i class="fa fa-plus" aria-hidden="true"></i>
                                    </asp:LinkButton>
                                    <div style="max-height: 130px; height: 130px; overflow: scroll;">
                                        <telerik:RadTreeView RenderMode="Lightweight" ID="rtvListEmpleado" runat="server" Width="100%"
                                            Style="background-color: white; font-size: 9px;" Skin="Bootstrap">
                                            <DataBindings>
                                                <telerik:RadTreeNodeBinding Expanded="False"></telerik:RadTreeNodeBinding>
                                            </DataBindings>
                                        </telerik:RadTreeView>
                                    </div>

                                    <label>
                                        <asp:Label ID="lblcountlistempleados" runat="server" Text="0"></asp:Label></label>
                                </div>
                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                                    <br />
                                    <asp:LinkButton ID="lnklimpiar" OnClick="lnklimpiar_Click"
                                        CssClass="btn btn-danger btn-flat btn-xs" runat="server">
                                        Limpiar&nbsp;<i class="fa fa-trash" aria-hidden="true"></i>
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="lnkeliminarselecion" OnClick="lnkeliminarselecion_Click"
                                        CssClass="btn btn-danger btn-flat btn-xs" runat="server">
                                        Seleccion&nbsp;<i class="fa fa-trash" aria-hidden="true"></i>
                                    </asp:LinkButton>
                                    <div style="max-height: 130px; height: 130px; overflow: scroll;">
                                        <telerik:RadListBox RenderMode="Lightweight" runat="server" ID="rdtselecteds" Width="100%"
                                            Style="font-size: 9px" Skin="Bootstrap" SelectionMode="Multiple" Sort="Ascending">
                                        </telerik:RadListBox>
                                    </div>
                                    <label>
                                        <asp:Label ID="lblcountselecteds" runat="server" Text="0"></asp:Label></label>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer ">
                            <button type="button" class="btn btn-default pull-left" data-dismiss="modal">Cerrar</button>
                            <asp:LinkButton OnClientClick="return false;" ID="lnkcargando" CssClass="btn btn-primary btn-flat" runat="server" Style="display: none;">
                                            <i class="fa fa-refresh fa-spin fa-fw"></i>
                                            <span class="sr-only">Loading...</span>&nbsp;Generando Reporte
                            </asp:LinkButton>
                            <asp:LinkButton ID="lnkguardar" CssClass="btn btn-primary btn-flat" OnClick="lnkguardar_Click"
                                OnClientClick="return ConfirmwidgetProyectoModal();" runat="server">
                                            <i class="fa fa-database" aria-hidden="true"></i>&nbsp;Generar Reporte
                            </asp:LinkButton>
                        </div>
                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div class="modal fade bs-example-modal-lg" tabindex="-1" id="modal_cumpl_compromisos" role="dialog" aria-labelledby="mySmallModalLabel" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg" role="document">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnfiltrocumcompro" EventName="Click" />
                </Triggers>
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">×</span></button>
                            <h4 class="modal-title">Compromisos</h4>
                        </div>
                        <div class="modal-body" id="div1" runat="server">
                            <div class="row">
                                <div class="col-lg-12 col-xs-12">
                                    <div class="table-responsive" style="max-height: 420px; overflow: scroll;">
                                        <table class="table table-resposive table-bordered table-condensed">
                                            <thead>
                                                <tr style="font-size: 11px; color: white; background-color: #C42C2C">
                                                    <td style="min-width: 80px;">Num Oport</td>
                                                    <td style="min-width: 240px;">Cliente</td>
                                                    <td style="min-width: 220px;">Creado Por</td>
                                                    <td style="min-width: 400px;">Desc Comp</td>
                                                    <td style="min-width: 140px;">Tipo Comp</td>
                                                    <td style="min-width: 220px;">Tecnologia</td>
                                                    <td style="min-width: 140px;">Clasificador</td>
                                                    <td style="min-width: 220px;">Asignado A</td>
                                                    <td style="min-width: 140px;">Estatus</td>
                                                    <td style="min-width: 60px;">Horas</td>
                                                    <td style="min-width: 120px;">Prioridad</td>
                                                    <td style="min-width: 140px;">F Creacion</td>
                                                    <td style="min-width: 140px;">F Inicio</td>
                                                    <td style="min-width: 140px;">F Asignado</td>
                                                    <td style="min-width: 140px;">F Comp Ini</td>
                                                    <td style="min-width: 140px;">F Comp Final</td>
                                                    <td style="min-width: 140px;">F Terminado</td>
                                                    <td style="min-width: 80px;">En Asignar</td>
                                                    <td style="min-width: 80px;">Diferencia</td>
                                                    <td style="min-width: 140px;">Iniciar</td>
                                                    <td style="min-width: 100px;">Semana</td>
                                                    <td style="min-width: 140px;">Usuario Cierra</td>
                                                    <td style="min-width: 140px;">Fecha Cierre</td>
                                                    <td style="min-width: 140px;">Calificacion</td>
                                                    <td style="min-width: 80px;">Re Apertura</td>
                                                    <td style="min-width: 80px;">Re Open</td>
                                                    <td style="min-width: 200px;">Cumple</td>
                                                    <td style="min-width: 100px;">Dif Dias Venta</td>
                                                    <td style="min-width: 100px;">Dif Dias Practica</td>
                                                    <td style="min-width: 100px;">Dif Dias Asignacion</td>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="repeater_cumpli_compromisos_detalles" runat="server">
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
    <div class="modal fade bs-example-modal-lg" tabindex="-1" id="modal_cumpl_oportunidades" role="dialog" aria-labelledby="mySmallModalLabel" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg" role="document">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Always">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnfiltrooportunidades" EventName="Click" />
                </Triggers>
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">×</span></button>
                            <h4 class="modal-title">Oportunidades</h4>
                        </div>
                        <div class="modal-body" id="div2" runat="server">
                            <div class="row">
                                <div class="col-lg-12 col-xs-12">
                                    <div class="table-responsive" style="max-height: 420px; overflow: scroll;">
                                        <table class="table table-resposive table-bordered table-condensed">
                                            <thead>
                                                <tr style="font-size: 11px; color: white; background-color: #C42C2C">

                                                    <th style="min-width: 80px;" scope="col">Folio OP</th>
                                                    <th style="min-width: 300px;" scope="col">Cliente</th>
                                                    <th style="min-width: 80px;" scope="col">Total Horas</th>
                                                    <th style="min-width: 120px;" scope="col">Monto</th>
                                                    <th style="min-width: 120px;" scope="col">Margen</th>
                                                    <th style="min-width: 200px;" scope="col">Fecha Auto.</th>
                                                    <th style="min-width: 100px;" scope="col">Usuario Agente</th>
                                                    <th style="min-width: 240px;" scope="col">Agente</th>
                                                    <th style="min-width: 100px;" scope="col">Login INg</th>
                                                    <th style="min-width: 240px;" scope="col">Ingeniero</th>
                                                    <th style="min-width: 120px;" scope="col">Total Horas Ing.</th>
                                                    <th style="min-width: 120px;" scope="col">Estatus</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="repeat_oportunidades" runat="server">
                                                    <ItemTemplate>
                                                        <tr style="font-size: 11px">
                                                            <td><%# Eval("FOLIO_OP") %></td>
                                                            <td><%# Eval("Cliente") %></td>
                                                            <td><%# Eval("TOTAL_HORAS_OP") %></td>
                                                            <td><%# Convert.ToDecimal(Eval("MONTO_OP")).ToString("C") %></td>
                                                            <td><%# Convert.ToDecimal(Eval("MARGEN_OP")).ToString("C") %></td>
                                                            <td><%# Convert.ToDateTime(Eval("FECHA_AUTORIZACION")).ToString("dddd dd MMMM, yyyy hh:mm:ss", System.Globalization.CultureInfo.CreateSpecificCulture("es-MX")).ToUpper() %></td>
                                                            <td><%# Eval("LOGIN_AGENTE_VENTA") %></td>
                                                            <td><%# Eval("AGENTE_VENTA") %></td>
                                                            <td><%# Eval("LOGIN_ING_PREVENTA") %></td>
                                                            <td><%# Eval("ING_PREVENTA") %></td>
                                                            <td><%# Eval("TOTAL_HORAS_ING_PREVENTA") %></td>
                                                            <td><%# Eval("ESTATUS") %></td>

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
    <div class="modal fade bs-example-modal-lg" tabindex="-1" id="modal_valor_ganado" role="dialog" aria-labelledby="mySmallModalLabel" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg" role="document">
            <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Always">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnviewvalor_ganado" EventName="Click" />
                </Triggers>
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">×</span></button>
                            <h4 class="modal-title">Detalles valor ganado</h4>
                        </div>
                        <div class="modal-body" id="div_valor_ganado">
                            <div class="row">
                                <div class="col-lg-12 col-md-12 col-sm-12">
                                    <div id="valor_ganados" style="min-width: 200px; height: 300px; max-width: 1200px; margin: 0 auto">
                                    </div>
                                </div>
                                <br />
                                <div class=" col-lg-12 col-md-12 col-sm-12" id="div_Detalles_vg" runat="server" visible="false">
                                    <div class="table-responsive" style="max-height: 170px; overflow: scroll;">
                                        <table class="table table-responsive table-bordered table-condensed">
                                            <thead>
                                                <tr style="font-size: 11px;">
                                                    <th style="min-width: 80px; text-align: left;" scope="col">Cve Opor</th>
                                                    <th style="min-width: 80px; text-align: center;" scope="col">Folio Op</th>
                                                    <th style="min-width: 300px; text-align: center;" scope="col">Cliente</th>
                                                    <th style="min-width: 100px; text-align: center;" scope="col">Horas</th>
                                                    <th style="min-width: 100px; text-align: center;" scope="col">% Horas</th>
                                                    <th style="min-width: 100px; text-align: center;" scope="col">Margen Bruto</th>
                                                    <th style="min-width: 100px; text-align: center;" scope="col">Margen 10</th>
                                                    <th style="min-width: 100px; text-align: center;" scope="col">Monto Ing.</th>
                                                    <th style="min-width: 120px; text-align: center;" scope="col">Monto Ing new</th>
                                                    <th style="min-width: 100px; text-align: center;" scope="col">Estatus</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="repeater_detalles_vg" runat="server">
                                                    <ItemTemplate>
                                                        <tr style="font-size: 11px; height: 10px;">

                                                            <td style="text-align: center;">
                                                                <%# Eval("cveoport") %>
                                                            </td>
                                                            <td style="text-align: center;">
                                                                <%# Eval("folioop") %>
                                                            </td>
                                                            <td style="text-align: center;">
                                                                <%# Eval("cliente") %>
                                                            </td>
                                                            <td style="text-align: center;">
                                                                <%# Convert.ToDecimal(Eval("horas")) %>
                                                            </td>
                                                            <td style="text-align: center;">
                                                                <%# Eval("porchoras").ToString() + " %" %>
                                                            </td>
                                                            <td style="text-align: center;">
                                                                <%# Convert.ToDecimal(Eval("margenbruto")).ToString("C") %>
                                                            </td>
                                                            <td style="text-align: center;">
                                                                <%# Convert.ToDecimal(Eval("margen10")).ToString("C") %>
                                                            </td>
                                                            <td style="text-align: center;">
                                                                <%# Convert.ToDecimal(Eval("montoing")).ToString("C") %>
                                                            </td>
                                                            <td style="text-align: center;">
                                                                <%# Convert.ToDecimal(Eval("montoingnew")).ToString("C") %>
                                                            </td>
                                                            <td style="text-align: center;">
                                                                <%# Eval("estatus") %>
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
                        <div class="modal-footer ">
                            <button type="button" class="btn btn-default pull-left" data-dismiss="modal">Cerrar</button>
                        </div>
                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <asp:Button ID="btnfiltrocumcompro" OnClick="btnfiltrocumcompro_Click" Style="display: none" runat="server" Text="Button" />
    <asp:Button ID="btnfiltrooportunidades" OnClick="btnfiltrooportunidades_Click" Style="display: none" runat="server" Text="Button" />
    <asp:Button ID="btnviewvalor_ganado" OnClick="btnviewvalor_ganado_Click" Style="display: none" runat="server" Text="Button" />
    <asp:HiddenField ID="hdfingeniero" runat="server" />
    <asp:HiddenField ID="hdftipocompromisos" runat="server" />
    <asp:HiddenField ID="hdftiempo" runat="server" />
    <asp:HiddenField ID="hdftipo_tiempo" runat="server" />
    <asp:HiddenField ID="hdfaño" runat="server" />
    <asp:HiddenField ID="hdfmes" runat="server" />
    <asp:HiddenField ID="hdftipofiltro_oportunidades" runat="server" />
    <asp:HiddenField ID="hdfestatus" runat="server" />
    <asp:HiddenField ID="hdfvalor_ganado" runat="server" />
    <asp:HiddenField ID="hdfmonto_max" runat="server" />
</asp:Content>
