<%@ Page Title="" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="reporte_performance_ingenieria_netdiario.aspx.cs" Inherits="presentacion.reporte_performance_ingenieria_netdiario" %>

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
            <h3 class="page-header">Performance Ingenieria</h3>
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
         <telerik:RadGrid ID="gridPerformance" AllowSorting="True"   runat="server" DataKeyNames="id_request_bond"
            HeaderStyle-ForeColor="#000000" HeaderStyle-Font-Bold="false" SortingSettings-SortToolTip="Ordenar Listado" 
            AutoGenerateColumns="False" HierarchySettings-CollapseTooltip="Ocultar Detalle"  
            HierarchySettings-ExpandTooltip="Ver Detalle" GroupPanelPosition="Top" 
            ResolvedRenderMode="Classic" Skin="Default" OnDetailTableDataBind="gridPerformance_DetailTableDataBind" OnNeedDataSource="gridPerformance_NeedDataSource"  >
  
            <HeaderStyle HorizontalAlign="Center" />
            <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>

            <SortingSettings SortToolTip="Ordenar Listado"></SortingSettings>

            <HierarchySettings ExpandTooltip="Ver Detalle" CollapseTooltip="Ocultar Detalle"></HierarchySettings>

            <ClientSettings>
             <Selecting AllowRowSelect="True" /> 
                <ClientEvents OnRowSelected = "CancelarEditar" />
            </ClientSettings>
            <MasterTableView ShowFooter="false" TableLayout="Fixed" ItemStyle-Height="28px" AlternatingItemStyle-Height="28px" DataKeyNames="id_request_bond"   EnableNoRecordsTemplate="true" ClientDataKeyNames="id_request_bond,Estatus,bond_name,Monto,Ocurrencias,OcurrenciasPend,CC_Cargo"   
            ShowHeadersWhenNoRecords="true"   
            NoDetailRecordsText="No se han encontrado registros..." NoMasterRecordsText="No se han encontrado registros..." 
             FooterStyle-Font-Bold="true"    >
            <DetailTables>
            <telerik:GridTableView NoDetailRecordsText="No se han encontrado registros" HierarchyLoadMode="ServerOnDemand" HierarchyDefaultExpanded="false" EnableHierarchyExpandAll="true" 
            DataKeyNames="Login" Width="100%" runat="server" >
            <ParentTableRelation>
                <telerik:GridRelationFields DetailKeyField="Login" MasterKeyField="id_request_bond"></telerik:GridRelationFields>
            </ParentTableRelation>
             <Columns>
                <telerik:GridBoundColumn SortExpression="Folio_OP" HeaderText="Folio OP" HeaderButtonType="TextButton" DataField="Folio_OP" UniqueName="Folio_OP"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn SortExpression="Folio_Proyecto" HeaderText="Folio_Proyecto" HeaderButtonType="TextButton" DataField="Folio_Proyecto" UniqueName="Folio_Proyecto"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn SortExpression="Cliente" HeaderText="Cliente" HeaderButtonType="TextButton" DataField="Cliente" UniqueName="Cliente"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn SortExpression="Monto_OP" HeaderText="Monto OP" HeaderButtonType="TextButton" DataField="Monto_OP" UniqueName="Monto_OP"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn SortExpression="Margen_OP" HeaderText="Margen OP" HeaderButtonType="TextButton" DataField="Margen_OP" UniqueName="Margen_OP"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn SortExpression="Total_Hrs_OP" HeaderText="Total Hrs OP" HeaderButtonType="TextButton" DataField="Total_Hrs_OP" UniqueName="Total_Hrs_OP"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn SortExpression="Total_Hrs_Ing" HeaderText="Total Hrs Ing" HeaderButtonType="TextButton" DataField="Total_Hrs_Ing" UniqueName="Total_Hrs_Ing"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn SortExpression="Costo" HeaderText="Costo Total Ing" HeaderButtonType="TextButton" DataField="Costo" UniqueName="Costo"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn SortExpression="Viaticos_Total_OP" HeaderText="Viaticos Total OP" HeaderButtonType="TextButton" DataField="Viaticos_Total_OP" UniqueName="Viaticos_Total_OP"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn SortExpression="Viaticos_Total_Ing" HeaderText="Viaticos Total Ing" HeaderButtonType="TextButton" DataField="Viaticos_Total_Ing" UniqueName="Viaticos_Total_Ing"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn SortExpression="Estatus" HeaderText="Estatus" HeaderButtonType="TextButton" DataField="Estatus" UniqueName="Estatus"></telerik:GridBoundColumn>                                                                        
            </Columns>
            </telerik:GridTableView>

                 <telerik:GridTableView NoDetailRecordsText="No se han encontrado registros" HierarchyLoadMode="ServerOnDemand" HierarchyDefaultExpanded="false" EnableHierarchyExpandAll="true" 
            DataKeyNames="Login" Width="100%" runat="server" >
            <ParentTableRelation>
                <telerik:GridRelationFields DetailKeyField="id_request_bond" MasterKeyField="id_request_bond"></telerik:GridRelationFields>
            </ParentTableRelation>
             <Columns>
                <telerik:GridBoundColumn SortExpression="Folio_Proyecto" HeaderText="Folio Proyecto" HeaderButtonType="TextButton" DataField="Folio_Proyecto" UniqueName="Folio_Proyecto"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn SortExpression="Folio_OP" HeaderText="Folio OP" HeaderButtonType="TextButton" DataField="Folio_OP" UniqueName="Folio_OP"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn SortExpression="Nombre_Cliente" HeaderText="Cliente" HeaderButtonType="TextButton" DataField="Nombre_Cliente" UniqueName="Nombre_Cliente"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn SortExpression="Total_Hrs_Proyecto" HeaderText="Total_Hrs_Proyecto" HeaderButtonType="TextButton" DataField="Total_Hrs_Proyecto" UniqueName="Total_Hrs_Proyecto"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn SortExpression="Total_Hrs_Ing" HeaderText="Total_Hrs_Ing" HeaderButtonType="TextButton" DataField="Total_Hrs_Ing" UniqueName="Total_Hrs_Ing"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn SortExpression="Costo_Ing" HeaderText="Costo_Ing" HeaderButtonType="TextButton" DataField="Costo_Ing" UniqueName="Costo_Ing"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn SortExpression="Viaticos_Total_Proyecto" HeaderText="Viaticos_Total_Proyecto" HeaderButtonType="TextButton" DataField="Viaticos_Total_Proyecto" UniqueName="Viaticos_Total_Proyecto"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn SortExpression="Viaticos_Total_Ing" HeaderText="Viaticos_Total_Ing" HeaderButtonType="TextButton" DataField="Viaticos_Total_Ing" UniqueName="Viaticos_Total_Ing"></telerik:GridBoundColumn>                                                                        
                <telerik:GridBoundColumn SortExpression="Estatus" HeaderText="Estatus" HeaderButtonType="TextButton" DataField="Estatus" UniqueName="Estatus"></telerik:GridBoundColumn>                                                                        
            </Columns>
            </telerik:GridTableView>

                 <telerik:GridTableView NoDetailRecordsText="No se han encontrado registros" HierarchyLoadMode="ServerOnDemand" HierarchyDefaultExpanded="false" EnableHierarchyExpandAll="true" 
            DataKeyNames="Login" Width="100%" runat="server" >
            <ParentTableRelation>
                <telerik:GridRelationFields DetailKeyField="id_request_bond" MasterKeyField="id_request_bond"></telerik:GridRelationFields>
            </ParentTableRelation>
             <Columns>
                <telerik:GridBoundColumn SortExpression="Incidente" HeaderText="Incidente" HeaderButtonType="TextButton" DataField="Incidente" UniqueName="Incidente"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn SortExpression="Cliente" HeaderText="Cliente" HeaderButtonType="TextButton" DataField="Cliente" UniqueName="Cliente"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn SortExpression="Contrato" HeaderText="Contrato" HeaderButtonType="TextButton" DataField="Contrato" UniqueName="Contrato"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn SortExpression="Prioridad" HeaderText="Prioridad" HeaderButtonType="TextButton" DataField="Prioridad" UniqueName="Prioridad"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn SortExpression="SLAs_Atencion" HeaderText="SLAs_Atencion" HeaderButtonType="TextButton" DataField="SLAs_Atencion" UniqueName="SLAs_Atencion"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn SortExpression="SLAs_Solucion" HeaderText="SLAs_Solucion" HeaderButtonType="TextButton" DataField="SLAs_Solucion" UniqueName="SLAs_Solucion"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn SortExpression="Tipo_Servicio" HeaderText="Tipo_Servicio" HeaderButtonType="TextButton" DataField="Tipo_Servicio" UniqueName="Tipo_Servicio"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn SortExpression="Total_Hrs_Incidente" HeaderText="Total_Hrs_Incidente" HeaderButtonType="TextButton" DataField="Total_Hrs_Incidente" UniqueName="Total_Hrs_Incidente"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn SortExpression="Costo" HeaderText="Costo" HeaderButtonType="TextButton" DataField="Costo" UniqueName="Costo"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn SortExpression="Viaticos_Total_Incidente" HeaderText="Viaticos_Total_Incidente" HeaderButtonType="TextButton" DataField="Viaticos_Total_Incidente" UniqueName="Viaticos_Total_Incidente"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn SortExpression="Viaticos_Total_Ing" HeaderText="Viaticos_Total_Ing" HeaderButtonType="TextButton" DataField="Viaticos_Total_Ing" UniqueName="Viaticos_Total_Ing"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn SortExpression="Estatus" HeaderText="Estatus" HeaderButtonType="TextButton" DataField="Estatus" UniqueName="Estatus"></telerik:GridBoundColumn>                                                                        
            </Columns>
            </telerik:GridTableView>
            </DetailTables>

    <ItemStyle Height="28px"></ItemStyle>

  <AlternatingItemStyle Height="28px"></AlternatingItemStyle>
        <FooterStyle Font-Bold="true"  />      
                <RowIndicatorColumn Visible="False">
                </RowIndicatorColumn>
                <ExpandCollapseColumn Created="True">
                </ExpandCollapseColumn> 
          <Columns> 
                <telerik:GridBoundColumn DataField="Login" HeaderText="Login" HeaderStyle-Width="100px" ItemStyle-Wrap="false">
                <HeaderStyle Width="100px"></HeaderStyle>

                <ItemStyle Wrap="False"></ItemStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Ingeniero" HeaderText="Ingeniero" HeaderStyle-Width="160px" ItemStyle-Wrap="false">
                <HeaderStyle Width="160px"></HeaderStyle>

                <ItemStyle Wrap="False"></ItemStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Soporte" HeaderText="Soporte" HeaderStyle-Width="60px" ItemStyle-Wrap="false">
                <HeaderStyle Width="60px"></HeaderStyle>

                <ItemStyle Wrap="False"></ItemStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Preventa" HeaderText="Preventa" DataFormatString="{0:C}" HeaderStyle-Width="60px" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right">
                <HeaderStyle Width="60px"></HeaderStyle>

                <ItemStyle HorizontalAlign="Right" Wrap="False"></ItemStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Desarrollo_Negocio" HeaderText="Desarrollo de Negocio" DataFormatString="{0:C}" HeaderStyle-Width="60px" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right">
                <HeaderStyle Width="60px"></HeaderStyle>

                <ItemStyle HorizontalAlign="Right" Wrap="False"></ItemStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Capacitacion" HeaderText="Capacitación" HeaderStyle-Width="60px" ItemStyle-Wrap="false">
                <HeaderStyle Width="60px"></HeaderStyle>

                <ItemStyle Wrap="False"></ItemStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Administrativas" HeaderText="Administrativas" DataFormatString="{0:C}" HeaderStyle-Width="60px" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right">
                <HeaderStyle Width="60px"></HeaderStyle>

                <ItemStyle HorizontalAlign="Right" Wrap="False"></ItemStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Incapacidad" HeaderText="Incapacidad" DataFormatString="{0:C}" HeaderStyle-Width="60px" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right">
                <HeaderStyle Width="60px"></HeaderStyle>

                <ItemStyle HorizontalAlign="Right" Wrap="False"></ItemStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Uptime" HeaderText="Uptime" DataFormatString="{0:C}" HeaderStyle-Width="60px" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right">
                <HeaderStyle Width="60px"></HeaderStyle>

                <ItemStyle HorizontalAlign="Right" Wrap="False"></ItemStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Proyectos_Internos_Facturables" HeaderText="Proyectos Internos Facturables" DataFormatString="{0:C}" HeaderStyle-Width="60px" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right">
                <HeaderStyle Width="60px"></HeaderStyle>

                <ItemStyle HorizontalAlign="Right" Wrap="False"></ItemStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Proyectos_Internos_No_Facturables" HeaderText="Proyectos_Internos_No_Facturables" DataFormatString="{0:C}" HeaderStyle-Width="60px" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right">
                <HeaderStyle Width="60px"></HeaderStyle>

                <ItemStyle HorizontalAlign="Right" Wrap="False"></ItemStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Proyectos" HeaderText="Proyectos" DataFormatString="{0:C}" HeaderStyle-Width="60px" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right">
                <HeaderStyle Width="60px"></HeaderStyle>

                <ItemStyle HorizontalAlign="Right" Wrap="False"></ItemStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Tiempo_Personal" HeaderText="Tiempo_Personal" DataFormatString="{0:C}" HeaderStyle-Width="60px" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right">
                <HeaderStyle Width="60px"></HeaderStyle>

                <ItemStyle HorizontalAlign="Right" Wrap="False"></ItemStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Usabilidad" HeaderText="Usabilidad" DataFormatString="{0:C}" HeaderStyle-Width="60px" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right">
                <HeaderStyle Width="60px"></HeaderStyle>

                <ItemStyle HorizontalAlign="Right" Wrap="False"></ItemStyle>
                </telerik:GridBoundColumn>                                                        
                <telerik:GridBoundColumn DataField="Total_Horas" HeaderText="Total Horas" DataFormatString="{0:C}" HeaderStyle-Width="60px" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right">
                <HeaderStyle Width="60px"></HeaderStyle>

                <ItemStyle HorizontalAlign="Right" Wrap="False"></ItemStyle>
                </telerik:GridBoundColumn> 
         </Columns>         
               <NoRecordsTemplate>
              <div style="height:30px" >No se han encontrado registros...</div>
               </NoRecordsTemplate>
            </MasterTableView>
        </telerik:RadGrid>
                    </div>
                </div>
                <div class=" box-footer">
                    
                   <%-- <asp:LinkButton ID="lnkgenerarpdf" CssClass="btn btn-danger btn-flat" 
                        OnClick="lnkgenerarpdf_Click" runat="server">
                        <i class="fa fa-file-pdf-o" aria-hidden="true"></i>&nbsp;Exportar a PDF
                    </asp:LinkButton>
                    <asp:LinkButton ID="lnkgenerarexcel" CssClass="btn btn-success btn-flat" 
                        OnClick="lnkgenerarexcel_Click" runat="server">
                        <i class="fa fa-file-excel-o" aria-hidden="true"></i>&nbsp;Exportar a Excel
                    </asp:LinkButton>--%>
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
                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                    <h6><strong><i class="fa fa-calendar" aria-hidden="true"></i>&nbsp;Fecha Inicial</strong></h6>
                                    <telerik:RadDatePicker ID="rdpfechainicial" runat="server" CssClass =" form-control"></telerik:RadDatePicker>
                                </div>
                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12"  style="font-size:10px;">
                                    <h6><strong><i class="fa fa-calendar" aria-hidden="true"></i>&nbsp;Fecha Final</strong></h6>
                                    <telerik:RadDatePicker ID="rdpfechafinal" runat="server" CssClass =" form-control"></telerik:RadDatePicker>
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
