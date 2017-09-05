<%@ Page Title="Preventa" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="reporte_performance_preventa.aspx.cs" Inherits="presentacion.reporte_performance_preventa" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

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

         function ChanegdTextLoad()
        {
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

         function ViewDetailsCumpCompro(ingeniero, tipo, num)
         {
            if(num > 0){
                 var nombre = document.getElementById('<%= hdfingeniero.ClientID %>');
                var tipo_ = document.getElementById('<%= hdftipocompromisos.ClientID %>');
                nombre.value = ingeniero;
                tipo_.value = tipo;
                document.getElementById('<%= btnfiltrocumcompro.ClientID%>').click();
            }
            return true;
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
        <div class="col-lg-12" id="div_reporte" runat="server">
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
                                                <table class="dvv table table-responsive table-bordered table-condensed">
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
                                                        <asp:Repeater ID="repeater_cumpli_compromisos" runat="server">
                                                            <ItemTemplate>
                                                                <tr style="font-size: 12px">
                                                                    <td><%# Eval("Ingeniero") %></td>
                                                                    <td style="text-align:center;">
                                                                        <a style="cursor:pointer" onclick='<%# "return ViewDetailsCumpCompro("+@"""" + Eval("Login")+@""""+@",""Terminados a Tiempo"","+ Eval("Terminados a Tiempo")+");" %>'>
                                                                            <%# Eval("Terminados a Tiempo") %>
                                                                        </a>

                                                                    </td>  
                                                                    <td style="text-align:center;">
                                                                        <a style="cursor:pointer" onclick='<%# "return ViewDetailsCumpCompro("+@"""" + Eval("Login")+@""""+@",""Terminados Fuera de Tiempo"","+ Eval("Terminados Fuera de Tiempo")+");" %>'>
                                                                            <%# Eval("Terminados Fuera de Tiempo") %>
                                                                        </a>

                                                                    </td>
                                                                    <td style="text-align:center;">
                                                                        <a style="cursor:pointer" onclick='<%# "return ViewDetailsCumpCompro("+@"""" + Eval("Login")+@""""+@",""No Terminados Dentro de Tiempo"","+ Eval("No Terminados Dentro de Tiempo")+");" %>'>
                                                                            <%# Eval("No Terminados Dentro de Tiempo") %>
                                                                        </a>

                                                                    </td>
                                                                    <td style="text-align:center;">
                                                                        <a style="cursor:pointer" onclick='<%# "return ViewDetailsCumpCompro("+@"""" + Eval("Login")+@""""+@",""No Terminados Fuera de Tiempo"","+ Eval("No Terminados Fuera de Tiempo")+");" %>'>
                                                                            <%# Eval("No Terminados Fuera de Tiempo") %>
                                                                        </a>

                                                                    </td>
                                                                    <td style="text-align:center;">
                                                                        <a style="cursor:pointer" onclick='<%# "return ViewDetailsCumpCompro("+@"""" + Eval("Login")+@""""+@","""","+ Eval("Total de compromisos")+");" %>'>
                                                                            <%# Eval("Total de compromisos") %>
                                                                        </a>

                                                                    </td>
                                                                   
                                                                    <td style="text-align:center;"><%# Eval("Porcentaje de eficiencia") %></td>
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
                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                    <h6><strong><i class="fa fa-calendar" aria-hidden="true"></i>&nbsp;Fecha Inicial</strong></h6>
                                    <telerik:RadDatePicker ID="rdpfechainicial" runat="server" Width="100%" Skin="Bootstrap"></telerik:RadDatePicker>
                                </div>
                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12" style="font-size: 10px;">
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
                                            <databindings>
                                                <telerik:RadTreeNodeBinding Expanded="False"></telerik:RadTreeNodeBinding>
                                            </databindings>
                                        </telerik:RadTreeView>
                                    </div>

                                    <label>
                                        <asp:Label ID="lblcountlistempleados" runat="server" Text="0"></asp:Label></label>
                                </div>
                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                                    <br />
                                    <asp:LinkButton ID="lnklimpiar" OnClick="lnklimpiar_Click"
                                        CssClass="btn btn-danger btn-flat btn-xs" runat="server">
                                        Limpiar lista&nbsp;<i class="fa fa-trash" aria-hidden="true"></i>
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
                                                <tr style="font-size: 11px;">
                                                    <td>Num Oport</td>
                                                    <td>Cliente</td>
                                                    <td>Creado Por</td>
                                                    <td>Desc Comp</td>
                                                    <td>Tipo Comp</td>
                                                    <td>Tecnologia</td>
                                                    <td>Clasificador</td>
                                                    <td>Asignado A</td>
                                                    <td>Estatus</td>
                                                    <td>Horas</td>
                                                    <td>Prioridad</td>
                                                    <td>F Creacion</td>
                                                    <td>F Inicio</td>
                                                    <td>F Asignado</td>
                                                    <td>F Comp Ini</td>
                                                    <td>F Comp Final</td>
                                                    <td>F Terminado</td>
                                                    <td>En Asignar</td>
                                                    <td>Diferencia</td>
                                                    <td>Iniciar</td>
                                                    <td>Semana</td>
                                                    <td>Usuario Cierra</td>
                                                    <td>Fecha Cierre</td>
                                                    <td>Calificacion</td>
                                                    <td>Re Apertura</td>
                                                    <td>Re Open</td>
                                                    <td>cumple</td>
                                                    <td>Dif Dias Venta</td>
                                                    <td>Dif Dias Practica</td>
                                                    <td>Dif Dias Asignacion</td>
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
    <asp:Button ID="btnfiltrocumcompro" OnClick="btnfiltrocumcompro_Click" style="display:none" runat="server" Text="Button" />
    <asp:HiddenField ID="hdfingeniero" runat="server" />
    <asp:HiddenField ID="hdftipocompromisos" runat="server" />
</asp:Content>
