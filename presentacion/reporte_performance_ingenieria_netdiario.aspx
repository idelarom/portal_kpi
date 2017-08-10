<%@ Page Title="" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="reporte_performance_ingenieria_netdiario.aspx.cs" Inherits="presentacion.reporte_performance_ingenieria_netdiario" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .RadGrid_Default .rgRow a, .RadGrid_Default .rgAltRow a, .RadGrid_Default .rgEditRow a{
            color:#72afd2;
        }
    </style>
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

        function ViewEmpleado(name,usr,usuario,prev,imple,sop,comp)
        {
            var nombre = document.getElementById('<%= hdfnombre.ClientID %>');
            var vusr = document.getElementById('<%= hdfusr.ClientID %>');
            var commando = document.getElementById('<%= hdfuserselected.ClientID %>');
            var vpreventa = document.getElementById('<%= hdfpreventa.ClientID %>');
            var vimple = document.getElementById('<%= hdfimplementacion.ClientID %>');
            var vsoporte = document.getElementById('<%= hdfsoporte.ClientID %>');
            var vcompro = document.getElementById('<%= hdfocompro.ClientID %>');
            nombre.value = name;
            vpuesto.value = usr;
            commando.value = usuario;
            vpreventa.value = prev;
            vimple.value = imple;
            vsoporte.value = sop;
            vcompro.value = comp;
            document.getElementById('<%= btnverempleadodetalles.ClientID%>').click();
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
                            &nbsp;<asp:Label ID="lblfechafin" runat="server" Text="Label"></asp:Label>
                        </h4>
                        
                    </div>
                </div>
                <div class="box-body">
                    <div class="table-responsive">
         <telerik:RadGrid ID="gridPerformance" AllowSorting="True"   runat="server" DataKeyNames="Login"
            HeaderStyle-ForeColor="#000000" HeaderStyle-Font-Bold="false" SortingSettings-SortToolTip="Ordenar Listado" 
            AutoGenerateColumns="False" HierarchySettings-CollapseTooltip="Ocultar Detalle"  
            HierarchySettings-ExpandTooltip="Ver Detalle" GroupPanelPosition="Top" 
            ResolvedRenderMode="Classic" Skin="Bootstrap" OnDetailTableDataBind="gridPerformance_DetailTableDataBind" OnNeedDataSource="gridPerformance_NeedDataSource"  >
  
            <HeaderStyle HorizontalAlign="Center" />
            <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>

            <SortingSettings SortToolTip="Ordenar Listado"></SortingSettings>

            <HierarchySettings ExpandTooltip="Ver Detalle" CollapseTooltip="Ocultar Detalle"></HierarchySettings>

            <ClientSettings>
             <Selecting AllowRowSelect="True" /> 
                
            </ClientSettings>
            <MasterTableView ShowFooter="false" TableLayout="Fixed" ItemStyle-Height="28px" AlternatingItemStyle-Height="28px" DataKeyNames="Login"   EnableNoRecordsTemplate="true" ClientDataKeyNames="Login"   
            ShowHeadersWhenNoRecords="true"   
            NoDetailRecordsText="No se han encontrado registros..." NoMasterRecordsText="No se han encontrado registros..." 
             FooterStyle-Font-Bold="true" CssClass="dvv table table-responsive table-condensed">
            <DetailTables>
            <telerik:GridTableView NoDetailRecordsText="No se han encontrado registros" HierarchyLoadMode="ServerOnDemand" HierarchyDefaultExpanded="false" EnableHierarchyExpandAll="true" 
            DataKeyNames="Login" Width="100%" runat="server">
            <ParentTableRelation>
                <telerik:GridRelationFields DetailKeyField="Login" MasterKeyField="Login"></telerik:GridRelationFields>
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
            DataKeyNames="Login" Width="100%" runat="server">
            <ParentTableRelation>
                <telerik:GridRelationFields DetailKeyField="Login" MasterKeyField="Login"></telerik:GridRelationFields>
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
            DataKeyNames="Login" Width="100%" runat="server">
            <ParentTableRelation>
                <telerik:GridRelationFields DetailKeyField="Login" MasterKeyField="Login"></telerik:GridRelationFields>
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
                <telerik:GridBoundColumn DataField="Login" HeaderText="Login" HeaderStyle-Width="100px" ItemStyle-Wrap="false" Visible="false">
                <HeaderStyle Width="100px"></HeaderStyle>

                <ItemStyle Wrap="False"></ItemStyle>
                </telerik:GridBoundColumn>
               <telerik:GridTemplateColumn ItemStyle-Wrap="false" UniqueName="" HeaderText="Ingeniero">
                 <ItemTemplate>                    
                    <%-- <asp:Literal ID="lbtUsr" runat="server" Text="<a href='#' onclick='Open();'></a>"></asp:Literal>--%>
                     <asp:HyperLink runat="server" CssClass="btn btn-link"  ID="lnkUsuario" Text='<%#Eval("Ingeniero") %>' Style=""  NavigateUrl="#" onclick=""></asp:HyperLink>
                 </ItemTemplate>
                 <HeaderStyle Width="300px"></HeaderStyle>
                <ItemStyle Wrap="False"></ItemStyle>
               </telerik:GridTemplateColumn>
               <%-- <telerik:GridBoundColumn DataField="Ingeniero" HeaderText="Ingeniero" HeaderStyle-Width="160px" ItemStyle-Wrap="false">
                <HeaderStyle Width="160px"></HeaderStyle>

                <ItemStyle Wrap="False"></ItemStyle>
                </telerik:GridBoundColumn>--%>
                <telerik:GridBoundColumn DataField="Soporte" HeaderText="Soporte" ItemStyle-Wrap="false">
                
                 <HeaderStyle Width="70px"></HeaderStyle>
                <ItemStyle Wrap="False"></ItemStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Preventa" HeaderText="Preventa" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right">
                
                    <HeaderStyle Width="70px"></HeaderStyle>
                <ItemStyle HorizontalAlign="Right" Wrap="False"></ItemStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Desarrollo_Negocio" HeaderText="Desarrollo de Negocio" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right">
               
                 <HeaderStyle Width="200px"></HeaderStyle>
                <ItemStyle HorizontalAlign="Right" Wrap="False"></ItemStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Capacitacion" HeaderText="Capacitación" ItemStyle-Wrap="false">
                
                  <HeaderStyle Width="95px"></HeaderStyle>
                <ItemStyle Wrap="False"></ItemStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Administrativas" HeaderText="Administrativas" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right">               
                   
                    <HeaderStyle Width="105px"></HeaderStyle>
                <ItemStyle HorizontalAlign="Right" Wrap="False"></ItemStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Incapacidad" HeaderText="Incapacidad" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right">
                
                  <HeaderStyle Width="95px"></HeaderStyle>
                <ItemStyle HorizontalAlign="Right" Wrap="False"></ItemStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Uptime" HeaderText="Uptime" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right">
                
                 <HeaderStyle Width="65px"></HeaderStyle>
                <ItemStyle HorizontalAlign="Right" Wrap="False"></ItemStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Proyectos_Internos_Facturables" HeaderText="Proyectos Internos Facturables" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right">
                
                 <HeaderStyle Width="250px"></HeaderStyle>
                <ItemStyle HorizontalAlign="Right" Wrap="False"></ItemStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Proyectos_Internos_No_Facturables" HeaderText="Proyectos Internos No Facturables" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right">
                
                 <HeaderStyle Width="250px"></HeaderStyle>
                <ItemStyle HorizontalAlign="Right" Wrap="False"></ItemStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Proyectos" HeaderText="Proyectos" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right">
               
                 <HeaderStyle Width="85px"></HeaderStyle>
                <ItemStyle HorizontalAlign="Right" Wrap="False"></ItemStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Tiempo_Personal" HeaderText="Tiempo Personal" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right">
                
                   <HeaderStyle Width="150px"></HeaderStyle>
                <ItemStyle HorizontalAlign="Right" Wrap="False"></ItemStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Usabilidad" HeaderText="Usabilidad" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right">
                
                <HeaderStyle Width="80px"></HeaderStyle>
                <ItemStyle HorizontalAlign="Right" Wrap="False"></ItemStyle>
                </telerik:GridBoundColumn>                                                        
                <telerik:GridBoundColumn DataField="Total_Horas" HeaderText="Total Horas" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right">
                
               <HeaderStyle Width="95px"></HeaderStyle>
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
                                 <div class="col-lg-12 col-xs-12">
                                    <h6><strong><i class="fa fa-users" aria-hidden="true"></i>&nbsp;Seleccione el empleado a consultar</strong>
                                        &nbsp; 
                                        <asp:CheckBox ID="cbxnoactivo" Text="Ver no Activos" Checked="true" runat="server" />
                                    </h6>
                                    <div class="input-group input-group-sm">
                                        <asp:TextBox
                                             onfocus="this.select();" ID="txtfilterempleado" CssClass=" form-control" 
                                            placeholder="Ingrese un filtro" runat="server" OnTextChanged="txtfilterempleado_TextChanged"></asp:TextBox>
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
                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12"  style="font-size:10px;">
                                    <h6><strong><i class="fa fa-calendar" aria-hidden="true"></i>&nbsp;Fecha Final</strong></h6>
                                    <telerik:RadDatePicker ID="rdpfechafinal" runat="server" Width="100%"  Skin="Bootstrap"></telerik:RadDatePicker>
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
       <div class="modal fade bs-example-modal-lg" tabindex="-1" id="ModalEmpleado" role="dialog" aria-labelledby="mySmallModalLabel" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg" role="document">
            <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnverempleadodetalles" EventName="Click" />
                </Triggers>
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">×</span></button>
                            <h4 class="modal-title">Detalles del empleado</h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-lg-12 col-md-12 col-sm-12">
                                    <div class="box box-widget widget-user">
                                        <!-- Add the bg color to the header using any of the bg-* classes -->
                                        <div class="widget-user-header bg-aqua-active">
                                            <h3 class="widget-user-username"> <asp:Label ID="lblnombre" runat="server" Text=""></asp:Label></h3>
                                            <h5 class="widget-user-desc">
                                                <asp:Label ID="lblpuesto" runat="server" Text=""></asp:Label></h5>
                                        </div>
                                        <div class="widget-user-image">
                                          <asp:Image ID="img_employee" runat="server" ImageUrl="~/img/user.png"
                                                CssClass="img-responsive img-circle" />
                                        </div>
                                        <div class="box-footer">
                                            <div class="row">
                                                <div class="col-sm-3 border-right">
                                                    <div class="description-block">
                                                        <h5 class="description-header"><asp:Label ID="lblprev" runat="server" Text=""></asp:Label></h5>
                                                        <span class="description-text">Preventa</span>
                                                    </div>
                                                    <!-- /.description-block -->
                                                </div>
                                                <!-- /.col -->
                                                <div class="col-sm-3 border-right">
                                                    <div class="description-block">
                                                        <h5 class="description-header"><asp:Label ID="lblimple" runat="server" Text=""></asp:Label></h5>
                                                        <span class="description-text">Implementación</span>
                                                    </div>
                                                    <!-- /.description-block -->
                                                </div>
                                                <!-- /.col -->
                                                <div class="col-sm-3">
                                                    <div class="description-block">
                                                        <h5 class="description-header"><asp:Label ID="lblsopo" runat="server" Text=""></asp:Label></h5>
                                                        <span class="description-text">Soporte</span>
                                                    </div>
                                                    <!-- /.description-block -->
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="description-block">
                                                        <h5 class="description-header"><asp:Label ID="lblcompro" runat="server" Text=""></asp:Label></h5>
                                                        <span class="description-text">Administracion</span>
                                                    </div>
                                                    <!-- /.description-block -->
                                                </div>
                                                <!-- /.col -->
                                            </div>
                                            <!-- /.row -->
                                        </div>
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
    <asp:Button ID="btnverempleadodetalles" runat="server" style="display:none;" Text="Button" OnClick="btnverempleadodetalles_Click" />
    <asp:HiddenField ID="hdfsessionid" runat="server" />
        <asp:HiddenField ID="hdfuserselected" runat="server" />
    <asp:HiddenField ID="hdfnombre" runat="server" />
    <asp:HiddenField ID="hdfusr" runat="server" />
    <asp:HiddenField ID="hdfpreventa" runat="server" />
    <asp:HiddenField ID="hdfimplementacion" runat="server" />
    <asp:HiddenField ID="hdfsoporte" runat="server" />
    <asp:HiddenField ID="hdfocompro" runat="server" />
</asp:Content>
