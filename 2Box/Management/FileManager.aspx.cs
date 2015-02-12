using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using System.Web.Services;

public partial class Management_FileManager : System.Web.UI.Page
{
    #region Methods
    void displayServerFiles()
    {
        string defaultPath = Server.MapPath("~/Uploads/");
        string absolutPath = "../Uploads/";
        string [] extensions = new string[]{".jpg",".bmp",".gif",".png"};
        StringBuilder sb = new StringBuilder();
        foreach (string file in Directory.GetFiles(defaultPath))
        {
            sb.Append("<span class=\"image-wrapper\">");
            sb.Append("<span class=\"delete\">x</span>");
            string[] fileSegments = file.Split('\\');
            string fileName = fileSegments[fileSegments.Length-1];
            sb.Append(string.Format("<img src=\"{0}{1}\"><br/>", absolutPath, fileName));           
            sb.Append(string.Format("<span class=\"file-name\">{0}</span>",fileName));
            sb.Append("</span>");
        }
        ltrImage.Text = sb.ToString();
    }
[WebMethod]
    public static string DeleteFile(string FileName)
    {
        string file = HttpContext.Current.Server.MapPath("../uploads/" + FileName);
        File.Delete(file);
        return string.Empty;
    }
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        bool isExpire = false;
        if (Blog.Session.IsExpired())
        {
            isExpire=true;
        }
        else
        {
            string user = Blog.Session.GetCurrentUserID();
            if (string.IsNullOrEmpty(Blog.Database.UserProfile(user).UserID))
            {
                isExpire = true;
            }
        }
        if (isExpire)
        {
            string message = "Authentication failed or Session expire.";
            Response.Redirect("../Message/" + message.Replace(" ", "-"));
        }
        if (!Page.IsPostBack) displayServerFiles();
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (fileUpload.HasFiles)
        {
            string defaultPath = Server.MapPath("~/Uploads/");            
            fileUpload.SaveAs(defaultPath+fileUpload.FileName);
            Response.Redirect(Request.Url.AbsoluteUri);
        }
    }
}