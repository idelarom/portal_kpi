<%@ Page Title="Catalogo Usuarios" Language="C#" MasterPageFile="~/Pages/MP/Global.Master" AutoEventWireup="true" CodeBehind="catalogo_usuarios.aspx.cs" Inherits="presentacion.Pages.Catalogs.catalogo_usuarios" %>
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
        function EditarClick(usuario, no_) {
            var hdfusuario = document.getElementById('<%= hdfusuario.ClientID %>');
            hdfusuario.value = usuario;
            var hdfnum_empleado = document.getElementById('<%= hdfnum_empleado.ClientID %>');
            hdfnum_empleado.value = no_;
            hdfusuario.value = usuario;
            document.getElementById('<%= btnver.ClientID%>').click();
            return false;
        }
        
        function DelegadosClick(usuario) {
            var hdfusuario = document.getElementById('<%= hdfusuario.ClientID %>');
            hdfusuario.value = usuario;
            document.getElementById('<%= btndelegados.ClientID%>').click();
            return false;
        }

        function ChangedTextLoad3()
        {
            $("#<%= imgmenu.ClientID%>").show();
            $("#<%= lblmenu.ClientID%>").show();
            return true;
        }
        function ChangedTextLoadadd()
        {
            $("#<%= imgloadempleado.ClientID%>").show();
            $("#<%= lblbemp.ClientID%>").show();
            return true;
        }
        function ChangedTextLoadperfil()
        {
            $("#<%= imgperfil.ClientID%>").show();
            $("#<%= lblperfilb.ClientID%>").show();
            return true;
        }
        function ChangedTextLoad4()
        {
            $("#<%= imgpermiso.ClientID%>").show();
            $("#<%= lblpermiso.ClientID%>").show();
            return true;
        }
        function ChangedTextLoad2() {
            $("#<%= imgloadempleado_.ClientID%>").show();
            $("#<%= lblbe2.ClientID%>").show();
            return true;
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
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
              <div class="col-lg-12">
            <h4 class="page-header">Catálogo usuarios</h4>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-12">
            <div class="box box-danger">
                <div class="box-body">
            
                  
            <asp:LinkButton ID="lnknuevousuario" CssClass="btn btn-primary btn-flat" OnClick="lnknuevousuario_Click" runat="server">
               Nuevo usuario&nbsp;<i class="fa fa-plus" aria-hidden="true"></i>
            </asp:LinkButton>
                    <div class="table-responsive">
                        <table class="dvv table no-margin table-condensed">
                            <thead>
                                <tr style="font-size: 11px;">
                                    <th style="max-width: 20px; text-align: center;" scope="col"></th>
                                    <th style="min-width: 40px; text-align: left;" scope="col">Usuario</th>
                                    <th style="min-width: 70px; text-align: left;" scope="col">No. Empleado</th>
                                    <th style="min-width: 200px; text-align: left;" scope="col">Nombre</th>
                                    <th style="min-width: 200px; text-align: left;" scope="col">Puesto</th>
                                    <th style="min-width: 100px; text-align: left;" scope="col">Correo</th>
                                    <th style="min-width: 150px; text-align: left;" scope="col">Perfil</th>
                                    <th style="min-width: 40px; text-align: left;" scope="col">Delegados</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="repeat_usuarios" runat="server">
                                    <ItemTemplate>
                                        <tr style="font-size: 11px">
                                            <td style="text-align: center;">
                                                <a style="cursor: pointer;" 
                                                    onclick='<%# "return EditarClick("+@""""+Eval("usuario")+@"""" +","+Eval("num_empleado")+");" %>'>
                                                    <i class="fa fa-pencil fa-2x" aria-hidden="true"></i>
                                                </a>
                                            </td>
                                            <td style="text-align: left;"><%# Eval("Usuario") %></td>
                                            <td style="text-align: center;"><%# Eval("num_empleado") %></td>
                                            <td style="text-align: left;"><%# Eval("Nombre") %></td>
                                            <td style="text-align: left;"><%# Eval("Puesto") %></td>
                                            <td style="text-align: left;"><%# Eval("correo") %></td>
                                            <td style="text-align: left;"><%# Eval("Perfil") %></td>
                                            <td style="text-align: center;"> 
                                                 <a style="cursor: pointer;min-width:40px" class="btn btn-primary btn-flat btn-xs" 
                                                    onclick='<%# "return DelegadosClick("+@""""+Eval("usuario")+@"""" +");" %>'>
                                                   <%# Eval("total_delegados") %>
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
    </div>
    <div class="modal fade bs-example-modal-lg" tabindex="-1" id="ModalEmpleado" role="dialog" aria-labelledby="mySmallModalLabel" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg" role="document">
            <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnver" EventName="Click" />
                </Triggers>
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">×</span></button>
                            <h4 class="modal-title">Detalles del empleado</h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-lg-12 col-md-12 col-sm-12">
                                    <div class="box box-widget widget-user">
                                        <!-- Add the bg color to the header using any of the bg-* classes -->
                                        <div class="widget-user-header bg-aqua-active">
                                            <h3 class="widget-user-username">
                                                <asp:Label ID="lblnombre" runat="server" Text=""></asp:Label></h3>
                                            <h5 class="widget-user-desc">
                                                <asp:Label ID="lblusuario" runat="server" Text=""></asp:Label></h5>
                                        </div>
                                        <div class="widget-user-image">
                                            <asp:Image ID="img_employee" runat="server" ImageUrl="~/img/user.png"
                                                CssClass="img-responsive img-circle" Style="max-height: 100px" />
                                        </div>
                                        <div class="box-footer">
                                            <div class="row">
                                                <div class="col-sm-3 border-right">
                                                    <div class="description-block">
                                                        <h5 class="description-header">Puesto</h5>
                                                        <span class="description-text">
                                                            <asp:Label ID="lblpuesto" runat="server" Text=""></asp:Label></span>
                                                    </div>
                                                    <!-- /.description-block -->
                                                </div>
                                                <!-- /.col -->
                                                <div class="col-sm-3">
                                                    <div class="description-block">
                                                        <h5 class="description-header">Perfil</h5>
                                                        <span class="description-text">
                                                            <asp:Label ID="lblperfil" runat="server" Text=""></asp:Label>
                                                            <br />
                                                            <asp:LinkButton CssClass="btn btn-danger btn-sm" OnClick="lnkaddperfil_Click"
                                                                ID="lnkaddperfil" runat="server">Cambiar perfil</asp:LinkButton>
                                                            </span>
                                                    </div>
                                                    <!-- /.description-block -->
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="description-block">
                                                        <h5 class="description-header">Menus</h5>
                                                        <span class="description-text">
                                                            Menus disponibles
                                                            <br />
                                                            <asp:LinkButton ID="lnkaddmenus" OnClick="lnkaddmenus_Click" CssClass="btn btn-danger  btn-sm"
                                                                runat="server">Ver menus
                                                            </asp:LinkButton>
                                                        </span>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="description-block">
                                                        <h5 class="description-header">Permisos</h5>
                                                        <span class="description-text">
                                                            Permisos asignados
                                                            <br />
                                                            <asp:LinkButton ID="lnkaddpermisos" OnClick="lnkaddpermisos_Click" CssClass="btn btn-danger  btn-sm"
                                                                runat="server">Ver permisos
                                                            </asp:LinkButton>
                                                        </span>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" id="div_permiso" runat="server" visible="false">
                                                <div class="col-lg-6 col-md-6 col-sm-12">
                                                    <h5><strong>Agregar permiso</strong></h5>
                                                    <div style="text-align: left;" class="input-group input-group-sm">
                                                        <asp:TextBox ID="txtbuscarpermiso" CssClass="form-control" placeholder="Buscar" runat="server"></asp:TextBox>
                                                        <span class="input-group-btn">
                                                            <asp:LinkButton ID="lnkbuscarpermiso" CssClass="btn btn-primary btn-flat" runat="server"
                                                                OnClientClick="return ChangedTextLoad4();" OnClick="lnkbuscarpermiso_Click">
                                                                        <i class="fa fa-search" aria-hidden="true"></i>
                                                            </asp:LinkButton>
                                                        </span>
                                                    </div>

                                                    <asp:Image ID="imgpermiso" Style="display: none;" ImageUrl="~/img/load.gif" runat="server" />
                                                    <label id="lblpermiso" runat="server" style="display: none; color: #1565c0">Buscando permisos</label>

                                                    <asp:DropDownList ID="ddlpermiso" CssClass="form-control" runat="server"></asp:DropDownList>
                                                    <br />
                                                    <div style="text-align: right">
                                                        <asp:LinkButton ID="lnkaddpermiso" CssClass="btn btn-primary btn-flat btn-sm"
                                                            OnClick="lnkaddpermiso_Click" OnClientClick="return confirm('¿Desea asignar este permiso al usuario?');"
                                                            runat="server">Agregar permiso&nbsp;<i class="fa fa-plus-circle" aria-hidden="true"></i></asp:LinkButton>
                                                    </div>
                                                </div>
                                                <div class="col-lg-6 col-md-6 col-sm-12">
                                                    <h5><strong>Permisos disponibles para este usuario</strong></h5>
                                                    <div style="width: 100%; height: 110px; overflow: scroll;">
                                                        <ul style="font-size: 10px">
                                                            <asp:Repeater ID="repeater_permisos" runat="server">
                                                                <ItemTemplate>
                                                                    <li>
                                                                        <%# Eval("permiso") %>
                                                                    </li>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </ul>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" id="div_menus" runat="server" visible="false">
                                                <div class="col-lg-6 col-md-6 col-sm-12">
                                                    <h5><strong>Agregar menu</strong></h5>
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

                                                    <asp:DropDownList ID="ddlmenus" CssClass="form-control" runat="server"></asp:DropDownList>
                                                    <br />
                                                    <div style="text-align: right">
                                                        <asp:LinkButton ID="lnkaddmenu" CssClass="btn btn-primary btn-flat btn-sm"
                                                            OnClick="lnkaddmenu_Click" OnClientClick="return confirm('¿Desea asignar el menu al usuario?');"
                                                            runat="server">Agregar menu&nbsp;<i class="fa fa-plus-circle" aria-hidden="true"></i></asp:LinkButton>
                                                    </div>
                                                </div>
                                                <div class="col-lg-6 col-md-6 col-sm-12">
                                                    <h5><strong>Menus disponibles para este usuario</strong></h5>
                                                    <div style="width: 100%; height: 110px; overflow: scroll;">
                                                        <ul style="font-size: 10px">
                                                            <asp:Repeater ID="repeater_menu" runat="server">
                                                                <ItemTemplate>
                                                                    <li>
                                                                        <%# Eval("nombre") %>
                                                                    </li>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </ul>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" id="div_addperfil" runat="server" visible="false">
                                                <div class="col-lg-12">
                                                    <div style="text-align: left;" class="input-group input-group-sm">
                                                        <asp:TextBox ID="txtbuscarperfil" CssClass="form-control" placeholder="Buscar" runat="server"></asp:TextBox>
                                                        <span class="input-group-btn">
                                                            <asp:LinkButton ID="LinkButton1" CssClass="btn btn-primary btn-flat" runat="server"
                                                                OnClientClick="return ChangedTextLoadperfil();" OnClick="lnkbuscarperfil_Click">
                                                                        <i class="fa fa-search" aria-hidden="true"></i>
                                                            </asp:LinkButton>
                                                        </span>
                                                    </div>

                                                    <asp:Image ID="imgperfil" Style="display: none;" ImageUrl="~/img/load.gif" runat="server" />
                                                    <label id="lblperfilb" runat="server" style="display: none; color: #1565c0">Buscando perfiles</label>

                                                    <asp:DropDownList ID="ddlperfiles" CssClass="form-control" runat="server"></asp:DropDownList>
                                                     <br />
                                                    <div style="text-align: right">
                                                        <asp:LinkButton ID="lnksaveperfil" CssClass="btn btn-primary btn-flat btn-sm"
                                                            OnClick="lnkasaveperfil_Click" OnClientClick="return confirm('¿Desea asignar el perfil al usuario?');"
                                                            runat="server">Guardar perfil&nbsp;<i class="fa fa-save" aria-hidden="true"></i></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                            <!-- /.row -->
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>
                        <div class="modal-footer ">
                            <button type="button" class="btn btn-default pull-left" data-dismiss="modal">Cerrar</button>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div class="modal fade bs-example-modal-lg" tabindex="-1" id="modal_delegados" role="dialog" aria-labelledby="mySmallModalLabel" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg" role="document">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btndelegados" EventName="Click" />
                </Triggers>
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">×</span></button>
                            <h4 class="modal-title">Usuarios delegados</h4>
                        </div>
                        <div class="modal-body">

                            <div class="row">
                                <div class="col-lg-12 col-xs-12">
                                    <h5><strong>Listado de usuarios que pueden ser visualizados por el seleccionado.</strong></h5>
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
                                    <div style="height: 330px; min-width: 500px; overflow: scroll;">
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
                        <div class="modal-footer ">
                            <button type="button" class="btn btn-default pull-left" data-dismiss="modal">Cerrar</button>
                            <asp:LinkButton OnClientClick="return false;" ID="lnkcargando" CssClass="btn btn-primary btn-flat" runat="server" Style="display: none;">
                                            <i class="fa fa-refresh fa-spin fa-fw"></i>
                                            <span class="sr-only">Loading...</span>&nbsp;Guardando...
                            </asp:LinkButton>
                            <asp:LinkButton ID="lnkguardar" CssClass="btn btn-primary btn-flat" OnClick="lnkguardar_Click"
                                OnClientClick="return ConfirmEmpleadoProyectoModal('¿Desea Guardar esta información?');" runat="server">
                                            <i class="fa fa-floppy-o" aria-hidden="true"></i>&nbsp;Guardar
                            </asp:LinkButton>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    
    <div class="modal fade bs-example-modal-lg" tabindex="-1" id="modal_usuarrios" role="dialog" aria-labelledby="mySmallModalLabel" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg" role="document">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="lnknuevousuario" EventName="Click" />
                </Triggers>
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">×</span></button>
                            <h4 class="modal-title">Usuario</h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-lg-6 col-md-6 col-sm-12">
                                    <h6><strong><i class="fa fa-user" aria-hidden="true"></i>&nbsp;Usuario</strong>
                                    </h6>
                                    <asp:TextBox ID="txtusuario" MaxLength="250"  CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                                
                                
                                <div class="col-lg-6 col-md-6 col-sm-12">
                                    <h6><strong><i class="fa fa-edge" aria-hidden="true"></i>&nbsp;Correo electronico</strong>
                                    </h6>
                                    <asp:TextBox ID="txtcorreo" TextMode="Email" MaxLength="250" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-lg-6 col-md-6 col-sm-12">
                                    <h6><strong><i class="fa fa-key" aria-hidden="true"></i>&nbsp;Contraseña</strong>
                                    </h6>
                                    <asp:TextBox ID="txtcontra" MaxLength="250" TextMode="Password"  CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-lg-6 col-md-6 col-sm-12">
                                    <h6><strong><i class="fa fa-key" aria-hidden="true"></i>&nbsp;Confirmar contraseña</strong>
                                    </h6>
                                    <asp:TextBox ID="txtconfirmacontra" TextMode="Password"  MaxLength="250"  CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12">
                                    <h6><strong><i class="fa fa-id-card-o" aria-hidden="true"></i>&nbsp;Nombres</strong>
                                    </h6>
                                    <asp:TextBox ID="txtnombres" MaxLength="250"  CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-lg-6 col-md-6 col-sm-12">
                                    <h6><strong><i class="fa fa-id-card-o" aria-hidden="true"></i>&nbsp;Ap. Paterno</strong>
                                    </h6>
                                    <asp:TextBox ID="txtapaterno" MaxLength="250"  CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-lg-6 col-md-6 col-sm-12">
                                    <h6><strong><i class="fa fa-id-card-o" aria-hidden="true"></i>&nbsp;Ap. Materno</strong>
                                    </h6>
                                    <asp:TextBox ID="txtamaterno" MaxLength="250"  CssClass="form-control" runat="server"></asp:TextBox>
                                </div>

                            </div>
                            <div class="row">
                                
                                <div class="col-lg-6 col-md-6 col-sm-12">
                                    <h6><strong><i class="fa fa-briefcase" aria-hidden="true"></i>&nbsp;Puesto</strong>
                                    </h6>
                                    <asp:TextBox ID="txtpuesto" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-lg-6 col-md-6 col-sm-12">
                                    <h6><strong><i class="fa fa-users" aria-hidden="true"></i>&nbsp;Empleado</strong>
                                        &nbsp; 
                                        <asp:CheckBox ID="cbxnoactivo" Text="Ver no Activos" Checked="true" runat="server" />
                                    </h6>
                                    <div class="input-group input-group-sm">
                                        <asp:TextBox
                                            onfocus="this.select();" ID="txtfilterempleado" CssClass=" form-control"
                                            placeholder="Ingrese un filtro" runat="server"></asp:TextBox>
                                        <span class="input-group-btn">
                                            <asp:LinkButton ID="lnksearch" CssClass="btn btn-primary btn-flat"
                                                OnClientClick="return ChangedTextLoadadd();" OnClick="lnksearch_Click" runat="server">
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
                        </div>
                        <div class="modal-footer ">
                            <button type="button" class="btn btn-default pull-left" data-dismiss="modal">Cerrar</button>
                            <asp:LinkButton OnClientClick="return false;" ID="LinkButton3" CssClass="btn btn-primary btn-flat" runat="server" Style="display: none;">
                                            <i class="fa fa-refresh fa-spin fa-fw"></i>
                                            <span class="sr-only">Loading...</span>&nbsp;Guardando...
                            </asp:LinkButton>
                            <asp:LinkButton ID="lnkguardarempleado" CssClass="btn btn-primary btn-flat" OnClick="lnkguardarempleado_Click"
                                OnClientClick="return ConfirmEmpleadoProyectoModal('¿Desea Guardar esta información?');" runat="server">
                                            <i class="fa fa-floppy-o" aria-hidden="true"></i>&nbsp;Guardar
                            </asp:LinkButton>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <asp:Button ID="btnver" runat="server" Text="" style="display:none" OnClick="btnver_Click" />
    <asp:Button ID="btndelegados" runat="server" Text="" style="display:none" OnClick="btndelegados_Click" />
    <asp:HiddenField ID="hdfusuario" runat="server" />
    <asp:HiddenField ID="hdfnum_empleado" runat="server" />
</asp:Content>
