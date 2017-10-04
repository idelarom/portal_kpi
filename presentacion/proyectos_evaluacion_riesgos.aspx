<%@ Page Title="Evaluaciones" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="proyectos_evaluacion_riesgos.aspx.cs" Inherits="presentacion.proyectos_evaluacion_riesgos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .nav-tabs-custom > .tab-content {
            background: #fff;
            padding: 10px;
            border-bottom-right-radius: 3px;
            border-bottom-left-radius: 3px;
        }
    </style>
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
                                <div class="row">
                                    <div class="col-lg-12">
                                        <h5><strong><i class="fa fa-calendar-o" aria-hidden="true"></i>&nbsp;
                                            Detalles de evaluación del dia <%# Eval("fecha_evaluacion_str") %></strong></h5>
                                    </div>
                                    <div class="col-lg-12" id="div_no_hay_riesgos" runat="server">
                                        <p class=" text-red">Esta evaluación no contiene riesgos.</p>
                                    </div>
                                    <div class="col-lg-12">
                                       
                                                <asp:LinkButton ID="lnknuevoriesgo" CssClass="btn btn-primary btn-flat" CommandName="nuevo_riesgo"
                                                    runat="server">Nuevo riesgo&nbsp;<i class="fa fa-plus" aria-hidden="true"></i></asp:LinkButton>

                                                <asp:LinkButton ID="lnkimportarriesgos" CssClass="btn btn-success btn-flat" CommandName="importar_riesgos"
                                                    runat="server">Importar riesgos&nbsp;<i class="fa fa-plus" aria-hidden="true"></i></asp:LinkButton>

                                          

                                    </div>
                                </div>
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
                                    <h5><strong><i class="fa fa-bars" aria-hidden="true"></i>&nbsp;Estatus</strong></h5>
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
                                            <asp:TextBox 
                                                OnTextChanged="txtpprobabilidad_TextChanged" AutoPostBack="true"
                                                 ID="txtpprobabilidad" MaxLength="250" CssClass=" form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                    <h5><strong><i class="fa fa-bars" aria-hidden="true"></i>&nbsp;Impacto costo</strong></h5>
                                    <div class="row">
                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                                            <asp:DropDownList ID="ddlimpacto_costo" CssClass="form-control" runat="server"></asp:DropDownList>
                                        </div>
                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                                            <asp:TextBox ID="txtimpacto_costo" MaxLength="250" CssClass=" form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                    <h5><strong><i class="fa fa-bars" aria-hidden="true"></i>&nbsp;Impacto tiempo</strong></h5>
                                    <div class="row">
                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                                            <asp:DropDownList ID="ddlimpacto_tiempo" CssClass="form-control" runat="server"></asp:DropDownList>
                                        </div>
                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                                            <asp:TextBox ID="txtimpacto_tiempo" MaxLength="250" CssClass=" form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                    <h5><strong><i class="fa fa-bars" aria-hidden="true"></i>&nbsp;% Riesgo costo</strong></h5> 
                                   
                                            <asp:TextBox ID="txtriesgo_costo" MaxLength="250" CssClass=" form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                    <h5><strong><i class="fa fa-bars" aria-hidden="true"></i>&nbsp;% Riesgo tiempo</strong></h5>
                                    <asp:TextBox ID="txtriesgo_tiempo" MaxLength="250" CssClass=" form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                    <h5><strong><i class="fa fa-bars" aria-hidden="true"></i>&nbsp;Estrategia</strong></h5>
                                    <asp:DropDownList ID="ddlestrategias" CssClass="form-control" runat="server"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer ">
                            <button type="button" class="btn btn-default pull-left" data-dismiss="modal">Cerrar</button>
                            <asp:LinkButton OnClientClick="return false;" ID="lnkcargando" CssClass="btn btn-primary btn-flat" runat="server" Style="display: none;">
                                            <i class="fa fa-refresh fa-spin fa-fw"></i>
                                            <span class="sr-only">Loading...</span>&nbsp;Guardando...
                            </asp:LinkButton>
                            <asp:LinkButton ID="lnkguardar" CssClass="btn btn-primary btn-flat" runat="server">
                                            <i class="fa fa-floppy-o" aria-hidden="true"></i>&nbsp;Guardar
                            </asp:LinkButton>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
