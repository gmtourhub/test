using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class masterpage_Management : System.Web.UI.MasterPage
{
    #region Methods
    void initialize()
    {        
        getProfile();
    }
    void checkAuthenticate()
    {
        if (Blog.Session.IsExpired())
        {
            string message = "Authentication failed or Session expire.";
            Response.Redirect("../Message/"+message.Replace(" ","-"));
        }
        else
        {
            if(string.IsNullOrEmpty(Blog.Database.UserProfile(Session["userid"].ToString()).UserID))
                Response.Redirect("http://www.gmtour.com/2Box/");
        }
    }
    void getProfile()
    {
        string userID = Blog.Session.GetCurrentUserID();
        var profile = Blog.Database.UserProfile(userID);
        lblUsername.Text = profile.UserName;
    }
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        checkAuthenticate();
        if (!Page.IsPostBack) initialize();
    }
    protected void lnkLogout_Click(object sender, EventArgs e)
    {
        Session.RemoveAll();
        Session.Abandon();
        Response.Redirect("../");
    }
}