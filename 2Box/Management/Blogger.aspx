<%@ Page Title="2Box - Blogger" Language="C#" MasterPageFile="~/Masterpage/Management.master" AutoEventWireup="true" CodeFile="Blogger.aspx.cs" Inherits="Management_Blogger" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="blogger-header">
        <h2 class="myblog-logo">My Blogs</h2>
        <div class="blog-name-header">
            <span class="blog-name"><asp:Label ID="lblBlogName" runat="server"></asp:Label></span>
            <span class="header-arrow">&nbsp;›&nbsp;</span>
            <span class="group-name"></span>
            <span class="header-arrow">&nbsp;›&nbsp;</span>
            <span class="menu-name"></span>
        </div>
        <div class="blog-search">
            <input type="text" class="search-post" title="Search posts" placeholder="Search posts" />
        </div>
    </div>
    <div class="myblog-bar">
        <a href="PostBlog.aspx" class="button newblog primary-button">New Blog</a>
        <div class="management-button-box">
            <span class="button-box">
                <asp:CheckBox ID="chkSelectAll" runat="server" Checked="false" />                
            </span>
            <span class="button-list post-group">
                <input type="button" class="button update-status" value="Publish" status="Published" />
                <input type="button" class="button update-status" value="Revert to draft" status="Draft" />
                <input type="button" class="button update-private" value="Public" status="Public" />
                <input type="button" class="button update-private" value="Revert to private" status="Private" />
                <input type="button" class="button button-delete" value=" " />
            </span>
            <span class="button-list comment-group">
                <input type="button" class="button comment-publish-all" value="Publish" status="Published" />
                <input type="button" class="button comment-spam-all" value="Spam" status="Spam" />
                <input type="button" class="button comment-delete-all" value=" " />
            </span>
        </div>
    </div>
    <asp:Panel ID="panelManagementLeft" runat="server" class="management-panel-left"><asp:Literal ID="ltrMenus" runat="server"></asp:Literal></asp:Panel>
    <asp:Panel ID="panelManagementRight" runat="server" class="management-panel-right"></asp:Panel>
</asp:Content>