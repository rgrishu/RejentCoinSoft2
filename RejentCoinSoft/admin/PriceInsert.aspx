<%@ Page Title="Insert Price" Language="C#" MasterPageFile="adminmaster.master" AutoEventWireup="true" CodeFile="PriceInsert.aspx.cs" Inherits="blue_Dashboard" EnableEventValidation="false" ValidateRequest="false"  MaintainScrollPositionOnPostBack = "true"  %>


<asp:Content ID="Content2" ContentPlaceHolderID="contentHeader" runat="Server">
    <h4 class="page-title">Priv</h4>
    <ol class="breadcrumb">
        <li class="breadcrumb-item"><a href="Dashboard.aspx">Dashboard</a></li>
        <li class="breadcrumb-item active"></li>
    </ol>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentData" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <!-- end row -->
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
   

    <!-- end row -->
    <div class="row">
        <div class="col-xl-12">
            <div class="card">
                <div class="card-header">
                      <strong>Price Insert</strong>
                </div>
                <div class="card-body">
                  
                    <div class="row form-group">
                              <div class="col-md-2">Price</div>
                                    <div class="col-md-10">

                                        <asp:TextBox ID="txtPrice"  CssClass="form-control" runat="server"  onkeypress="return isNumber(event)"></asp:TextBox>
                                        
                                    </div>
                    </div>
                       <div class="row rowfooter">
                        <div class="col-md-12">
                            <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary" OnClick="btnSubmit_Click"  Text="Submit" />
                            <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-danger" OnClick="btnCancel_Click" Text="Cancel" />
                        </div>
                        
                    </div>
                </div>
            </div>
        </div>

    </div>
      <div class="row">
        

    </div>
                 </ContentTemplate>
         <Triggers>
             <asp:PostBackTrigger ControlID="btnSubmit" />
         </Triggers>
    </asp:UpdatePanel>
</asp:Content>


