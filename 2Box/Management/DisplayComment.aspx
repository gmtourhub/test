<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DisplayComment.aspx.cs" Inherits="Management_DisplayComment" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../css/management.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="display-comment-wrapper">
            <asp:GridView ID="gvDisplayBlog" runat="server" CssClass="grid-display-comment" AutoGenerateColumns="False" DataKeyNames="BlogID" DataSourceID="SqlDataSource1" ShowHeader="False">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkSelect" runat="server" CssClass="select-comment" key='<%# Eval("CommentID") %>' />
                        </ItemTemplate>
                        <ItemStyle Width="30px" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <div>
                                <asp:Label ID="lblComment" runat="server" Text='<%# Eval("Comment") %>'></asp:Label>&nbsp;on&nbsp;
                                <a href="../Post/<%# Eval("BlogName").ToString().Replace(".","-").Replace(" ","-") %>#preview" target="_blank"><%# Eval("BlogName") %></a>                            
                            </div>
                            <div class="visible-on-select" key='<%# Eval("CommentID") %>'>
                                <asp:LinkButton ID="lnkPublish" runat="server" CssClass="set-publish" Text="Publish" Visible='<%# Eval("CommentType").Equals("Spam") %>'></asp:LinkButton>     
                                <asp:Label runat="server" Text="&nbsp;|&nbsp;" Visible='<%# Eval("CommentType").Equals("Spam")%>'></asp:Label>                                                    
                                <asp:LinkButton ID="lnkSpam" runat="server" CssClass="set-spam" Text="Spam" Visible='<%# Eval("CommentType").Equals("Published") %>'></asp:LinkButton>
                                <asp:Label runat="server" Text="&nbsp;|&nbsp;" Visible='<%# Eval("CommentType").Equals("Published")%>'></asp:Label>  
                                <a class="delete-comment">Delete</a>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Label ID="lblBlogStatus" runat="server" Text='<%# Eval("CommentType") %>' Font-Italic="True" ForeColor="#FF9504"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="50px" />
                    </asp:TemplateField>                    
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Label ID="lblBlogDate" runat="server" Text='<%# Eval("CommentDate", "{0:dd/MM/yyyy HH:mm}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="150px" />
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:GMDataCenterConnectionString %>" SelectCommand="SELECT * FROM Qry_Blog_Comment WHERE (CommentType = @CommentType OR @CommentType = 'ALL') AND (CreateBy = @CreateBy) AND (SearchContent LIKE '%'+@SearchContent+'%' OR @SearchContent='ALL') ORDER BY BlogName,CommentDate">
                <SelectParameters>
                    <asp:Parameter Name="CreateBy" />
                    <asp:Parameter Name="CommentType" />                
                    <asp:Parameter Name="SearchContent" />
                </SelectParameters>
            </asp:SqlDataSource>
            <asp:Panel ID="pnNoPostBox" runat="server" CssClass="no-post-box">
                <asp:Label ID="lblMessage" runat="server" Text="" CssClass="no-post-message"></asp:Label>
            </asp:Panel>
        </div>
    </div>
    </form>
</body>
</html>
