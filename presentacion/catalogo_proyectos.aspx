<%@ Page Title="Catalogo Proyectos" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="catalogo_proyectos.aspx.cs" Inherits="presentacion.catalogo_proyectos" %>

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
                "ordering": false,
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

        function ModalClose() {
            $('#myModal').modal('hide');
            $('#myModalEntregablle').modal('hide');
            $('#myModalTareas').modal('hide');
            $('#myModalExcel').modal('hide');
        }

        function OnClickConfirm() {
            var nombre = $("#<%= rtxtproyecto.ClientID%>").val();
            var fechainicio = $("#<%= rdtpinicio.ClientID%>").val();
            var fechafin = $("#<%= rdtpfin.ClientID%>").val();
            var descripcion = $("#<%= rtxtdescripcion.ClientID%>").val();
            var avance = $("#<%= rtxtavance.ClientID%>").val();
            if (nombre != "" && fechafin != "" && fechainicio != "" && avance != "" && descripcion != "") {

                return confirm('¿Desea Guardar los datos del Proyecto?');
            }
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="row">
        <div class="col-lg-12">
            <h4 class="page-header">Catalogo de Proyectos
                
            </h4>
            <asp:LinkButton ID="lnknuevoproyecto" OnClick="lnknuevoproyecto_Click" CssClass="btn btn-danger btn-flat" runat="server">Nuevo Proyecto&nbsp;<i class="fa fa-plus" aria-hidden="true"></i></asp:LinkButton>
                &nbsp;<asp:LinkButton ID="lnknuevoproyectoExcel" OnClick="lnknuevoproyectoExcel_Click" CssClass="btn btn-success btn-flat" runat="server">Nuevo Proyecto Mediante Archivo&nbsp;<i class="fa fa-file" aria-hidden="true"></i></asp:LinkButton>
              
        </div>
    </div>
    <div class="row">
        <div class="col-lg-12">
            <div class="table table-responsive">

                <telerik:RadGrid runat="server" ID="grid_proyectos" Skin="Metro">
                    <MasterTableView AutoGenerateColumns="false" CssClass="dvv table table-responsive"
                        HeaderStyle-BackColor="White" HeaderStyle-ForeColor="Black"
                        Width="100%">
                        <Columns>
                            <telerik:GridTemplateColumn>
                                <HeaderStyle Width="30px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkeditar" runat="server" CommandName="Editar"
                                        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "id_proyecto").ToString() %>' OnClick="lnkeditar_Click">
                                                <i class="fa fa-pencil fa-2x" aria-hidden="true"></i>
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn>
                                <HeaderStyle Width="30px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton2" OnClientClick="return confirm('¿Desea Eliminar este Proyecto?');" runat="server" CommandName="Eliminar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "id_proyecto").ToString() %>'>
                                                <i class="fa fa-trash fa-2x" aria-hidden="true"></i>
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            
                            <telerik:GridTemplateColumn HeaderText="Proyecto">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnks" runat="server" PostBackUrl=' <%#"proyecto_general.aspx?id_proyecto="+ presentacion.funciones.deTextoa64(Eval("id_proyecto") .ToString())%>'> <%# Eval("Proyecto") %></asp:LinkButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="avance" HeaderText="% Avance" UniqueName="avance"
                                Visible="true">
                                <HeaderStyle Width="70px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>

                            <telerik:GridTemplateColumn HeaderText="Planeación">
                                <HeaderStyle Width="30px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <%# Eval("Planeación") %>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="Ejecución">
                                <HeaderStyle Width="30px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <%# Eval("Ejecución") %>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="Cierre">
                                <HeaderStyle Width="30px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <%# Eval("Cierre") %>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="PM">
                                <HeaderStyle Width="200px" />
                                <ItemStyle HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <%# Eval("pm") %>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </div>
        </div>
    </div>

    <div class="modal fade bs-example-modal-lg" tabindex="-1" id="myModal" role="dialog" aria-labelledby="mySmallModalLabel" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg" role="document">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="lnknuevoproyecto" EventName="Click" />
                </Triggers>
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">×</span></button>
                            <h4 class="modal-title">Proyecto</h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
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
                            </div>
                            <div class="row">
                                <div class="col-lg-12">
                                    <h5><strong><i class="fa fa-wrench" aria-hidden="true"></i>&nbsp;Avance</strong></h5>
                                    <telerik:RadTextBox ID="rtxtavance" runat="server" Skin="Bootstrap" InputType="Number">

                                        <ClientEvents OnKeyPress="keyPressInteger" />
                                    </telerik:RadTextBox>
                                </div>
                            </div>
                            <asp:TextBox ID="txtid_proyecto" Text="" Visible="false" runat="server"></asp:TextBox>
                        </div>
                        <div class="modal-footer ">
                            <div class="row" id="div_errormodal" runat="server" visible="false" style="text-align: left;">

                                <div class="col-lg-12">
                                    <div class="alert alert-danger alert-dismissible">
                                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                        <asp:Label ID="lblerrormodal" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <button type="button" class="btn btn-default pull-left" data-dismiss="modal">Cerrar</button>
                            <asp:LinkButton ID="lnkeditar_cambios"
                                OnClientClick="return OnClickConfirm();" CssClass="btn btn-primary btn-flat" runat="server" OnClick="lnkeditar_cambios_Click">
                           <i class="fa fa-floppy-o" aria-hidden="true"></i>&nbsp;Guardar
                            </asp:LinkButton>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
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
                            <h4 class="modal-title">Alta de Proyecto</h4>
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
                                    <asp:FileUpload ID="fuparchivos" runat="server"  onchange="return ValidateUF(this,5);" />
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