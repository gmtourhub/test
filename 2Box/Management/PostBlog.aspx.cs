using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Management_PostBlog : System.Web.UI.Page
{
    public string htmlContent = string.Empty;
    public string previewUrl = string.Empty;
    #region Methods
    void initialize()
    {
        txtSetDateAndTime.Text = DateTime.Now.ToString("dd/MM/yyyy 00:00");
        rdbAutomatic.Checked = true;        
        bindCategories();
        displayBlogData();
    }
    void bindCategories()
    {
        GMDataCenterDataContext context = new GMDataCenterDataContext();
        var categories = context.Application_Blog_Categories.OrderBy(o => o.CategoryName);
        chkCategories.DataSource = categories.Select(s=>s.CategoryName).ToList();
        chkCategories.DataBind();
        foreach (ListItem item in chkCategories.Items)
        {                        
            item.Attributes.Add("CategoryID",categories.Where(w=>w.CategoryName.Equals(item.Text)).FirstOrDefault().CategoryID.ToString());
        }
        context.Dispose();
    }
    void displayBlogData()
    {
        if (Request.QueryString["key"] != null)
        {
            string key = Request.QueryString["key"].ToString();
            GMDataCenterDataContext context = new GMDataCenterDataContext();
            var data = context.Application_Blogs.Where(w => w.BlogKey.ToString().ToLower().Equals(key.ToLower())).FirstOrDefault();
            if (data != null)
            {
                txtPostTitle.Text = data.BlogName;
                htmlContent = data.BlogContent;
                hdfBlogKey.Value = data.BlogKey.ToString();
                previewUrl = string.Format("../Preview/{0}",data.BlogKey.ToString());

                var categories = context.Application_Blog_SelectCategories.Where(w => w.BlogID.Equals(data.BlogID));
                foreach (ListItem chk in chkCategories.Items)
                {
                    int categoryID = 0;
                    int.TryParse(chk.Attributes["CategoryID"], out categoryID);
                    if (categories.Where(w => w.CategoryID.Equals(categoryID)).Count() > 0) chk.Selected = true;
                }
                chkIsPrivate.Checked = data.IsPrivate;
                rdbAutomatic.Checked = data.BlogSchedule.ToLower().Equals("automatic");
                rdbSetDateAndTime.Checked = !rdbAutomatic.Checked;
                if (rdbAutomatic.Checked) txtSetDateAndTime.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
                else txtSetDateAndTime.Text = data.BlogScheduleDate.HasValue ? data.BlogScheduleDate.Value.ToString("dd/MM/yyyy HH:mm") : DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            }
            else
            {
                this.Master.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Data not found.');window.location='Blogger.aspx';", true);
            }
            context.Dispose();
        }
    }
    [WebMethod]
    public static Blog.Structure.PostBlogResponse PostBlog(Blog.Structure.Blog BlogParam)
    {
        Blog.Structure.PostBlogResponse postBlogResponse = new Blog.Structure.PostBlogResponse();
        if (Blog.Session.IsExpired()) {
            postBlogResponse.Status = false;
            postBlogResponse.Message = "Session Expired.";
            return postBlogResponse;
        }

        try
        {
            bool isEdit = string.IsNullOrEmpty(BlogParam.BlogKey) ? false : true;
            GMDataCenterDataContext context = new GMDataCenterDataContext();
            int blogID = 0;
            string blogStatus = BlogParam.BlogStatus.ToLower().Equals("save") ? "Draft" : "Published";
            if (isEdit) // edit blog
            {
                var update = context.Application_Blogs.Where(w => w.BlogKey.Equals(BlogParam.BlogKey)).FirstOrDefault();
                if (update != null)
                {
                    blogID = update.BlogID;
                    update.BlogName = BlogParam.BlogName;
                    update.BlogContent = BlogParam.BlogContent;
                    if(update.BlogStatus.Equals("Draft")) update.BlogStatus = blogStatus;
                    update.BlogSchedule = BlogParam.BlogSchedule;
                    update.IsPrivate = BlogParam.IsPrivate;
                    if (BlogParam.BlogSchedule.ToLower().Equals("automatic"))
                        update.BlogScheduleDate = null;
                    else
                        update.BlogScheduleDate = BlogParam.BlogScheduleDate.Value;
                    update.UpdateBy = Blog.Session.GetCurrentUserID();
                    update.UpdateDate = DateTime.Now;
                }

                // delete categories
                var delete = context.Application_Blog_SelectCategories.Where(w => w.BlogID.Equals(update.BlogID));
                if (delete.Count() > 0) context.Application_Blog_SelectCategories.DeleteAllOnSubmit(delete);
            }
            else // add blog
            {
                Application_Blog create = new Application_Blog();
                create.BlogName = BlogParam.BlogName;
                create.BlogContent = BlogParam.BlogContent;
                create.BlogStatus = blogStatus;
                create.BlogSchedule = BlogParam.BlogSchedule;
                create.IsPrivate = BlogParam.IsPrivate;
                if (BlogParam.BlogSchedule.ToLower().Equals("automatic"))
                    create.BlogScheduleDate = null;
                else
                    create.BlogScheduleDate = BlogParam.BlogScheduleDate.Value;
                create.CreateBy = Blog.Session.GetCurrentUserID();
                create.CreateDate = DateTime.Now;
                context.Application_Blogs.InsertOnSubmit(create);
                context.SubmitChanges();
                blogID = create.BlogID;
            }

            // add categories
            if (BlogParam.Categories != null)
            {
                List<Application_Blog_SelectCategory> categoryList = new List<Application_Blog_SelectCategory>();
                foreach (var item in BlogParam.Categories)
                {
                    categoryList.Add(new Application_Blog_SelectCategory()
                    {
                        BlogID = blogID,
                        CategoryID = item.CategoryID
                    });
                }
                if (categoryList.Count > 0) context.Application_Blog_SelectCategories.InsertAllOnSubmit(categoryList);
            }

            context.SubmitChanges();
            context.Dispose();
            postBlogResponse.Status = true;
            postBlogResponse.Message = "Success";
        }
        catch (Exception ex)
        {
            postBlogResponse.Status = false;
            postBlogResponse.Message = ex.Message;
        }
        return postBlogResponse;
    }
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack) initialize();
    }
}