using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Management_DisplayBlog : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Blog.Session.IsExpired()) return;
        string blogStatus = Request.QueryString["BlogStatus"] == null ? "ALL" : Request.QueryString["BlogStatus"].ToString().ToUpper();
        string isPrivate = blogStatus.ToLower().Equals("private") ? "1" : "2";
        blogStatus = string.IsNullOrEmpty(blogStatus) || blogStatus.ToLower().Equals("private") ? "ALL" : blogStatus;        
        string searchContent = Request.QueryString["SearchContent"] == null ? "ALL" : Request.QueryString["SearchContent"].ToString();
        searchContent = string.IsNullOrEmpty(searchContent) ? "ALL" : searchContent;
        SqlDataSource1.SelectParameters["BlogStatus"].DefaultValue = System.Globalization.CultureInfo.CurrentUICulture.TextInfo.ToTitleCase(blogStatus);
        SqlDataSource1.SelectParameters["SearchContent"].DefaultValue = searchContent;
        SqlDataSource1.SelectParameters["CreateBy"].DefaultValue = Blog.Session.GetCurrentUserID();
        SqlDataSource1.SelectParameters["IsPrivate"].DefaultValue = isPrivate;
        SqlDataSource1.DataBind();
        gvDisplayBlog.DataBind();
        lblMessage.Text = gvDisplayBlog.Rows.Count > 0 ? string.Empty : "There are no posts.";
        pnNoPostBox.Visible = !(gvDisplayBlog.Rows.Count > 0);
    }
}