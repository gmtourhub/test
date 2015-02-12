using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login : System.Web.UI.Page
{
    #region Methods
    void checkAuthentication(string username,string password,bool checkPassword)
    {
        if (Blog.Database.UserAuthenticate(username, password, checkPassword))
        {
            Session["userid"] = username.ToUpper();
            if(string.IsNullOrEmpty(Blog.Database.UserProfile(Blog.Session.GetCurrentUserID()).UserID) && Request.QueryString["post"] == null)
                Response.Redirect(".");
            else if (Request.QueryString["post"]==null)
                Response.Redirect("Management/Blogger.aspx");
            else if (Request.QueryString["post"] != null)
            {
                string postKey = Request.QueryString["post"];
                keepViewLog(postKey);
                Response.Redirect("Post/" + postKey);
            }
        }
        else
        {
            Session.Clear();
            Session.Abandon();
            string message = "Username and password is invalid.".Replace(" ","-");
            Response.Redirect("Message/"+message);
        }
    }
    [WebMethod]
    public static string keepViewLog(string BlogKey)
    {
        GMDataCenterDataContext context = new GMDataCenterDataContext();
        var blog = context.Application_Blogs.Where(w => w.BlogKey.ToString().ToLower().Equals(BlogKey.ToLower())).FirstOrDefault();
        if (blog != null)
        {
            Application_Blog_ViewLog create = new Application_Blog_ViewLog();
            create.BlogID = blog.BlogID;
            create.ViewType = Blog.Session.IsExpired() ? "Public" : "Private";
            create.UserID = Blog.Session.GetCurrentUserID();
            create.ViewDate = DateTime.Now;
            context.Application_Blog_ViewLogs.InsertOnSubmit(create);
            context.SubmitChanges();
        }
        context.Dispose();
        return "Success";
    }
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["mode"] != null && Request.QueryString["mode"] == "logout")
        {
            Session.Clear();
            Session.Abandon();
        }
        else if (Request.QueryString["key"] == null)
        {
            if (!Blog.Session.IsExpired()) Response.Redirect("Management/Blogger.aspx");
        }
        else
        {            
            string key = Request.QueryString["key"].ToString();
            int logid = 0;
            int.TryParse(key, out logid);
            WebfaresDataContext context = new WebfaresDataContext();
            var logData = context.tr_login_logs.Where(w => w.id.Equals(logid)).FirstOrDefault();
            string username = string.Empty;
            if (logData != null)
            {
                username = logData.uid;
            }
            context.Dispose();
            checkAuthentication(username, string.Empty, false);            
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        checkAuthentication(txtUsername.Text, txtPassword.Text,true);
    }
}