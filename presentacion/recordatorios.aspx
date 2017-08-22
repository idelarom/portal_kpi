<%@ Page Title="" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="recordatorios.aspx.cs" Inherits="presentacion.recordatorios" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="dist/js/loading.js"></script>
    <!-- fullCalendar 2.2.5 -->
    <link href="plugins/fullcalendar/fullcalendar.css" rel="stylesheet" />
    <script src="plugins/fullcalendar/moment.min.js"></script>
    <script src="plugins/fullcalendar/fullcalendar.js"></script>
    <script src="plugins/fullcalendar/locale-all.js"></script>
    <script src="dist/js/pages/recordatorios.js"></script>
    <style type="text/css">
        .day-style {
            background-color: #ef5350;
        }

        .products-list .product-info {
            margin-left: 6px;
        }
        a:link, span.MsoHyperlink {
            color: white;
            text-decoration: none;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row" id="div_body">
        <div class="col-lg-12">
            <h4 class="page-header">Mis Recordatorios y Reuniones</h4>
        </div>
        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnedit" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btncalendar" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="lnkaddrecordatorio" EventName="Click" />
                </Triggers>
                <ContentTemplate>
                    <div class="box box-danger">
                        <div class="box-header with-border">

                            <h3 class="box-title">Visualizando el dia
                                <asp:Label ID="lblfechaselected" runat="server" Text="Label"></asp:Label></h3>
                        </div>
                        <div class="box-body">
                            <asp:LinkButton ID="lnkaddrecordatorio" OnClick="lnkaddrecordatorio_Click" runat="server" CssClass="btn btn-danger btn-flat">
                                <i class="fa fa-plus-circle" aria-hidden="true"></i>&nbsp;Nuevo recordatorio
                            </asp:LinkButton>
                            <asp:LinkButton ID="lnkaddapointment" Visible="false" OnClick="lnkaddapointment_Click" runat="server" CssClass="btn btn-primary btn-flat">
                                <i class="fa fa-plus-circle" aria-hidden="true"></i>&nbsp;Nuevo meeting
                            </asp:LinkButton>
                            <ul class="products-list product-list-in-box" style="font-size: 12px;">
                                <asp:Repeater ID="repeat_list_rec" runat="server">
                                    <ItemTemplate>
                                        <li class="item">
                                            <div class="product-info">
                                                <div class="product-title">
                                                    <a style="font-size: 15px; cursor: pointer;" onclick='<%# "return EditRecordatorios("+Eval("id_recordatorio")+");" %>'><%# Eval("titulo") %></a>
                                                    <span class="pull-right" id="s" runat="server" visible='<%# !Convert.ToBoolean(Eval("appointment")) %>'>
                                                        <asp:LinkButton CssClass="btn btn-danger btn-sm" ID="lnkeliminar" Visible="true" runat="server" CommandName="Eliminar"
                                                            OnClientClick="return ConfirmEntregableDelete('¿Desea Eliminar este Recordatorio?')" OnClick="lnkeliminar_Click"
                                                            CommandArgument='<%# DataBinder.Eval(Container.DataItem, "id_recordatorio").ToString() %>'>
                                                                <i class="fa fa-trash" aria-hidden="true"></i>
                                                        </asp:LinkButton>
                                                    </span>
                                                    <span class=" pull-right" id="s2" runat="server" visible='<%# Convert.ToBoolean(Eval("appointment")) %>'>
                                                        <div class="dropdown">
                                                            <button class="btn btn-link dropdown-toggle" style="cursor: pointer;" type="button" id="dropdownMenu1"
                                                                data-toggle="dropdown" aria-haspopup="true" aria-expanded="true">
                                                                <i class="fa fa-th"></i>
                                                            </button>
                                                            <ul class="dropdown-menu  dropdown-menu-right" aria-labelledby="dropdownMenu1">
                                                                <li style="cursor: pointer;" id="li_cs" runat="server" visible='<%# Convert.ToString(Session["mail"]).ToLower() == Eval("organizer_address").ToString().ToLower() %>'>
                                                                    <asp:LinkButton ID="LinkButton1" Visible="true" runat="server" CommandName="Eliminar"
                                                                        OnClientClick="return ConfirmEntregableDelete('¿Desea cancelar este meeting?')" OnClick="lnkeliminar_Click"
                                                                        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "id_recordatorio").ToString() %>'>
                                                                         <i class="fa fa-trash" aria-hidden="true"></i>&nbsp;Cancelar
                                                                    </asp:LinkButton>
                                                                </li>
                                                                <li style="cursor: pointer;" id="li1" runat="server">
                                                                    <asp:LinkButton ID="lnkcommandevent" Visible="true" runat="server" CommandName="Aceptar"
                                                                        OnClientClick="return sconfirm('¿Desea aceptar este meeting?')" OnClick="lnkcommandevent_Click"
                                                                        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "id_recordatorio").ToString() %>'>
                                                                         <i class="fa fa-check" aria-hidden="true"></i>&nbsp;Aceptar
                                                                    </asp:LinkButton>
                                                                </li>
                                                                <li style="cursor: pointer;" id="li2" runat="server">
                                                                    <asp:LinkButton ID="LinkButton2" Visible="true" runat="server" CommandName="Rechazar"
                                                                        OnClientClick="return ConfirmEntregableDelete('¿Desea rechazar este meeting?')" OnClick="lnkcommandevent_Click"
                                                                        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "id_recordatorio").ToString() %>'>
                                                                         <i class="fa fa-times" aria-hidden="true"></i>&nbsp;Rechazar
                                                                    </asp:LinkButton>
                                                                </li>
                                                            </ul>
                                                        </div>
                                                    </span>
                                                </div>
                                                <span class="product-description">
                                                    <%# Convert.ToDateTime(Eval("fecha")).ToString("hh:mm tt", System.Globalization.CultureInfo.InvariantCulture) +" - "+
                                                            (Convert.ToBoolean(Eval("appointment"))?Eval("location").ToString()+" - "+ Eval("organizer").ToString():Eval("descripcion_corta").ToString())  %> 
                                                </span>
                                            </div>
                                        </li>

                                    </ItemTemplate>
                                </asp:Repeater>


                            </ul>
                        </div>

                        <div class="overlay" id="load_items" runat="server" style="display: none;">
                            <i class="fa fa-refresh fa-spin"></i>
                        </div>
                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
            <div class="box box-danger">
                <div class="box-body no-padding">
                    <div id="calendar"></div>
                </div>
                <div class="overlay" id="load_calendar" runat="server" style="display: none;">
                    <i class="fa fa-refresh fa-spin"></i>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade bs-example-modal-lg" tabindex="-1" id="myModal" role="dialog" aria-labelledby="mySmallModalLabel" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg" role="document">

            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnedit" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btncalendar" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="lnkaddrecordatorio" EventName="Click" />
                    <asp:PostBackTrigger ControlID="lnkguardar"  />
                </Triggers>
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">×</span></button>
                            <h4 class="modal-title">Recordatorios y Reuniones</h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-lg-6 col-xs-6">
                                    <h5><strong><i class="fa fa-calendar" aria-hidden="true"></i>&nbsp;Fecha y hora</strong></h5>

                                    <telerik:RadDateTimePicker ID="txtfecharec" runat="server" Width="100%" Skin="Bootstrap"></telerik:RadDateTimePicker>
                                    <%--<asp:TextBox ID="txtfecharec" TextMode="DateTimeLocal" CssClass="form-control" runat="server"></asp:TextBox>--%>
                                </div>
                                <div class="col-lg-6 col-xs-6"  id="div_fecha_fin" runat="server" visible="false">
                                    <h5><strong><i class="fa fa-calendar" aria-hidden="true"></i>&nbsp;Fecha y hora fin</strong></h5>
                                    <telerik:RadDateTimePicker ID="txtfechafin" runat="server" Width="100%" Skin="Bootstrap"></telerik:RadDateTimePicker>
                                    <%--<asp:TextBox ID="txtfechafin" TextMode="DateTimeLocal" CssClass="form-control" runat="server"></asp:TextBox>--%>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-lg-12 col-sm-12">
                                    <h5><strong><i class="fa fa-outdent" aria-hidden="true"></i>&nbsp;Titulo</strong></h5>
                                    <telerik:RadTextBox ID="rtxttitulo" Width="100%" runat="server" Skin="Bootstrap"></telerik:RadTextBox>
                                </div>
                            </div>
                            <div class="row" id="div_organizador" runat="server" visible="false">
                                <div class="col-lg-12 col-sm-12">
                                    <h5><strong><i class="fa fa-map" aria-hidden="true"></i>&nbsp;Lugar</strong></h5>
                                    <telerik:RadTextBox ID="rtxtlugar" ReadOnly="true" Width="100%" runat="server" Skin="Bootstrap"></telerik:RadTextBox>
                                </div>
                                <div class="col-lg-6  col-sm-6  col-xs-12 ">
                                    <h5><strong><i class="fa fa-user" aria-hidden="true"></i>&nbsp;Organizador</strong></h5>
                                    <telerik:RadTextBox ID="rtxtorganizador" ReadOnly="true" Width="100%" runat="server" Skin="Bootstrap"></telerik:RadTextBox>
                                </div>
                                <div class="col-lg-6 col-sm-6  col-xs-12">
                                    <h5><strong><i class="fa fa-user" aria-hidden="true"></i>&nbsp;Correo del organizador</strong></h5>
                                    <telerik:RadTextBox ID="rtxtcorreorganizador" ReadOnly="true" Width="100%" runat="server" Skin="Bootstrap"></telerik:RadTextBox>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-lg-12 col-sm-12">
                                    <h5><strong><i class="fa fa-commenting" aria-hidden="true"></i>&nbsp;Descripción</strong></h5>
                                    <telerik:RadTextBox  style="font-size:12px;" ID="rtxtdescripcion" TextMode="MultiLine" Rows="4" Width="100%" runat="server" Skin="Bootstrap"></telerik:RadTextBox>
                                   
                                </div>
                            </div>

                            <asp:TextBox ID="txtid_recordatorio" Visible="false" runat="server"></asp:TextBox>
                        </div>
                        <div class="modal-footer ">
                            <button type="button" class="btn btn-default pull-left" data-dismiss="modal">Cerrar</button>
                            <asp:LinkButton OnClientClick="return false;" ID="lnkcargando" CssClass="btn btn-primary btn-flat" runat="server" Style="display: none;">
                                            <i class="fa fa-refresh fa-spin fa-fw"></i>
                                            <span class="sr-only">Loading...</span>&nbsp;Guardando...
                            </asp:LinkButton>
                            <asp:LinkButton ID="lnkguardar" CssClass="btn btn-primary btn-flat" OnClick="lnkguardar_Click"
                                OnClientClick="return ConfirmwidgetProyectoModal('¿Desea Guardar este recordatorio?');" runat="server">
                                            <i class="fa fa-floppy-o" aria-hidden="true"></i>&nbsp;Guardar
                            </asp:LinkButton>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    
    <asp:HiddenField ID="hdffecha" runat="server" />
    <asp:TextBox ID="txtfecha" TextMode="Date"  style="display:none;"  runat="server"></asp:TextBox>
    <asp:Button ID="btnedit" style="display:none;" OnClick="btnedit_Click" runat="server" Text="Button" />
    <asp:Button ID="btncalendar" style="display:none;" OnClick="btncalendar_Click" runat="server" Text="Button" />
    <asp:HiddenField ID="hdfmotivos" runat="server" />
    <asp:HiddenField ID="hdfid_rec" runat="server" />
</asp:Content>
