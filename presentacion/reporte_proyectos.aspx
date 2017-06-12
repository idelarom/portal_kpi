<%@ Page Title="Reporte Proyectos" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="reporte_proyectos.aspx.cs" Inherits="presentacion.reporte_proyectos" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
           
        });
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

        .rcbList li {
            font-size: 10px;
        }

        .rcbCheckAllItems label {
            font-size: 11px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <h4 class="page-header">Reporte General de Proyectos</h4>
        </div>
    </div>

    <div class="row">
        <asp:UpdatePanel ID="UODATA" runat="server" UpdateMode="Always">
            <ContentTemplate>

                <div class="col-lg-12 col-sm-12">
                    <div class="box box-danger">
                        <div class="box-header with-border">
                            <h3 class="box-title">Proyectos</h3>
                            <div class="box-tools pull-right">
                                <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                    <i class="fa fa-minus"></i>
                                </button>
                            </div>
                        </div>
                        <div class="box-body">
                            <div class="row" id="div_filtros_reporte" runat="server" visible="true">
                                <div class="col-lg-2 col-md-6 col-sm-6 col-xs-12">
                                    <h5><strong><i class="fa fa-calendar" aria-hidden="true"></i>&nbsp;Fecha Inicio</strong></h5>
                                    <asp:TextBox ID="rfinicio" runat="server" CssClass="form-control" type="date"></asp:TextBox>
                                    <%--<telerik:RadTextBox ID="rfinicio" Width="100%" Skin="Bootstrap" runat="server" InputType="Date"></telerik:RadTextBox>--%>
                                </div>
                                <div class="col-lg-2 col-md-6 col-sm-6 col-xs-12">
                                    <h5><strong><i class="fa fa-calendar" aria-hidden="true"></i>&nbsp;Fecha Fin</strong></h5>
                                    <asp:TextBox ID="rfin" runat="server" CssClass="form-control" type="date"></asp:TextBox>
                                   <%-- <telerik:RadTextBox ID="rfin" Width="100%" Skin="Bootstrap" runat="server" InputType="Date"></telerik:RadTextBox>--%>
                                </div>
                                <div class="col-lg-4 col-md-6 col-sm-6 col-xs-12">
                                    <h5><strong><i class="fa fa-address-card-o" aria-hidden="true"></i>&nbsp;Clientes</strong></h5>
                                    
                                    <telerik:RadComboBox RenderMode="Lightweight" ID="ddlcliente_x_proyecto" runat="server" CheckBoxes="true"
                                        EnableCheckAllItemsCheckBox="true" Style="font-size: 11px;" EmptyMessage="Filtrar por Cliente"
                                        Width="100%" Skin="Bootstrap" Localization-AllItemsCheckedString="Todos los Clientes seleccionados"
                                        Localization-NoMatches="No hay resultados" Localization-CheckAllString="Seleccionar todos" Localization-ItemsCheckedString="Cliente(s) selecionado(s)">
                                    </telerik:RadComboBox>
                                </div>
                                <div class="col-lg-4 col-md-6 col-sm-6 col-xs-12" id="div_combo_pm_x_proyecto" runat="server" visible="true">                                    
                                    <h5><strong><i class="fa fa-users" aria-hidden="true"></i>&nbsp;PM</strong></h5>
                                    <telerik:RadComboBox RenderMode="Lightweight" ID="ddlpm_x_proyecto" runat="server" CheckBoxes="true"
                                        EnableCheckAllItemsCheckBox="true" Style="font-size: 11px;" EmptyMessage="Filtrar por PM"
                                        Width="100%" Skin="Bootstrap" Localization-AllItemsCheckedString="Todos los PM seleccionados"
                                        Localization-NoMatches="No hay resultados" Localization-CheckAllString="Seleccionar todos" Localization-ItemsCheckedString="PM selecionado(s)">
                                    </telerik:RadComboBox>                                
                                </div>
                                <div style="text-align:right;" class="col-lg-12 col-xs-12">
                                    <br />
                                    <asp:LinkButton ID="lnkgenerar_reporte" CssClass="btn btn-primary btn-flat" runat="server" OnClick="lnkfiltro_Click">
                                        Generar Reporte&nbsp;<i class="fa fa-hourglass" aria-hidden="true"></i>
                                    </asp:LinkButton>
                                </div>
                            </div>
                            <div class="table-responsive">
                                <table class="table no-margin">
                                    <thead>
                                        <tr>
                                            <th>Proyecto</th>
                                            <th style="text-align: left; width: 200px;">Inicio</th>
                                            <th style="text-align: left; width: 200px;">Fin</th>
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
                                                    <td style="text-align: left;"><%# Eval("fecha_inicio_str") %></td>
                                                    <td style="text-align: left;"><%# Eval("fecha_fin_str") %></td>
                                                    <td style="text-align: center;"><%# Eval("Planeación") %></td>
                                                    <td style="text-align: center;"><%# Eval("Ejecución") %></td>
                                                    <td style="text-align: center;"><%# Eval("Cierre") %></td>
                                                    <td id="td_pm" runat="server" visible='<%# Convert.ToBoolean(Eval("view_pm_filters"))%>'
                                                        style="text-align: left; font-size: 10px;"><%# Eval("PM") %></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                        <div class="box-footer clearfix">
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
