<%@ Page Title="2Box" Language="C#" MasterPageFile="~/masterpage/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="display-blog-wrapper">
        <div class="panel-left clearfix">
            <asp:DataList ID="dtlBlog" runat="server" CssClass="display-blog-list" OnItemDataBound="dtlBlog_ItemDataBound">
                <ItemTemplate>
                    <div class="post-wrapper">
                        <div class="date-of-post">
                            <span class="date"><%#Convert.ToInt16(Eval("PostDate","{0:dd}")).ToString() %></span><br />
                            <span class="month"><%#Eval("PostDate","{0:MMM}").ToUpper()+(Convert.ToDateTime(Eval("PostDate")).Year==DateTime.Now.Year ? string.Empty : Eval("PostDate","{0:-yy}")) %></span>
                        </div>
                        <div class="post-content">
                            <span class="post-header"><a key='<%#Eval("BlogKey") %>' href='<%#string.Format("{0}Post/{1}",urlDefault,Eval("BlogKey").ToString().Replace(".","-").Replace(" ","-").Replace("/",string.Empty)) %>'><%#Eval("BlogName") %></a></span>
                            <div class="content clearfix">
                                <div class="wrapper"><%#Eval("BlogContent") %></div>
                                <asp:Panel ID="panelPostBy" runat="server" CssClass="post-by">Post by 
                                    <asp:Label ID="lblPostBy" runat="server" Text='<%# System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Eval("CreateByName").ToString().ToLower()) %>'></asp:Label>
                                </asp:Panel>
                                <asp:Panel ID="panelViewLog" runat="server" CssClass="view-log">
                                    <span class="statistic-box" style="margin-right:-10px;">
                                        <asp:Label ID="lblViewCount" runat="server" Text="0"></asp:Label>
                                        <div class="view-label">VIEWS</div>
                                    </span>
                                    <% if(!Blog.Session.IsExpired()){ %>
                                    <span class="statistic-box">
                                        <asp:Label ID="lblCommentCount" runat="server" Text="0"></asp:Label>
                                        <div class="comment-label">COMMENTS</div>  
                                    </span>
                                    <% } %>
                                </asp:Panel>
                            </div>
                        </div>                        
                        <asp:Panel ID="panelDateofPostBlank" runat="server" class="date-of-post no-background-color"></asp:Panel>
                        <asp:Panel ID="panelComment" runat="server" CssClass="post-comment">
                            <div class="display-comment clearfix" key='<%#Eval("BlogKey") %>'></div>
                            <div class="comment-submit-box">
                                <label>Comment</label><br />
                                <asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine" Rows="3" MaxLength="500"></asp:TextBox>
                                <input type="button" value="Submit" class="submit-comment button primary-button pull-right" key='<%#Eval("BlogKey") %>' />
                            </div>
                        </asp:Panel>
                    </div>                    
                </ItemTemplate>
            </asp:DataList>
        </div>
        <div class="panel-right">
            <h4 class="header">Categories</h4>
            <asp:Literal ID="ltrCategories" runat="server"></asp:Literal>
        </div>
    </div>
    <asp:HiddenField ID="hdfFullContent" runat="server" Value="N" />
</asp:Content>