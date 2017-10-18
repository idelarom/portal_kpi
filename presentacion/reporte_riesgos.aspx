<%@ Page Title="" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="reporte_riesgos.aspx.cs" Inherits="presentacion.reporte_riesgos" %>
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
         function dashboardproyectos(id_proyect)
         {
             var id_proyecto =  btoa(id_proyect);
             window.location.href = "proyectos_dashboard.aspx?id_proyecto=" + id_proyecto;
         }

       
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div id="div_body_reportedashboard">

        <div class="row">
            <div class="col-lg-12">
                <h3 class="page-header">Dashboard Riesgos</h3>
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
                                        <th style="min-width: 80px; text-align: left;" scope="col">Usuario</th>
                                        <th style="min-width: 200px; text-align: left;" scope="col">Empleado</th>
                                        <th style="min-width: 250px; text-align: center;" scope="col">Proyecto</th>
                                        <th style="min-width: 250px; text-align: center;" scope="col">Riesgo</th>
                                        <th style="min-width: 80px; text-align: center;" scope="col">Tecnologia</th>                                        
                                        <th style="min-width: 80px; text-align: left;" scope="col">Probabilidad</th>
                                        <th style="min-width: 100px; text-align: left;" scope="col">p_probabilidad</th>
                                        <th style="min-width: 100px; text-align: left;" scope="col">impacto_costo</th>
                                        <th style="min-width: 100px; text-align: left;" scope="col">p_impacto_costo</th>
                                        <th style="min-width: 80px; text-align: center;" scope="col">impacto_tiempo</th>
                                        <th style="min-width: 100px; text-align: center;" scope="col">p_impacto_tiempo</th>
                                        <th style="min-width: 100px; text-align: center;" scope="col">estrategia</th>
                                        <th style="min-width: 30px; text-align: center;" scope="col">fecha_evaluacion</th>
                                        <th style="min-width: 80px; text-align: center;" scope="col">riesgo_costo</th>
                                        <th style="min-width: 80px; text-align: center;" scope="col">riesgo_tiempo</th>
                                        <th style="min-width: 50px; text-align: center;" scope="col">Estatus</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="repeater_reporte_riesgos" runat="server">
                                        <ItemTemplate>
                                            <tr style="font-size: 11px">                                                
                                                <td style="text-align: center;"><%# Eval("usuario") %></td>
                                                <td style="text-align: center;"><%# Eval("empleado") %></td>
                                                <td style="text-align: center;"><%# Eval("proyecto") %></td>
                                                <td>
                                                    <a style="cursor: pointer;" onclick='<%# "return dashboardproyectos("+@"""" + Eval("id_riesgo") + @"""" + ");" %>'>
                                                        <%# Eval("riesgo") %>
                                                    </a>
                                                </td>
                                                <td style="text-align: center;"><%# Eval("tecnologia") %></td>                                                
                                                <td style="text-align: center;"><%# Eval("probabilidad") %></td>
                                                <td style="text-align: center;"><%# Eval("p_probabilidad") %></td>
                                                <td style="text-align: center;"><%# Eval("impacto_costo") %></td>
                                                <td style="text-align: center;"><%# Eval("p_impacto_costo") %></td>
                                                <td style="text-align: center;"><%# Eval("impacto_tiempo") %></td>
                                                <td style="text-align: center;"><%# Eval("p_impacto_tiempo") %></td>
                                                <td style="text-align: center;"><%# Eval("estrategia")%></td>
                                                <td style="text-align: center;"><%# Eval("fecha_evaluacion", "{0:d}") %></td>
                                                <td style="text-align: center;"><%# Eval("riesgo_costo") %></td>
                                                <td style="text-align: center;"><%# Eval("riesgo_tiempo") %></td>
                                                <td style="text-align: center;"><%# Eval("estatus") %></td>
                                            </tr>b
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

         <div class="modal fade bs-example-modal-lg" tabindex="-1" id="modal_acciones" role="dialog" aria-labelledby="mySmallModalLabel" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg" role="document">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btneditarriesgo" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="lnkacciones" EventName="Click" />
                    <asp:PostBackTrigger ControlID="btndescargardocumento" />

                </Triggers>
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">×</span></button>
                            <h4 class="modal-title">Acciones</h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                    <h5><strong><i class="fa fa-handshake-o" aria-hidden="true"></i>&nbsp;Accion</strong></h5>
                                    <asp:TextBox ID="txtaccion" MaxLength="250" CssClass=" form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                                    <h5><strong><i class="fa fa-calendar" aria-hidden="true"></i>&nbsp;Fecha de ejecución</strong></h5>
                                     <telerik:RadDatePicker ID="txtfechaejecuacion" runat="server" Width="100%"  Skin="Bootstrap"></telerik:RadDatePicker>
                                </div>
                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                    <h5><strong><i class="fa fa-file-archive-o" aria-hidden="true"></i>&nbsp;Documento</strong></h5>
                                    <telerik:RadAsyncUpload RenderMode="Lightweight" ID="AsyncUpload1" runat="server"
                                        OnFileUploaded="AsyncUpload1_FileUploaded" PostbackTriggers="lnkguardaracciones"
                                        MaxFileSize="2097152" Width="100%"
                                        AutoAddFileInputs="false" Localization-Select="Seleccionar" Skin="Silk" />
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-lg-12 col-sm-12 col-xs-12">
                                    <h6><strong><i class="fa fa-users" aria-hidden="true"></i>&nbsp;Seleccione el empleado responsable</strong>
                                        &nbsp; 
                                    </h6>
                                    <div class="input-group input-group-sm" runat="server" id="div_filtro_empleados">
                                        <asp:TextBox
                                            onfocus="this.select();" ID="txtfilterempleado" CssClass=" form-control"
                                            placeholder="Ingrese un filtro" runat="server"></asp:TextBox>
                                        <span class="input-group-btn">
                                            <asp:LinkButton ID="lnksearch" CssClass="btn btn-primary btn-flat"
                                                OnClientClick="return ChangedTextLoad2();" OnClick="lnksearch_Click" runat="server">
                                                <i class="fa fa-search" aria-hidden="true"></i>
                                            </asp:LinkButton>
                                        </span>
                                    </div>
                                    <asp:Image ID="imgloadempleado" Style="display: none;" ImageUrl="~/img/load.gif" runat="server" />
                                    <label id="lblbemp" runat="server" style="display: none; color: #1565c0">Buscando Empleados</label>
                                    <asp:DropDownList Visible="true" ID="ddlempleado_a_consultar" CssClass="form-control" runat="server">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-lg-12 col-sm-12 col-xs-12">
                                    <br />
                                    <asp:LinkButton OnClientClick="return false;" ID="lnkcargando2" CssClass="btn btn-danger btn-flat pull-right btn-sm" runat="server" Style="display: none;">
                                            <i class="fa fa-refresh fa-spin fa-fw"></i>
                                            <span class="sr-only">Loading...</span>&nbsp;Agregando
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="lnkguardaracciones" OnClientClick="return ConfirmwidgetProyectoModal2('¿Desea Guardar esta acción?');"
                                        OnClick="lnkguardaracciones_Click" CssClass="btn btn-danger btn-flat pull-right btn-sm" runat="server">
                                            Agregar acción&nbsp;<i class="fa fa-plus" aria-hidden="true"></i>
                                    </asp:LinkButton>
                                </div>
                                <div class="col-lg-12 col-xs-12">
                                    <div class="table table-responsive" style="height: 130px; overflow: scroll;">
                                        <telerik:RadGrid ID="grid_acciones" runat="server" Skin="Metro">
                                            <MasterTableView AutoGenerateColumns="false" CssClass="table table-responsive table-bordered"
                                                HeaderStyle-BackColor="White" HeaderStyle-ForeColor="Black" Style="font-size: 9px"
                                                Width="100%">
                                                <Columns>
                                                    <telerik:GridTemplateColumn>
                                                        <HeaderStyle Width="20px" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkeliminarparticipante"
                                                                OnClientClick="return confirm('¿Desea Eliminar esta acción?');"
                                                                OnClick="lnkeliminarparticipante_Click" runat="server" CommandName="View"
                                                                CommandArgument='<%# DataBinder.Eval(Container.DataItem, "id_actividad").ToString() %>'>
                                                        <i class="fa fa-trash fa-2x" aria-hidden="true"></i>
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn>
                                                        <HeaderStyle Width="20px" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkdescargararchivo" OnClientClick='<%# "return DownloadFile("+ DataBinder.Eval(Container.DataItem, "id_actividad").ToString()+");" %>'
                                                                OnClick="lnkdescargararchivo_Click" runat="server" CommandName="Download"
                                                                CommandArgument='<%# DataBinder.Eval(Container.DataItem, "id_actividad").ToString() %>'>
                                                        <i class="fa fa-file-archive-o fa-2x" aria-hidden="true"></i>
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridBoundColumn DataField="nombre" HeaderText="Acción" UniqueName="accion"
                                                        Visible="true">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="empleado_resp" HeaderText="Responsable" UniqueName="empleado_resp"
                                                        Visible="true">
                                                        <HeaderStyle Width="200px" />
                                                    </telerik:GridBoundColumn>

                                                    <telerik:GridTemplateColumn HeaderText="Fecha ejecución">
                                                        <HeaderStyle Width="110px" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                        <ItemTemplate>
                                                            <label><%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "fecha_ejecucion")).ToString("dd MMMM yyyy", System.Globalization.CultureInfo.CreateSpecificCulture("es-MX")) %></label>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                </Columns>
                                            </MasterTableView>
                                        </telerik:RadGrid>

                                    </div>
                                    <button type="button" class="btn btn-default pull-left" data-dismiss="modal">Cerrar acciones</button>
                                </div>
                                <asp:HiddenField ID="hdfid_riesgo" runat="server" />
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
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
                                    <h6><strong><i class="fa fa-calendar" aria-hidden="true"></i>&nbsp;Fecha Inicial</strong></h6>
                                    <telerik:RadDatePicker ID="rdpfechainicial" runat="server" Width="100%" Skin="Bootstrap"></telerik:RadDatePicker>                                     
                                </div>
                                <div class="col-lg-6 col-md-6 col-sm-12"  style="font-size:10px;">
                                    <h6><strong><i class="fa fa-calendar" aria-hidden="true"></i>&nbsp;Fecha Final</strong></h6>
                                    <telerik:RadDatePicker ID="rdpfechafinal" runat="server" Width="100%"  Skin="Bootstrap"></telerik:RadDatePicker>
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
    <asp:HiddenField ID="hdfguid" runat="server" />
    <asp:HiddenField ID="hdfid_actividad" runat="server" />
    <asp:HiddenField ID="hdfmotivos" runat="server" />
    <asp:HiddenField ID="hdfcommandgrid" runat="server" />
    <asp:Button ID="btneditarriesgo" OnClick="btneditarriesgo_Click" runat="server" Text="" Style="display: none" />
    <asp:Button ID="btndescargardocumento" OnClick="lnkdescargararchivo_Click" runat="server" Text="" Style="display: none" />
    <asp:HiddenField ID="hdfsessionid" runat="server" />
    </div>
</asp:Content>
