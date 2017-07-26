<%@ Page Title="DashBoard Bonos" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="reporte_dashboard_bonos_kpi.aspx.cs" Inherits="presentacion.reporte_dashboard_bonos_kpi" %>
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

          function ConfirmwidgetProyectoModal() {            
              $("#<%= lnkcargando.ClientID%>").show();
              $("#<%= lnkguardar.ClientID%>").hide();
              return true;
          }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <h3 class="page-header">Dashboard Bonos</h3>
        </div>
        <div class="col-lg-12">
            <asp:LinkButton ID="lnkfiltros" CssClass="btn btn-primary btn-flat" OnClick="lnkfiltros_Click" runat="server">
               <i class="fa fa-filter" aria-hidden="true"></i>&nbsp;Filtros
            </asp:LinkButton>
        </div>
    </div>
    <div class="row" id="div_reporte" runat="server" visible="false">

        <div class="col-lg-12">
            <br />
            <div class="box box-primary">
                <!-- /.box-header -->
                <div class="box-header">
                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                        <h6><strong><i class="fa fa-calendar" aria-hidden="true"></i>&nbsp;Fecha Inicial</strong></h6>
                    </div>
                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                        <h6><strong><i class="fa fa-calendar" aria-hidden="true"></i>&nbsp;Fecha Final</strong></h6>
                    </div>
                </div>
                <div class="box-body">
                    <div class="table-responsive">
                        <table class="dvv table no-margin table-condensed">                           
                            <thead>
                                <tr style="font-size: 11px;">
                                    <th style="min-width: 210px; text-align: left;" scope="col">Nombre</th>
                                    <th style="min-width: 70px; text-align: left;" scope="col">CC</th>
                                    <th style="min-width: 80px; text-align: center;" scope="col">Monto Bono</th>
                                    <th style="min-width: 65px; text-align: center;" scope="col">KPI Individual</th>
                                    <th style="min-width: 55px; text-align: center;" scope="col">KPI Grupo</th>
                                    <th style="min-width: 40px; text-align: center;" scope="col">% Individual</th>
                                    <th style="min-width: 40px; text-align: center;" scope="col">% Grupal</th>
                                    <th style="min-width: 65px; text-align: center;" scope="col">Bono</th>
                                    <th style="min-width: 70px; text-align: center;" scope="col">% Cump. Compromisos</th>
                                    <th style="min-width: 65px; text-align: center;" scope="col">Total Final</th>
                                    <th style="min-width: 65px; text-align: center;" scope="col">% Total Final</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="repeater_bonos" runat="server">
                                    <ItemTemplate>
                                        <tr style="font-size: 11px">
                                            <td><%# Eval("nombre") %></td>
                                            <td><%# Eval("CC") %></td>
                                            <td style="text-align: center;"><%# Convert.ToDecimal(Eval("amount")).ToString("C") %></td>
                                            <td style="text-align: center;"><%# Convert.ToDecimal(Eval("kpiind")).ToString("P2")  %></td>
                                            <td style="text-align: center;"><%# Convert.ToDecimal(Eval("kpigroup")).ToString("P2")  %></td>
                                            <td style="text-align: center;"><%# Convert.ToDecimal(Eval("porcind")).ToString("P0")  %></td>
                                            <td style="text-align: center;"><%# Convert.ToDecimal(Eval("porcgrupal")).ToString("P0")  %></td>
                                            <td style="text-align: center;"><%# Convert.ToDecimal(Eval("resultadototal")).ToString("C") %></td>
                                            <td style="text-align: center;"><%# (Convert.ToInt32(Eval("cumplimiento_compromisos")) * 100).ToString() %>&nbsp;%</td>
                                            <td style="text-align: center;"><%# Convert.ToDecimal(Eval("resultadototal")).ToString("C") %></td>
                                            <td style="text-align: center;"><%# (Convert.ToInt32(Eval("totalpor100")) * 100).ToString() %>&nbsp;%</td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                    </div>
                </div>
                <div class=" box-footer">
                    
                    <asp:LinkButton ID="lnkgenerarpdf" CssClass="btn btn-danger btn-flat" 
                        OnClick="lnkgenerarpdf_Click" runat="server">
                        <i class="fa fa-file-pdf-o" aria-hidden="true"></i>&nbsp;Exportar a PDF
                    </asp:LinkButton>
                    <asp:LinkButton ID="lnkgenerarexcel" CssClass="btn btn-success btn-flat" 
                        OnClick="lnkgenerarexcel_Click" runat="server">
                        <i class="fa fa-file-excel-o" aria-hidden="true"></i>&nbsp;Exportar a Excel
                    </asp:LinkButton>
                </div>
            </div>

        </div>
    </div>
    <div class="modal fade bs-example-modal-lg" tabindex="-1" id="myModal" role="dialog" aria-labelledby="mySmallModalLabel" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg" role="document">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="lnkfiltros" EventName="Click" />
                    <asp:PostBackTrigger ControlID="lnkguardar" />
                </Triggers>
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">×</span></button>
                            <h4 class="modal-title">Filtros</h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-lg-6 col-md-6 col-sm-12">
                                    <h6><strong><i class="fa fa-list" aria-hidden="true"></i>&nbsp;Tipo de Filtro</strong></h6>
                                    <asp:DropDownList ID="ddltipofiltro" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddltipofiltro_SelectedIndexChanged" runat="server">
                                        <asp:ListItem Value="1" Text="-Seleccion por Trimestre" Selected="True"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="-Selección libre"></asp:ListItem>
                                    </asp:DropDownList>
                                    <h5 style="color: #e53935"><strong>
                                        <asp:Label ID="lblinfotipofiltro" runat="server" Text="Permite seleccionar solo por rangos de trimestres."></asp:Label></strong></h5>

                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                    <h6><strong><i class="fa fa-calendar" aria-hidden="true"></i>&nbsp;Fecha Inicial</strong></h6>
                                    <asp:TextBox ID="txtfechainicio" AutoPostBack="true"
                                        OnTextChanged="txtfechainicio_TextChanged" ReadOnly="false" CssClass="form-control" TextMode="Date" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                    <h6><strong><i class="fa fa-calendar" aria-hidden="true"></i>&nbsp;Fecha Final</strong></h6>
                                    <asp:TextBox ID="txtfechafinal" ReadOnly="true" CssClass="form-control" TextMode="Date" runat="server"></asp:TextBox>
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
    <asp:HiddenField ID="hdfsessionid" runat="server" />
</asp:Content>
