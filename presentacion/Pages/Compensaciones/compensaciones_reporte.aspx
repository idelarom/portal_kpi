﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/MP/Global.Master" AutoEventWireup="true" CodeBehind="compensaciones_reporte.aspx.cs" Inherits="presentacion.Pages.Compensaciones.compensaciones_reporte" %>
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

        function ConfirmwidgetBonosModal() {
            $("#<%= div_modalbodyfiltros.ClientID%>").hide();
            $("#<%= lnkcargando.ClientID%>").show();
            $("#<%= lnkguardar.ClientID%>").hide();
            return true;
        }
         function Open_files(id_request_bond) {
             var hdfid_request_bond = document.getElementById('<%= hdfid_request_bond.ClientID %>');
             hdfid_request_bond.value = id_request_bond;
             document.getElementById('<%= btnviewrequest.ClientID%>').click();
             return false
         }

        function Download(path) {
            var hdfpath = document.getElementById('<%= hdfpath.ClientID %>');
            hdfpath.value = path;
            console.log(path);
            document.getElementById('<%= lnkdescargas.ClientID%>').click();
        }
    </script>
     <style type="text/css">
         .auto-style1 {
             width: 75px;
         }
     </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div id="div_body_reportedashboard">

        <div class="row">
            <div class="col-lg-12">
                <h3 class="page-header">Reporte de bonos</h3>
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
                                        <th style="max-width: 40px; text-align: center;" scope="col">Archivos</th>
                                        <th style="min-width: 60px; text-align: center;" scope="col" class="auto-style1">Solicitud</th>
                                        <th style="min-width: 40px; text-align: center;" scope="col">Fecha</th>
                                        <th style="min-width: 80px; text-align: center;" scope="col">Tipo de bono</th>
                                        <th style="min-width: 150px; text-align: center;" scope="col">Empleado</th>
                                        <th style="min-width: 80px; text-align: center;" scope="col">Monto</th>
                                        <th style="min-width: 50px; text-align: center;" scope="col">Estatus</th>
                                        <th style="min-width: 50px; text-align: center;" scope="col">CC cargo</th>
                                        <th style="min-width: 120px; text-align: center;" scope="col">Solicitado por</th>
                                        <th style="min-width: 120px; text-align: center;" scope="col">Autorizado/rechazado por</th>
                                         <th style="min-width: 15px; text-align: left; display: none" scope="col">Archivo</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="repeater_reporte_bonos" runat="server" OnItemDataBound="repeater_reporte_bonos_ItemDataBound">
                                        <ItemTemplate>
                                            <tr style="font-size: 11px">   
                                                <%--<td style="text-align: center">
                                                    <a class="btn btn-success btn-flat btn-xs"  id="repLinkFiles"
                                                        onclick='<%# "return Open_files("+ Eval("id_request_bond").ToString()+");" %>'>Archivos
                                                    </a>
                                                </td>--%>
                                                <td>
                                                    <asp:LinkButton ID="repLinkFiles" class="btn btn-success btn-flat btn-xs" runat="server" OnClientClick='<%# "return Open_files("+ Eval("id_request_bond").ToString()+");" %>'>Archivos</asp:LinkButton>
                                                </td>
                                                <td style="text-align: center;"><%# Eval("id_request_bond") %></td>                                             
                                                <td style="text-align: center;"><%# Eval("created", "{0:d}") %></td>
                                                <td style="text-align: center;"><%# Eval("bond_name") %></td>
                                                <td style="text-align: center;"><%# Eval("full_name") %></td>                                                
                                                <td style="text-align: center;"><%# Convert.ToDecimal(Eval("authorization_amount")).ToString("C2") %></td>
                                                <td style="text-align: center;"><%# Eval("request_status_name")%></td>
                                                <td style="text-align: center;"><%# Eval("CC_Cargo") %></td>
                                                <td style="text-align: center;"><%# Eval("created_by") %></td>
                                                <td style="text-align: center;"><%# Eval("modified_by") %></td>
                                                <td style="display: none">
                                                    <asp:Label ID="lblArchivo" runat="server" Text='<%# Eval("Archivo") %>'></asp:Label>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>
                        </div>
                    </div>
                    <div class=" box-footer">

                       <%-- <asp:LinkButton ID="lnkgenerarpdf" CssClass="btn btn-danger btn-flat"
                            OnClick="lnkgenerarpdf_Click" runat="server">
                        <i class="fa fa-file-pdf-o" aria-hidden="true"></i>&nbsp;Exportar a PDF
                        </asp:LinkButton>--%>
                        <asp:LinkButton ID="lnkgenerarexcel" CssClass="btn btn-success btn-flat"
                            OnClick="lnkgenerarexcel_Click" runat="server">
                        <i class="fa fa-file-excel-o" aria-hidden="true"></i>&nbsp;Exportar a Excel
                        </asp:LinkButton>
                    </div>
                </div>

            </div>
        </div>

        <div class="modal fade bs-example-modal-lg" tabindex="-1" id="ModalBonos" role="dialog" aria-labelledby="mySmallModalLabel" data-backdrop="static" data-keyboard="false">
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
                                <div class="col-lg-4 col-md-6 col-sm-12">
                                    <asp:TextBox ID="txtsolicitud" MaxLength="250" CssClass=" form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-lg-4 col-md-6 col-sm-12">
                                    <asp:DropDownList ID="ddlbonos" MaxLength="250" CssClass=" form-control" runat="server"></asp:DropDownList>
                                </div>
                                <div class="col-lg-4 col-md-6 col-sm-12">
                                    <asp:DropDownList ID="ddlEstatus" MaxLength="250" CssClass=" form-control" runat="server"></asp:DropDownList>
                                </div>
                            </div>  
                            <div class="row">
                                <div class="col-lg-6 col-md-6 col-sm-6  col-xs-6">
                                    <h6><strong><i class="fa fa-calendar" aria-hidden="true"></i>&nbsp;Fecha Inicial</strong></h6>
                                    <telerik:RadDatePicker ID="rdpfechainicial" runat="server" Width="100%" Skin="Bootstrap"></telerik:RadDatePicker>                                     
                                </div>
                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6"  style="font-size:10px;">
                                    <h6><strong><i class="fa fa-calendar" aria-hidden="true"></i>&nbsp;Fecha Final</strong></h6>
                                    <telerik:RadDatePicker ID="rdpfechafinal" runat="server" Width="100%"  Skin="Bootstrap"></telerik:RadDatePicker>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12 col-xs-12">
                                    <h6><strong><i class="fa fa-users" aria-hidden="true"></i>&nbsp;Seleccione el empleado a consultar</strong>
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
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlempleado_a_consultar_SelectedIndexChanged" runat="server">
                                    </asp:DropDownList>
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
                                OnClientClick="return ConfirmwidgetBonosModal();" runat="server">
                                            <i class="fa fa-database" aria-hidden="true"></i>&nbsp;Generar Reporte
                            </asp:LinkButton>
                        </div>
                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

        <div class="modal fade bs-example-modal-lg" tabindex="-1" id="modal_archivos" role="dialog"
        aria-labelledby="mySmallModalLabel" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg" role="document">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnviewrequest" EventName="Click" />                    
                    <%--<asp:AsyncPostBackTrigger ControlID="lnkadjuntarfiles" EventName="Click" />--%>
                    <asp:PostBackTrigger ControlID="lnkdescargas" />                 
                </Triggers>
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">×</span></button>
                            <h4 class="modal-title">Seleccione un archivo</h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                              <%--  <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                    <h5><strong><i class="fa fa-file-archive-o" aria-hidden="true"></i>&nbsp;Documento</strong></h5>
                                    <telerik:RadAsyncUpload RenderMode="Lightweight" ID="AsyncUpload1" runat="server"
                                        OnFileUploaded="AsyncUpload1_FileUploaded" PostbackTriggers="lnkguardaresultados"
                                        MaxFileSize="2097152" Width="100%"
                                        AutoAddFileInputs="false" Localization-Select="Seleccionar" Skin="Bootstrap" />
                                </div>--%>
                           <%--     <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                    <br />
                                    <asp:LinkButton OnClientClick="return false;" ID="lnkguardaresultadosload" CssClass="btn btn-primary btn-flat pull-right" runat="server" Style="display: none;">
                                            <i class="fa fa-refresh fa-spin fa-fw"></i>
                                            <span class="sr-only">Loading...</span>&nbsp;Guardando
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="lnkguardaresultados"
                                        OnClientClick="return ConfirmLoadResultados('¿Desea guardar el resultado?');"
                                        OnClick="lnkguardaresultados_Click" CssClass="btn btn-primary btn-flat pull-right" runat="server">
                                            Guardar documento&nbsp;<i class="fa fa-floppy-o" aria-hidden="true"></i>
                                    </asp:LinkButton>
                                </div>--%>
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                    <div class="table table-responsive">
                                        <table id="table_archivos" class=" table table-responsive table-bordered table-condensed"
                                            style="font-size: 12px;">
                                            <thead>
                                                <tr>
                                                   <%-- <th style="max-width: 10px; text-align: center;" scope="col"></th>--%>
                                                    <th style="max-width: 10px; text-align: center;" scope="col"></th>
                                                    <th style="min-width: 200px; text-align: left;" scope="col">Documento</th>
                                                    <th style="min-width: 90px; text-align: center;" scope="col">Tamaño</th>
                                                    <th style="min-width: 240px; text-align: left;" scope="col">Fecha de carga</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="repeater_archivos" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <%--<td style="text-align: center;">
                                                                <asp:LinkButton
                                                                    OnClientClick="return confirm('¿Desea eliminar este archivo de la solicitud?');"
                                                                    OnClick="lnkdeletefile_Click" class="btn btn-primary btn-flat btn-xs"
                                                                    ID="lnkdeletefile" runat="server"
                                                                    file_name='<%# Eval("file_name").ToString().Trim() %>'>
                                                                  Eliminar
                                                                </asp:LinkButton>
                                                            </td>--%>
                                                            <td style="text-align: center;">
                                                                <a style="cursor: pointer;" class="btn btn-success btn-flat btn-xs"
                                                                    onclick='<%#"return Download("+@""""+ Eval("file_name").ToString().Replace(@"\","/").Trim()+@""""+");" %>'>Descargar</a>

                                                            </td>
                                                            <td>
                                                                <%# Eval("file_name").ToString().Trim() %>
                                                            </td>
                                                            <td style="text-align: center;">
                                                                <%# Math.Round((Convert.ToDecimal(Eval("size"))/1000000),2).ToString()+" mb" %>
                                                            </td>
                                                            <td>
                                                                <%# Convert.ToDateTime(Eval("date_attach")).ToString("dddd dd MMMM, yyyy h:mm:ss tt", System.Globalization.CultureInfo.CreateSpecificCulture("es-MX")) %>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                                <asp:HiddenField ID="hdfpath" runat="server" />
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default pull-left" data-dismiss="modal">Cerrar</button>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <asp:HiddenField ID="hdfsessionid" runat="server" />
    <asp:HiddenField ID="hdfguid" runat="server" />
    <asp:HiddenField ID="hdfid_request_bond" runat="server" />
    <asp:HiddenField ID="hdfFechaInicial" runat="server" />
    <asp:HiddenField ID="hdfFechaFinal" runat="server" />
    <asp:HiddenField ID="hdfid_profile" runat="server" />
    <asp:Button ID="btnviewrequest"  runat="server" Text="" Style="display: none" OnClick="btnviewrequest_Click" />
    <asp:Button ID="lnkdescargas" Style="display: none;" runat="server" Text="Button" OnClick="lnkdescargas_Click" />
    </div>
</asp:Content>
