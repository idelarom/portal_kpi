<%@ Page Title="" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="catalogo_perfiles.aspx.cs" Inherits="presentacion.catalogo_perfiles" %>

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

        function ConfirmEmpleadoProyectoModal(msg) {
            if (confirm(msg)) {
                $("#<%= lnkcargando.ClientID%>").show();
                $("#<%= lnkguardar.ClientID%>").hide();
                return true;
            } else {
                return false;
            }
        }

        function ChangedTextLoad2() {
            $("#<%= imgloadempleado_.ClientID%>").show();
            $("#<%= lblbe2.ClientID%>").show();
            return true;
        }

        function ChangedTextLoad3() {
            $("#<%= imgmenu.ClientID%>").show();
            $("#<%= lblmenu.ClientID%>").show();
            return true;
        }
        function ChangedTextLoad1() {
            $("#<%= img_widget.ClientID%>").show();
              $("#<%= lblwidget.ClientID%>").show();
              return true;
          }
          function OpenModalEditGrid(id_perfil, command) {
              var myHidden = document.getElementById('<%= hdfid_perfil.ClientID %>');

              myHidden.value = id_perfil;

              var commando = document.getElementById('<%= hdfcommand.ClientID %>');

              commando.value = command;
              document.getElementById('<%= btneventgrid.ClientID%>').click();
              return false;
          }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <h4 class="page-header">Catálogo Perfiles de Usuarios</h4>
        </div>

        <div class="col-lg-12">
            <asp:LinkButton ID="lnknuevoperfil" OnClick="lnknuevoperfil_Click" CssClass="btn btn-primary btn-flat" runat="server">
                Nuevo Perfil de Usuario&nbsp;<i class="fa fa-plus" aria-hidden="true"></i>
            </asp:LinkButton>
            <div class="table table-responsive">
                <telerik:RadGrid ID="grid_perfiles" runat="server" Skin="Metro">
                    <MasterTableView AutoGenerateColumns="false" CssClass="dvv table table-responsive"
                        HeaderStyle-BackColor="White" HeaderStyle-ForeColor="Black"
                        Width="100%">
                        <Columns>
                            <telerik:GridTemplateColumn HeaderText="">
                                <HeaderStyle Width="50px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <a style="cursor: pointer;" onclick='<%# "return OpenModalEditGrid("+DataBinder.Eval(Container.DataItem, "id_perfil").ToString()+@",""" +"actualizar"+@""""+");" %>'>
                                        <i class="fa fa-pencil fa-2x" aria-hidden="true"></i>
                                    </a>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="">
                                <HeaderStyle Width="50px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkcommand2" Visible="true" runat="server" CommandName="Eliminar"
                                        OnClientClick="return ConfirmEntregableDelete('¿Desea Eliminar este Perfil?')" OnClick="lnkcommand2_Click"
                                        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "id_perfil").ToString() %>'>
                                         <i class="fa fa-trash fa-2x" aria-hidden="true"></i>
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>

                            <telerik:GridBoundColumn DataField="Perfil" HeaderText="Perfil" UniqueName="Perfil"
                                Visible="true">
                            </telerik:GridBoundColumn>

                            <telerik:GridTemplateColumn HeaderText="Usuarios Relacionados">
                                <HeaderStyle Width="140px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <button class="btn btn-warning btn-flat" style="cursor: pointer;" onclick='<%# "return OpenModalEditGrid("+DataBinder.Eval(Container.DataItem, "id_perfil").ToString()+@",""" +"usuarios"+@""""+");" %>'>
                                        <%# DataBinder.Eval(Container.DataItem, "total_usuarios").ToString() %>
                                    </button>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="Widgets Relacionados">
                                <HeaderStyle Width="140px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <button class="btn btn-info btn-flat" style="cursor: pointer;" onclick='<%# "return OpenModalEditGrid("+DataBinder.Eval(Container.DataItem, "id_perfil").ToString()+@",""" +"widgets"+@""""+");" %>'>
                                        <%# DataBinder.Eval(Container.DataItem, "total_widgets").ToString() %>
                                    </button>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="Menus Relacionados">
                                <HeaderStyle Width="130px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <button class="btn btn-danger btn-flat" style="cursor: pointer;" onclick='<%# "return OpenModalEditGrid("+DataBinder.Eval(Container.DataItem, "id_perfil").ToString()+@",""" +"menus"+@""""+");" %>'>
                                        <%# DataBinder.Eval(Container.DataItem, "total_menus").ToString() %>
                                    </button>
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
            <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btneventgrid" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="lnknuevoperfil" EventName="Click" />
                </Triggers>
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">×</span></button>
                            <h4 class="modal-title">Perfiles de Usuario</h4>
                        </div>
                        <div class="modal-body">
                            <div class="row" id="div_perfil" runat="server">
                                <div class="col-lg-12 col-sm-12">
                                    <h5><strong><i class="fa fa-user" aria-hidden="true"></i>&nbsp;Nombre del Perfil</strong></h5>
                                    <telerik:RadTextBox ID="rtxtperfil" Width="100%" runat="server" Skin="Bootstrap"></telerik:RadTextBox>
                                </div>
                                <div class="col-lg-12 col-sm-12">
                                    <asp:CheckBox ID="cbxvertodosempleados" Text="Ver todos los empleados." runat="server" />
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="nav-tabs-custom">
                                        <ul class="nav nav-tabs">
                                            <li class="active" id="tusu" runat="server">
                                                <asp:LinkButton ID="lnkusuarios" CommandName="usuarios" runat="server" OnClick="lnkusuarios_Click">
                                                    <strong><i class="fa fa-users" aria-hidden="true"></i>&nbsp;Usuarios</strong>
                                                </asp:LinkButton>
                                            </li>
                                            <li class="" id="twid" runat="server">
                                                <asp:LinkButton ID="LinkButton6" CommandName="widgets" runat="server" OnClick="lnkusuarios_Click"><strong>
                                                   <i class="fa fa-window-restore" aria-hidden="true"></i>&nbsp;Widgets</strong></asp:LinkButton>
                                            </li>
                                            <li class="" id="tmen" runat="server">
                                                <asp:LinkButton ID="LinkButton7" CommandName="menus" runat="server" OnClick="lnkusuarios_Click"><strong>
                                                    <i class="fa fa-window-maximize" aria-hidden="true"></i>&nbsp;Menus</strong></asp:LinkButton>
                                            </li>
                                        </ul>
                                        <div class="tab-content">
                                            <div class="tab-pane active" id="div_empleados" runat="server">
                                                <div class="row">
                                                    <div class="col-lg-12 col-xs-12">
                                                        <h5><strong>Usuarios relacionados a este perfil</strong></h5>
                                                        <div style="text-align: left;" class="input-group input-group-sm">
                                                            <asp:TextBox ID="txtbuscarempleado" CssClass="form-control" placeholder="Buscar" runat="server"></asp:TextBox>
                                                            <span class="input-group-btn">
                                                                <asp:LinkButton ID="btnbuscarempleado2" CssClass="btn btn-primary btn-flat" runat="server"
                                                                    OnClientClick="return ChangedTextLoad2();" OnClick="btnbuscarempleado2_Click">
                                                                    <i class="fa fa-search" aria-hidden="true"></i>
                                                                </asp:LinkButton>
                                                            </span>
                                                        </div>

                                                        <asp:Image ID="imgloadempleado_" Style="display: none;" ImageUrl="~/img/load.gif" runat="server" />
                                                        <label id="lblbe2" runat="server" style="display: none; color: #1565c0">Buscando Empleados</label>
                                                    </div>

                                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                        <asp:CheckBox ID="cbxcheckall_empleados" Text="Seleccionar todos" OnCheckedChanged="cbxcheckall_empleados_CheckedChanged" AutoPostBack="true" runat="server" />
                                                        <div style="height: 150px; min-width: 500px; overflow: scroll;">
                                                            <asp:Repeater ID="rdllista_empleados" runat="server">
                                                                <ItemTemplate>
                                                                    <asp:UpdatePanel ID="jajaja" runat="server" UpdateMode="Always">
                                                                        <Triggers>
                                                                            <asp:AsyncPostBackTrigger ControlID="mycheck" EventName="CheckedChanged" />
                                                                        </Triggers>
                                                                        <ContentTemplate>
                                                                            <asp:CheckBox ID="mycheck" Text='<%# Eval("nombre_usuario").ToString()  %>'
                                                                                ToolTip='<%# Eval("Usuario_Red").ToString()  %>' OnCheckedChanged="mycheck_CheckedChanged"
                                                                                runat="server"
                                                                                AutoPostBack="true"></asp:CheckBox>
                                                                            <br />
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="tab-pane" id="div_widgets" runat="server">
                                                <div class="row">
                                                    <div class="col-lg-12 col-xs-12">
                                                        <h5><strong>Widgets relacionados a este perfil</strong></h5>
                                                        <div style="text-align: left;" class="input-group input-group-sm">
                                                            <asp:TextBox ID="txtbuscarwidget" CssClass="form-control" placeholder="Buscar" runat="server"></asp:TextBox>
                                                            <span class="input-group-btn">
                                                                <asp:LinkButton ID="lnkbuscarwidget" CssClass="btn btn-primary btn-flat" runat="server"
                                                                    OnClientClick="return ChangedTextLoad1();" OnClick="lnkbuscarwidget_Click">
                                                                        <i class="fa fa-search" aria-hidden="true"></i>
                                                                </asp:LinkButton>
                                                            </span>
                                                        </div>

                                                        <asp:Image ID="img_widget" Style="display: none;" ImageUrl="~/img/load.gif" runat="server" />
                                                        <label id="lblwidget" runat="server" style="display: none; color: #1565c0">Buscando Widgets</label>
                                                    </div>

                                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                        <asp:CheckBox ID="cbxcheckall_widgets" Text="Seleccionar todos"
                                                            OnCheckedChanged="cbxcheckall_widgets_CheckedChanged" AutoPostBack="true" runat="server" />

                                                        <div style="height: 150px; min-width: 500px; overflow: scroll;">
                                                            <asp:Repeater ID="repeater_widgets" runat="server">
                                                                <ItemTemplate>
                                                                    <asp:UpdatePanel ID="jajaja" runat="server" UpdateMode="Always">
                                                                        <Triggers>
                                                                            <asp:AsyncPostBackTrigger ControlID="mycheck_widgets" EventName="CheckedChanged" />
                                                                        </Triggers>
                                                                        <ContentTemplate>
                                                                            <asp:CheckBox ID="mycheck_widgets" Text='<%# Eval("widget").ToString()  %>'
                                                                                ToolTip='<%# Eval("id_widget").ToString()  %>' OnCheckedChanged="mycheck_widgets_CheckedChanged"
                                                                                runat="server"
                                                                                AutoPostBack="true"></asp:CheckBox>
                                                                            <br />
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="tab-pane" id="div_menus" runat="server">
                                                <div class="row">
                                                    <div class="col-lg-12 col-xs-12">
                                                        <h5><strong>Menus relacionados a este perfil</strong></h5>
                                                        <div style="text-align: left;" class="input-group input-group-sm">
                                                            <asp:TextBox ID="txtbuscarmenu" CssClass="form-control" placeholder="Buscar" runat="server"></asp:TextBox>
                                                            <span class="input-group-btn">
                                                                <asp:LinkButton ID="lnkbuscarmenu" CssClass="btn btn-primary btn-flat" runat="server"
                                                                    OnClientClick="return ChangedTextLoad3();" OnClick="lnkbuscarmenu_Click">
                                                                        <i class="fa fa-search" aria-hidden="true"></i>
                                                                </asp:LinkButton>
                                                            </span>
                                                        </div>

                                                        <asp:Image ID="imgmenu" Style="display: none;" ImageUrl="~/img/load.gif" runat="server" />
                                                        <label id="lblmenu" runat="server" style="display: none; color: #1565c0">Buscando Menus</label>
                                                    </div>

                                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                        <asp:CheckBox ID="cbxcheckall_menus" Text="Seleccionar todos"
                                                            OnCheckedChanged="cbxcheckall_menus_CheckedChanged" AutoPostBack="true" runat="server" />
                                                        <div style="height: 150px; min-width: 500px; overflow: scroll;">
                                                            <asp:Repeater ID="repeater_menus" runat="server">
                                                                <ItemTemplate>
                                                                    <asp:UpdatePanel ID="jajaja" runat="server" UpdateMode="Always">
                                                                        <Triggers>
                                                                            <asp:AsyncPostBackTrigger ControlID="mycheck_menus" EventName="CheckedChanged" />
                                                                        </Triggers>
                                                                        <ContentTemplate>
                                                                            <asp:CheckBox ID="mycheck_menus" Text='<%# Eval("name").ToString()  %>'
                                                                                ToolTip='<%# Eval("id_menu").ToString()  %>' OnCheckedChanged="mycheck_menus_CheckedChanged"
                                                                                runat="server"
                                                                                AutoPostBack="true"></asp:CheckBox>
                                                                            <br />
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <asp:TextBox ID="txtid_perfil" Visible="false" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="modal-footer ">
                            <div class="row" id="div_error" runat="server" visible="false" style="text-align: left;">
                                <div class="col-lg-12">
                                    <div class="alert alert-danger alert-dismissible">
                                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                        <asp:Label ID="lblerror" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <button type="button" class="btn btn-default pull-left" data-dismiss="modal">Cerrar</button>
                            <asp:LinkButton OnClientClick="return false;" ID="lnkcargando" CssClass="btn btn-primary btn-flat" runat="server" Style="display: none;">
                                            <i class="fa fa-refresh fa-spin fa-fw"></i>
                                            <span class="sr-only">Loading...</span>&nbsp;Guardando...
                            </asp:LinkButton>
                            <asp:LinkButton ID="lnkguardar" CssClass="btn btn-primary btn-flat" OnClick="lnkguardar_Click"
                                OnClientClick="return ConfirmEmpleadoProyectoModal('¿Desea Guardar este Perfil?');" runat="server">
                                            <i class="fa fa-floppy-o" aria-hidden="true"></i>&nbsp;Guardar
                            </asp:LinkButton>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <asp:Button ID="btneventgrid" OnClick="btneventgrid_Click" runat="server" Text="Button" Style="display: none;" />
    <asp:HiddenField ID="hdfmotivos" runat="server" />
    <asp:HiddenField ID="hdfcommand" runat="server" />
    <asp:HiddenField ID="hdfid_perfil" runat="server" />
</asp:Content>