using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    public bool isHomePage = true;
    #region Methods
    void initialize()
    {
        if (Blog.Session.IsExpired())
        {
            panelUsername.Visible = false;
            lnkLogout.Visible = false;
        }
        else
        {
            lnkLogin.Visible = false;
            lnkLogout.Visible = true;
            var user = Blog.Database.AllUserProfile(Blog.Session.GetCurrentUserID());
            lnkManagement.Visible = user.IsAdmin;
            lblUsername.Text = user.UserName;            
        }
        if (Request.Url.Host.Contains("gmtour.com"))
            lnkLogin.Attributes.Add("href","http://www.gmtour.com/2Box/Login.aspx");
        else
            lnkLogin.Attributes.Add("href", "http://localhost:30748/Login.aspx");
        lnkHome.Attributes.Add("href", "http://www.gmtour.com/2Box/");
        lnkManagement.Attributes.Add("href", "http://www.gmtour.com/2Box/Management/Blogger.aspx");
        lnkLogout.Attributes.Add("href", "#");

        if (Request.QueryString["request"] != null && Request.QueryString["request"].ToString().ToLower().StartsWith("preview/"))
        {
            lnkHome.Visible = false;
            lnkLogin.Visible = false;
            lnkLogout.Visible = false;
        }        
        
        imageLogo.ImageUrl = "../images/2Box_Logo.png";

        isHomePage = Request.QueryString["request"] == null;        

        if (isHomePage)
        {
            Literal hControl = new Literal();
            hControl.Text = "<script type=\"text/javascript\" src=\"script/jquery-1.11.1.min.js\"></script>";
            hControl.Text += "<script type=\"text/javascript\" src=\"script/jquery-ui.js\"></script>";
            hControl.Text += "<script type=\"text/javascript\" src=\"script/blog.js\"></script>";
            this.Page.Header.Controls.Add(hControl);
        }
    }
    void blogAuthenticate()
    {
        
    }
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Page.Form.Action = "Default.aspx";
        if (!Page.IsPostBack) initialize();        
    }
    protected void btnSumit_Click(object sender, EventArgs e)
    {
        this.blogAuthenticate();
    }
    protected void lnkLogout_Click(object sender, EventArgs e)
    {
        Session.RemoveAll();
        Session.Abandon();
        //Response.Redirect("http://www.gmtour.com/2Box/");
        Response.Redirect("http://www.google.co.th");
    }
}
