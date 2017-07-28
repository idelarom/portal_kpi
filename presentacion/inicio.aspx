<%@ Page Title="Inicio" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="inicio.aspx.cs" Inherits="presentacion.inicio" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="dist/js/loading.js"></script>
    <script type="text/javascript">
        //declaramos los objetos de tipo load desde el inicio
        var opts = {
            lines: 13 // The number of lines to draw
           , length: 28 // The length of each line
           , width: 14 // The line thickness
           , radius: 42 // The radius of the inner circle
           , scale: .45 // Scales overall size of the spinner
           , corners: 1 // Corner roundness (0..1)
           , color: '#fff' // #rgb or #rrggbb or array of colors
           , opacity: 0.5 // Opacity of the lines
           , rotate: 0 // The rotation offset
           , direction: 1 // 1: clockwise, -1: counterclockwise
           , speed: 1 // Rounds per second
           , trail: 60 // Afterglow percentage
           , fps: 20 // Frames per second when using setTimeout() as a fallback for CSS
           , zIndex: 2e9 // The z-index (defaults to 2000000000)
           , className: 'spinner' // The CSS class to assign to the spinner
           , top: '45%' // Top position relative to parent
           , left: '50%' // Left position relative to parent
           , shadow: false // Whether to render a shadow
           , hwaccel: false // Whether to use hardware acceleration
           , position: 'absolute' // Element positioning
        };

        function ModalClose() {
            $('#myModalExcel').modal('hide');
        }

        $(document).ready(function () {
            CargarDashboardbonos();
        });
        function CargarDashboardbonos() {
            //load de dashboard bonos
            var target = document.getElementById('dashboard_kpi'); //put your target here!
            var spinner = new Spinner(opts).spin(target);
            var usuario = "SGAYTANS";// $('#<%= hdf_usuario.ClientID%>').val();
            $.ajax({
                url: 'reporte_dashboard_bonos_kpi.aspx/GetDashboardBonosValues',
                contentType: "application/json; charset=utf-8",
                type: "POST",
                dataType: "json",
                data: "{lista_usuarios:'" + usuario + "',usuario:'" + usuario + "'}",
                success: function (response) {
                    var bono = JSON.parse(response.d);
                    console.log("bono", bono[0].Total_Final);
                    var bono_porcentaje =  bono[0]._Total_Final.replace(' %','');
                    $("#bono_trimestral").text(bono[0].Total_Final);
                    $("#progress_bar_bono_kpi").css("width", Math.round(bono_porcentaje) + "%");
                    $("#progress_bono_kpi").text(bono_porcentaje + " % alcanzado");
                    spinner.stop();
                },
                error: function (result, status, err) {
                    console.log("error", result.responseText);
                    spinner.stop();
                }
            });
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
        <div class="col-md-3 col-sm-4 col-xs-12" id="dashboard_kpi">
            <div class="info-box bg-red">
                <span class="info-box-icon"><i class="fa fa-bookmark-o"></i></span>

                <div class="info-box-content">
                    <span class="info-box-text">Bono Trimestral</span>
                    <span class="info-box-number">
                        <label id="bono_trimestral">$ 0.00</label></span>

                    <div class="progress">
                        <div id="progress_bar_bono_kpi" class="progress-bar" style="width: 0%"></div>
                    </div>
                    <span id="progress_bono_kpi" class="progress-description">0 % porcentaje alcanzado.
                  </span>
                </div>
                <!-- /.info-box-content -->
            </div>
            <!-- /.info-box -->
        </div>
        <!-- ./col -->
    </div>
    <div class="row">

        <div class="col-lg-6 col-md-6 col-sm-12" id="Oportunidades" style="display:none">
            <!-- TABLE: LATEST ORDERS -->
            <div class="box box-danger">
                <div class="box-header with-border">
                    <h3 class="box-title">Dashboard bonos</h3>
                    <div class="box-tools pull-right">
                    </div>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                </div>
                
                    <div class="overlay info-box-content">
                            <i class="fa fa-refresh fa-spin"></i>
                        </div>
                <!-- /.box-body -->
                <div class="box-footer clearfix">
                </div>
                <!-- /.box-footer -->
            </div>
        </div>
        <div class="col-lg-6 col-md-6 col-sm-12" id="Valor_ganado"  style="display:none">
            <!-- TABLE: LATEST ORDERS -->
            <div class="box box-danger">
                <div class="box-header with-border">
                    <h3 class="box-title">Dashboard bonos</h3>
                    <div class="box-tools pull-right">
                    </div>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                </div>
                <!-- /.box-body -->
                <div class="box-footer clearfix">
                </div>
                <!-- /.box-footer -->
            </div>
        </div>
        <div class="col-lg-6 col-md-6 col-sm-12" id="KPI"  style="display:none">
            <!-- TABLE: LATEST ORDERS -->
            <div class="box box-danger">
                <div class="box-header with-border">
                    <h3 class="box-title">KPI</h3>
                    <div class="box-tools pull-right">
                    </div>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                </div>
                <!-- /.box-body -->
                <div class="box-footer clearfix">
                </div>
                <!-- /.box-footer -->
            </div>
        </div>
       
    </div>
    
                <asp:HiddenField ID="hdf_usuario" runat="server" />
</asp:Content>