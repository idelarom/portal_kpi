<%@ Page Title="Inicio" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="inicio.aspx.cs" Inherits="presentacion.inicio" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- fullCalendar 2.2.5 -->
    <link href="plugins/fullcalendar/fullcalendar.css" rel="stylesheet" />
    <script src="plugins/fullcalendar/moment.min.js"></script>
    <script src="plugins/fullcalendar/fullcalendar.js"></script>
    <script src="plugins/fullcalendar/locale-all.js"></script>
    <script src="dist/js/pages/inicio.js"></script>
    <script type="text/javascript">
      
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
       .products-list .bono-info {
            margin-left: 6px;
        }
       .box-body {
            border-top-left-radius: 0;
            border-top-right-radius: 0;
            border-bottom-right-radius: 3px;
            border-bottom-left-radius: 3px;
            padding: 10px;
            min-height:270px;
        }
       .fc-toolbar.fc-header-toolbar {
            margin-bottom: 1em;
            font-size: 11px;
        }.fc-view-container *, .fc-view-container *:before, .fc-view-container *:after {
            -webkit-box-sizing: content-box;
            -moz-box-sizing: content-box;
            box-sizing: content-box;
            font-size: 10px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <h4 class="page-header">Inicio</h4>
        </div>
    </div>
    <!-- /Widgets individuales -->
    <div class="row">
        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12" id="dashboard_kpi_ind" style="display: none;">
            <div class="info-box bg-red">
                <span class="info-box-icon"><i class="fa fa-bookmark-o"></i></span>

                <div class="info-box-content">
                    <span class="info-box-text">Bono Trimestral</span>
                    <span class="info-box-number">
                        <label id="bono_trimestral">$ 0.00</label></span>

                    <div class="progress">
                        <div id="progress_bar_bono_kpi_ind" class="progress-bar" style="width: 0%"></div>
                    </div>
                    <span id="progress_bono_kpi_ind" class="progress-description">0 % porcentaje alcanzado.
                  </span>
                </div>
            </div>
            
            <!-- /.info-box -->
        </div>

         <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12" id="Performance_ing_ind" style="display: none;">
            <div class="info-box bg-blue">
                <span class="info-box-icon"><i class="fa fa-user"></i></span>

                <div class="info-box-content">
                    <span class="info-box-text">Horas Ingeniero Mensual</span>
                    <span class="info-box-number">
                        <label id="horas_Semanal">0.00 hrs</label></span>

                    <div class="progress">
                        <div id="progress_bar_performance_ing_ind" class="progress-bar" style="width: 0%"></div>
                    </div>
                    <span id="progress_performance_ing_ind"" class="progress-description">0 % porcentaje alcanzado.
                  </span>
                </div>
            </div>
            
            <!-- /.info-box -->
        </div>

        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12" id="dashboard_compromisos_ind" style="display: none;">
            <div class="info-box bg-yellow">
                <span class="info-box-icon"><i class="fa fa-calendar-check-o"></i></span>

                <div class="info-box-content">
                    <span class="info-box-text">Compromisos Trimestral</span>
                    <span class="info-box-number">
                        <label id="compromisos_trimestral">0</label></span>

                    <div class="progress">
                        <div id="progress_bar_Compromisos_kpi_ind" class="progress-bar" style="width: 0%"></div>
                    </div>
                    <span id="progress_Compromisos_kpi_ind"" class="progress-description">0 % porcentaje alcanzado.
                  </span>
                </div>
            </div>
            
            <!-- /.info-box -->
        </div>
    </div>
    
    <!-- /Widgets Grupales -->
    <div class="row">
        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12" id="calendario" style="display: none;">
            <div class="box box-danger">
                <div class="box-header with-border">
                    <h3 class="box-title">Calendario</h3>
                    <div class="box-tools pull-right">
                        <a id="btnayuda_calendario">
                            <i class="material-icons">info</i>
                        </a>
                    </div>
                </div>
                <div class="box-body no-padding" style="height: 325px">
                    <div id="calendar"></div>
                </div>
            </div>
        </div>
        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12" id="desglo_dashboard_kpi_ind" style="display: none;">
            <div class="box box-danger">
                <div class="box-header with-border">
                    <h3 class="box-title">Valor ganado bono trimestral</h3>
                    <div class="box-tools pull-right">
                        <a id="btnayuda_desglo_dashboard_kpi_ind">
                            <i class="material-icons">info</i>
                        </a>
                    </div>
                </div>
                <div class="box-body">
                    <ul class="products-list product-list-in-box">
                        <li class="item">
                            <div class="bono-info">
                                <a class="product-title" href="javascript:void(0)">Preventa                          
                                <span class="label label-primary pull-right" style="width: 55px" id="dashboard_bonos_preventa">0 %</span>
                                    <span class="pull-right">&nbsp;</span>
                                    <span class="label label-primary pull-right" id="dashboard_bonos_totalpreventa" style="width: 70px">$ 0.00</span>
                                </a>

                            </div>
                        </li>
                        <li class="item">
                            <div class="bono-info">
                                <a class="product-title" href="javascript:void(0)">Implementación                    
                                <span class="label label-primary pull-right" style="width: 55px" id="dashboard_bonos_imp">0 %</span>
                                    <span class="pull-right">&nbsp;</span>
                                    <span class="label label-primary pull-right" id="dashboard_bonos_totalimp" style="width: 70px">$ 0.00</span>
                                </a>
                            </div>
                        </li>
                        <li class="item">
                            <div class="bono-info">
                                <a class="product-title" href="javascript:void(0)">Soporte                    
                                <span class="label label-primary pull-right" style="width: 55px" id="dashboard_bonos_soporte">0 %</span>
                                    <span class="pull-right">&nbsp;</span>
                                    <span class="label label-primary pull-right" id="dashboard_bonos_totalsoporte" style="width: 70px">$ 0.00</span>
                                </a>

                            </div>
                        </li>
                        <li class="item">
                            <div class="bono-info">
                                <a class="product-title" href="javascript:void(0)">Cump. Compromisos                         
                                <span class="label label-primary pull-right" style="width: 55px" id="dashboard_bonos_compromisos">0 %</span>
                                </a>
                            </div>
                        </li>
                        <li class="item">
                            <div class="bono-info">
                                <a class="product-title" href="javascript:void(0)">KPI Individual                    
                                <span class="label label-primary pull-right" style="width: 55px" id="dashboard_bonos_kpi">0 %</span>
                                </a>
                            </div>
                        </li>
                        <li class="item" style="display: none;">
                            <div class="bono-info">
                                <a class="product-title" href="javascript:void(0)">KPI Grupal                    
                                <span class="label label-primary pull-right" style="width: 55px" id="dashboard_bonos_kpig">0 %</span>
                                </a>
                            </div>
                        </li>
                    </ul>
                </div>

                <div class="box-footer clearfix">
                    <a class="btn btn-sm btn-danger btn-flat pull-right" id="link_desgdashboard_kpi"
                         onclick="CloseAjax('reporte_dashboard_bonos_kpi.aspx?filter=1');">Ver Reporte
                    </a>
                </div>
            </div>
        </div>

        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12" id="desglo_performance_ing_ind" style="display: none;">
            <div class="box box-danger">
                <div class="box-header with-border">
                    <h3 class="box-title">Ocupación tiempo ingeniero</h3>
                    
                    <div class="box-tools pull-right">
                        <a id="btnayuda_desglo_performance_ing_ind">
                            <i class="material-icons">info</i>
                        </a>
                    </div>
                </div>
                <div class="box-body">
                    <ul class="products-list product-list-in-box">
                        <li class="item">
                            <div class="bono-info">
                                <a class="product-title" href="javascript:void(0)">Preventa                          
                                <span class="label label-primary pull-right" style="width: 55px" id="performance_ing_preventa">0 %</span>
                                    <span class="pull-right">&nbsp;</span>
                                    <span class="label label-primary pull-right" id="performance_ing_totalpreventa" style="width: 70px">0.00  hrs</span>
                                </a>

                            </div>
                        </li>
                        <li class="item">
                            <div class="bono-info">
                                <a class="product-title" href="javascript:void(0)">Implementacion           
                                <span class="label label-primary pull-right" style="width: 55px" id="performance_ingenieria_imp">0 %</span>
                                    <span class="pull-right">&nbsp;</span>
                                    <span class="label label-primary pull-right" id="performance_ingenieria_totalimp" style="width: 70px">0.00 hrs</span>
                                </a>
                            </div>
                        </li>
                        <li class="item">
                            <div class="bono-info">
                                <a class="product-title" href="javascript:void(0)">Soporte                    
                                <span class="label label-primary pull-right" style="width: 55px" id="performance_ingenieria_sop">0 %</span>
                                    <span class="pull-right">&nbsp;</span>
                                    <span class="label label-primary pull-right" id="performance_ingenieria_totalsop" style="width: 70px">0.00 hrs</span>
                                </a>
                            </div>
                        </li>
                        <li class="item">
                            <div class="bono-info">
                                <a class="product-title" href="javascript:void(0)">Adminitrativas                    
                                <span class="label label-primary pull-right" style="width: 55px" id="performance_ingenieria_admin">0 %</span>
                                    <span class="pull-right">&nbsp;</span>
                                    <span class="label label-primary pull-right" id="performance_ingenieria_totaladmin" style="width: 70px">0.00 hrs</span>
                                </a>
                            </div>
                        </li>
                         <li class="item">
                            <div class="bono-info">
                                <a class="product-title" href="javascript:void(0)">Total horas                    
                                <span class="label label-primary pull-right" style="width: 55px" id="performance_ingenieria_th">0 %</span>
                                    <span class="pull-right">&nbsp;</span>
                                    <span class="label label-primary pull-right" id="performance_ingenieria_totaladhr" style="width: 70px">0.00 hrs</span>
                                </a>
                            </div>
                        </li>

                    </ul>
                </div>

                <div class="box-footer clearfix">
                    <a class="btn btn-sm btn-danger btn-flat pull-right" id="link_performance_ingenieria" 
                        onclick="CloseAjax('reporte_performance_ingenieria_netdiario.aspx?filter=1');">Ver Reporte
                    </a>
                </div>
            </div>
        </div>
        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12" id="performance_ing" style="display: none;">
            <div class="box box-danger">
                <div class="box-header with-border">
                    <h3 class="box-title">Performance Ingenieria</h3>
                    <div class="box-tools pull-right">
                        <a id="btnayuda_performance_ing">
                            <i class="material-icons">info</i>
                        </a>
                    </div>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="table table-responsive" style="width: 100%; max-height:250px; overflow: scroll;">
                        <table id="table_performance_ing" class="dvv table table-responsive table-condensed">
                            <thead>
                                <tr style="font-size: 11px;">
                                    <th style="min-width: 210px; text-align: left;" scope="col">Empleado</th>
                                    <th style="min-width: 55px; text-align: center;" scope="col">Soporte</th>
                                    <th style="min-width: 55px; text-align: center;" scope="col">Preventa</th>
                                    <th style="min-width: 55px; text-align: center;" scope="col">Administrativas</th>
                                    <th style="min-width: 55px; text-align: center;" scope="col">Implementacion</th>
                                </tr>
                            </thead>
                            <tbody id="tbody_table_performance_ing"
                                style="font-size: 11px;">
                            </tbody>
                        </table>
                    </div>
                    <!-- /.table-responsive -->
                </div>

                <div class="box-footer clearfix">
                    <a class="btn btn-sm btn-danger btn-flat pull-right" id="link_performance_ing" 
                        onclick="CloseAjax('reporte_performance_ingenieria_netdiario.aspx?filter=1');">Ver Reporte
                    </a>
                </div>
                <!-- /.box-footer -->
            </div>
        </div>
        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12" id="proyectos" style="display:none;">
            <div class="box box-danger">
                <div class="box-header with-border">
                    <h3 class="box-title">Mis proyectos</h3>
                    <div class="box-tools pull-right">
                        <a id="btnayuda_proyectos">
                            <i class="material-icons">info</i>
                        </a>
                    </div>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="table table-responsive" style="width: 100%;overflow: scroll;">
                        <table id="table_proyectos" class="dvv table table-responsive table-condensed">
                            <thead>
                                <tr style="font-size: 12px;">
                                    <th style="min-width: 250px; text-align: left;" scope="col">Proyecto</th>
                                    <th style="min-width: 50px; text-align: left;" scope="col">Estatus</th>
                                    <th style="min-width: 190px; text-align: left;" scope="col">Empleado</th>
                                </tr>
                            </thead>
                            <tbody id="tbody_table_proyectos"
                                style="font-size: 12px;">
                            </tbody>
                        </table>
                    </div>
                    <!-- /.table-responsive -->
                </div>

                <div class="box-footer clearfix">
                    <a class="btn btn-sm btn-danger btn-flat pull-right" id="link_proyectos"
                        onclick="CloseAjax('mis_proyectos.aspx');">Ver todos
                    </a>
                </div>
                <!-- /.box-footer -->
            </div>
        </div>
    </div>    
    <div class="modal fade  bs-example-modal-lg" id="modal_widget" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog  modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title"><label id="lblwidgettitle"></label></h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-lg-12" id="divbodywidget">
                        </div>

                    </div>
                    
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default pull-left" data-dismiss="modal">Cerrar</button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <div class="modal fade  bs-example-modal-lg" id="modal_evento" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog  modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Recordatorio</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-lg-6 col-xs-6">
                            <h5><strong><i class="fa fa-calendar" aria-hidden="true"></i>&nbsp;Fecha y hora</strong></h5>
                            <telerik:RadTextBox ID="txtfechainicio" ReadOnly="true" Width="100%" runat="server" Skin="Bootstrap"></telerik:RadTextBox>
                        </div>
                        <div class="col-lg-6 col-xs-6" id="div_fecha_fin">
                            <h5><strong><i class="fa fa-calendar" aria-hidden="true"></i>&nbsp;Fecha y hora fin</strong></h5>
                            <telerik:RadTextBox ID="txtfechafin" ReadOnly="true" Width="100%" runat="server" Skin="Bootstrap"></telerik:RadTextBox>
                        </div>

                    </div>
                    <div class="row">
                        <div class="col-lg-12 col-sm-12">
                            <h5><strong><i class="fa fa-outdent" aria-hidden="true"></i>&nbsp;Titulo</strong></h5>
                            <telerik:RadTextBox ID="rtxttitulo" ReadOnly="true" Width="100%" runat="server" Skin="Bootstrap"></telerik:RadTextBox>
                        </div>
                    </div>
                    <div class="row" id="div_organizador">
                        <div class="col-lg-12 col-sm-12">
                            <h5><strong><i class="fa fa-map" aria-hidden="true"></i>&nbsp;Lugar</strong></h5>
                            <telerik:RadTextBox ID="rtxtlugar" ReadOnly="true" Width="100%" runat="server" Skin="Bootstrap"></telerik:RadTextBox>
                        </div>
                        <div class="col-lg-6 col-sm-12">
                            <h5><strong><i class="fa fa-user" aria-hidden="true"></i>&nbsp;Organizador</strong></h5>
                            <telerik:RadTextBox ID="rtxtorganizador" ReadOnly="true" Width="100%" runat="server" Skin="Bootstrap"></telerik:RadTextBox>
                        </div>
                        <div class="col-lg-6 col-sm-12">
                            <h5><strong><i class="fa fa-user" aria-hidden="true"></i>&nbsp;Correo del organizador</strong></h5>
                            <telerik:RadTextBox ID="rtxtcorreorganizador" ReadOnly="true" Width="100%" runat="server" Skin="Bootstrap"></telerik:RadTextBox>
                        </div>

                    </div>
                    <div class="row">
                        <div class="col-lg-12 col-sm-12">
                            <h5><strong><i class="fa fa-commenting" aria-hidden="true"></i>&nbsp;Descripción</strong></h5>
                            <telerik:RadTextBox Style="font-size: 12px;" ReadOnly="true" ID="rtxtdescripcion" TextMode="MultiLine" Rows="4" Width="100%" runat="server" Skin="Bootstrap"></telerik:RadTextBox>

                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default pull-left" data-dismiss="modal">Cerrar</button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
        <!-- /.modal -->
    <asp:HiddenField ID="hdf_usuario" runat="server" />
    <asp:HiddenField ID="hdf_numempleado" runat="server" />
    <asp:HiddenField ID="hdf_ver_Todos_empleados" runat="server" />
</asp:Content>