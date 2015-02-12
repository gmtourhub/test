<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" Title="2Box - Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>G.M.Tour & Travel - Blog Login</title>
    <link href="css/bootstrap.css" rel="stylesheet" />
    <link href="css/blog.css" rel="stylesheet" />
    <style type="text/css">
        body{
            background:#fff;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" defaultbutton="btnSubmit" defaultfocus="txtUsername">
    <div>
        <asp:Panel ID="panelLogin" runat="server" CssClass="login-box">
            <h3>2Box Login</h3>
                <asp:TextBox ID="txtUsername" runat="server" placeholder="Username" AutoCompleteType="Disabled"></asp:TextBox>
                <br />
                <asp:TextBox ID="txtPassword" runat="server" placeholder="Password" TextMode="Password"></asp:TextBox>
                <br />
                <asp:Button ID="btnSubmit" runat="server" CssClass="login-submit" Text="Login" OnClick="btnSubmit_Click" /><br />
        </asp:Panel>
    </div>
    </form>
</body>
</html>
