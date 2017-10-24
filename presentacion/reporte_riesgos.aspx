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

         function dashboardproyectos(id_proyect){
             var id_proyecto =  btoa(id_proyect);
             window.location.href = "proyectos_dashboard.aspx?id_proyecto=" + id_proyecto;
         }

         function Open_actions(id_riesgo, id_proyect) {
              var hdf_id_riesgo = document.getElementById('<%= hdfid_riesgo.ClientID %>');
             hdf_id_riesgo.value = id_riesgo;
              var hdf_id_proyecto = document.getElementById('<%= hdfid_proyecto.ClientID %>');
             hdf_id_proyecto.value = id_proyect;
             document.getElementById('<%= btnacciones.ClientID%>').click();
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
                                        <%--<th style="min-width: 100px; text-align: left;" scope="col">p_probabilidad</th>--%>
                                       <%-- <th style="min-width: 100px; text-align: left;" scope="col">impacto_costo</th>--%>
                                       <%-- <th style="min-width: 100px; text-align: left;" scope="col">p_impacto_costo</th>--%>
                                       <%-- <th style="min-width: 80px; text-align: center;" scope="col">impacto_tiempo</th>--%>
                                        <%--<th style="min-width: 100px; text-align: center;" scope="col">p_impacto_tiempo</th>--%>
                                        <th style="min-width: 100px; text-align: center;" scope="col">estrategia</th>
                                        <th style="min-width: 30px; text-align: center;" scope="col">fecha_evaluacion</th>
                                        <%--<th style="min-width: 80px; text-align: center;" scope="col">riesgo_costo</th>
                                        <th style="min-width: 80px; text-align: center;" scope="col">riesgo_tiempo</th>--%>
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
                                                    <a style="cursor: pointer;" onclick='<%# "return Open_actions("+@"""" + Eval("id_riesgo")+@""""+@",""" + Eval("id_proyecto") + @"""" + ");" %>'>
                                                        <%# Eval("riesgo").ToString().Length>99?Eval("riesgo").ToString().Substring(0,99):Eval("riesgo").ToString() %>
                                                    </a>
                                                </td>
                                                <td style="text-align: center;"><%# Eval("tecnologia") %></td>                                                
                                                <td style="text-align: center;"><%# Eval("probabilidad") %></td>
                                               <%-- <td style="text-align: center;"><%# Eval("p_probabilidad") %></td>--%>
                                              <%--  <td style="text-align: center;"><%# Eval("impacto_costo") %></td>--%>
                                               <%-- <td style="text-align: center;"><%# Eval("p_impacto_costo") %></td>--%>
                                                <%--<td style="text-align: center;"><%# Eval("impacto_tiempo") %></td>--%>
                                               <%-- <td style="text-align: center;"><%# Eval("p_impacto_tiempo") %></td>--%>
                                                <td style="text-align: center;"><%# Eval("estrategia")%></td>
                                                <td style="text-align: center;"><%# Eval("fecha_evaluacion", "{0:d}") %></td>
                                                <%--<td style="text-align: center;"><%# Eval("riesgo_costo") %></td>
                                                <td style="text-align: center;"><%# Eval("riesgo_tiempo") %></td>--%>
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

         <%--<div class="modal fade bs-example-modal-lg" tabindex="-1" id="modal_historial_acciones" role="dialog" aria-labelledby="mySmallModalLabel" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg" role="document">
            <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Always">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="lnkcargarleccionesaprendidas" EventName="Click" />
                </Triggers>
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">×</span></button>
                            <h4 class="modal-title">Lecciones aprendidad(Acciones)</h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="table table-responsive">
                                        <table class="table table-responsive table-condensed  table-bordered" id="tabla_historial_acciones">
                                            <thead>
                                                <tr style="font-size: 11px;">
                                                    <th style="min-width: 450px; text-align: left;" scope="col">Acción</th>
                                                    <th style="min-width: 100px; text-align: left;" scope="col">Tipo</th>
                                                    <th style="min-width: 30px; text-align: left;" scope="col">Recomendada</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="repeter_hisitorial_acciones" runat="server">
                                                    <ItemTemplate>
                                                        <tr style="font-size: 11px">
                                                            <td style="min-width: 450px; text-align: left;">
                                                               <a style="cursor:pointer;" onclick='<%# "return ViewLeccionesAprendidas("+@""""+Eval("nombre").ToString().Replace(@"""","'").Replace(System.Environment.NewLine,"").Replace("\n", String.Empty).Replace("\t", String.Empty).Replace("\r", String.Empty)+@"""" +","+Eval("id_actividad_tipo")+");"%>'>
                                                               <%# Eval("nombre").ToString().Substring(0,(Eval("nombre").ToString().Length>200?200:Eval("nombre").ToString().Length))+
                                                                                     (Eval("nombre").ToString().Length>200?"...":"")    %>
                                                               </a>
                                                            </td>
                                                            <td style="min-width: 100px; text-align: left;"><%# Eval("tipo") %></td>
                                                            <td style="max-width: 30px; text-align: center">
                                                                <asp:CheckBox ID="CheckBox1" AutoPostBack="false" Enabled="false"
                                                                    Checked='<%# Eval("recomendada") %>' runat="server" />
                                                            </td>

                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                    </div>
                                    <button type="button" class="btn btn-default pull-left" data-dismiss="modal">Cerrar</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>--%>

         <div class="modal fade bs-example-modal-lg" tabindex="-1" id="modal_acciones" role="dialog" aria-labelledby="mySmallModalLabel" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg" role="document">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnacciones" EventName="Click" />
                   <%-- <asp:AsyncPostBackTrigger ControlID="lnkacciones" EventName="Click" />
                    <asp:PostBackTrigger ControlID="btndescargardocumento" />--%>

                </Triggers>
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">×</span></button>
                            <h4 class="modal-title">Acciones</h4>
                        </div>
                        <div class="modal-body">
                          <%--  <div id="div_nueva_Accion" runat="server" visible="true">
                                <div class="row">                                    
                                    <div class="col-lg-12 col-sm-12 col-xs-12">
                                        <br />
                                        <asp:LinkButton ID="lnkcargarleccionesaprendidas" OnClick="lnkcargarleccionesaprendidas_Click"
                                            CssClass="btn btn-success btn-flat btn-sm" runat="server">
                                            Lecciones aprendidas
                                        </asp:LinkButton>
                                    </div>

                                </div>
                            </div>--%>
                            <%--<div class="row" id="div_cierre_actividad" runat="server" visible="false">
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                    <h5><strong><i class="fa fa-handshake-o" aria-hidden="true"></i>&nbsp;Accion</strong></h5>
                                        <asp:TextBox ID="txtaccion_title" ReadOnly="true" TextMode="MultiLine" Rows="2" MaxLength="250" CssClass=" form-control" runat="server"></asp:TextBox>
                                  
                                </div>
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                    <h5><strong><i class="fa fa-file-archive-o" aria-hidden="true"></i>&nbsp;Documento</strong></h5>
                                    <telerik:RadAsyncUpload RenderMode="Lightweight" ID="AsyncUpload1" runat="server"
                                        OnFileUploaded="AsyncUpload1_FileUploaded" PostbackTriggers="lnkguardaresultados"
                                        MaxFileSize="2097152" Width="100%"
                                        AutoAddFileInputs="false" Localization-Select="Seleccionar" Skin="Silk" />
                                </div>
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                    <h5><strong><i class="fa fa-file-archive-o" aria-hidden="true"></i>&nbsp;Resultado</strong></h5>
                                    <asp:TextBox ID="txtresultado" CssClass="form-control" Rows="2" TextMode="MultiLine" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                    <asp:CheckBox ID="cbxrecomendado" Text="Recomendado" runat="server" />
                                </div>
                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                    <asp:CheckBox ID="cbxleccionesapren" Text="Lecciones aprendidas" runat="server" />
                                </div>
                                <div class="col-lg-12 col-sm-12 col-xs-12">
                                    <br />
                                    <asp:LinkButton OnClick="lnkcancelar_Click" ID="lnkcancelar" CssClass="btn btn-danger btn-flat btn-sm"
                                        runat="server">
                                           Cancelar
                                    </asp:LinkButton>
                                    <asp:LinkButton OnClientClick="return false;" ID="lnkguardaresultadosload" CssClass="btn btn-primary btn-flat pull-right btn-sm" runat="server" Style="display: none;">
                                            <i class="fa fa-refresh fa-spin fa-fw"></i>
                                            <span class="sr-only">Loading...</span>&nbsp;Guardando
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="lnkguardaresultados" OnClientClick="return ConfirmwidgetProyectoModal2('¿Desea Guardar el resultado?');"
                                        OnClick="lnkguardaresultados_Click" CssClass="btn btn-primary btn-flat pull-right btn-sm" runat="server">
                                            Guardar resultado&nbsp;<i class="fa fa-floppy-o" aria-hidden="true"></i>
                                    </asp:LinkButton>
                                </div>
                            </div>--%>
                            <div class="row">
                                <div class="col-lg-12 col-xs-12">
                                    <div class="table table-responsive" style="height: 130px; overflow: scroll;">
                                        <div class="table table-responsive">
                                        <table class="table table-responsive table-condensed  table-bordered" id="tabla_acciones">
                                            <thead>
                                                <tr style="font-size: 11px;">
                                                    <%--<th style="min-width: 80px; text-align: left;" scope="col"></th>
                                                    <th style="min-width: 30px; text-align: left;" scope="col"></th>
                                                    <th style="min-width: 30px; text-align: left;" scope="col"></th>--%>
                                                    <th style="min-width: 250px; text-align: left;" scope="col">Acción</th>
                                                    <th style="min-width: 100px; text-align: left;" scope="col">Fecha Estimada</th>
                                                    <th style="min-width: 100px; text-align: left;" scope="col">Fecha Real</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="repeater_acciones" runat="server">
                                                    <ItemTemplate>
                                                        <tr style="font-size: 11px">
                                                              <%-- <td style="max-width: 30px; text-align: center">
                                                             <asp:LinkButton ID="lnkresultado"
                                                                    OnClick="lnkresultado_Click" Style="color: white;" runat="server" CommandName="View" CssClass="btn btn-primary btn-flat btn-xs"
                                                                    CommandArgument='<%# Eval("id_actividad").ToString() %>'>
                                                                    Resultado
                                                                </asp:LinkButton>
                                                            </td>--%>
                                                           <%-- <td style="max-width: 30px; text-align: center">
                                                                <asp:LinkButton Visible='<%#Convert.ToBoolean(Eval("terminada")) %>'
                                                                     ID="lnkdescargararchivo" OnClientClick='<%# "return DownloadFile("+ DataBinder.Eval(Container.DataItem, "id_actividad").ToString()+");" %>'
                                                                    OnClick="lnkdescargararchivo_Click" runat="server" CommandName="Download"
                                                                    CommandArgument='<%# Eval("id_actividad").ToString() %>'>
                                                                    <i class="fa fa-file-archive-o fa-2x" aria-hidden="true"></i>
                                                                </asp:LinkButton>
                                                            </td>--%>
                                                             <%--<td style="max-width: 30px; text-align: center">
                                                               <asp:LinkButton ID="lnkeliminarparticipante"
                                                                    OnClientClick="return confirm('¿Desea Eliminar esta acción?');"
                                                                    OnClick="lnkeliminarparticipante_Click" runat="server" CommandName="View"
                                                                    CommandArgument='<%# Eval("id_actividad").ToString() %>'>
                                                                    <i class="fa fa-trash fa-2x" aria-hidden="true"></i>
                                                                </asp:LinkButton>
                                                            </td>--%>
                                                             <td style="min-width: 250px; text-align: left;">                                                              
                                                               <%# Eval("nombre").ToString().Substring(0,(Eval("nombre").ToString().Length>100?100:Eval("nombre").ToString().Length))+
                                                                                     (Eval("nombre").ToString().Length>100?"...":"")    %>
                                                            </td>
                                                            <td style="min-width: 100px; text-align: left;"><%# Convert.ToDateTime(Eval("fecha_asignacion")).ToString("dd MMMM yyyy", System.Globalization.CultureInfo.CreateSpecificCulture("es-MX")) %></td>
                                                           <td style="min-width: 100px; text-align: left;"><%#( !Convert.ToBoolean(Eval("terminada"))?"--Acción sin ejecutar": Convert.ToDateTime(Eval("fecha_ejecucion")).ToString("dd MMMM yyyy", System.Globalization.CultureInfo.CreateSpecificCulture("es-MX"))) %></td>
                                                           
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                    </div>


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
    <asp:HiddenField ID="hdfsessionid" runat="server" />
    <asp:HiddenField ID="hdfguid" runat="server" />
    <asp:HiddenField ID="hdfid_riesgos" runat="server" />
    <asp:HiddenField ID="hdfid_proyecto" runat="server" />
    <asp:Button ID="btnacciones" OnClick="btnacciones_Click" runat="server" Text="" Style="display: none" />
    </div>
</asp:Content>
