<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Message.aspx.cs" Inherits="Management_Message" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>2Box - Message</title>
    <link href="../css/bootstrap.css" rel="stylesheet" />        
    <link href="../css/management.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="management-header">
        2Box Management        
    </div>
    <div class="management-wrapper" style="width:1000px;">
        <div class="message-body">
            <asp:Label ID="lblMessage" runat="server"></asp:Label><br />
            <span class="goto-gmblog">Goto 2Box click <a href="../">here</a></span><br />
            <span class="goto-gmblog">Goto login click <a href="../Login.aspx">here</a></span><br />
        </div>
    </div>
    </form>
</body>
</html>
