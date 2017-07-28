﻿<%@ Page Title="DashBoard Bonos" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="reporte_dashboard_bonos_kpi.aspx.cs" Inherits="presentacion.reporte_dashboard_bonos_kpi" %>
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
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <h3 class="page-header">Dashboard Bonos</h3>
        </div>
        <div class="col-lg-12">
                <asp:LinkButton OnClientClick="return false;" ID="nkcargandofiltros" CssClass="btn btn-primary btn-flat" runat="server" Style="display: none;">
                                            <i class="fa fa-refresh fa-spin fa-fw"></i>
                                            <span class="sr-only">Loading...</span>&nbsp;Cargando filtros
                            </asp:LinkButton>
            <asp:LinkButton ID="lnkfiltros" CssClass="btn btn-primary btn-flat" OnClick="lnkfiltros_Click" runat="server"
                OnClientClick="return Carganodfiltros();" >
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
                        <h4 class="box-title"><strong><i class="fa fa-calendar" aria-hidden="true"></i>&nbsp;Fecha Inicial:</strong>
                            &nbsp;<asp:Label ID="lblfechaini" runat="server" Text="Label"></asp:Label>
                            &nbsp;<strong><i class="fa fa-calendar" aria-hidden="true"></i>&nbsp;Fecha Final:</strong>
                            &nbsp;<asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                        </h4>
                        
                    </div>
                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                        <h5><strong><i class="fa fa-calendar" aria-hidden="true"></i>&nbsp;Fecha Final:</strong>
                            &nbsp;<asp:Label ID="lblfechafin" runat="server" Text="Label"></asp:Label>
                        </h5>
                        
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
                                                    <td><%# Eval("Nombre") %></td>
                                                    <td><%# Eval("CC") %></td>
                                                    <td style="text-align: center;"><%# Eval("Monto Bono") %></td>
                                                    <td style="text-align: center;"><%# Eval("KPI Individual") %></td>
                                                    <td style="text-align: center;"><%# Eval("KPI Grupo") %></td>
                                                    <td style="text-align: center;"><%# Eval("% Individual") %></td>
                                                    <td style="text-align: center;"><%# Eval("% Grupal") %></td>
                                                    <td style="text-align: center;"><%# Eval("Bono") %></td>
                                                    <td style="text-align: center;"><%# Eval("% Cump") %></td>
                                                    <td style="text-align: center;"><%# Eval("Total Final") %></td>
                                                    <td style="text-align: center;"><%# Eval("% Total Final") %></td>
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
                                <div class="col-lg-6 col-md-6 col-sm-12">
                                    <h6 ><strong><i class="fa fa-list" aria-hidden="true"></i>&nbsp;Tipo de Filtro</strong></h6>
                                    <asp:DropDownList ID="ddltipofiltro" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddltipofiltro_SelectedIndexChanged" runat="server">
                                        <asp:ListItem Value="1" Text="-Seleccion por Trimestre" Selected="True"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="-Selección libre"></asp:ListItem>
                                    </asp:DropDownList>
                                    <h6 style="color: #e53935"><strong>
                                        <asp:Label ID="lblinfotipofiltro" runat="server" Text="Permite seleccionar solo por rangos de trimestres."></asp:Label></strong></h6>

                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                    <h6><strong><i class="fa fa-calendar" aria-hidden="true"></i>&nbsp;Fecha Inicial</strong></h6>
                                    <asp:DropDownList  Visible="true" ID="ddltrimestres" CssClass="form-control"
                                         AutoPostBack="true" OnSelectedIndexChanged="ddltrimestres_SelectedIndexChanged" runat="server"></asp:DropDownList>
                                    <asp:TextBox ID="txtfechainicio" Visible="false" AutoPostBack="true"
                                        OnTextChanged="txtfechainicio_TextChanged" ReadOnly="false" CssClass="form-control" TextMode="Date" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12"  style="font-size:10px;">
                                    <h6><strong><i class="fa fa-calendar" aria-hidden="true"></i>&nbsp;Fecha Final</strong></h6>
                                    <asp:TextBox ID="txtfechafinal" ReadOnly="true" CssClass="form-control" TextMode="Date" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12 col-xs-12">
                                    <h6><strong><i class="fa fa-users" aria-hidden="true"></i>&nbsp;Seleccione el empleado a consultar</strong>
                                        &nbsp;  <asp:CheckBox ID="cbxnoactivo" Text="Ver no Activos" Checked="true" runat="server" />
                                    </h6>
                                    <asp:DropDownList Visible="true" ID="ddlempleado_a_consultar" CssClass="form-control"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlempleado_a_consultar_SelectedIndexChanged" runat="server">
                                    </asp:DropDownList>
                                  
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-6 col-md-6 col-sm-12">
                                    <br />
                                    <asp:LinkButton ID="lnkagregarseleccion" OnClick="lnkagregarseleccion_Click" 
                                        CssClass="btn btn-primary btn-flat btn-sm" runat="server">
                                        Agregar selección&nbsp;<i class="fa fa-plus" aria-hidden="true"></i>
                                    </asp:LinkButton>                                   
                                    <asp:LinkButton ID="lnkagregartodos" OnClick="lnkagregartodos_Click" 
                                        CssClass="btn btn-primary btn-flat btn-sm" runat="server">
                                        Todos&nbsp;<i class="fa fa-plus" aria-hidden="true"></i>
                                    </asp:LinkButton>
                                     <div style="max-height: 130px; height: 130px; overflow: scroll;">
                                        <telerik:RadTreeView RenderMode="Lightweight" ID="rtvListEmpleado" runat="server" Width="100%"
                                            Style="background-color: white; font-size: 10px;" Skin="Bootstrap">
                                            <DataBindings>
                                                <telerik:RadTreeNodeBinding Expanded="False"></telerik:RadTreeNodeBinding>
                                            </DataBindings>
                                        </telerik:RadTreeView>
                                    </div>
                                    
                                    <label>
                                        <asp:Label ID="lblcountlistempleados" runat="server" Text="0"></asp:Label></label>
                                </div>
                                <div class="col-lg-6 col-md-6 col-sm-12">
                                    <br />                                
                                    <asp:LinkButton ID="lnklimpiar" OnClick="lnklimpiar_Click" 
                                        CssClass="btn btn-danger btn-flat btn-sm" runat="server">
                                        Limpiar lista&nbsp;<i class="fa fa-trash" aria-hidden="true"></i>
                                    </asp:LinkButton>                      
                                    <asp:LinkButton ID="lnkeliminarselecion" OnClick="lnkeliminarselecion_Click"
                                        CssClass="btn btn-danger btn-flat btn-sm" runat="server">
                                        Eliminar seleccion&nbsp;<i class="fa fa-trash" aria-hidden="true"></i>
                                    </asp:LinkButton>
                                    <div style="max-height: 130px; height: 130px; overflow: scroll;">
                                        <telerik:RadListBox RenderMode="Lightweight" runat="server" ID="rdtselecteds" Width="100%"
                                            Style="font-size: 10px" Skin="Bootstrap" SelectionMode="Multiple" Sort="Ascending">
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
    <asp:HiddenField ID="hdfsessionid" runat="server" />
</asp:Content>