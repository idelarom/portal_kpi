<%@ Page Title="" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="reporte_proyectos.aspx.cs" Inherits="presentacion.reporte_proyectos" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script type="text/javascript">
         //declaramos los objetos de tipo load desde el inicio (load oscuro)
         var opts2 = {
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
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div id="div_body_reportedashboard">

        <div class="row">
            <div class="col-lg-12">
                <h3 class="page-header">Dashboard Proyectos</h3>
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
                <div class="box box-danger">
                    <div class="box-body">
                        <h4 class="box-title"><strong><i class="fa fa-calendar" aria-hidden="true"></i>&nbsp;Fecha Inicial:</strong>
                            &nbsp;<asp:Label ID="lblfechaini" runat="server" Text="Label"></asp:Label>
                            &nbsp;<strong><i class="fa fa-calendar" aria-hidden="true"></i>&nbsp;Fecha Final:</strong>
                            &nbsp;<asp:Label ID="lblfechafin" runat="server" Text="Label"></asp:Label>
                        </h4>
                        <div class=" table table-responsive">
                            <table class="dvv table table-responsive table-condensed">
                                <thead>
                                    <tr style="font-size: 11px;">
                                        <th style="min-width: 100px; text-align: left;" scope="col">Usuario</th>
                                        <th style="min-width: 200px; text-align: left;" scope="col">Empleado</th>
                                        <th style="min-width: 250px; text-align: center;" scope="col">Proyecto</th>
                                        <%--<th style="min-width: 250px; text-align: center;" scope="col">Descripcion</th>--%>
                                        <th style="min-width: 150px; text-align: center;" scope="col">Tecnologia</th>
                                        <th style="min-width: 150px; text-align: center;" scope="col">Periodo</th>
                                        <th style="min-width: 100px; text-align: left;" scope="col">ClaveOP</th>
                                        <th style="min-width: 100px; text-align: left;" scope="col">Folio pmtracker</th>
                                        <th style="min-width: 100px; text-align: center;" scope="col">CPED</th>
                                        <th style="min-width: 100px; text-align: center;" scope="col">Monto USD</th>
                                        <th style="min-width: 100px; text-align: center;" scope="col">Monto MN</th>
                                        <th style="min-width: 100px; text-align: center;" scope="col">Tipo de moneda</th>
                                        <th style="min-width: 100px; text-align: center;" scope="col">Fecha registro</th>
                                        <th style="min-width: 100px; text-align: center;" scope="col">fecha inicial</th>
                                        <th style="min-width: 100px; text-align: center;" scope="col">Fecha final</th>
                                        <th style="min-width: 100px; text-align: center;" scope="col">Estatus</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="repeater_reporte_proyectos" runat="server">
                                        <ItemTemplate>
                                            <tr style="font-size: 11px">                                                
                                                <td style="text-align: center;"><%# Eval("usuario") %></td>
                                                <td style="text-align: center;"><%# Eval("empleado") %></td>
                                                <td>
                                                    <a style="cursor: pointer;" onclick='<%# "return ViewEmpleado("+@"""" + Eval("id_proyecto") + @"""" + ");" %>'>
                                                        <%# Eval("proyecto") %>
                                                    </a>
                                                </td>
                                               <%-- <td style="text-align: center;"><%# Eval("descripcion") %></td>--%>
                                                <td style="text-align: center;"><%# Eval("tecnologia") %></td>
                                                <td style="text-align: center;"><%# Eval("periodo") %></td>
                                                <td style="text-align: center;"><%# Eval("cveoport") %></td>
                                                <td style="text-align: center;"><%# Eval("folio_pmt") %></td>
                                                <td style="text-align: center;"><%# Eval("cped") %></td>
                                                <td style="text-align: center;"><%# Eval("costo_usd") %></td>
                                                <td style="text-align: center;"><%# Eval("costo_mn") %></td>
                                                <td style="text-align: center;"><%# Eval("tipo_moneda") %></td>
                                                <td style="text-align: center;"><%# Eval("fecha_registro") %></td>
                                                <td style="text-align: center;"><%# Eval("fecha_inicio") %></td>
                                                <td style="text-align: center;"><%# Eval("fecha_fin") %></td>
                                                <td style="text-align: center;"><%# Eval("estatus") %></td>
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
                           <%-- <div class="row">
                                 <div class="col-lg-12 col-xs-12">
                                    <h6><strong><i class="fa fa-users" aria-hidden="true"></i>&nbsp;Seleccione el empleado a consultar</strong>
                                        &nbsp; 
                                        <asp:CheckBox ID="cbxnoactivo" Text="Ver no Activos" Checked="true" runat="server" />
                                    </h6>
                                    <div class="input-group input-group-sm" runat="server" id="div_filtro_empleados">
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
                            </div>--%>
                            <div class="row">
                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                                    <h6><strong><i class="fa fa-calendar" aria-hidden="true"></i>&nbsp;Fecha Inicial</strong></h6>
                                    <telerik:RadDatePicker ID="rdpfechainicial" runat="server" Width="100%" Skin="Bootstrap"></telerik:RadDatePicker>                                     
                                </div>
                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6"  style="font-size:10px;">
                                    <h6><strong><i class="fa fa-calendar" aria-hidden="true"></i>&nbsp;Fecha Final</strong></h6>
                                    <telerik:RadDatePicker ID="rdpfechafinal" runat="server" Width="100%"  Skin="Bootstrap"></telerik:RadDatePicker>
                                </div>
                                <div class="col-lg-3 col-md-3 col-sm-12">
                        <h5><strong><i class="fa fa-braille" aria-hidden="true"></i>&nbsp;Filtro estatus</strong></h5>
                            <asp:DropDownList ID="ddlstatus" MaxLength="250" CssClass=" form-control" runat="server"></asp:DropDownList>
                        </div>
                            </div>
                            <%--<div class="row">
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
                            </div>--%>
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
        <%--<div class="modal fade bs-example-modal-lg" tabindex="-1" id="ModalEmpleado" role="dialog" aria-labelledby="mySmallModalLabel" >
            <div class="modal-dialog modal-lg" role="document">
                <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnverempleadodetalles" EventName="Click" />
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-content" id="modal_cn">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">×</span></button>
                                <h4 class="modal-title">Detalles del empleado</h4>
                            </div>
                            <div class="modal-body">
                                <div class="row" id="modal_bdy" runat="server">
                                    <div class="col-lg-12 col-md-12 col-sm-12">
                                        <div class="box box-widget widget-user">
                                            <!-- Add the bg color to the header using any of the bg-* classes -->
                                            <div class="widget-user-header bg-aqua-active">
                                                <h3 class="widget-user-username">
                                                    <asp:Label ID="lblnombre" runat="server" Text=""></asp:Label></h3>
                                                <h5 class="widget-user-desc">
                                                    <asp:Label ID="lblpuesto" runat="server" Text=""></asp:Label></h5>
                                            </div>
                                            <div class="widget-user-image">
                                                <asp:Image ID="img_employee" runat="server" ImageUrl="~/img/user.png"
                                                    CssClass="img-responsive img-circle" />
                                            </div>
                                            <br /><br />
                                            <div class="box-footer">
                                                <div class="row">
                                                      <div class=" table table-responsive" style="width:100%; height:250px; overflow: scroll;">
                                                        <table class="table table-responsive table-condensed">
                                                            <thead>
                                                                <tr style="font-size: 11px;">
                                                                    <th style="min-width: 50px; text-align: center;" scope="col">#Oport.</th>
                                                                    <th style="min-width: 50px; text-align: center;" scope="col">#Comp.</th>
                                                                    <th style="min-width: 400px;" scope="col">Cliente</th>
                                                                    <th style="min-width: 250px;" scope="col">Creado Por</th>
                                                                    <th style="min-width: 400px;" scope="col">Compromiso</th>
                                                                    <th style="min-width: 150px; text-align: center;" scope="col">T.Compromiso</th>
                                                                    <th style="min-width: 250px; text-align: center;" scope="col">Asignado</th>
                                                                    <th style="min-width: 150px; text-align: center;" scope="col">Estatus</th>
                                                                    <th style="min-width: 150px; text-align: center;" scope="col">F.Creacion</th>
                                                                    <th style="min-width: 150px; text-align: center;" scope="col">F.Inicio</th>
                                                                    <th style="min-width: 150px; text-align: center;" scope="col">F.Asignado</th>
                                                                    <th style="min-width: 150px; text-align: center;" scope="col">F.Inicial</th>
                                                                    <th style="min-width: 150px; text-align: center;" scope="col">F.Final</th>
                                                                    <th style="min-width: 150px; text-align: center;" scope="col">F.Terminado</th>
                                                                    <th style="min-width: 50px; text-align: center;" scope="col">Cumplido</th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <asp:Repeater ID="repeater_compromisos_detalle" runat="server">
                                                                    <ItemTemplate>
                                                                        <tr style="font-size: 11px">
                                                                             <td style="text-align: center;"><%# Eval("NumOport") %></td>
                                                                             <td style="text-align: center;"><%# Eval("NumComp") %></td>
                                                                             <td style=""><%# Eval("Cliente") %></td>
                                                                             <td ><%# Eval("CreadorPor") %></td>
                                                                             <td style=""><%# Eval("Compromiso") %></td>
                                                                             <td style="text-align: center;"><%# Eval("TipoCompromiso") %></td>
                                                                             <td style="text-align: center;"><%# Eval("Asignado") %></td>
                                                                             <td style="text-align: center;"><%# Eval("Estatus") %></td>
                                                                             <td style="text-align: center;"><%# Eval("FechaCreacion") %></td>
                                                                             <td style="text-align: center;"><%# Eval("FechaInicio") %></td>
                                                                             <td style="text-align: center;"><%# Eval("FechaAsignado") %></td>
                                                                             <td style="text-align: center;"><%# Eval("FechaCompInicial") %></td>
                                                                             <td style="text-align: center;"><%# Eval("FechaCompFinal") %></td>
                                                                             <td style="text-align: center;"><%# Eval("FechaTerminado") %></td>
                                                                             <td style="text-align: center;"><%# Eval("Cumplido") %></td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:Repeater>
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                                <!-- /.row -->
                                            </div>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>--%>
        <asp:Button ID="btnverempleadodetalles" runat="server" Text="Button"  style="display:none"/>
        <asp:HiddenField ID="hdfsessionid" runat="server" />
        <asp:HiddenField ID="hdfuserselected" runat="server" />
        <asp:HiddenField ID="hdfnombre" runat="server" />
        <asp:HiddenField ID="hdffechainicio" runat="server" />
        <asp:HiddenField ID="hdffechafin" runat="server" />
    </div>
</asp:Content>
