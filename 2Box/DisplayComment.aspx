<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DisplayComment.aspx.cs" Inherits="DisplayComment" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/blog.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="display-comment-wrapper">
        <asp:DataList ID="dtlComment" runat="server" CssClass="datalist-display-comment" OnItemDataBound="dtlComment_ItemDataBound">
            <ItemTemplate>
                <asp:Panel ID="panelCommentBox" runat="server" CssClass="comment-box">
                    <span class="username">
                        <span class="photo comment-photo"></span><label><%#Eval("Username") %></label>
                    </span>
                    <div class="comment-text"><%#Eval("Comment") %></div>
                    <div class="comment-footer"><span><%#Eval("CommentDate","{0:dd-MMM-yyyy HH:mm:ss}") %></span></div>
                    <asp:Panel ID="panelManageBox" runat="server" CssClass="manage-box" key='<%#Eval("CommentID") %>'></asp:Panel>
                </asp:Panel>
            </ItemTemplate>
        </asp:DataList>
    </div>
    </form>
</body>
</html>
