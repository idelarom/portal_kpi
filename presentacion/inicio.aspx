<%@ Page Title="Inicio" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="inicio.aspx.cs" Inherits="presentacion.inicio" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="dist/js/loading.js"></script>
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
       
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <h4 class="page-header">DashBoard</h4>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-3 col-md-3 col-sm-4 col-xs-12" id="dashboard_kpi_ind" style="display:none;">
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
                <!-- /.info-box-content -->
            </div>
            <!-- /.info-box -->
        </div>
        <!-- ./col -->
    </div>
    <div class="row">
        <div class="col-lg-6 col-md-6 col-sm-12" id="dashboard_kpi"  style="display:none;" >
            <div class="box box-danger">
                <div class="box-header with-border">
                    <h3 class="box-title">Dashboard Bonos</h3>

                    <div class="box-tools pull-right">
                        <button class="btn btn-box-tool" type="button" data-widget="collapse">
                            <i class="fa fa-minus"></i>
                        </button>
                    </div>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="table-responsive">
                        <table id="table_dashboard_kpi" class="dvv table no-margin table-condensed">
                            <thead>
                                <tr style="font-size: 11px;">
                                    <th style="min-width: 210px; text-align: left;" scope="col">Empleado</th>
                                    <th style="min-width: 80px; text-align: center;" scope="col">Monto Bono</th>
                                    <th style="min-width: 65px; text-align: center;" scope="col">Total Final</th>
                                    <th style="min-width: 65px; text-align: center;" scope="col">% Total Final</th>
                                </tr>
                            </thead>
                            <tbody id="tbody_table_dashboard_kpi"
                                 style="font-size: 11px;">
                            </tbody>
                        </table>
                    </div>
                    <!-- /.table-responsive -->
                </div>
                <!-- /.box-body -->
                <div class="box-footer clearfix">
                    <a class="btn btn-sm btn-danger btn-flat pull-right" id="link_dashboard_kpi" ">Ver Reporte
                    </a>
                </div>
                <!-- /.box-footer -->
            </div>
        </div>
        
    </div>

    <asp:HiddenField ID="hdf_usuario" runat="server" />
    <asp:HiddenField ID="hdf_numempleado" runat="server" />
</asp:Content>