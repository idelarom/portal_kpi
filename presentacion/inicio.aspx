<%@ Page Title="" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="inicio.aspx.cs" Inherits="presentacion.inicio" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 
    <script type="text/javascript">
        function ModalClose() {
            $('#myModalExcel').modal('hide');
        }
        
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
        .rcbList li{
            font-size: 10px;
        }
        .rcbCheckAllItems label{
            font-size: 11px;
        }

       
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <h4 class="page-header">DashBoard</h4>
        </div>


        <div class="col-lg-6 col-md-6 col-sm-12" id="widget1">
            <!-- TABLE: LATEST ORDERS -->
            <div class="box box-danger">
                <div class="box-header with-border">
                    <h3 class="box-title">Widget 1</h3>
                    <div class="box-tools pull-right">
                        <button type="button" class="btn btn-box-tool" data-widget="collapse">
                            <i class="fa fa-minus"></i>
                        </button>
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
        <div class="col-lg-6 col-md-6 col-sm-12" id="widget2">
            <!-- TABLE: LATEST ORDERS -->
            <div class="box box-danger">
                <div class="box-header with-border">
                    <h3 class="box-title">Widget 2</h3>
                    <div class="box-tools pull-right">
                        <button type="button" class="btn btn-box-tool" data-widget="collapse">
                            <i class="fa fa-minus"></i>
                        </button>
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
        <div class="col-lg-6 col-md-6 col-sm-12" id="widget3">
            <!-- TABLE: LATEST ORDERS -->
            <div class="box box-danger">
                <div class="box-header with-border">
                    <h3 class="box-title">Widget 3</h3>
                    <div class="box-tools pull-right">
                        <button type="button" class="btn btn-box-tool" data-widget="collapse">
                            <i class="fa fa-minus"></i>
                        </button>
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
        <div class="col-lg-6 col-md-6 col-sm-12" id="widget4">
            <!-- TABLE: LATEST ORDERS -->
            <div class="box box-danger">
                <div class="box-header with-border">
                    <h3 class="box-title">Widget 4</h3>
                    <div class="box-tools pull-right">
                        <button type="button" class="btn btn-box-tool" data-widget="collapse">
                            <i class="fa fa-minus"></i>
                        </button>
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
        <div class="col-lg-6 col-md-6 col-sm-12" id="widget5">
            <!-- TABLE: LATEST ORDERS -->
            <div class="box box-danger">
                <div class="box-header with-border">
                    <h3 class="box-title">Widget 5</h3>
                    <div class="box-tools pull-right">
                        <button type="button" class="btn btn-box-tool" data-widget="collapse">
                            <i class="fa fa-minus"></i>
                        </button>
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
        <div class="col-lg-6 col-md-6 col-sm-12" id="widget6">
            <!-- TABLE: LATEST ORDERS -->
            <div class="box box-danger">
                <div class="box-header with-border">
                    <h3 class="box-title">Widget 6</h3>
                    <div class="box-tools pull-right">
                        <button type="button" class="btn btn-box-tool" data-widget="collapse">
                            <i class="fa fa-minus"></i>
                        </button>
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
    
</asp:Content>