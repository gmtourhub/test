using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Management_Message : System.Web.UI.Page
{
    #region Methods
    void initialize()
    {
        if (Request.QueryString["message"] != null) lblMessage.Text = Request.QueryString["message"].Replace("-", " ");
    }
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack) this.initialize();
        
    }
}