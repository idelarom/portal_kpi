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
        $(document).ready(function () {
            Init('.dvv');
        });
        function Init(value) {
            $(value).DataTable({
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
        function ConfirmwidgetProyectoModal(msg) {
            if (confirm(msg)) {
                $("#<%= lnkcargando.ClientID%>").show();
                $("#<%= lnkguardar.ClientID%>").hide();
                return true;
            } else {
                return false;
            }
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
        function ChangedTextLoad2() {
            $("#<%= imgloadempleado.ClientID%>").show();
            $("#<%= lblbemp.ClientID%>").show();
            return true;
        }
        function CargarRiesgos(id_riesgo) {
            var hdf_id_riesgo = document.getElementById('<%= hdf_id_riesgo.ClientID %>');
            hdf_id_riesgo.value = id_riesgo;
            document.getElementById('<%= btneditarriesgo.ClientID%>').click();
        }
        function DownloadFile(id_actividad) {
            var hdf_id_riesgo = document.getElementById('<%= hdfid_actividad.ClientID %>');
            hdf_id_riesgo.value = id_actividad;
            document.getElementById('<%= btndescargardocumento.ClientID%>').click();
            return true;
        }
        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <h5 class="page-header">Evaluaciones de riesgos</h5>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-12">
            <asp:LinkButton OnClientClick="return confirm('¿Desea agregar una nueva evaluación.?');" 
                 OnClick="lnknuevaevaluacion_Click" ID="lnknuevaevaluacion" CssClass="btn btn-primary btn-flat" 
                runat="server">Nueva evaluación&nbsp;<i class="fa fa-plus" aria-hidden="true"></i></asp:LinkButton>
            </div>
        <div class="col-lg-12">
            <div class="nav-tabs-custom">
                <ul class="nav nav-tabs">
                    <asp:Repeater ID="repeater_evaluaciones" runat="server">
                        <ItemTemplate>
                            <li class='<%# Container.ItemIndex == 0? "active":"" %>'><a href='<%# "#tab_"+Eval("id_proyecto_evaluacion") %>' data-toggle="tab">
                                <%# Eval("fecha_evaluacion_str") %></a></li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
                <div class="tab-content">
                    <asp:Repeater ID="repeater_evaluaciones_details" runat="server"
                        OnItemCommand="repeater_evaluaciones_details_ItemCommand"
                        OnItemDataBound="repeater_evaluaciones_details_ItemDataBound">
                        <ItemTemplate>
                            <div class='<%#  "tab-pane" +(Container.ItemIndex == 0? " active":"") %>' id='<%# "tab_"+Eval("id_proyecto_evaluacion") %>'>
                                <asp:UpdatePanel ID="ss" runat="server" UpdateMode="Always">
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="lnkguardar" EventName="Click" />
                                    </Triggers>
                                    <ContentTemplate>

                                        <div class="row">
                                            <div class="col-lg-12">
                                                <h5><strong><i class="fa fa-calendar-o" aria-hidden="true"></i>&nbsp;
                                            Detalles de evaluación del dia <%# Eval("fecha_evaluacion_str") %></strong></h5>
                                            </div>
                                            <div class="col-lg-12" id="div_no_hay_riesgos" runat="server">
                                                <p class=" text-red">Esta evaluación no contiene riesgos.</p>
                                            </div>
                                            <div class="col-lg-12">
                                                <asp:LinkButton ID="lnknuevoriesgo" CssClass="btn btn-primary btn-flat btn-sm" CommandName="nuevo_riesgo" CommandArgument='<%# Eval("id_proyecto_evaluacion") %>'
                                                    runat="server">Nuevo riesgo&nbsp;<i class="fa fa-plus" aria-hidden="true"></i></asp:LinkButton>

                                                <asp:LinkButton ID="lnkimportarriesgos" CssClass="btn btn-success btn-flat btn-sm" CommandName="importar_riesgos" CommandArgument='<%# Eval("id_proyecto_evaluacion") %>'
                                                    runat="server">Importar riesgos&nbsp;<i class="fa fa-plus" aria-hidden="true"></i></asp:LinkButton>

                                            </div>
                                            <div class="col-lg-12">
                                                <div class="table table-responsive">
                                                    <table class="table table-responsive table-hover table-condensed table-bordered">
                                                        <thead>
                                                            <tr style="font-size: 11px;">
                                                                <th style="min-width: 30px; text-align: left;" scope="col">Editar</th>
                                                                <th style="min-width: 180px; text-align: left;" scope="col">Riesgo</th>
                                                                <th style="min-width: 40px; text-align: center;" scope="col">Estatus</th>
                                                                <th style="min-width: 50px; text-align: left;" scope="col">Probabilidad</th>
                                                                <th style="min-width: 10px; text-align: left;" scope="col"></th>
                                                                <th style="min-width: 90px; text-align: left;" scope="col">Impacto costo</th>
                                                                <th style="min-width: 10px; text-align: left;" scope="col"></th>
                                                                <th style="min-width: 90px; text-align: left;" scope="col">Impacto tiempo</th>
                                                                <th style="min-width: 10px; text-align: left;" scope="col"></th>
                                                                <th style="min-width: 80px; text-align: left;" scope="col">Riesgo costo</th>
                                                                <th style="min-width: 85px; text-align: left;" scope="col">Riesgo tiempo</th>
                                                                <th style="min-width: 50px; text-align: left;" scope="col">Estrategia</th>
                                                                <th style="min-width: 50px; text-align: left;" scope="col">Acciones</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <asp:Repeater ID="repeater_riesgos" OnItemCreated="repeater_riesgos_ItemCreated"
                                                                OnItemDataBound="repeater_riesgos_ItemDataBound" runat="server">
                                                                <ItemTemplate>
                                                                    <tr style="font-size: 11px">
                                                                        <td style="text-align:center;">
                                                                            <a style="cursor:pointer;" onclick='<%# "return CargarRiesgos("+Eval("id_riesgo").ToString()+");" %>'>
                                                                                <i class="fa fa-pencil fa-2x" aria-hidden="true"></i>
                                                                            </a>
                                                                        </td>
                                                                        <td style="text-align: left;"><%# Eval("riesgo") %></td>
                                                                        <td style="text-align: center;"><%# Eval("estatus") %></td>
                                                                        <td style="text-align: left;">
                                                                            <asp:DropDownList AutoPostBack="true"
                                                                                OnSelectedIndexChanged="ddlprobabilidad_rep_SelectedIndexChanged"
                                                                                ID="ddlprobabilidad_rep" runat="server">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td style="text-align: left;"><%# Eval("p_probabilidad") %>&nbsp;%</td>
                                                                        <td style="text-align: left;">
                                                                            <asp:DropDownList AutoPostBack="true"
                                                                                OnSelectedIndexChanged="ddlimpacto_costo_rep_SelectedIndexChanged"
                                                                                ID="ddlimpacto_costo_rep" runat="server">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td style="text-align: left;"><%# Eval("p_impacto_costo") %>&nbsp;%</td>
                                                                        <td style="text-align: left;">
                                                                            <asp:DropDownList AutoPostBack="true"
                                                                                OnSelectedIndexChanged="ddlimpacto_tiempo_rep_SelectedIndexChanged" ID="ddlimpacto_tiempo_rep" runat="server">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td style="text-align: left;"><%# Eval("p_impacto_tiempo") %>&nbsp;%</td>
                                                                        <td style="text-align: center;"><%# Eval("riesgo_costo") %>&nbsp;%</td>
                                                                        <td style="text-align: center;"><%# Eval("riesgo_tiempo") %>&nbsp;%</td>
                                                                        <td style="text-align: left;">
                                                                            <asp:DropDownList AutoPostBack="true"
                                                                                OnSelectedIndexChanged="ddlestrategia_rep_SelectedIndexChanged" ID="ddlestrategia_rep" runat="server">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td style="text-align:center;">
                                                                            <a class="btn btn-success btn-flat btn-xs">
                                                                                Acciones
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
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hdf_dias_periodo" runat="server" />
    
    <div class="modal fade bs-example-modal-lg" tabindex="-1" id="modal_riesgo" role="dialog" aria-labelledby="mySmallModalLabel" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg" role="document">
            <asp:UpdatePanel ID="ss" runat="server" UpdateMode="Always">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlprobabilidad" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="txtpprobabilidad" EventName="TextChanged" />
                    <asp:AsyncPostBackTrigger ControlID="ddlimpacto_costo" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="txtimpacto_costo" EventName="TextChanged" />
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
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                    <h5><strong><i class="fa fa-bars" aria-hidden="true"></i>&nbsp;Riesgo</strong></h5>
                                    <asp:TextBox ID="txtriesgo" MaxLength="250" CssClass=" form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                    <h5><strong><i class="fa fa-briefcase" aria-hidden="true"></i>&nbsp;Estatus</strong></h5>
                                    <asp:DropDownList ID="ddlestatus_riesgo" CssClass="form-control" runat="server"></asp:DropDownList>
                                </div>
                                
                                <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                    <h5><strong><i class="fa fa-bars" aria-hidden="true"></i>&nbsp;Probabilidad</strong></h5>
                                    <div class="row">
                                        <div class="col-lg-6 col-md-6 col-sm-8 col-xs-7">
                                            <asp:DropDownList ID="ddlprobabilidad"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlprobabilidad_SelectedIndexChanged"
                                                 CssClass="form-control" runat="server"></asp:DropDownList>
                                        </div>
                                        <div class="col-lg-6 col-md-6 col-sm-4 col-xs-5">
                                            <asp:TextBox ReadOnly="true" 
                                                OnTextChanged="txtpprobabilidad_TextChanged" AutoPostBack="true"
                                                 ID="txtpprobabilidad" MaxLength="250" CssClass=" form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                    <h5><strong><i class="fa fa-money" aria-hidden="true"></i>&nbsp;Impacto costo</strong></h5>
                                    <div class="row">
                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                                            <asp:DropDownList ID="ddlimpacto_costo" 
                                                 AutoPostBack="true" OnSelectedIndexChanged="ddlimpacto_costo_SelectedIndexChanged"
                                                 CssClass="form-control" runat="server"></asp:DropDownList>
                                        </div>
                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                                            <asp:TextBox ID="txtimpacto_costo"  ReadOnly="true" 
                                                 AutoPostBack="true" OnTextChanged="txtimpacto_costo_TextChanged"
                                                 MaxLength="250" CssClass=" form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                    <h5><strong><i class="fa fa-clock-o" aria-hidden="true"></i>&nbsp;Impacto tiempo</strong></h5>
                                    <div class="row">
                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                                            <asp:DropDownList ID="ddlimpacto_tiempo" AutoPostBack="true"
                                                 OnSelectedIndexChanged="ddlimpacto_tiempo_SelectedIndexChanged" CssClass="form-control" runat="server"></asp:DropDownList>
                                        </div>
                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                                            <asp:TextBox ID="txtimpacto_tiempo" ReadOnly="true"
                                                 OnTextChanged="txtimpacto_tiempo_TextChanged"
                                                 MaxLength="250" CssClass=" form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                    <h5><strong><i class="fa fa-money" aria-hidden="true"></i>&nbsp;% Riesgo costo</strong></h5>
                                    <asp:TextBox ID="txtriesgo_costo" ReadOnly="true" MaxLength="250" CssClass=" form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                    <h5><strong><i class="fa fa-clock-o" aria-hidden="true"></i>&nbsp;% Riesgo tiempo</strong></h5>
                                    <asp:TextBox ID="txtriesgo_tiempo" ReadOnly="true" MaxLength="250" CssClass=" form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                    <h5><strong><i class="fa fa-handshake-o" aria-hidden="true"></i>&nbsp;Estrategia</strong></h5>
                                    <asp:DropDownList ID="ddlestrategias" CssClass="form-control" runat="server"></asp:DropDownList>
                                </div>
                                
                                <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                    <h5><strong><i class="fa fa-handshake-o" aria-hidden="true"></i>&nbsp;Acciones</strong></h5>
                                    <asp:LinkButton ID="lnkacciones" OnClick="lnkacciones_Click" CssClass="btn btn-success btn-flat" runat="server">Acciones</asp:LinkButton>
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
                            <asp:LinkButton ID="lnkguardar"   OnClientClick="return ConfirmwidgetProyectoModal('¿Desea Guardar este riesgo?');" 
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
                    <asp:AsyncPostBackTrigger ControlID="lnkacciones" EventName="Click" />
                    <asp:PostBackTrigger ControlID="btndescargardocumento"/>
                    <asp:PostBackTrigger ControlID="lnkguardaracciones"/>
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
                                <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                    <h5><strong><i class="fa fa-calendar" aria-hidden="true"></i>&nbsp;Fecha de ejecución</strong></h5>
                                    <asp:TextBox ID="txtfechaejecuacion" TextMode="Date"  MaxLength="250" CssClass=" form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                    <h5><strong><i class="fa fa-file-archive-o" aria-hidden="true"></i>&nbsp;Documento</strong></h5>
                                    <asp:FileUpload ID="fuparchivo" CssClass=" form-control" runat="server" />
                                </div>
                                <div class="col-lg-12 col-xs-12">
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

                                <div class="col-lg-12 col-xs-12">
                                    <br />
                                    <div class="table table-responsive">
                                        <telerik:RadGrid ID="grid_acciones" runat="server" Skin="Metro">
                                            <MasterTableView AutoGenerateColumns="false" CssClass="table table-responsive table-bordered"
                                                HeaderStyle-BackColor="White" HeaderStyle-ForeColor="Black"
                                                Width="100%">
                                                <Columns>
                                                    <telerik:GridTemplateColumn>
                                                        <HeaderStyle Width="40px" />
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
                                                        <HeaderStyle Width="40px" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkdescargararchivo"  OnClientClick='<%# "return DownloadFile("+ DataBinder.Eval(Container.DataItem, "id_actividad").ToString()+");" %>'
                                                                OnClick="lnkdescargararchivo_Click" runat="server" CommandName="Download"
                                                                CommandArgument='<%# DataBinder.Eval(Container.DataItem, "id_actividad").ToString() %>'>
                                                        <i class="fa fa-file-archive-o fa-2x" aria-hidden="true"></i>
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridBoundColumn DataField="nombre" HeaderText="Acción" UniqueName="accion"
                                                        Visible="true">
                                                        <HeaderStyle Width="200px" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="empleado_resp" HeaderText="Responsable" UniqueName="empleado_resp"
                                                        Visible="true">
                                                        <HeaderStyle Width="200px" />
                                                    </telerik:GridBoundColumn>
                                                    
                                                    <telerik:GridTemplateColumn  HeaderText="Fecha ejecución">
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
                                </div>
                                <asp:HiddenField ID="hdfid_riesgo" runat="server" />
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default pull-left" data-dismiss="modal">Cerrar</button>
                                  <asp:LinkButton OnClientClick="return false;" ID="lnkcargando2" CssClass="btn btn-primary btn-flat" runat="server" Style="display: none;">
                                            <i class="fa fa-refresh fa-spin fa-fw"></i>
                                            <span class="sr-only">Loading...</span>&nbsp;Guardando
                            </asp:LinkButton>
                            <asp:LinkButton ID="lnkguardaracciones"   OnClientClick="return ConfirmwidgetProyectoModal2('¿Desea Guardar esta acción?');" 
                                 OnClick="lnkguardaracciones_Click" CssClass="btn btn-primary btn-flat" runat="server">
                                            <i class="fa fa-floppy-o" aria-hidden="true"></i>&nbsp;Guardar
                            </asp:LinkButton>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <asp:HiddenField ID="hdfguid" runat="server" />
    <asp:HiddenField ID="hdfid_actividad" runat="server" />
    <asp:Button ID="btneditarriesgo" OnClick="btneditarriesgo_Click" runat="server" Text="" style="display:none" />
    <asp:Button ID="btndescargardocumento" OnClick="lnkdescargararchivo_Click" runat="server" Text="" style="display:none" />
</asp:Content>
