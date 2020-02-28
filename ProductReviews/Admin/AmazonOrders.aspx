<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeBehind="AmazonOrders.aspx.cs" Inherits="ProductReviews.Admin.AmazonOrders" %>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <asp:FileUpload ID="ctlFileUpload" runat="server" />
        <br />
        <asp:Button runat="server" ID="btnUpload" Text="Upload" OnClick="btnUpload_Click" />
        <br />
        <asp:GridView ID="gvorderUploadStatuses" runat="server"></asp:GridView>
    </div>
</asp:Content>