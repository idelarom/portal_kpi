<%@ Page Title="" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="inicio.aspx.cs" Inherits="presentacion.inicio" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 
    <script type="text/javascript">
        function ModalClose() {
            $('#myModalExcel').modal('hide');
        }
        function ConfirmExcelModal(msg) {
            if (confirm(msg)) {
                $("#<%= lnkloadingexcel.ClientID%>").show();
                $("#<%= lnksubirexcel.ClientID%>").hide();
                return true;
            } else {
                return false;
            }
        }
       
    </script>
    <style type="text/css">
        .small-box .icon {
            -webkit-transition: all .3s linear;
            -o-transition: all .3s linear;
            transition: all .3s linear;
            position: absolute;
            top: -10px;
            right: 10px;
            z-index: 0;
            font-size: 65px;
            color: white;
        }
        .rcbList li{
            font-size: 10px;
        }
        .rcbCheckAllItems label{
            font-size: 11px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <h4 class="page-header">DashBoard</h4>
        </div>
    </div>
    
        <div class="row" id="div_usuarios" runat="server">
            <asp:Repeater ID="repeat_menu" runat="server">
                <ItemTemplate>
                    <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12">
                        <!-- small box -->
                        <div class='<%#Eval("color_menu") %>' onclick="<%# Eval("onclick") %>"  style="cursor: pointer">
                            <div class="inner">
                                <h3></h3>

                                <p><%#Eval("name") %></p>
                            </div>
                            <div class="icon">
                                <i class='<%#Eval("icon") %>'></i>
                            </div>
                            <a href='<%#Eval("menu") %>' class="small-box-footer"><i class="fa fa-arrow-circle-right"></i></a>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>

        </div>
    <div class="row">
        <asp:UpdatePanel ID="UODATA" runat="server" UpdateMode="Always">
            <ContentTemplate>

                <div class="col-lg-6 col-md-6 col-sm-12">
                    <!-- TABLE: LATEST ORDERS -->
                    <div class="box box-danger">
                        <div class="box-header with-border">
                            <h3 class="box-title">Proyectos</h3>
                            <div class="box-tools pull-right">
                                <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                    <i class="fa fa-minus"></i>
                                </button>
                            </div>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <div class="row" id="div_combo_pm_x_proyecto" runat="server" visible="false">
                                <div class="col-lg-4 col-md-2 hidden-sm hidden-xs"></div>
                                <div style="text-align: right;" class="col-lg-8 col-md-10 col-sm-12 col-xs-12" >
                                   
                                    <telerik:RadComboBox RenderMode="Lightweight" ID="ddlpm_x_proyecto" runat="server" CheckBoxes="true"
                                        EnableCheckAllItemsCheckBox="true" style="font-size:11px;" EmptyMessage="Filtrar por PM"
                                        Width="88%" Skin="Bootstrap" Localization-AllItemsCheckedString="Todos los PM seleccionados"
                                        Localization-NoMatches="No hay resultados" OnItemChecked="ddlpm_x_proyecto_ItemChecked" Localization-CheckAllString="Seleccionar todos" Localization-ItemsCheckedString="PM selecionado(s)">
                                  
                                    </telerik:RadComboBox>
                                    <span>
                                    <asp:LinkButton Style="font-size: 11px" ID="lnkfiltro" Width="10%" runat="server" OnClick="lnkfiltro_Click" CssClass="btn btn-primary btn-sm"><i class="fa fa-search" aria-hidden="true"></i></asp:LinkButton>
                                    <%--  <asp:DropDownList ID="ddlpm_x_proyecto" Width="200px" CssClass="btn btn-default btn-flat right" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlpm_x_proyecto_SelectedIndexChanged"></asp:DropDownList>
                                    --%></span>
                                </div>
                            </div>
                            
                            <div class="table-responsive">
                                <table class="table no-margin">
                                    <thead>
                                        <tr>
                                            <th>Proyecto</th>
                                            <th style="text-align: center; width: 60px;">Planeación</th>
                                            <th style="text-align: center; width: 60px;">Ejecución</th>
                                            <th style="text-align: center; width: 60px;">Cierre</th>
                                            <th id="th_pm" runat="server" visible="true" style="text-align: left; width: 200px;">PM</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="repeat_mis_proyectos" runat="server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td><a href='<%# "proyecto_general.aspx?id_proyecto="+ presentacion.funciones.deTextoa64(Eval("id_proyecto").ToString()) %>'><%# Eval("proyecto") %></a></td>
                                                    <td style="text-align: center;"><%# Eval("Planeación") %></td>
                                                    <td style="text-align: center;"><%# Eval("Ejecución") %></td>
                                                    <td style="text-align: center;"><%# Eval("Cierre") %></td>
                                                    <td id="td_pm" runat="server" visible='<%# Convert.ToBoolean(Eval("view_pm_filters"))%>' 
                                                        style="text-align: left; font-size:10px;"><%# Eval("PM") %></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                </table>
                            </div>
                            <!-- /.table-responsive -->
                        </div>
                        <!-- /.box-body -->
                        <div class="box-footer clearfix">
                            <a href="javascript:void(0)" class="btn btn-sm btn-primary btn-flat pull-left" onclick="return ModalShow('#myModalExcel');" id="nvoproyect" runat="server">Agregar Proyecto</a>
                            <a href="catalogo_proyectos.aspx" class="btn btn-sm btn-default btn-flat pull-right">Todos los Proyectos</a>
                        </div>
                        <!-- /.box-footer -->
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <div class="modal fade bs-example-modal-lg" tabindex="-1" id="myModalExcel" role="dialog" aria-labelledby="mySmallModalLabel">
        <div class="modal-dialog modal-lg" role="document">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <Triggers>
                    <asp:PostBackTrigger ControlID="lnksubirexcel" />
                </Triggers>
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">×</span></button>
                            <h4 class="modal-title">Alta de Proyecto<small>&nbsp;<asp:LinkButton ID="lnknuevoproyect" OnClick="lnknuevoproyect_Click" CssClass="btn btn-danger btn-flat btn-xs" runat="server">Puede Agregar un Proyecto Manualmente</asp:LinkButton>
                                </small></h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <p>Puede Agregar un nuevo Proyecto mediante un archivo de CSV.</p>
                                    <ul>
                                        <li>La Primer Fila, debe contener el nombre del Proyecto, por lo que las actividades seran tomadas en cuenta, a partir de la segunda fila.</li>
                                        <li>Se le recomienda utilizar el asistente de exportación de Microsoft Project para generar el archivo de CSV</li>
                                        <li>Si tiene dudas, contacte al administrador</li>
                                    </ul>
                                    <asp:FileUpload ID="fuparchivos" runat="server" onchange="ValidateUF(this,5);" />
                                    </div>
                            </div>
                             <div class="row" id="div_proyecto_manual" runat="server" visible="false">
                                <div class="col-lg-12">
                                    <h5><strong><i class="fa fa-bars" aria-hidden="true"></i>&nbsp;Proyecto</strong></h5>
                                    <telerik:RadTextBox ID="rtxtproyecto" Width="100%" runat="server" Skin="Bootstrap" placeholder="Proyecto"></telerik:RadTextBox>
                                </div>
                                <div class="col-lg-12">
                                    <h5><strong><i class="fa fa-bars" aria-hidden="true"></i>&nbsp;Descripción del Proyecto</strong></h5>
                                    <telerik:RadTextBox ID="rtxtdescripcion" Width="100%" runat="server" Skin="Bootstrap" placeholder="Descripción" TextMode="MultiLine" Rows="3"></telerik:RadTextBox>
                                </div>
                                <div class="col-lg-6 col-sm-6 col-xs-12">
                                    <h5><strong><i class="fa fa-calendar" aria-hidden="true"></i>&nbsp;Fecha Inicio</strong></h5>
                                    <telerik:RadDatePicker ID="rdtpinicio" runat="server" Width="100%" Skin="Bootstrap"></telerik:RadDatePicker>
                                </div>
                                <div class="col-lg-6 col-sm-6 col-xs-12">
                                    <h5><strong><i class="fa fa-calendar" aria-hidden="true"></i>&nbsp;Fecha Fin</strong></h5>
                                    <telerik:RadDatePicker ID="rdtpfin" runat="server" Width="100%" Skin="Bootstrap"></telerik:RadDatePicker>
                                </div>
                                <div class="col-lg-12">
                                    <h5><strong><i class="fa fa-wrench" aria-hidden="true"></i>&nbsp;Avance</strong></h5>
                                    <telerik:RadTextBox ID="rtxtavance" runat="server" Skin="Bootstrap" InputType="Number">

                                        <ClientEvents OnKeyPress="keyPressInteger" />
                                    </telerik:RadTextBox>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer ">

                            <div class="row" id="div_errormodal2" runat="server" visible="false" style="text-align: left;">
                                <div class="col-lg-12">
                                    <div class="alert alert-danger alert-dismissible">
                                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                        <asp:Label ID="lblerrormodal2" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <button type="button" class="btn btn-default pull-left" data-dismiss="modal">Cerrar</button>
                            <asp:LinkButton OnClientClick="return false;" ID="lnkloadingexcel" CssClass="btn btn-success btn-flat" runat="server" Style="display: none;">
                                            <i class="fa fa-refresh fa-spin fa-fw"></i>
                                            <span class="sr-only">Loading...</span>&nbsp;Cargando Archivo...
                            </asp:LinkButton>
                            <asp:LinkButton ID="lnksubirexcel" OnClientClick="return ConfirmExcelModal('¿Desea Guardar el proyecto?');" CssClass="btn btn-success btn-flat" runat="server"
                                OnClick="lnksubirexcel_Click">
                                            <i class="fa fa-upload" aria-hidden="true"></i>&nbsp;Subir Archivo
                            </asp:LinkButton>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>