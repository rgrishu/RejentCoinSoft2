<%@ Page Title="" Language="C#" MasterPageFile="~/user/usermaster.master" AutoEventWireup="true" CodeFile="Reply.aspx.cs" Inherits="user_Reply" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    

    <script type="text/javascript">
        var counter = 0;
        function AddFileUpload() {
            var div = document.createElement('DIV');
            div.innerHTML = '<input id="file' + counter + '" name = "file' + counter + '" type="file" /><input id="Button' + counter + '" type="button" value="Remove" onclick = "RemoveFileUpload(this)" />';
            document.getElementById("FileUploadContainer").appendChild(div);
            counter++;
        }
        function RemoveFileUpload(div) {
            document.getElementById("FileUploadContainer").removeChild(div.parentNode);
        }

        function openModal() {
            $('#myModal').modal('show');
        }

    </script>


    <style>
        .widget-header {
            width: 100%;
            height: 34px;
            background: -webkit-gradient(linear, 0 50%, 0 100%, from(#f2f0f2), to(#edeaf0));
            background: -webkit-linear-gradient(top, #f2f0f2, #edeaf0);
            background: -moz-linear-gradient(top, #f2f0f2, #edeaf0);
            background: -o-linear-gradient(top, #f2f0f2, #edeaf0);
            background: linear-gradient(to bottom, #f2f0f2, #edeaf0);
            border-bottom: 1px solid #d4d1d7;
            box-shadow: inset 0 1px 0 #fff;
            border-top-left-radius: 5px;
            border-top-right-radius: 5px;
        }

        .modal-danger {
            background: #6e0000;
            color: #fff;
        }

        input[type="radio"], input[type="checkbox"] {
            margin: 4px 0 0;
            margin-top: 0px;
            line-height: normal;
            /* padding-top: 14px; */
            margin-right: 7px;
        }

        body {
            font-family: Verdana;
        }

        /* FANCY COLLAPSE PANEL STYLES */
        .fancy-collapse-panel .panel-default > .panel-heading {
            padding: 0;
        }

        .fancy-collapse-panel .panel-heading a {
            padding: 12px 35px 12px 15px;
            display: inline-block;
            width: 100%;
            background-color: #EE556C;
            color: #ffffff;
            position: relative;
            text-decoration: none;
            border-radius: 4px;
        }

            .fancy-collapse-panel .panel-heading a:after {
                font-family: "FontAwesome";
                content: "\f147";
                position: absolute;
                right: 20px;
                font-size: 27px;
                font-weight: 400;
                top: 50%;
                line-height: 1;
                margin-top: -12px;
            }

            .fancy-collapse-panel .panel-heading a.collapsed:after {
                content: "\f196";
            }

        .user {
            padding: 7px 0;
        }

            .user i {
                float: left;
                font-size: 2.2em;
                padding-right: 4px;
            }

            .user .name {
                font-size: 1.2em;
                color: #000;
                font-weight: 500;
            }

            .user small {
                font-size: 0.8em;
            }

        table > tbody > tr > td > .widget {
            width: 886px;
        }

        hr {
            margin: 8px 0;
        }

        .user {
            padding: 2px 0;
        }

        .default-according.style-1 a[aria-expanded="false"]:before {
            content: "";
            font-family: IcoFont;
        }

        .default-according.style-1 a:before {
            right: 20px;
            position: absolute;
            -webkit-transition: 0.4s;
            transition: 0.4s;
        }

        .default-according.style-1 a[aria-expanded="true"]:before {
            content: "";
            font-family: IcoFont;
        }
        .pl-0 {
            padding-left:0!important;
        }
        .table-condensed thead, tbody, tfoot, tr, td, th {
            text-align:left!important;
        }
        .dashboard-default-sec .card .card-header i, .dashboard-2-main .card .card-header i {
            font-size: 24px !important;
            padding: 0px 8px!important;
        }
        .dashboard-default-sec .card .card-header h5, .dashboard-2-main .card .card-header h5 {
    font-size: 17px!important;
    font-weight: normal!important;
    
}
        .card .card-header {
   
    background-color: #efedf1;
}
        .card {
    margin-bottom: inherit!important; 
    
}
    </style>

    <script src="../ckeditor/ckeditor.js"></script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="contentHeader" runat="Server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="contentData" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

         <div class="row">
        <div class="col-sm-12">
            <div class="card">
                <div class="card-header">
                    <h5>Ticket</h5>
                  </div>

                <div class="card-body">

                    <div class="default-according style-1" id="accordionoc">
        <div class="card">
            <div class="card-header bg-primary">
                <h5 class="mb-0 pl-0">
                    <a class="btn btn-link text-white collapsed" data-bs-toggle="collapse" id="test" runat="server" data-bs-target="#collapseicon" aria-expanded="false" aria-controls="collapse11"><i class="fa fa-pencil"></i>Reply</a>
                </h5>
            </div>
                <div class="collapse" id="collapseicon" aria-labelledby="collapseicon" data-bs-parent="#accordionoc" style="">
                    <div class="card-body">
                    <div class="mb-3">
                        <label class="col-form-label">Message</label>
                         <CKEditor:CKEditorControl ID="CKEditor1" BasePath="/ckeditor/" runat="server"></CKEditor:CKEditorControl>
                    </div>

                    <div class="mb-3 row">
                         <label class="col-form-label">Attachments</label>
                        <div class="col-md-4">
                               <div id="FileUploadContainer" for="uploader" style="margin-bottom: 0;">
                                            <div>
                                                <asp:FileUpload ID="FileUpload1" runat="server" />
                                                
                                            </div>
                                        </div>
                                        <small>Allowed File Extensions: .jpg, .gif, .jpeg, .png</small>
                        </div>
                         <div class="col-md-8">
                                <label for="inputSubject"></label>
                                        <input id="Button1" type="button" value="Add More" class="btn btn-dark" onclick="AddFileUpload()"/>
                        </div>
                      
                    </div>

                       <div class="mb-3 text-center">
                          <asp:Button ID="btn_SubmitClose" Text="Submit & Close" runat="server" CssClass="btn btn-light" OnClick="btn_SubmitClose_Click" />
                                <asp:Button ID="btn_Submit" Text="Submit" runat="server" CssClass="btn btn-primary" OnClick="btn_Submit_Click" />

                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-danger" OnClick="btnCancel_Click" />
                           </div>
                        </div>
                    </div>
            </div>
                        </div>


                  <div class="row">
                            <div class="col-12">
                                <asp:DataList ID="DataList1" runat="server" Style="width: 100%;">

                <ItemTemplate>
                    <asp:Label ID="lblResponseId" runat="server" Text='<%#Eval("id") %>' Visible="false"></asp:Label>
                    <div class="card" style="background-color: <%#Eval("Background") %>;">
                        <div class="card-header">
                            <div class="header-top d-sm-flex align-items-center">
                         <h5><i class="fa fa-user"></i>  <%#Eval("UserName") %>
                                        <asp:Label ID="lbl_TicketId" runat="server" Text='<%#Eval("TicketId") %>' Visible="false"></asp:Label></h5>
                          <div class="center-content mt-0">
                           <%-- <p>Client</p>--%>
                          </div>
                          <div class="setting-list">
                          <%#DataBinder.Eval(Container.DataItem, "MentionDate", "{0:dd/MMM/yyyy  hh:mm:ss tt}")%>
                          </div>
                        </div>
                        </div>
                        <!-- /widget-header -->

                        <div class="card-body">

                            <div class="comment-body" style="overflow: auto">

                            <%--    <strong><%#Eval("MessageTitle") %></strong><br />--%>
                                <%#Eval("MessageDescription") %>
                                <br>
                            </div>
                        </div>

                        <!-- /comment -->

                        <%-- <div class="comment" style="background-color:">
                                <div class="comment-body">
                                    <%#Eval("AttachmentCount") %>


                                    <asp:DataList ID="dtl_Attachment" runat="server">
                                        <ItemTemplate>

                                            <a href='<%#Eval("AttachmentPath") %>' target="_blank">View</a>


                                        </ItemTemplate>
                                    </asp:DataList>
                                </div>
                            </div>--%>
                        <div class="card-body">

                            <div class="comment-body" style="overflow: auto">

                            <%--    <strong><%#Eval("MessageTitle") %></strong><br />--%>
                                <a href='<%#Eval("ImagePath") %>' target="_blank">View</a>
                                <br>
                            </div>
                        </div>
                    </div>

                </ItemTemplate>


            </asp:DataList>
                                
                            </div>
                        </div>
                </div>
            </div>
        </div>
    </div>

    <%-- </div>--%>

    <div class="row">
        <div class="col-md-12">
            <asp:DataList ID="dtl_ViewResponse" runat="server" Style="width: 100%;">

                <ItemTemplate>
                    <asp:Label ID="lblResponseId" runat="server" Text="ResponsId" Visible="false"></asp:Label>
                    <div class="widget" style="background-color: antiquewhite">
                        <div class="widget-header" style="height: 43px;">
                            <div class="widget-title" style="line-height: 11px; width: 500px;">
                                <div class="user">
                                    <i class="fa fa-user"></i>
                                    <span class="name">Name
                                    <asp:Label ID="lbl_TicketId" runat="server" Text="TicketId" Visible="false"></asp:Label>

                                    </span>

                                </div>
                </ItemTemplate>
            </asp:DataList>
        </div>
    </div>

   

</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="contentScript" runat="Server">
</asp:Content>
