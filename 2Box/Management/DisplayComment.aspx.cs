using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Management_DisplayComment : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Blog.Session.IsExpired()) return;
        string commentType = Request.QueryString["CommentType"] == null ? "ALL" : Request.QueryString["CommentType"].ToString().ToUpper();       
        string searchContent = Request.QueryString["SearchContent"] == null ? "ALL" : Request.QueryString["SearchContent"].ToString();
        searchContent = string.IsNullOrEmpty(searchContent) ? "ALL" : searchContent;
        SqlDataSource1.SelectParameters["CommentType"].DefaultValue = System.Globalization.CultureInfo.CurrentUICulture.TextInfo.ToTitleCase(commentType);
        SqlDataSource1.SelectParameters["SearchContent"].DefaultValue = searchContent;
        SqlDataSource1.SelectParameters["CreateBy"].DefaultValue = Blog.Session.GetCurrentUserID();
        SqlDataSource1.DataBind();
        gvDisplayBlog.DataBind();
        lblMessage.Text = gvDisplayBlog.Rows.Count > 0 ? string.Empty : "There are no comments.";
        pnNoPostBox.Visible = !(gvDisplayBlog.Rows.Count > 0);
    }
}