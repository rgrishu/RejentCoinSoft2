<%@ Page Title="" Language="C#" MasterPageFile="~/admin/adminmaster.master" AutoEventWireup="true" CodeFile="ViewTicket.aspx.cs" Inherits="admin_ViewTicket" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="contentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentData" runat="Server">

    <asp:GridView ID="grdticket" runat="server" CssClass="table table-bordered table-hover" Width="100%" AutoGenerateColumns="False" OnRowDataBound = "OnRowDataBound" OnSelectedIndexChanged = "OnSelectedIndexChanged" AllowPaging="true" PageSize="50">
        <Columns>

            <%--id	FromId	ToId	MessageTitle	MessageDescription	MentionBy	MentionDate	ImageName	TicketID	UserId	EntryBy	ModifyBy	EntryDate	ModifyDate	Status	username

41	204793	admin	sunny	sunny	204793	2022-02-01 18:59:21.070	support_01034fScreenshot (7).png	6	NULL	NULL	NULL	NULL	NULL	NULL	Sanjiv Kumar Kushwaha--%>


            <asp:TemplateField HeaderText="#">
                <ItemTemplate>
                    <%#Container.DataItemIndex+1 %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Title">
                <ItemTemplate>
                   <asp:Label ID="lblMsgTitle" runat="server" Text='<%#Eval("MessageTitle") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Date">
                <ItemTemplate>
                    <asp:Label ID="lblMentionDate" runat="server" Text= '<%#Eval("EntryDate", "{0:dd, MMM yyyy}") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Status">
                <ItemTemplate>
               
                    <asp:Label ID="lblStatus" runat="server"  Text='<%#Eval("statustext") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Last Reply">
                <ItemTemplate>
                    <asp:Label ID="lblrep" runat="server" Text='<%#Eval("LastReply") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Action" Visible="false">
                <ItemTemplate>
                    <asp:Button runat="server" ID="rep" OnClick="btnrep_click" Text="Reply" />
                    <asp:HiddenField id="hidTecketId" runat="server" Value='<%#Eval("TicketID") %>' />
                </ItemTemplate>
            </asp:TemplateField>
           
        </Columns>
    </asp:GridView>

</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="contentScript" runat="Server">
</asp:Content>

