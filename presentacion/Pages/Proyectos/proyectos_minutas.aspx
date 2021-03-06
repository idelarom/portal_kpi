﻿<%@ Page Title="Minutas" Language="C#" MasterPageFile="~/Pages/MP/Global.Master" AutoEventWireup="true" CodeBehind="proyectos_minutas.aspx.cs" Inherits="presentacion.Pages.Proyectos.proyectos_minutas" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">


        $(document).ready(function () {
            Init('.dvv');
        });
        function Init(value) {
            if ($.fn.dataTable.isDataTable(value)) {
                table = $(value).DataTable();
            }
            else {
                $(value).DataTable({
                    "paging": true,
                    "pageLength": 10,
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

        function ConfirmMinutaModal(msg) {
            if (confirm(msg)) {
                $("#<%= lnkcargandoMinuta.ClientID%>").show();
                $("#<%= lnkguardarminuta.ClientID%>").hide();
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

        function ChangedTextLoad3() {
            $("#<%= imgempleado.ClientID%>").show();
            $("#<%= lblempleado.ClientID%>").show();
            return true;
        }
        function ConfirmEntregableDelete(msg) {
            if (confirm(msg)) {
                return ReturnPrompMsg(msg);
            } else {
                return false;
            }
        }

        function ReturnPrompMsg() {
            var motivo = prompt("Motivo de eliminación", "");
            if (motivo != null) {
                if (motivo != '') {
                    var myHidden = document.getElementById('<%= hdfmotivos.ClientID %>');
                    myHidden.value = motivo;
                    return true;
                } else {
                    alert('ES NECESARIO EL MOTIVO DE LA ELIMINACIÓN.');
                    ReturnPrompMsg();
                }
            } else {
                return false;
            }

        }
        var opts = {
            lines: 13 // The number of lines to draw
                , length: 28 // The length of each line
                , width: 14 // The line thickness
                , radius: 42 // The radius of the inner circle
                , scale: 1.2 // Scales overall size of the spinner
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
        function Loading(modal) {            
            var target = document.getElementById(modal);
            var spinner = new Spinner(opts).spin(target);
        }

        function ViewMinuta(id){
        
            var hdfid_minuta = document.getElementById('<%= hdfid_minuta.ClientID %>');
            hdfid_minuta.value = id;
            
            document.getElementById('<%= btnevent.ClientID%>').click();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <h5 class="page-header">Minutas del proyecto</h5>
        </div>
        <div class="col-lg-12">
            <div class="box box-danger">
                <div class="box-body with-border" style="">
                    <div class="row">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            <h5><strong><i class="fa fa-cubes" aria-hidden="true"></i>&nbsp;Proyecto</strong>
                                &nbsp;<asp:LinkButton ID="lnkdashboard" CssClass="btn btn-danger btn-xs btn-flat"
                                    OnClick="lnkdashboard_Click" runat="server">Dashboard</asp:LinkButton>
                            </h5>
                            <p class="text-muted">
                                <asp:Label ID="lblproyect" runat="server" Text="nombre del proyecto"></asp:Label>
                            </p>
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-12  col-xs-12">
                            <h5><strong><i class="fa fa-user" aria-hidden="true"></i>&nbsp;Tecnología(s)</strong></h5>
                            <p class="text-muted">
                                <asp:Label ID="lbltecnologia" runat="server" Text="Label"></asp:Label>
                            </p>
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-6  col-xs-12">
                            <h5><strong><i class="fa fa-tasks" aria-hidden="true"></i>&nbsp;CPED</strong></h5>
                            <p class="text-muted">
                                <asp:Label ID="lblcped" runat="server" Text="Label"></asp:Label>
                            </p>
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-6  col-xs-12">
                            <h5><strong><i class="fa fa-money" aria-hidden="true"></i>&nbsp;Monto</strong></h5>
                            <p class="text-muted">
                                <asp:Label ID="lblmonto" runat="server" Text="Label"></asp:Label>
                            </p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-lg-12">
            <div class="box box-danger">
                <div class="box-body with-border" style=""  id="table_minutas">
                    <div class="row">
                        <div class="col-lg-12">
                            <asp:LinkButton OnClick="lnknuevaminuta_Click" ID="lnknuevaminuta" 
                                CssClass="btn btn-danger btn-flat btn-sm" runat="server">
                                       Nueva minuta&nbsp;<i class="fa fa-plus" aria-hidden="true"></i>
                            </asp:LinkButton>
                            
                                <div class="table table-responsive" style="font-size:12px;">
                                    <table class="dvv table table-responsive table-bordered table-condensed">
                                        <thead>
                                            <tr>
                                                <th style="min-width: 20px"></th>
                                                <th style="min-width: 20px"></th>
                                                <th style="min-width: 20px"></th>
                                                <th style="min-width: 20px;display:none;"></th>
                                                <th style="min-width: 400px">Asunto</th>
                                                <th style="min-width: 200px">Fecha</th>
                                                <th style="min-width: 200px">Lugar</th>
                                                <th style="width: 40px;">Enviada</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="repeater_minutas" runat="server">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td style="text-align: center">
                                                            <asp:LinkButton ID="lnkcerrarminuta" runat="server" 
                                                                OnClientClick="return confirm('¿Desea enviar esta minuta?');" 
                                                                OnClick="lnkeditminuta_Click" CommandName="Terminar"
                                                                CssClass="btn btn-success btn-flat"  Visible='<%# !Convert.ToBoolean(Eval("enviada")) %>'
                                                                CommandArgument='<%# Eval("id_proyectomin") %>'>
                                                                     <i class="fa fa-share" aria-hidden="true"></i>
                                                            </asp:LinkButton>
                                                        </td>
                                                        <td style="text-align:center;">
                                                            <asp:LinkButton ID="lnkeditminuta" runat="server"  OnClientClick="return Loading('table_minutas');"
                                                                OnClick="lnkeditminuta_Click" CommandName="Editar" CssClass="btn btn-primary btn-flat"
                                                                CommandArgument='<%# Eval("id_proyectomin") %>' Visible='<%# !Convert.ToBoolean(Eval("enviada")) %>'>
                                                                     <i class="fa fa-pencil" aria-hidden="true"></i>
                                                            </asp:LinkButton>
                                                        </td>
                                                        <td style="text-align:center;">
                                                            <asp:LinkButton ID="lnkdeleteminuta" runat="server" 
                                                                OnClick="lnkeditminuta_Click" CommandName="Delete" 
                                                                CssClass="btn btn-danger btn-flat"
                                                                CommandArgument='<%# Eval("id_proyectomin") %>' Visible='<%# !Convert.ToBoolean(Eval("enviada")) %>' 
                                                                OnClientClick="return ConfirmEntregableDelete('¿Desea eliminar la minuta?');">
                                                                     <i class="fa fa-trash" aria-hidden="true"></i>
                                                            </asp:LinkButton>
                                                        </td>
                                                        <td style="text-align:center;display:none;">
                                                            <asp:LinkButton ID="LinkButton1" runat="server" 
                                                                OnClick="lnkeditminuta_Click" CommandName="Descargar" 
                                                                CssClass="btn btn-info btn-flat"
                                                                CommandArgument='<%# Eval("id_proyectomin") %>' 
                                                              >
                                                                    <i class="fa fa-download" aria-hidden="true"></i>
                                                            </asp:LinkButton>
                                                        </td>
                                                        <td><a style="cursor:pointer;" onclick='<%# "return ViewMinuta("+ Eval("id_proyectomin").ToString() +");" %>'>
                                                            <%# Eval("asunto").ToString() %></a>
                                                        </td>
                                                        <td><%# Convert.ToDateTime(Eval("fecha")).ToString("dddd dd MMMM, yyyy", System.Globalization.CultureInfo.CreateSpecificCulture("es-MX")) %></td>
                                                        <td><%# Eval("lugar") %></td>
                                                        <td style="text-align:center;">
                                                            <asp:CheckBox Enabled="false" ID="CheckBox1" runat="server" Checked='<%# Convert.ToBoolean(Eval("Enviada")) %>' /></td>
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
     <div class="modal fade bs-example-modal-lg" tabindex="-1" id="myModalMinutas" role="dialog" aria-labelledby="mySmallModalLabel" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg" role="document">
            <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="lnknuevaminuta" EventName="Click" />
                </Triggers>
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">×</span></button>
                            <h4 class="modal-title">Minutas</h4>
                        </div>
                        <div class="modal-body" id="modal_content">
                            <div class="row">
                                <div class="col-lg-12 col-sm-12 col-xs-12">
                                    <h5><strong><i class="fa fa-bars" aria-hidden="true"></i>&nbsp;Asunto</strong></h5>
                                    <telerik:RadTextBox ID="rtxtasuntominuta" Width="100%" runat="server" Skin="Bootstrap"></telerik:RadTextBox>
                                </div>

                                <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                    <h5><strong><i class="fa fa-calendar" aria-hidden="true"></i>&nbsp;Fecha</strong></h5>
                                    <telerik:RadDatePicker ID="rdpfechaminuta" Width="100%" Skin="Bootstrap" runat="server"></telerik:RadDatePicker>
                                </div>
                                <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                    <h5><strong><i class="fa fa-map-marker" aria-hidden="true"></i>&nbsp;Lugar</strong></h5>
                                    <telerik:RadTextBox ID="rtxtlugarminuta" Width="100%" runat="server" Skin="Bootstrap"></telerik:RadTextBox>
                                </div>
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                    <h5><strong><i class="fa fa-wrench" aria-hidden="true"></i>&nbsp;Propósito</strong></h5>
                                    <telerik:RadTextBox ID="rtxtpropositos" Width="100%" runat="server" TextMode="MultiLine" Rows="2" Skin="Bootstrap"></telerik:RadTextBox>
                                </div>
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                    <h5><strong><i class="fa fa-line-chart" aria-hidden="true"></i>&nbsp;Resultados</strong></h5>
                                    <telerik:RadTextBox ID="rtxtresultados" Width="100%" runat="server" TextMode="MultiLine" Rows="2" Skin="Bootstrap"></telerik:RadTextBox>
                                </div>
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                    <h5><strong><i class="fa fa-hand-paper-o" aria-hidden="true"></i>&nbsp;Acuerdos</strong></h5>
                                    <telerik:RadTextBox ID="rtxtacuerdos" Width="100%" runat="server" TextMode="MultiLine" Rows="2" Skin="Bootstrap"></telerik:RadTextBox>
                                </div>
                                 <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                    <br />
                                    <div style="text-align: right;">
                                        <asp:LinkButton ID="lnkpendientes" OnClientClick="return Loading('modal_content');" runat="server" CssClass="btn btn-success btn-flat" OnClick="lnkpendientes_Click">
                                           <i class="fa fa-briefcase" aria-hidden="true"></i>&nbsp;Pendientes
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="lnkparticipantes" OnClientClick="return Loading('modal_content');"  runat="server" CssClass="btn btn-danger btn-flat" OnClick="lnkparticipantes_Click">
                                           <i class="fa fa-users"></i>&nbsp;Participantes
                                        </asp:LinkButton>
                                    </div>
                                </div>
                                <asp:TextBox ID="txtid_minuta" runat="server" Visible="false"></asp:TextBox>
                            </div>
                        </div>
                        <div class="modal-footer ">
                            <button type="button" class="btn btn-default pull-left" data-dismiss="modal">Cerrar</button>
                            <asp:LinkButton ID="lnkcargandoMinuta" CssClass="btn btn-primary btn-flat" runat="server" OnClientClick="return false;" 
                                Style="display: none;">
                                        <i class="fa fa-refresh fa-spin fa-fw"></i>
                                            <span class="sr-only">Loading...</span>&nbsp;Guardando Minuta...</asp:LinkButton>
                            <asp:LinkButton ID="lnkguardarminuta" runat="server" CssClass="btn btn-primary btn-flat" OnClick="lnkguardarminuta_Click"
                                OnClientClick="return ConfirmMinutaModal('¿Desea Agregar Esta Minuta?');">
                                    <i class="fa fa-floppy-o" aria-hidden="true"></i>&nbsp;Guardar
                            </asp:LinkButton>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

     <div class="modal fade bs-example-modal-lg" tabindex="-1" id="myModalParticipantes" role="dialog" aria-labelledby="mySmallModalLabel" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg" role="document">
            <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="lnkparticipantes" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="lnknuevaminuta" EventName="Click" />
                </Triggers>
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">×</span></button>
                            <h4 class="modal-title">Participantes</h4>
                        </div>
                        <div class="modal-body" style="width: 100%; height: 450px; overflow: scroll;">
                            <div class="row">
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                    <h6><strong><i class="fa fa-users" aria-hidden="true"></i>&nbsp;Empleado responsable</strong>
                                        &nbsp; 
                                    </h6>
                                    <div class="input-group input-group-sm" runat="server" id="div1">
                                        <asp:TextBox
                                            onfocus="this.select();" ID="txtfiltroempleado2" CssClass=" form-control"
                                            placeholder="Ingrese un filtro" runat="server"></asp:TextBox>
                                        <span class="input-group-btn">
                                            <asp:LinkButton ID="lnkserach2" CssClass="btn btn-primary btn-flat"
                                                OnClientClick="return ChangedTextLoad3();" OnClick="lnkserach2_Click" runat="server">
                                                <i class="fa fa-search" aria-hidden="true"></i>
                                            </asp:LinkButton>
                                        </span>
                                    </div>
                                    <asp:Image ID="imgempleado" Style="display: none;" ImageUrl="~/img/load.gif" runat="server" />
                                    <label id="lblempleado" runat="server" style="display: none; color: #1565c0">Buscando Empleados</label>
                                    <asp:DropDownList Visible="true" AutoPostBack="true" OnSelectedIndexChanged="ddlempleado_participante_SelectedIndexChanged" ID="ddlempleado_participante" CssClass="form-control" runat="server">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                    <h5><strong><i class="fa fa-user" aria-hidden="true"></i>&nbsp;Nombre</strong></h5>
                                    <telerik:RadTextBox ReadOnly="true" ID="rtxtnombreparticipante" Width="100%" runat="server" Skin="Bootstrap"></telerik:RadTextBox>
                                </div>

                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                    <h5><strong><i class="fa fa-briefcase" aria-hidden="true"></i>&nbsp;Rol</strong></h5>
                                    <telerik:RadTextBox ReadOnly="true" ID="rtxtrol" Width="100%" runat="server" Skin="Bootstrap"></telerik:RadTextBox>
                                </div>
                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                    <h5><strong><i class="fa fa-address-book" aria-hidden="true"></i>&nbsp;Organización</strong></h5>
                                    <telerik:RadTextBox ReadOnly="true" ID="rtxtorganizacion" Width="100%" runat="server" Skin="Bootstrap"></telerik:RadTextBox>
                                </div>
                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                    <h5><strong><i class="fa fa-address-book" aria-hidden="true"></i>&nbsp;Correo(s)</strong></h5>
                                    <asp:TextBox ReadOnly="true" ID="rtxtcorreo" CssClass=" form-control" TextMode="Email" runat="server" ></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12" id="div_selectedinvo" runat="server" visible="true" style="text-align: right;">
                                    <br />
                                    <asp:LinkButton ID="lnkagregar" OnClick="lnkagregar_Click" CssClass="btn btn-danger btn-flat" runat="server">
                                        Persona externa</asp:LinkButton>


                                    <asp:LinkButton ID="lnkaddparticipante" CssClass="btn btn-primary btn-flat" OnClick="lnkaddparticipante_Click" OnClientClick="return confirm('¿Desea Agregar Este Participante?');" runat="server">
                                            Agregar participante&nbsp;<i class="fa fa-plus" aria-hidden="true"></i>
                                    </asp:LinkButton>
                                </div>
                                <div class="col-lg-12">
                                    <div class="table table-responsive">

                                        <telerik:RadGrid ID="rgrid_participantes" runat="server" Skin="Metro">
                                            <MasterTableView AutoGenerateColumns="false" CssClass="table table-responsive"
                                                HeaderStyle-BackColor="White" HeaderStyle-ForeColor="Black" Style="font-size: 10px;"
                                                Width="100%">
                                                <Columns>

                                                    <telerik:GridTemplateColumn>
                                                        <HeaderStyle Width="40px" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkeliminarparticipante" OnClientClick="return confirm('¿Desea Eliminar este Participante?');" OnClick="lnkeliminarparticipante_Click" runat="server" CommandName="View"
                                                                CommandArgument='<%# DataBinder.Eval(Container.DataItem, "nombre").ToString() %>'>
                                                        <i class="fa fa-trash fa-2x" aria-hidden="true"></i>
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridBoundColumn DataField="nombre" HeaderText="Participante" UniqueName="nombre"
                                                        Visible="true">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="correo" HeaderText="Correos" UniqueName="correo"
                                                        Visible="true">
                                                        <HeaderStyle Width="200px" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="rol" HeaderText="Rol" UniqueName="rol"
                                                        Visible="true">
                                                        <HeaderStyle Width="200px" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="organización" HeaderText="Organización" UniqueName="organizacion"
                                                        Visible="true">
                                                        <HeaderStyle Width="130px" />
                                                    </telerik:GridBoundColumn>
                                                </Columns>
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                    </div>
                                </div>
                            </div>
                            <asp:HiddenField ID="hdf_usuario_participante" runat="server" />
                        </div>
                        <div class="modal-footer ">
                            <button type="button" class="btn btn-default pull-left" data-dismiss="modal">Cerrar</button>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <div class="modal fade bs-example-modal-lg" tabindex="-1" id="myModalPendientes" role="dialog" aria-labelledby="mySmallModalLabel" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg" role="document">
            <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="lnkpendientes" EventName="Click" />
                </Triggers>
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">×</span></button>
                            <h4 class="modal-title">Pendientes</h4>
                        </div>
                        <div class="modal-body" style="width: 100%; height: 450px; overflow: scroll;">
                            <div class="row">
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                    <h6><strong><i class="fa fa-users" aria-hidden="true"></i>&nbsp;Empleado responsable</strong>
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
                            </div>
                            <div class="row">
                                <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                    <h5><strong><i class="fa fa-calendar" aria-hidden="true"></i>&nbsp;Fecha Planeada</strong></h5>
                                    <telerik:RadDatePicker ID="rdtfecha_planeada" Width="100%" Skin="Bootstrap" runat="server"></telerik:RadDatePicker>
                                </div>
                                <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                    <h5><strong><i class="fa fa-wrench" aria-hidden="true"></i>&nbsp;Avance</strong></h5>

                                    <asp:TextBox ID="txtavancependientes" Text="0" CssClass=" form-control" runat="server" type="number" onkeypress="return validarEnteros(event);"
                                        onpaste="return false;"></asp:TextBox>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                    <h5><strong><i class="fa fa-briefcase" aria-hidden="true"></i>&nbsp;Pendiente</strong></h5>
                                    <telerik:RadTextBox ID="rtxtpendiente" Width="100%" runat="server" Skin="Bootstrap" TextMode="MultiLine" Rows="2"></telerik:RadTextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" id="involucrados" runat="server" visible="false">
                                    <h5><strong><i class="fa fa-user" aria-hidden="true"></i>&nbsp;Puede Seleccionar un Involucrado del Proyecto</strong></h5>

                                    <div style="height: 120px; overflow: scroll;">
                                        <telerik:RadListBox RenderMode="Lightweight" Style="font-size: 11px" runat="server" ID="rdlinvopendientes" Width="100%" Skin="Bootstrap" SelectionMode="Multiple">
                                        </telerik:RadListBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12" id="div2" runat="server" visible="true" style="text-align: right;">
                                    <br />

                                    <asp:LinkButton ID="lnkaddpendientes" CssClass="btn btn-primary btn-flat" OnClick="lnkaddpendientes_Click" OnClientClick="return confirm('¿Desea Agregar Este Pendiente?');" runat="server">
                                            Agregar pendiente&nbsp;<i class="fa fa-plus" aria-hidden="true"></i>
                                    </asp:LinkButton>
                                </div>
                                <div class="col-lg-12">
                                    <br />
                                    <div class="table table-responsive">
                                        <telerik:RadGrid ID="grid_pendiente" runat="server" Skin="Metro">
                                            <MasterTableView AutoGenerateColumns="false" CssClass="table table-responsive"
                                                HeaderStyle-BackColor="White" HeaderStyle-ForeColor="Black" Style="font-size: 10px;"
                                                Width="100%">
                                                <Columns>
                                                    <telerik:GridTemplateColumn>
                                                        <HeaderStyle Width="40px" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkeditarparticipante"
                                                                OnClick="lnkeditarparticipante_Click"
                                                                id_proyectominpen='<%# DataBinder.Eval(Container.DataItem, "id_proyectominpen").ToString() %>'
                                                                runat="server" CommandName='<%# DataBinder.Eval(Container.DataItem, "descripcion").ToString() %>'
                                                                CommandArgument='<%# DataBinder.Eval(Container.DataItem, "usuario_resp").ToString() %>'>
                                                                <i class="fa fa-pencil fa-2x" aria-hidden="true"></i>
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn>
                                                        <HeaderStyle Width="40px" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkeliminarparticipante" OnClientClick="return confirm('¿Desea Eliminar este Pendiente?');"
                                                                OnClick="lnkeliminarpendiente_Click" runat="server" CommandName='<%# DataBinder.Eval(Container.DataItem, "descripcion").ToString() %>'
                                                                CommandArgument='<%# DataBinder.Eval(Container.DataItem, "usuario_resp").ToString() %>'>
                                                                <i class="fa fa-trash fa-2x" aria-hidden="true"></i>
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridBoundColumn DataField="nombre" HeaderText="Responsable" UniqueName="nombre"
                                                        Visible="true">
                                                        <HeaderStyle Width="250px" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="descripcion" HeaderText="Pendiente" UniqueName="rol"
                                                        Visible="true">
                                                    </telerik:GridBoundColumn>

                                                    <telerik:GridTemplateColumn HeaderText="Fecha Planeada">

                                                        <HeaderStyle Width="190px" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblfecha_pendiente" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "Fecha")).ToString("dddd dd MMMM, yyyy", System.Globalization.CultureInfo.CreateSpecificCulture("es-MX")) %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                </Columns>
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                    </div>
                                </div>
                            </div>
                            <asp:HiddenField ID="hdf_id_proyectominpen" runat="server" />
                        </div>
                        <div class="modal-footer ">
                            <button type="button" class="btn btn-default pull-left" data-dismiss="modal">Cerrar</button>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    
    <asp:HiddenField ID="hdfmotivos" runat="server" />
    <asp:HiddenField ID="hdfid_minuta" runat="server" />
    <asp:Button ID="btnevent" runat="server" Text="Button" style="display:none;" OnClick="btnevent_Click" />
</asp:Content>
