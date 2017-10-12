<%@ Page Title="" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="mis_proyectos.aspx.cs" Inherits="presentacion.mis_proyectos" %>
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
        function ConfirmEntregableDelete(id_permiso) {
            <%--if (confirm('¿Desea cerrar este proyecto?')) {
                var hdfusuario = document.getElementById('<%= hdfid_proyecto.ClientID %>');
                hdfusuario.value = id_permiso;
                document.getElementById('<%= btneliminar.ClientID%>').click();
                return true;
            } else {
                return false;
            }--%>
               var hdfusuario = document.getElementById('<%= hdfid_proyecto.ClientID %>');
                hdfusuario.value = id_permiso;
                document.getElementById('<%= btneliminar.ClientID%>').click();
            return true;
        }
        function EditarClick(id_permiso) {
            var hdfusuario = document.getElementById('<%= hdfid_proyecto.ClientID %>');
            hdfusuario.value = id_permiso;
            document.getElementById('<%= btneventgrid.ClientID%>').click();
            return false;
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
        function opendashboardriesgos(id_proyecto)
        {
            var idproyecto = document.getElementById('<%= hdfid_proyecto.ClientID %>');
            idproyecto.value = id_proyecto;
            document.getElementById('<%= btnopendashboard.ClientID%>').click();
            return true;
        }
         function ChangedTextLoad2() {
            $("#<%= imgloadempleado.ClientID%>").show();
            $("#<%= lblbemp.ClientID%>").show();
            return true;
         }

        <%-- function GoRiesgos() {
            document.getElementById('<%= lnkgo_riesgos.ClientID%>').click();
            return true;
        }--%>
         function ConfirmwidgetProyectoModal(msg) {
            if (confirm(msg)) {
                $("#<%= LinkButton2.ClientID%>").show();
                $("#<%= lnkguardarhistorial.ClientID%>").hide();
                return true;
            } else {
                return false;
            }
         }
        function validNumericos(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (((charCode == 8) || (charCode == 46)
            || (charCode >= 35 && charCode <= 40)
                || (charCode >= 48 && charCode <= 57)
                || (charCode >= 96 && charCode <= 105))) {
                return true;
            }
            else if (charCode==13) {
                
            }
            else {
                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="row">
        <div class="col-lg-12">
                <h4 class="page-header">Mis proyectos</h4>
          
        </div>
    </div>
    <div class="row">
        <div class="col-lg-12">
            <div class="box box-danger">
                <div class="box-body"><asp:LinkButton ID="lnknuevoproyecto" CssClass="btn btn-primary btn-flat" runat="server" OnClick="lnknuevoproyecto_Click">
                Nuevo proyecto&nbsp;<i class="fa fa-plus" aria-hidden="true"></i>
            </asp:LinkButton>
                     <div class="row">
     <%--   <div class="col-lg-12">
            <h4 class="page-header">
                <i class="fa fa-cubes" aria-hidden="true"></i>&nbsp;<asp:Label ID="lblproyect" runat="server" Text="Mis Proyecto"></asp:Label></h4>
            <asp:LinkButton ID="lnknuevoproyecto" CssClass="btn btn-primary btn-flat" runat="server" OnClick="lnknuevoproyecto_Click">
                Nuevo proyecto&nbsp;<i class="fa fa-plus" aria-hidden="true"></i>
            </asp:LinkButton>
        </div>--%>
        <div class="col-lg-12">
            <div class="box box-danger">
                <div class="box-header with-border">
                    <h3 class="box-title">Filtros</h3>

                    <div class="box-tools pull-right">
                        <button type="button" class="btn btn-box-tool" data-widget="collapse">
                            <i class="fa fa-minus"></i>
                        </button>
                    </div>
                </div>
                <div class="box-body" style="">
                    <div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-12">
                        <h5><strong><i class="fa fa-braille" aria-hidden="true"></i>&nbsp;Filtro estatus</strong></h5>
                            <asp:DropDownList ID="ddlstatus" MaxLength="250" CssClass=" form-control" runat="server" OnSelectedIndexChanged="ddlstatus_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-12">
                            <h5><strong><i class="fa fa-play-circle-o" aria-hidden="true"></i>&nbsp;Tecnologia</strong></h5>
                            <asp:DropDownList ID="ddltechnology" MaxLength="250" CssClass=" form-control" runat="server" OnSelectedIndexChanged="ddltechnology_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                        </div>
                       
                        
                    </div>
                </div>
            </div>
        </div>
    </div>
                   
                    <div class="table-responsive">
                        <table class="dvv table no-margin table-condensed">
                            <thead>
                                <tr style="font-size: 12px;">
                                    <th style="max-width: 20px; text-align: center;" scope="col"></th>
                                    <th style="max-width: 20px; text-align: center;" scope="col"></th>
                                    <th style="min-width: 300px; text-align: left;" scope="col">Proyecto</th>
                                    <th style="min-width: 100px; text-align: left;" scope="col">Usuario</th>
                                    <th style="min-width: 250px; text-align: left;" scope="col">Empleado responsable</th>
                                    <th style="min-width: 100px; text-align: left;" scope="col">Estatus</th>
                                    <th style="min-width: 110px; text-align: left;" scope="col">Periodo evalucion</th>
                                    <th style="min-width: 80px; text-align: left;" scope="col">Tecnologia</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="repeat_proyectos" runat="server">
                                    <ItemTemplate>
                                        <tr style="font-size: 12px">
                                            <td style="text-align: center;">
                                                <a style="cursor: pointer;"
                                                    onclick='<%# "return EditarClick("+Eval("id_proyecto")+");" %>'>
                                                    <i class="fa fa-pencil fa-2x" aria-hidden="true"></i>
                                                </a>
                                            </td>
                                            <td style="text-align: center;">
                                                <a style="cursor: pointer;"
                                                    onclick='<%# "return ConfirmEntregableDelete("+Eval("id_proyecto")+");" %>'>
                                                    <i class="fa fa-trash fa-2x" aria-hidden="true"></i>
                                                </a>
                                            </td>
                                             <td>
                                                <a style="cursor: pointer;" onclick='<%# "return opendashboardriesgos("+@"""" + Eval("id_proyecto") + @"""" + ");" %>'>
                                                    <%# Eval("proyecto") %>
                                                </a>
                                            </td>
                                             <td style="text-align: left;"><%# Eval("usuario") %></td>
                                             <td style="text-align: left;"><%# Eval("empleado") %></td>
                                             <td style="text-align: left;"><%# Eval("estatus") %></td>
                                             <td style="text-align: left;"><%# Eval("periodo") %></td>
                                             <td style="text-align: left;"><%# Eval("tecnologia") %></td>
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
    
    <div class="modal fade bs-example-modal-lg" tabindex="-1" id="modal_terminacion" role="dialog" aria-labelledby="mySmallModalLabel" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg" role="document">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btneliminar" EventName="Click" />
                </Triggers>
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">×</span></button>
                            <h4 class="modal-title">Cierre de proyecto</h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <h5><strong><i class="fa fa-file-archive-o" aria-hidden="true"></i>&nbsp;Documento de cierre</strong></h5>
                                    <telerik:RadAsyncUpload RenderMode="Lightweight" ID="AsyncUpload1" runat="server"
                                        OnFileUploaded="AsyncUpload1_FileUploaded" PostbackTriggers="lnkguardarhistorial"
                                        MaxFileSize="2097152" Width="100%"
                                        AutoAddFileInputs="false" Localization-Select="Seleccionar" Skin="Bootstrap" />
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default pull-left" data-dismiss="modal">Cerrar</button>
                                    <asp:LinkButton OnClientClick="return false;" ID="LinkButton2" CssClass="btn btn-primary btn-flat" runat="server" Style="display: none;">
                                            <i class="fa fa-refresh fa-spin fa-fw"></i>
                                            <span class="sr-only">Loading...</span>&nbsp;Terminando
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="lnkguardarhistorial"
                                         OnClientClick="return ConfirmwidgetProyectoModal('¿Desea terminar este proyecto?');"
                                        OnClick="lnkguardarhistorial_Click" CssClass="btn btn-primary btn-flat pull-right" runat="server">
                                            Terminar proyecto
                                    </asp:LinkButton>
                        </div>
                    </div>
                </ContentTemplate>
                </asp:UpdatePanel>
            </div>
           </div>

       <div class="modal fade bs-example-modal-lg" tabindex="-1" id="ModalCapturaProyectos" role="dialog" aria-labelledby="mySmallModalLabel" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg" role="document">
            <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="lnknuevoproyecto" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btneventgrid" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnopendashboard" EventName="Click" />
                    <asp:PostBackTrigger ControlID="lnkguardar" />
                     <asp:AsyncPostBackTrigger ControlID="txtcveop" EventName="TextChanged"/>
                     <asp:AsyncPostBackTrigger ControlID="txtfolopmt" EventName="TextChanged" />
                    <asp:AsyncPostBackTrigger ControlID="txtcped" EventName="TextChanged" />
                </Triggers>
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">×</span></button>
                            <h4 class="modal-title">Captura de proyecto</h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                    <h5><strong><i class="fa fa-cubes" aria-hidden="true"></i>&nbsp;Nombre del proyecto</strong></h5>
                                    <asp:TextBox ID="txtnombreproyecto" MaxLength="250" CssClass=" form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                    <h5><strong><i class="fa fa-file-text-o" aria-hidden="true"></i>&nbsp;Descripcion</strong></h5>
                                     <asp:TextBox ID="txtdescripcion" MaxLength="250" CssClass=" form-control" runat="server" Rows="2" TextMode="MultiLine"></asp:TextBox>
                                </div>
                                 <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                                    <h5><strong><i class="fa fa-play-circle-o" aria-hidden="true"></i>&nbsp;Tecnologia</strong></h5>
                                    <asp:DropDownList ID="ddltegnologia" MaxLength="250" CssClass=" form-control" runat="server"></asp:DropDownList>
                                </div>
                                 <div class="col-lg-3 col-md-3 col-sm-6 col-xs-6">
                                    <h5><strong><i class="fa fa-calendar" aria-hidden="true"></i>&nbsp;Periodo de evaluación </strong></h5>
                                     <asp:DropDownList ID="ddlperiodo" MaxLength="250" CssClass=" form-control" runat="server"></asp:DropDownList>
                                </div>
                                 <div class="col-lg-3 col-md-3 col-sm-6 col-xs-6">
                                    <h5><strong><i class="fa fa-braille" aria-hidden="true"></i>&nbsp;Estatus</strong></h5>
                                    <asp:DropDownList ID="ddlestatus" MaxLength="250" CssClass=" form-control" runat="server"></asp:DropDownList>
                                </div>
                                </div>
                            <div class="row">
                                 <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                                    <h5><strong><i class="fa fa-sort-numeric-asc" aria-hidden="true"></i>&nbsp;Clave Oportunidad</strong></h5>
                                    <asp:TextBox ID="txtcveop" MaxLength="250" CssClass=" form-control" runat="server" OnTextChanged="txtcveop_TextChanged" TextMode="Number" onkeypress="return validarEnteros(event);" AutoPostBack="true"></asp:TextBox>
                                </div>
                                 <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                                    <h5><strong><i class="fa fa-ticket" aria-hidden="true"></i>&nbsp;Folio pmtracker</strong></h5>
                                    <asp:TextBox ID="txtfolopmt" MaxLength="250" CssClass=" form-control" runat="server" OnTextChanged="txtfolopmt_TextChanged" AutoPostBack="true"></asp:TextBox>
                                </div>
                                 <div class="col-lg-3 col-md-3 col-sm-6 col-xs-6">
                                    <h5><strong><i class="fa fa-tasks" aria-hidden="true"></i>&nbsp;CPED</strong></h5>
                                    <asp:TextBox ID="txtcped" MaxLength="250" CssClass=" form-control" runat="server" OnTextChanged="txtcped_TextChanged" TextMode="Number" AutoPostBack="true"></asp:TextBox>
                                </div>
                                 <div class="col-lg-3 col-md-3 col-sm-6 col-xs-6">
                                    <h5><strong><i class="fa fa-money" aria-hidden="true"></i>&nbsp;Monto USD</strong></h5>
                                    <asp:TextBox ID="txtmonto" MaxLength="250" CssClass=" form-control" runat="server" Enabled="false"></asp:TextBox>
                                </div>
                                 <div class="col-lg-3 col-md-3 col-sm-6 col-xs-6">
                                    <h5><strong><i class="fa fa-money" aria-hidden="true"></i>&nbsp;Monto MN</strong></h5>
                                    <asp:TextBox ID="txtmontomn" MaxLength="250" CssClass=" form-control" runat="server" Enabled="false"></asp:TextBox>
                                     <asp:TextBox ID="txtmoneda" MaxLength="250" CssClass=" form-control" runat="server" Enabled="false" Visible="false"></asp:TextBox>
                                </div>
                                </div>
                            <div class="row">
                                 <div class="col-lg-3 col-md-3 col-sm-6 col-xs-6">
                                     <h6><strong><i class="fa fa-calendar" aria-hidden="true"></i>&nbsp;Fecha Inicial</strong></h6>
                                    <telerik:RadDatePicker ID="rdpfechainicial" runat="server" Width="100%" Skin="Bootstrap"></telerik:RadDatePicker> 
                                </div>
                                 <div class="col-lg-3 col-md-3 col-sm-6 col-xs-6">
                                   <h6><strong><i class="fa fa-calendar" aria-hidden="true"></i>&nbsp;Fecha Final</strong></h6>
                                    <telerik:RadDatePicker ID="rdpfechafinal" runat="server" Width="100%"  Skin="Bootstrap"></telerik:RadDatePicker>
                                </div>
                            </div>
                             <div class="row">
                                    <div class="col-lg-12 col-xs-12">
                                        <h6><strong><i class="fa fa-users" aria-hidden="true"></i>&nbsp;Seleccione el empleado responsable del proyecto</strong>
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
                                        <asp:DropDownList Visible="true" ID="ddlempleado_a_consultar" CssClass="form-control"
                                            AutoPostBack="true"  runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                        </div>

                        <div class="modal-footer ">
                            <button type="button" class="btn btn-default pull-left" data-dismiss="modal">Cerrar</button>
                            <asp:LinkButton OnClientClick="return false;" ID="lnkcargando" CssClass="btn btn-primary btn-flat" runat="server" Style="display: none;">
                                            <i class="fa fa-refresh fa-spin fa-fw"></i>
                                            <span class="sr-only">Loading...</span>&nbsp;Guardando...
                            </asp:LinkButton>
                            <asp:LinkButton ID="lnkguardar" CssClass="btn btn-primary btn-flat" OnClick="lnkguardar_Click"
                                OnClientClick="return ConfirmwidgetProyectoModal('¿Desea Guardar este proyecto ?');" runat="server">
                                            <i class="fa fa-floppy-o" aria-hidden="true"></i>&nbsp;Guardar
                            </asp:LinkButton>
                        </div>
                </ContentTemplate>
                </asp:UpdatePanel>
            </div>
           </div>


    <asp:Button ID="btneventgrid" OnClick="btneventgrid_Click" runat="server" Text="Button" Style="display: none;" />
    <asp:Button ID="btneliminar" OnClick="btneliminar_Click" runat="server" Text="Button" Style="display: none;" />
    <asp:Button ID="btnopendashboard" OnClick="btnopendashboard_Click" runat="server" Text="Button" Style="display: none;" />    
    <asp:HiddenField ID="hdfcommand" runat="server" />
    <asp:HiddenField ID="hdfid_proyecto" runat="server" />
</asp:Content>