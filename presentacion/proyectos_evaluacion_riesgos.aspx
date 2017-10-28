<%@ Page Title="Evaluaciones" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="proyectos_evaluacion_riesgos.aspx.cs" Inherits="presentacion.proyectos_evaluacion_riesgos" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .nav-tabs-custom > .tab-content {
            background: #fff;
            padding: 10px;
            border-bottom-right-radius: 3px;
            border-bottom-left-radius: 3px;
        }
    </style>
    <script type="text/javascript">

        var opts = {
            lines: 13 // The number of lines to draw
          , length: 28 // The length of each line
          , width: 14 // The line thickness
          , radius: 42 // The radius of the inner circle
          , scale: 1 // Scales overall size of the spinner
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
            Init('.dvv');
        });
        function InitPagging(value) {
            if ($.fn.dataTable.isDataTable(value)) {
                table = $(value).DataTable();
            }
            else {
                $(value).DataTable({
                    "paging": true,
                    "pageLength": 6,
                    "lengthChange": false,
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

        }
        function Init(value) {
            if ($.fn.dataTable.isDataTable(value)) {
                table = $(value).DataTable();
            }
            else {
                $(value).DataTable({
                    "paging": false,
                    "pageLength": 500,
                    "lengthChange": false,
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

        }

        function ConfirmEntregableDelete(msg) {
            if (confirm(msg)) {
                return ReturnPrompMsg(msg);
            } else {
                return false;
            }
        }
        function ReturnPrompMsg() {
            var motivo = prompt("Motivo de Eliminación", "");
            if (motivo != null) {
                if (motivo != '') {
                    var myHidden = document.getElementById('<%= hdfmotivos.ClientID %>');
                    myHidden.value = motivo;
                    return true;
                } else {
                    alert('ES NECESARIO EL MOTIVO DE LA ELIMINACIÓN.');
                    ReturnPrompMsg();
                    return false;
                }
            } else {
                return false;
            }

        }
        function ConfirmwidgetProyectoModal(msg) {
            if (confirm(msg)) {
                $("#<%= lnkcargando.ClientID%>").show();
                $("#<%= lnkguardar.ClientID%>").hide();
                return true;
            } else {
                return false;
            }
        }

        function ChangedValue() {
            $("#<%= load_cumpli_compromisos.ClientID%>").show();
            var target = document.getElementById('<%= load_cumpli_compromisos.ClientID %>');
            var spinner = new Spinner(opts).spin(target);
            return true;
        }

        function ConfirmwidgetProyectoModal2(msg) {
            if (confirm(msg)) {
                $("#<%= lnkcargando2.ClientID%>").show();
                $("#<%= lnkguardaracciones.ClientID%>").hide();
                return true;
            } else {
                return false;
            }
        }

     
        function ConfirmLoadResultados(msg) {
            if (confirm(msg)) {
                $("#<%= lnkguardaresultadosload.ClientID%>").show();
                $("#<%= lnkguardaresultados.ClientID%>").hide();
                return true;
            } else {
                return false;
            }
        }
        function ChangedTextLoad2() {
            $("#<%= imgloadempleado.ClientID%>").show();
            $("#<%= lblbemp.ClientID%>").show();
            return true;
        }
        function CargarRiesgos(id_riesgo, command) {
            var hdf_id_riesgo = document.getElementById('<%= hdf_id_riesgo.ClientID %>');
            hdf_id_riesgo.value = id_riesgo;
            var hdfid_riesgo = document.getElementById('<%= hdfid_riesgo.ClientID %>');
            hdfid_riesgo.value = id_riesgo;
            var hdfcommandgrid = document.getElementById('<%= hdfcommandgrid.ClientID %>');
            hdfcommandgrid.value = command;
            $("#<%= load_cumpli_compromisos.ClientID%>").show();
            var target = document.getElementById('<%= load_cumpli_compromisos.ClientID %>');
            var spinner = new Spinner(opts).spin(target);
            document.getElementById('<%= btneditarriesgo.ClientID%>').click();
            return true;
        }
        function DownloadFile(id_actividad) {
            var hdf_id_riesgo = document.getElementById('<%= hdfid_actividad.ClientID %>');
            hdf_id_riesgo.value = id_actividad;
            document.getElementById('<%= btndescargardocumento.ClientID%>').click();
            return true;
        }

        function ViewLeccionesAprendidas(nombre, id_tipo) {
            
            var txtaccion = document.getElementById('<%= txtaccion.ClientID %>');
            txtaccion.value = nombre;
            $("#<%=ddltipo_actividad.ClientID%>").val(id_tipo);
            ModalCloseGlobal("#modal_historial_acciones");
            return true;
        }
        function Loading(modal) {
            var target = document.getElementById(modal);
            var spinner = new Spinner(opts).spin(target);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <h5 class="page-header">Evaluaciones de riesgos</h5>
        </div>
        <div class="col-lg-12">
            <div class="box box-danger">
                <div class="box-body with-border" style="">
                    <div class="row">
                        <div class="col-lg-8 col-md-8 col-sm-12 col-xs-12">
                            <h5><strong><i class="fa fa-cubes" aria-hidden="true"></i>&nbsp;Proyecto</strong>
                                &nbsp;<asp:LinkButton ID="lnkdashboard" CssClass="btn btn-danger btn-xs btn-flat"
                                    OnClick="lnkdashboard_Click" runat="server">Dashboard</asp:LinkButton>
                            </h5>
                            <p class="text-muted">
                                <asp:Label ID="lblproyect" runat="server" Text="Label"></asp:Label>
                            </p>
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-6  col-xs-12">
                            <h5><strong><i class="fa fa-calendar-o" aria-hidden="true"></i>&nbsp;Periodo de evaluación</strong></h5>
                            <p class="text-muted">
                                <asp:Label ID="lblperiodo" runat="server" Text="Label"></asp:Label>
                            </p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-12">
            <asp:LinkButton OnClientClick="return confirm('¿Desea agregar una nueva evaluación?');"
                OnClick="lnknuevaevaluacion_Click" ID="lnknuevaevaluacion" CssClass="btn btn-warning btn-flat"
                runat="server">Nueva evaluación&nbsp;<i class="fa fa-plus" aria-hidden="true"></i></asp:LinkButton>
        </div>
        <div class="col-lg-12">
            <div class="nav-tabs-custom">
                <ul class="nav nav-tabs">

                    <asp:Repeater ID="repeater_evaluaciones" runat="server" OnItemCommand="repeater_evaluaciones_ItemCommand">
                        <ItemTemplate>
                            <li id_eval='<%#Eval("id_proyecto_evaluacion") %>' class='' id="link_view" runat="server">
                                <asp:LinkButton ID="lnkview"
                                    CommandArgument='<%#Eval("id_proyecto_evaluacion") %>' runat="server">
                                    <%# Eval("fecha_evaluacion_str") %>
                                </asp:LinkButton>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
                <div class="tab-content">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Always">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="lnkguardar" EventName="Click" />
                        </Triggers>
                        <ContentTemplate>
                            <div id="load_cumpli_compromisos" class="loading" runat="server" style="display: none;">
                            </div>
                            <asp:Repeater ID="repeater_evaluaciones_details" runat="server"
                                OnItemCommand="repeater_evaluaciones_details_ItemCommand"
                                OnItemDataBound="repeater_evaluaciones_details_ItemDataBound">
                                <ItemTemplate>
                                    <asp:Panel CssClass='<%#Eval("id_proyecto_evaluacion") %>' ID="div_principal" runat="server">
                                        <div class='<%#  "tab-pane active" %>' id='<%# "tab_"+Eval("id_proyecto_evaluacion") %>'>

                                            <div class="row">
                                                <div class="col-lg-12">
                                                    <h5><strong><i class="fa fa-calendar-o" aria-hidden="true"></i>&nbsp;
                                                        Detalles de evaluación del dia <%# Eval("fecha_evaluacion_str") %></strong></h5>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-lg-12" id="div_no_hay_riesgos" runat="server">
                                                    <p class=" text-red">Esta evaluación no contiene riesgos.</p>
                                                </div>
                                                <div class="col-lg-12">
                                                    <asp:LinkButton ID="lnknuevoriesgo" CssClass="btn btn-primary btn-flat btn-sm" CommandName="nuevo_riesgo" CommandArgument='<%# Eval("id_proyecto_evaluacion") %>'
                                                        runat="server">Nuevo riesgo&nbsp;<i class="fa fa-plus" aria-hidden="true"></i></asp:LinkButton>

                                                    <asp:LinkButton ID="lnkimportarriesgos" CssClass="btn btn-success btn-flat btn-sm" CommandName="importar_riesgos" CommandArgument='<%# Eval("id_proyecto_evaluacion") %>'
                                                        runat="server">Importar riesgos&nbsp;<i class="fa fa-plus" aria-hidden="true"></i></asp:LinkButton>

                                                    <asp:LinkButton ID="lnkeliminarevaluacion" CssClass="btn btn-danger btn-flat btn-sm" OnClientClick="return ConfirmEntregableDelete('¿Desea eliminar esta evaluación? Esta acción eliminara tambien los riesgos, acciones y documentos relacionados.');"
                                                        OnClick="lnkeliminarevaluacion_Click" CommandArgument='<%# Eval("id_proyecto_evaluacion") %>'
                                                        runat="server">Eliminar evaluación&nbsp;<i class="fa fa-trash" aria-hidden="true"></i></asp:LinkButton>


                                                </div>
                                                <div class="col-lg-12">
                                                    <div class="table table-responsive">
                                                        <table class="dvv table table-responsive table-hover table-condensed table-bordered">
                                                            <thead>
                                                                <tr style="font-size: 11px;">
                                                                    <th style="min-width: 20px; text-align: left;" scope="col"></th>
                                                                    <th style="min-width: 350px; text-align: left;" scope="col">Riesgo</th>
                                                                    <th style="min-width: 100px; text-align: left;" scope="col">Fecha</th>
                                                                    <th style="min-width: 20px; text-align: center;" scope="col">Estatus</th>
                                                                    <th style="min-width: 50px; text-align: left;" scope="col">Probabilidad</th>
                                                                    <th style="min-width: 50px; text-align: left;" scope="col">Impacto</th>
                                                                    <th style="min-width: 130px; text-align: center;" scope="col">Estrategia de respuesta</th>
                                                                    <th style="min-width: 30px; text-align: left;" scope="col"></th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <asp:Repeater ID="repeater_riesgos" OnItemCreated="repeater_riesgos_ItemCreated"
                                                                    OnItemDataBound="repeater_riesgos_ItemDataBound" runat="server">
                                                                    <ItemTemplate>
                                                                        <tr style="font-size: 11px">
                                                                            <td style="text-align: center;">
                                                                                <a style="cursor: pointer;" class="btn btn-primary btn-xs btn-flat" onclick='<%# "return CargarRiesgos("+Eval("id_riesgo").ToString()+",1);" %>'>Editar
                                                                                </a>
                                                                            </td>
                                                                            <td style="text-align: left;">
                                                                                <%# Eval("riesgo").ToString().Substring(0,(Eval("riesgo").ToString().Length>120?120:Eval("riesgo").ToString().Length))+
                                                                                     (Eval("riesgo").ToString().Length>120?"...":"")    %>

                                                                            </td>
                                                                            
                                                                            <td style="text-align: fecha;">
                                                                                <%# Convert.ToDateTime(Eval("fecha_registro")).ToString("dddd dd MMMM, yyyy", System.Globalization.CultureInfo.CreateSpecificCulture("es-MX")) %>
                                                                            </td>
                                                                            <td style="text-align: center;"><%# Eval("estatus") %></td>
                                                                            <td style="text-align: left;">
                                                                                <asp:DropDownList AutoPostBack="true" onchange="ChangedValue();"
                                                                                    OnSelectedIndexChanged="ddlprobabilidad_rep_SelectedIndexChanged"
                                                                                    ID="ddlprobabilidad_rep" runat="server">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td style="text-align: left;">
                                                                                <asp:DropDownList AutoPostBack="true" onchange="ChangedValue();"
                                                                                    OnSelectedIndexChanged="ddlimpacto_costo_rep_SelectedIndexChanged"
                                                                                    ID="ddlimpacto_costo_rep" runat="server">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td style="text-align: center;">
                                                                                <%# Eval("estrategia") %>
                                                                            </td>
                                                                            <td style="text-align: center;">
                                                                                <a class="btn btn-success btn-flat btn-xs" onclick='<%# "return CargarRiesgos("+Eval("id_riesgo").ToString()+",2);" %>'>Acciones
                                                                                </a>
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
                                    </asp:Panel>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hdf_dias_periodo" runat="server" />

    <div class="modal fade bs-example-modal-lg" tabindex="-1" id="modal_riesgo" role="dialog" aria-labelledby="mySmallModalLabel" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg" role="document">
            <asp:UpdatePanel ID="ss" runat="server" UpdateMode="Always">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btneditarriesgo" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="ddlprobabilidad" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="ddlimpacto_costo" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="btneditarriesgo" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="repeater_evaluaciones_details" EventName="ItemCommand" />
                </Triggers>
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">×</span></button>
                            <h4 class="modal-title">Riesgo</h4>
                        </div>
                        <div class="modal-body" id="body_modal_riesgo">
                            <div class="row">
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                    <h5><strong><i class="fa fa-bars" aria-hidden="true"></i>&nbsp;Riesgo</strong></h5>
                                    <asp:TextBox ID="txtriesgo"  style="font-size:12px" TextMode="MultiLine" Rows="3" MaxLength="250" CssClass=" form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                    <h5><strong><i class="fa fa-briefcase" aria-hidden="true"></i>&nbsp;Estatus</strong></h5>
                                    <asp:DropDownList ID="ddlestatus_riesgo" CssClass="form-control" runat="server"></asp:DropDownList>
                                </div>
                                <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12">
                                    <h5><strong><i class="fa fa-bars" aria-hidden="true"></i>&nbsp;Probabilidad</strong></h5>
                                    <asp:DropDownList ID="ddlprobabilidad" AutoPostBack="true" OnSelectedIndexChanged="ddlprobabilidad_SelectedIndexChanged"
                                        CssClass="form-control" runat="server">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12">
                                    <h5><strong><i class="fa fa-gavel" aria-hidden="true"></i>&nbsp;Impacto</strong></h5>
                                    <asp:DropDownList ID="ddlimpacto_costo" AutoPostBack="true" OnSelectedIndexChanged="ddlprobabilidad_SelectedIndexChanged"
                                        CssClass="form-control" runat="server">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                    <h5><strong><i class="fa fa-handshake-o" aria-hidden="true"></i>&nbsp;Estrategia de respuesta</strong></h5>
                                    <asp:HiddenField ID="hdfvalor_riesgo" runat="server" />
                                    <asp:HiddenField ID="hdfid_estrategia" runat="server" />
                                    <asp:TextBox ID="txtestrategia" ReadOnly="true" CssClass=" form-control" runat="server"></asp:TextBox>
                                    <%-- <asp:DropDownList ID="ddlestrategias" CssClass="form-control" Enabled="false" runat="server"></asp:DropDownList>--%>
                                </div>
                                
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                    <h5><strong>Detalles de la estrategia</strong></h5>
                                    <asp:TextBox ID="txtestrategia_det" ReadOnly="true" TextMode="MultiLine" Rows="2" style="font-size:12px" CssClass=" form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                    <br />
                                    <asp:LinkButton ID="lnkacciones" OnClick="lnkacciones_Click" CssClass="btn btn-success btn-flat btn-sm" 
                                        OnClientClick="return Loading('body_modal_riesgo');" runat="server">
                                        <i class="fa fa-handshake-o" aria-hidden="true"></i>&nbsp;Acciones</asp:LinkButton>
                                </div>
                            </div>
                            <asp:HiddenField ID="hdf_id_proyecto_evaluacion" runat="server" />
                            <asp:HiddenField ID="hdf_id_riesgo" runat="server" />
                        </div>
                        <div class="modal-footer ">
                            <button type="button" class="btn btn-default pull-left" data-dismiss="modal">Cerrar</button>
                            <asp:LinkButton OnClientClick="return false;" ID="lnkcargando" CssClass="btn btn-primary btn-flat" runat="server" Style="display: none;">
                                            <i class="fa fa-refresh fa-spin fa-fw"></i>
                                            <span class="sr-only">Loading...</span>&nbsp;Guardando
                            </asp:LinkButton>
                            <asp:LinkButton ID="lnkguardar" OnClientClick="return ConfirmwidgetProyectoModal('¿Desea guardar este riesgo?');"
                                OnClick="lnkguardar_Click" CssClass="btn btn-primary btn-flat" runat="server">
                                            <i class="fa fa-floppy-o" aria-hidden="true"></i>&nbsp;Guardar
                            </asp:LinkButton>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div class="modal fade bs-example-modal-lg" tabindex="-1" id="modal_acciones" role="dialog" aria-labelledby="mySmallModalLabel" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg" role="document">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
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
                        <div class="modal-body" id="body_modal_acciones">
                            <div id="div_nueva_Accion" runat="server" visible="true">
                                <div class="row">

                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                        <h5><strong><i class="fa fa-handshake-o" aria-hidden="true"></i>&nbsp;Accion</strong></h5>
                                        <asp:TextBox ID="txtaccion" TextMode="MultiLine" Rows="2" MaxLength="250" CssClass=" form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                        <h5><strong><i class="fa fa-handshake-o" aria-hidden="true"></i>&nbsp;Tipo de actividad</strong></h5>
                                        <asp:DropDownList ID="ddltipo_actividad" CssClass="form-control" runat="server"></asp:DropDownList>
                                    </div>
                                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                        <h5><strong><i class="fa fa-calendar" aria-hidden="true"></i>&nbsp;Fecha estimada</strong></h5>
                                        <telerik:RadDatePicker ID="txtfechaejecuacion" runat="server" Width="100%" Skin="Bootstrap"></telerik:RadDatePicker>
                                    </div>

                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs12">
                                        <h5><strong><i class="fa fa-users" aria-hidden="true"></i>&nbsp;Empleado responsable</strong>
                                            &nbsp; 
                                        </h5>
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
                                    <div class="col-lg-12 col-sm-12 col-xs-12" style="text-align:right;">
                                        <br />
                                        <asp:LinkButton ID="lnkcargarleccionesaprendidas" OnClick="lnkcargarleccionesaprendidas_Click"
                                            CssClass="btn btn-success btn-flat btn-sm" runat="server">
                                            <i class="fa fa-graduation-cap" aria-hidden="true"></i>&nbsp;Lecciones aprendidas
                                        </asp:LinkButton>
                                        <asp:LinkButton OnClientClick="return false;" ID="lnkcargando2" CssClass="btn btn-primary btn-flat btn-sm" runat="server" Style="display: none;">
                                            <i class="fa fa-refresh fa-spin fa-fw"></i>
                                            <span class="sr-only">Loading...</span>&nbsp;Agregando
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="lnkguardaracciones" OnClientClick="return ConfirmwidgetProyectoModal2('¿Desea guardar esta acción?');"
                                            OnClick="lnkguardaracciones_Click" CssClass="btn btn-primary btn-flat btn-sm" runat="server">
                                            Agregar&nbsp;<i class="fa fa-plus" aria-hidden="true"></i>
                                        </asp:LinkButton>
                                    </div>

                                </div>
                            </div>
                            <div class="row" id="div_cierre_actividad" runat="server" visible="false">
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                    <h5><strong><i class="fa fa-handshake-o" aria-hidden="true"></i>&nbsp;Accion</strong></h5>
                                    <asp:TextBox ID="txtaccion_title" ReadOnly="true" TextMode="MultiLine" Rows="2" MaxLength="250" CssClass=" form-control" runat="server"></asp:TextBox>

                                </div>
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                    <h5><strong><i class="fa fa-file-archive-o" aria-hidden="true"></i>&nbsp;Resultado</strong></h5>
                                    <asp:TextBox ID="txtresultado" CssClass="form-control"
                                       style="font-size:12px;"  Rows="2" TextMode="MultiLine" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                    <h5><strong><i class="fa fa-file-archive-o" aria-hidden="true"></i>&nbsp;Documento</strong></h5>
                                    <telerik:RadAsyncUpload RenderMode="Lightweight" ID="AsyncUpload1" runat="server"
                                        OnFileUploaded="AsyncUpload1_FileUploaded" PostbackTriggers="lnkguardaresultados"
                                        MaxFileSize="2097152" Width="100%"
                                        AutoAddFileInputs="false" Localization-Select="Seleccionar" Skin="Silk" />
                                </div>
                                <div class="col-lg-6 col-sm-6 col-xs-12">
                                    <asp:CheckBox ID="cbxrecomendado" Text="Recomendado" runat="server" />
                                </div>
                                <div class="col-lg-6 col-sm-6 col-xs-12">
                                     <asp:CheckBox ID="cbxleccionesapren" Text="Lecciones aprendidas" runat="server" />
                                </div>
                                <div class="col-lg-12 col-sm-12 col-xs-12" style="text-align:right;">
                                    <br />
                                    
                                    <asp:LinkButton OnClick="lnkcancelar_Click" ID="lnkcancelar" CssClass="btn btn-danger btn-flat btn-sm" OnClientClick="return Loading('body_modal_acciones');"
                                        runat="server">
                                           Cancelar
                                    </asp:LinkButton>
                                    <asp:LinkButton OnClientClick="return false;" ID="lnkguardaresultadosload" CssClass="btn btn-primary btn-flat btn-sm" runat="server" Style="display: none;">
                                            <i class="fa fa-refresh fa-spin fa-fw"></i>
                                            <span class="sr-only">Loading...</span>&nbsp;Guardando
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="lnkguardaresultados" OnClientClick="return ConfirmLoadResultados('¿Desea guardar el resultado?');"
                                        OnClick="lnkguardaresultados_Click" CssClass="btn btn-primary btn-flat btn-sm" runat="server">
                                            Guardar resultado&nbsp;<i class="fa fa-floppy-o" aria-hidden="true"></i>
                                    </asp:LinkButton>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12 col-xs-12">
                                    <div class="table table-responsive" style="height: 130px; overflow: scroll;">
                                        <div class="table table-responsive">
                                            <table class="table table-responsive table-condensed  table-bordered" id="tabla_acciones">
                                                <thead>
                                                    <tr style="font-size: 11px;">
                                                        <th style="min-width: 80px; text-align: left;" scope="col"></th>
                                                        <th style="min-width: 30px; text-align: left;" scope="col"></th>
                                                        <th style="min-width: 30px; text-align: left;" scope="col"></th>
                                                        <th style="min-width: 150px; text-align: left;" scope="col">Acción</th>
                                                        <th style="min-width: 220px; text-align: left;" scope="col">Empleado</th>
                                                        <th style="min-width: 100px; text-align: left;" scope="col">Fecha Estimada</th>
                                                        <th style="min-width: 100px; text-align: left;" scope="col">Fecha Real</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="repeater_acciones" runat="server">
                                                        <ItemTemplate>
                                                            <tr style="font-size: 11px">
                                                                <td style="max-width: 30px; text-align: center">
                                                                    <asp:LinkButton ID="lnkresultado" OnClientClick="return Loading('body_modal_acciones');"
                                                                        OnClick="lnkresultado_Click" Style="color: white;" runat="server" CommandName="View" CssClass="btn btn-primary btn-flat btn-xs"
                                                                        CommandArgument='<%# Eval("id_actividad").ToString() %>'>
                                                                    Resultado
                                                                    </asp:LinkButton>
                                                                </td>
                                                                <td style="max-width: 30px; text-align: center">
                                                                    <asp:LinkButton Visible='<%#Convert.ToBoolean(Eval("terminada")) %>'
                                                                        ID="lnkdescargararchivo" OnClientClick='<%# "return DownloadFile("+ DataBinder.Eval(Container.DataItem, "id_actividad").ToString()+");" %>'
                                                                        OnClick="lnkdescargararchivo_Click" runat="server" CommandName="Download"
                                                                        CommandArgument='<%# Eval("id_actividad").ToString() %>'>
                                                                    <i class="fa fa-file-archive-o fa-2x" aria-hidden="true"></i>
                                                                    </asp:LinkButton>
                                                                </td>
                                                                <td style="max-width: 30px; text-align: center">
                                                                    <asp:LinkButton ID="lnkeliminarparticipante"
                                                                        OnClientClick="return confirm('¿Desea Eliminar esta acción?');"
                                                                        OnClick="lnkeliminarparticipante_Click" runat="server" CommandName="View"
                                                                        CommandArgument='<%# Eval("id_actividad").ToString() %>'>
                                                                    <i class="fa fa-trash fa-2x" aria-hidden="true"></i>
                                                                    </asp:LinkButton>
                                                                </td>
                                                                <td style="min-width: 150px; text-align: left;">
                                                                    <%# Eval("nombre").ToString().Substring(0,(Eval("nombre").ToString().Length>50?50:Eval("nombre").ToString().Length))+
                                                                                     (Eval("nombre").ToString().Length>50?"...":"")    %>
                                                                </td>
                                                                <td style="min-width: 220px; text-align: left;">
                                                                    <%# Eval("empleado_resp").ToString()  %>
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
                                    <button type="button" class="btn btn-default pull-left" data-dismiss="modal">Cerrar</button>
                                </div>
                                <asp:HiddenField ID="hdfid_riesgo" runat="server" />
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <div class="modal fade bs-example-modal-lg" tabindex="-1" id="modal_historial" role="dialog" aria-labelledby="mySmallModalLabel" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg" role="document">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="repeater_evaluaciones_details" EventName="ItemCommand" />
                </Triggers>
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">×</span></button>
                            <h4 class="modal-title">Seleccione los riesgos a importar</h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="table table-responsive">
                                        <table class="table table-responsive table-condensed  table-bordered" id="tabla_historial">
                                            <thead>
                                                <tr style="font-size: 11px;">
                                                    <th style="min-width: 30px; text-align: left;" scope="col">Seleccionar</th>
                                                    <th style="min-width: 500px; text-align: left;" scope="col">Riesgo</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="repetaer_historial_riesgos" runat="server">
                                                    <ItemTemplate>
                                                        <tr style="font-size: 11px">
                                                            <td style="max-width: 30px; text-align: center">
                                                                <asp:CheckBox ID="cbxseleccionado" AutoPostBack="false"
                                                                    name='<%# Eval("riesgo") %>' 
                                                                    runat="server" />
                                                            </td>
                                                            <td style="min-width: 500px; text-align: left;"><%# Eval("riesgo") %></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                    </div>
                                    <button type="button" class="btn btn-default pull-left" data-dismiss="modal">Cerrar</button>
                                    <asp:LinkButton OnClientClick="return false;" ID="LinkButton2" CssClass="btn btn-primary btn-flat" runat="server" Style="display: none;">
                                            <i class="fa fa-refresh fa-spin fa-fw"></i>
                                            <span class="sr-only">Loading...</span>&nbsp;Guardando
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="lnkguardarhistorial" OnClientClick="return ConfirmwidgetProyectoModal('¿Desea importar los riesgos seleccionados?');"
                                        OnClick="lnkguardarhistorial_Click" CssClass="btn btn-primary btn-flat pull-right" runat="server">
                                            <i class="fa fa-floppy-o" aria-hidden="true"></i>&nbsp;Guardar
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>


    <div class="modal fade bs-example-modal-lg" tabindex="-1" id="modal_historial_acciones" role="dialog" aria-labelledby="mySmallModalLabel" data-backdrop="static" data-keyboard="false">
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
                                                    <th style="min-width: 250px; text-align: left;" scope="col">Acción</th>
                                                    <th style="min-width: 150px; text-align: left;" scope="col">Resultado</th>
                                                    <th style="min-width: 60px; text-align: left;" scope="col">Tipo</th>
                                                    <th style="min-width: 30px; text-align: left;" scope="col">Recomendada</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="repeter_hisitorial_acciones" runat="server">
                                                    <ItemTemplate>
                                                        <tr style="font-size: 11px">
                                                            <td style="min-width: 250px; text-align: left;">
                                                                <a style="cursor: pointer;" onclick='<%# "return ViewLeccionesAprendidas("+@""""+Eval("nombre").ToString().Replace(@"""","'").Replace(System.Environment.NewLine,"").Replace("\n", String.Empty).Replace("\t", String.Empty).Replace("\r", String.Empty)+@"""" +","+Eval("id_actividad_tipo")+");"%>'>
                                                                    <%# Eval("nombre").ToString().Substring(0,(Eval("nombre").ToString().Length>100?100:Eval("nombre").ToString().Length))+
                                                                                     (Eval("nombre").ToString().Length>100?"...":"")    %>
                                                                </a>
                                                            </td> 
                                                            <td style="min-width: 250px; text-align: left;">
                                                                <a style="cursor: pointer;" onclick='<%# "return ViewLeccionesAprendidas("+@""""+Eval("nombre").ToString().Replace(@"""","'").Replace(System.Environment.NewLine,"").Replace("\n", String.Empty).Replace("\t", String.Empty).Replace("\r", String.Empty)+@"""" +","+Eval("id_actividad_tipo")+");"%>' >
                                                                    <%# Eval("resultado").ToString().Substring(0,(Eval("resultado").ToString().Length>100?100:Eval("resultado").ToString().Length))+
                                                                                     (Eval("resultado").ToString().Length>100?"...":"")    %>
                                                                </a>
                                                            </td>
                                                            <td style="min-width: 80px; text-align: left;"><%# Eval("tipo") %></td>
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
    </div>

    <asp:HiddenField ID="hdfguid" runat="server" />
    <asp:HiddenField ID="hdfid_actividad" runat="server" />
    <asp:HiddenField ID="hdfmotivos" runat="server" />
    <asp:HiddenField ID="hdfcommandgrid" runat="server" />
    <asp:Button ID="btneditarriesgo" OnClick="btneditarriesgo_Click" runat="server" Text="" Style="display: none" />
    <asp:Button ID="btndescargardocumento" OnClick="lnkdescargararchivo_Click" runat="server" Text="" Style="display: none" />
</asp:Content>
