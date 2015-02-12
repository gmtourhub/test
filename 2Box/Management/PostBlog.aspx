<%@ Page Title="G.M.Blog - Post Blog" Language="C#" MasterPageFile="~/Masterpage/Management.master" AutoEventWireup="true" CodeFile="PostBlog.aspx.cs" Inherits="Management_PostBlog" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="blogger-header" style="padding:10px;">        
        <div class="blog-name-header" style="margin-left:12px;">
            <label>Post</label><asp:TextBox ID="txtPostTitle" runat="server" CssClass="post-name" placeholder="Post title" ></asp:TextBox>
        </div>
        <div class="blog-menus">
            <input type="button" class="button publish-blog primary-button" value="Publish" />
            <input type="button" class="button save-blog" value="Save" />
            <% if(!string.IsNullOrEmpty(htmlContent)){ %>
            <%--<input type="button" class="button preview-blog" value="Preview" preview='<%=previewUrl %>' />--%>
            <a href='<%=previewUrl %>' class="button preview-blog" target="_blank">Preview</a>
            <% } %>
            <input type="button" class="button close-blog" value="Close" />
        </div>
    </div>
    <asp:Panel ID="panelPostLeft" runat="server" class="post-panel-left"><textarea id="txtContent" class="ckeditor" name="txtContent" ><%=htmlContent %></textarea></asp:Panel>
    <asp:Panel ID="panelPostRight" runat="server" class="post-panel-right">
        <h4>Post settings</h4>
        <div class="setting-box categories">
            <label class="header">Categories</label>
            <asp:CheckBoxList ID="chkCategories" CssClass="select-category" runat="server"></asp:CheckBoxList>
        </div>
        <div class="setting-box schedule">
            <label class="header">Schedule</label>
            <asp:RadioButton ID="rdbAutomatic" runat="server" Text="Automatic" GroupName="schedule" /><br />
            <asp:RadioButton ID="rdbSetDateAndTime" runat="server" Text="Set date and Time" GroupName="schedule" /><br />
            <asp:TextBox ID="txtSetDateAndTime" runat="server" CssClass="calendar"></asp:TextBox>
        </div>
        <div class="setting-box privacy">
            <label class="header">Privacy</label>
            <asp:CheckBox ID="chkIsPrivate" runat="server" Text="Private" Checked="true" />
        </div>
        <a href="FileManager.aspx" class="file-manager-button button primary-button" target="_blank">File Manager</a>
    </asp:Panel>
    <asp:HiddenField ID="hdfBlogKey" runat="server" />    
</asp:Content>