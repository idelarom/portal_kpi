<%@ Page Title="Mis publicaciones" ValidateRequest="false" EnableEventValidation="false"  Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="mis_publicaciones.aspx.cs" Inherits="presentacion.mis_publicaciones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript">
  $(function () {
    // Replace the <textarea id="editor1"> with a CKEditor
    // instance, using default configuration.
      CKEDITOR.replace('ContentPlaceHolder1_editor1')
    //bootstrap WYSIHTML5 - text editor
    $('.textarea').wysihtml5()
  })
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <h3 class="page-header">Mis publicaciones</h3>
        </div>
                
        <div class="col-md-12"><div class="box box-danger">
            <div class="box-header">
              <h3 class="box-title">Realizar publicación
                <small>Puede escribir una publicación y compartirla en tiempo real con otros usuarios del portal.</small>
              </h3>
            </div>
            <!-- /.box-header -->
            <div class="box-body pad">
                <div>
                    <textarea id="editor1" name="editor1" rows="10" cols="80" runat="server">
                                            This is my textarea to be replaced with CKEditor.
                    </textarea>
                </div>
            </div>
            <div class="box-footer">   
        <asp:UpdatePanel ID="uod" runat="server">
            <ContentTemplate>
                <asp:Button ID="btnpublicar" OnClick="btnpublicar_Click" CssClass="btn btn-primary btn-flat pull-right" runat="server" Text="Publicar" />
            </ContentTemplate>
        </asp:UpdatePanel>          
            </div>
          </div>
          <!-- /.box -->
        </div>
    </div>
    <div class="row">
        <asp:Repeater ID="repeater_publicaciones" runat="server">
            <ItemTemplate>

                <div class="col-lg-12">
                    <!-- Box Comment -->
                    <div class="box box-widget">
                        <div class="box-header with-border">
                            <div class="user-block">
                                <img class="img-circle" 
                                    src='<%# "../img/"+ Eval("imagen_usuario").ToString() %>' alt="User Image">
                                <span class="username"><a href="#"><%# System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(Eval("nombre").ToString().ToLower()) %></a></span>
                                <span class="description"><%# Eval("fecha_str") %></span>
                            </div>
                            <!-- /.user-block -->
                            <div class="box-tools">
                                <button type="button" class="btn btn-box-tool" data-toggle="tooltip" title="" data-original-title="Mark as read">
                                    <i class="fa fa-circle-o"></i>
                                </button>
                                <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                    <i class="fa fa-minus"></i>
                                </button>
                                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
                            </div>
                            <!-- /.box-tools -->
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <%# Eval("descripcion") %>
                            <button type="button" class="btn btn-default btn-xs"><i class="fa fa-thumbs-o-up"></i>Like</button>
                            <span class="pull-right text-muted">127 likes - 3 comments</span>
                        </div>
                        <!-- /.box-body -->
                        <div class="box-footer box-comments">
                            <div class="box-comment">
                                <!-- User image -->
                                <img class="img-circle img-sm" src="../dist/img/user3-128x128.jpg" alt="User Image">

                                <div class="comment-text">
                                    <span class="username">Maria Gonzales
                        <span class="text-muted pull-right">8:03 PM Today</span>
                                    </span>
                                    <!-- /.username -->
                                    It is a long established fact that a reader will be distracted
                                    by the readable content of a page when looking at its layout.
                                </div>
                                <!-- /.comment-text -->
                            </div>
                            <!-- /.box-comment -->
                            <div class="box-comment">
                                <!-- User image -->
                                <img class="img-circle img-sm" src="../dist/img/user4-128x128.jpg" alt="User Image">

                                <div class="comment-text">
                                    <span class="username">Luna Stark
                        <span class="text-muted pull-right">8:03 PM Today</span>
                                    </span>
                                    <!-- /.username -->
                                    It is a long established fact that a reader will be distracted
                  by the readable content of a page when looking at its layout.
                                </div>
                                <!-- /.comment-text -->
                            </div>
                            <!-- /.box-comment -->
                        </div>
                        <!-- /.box-footer -->
                        <div class="box-footer">
                            <form action="#" method="post">
                                <img class="img-responsive img-circle img-sm" src="../dist/img/user4-128x128.jpg" alt="Alt Text">
                                <!-- .img-push is used to add margin to elements next to floating images -->
                                <div class="img-push">
                                    <input type="text" class="form-control input-sm" placeholder="Press enter to post comment">
                                </div>
                            </form>
                        </div>
                        <!-- /.box-footer -->
                    </div>
                    <!-- /.box -->
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
