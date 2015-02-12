<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DisplayBlog.aspx.cs" Inherits="Management_DisplayBlog" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../css/management.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="display-blog-wrapper">
        <asp:GridView ID="gvDisplayBlog" runat="server" CssClass="grid-display-blog" AutoGenerateColumns="False" DataKeyNames="BlogID" DataSourceID="SqlDataSource1" ShowHeader="False">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:CheckBox ID="chkSelect" runat="server" CssClass="select-blog" key='<%# Eval("BlogKey") %>' />
                    </ItemTemplate>
                    <ItemStyle Width="30px" />
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <div>
                            <a href="PostBlog.aspx?key=<%# Eval("BlogKey") %>#edit"><%# Eval("BlogName") %></a>                            
                        </div>
                        <div class="visible-on-select">
                            <a href="PostBlog.aspx?key=<%# Eval("BlogKey") %>#edit">Edit</a>
                            &nbsp;|&nbsp;
                            <a href="../Preview/<%# Eval("BlogKey").ToString() %>" target="_blank">Preview</a>
                            &nbsp;|&nbsp;
                            <a class="delete-blog" key='<%# Eval("BlogKey") %>'>Delete</a>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Label ID="lblBlogStatus" runat="server" Text='<%# Eval("BlogStatusDisplay") %>' Font-Italic="True" ForeColor="#FF9504"></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="60px" />
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Label ID="lblCommentCount" runat="server" Text='<%# Eval("CommentCount","{0:#,##0}") %>'></asp:Label>
                        <img alt="" src="../images/comment.png" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Right" Width="70px" CssClass="display-blog-count" />
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Label ID="lblViewCount" runat="server" Text='<%# Eval("ViewCount","{0:#,##0}") %>'></asp:Label>
                        <img alt="" src="../images/view-gray.png" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Right" Width="70px" CssClass="display-blog-count" />
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Label ID="lblBlogDate" runat="server" Text='<%# Eval("CreateDate", "{0:dd/MM/yyyy}") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:GMDataCenterConnectionString %>" SelectCommand="SELECT * FROM Qry_Blog WHERE (BlogStatus = @BlogStatus OR @BlogStatus = 'ALL') AND (CreateBy = @CreateBy) AND (SearchContent LIKE '%'+@SearchContent+'%' OR @SearchContent='ALL') AND (IsPrivate=@IsPrivate OR @IsPrivate='2') ORDER BY CreateDate">
            <SelectParameters>
                <asp:Parameter Name="BlogStatus" />
                <asp:Parameter Name="CreateBy" />
                <asp:Parameter Name="IsPrivate" />
                <asp:Parameter Name="SearchContent" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:Panel ID="pnNoPostBox" runat="server" CssClass="no-post-box">
            <asp:Label ID="lblMessage" runat="server" Text="" CssClass="no-post-message"></asp:Label>
        </asp:Panel>
    </div>
    </form>
</body>
</html>
