<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AmazonReviews.aspx.cs" Inherits="ProductReviews.Admin.AmazonReviews" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            url: <asp:TextBox runat="server" ID="txtUrl" TextMode="SingleLine" Width="800"></asp:TextBox><br />
            cookie: <asp:TextBox runat="server" ID="txtCookie" TextMode="MultiLine" Height="300" Width="800"></asp:TextBox><br /><br />
            <asp:Button runat="server" ID="btnProcess" Text="Process" OnClick="btnProcess_Click" />
        </div>
    </form>
</body>
</html>
