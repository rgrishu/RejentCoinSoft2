﻿<%@ Page Title="User Report" Language="C#" MasterPageFile="adminmaster.master" AutoEventWireup="true" CodeFile="UserReport.aspx.cs" Inherits="admin_UserReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentHeader" runat="Server">
  <h4 class="page-title">User Report</h4>
							<ol class="breadcrumb">
								<li class="breadcrumb-item"><a href="Dashboard.aspx">Home</a></li>
								<li class="breadcrumb-item active" aria-current="page">User Report</li>
							</ol>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentData" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div class="modal2">
                <div class="center2">
                    <img alt="" src="loader.gif" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="card">
                <div class="card-header">
                    <h5>Search Criteria</h5>
                </div>
                <div class="card-body">
                    <div class="row form-group">
                        <asp:HiddenField ID ="hiddWith" runat="server"  />
                        <div class="col-md-2">Name</div>
                        <div class="col-md-2">
                            <asp:TextBox ID="txtname" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-2">Mobile</div>
                        <div class="col-md-2">
                            <asp:TextBox ID="txtmobile" onkeypress="return isNumber(event)" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-2">Email</div>
                        <div class="col-md-2">
                            <asp:TextBox ID="txtemail" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row form-group">
                        <div class="col-md-2">From Date</div>
                        <div class="col-md-2">
                            <asp:TextBox ID="txtfromdate" CssClass="form-control datepicker-here" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-2">To Date</div>
                        <div class="col-md-2">
                            <asp:TextBox ID="txttodate" CssClass="form-control datepicker-here" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-2">Select Country</div>
                        <div class="col-md-2">
                            <asp:DropDownList ID="ddcountry" AutoPostBack="true" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddcountry_SelectedIndexChanged">
                                <asp:ListItem Value="0"> Select Country</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row form-group">
                        <div class="col-md-2">Select State</div>
                        <div class="col-md-2">
                            <asp:DropDownList ID="ddstate" CssClass="form-control" runat="server">
                                <asp:ListItem Value="0"> Select State</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-2">Select City</div>
                        <div class="col-md-2">
                            <asp:TextBox ID="txtcityname" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-2">Select Area</div>
                        <div class="col-md-2">
                            <asp:DropDownList ID="ddarea" CssClass="form-control" runat="server">
                                <asp:ListItem Value="0"> Select Area</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row form-group">
                        <div class="col-md-2">User Id</div>
                        <div class="col-md-2">
                            <asp:TextBox ID="txtuserid" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                           <div class="col-md-2">Wallet Address</div>
                        <div class="col-md-2">
                            <asp:TextBox ID="txtwalletaddress" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <hr />
                    <div class="row form-group">
                        <div class="col-md-12">
                            <asp:Button ID="btnSubmit" CssClass="btn btn-primary has-ripple" runat="server" Text="Search" OnClick="btnSubmit_Click" />
                            <asp:Button ID="btnCancel" CssClass="btn btn-danger has-ripple" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                            <asp:Button ID="btnExcel" CssClass="btn btn-danger has-ripple" runat="server" Text="Export" OnClick="btnExcel_Click" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="card">
                <div class="card-header">
                    <h5>User List</h5>
                </div>
                <div class="card-body">
                    <div class="table-responsive">
                        <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover" AllowPaging="true" PageSize="40" OnPageIndexChanging="GridView1_PageIndexChanging" Width="100%" OnRowDataBound="GridView1_RowDataBound" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="#">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="User ID">
                                    <ItemTemplate>
                                        <asp:Label ID="lbluserid" runat="server" Text='<%#Eval("userid") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblusername" runat="server" Text='<%#Eval("username") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sponser Id">
                                    <ItemTemplate>
                                        <asp:Label ID="lblsponserid" runat="server" Text='<%#Eval("sponserid") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Mobile">
                                    <ItemTemplate>
                                        <asp:Label ID="lblmobile" runat="server" Text='<%#Eval("mobile") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Email">
                                    <ItemTemplate>
                                        <asp:Label ID="lblemail" runat="server" Text='<%#Eval("email") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="City">
                                    <ItemTemplate>
                                        <asp:Label ID="lbladdress" runat="server" Text='<%#Eval("cityname") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%-- <asp:TemplateField HeaderText="Wallet Balance">
                                        <ItemTemplate>
                                            <asp:Label ID="lblewalletbalance" runat="server" Text='<%#Eval("balanceamount") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Password">
                                    <ItemTemplate>
                                        <asp:Label ID="lblpassword" runat="server" Text='<%#Eval("password") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lbldate" runat="server" Text='<%#Eval("mentiondate","{0:dd/MM/yyyy}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Daily Deduction %">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtdailydeduction" OnTextChanged="txtdailydeduction_TextChanged" style="width:120px" AutoPostBack="true" Text='<%#Eval("DailyDeduction") %>' runat="server"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                               
                                <asp:TemplateField HeaderText="Active Status">
                                    <ItemTemplate>
                                        <asp:Label ID="lblactivestatus" runat="server" Text='<%#Eval("activestatus") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Withdraw ">
                                    <ItemTemplate>
                                        -<asp:Label ID="lblloginstatus" runat="server" Visible="false" Text='<%#Eval("IsWithdrawalDisable") %>'></asp:Label>
                                        <asp:LinkButton ID="lbactive" CssClass="badge badge-success  btn-sm" CommandName="myactive" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" runat="server">Enable</asp:LinkButton>
                                          <asp:LinkButton ID="llbdeactive" CssClass="badge badge-danger btn-sm" CommandName="mydeactive" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" runat="server">Disable</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbEdit" CommandName="edt" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" runat="server"><i class="fa fa-pencil" aria-hidden="true"></i></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExcel" />
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" runat="Server">
    <asp:UpdatePanel runat="server" ID="uplMaster" UpdateMode="Always">
        <ContentTemplate>
            <div id="myModal" class="modal fade">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title">Edit User Details (User Id:
                                <asp:Label runat="server" ID="lbluseridedit"></asp:Label>)</h4>
                        </div>
                        <div class="modal-body">
                            <div class="row form-group">
                                <div class="col-md-2">Name</div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtnameedit" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-md-1"></div>
                                <div class="col-md-2">Mobile</div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtmobileedit" onkeypress="return isNumber(event)" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row form-group">
                                <div class="col-md-2">Email</div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtemailedit" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-md-1"></div>
                                <div class="col-md-2">Gender</div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddgenderedit" CssClass="form-control" runat="server">
                                        <asp:ListItem Value="0">Select Gender</asp:ListItem>
                                        <asp:ListItem Value="Male">Male</asp:ListItem>
                                        <asp:ListItem Value="Female">Female</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="row form-group">
                                <div class="col-md-2">Address</div>
                                <div class="col-md-9">
                                    <asp:TextBox ID="txtaddressedit" TextMode="MultiLine" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row form-group">
                                <div class="col-md-2">Select Country</div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddcountryedit" AutoPostBack="true" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddcountryedit_SelectedIndexChanged">
                                        <asp:ListItem Value="0"> Select Country</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-1"></div>
                                <div class="col-md-2">Select State</div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddstateedit" CssClass="form-control" runat="server">
                                        <asp:ListItem Value="0"> Select State</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="row form-group">
                                <div class="col-md-2">Select City</div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtcitynamedit" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-md-1"></div>
                                <div class="col-md-2">Area Name</div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtareaname" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>

                            </div>
                            <div class="row form-group">
                                <div class="col-md-2">Pincode</div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtpincodeedit" onkeypress="return isNumber(event)" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-md-1"></div>
                                <div class="col-md-2">Date of Birth</div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtdateofbirthedit" CssClass="form-control datepicker-here" runat="server"></asp:TextBox>
                                </div>
                            </div>
                           <%-- <h3>Bank Details</h3>
                            <div class="row form-group">
                                <div class="col-md-2">A/c Holder Name</div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtaccountholdername" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-md-1"></div>
                                <div class="col-md-2">A/c No</div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtaccountno" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row form-group">
                                <div class="col-md-2">IFSC Code</div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtifsccode" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-md-1"></div>
                                <div class="col-md-2">PAN number</div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtpan" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row form-group">
                                <div class="col-md-2">Bank</div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddbank" CssClass="form-control" runat="server"></asp:DropDownList>
                                </div>
                                <div class="col-md-1"></div>
                                <div class="col-md-2">Branch</div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtbranchname" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>--%>
                            <div class="row form-group">
                                <div class="col-md-2">Select Image</div>
                                <div class="col-md-3">
                                    <asp:FileUpload ID="FileUpload1" runat="server" />
                                    <asp:Label ID="lblimagename" Visible="false" runat="server" Text=""></asp:Label>
                                </div>
                                <div class="col-md-1"></div>

                            <%--    <div class="col-md-2">Is Dummy Data</div>
                                <div class="col-md-3">
                                    <asp:CheckBox ID="ck1" runat="server" />
                                </div>--%>
                            </div>
                             <%--<div class="row form-group">
                                <div class="col-md-2">Is Direct ROI</div>
                                <div class="col-md-3">
                                    <asp:CheckBox ID="ckdirectroi" runat="server" />
                                </div>
                                 <div class="col-md-1"></div>

                                <div class="col-md-2">Is Normal ROI</div>
                                <div class="col-md-3">
                                    <asp:CheckBox ID="cknormalroi" runat="server" />
                                </div>
                                 </div>
                            </div>--%>
                      
                        <div class="modal-footer">
                            <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClientClick="return validate2();" CssClass="btn btn-primary has-ripple" OnClick="btnUpdate_Click" />
                            <button type="button" class="btn btn-danger" data-bs-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnUpdate" />
        </Triggers>
    </asp:UpdatePanel>
    <script type="text/javascript">
        function validate2() {
            if (document.getElementById("<%=txtnameedit.ClientID%>").value == "") {

                   toastr.warning('Warning', 'Enter Name');
                   document.getElementById("<%=txtnameedit.ClientID%>").focus();
                return false;
               }
               if (document.getElementById("<%=txtmobileedit.ClientID%>").value == "") {

                   toastr.warning('Warning', 'Enter Mobile');
                   document.getElementById("<%=txtmobileedit.ClientID%>").focus();
                return false;
               }


        }
    </script>
    <script type="text/javascript">


        function showModal() {
            $('#myModal').modal('show');
        }
        function Closepopup() {
            $('#myModal').modal('hide');
            $('body').removeClass('modal-open');
            $('body').css('padding-right', '0');
            $('.modal-backdrop').remove();


        }
    </script>
</asp:Content>

