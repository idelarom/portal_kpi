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
        //declaramos los objetos de tipo load desde el inicio (load oscuro)
        var opts2 = {
            lines: 13 // The number of lines to draw
           , length: 28 // The length of each line
           , width: 14 // The line thickness
           , radius: 42 // The radius of the inner circle
           , scale: .45 // Scales overall size of the spinner
           , corners: 1 // Corner roundness (0..1)
           , color: '#000' // #rgb or #rrggbb or array of colors
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
            IniciarWidgets();
        });
        function IniciarWidgets()
        {
            //alert($('#dashboard_kpi_ind').css('display'));
            ////verificamos si los divs estan en la lista, si estan cargamos su informacion mediante ajax
            //if ($('#dashboard_kpi_ind').css('display') == 'block') {
            //    CargarDashboardbonosIndividual();
            //}
            var usuario = $('#<%= hdf_usuario.ClientID%>').val();
            $.ajax({
                url: 'inicio.aspx/getDivs',
                contentType: "application/json; charset=utf-8",
                type: "POST",
                dataType: "json",
                data: "{usuario:'" + usuario + "'}",
                success: function (response) {
                    var bono = JSON.parse(response.d);
                    for (indice = 0; indice < bono.length; indice++)
                    {
                        var div = bono[indice].nombre_codigo;
                        if (div == "dashboard_kpi_ind")
                        {
                            CargarDashboardbonosIndividual();
                        }
                    }
                    //spinner.stop();
                },
                error: function (result, status, err) {
                    console.log("error", result.responseText);
                    //spinner.stop();
                }
            });
        }
        function CargarDashboardbonosIndividual() {
            //load de dashboard bonos
            var target = document.getElementById('dashboard_kpi_ind');
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
                    $("#progress_bar_bono_kpi_ind").css("width", Math.round(bono_porcentaje) + "%");
                    $("#progress_bono_kpi_ind").text(bono_porcentaje + " % alcanzado");
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
        <div class="col-lg-3 col-md-3 col-sm-4 col-xs-12" id="dashboard_kpi_ind">
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
        <div class="col-lg-6 col-md-6 col-sm-12" id="dashboard_kpi"  >
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
                        <table class="table no-margin">
                            <thead>
                                <tr>
                                    <th>Order ID</th>
                                    <th>Item</th>
                                    <th>Status</th>
                                    <th>Popularity</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td><a href="pages/examples/invoice.html">OR9842</a></td>
                                    <td>Call of Duty IV</td>
                                    <td><span class="label label-success">Shipped</span></td>
                                    <td>
                                        <div class="sparkbar" data-height="20" data-color="#00a65a">
                                            <canvas width="34" height="20" style="width: 34px; height: 20px; vertical-align: top; display: inline-block;"></canvas>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td><a href="pages/examples/invoice.html">OR1848</a></td>
                                    <td>Samsung Smart TV</td>
                                    <td><span class="label label-warning">Pending</span></td>
                                    <td>
                                        <div class="sparkbar" data-height="20" data-color="#f39c12">
                                            <canvas width="34" height="20" style="width: 34px; height: 20px; vertical-align: top; display: inline-block;"></canvas>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td><a href="pages/examples/invoice.html">OR7429</a></td>
                                    <td>iPhone 6 Plus</td>
                                    <td><span class="label label-danger">Delivered</span></td>
                                    <td>
                                        <div class="sparkbar" data-height="20" data-color="#f56954">
                                            <canvas width="34" height="20" style="width: 34px; height: 20px; vertical-align: top; display: inline-block;"></canvas>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td><a href="pages/examples/invoice.html">OR7429</a></td>
                                    <td>Samsung Smart TV</td>
                                    <td><span class="label label-info">Processing</span></td>
                                    <td>
                                        <div class="sparkbar" data-height="20" data-color="#00c0ef">
                                            <canvas width="34" height="20" style="width: 34px; height: 20px; vertical-align: top; display: inline-block;"></canvas>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td><a href="pages/examples/invoice.html">OR1848</a></td>
                                    <td>Samsung Smart TV</td>
                                    <td><span class="label label-warning">Pending</span></td>
                                    <td>
                                        <div class="sparkbar" data-height="20" data-color="#f39c12">
                                            <canvas width="34" height="20" style="width: 34px; height: 20px; vertical-align: top; display: inline-block;"></canvas>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td><a href="pages/examples/invoice.html">OR7429</a></td>
                                    <td>iPhone 6 Plus</td>
                                    <td><span class="label label-danger">Delivered</span></td>
                                    <td>
                                        <div class="sparkbar" data-height="20" data-color="#f56954">
                                            <canvas width="34" height="20" style="width: 34px; height: 20px; vertical-align: top; display: inline-block;"></canvas>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td><a href="pages/examples/invoice.html">OR9842</a></td>
                                    <td>Call of Duty IV</td>
                                    <td><span class="label label-success">Shipped</span></td>
                                    <td>
                                        <div class="sparkbar" data-height="20" data-color="#00a65a">
                                            <canvas width="34" height="20" style="width: 34px; height: 20px; vertical-align: top; display: inline-block;"></canvas>
                                        </div>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <!-- /.table-responsive -->
                </div>
                <!-- /.box-body -->
                <div class="box-footer clearfix">
                    <a class="btn btn-sm btn-danger btn-flat pull-right" href="javascript:void(0)">Ver reporte</a>
                </div>
                <!-- /.box-footer -->
            </div>
        </div>
        
        <div class="col-lg-6 col-md-6 col-sm-12" id="Div1"   >
            <div class="box box-danger">
                <div class="box-header with-border">
                    <h3 class="box-title">div1</h3>

                    <div class="box-tools pull-right">
                        <button class="btn btn-box-tool" type="button" data-widget="collapse">
                            <i class="fa fa-minus"></i>
                        </button>
                    </div>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="table-responsive">
                        <table class="table no-margin">
                            <thead>
                                <tr>
                                    <th>Order ID</th>
                                    <th>Item</th>
                                    <th>Status</th>
                                    <th>Popularity</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td><a href="pages/examples/invoice.html">OR9842</a></td>
                                    <td>Call of Duty IV</td>
                                    <td><span class="label label-success">Shipped</span></td>
                                    <td>
                                        <div class="sparkbar" data-height="20" data-color="#00a65a">
                                            <canvas width="34" height="20" style="width: 34px; height: 20px; vertical-align: top; display: inline-block;"></canvas>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td><a href="pages/examples/invoice.html">OR1848</a></td>
                                    <td>Samsung Smart TV</td>
                                    <td><span class="label label-warning">Pending</span></td>
                                    <td>
                                        <div class="sparkbar" data-height="20" data-color="#f39c12">
                                            <canvas width="34" height="20" style="width: 34px; height: 20px; vertical-align: top; display: inline-block;"></canvas>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td><a href="pages/examples/invoice.html">OR7429</a></td>
                                    <td>iPhone 6 Plus</td>
                                    <td><span class="label label-danger">Delivered</span></td>
                                    <td>
                                        <div class="sparkbar" data-height="20" data-color="#f56954">
                                            <canvas width="34" height="20" style="width: 34px; height: 20px; vertical-align: top; display: inline-block;"></canvas>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td><a href="pages/examples/invoice.html">OR7429</a></td>
                                    <td>Samsung Smart TV</td>
                                    <td><span class="label label-info">Processing</span></td>
                                    <td>
                                        <div class="sparkbar" data-height="20" data-color="#00c0ef">
                                            <canvas width="34" height="20" style="width: 34px; height: 20px; vertical-align: top; display: inline-block;"></canvas>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td><a href="pages/examples/invoice.html">OR1848</a></td>
                                    <td>Samsung Smart TV</td>
                                    <td><span class="label label-warning">Pending</span></td>
                                    <td>
                                        <div class="sparkbar" data-height="20" data-color="#f39c12">
                                            <canvas width="34" height="20" style="width: 34px; height: 20px; vertical-align: top; display: inline-block;"></canvas>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td><a href="pages/examples/invoice.html">OR7429</a></td>
                                    <td>iPhone 6 Plus</td>
                                    <td><span class="label label-danger">Delivered</span></td>
                                    <td>
                                        <div class="sparkbar" data-height="20" data-color="#f56954">
                                            <canvas width="34" height="20" style="width: 34px; height: 20px; vertical-align: top; display: inline-block;"></canvas>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td><a href="pages/examples/invoice.html">OR9842</a></td>
                                    <td>Call of Duty IV</td>
                                    <td><span class="label label-success">Shipped</span></td>
                                    <td>
                                        <div class="sparkbar" data-height="20" data-color="#00a65a">
                                            <canvas width="34" height="20" style="width: 34px; height: 20px; vertical-align: top; display: inline-block;"></canvas>
                                        </div>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <!-- /.table-responsive -->
                </div>
                <!-- /.box-body -->
                <div class="box-footer clearfix">
                    <a class="btn btn-sm btn-danger btn-flat pull-right" href="javascript:void(0)">Ver reporte</a>
                </div>
                <!-- /.box-footer -->
            </div>
        </div>

        <div class="col-lg-6 col-md-6 col-sm-12" id="Div2"    >
            <div class="box box-danger">
                <div class="box-header with-border">
                    <h3 class="box-title">Div2</h3>

                    <div class="box-tools pull-right">
                        <button class="btn btn-box-tool" type="button" data-widget="collapse">
                            <i class="fa fa-minus"></i>
                        </button>
                    </div>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="table-responsive">
                        <table class="table no-margin">
                            <thead>
                                <tr>
                                    <th>Order ID</th>
                                    <th>Item</th>
                                    <th>Status</th>
                                    <th>Popularity</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td><a href="pages/examples/invoice.html">OR9842</a></td>
                                    <td>Call of Duty IV</td>
                                    <td><span class="label label-success">Shipped</span></td>
                                    <td>
                                        <div class="sparkbar" data-height="20" data-color="#00a65a">
                                            <canvas width="34" height="20" style="width: 34px; height: 20px; vertical-align: top; display: inline-block;"></canvas>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td><a href="pages/examples/invoice.html">OR1848</a></td>
                                    <td>Samsung Smart TV</td>
                                    <td><span class="label label-warning">Pending</span></td>
                                    <td>
                                        <div class="sparkbar" data-height="20" data-color="#f39c12">
                                            <canvas width="34" height="20" style="width: 34px; height: 20px; vertical-align: top; display: inline-block;"></canvas>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td><a href="pages/examples/invoice.html">OR7429</a></td>
                                    <td>iPhone 6 Plus</td>
                                    <td><span class="label label-danger">Delivered</span></td>
                                    <td>
                                        <div class="sparkbar" data-height="20" data-color="#f56954">
                                            <canvas width="34" height="20" style="width: 34px; height: 20px; vertical-align: top; display: inline-block;"></canvas>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td><a href="pages/examples/invoice.html">OR7429</a></td>
                                    <td>Samsung Smart TV</td>
                                    <td><span class="label label-info">Processing</span></td>
                                    <td>
                                        <div class="sparkbar" data-height="20" data-color="#00c0ef">
                                            <canvas width="34" height="20" style="width: 34px; height: 20px; vertical-align: top; display: inline-block;"></canvas>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td><a href="pages/examples/invoice.html">OR1848</a></td>
                                    <td>Samsung Smart TV</td>
                                    <td><span class="label label-warning">Pending</span></td>
                                    <td>
                                        <div class="sparkbar" data-height="20" data-color="#f39c12">
                                            <canvas width="34" height="20" style="width: 34px; height: 20px; vertical-align: top; display: inline-block;"></canvas>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td><a href="pages/examples/invoice.html">OR7429</a></td>
                                    <td>iPhone 6 Plus</td>
                                    <td><span class="label label-danger">Delivered</span></td>
                                    <td>
                                        <div class="sparkbar" data-height="20" data-color="#f56954">
                                            <canvas width="34" height="20" style="width: 34px; height: 20px; vertical-align: top; display: inline-block;"></canvas>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td><a href="pages/examples/invoice.html">OR9842</a></td>
                                    <td>Call of Duty IV</td>
                                    <td><span class="label label-success">Shipped</span></td>
                                    <td>
                                        <div class="sparkbar" data-height="20" data-color="#00a65a">
                                            <canvas width="34" height="20" style="width: 34px; height: 20px; vertical-align: top; display: inline-block;"></canvas>
                                        </div>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <!-- /.table-responsive -->
                </div>
                <!-- /.box-body -->
                <div class="box-footer clearfix">
                    <a class="btn btn-sm btn-danger btn-flat pull-right" href="javascript:void(0)">Ver reporte</a>
                </div>
                <!-- /.box-footer -->
            </div>
        </div>
    </div>

    <asp:HiddenField ID="hdf_usuario" runat="server" />
</asp:Content>