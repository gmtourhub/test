using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    GMDataCenterDataContext context;
    public string urlDefault = string.Empty;
    public bool isShowComment = true;
    #region Methods
    void initialize()
    {        
        displayCategories();
        displayBlogContent();    
    }
    void displayCategories()
    {
        GMDataCenterDataContext context = new GMDataCenterDataContext();
        bool isPrivate = !Blog.Session.IsExpired();
        string user = Blog.Session.GetCurrentUserID();
        var categotyList = (from blog in context.Application_Blogs
                    from cate in context.Application_Blog_SelectCategories
                    where blog.BlogID.Equals(cate.BlogID)
                    && (blog.BlogStatus.ToLower().Equals("published") || blog.CreateBy.Equals(user))
                    && (blog.IsPrivate.Equals(isPrivate) || isPrivate)
                    && (blog.BlogSchedule.ToLower().Equals("automatic") || (blog.BlogSchedule.ToLower().Equals("schedule") && blog.BlogScheduleDate.Value >= DateTime.Now))
                    select cate.CategoryID).Distinct().ToList();
        StringBuilder sb = new StringBuilder();
        if (categotyList.Count() > 0)
        {
            if(Request.Url.Host.Contains("gmtour.com"))
                urlDefault = "http://www.gmtour.com/2Box/";
            else
                urlDefault = "http://localhost:30748/";//33269,2045
            
            var data = context.Application_Blog_Categories.Where(w => categotyList.Contains(w.CategoryID));
            if (data.Count() > 0)
            {
                sb.Append("<ul class=\"display-categories\">");
                foreach (var item in data.OrderBy(o => o.CategoryName))
                {
                    sb.Append(string.Format("<li><a href=\"{2}Category/{1}\"><span>›</span>{0}</a></li>", item.CategoryName, item.CategoryName.Replace(' ', '-'), urlDefault));
                }
                sb.Append("</ul>");
            }
        }
        ltrCategories.Text = sb.ToString();
        context.Dispose();
    }
    void displayBlogContent()
    {
        context = new GMDataCenterDataContext();
        bool isLogin = !Blog.Session.IsExpired();
        string user = Blog.Session.GetCurrentUserID();
        List<Blog.Structure.DisplayBlog> blogData = (from blog in context.Application_Blogs
                                                    from grant in context.Application_Blog_Grants
                                                    where blog.CreateBy.Equals(grant.UserID)
                                                    && (blog.IsPrivate.Equals(isLogin) || isLogin)
                                                    && (blog.BlogStatus.ToLower().Equals("published") || blog.CreateBy.Equals(user))
                                                    orderby blog.CreateDate descending
                                                    select new Blog.Structure.DisplayBlog { 
                                                        BlogID = blog.BlogID,
                                                        BlogKey = blog.BlogKey.ToString(),
                                                        BlogName = blog.BlogName,
                                                        BlogContent = blog.BlogContent,
                                                        PostDate = blog.CreateDate,
                                                        CreateBy = blog.CreateBy,
                                                        CreateByName = grant.BlogerName,
                                                        FullContent = string.Format("{0} {1}",blog.BlogName,blog.BlogContent)
                                                    }).ToList();
        Panel searchContent = (Panel)this.Master.FindControl("panelSearch");
        if (Request.QueryString["request"] != null)
        {            
            string request = Request.QueryString["request"].ToString().ToLower();
            string[] segments = request.Split('/');
            string[] categories = new string[] { "post","category","preview" };
            if (categories.Contains(segments[0]))
            {
                string postName = segments.Count() > 1 ? segments[1] : string.Empty;
                if (!request.StartsWith("preview/")) postName = postName.Replace("-", string.Empty);
                if (segments[0] == "post")
                {
                    blogData = blogData.Where(w => w.BlogKey.ToString().ToLower().Replace("-", string.Empty).Equals(postName)).ToList();
                    hdfFullContent.Value = "Y";
                    searchContent.Visible = false;
                }
                else if (segments[0] == "category")
                {                    
                    var blogIDs = (from cate in context.Application_Blog_Categories
                                from selectCate in context.Application_Blog_SelectCategories                                
                                where cate.CategoryID.Equals(selectCate.CategoryID)
                                && cate.CategoryName.ToLower().Replace(".", string.Empty).Replace(" ", string.Empty).Replace("/", string.Empty).StartsWith(postName)
                                select selectCate.BlogID).ToList();
                    blogData = blogData.Where(w=>blogIDs.Contains(w.BlogID)).ToList();
                    isShowComment = false;
                    searchContent.Visible = false;
                }
                else if (segments[0] == "preview")
                {
                    System.Threading.Thread.Sleep(500);
                    blogData = blogData.Where(w => w.BlogKey.ToLower().Equals(postName.ToLower())).ToList();
                    hdfFullContent.Value = "Y";
                    searchContent.Visible = false;
                }
            }
        }
        else
        {
            isShowComment = false; 
        }

        if (isShowComment) isShowComment = hdfFullContent.Value.Equals("Y") && !Blog.Session.IsExpired();        

        if (hdfFullContent.Value.Equals("N") && searchContent.Visible && Request.QueryString["search"]!=null)
        {
            TextBox txtSearch = (TextBox)this.Master.FindControl("txtSearchContent");
            string search = Request.QueryString["search"].ToString().Replace("-", " ").Trim().Replace("search/",string.Empty);
            if (!string.IsNullOrEmpty(search)) { blogData = blogData.Where(w => w.FullContent.ToLower().Contains(search.ToLower())).ToList(); txtSearch.Text = search; }
        }

        dtlBlog.DataSource = blogData;
        dtlBlog.DataBind();
    }    
    [WebMethod]
    public static string PostComment(string BlogKey,string Comment)
    {
        if (Blog.Session.IsExpired()) return "Session Expire.";
        string _return = "Failed.";
        GMDataCenterDataContext context = new GMDataCenterDataContext();
        var blog = context.Application_Blogs.Where(w => w.BlogKey.ToString().ToLower().Equals(BlogKey.ToString())).FirstOrDefault();
        if (blog != null)
        {
            Application_Blog_Comment create = new Application_Blog_Comment();
            create.BlogID = blog.BlogID;
            create.Comment = Comment.Trim().Length > 500 ? Comment.Trim().Substring(0, 500) : Comment.Trim();
            create.CommentType = "Spam";
            create.UserID = Blog.Session.GetCurrentUserID();
            create.CommentDate = DateTime.Now;
            context.Application_Blog_Comments.InsertOnSubmit(create);
            context.SubmitChanges();
            _return = "Success.";
        }
        context.Dispose();
        return _return;
    }
    [WebMethod]
    public static string ManagementButton(int CommentID)
    {
        string user = Blog.Session.GetCurrentUserID();
        StringBuilder sb = new StringBuilder();
        GMDataCenterDataContext context = new GMDataCenterDataContext();
        var data = (from comment in context.Application_Blog_Comments
                   from blog in context.Application_Blogs
                   where comment.BlogID.Equals(blog.BlogID)
                   && comment.CommentID.Equals(CommentID)
                   select new {
                       Comment = comment,
                       Blog = blog
                    }).FirstOrDefault();
        if (data != null)
        {
            if (data.Blog.CreateBy.Equals(user))
            {
                string _type = data.Comment.CommentType.ToLower().Equals("spam") ? "Publish" : "Spam";
                sb.Append(string.Format("<span class=\"set-{1}\">{0}</span>",_type,_type.ToLower()));
                if(data.Comment.CommentType.ToLower().Equals("spam")) sb.Append("<span class=\"set-delete\">X</span>");
            }
        }
        context.Dispose();
        return sb.ToString();
    }
    [WebMethod]
    public static string SetCommentType(int CommentID,string CommentType)
    {
        GMDataCenterDataContext context = new GMDataCenterDataContext();
        var update = context.Application_Blog_Comments.Where(w => w.CommentID.Equals(CommentID)).FirstOrDefault();
        if (update != null)
        {
            CommentType = CommentType.ToLower().Equals("spam") ? CommentType : "Published";
            update.CommentType = System.Globalization.CultureInfo.CurrentUICulture.TextInfo.ToTitleCase(CommentType);
            context.SubmitChanges();
        }
        context.Dispose();
        return ManagementButton(CommentID);
    }
    [WebMethod]
    public static bool DeleteComment(int CommentID)
    {
        GMDataCenterDataContext context = new GMDataCenterDataContext();
        bool _response = false;
        var delete = context.Application_Blog_Comments.Where(w => w.CommentID.Equals(CommentID)).FirstOrDefault();
        if (delete != null)
        {
            context.Application_Blog_Comments.DeleteOnSubmit(delete);
            context.SubmitChanges();
            _response = true;
        }
        context.Dispose();
        return _response;
    }
    [WebMethod]
    public static bool SessionExpired()
    {
        return Blog.Session.IsExpired();
    }
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Master.Page.Form.Action = "http://www.gmtour.com/2Box";
        if (!Page.IsPostBack) initialize();
        else
        {
            Panel searchContent = (Panel)this.Master.FindControl("panelSearch");
            TextBox txtSearch = (TextBox)this.Master.FindControl("txtSearchContent");
            string searchText = txtSearch.Text.Replace(" ","-");
            if (searchContent.Visible && !string.IsNullOrEmpty(searchText.Trim())) Response.Redirect("http://www.gmtour.com/2Box/search/" + searchText);
            else if (string.IsNullOrEmpty(searchText.Trim())) Response.Redirect("http://www.gmtour.com/2Box/");
        }
    }
    protected void dtlBlog_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        ((Panel)e.Item.FindControl("panelDateofPostBlank")).Visible = isShowComment;
        ((Panel)e.Item.FindControl("panelComment")).Visible = isShowComment;
        
        if(Blog.Session.IsExpired()){
            ((Panel)e.Item.FindControl("panelPostBy")).Visible = false;
        }
        else
        {
            ((Panel)e.Item.FindControl("panelPostBy")).Visible = true;

        }           
        if (hdfFullContent.Value.Equals("Y")) ((Panel)e.Item.FindControl("panelViewLog")).Visible = false;
        else
        {
            Blog.Structure.DisplayBlog item = (Blog.Structure.DisplayBlog)e.Item.DataItem;
            int viewLogCount = context.Application_Blog_ViewLogs.Where(w => w.BlogID.Equals(item.BlogID)).Count();
            int commentCount = 0;
            if (item.CreateBy.Equals(Blog.Session.GetCurrentUserID()))
                commentCount = context.Application_Blog_Comments.Where(w => w.BlogID.Equals(item.BlogID)).Count();
            else
                commentCount = context.Application_Blog_Comments.Where(w => w.BlogID.Equals(item.BlogID) && (w.CommentType.Equals("Published") || w.UserID.Equals(Blog.Session.GetCurrentUserID()))).Count();
            ((Label)e.Item.FindControl("lblViewCount")).Text = viewLogCount.ToString("#,##0");
            ((Label)e.Item.FindControl("lblCommentCount")).Text = commentCount.ToString("#,##0");
        }
    }
    protected void lnkLogout_Click(object sender, EventArgs e)
    {
        Session.RemoveAll();
        Session.Abandon();
        //Response.Redirect("http://www.gmtour.com/2Box/");
        Response.Redirect("http://www.google.co.th");
    }
}