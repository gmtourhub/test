<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FileManager.aspx.cs" Inherits="Management_FileManager" Title="2Box - File Manager" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>File Manager</title>
    <link href="../css/bootstrap.css" rel="stylesheet" />    
    <link href="../css/jquery-ui.css" rel="stylesheet" />
    <link href="../css/jquery-ui.theme.css" rel="stylesheet" />
    <link href="../css/management.css" rel="stylesheet" />
    <script src="../script/jquery-1.11.1.min.js"></script>
    <script src="../script/jquery-ui.js"></script>    
    <script src="../script/blog.js"></script>
    <style type="text/css">
        body,html{
            overflow:auto;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="file-manager-header">File Manager</div>    
    <div class="file-manager-wrapper">
        <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click" />&nbsp;<asp:FileUpload ID="fileUpload" runat="server" AllowMultiple="true" />
        <div class="image-box">
            <asp:Literal ID="ltrImage" runat="server"></asp:Literal>
        </div>
    </div>
    </form>
    <div class="dialog-confirm" title="">
        <span class="ui-icon ui-icon-alert" style="float:left; margin:0 7px 20px 0;"></span><span class="dialog-confirm-text"></span>
    </div>
    <div class="dialog-message" title="Application Message">
        <span class="ui-icon ui-icon-info" style="float:left; margin:0 7px 20px 0;"></span><span class="dialog-message-text"></span>
    </div>
</body>
</html>
