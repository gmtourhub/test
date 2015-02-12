using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Management_Blogger : System.Web.UI.Page
{
    #region Methods
    [WebMethod]
    public static string GenerateManagementBlogMenus()
    {
        GMDataCenterDataContext context = new GMDataCenterDataContext();
        StringBuilder script = new StringBuilder();
        string user = Blog.Session.GetCurrentUserID();
        script.Append("<div class=\"management-menu\">");

        script.Append("<a class=\"posts-header posts\" href=\"#posts-all\">Posts</a>");
        int count = context.Application_Blogs.Where(w => w.CreateBy.Equals(user)).Count();
        script.Append(string.Format("<a href=\"#posts-all\">All ({0})</a>",count.ToString()));
        count = context.Application_Blogs.Where(w => w.CreateBy.Equals(user) && w.BlogStatus.Equals("Draft")).Count();
        script.Append(string.Format("<a href=\"#posts-draft\">Draft ({0})</a>",count.ToString()));
        count = context.Application_Blogs.Where(w => w.CreateBy.Equals(user) && w.BlogStatus.Equals("Published")).Count();
        script.Append(string.Format("<a href=\"#posts-published\">Published ({0})</a>",count.ToString()));

        script.Append("<a class=\"privacy-header privacy\" href=\"#privacy-private\">Privacy</a>");
        count = context.Application_Blogs.Where(w => w.CreateBy.Equals(user) && w.IsPrivate.Equals(true)).Count();
        script.Append(string.Format("<a href=\"#privacy-private\">Private ({0})</a>", count.ToString()));

        count = 0;
        script.Append("<a class=\"comment-header comment\" href=\"#comment-all\">Comments</a>");
        count = (from cb in context.Application_Blog_Comments
                     from b in context.Application_Blogs
                     where cb.BlogID.Equals(b.BlogID)
                     && b.CreateBy.Equals(user)
                     select cb).Count();
        script.Append(string.Format("<a href=\"#comment-all\">All ({0})</a>", count.ToString()));
        count = (from cb in context.Application_Blog_Comments
                     from b in context.Application_Blogs
                     where cb.BlogID.Equals(b.BlogID)
                     && b.CreateBy.Equals(user)
                     && cb.CommentType.Equals("Spam")
                     select cb).Count();
        script.Append(string.Format("<a href=\"#comment-spam\">Spam ({0})</a>", count.ToString()));
        count = (from cb in context.Application_Blog_Comments
                 from b in context.Application_Blogs
                 where cb.BlogID.Equals(b.BlogID)
                 && b.CreateBy.Equals(user)
                 && cb.CommentType.Equals("Published")
                 select cb).Count();
        script.Append(string.Format("<a href=\"#comment-published\">Published ({0})</a>", count.ToString()));

        script.Append("</div>");
        context.Dispose();
        return script.ToString();
    }
    [WebMethod]
    public static bool DeleteBlog(string BlogKeys)
    {
        bool _return = false;
        GMDataCenterDataContext context = new GMDataCenterDataContext();
        var blogs = context.Application_Blogs.Where(w => BlogKeys.ToLower().Split(',').Contains(w.BlogKey.ToString().ToLower()));
        if (blogs.Count()>0)
        {
            // delete blogs
            context.Application_Blogs.DeleteAllOnSubmit(blogs);
            // delete selected categories
            var categories = from blog in blogs
                             from cate in context.Application_Blog_SelectCategories
                             where blog.BlogID.Equals(cate.BlogID)
                             select cate;
            if (categories.Count() > 0) context.Application_Blog_SelectCategories.DeleteAllOnSubmit(categories);
            // delete comments
            var comments = from blog in blogs
                             from comment in context.Application_Blog_Comments
                           where blog.BlogID.Equals(comment.BlogID)
                           select comment;
            if (categories.Count() > 0) context.Application_Blog_Comments.DeleteAllOnSubmit(comments);
            // delete view log
            var viewlogs = from blog in blogs
                           from viewlog in context.Application_Blog_ViewLogs
                           where blog.BlogID.Equals(viewlog.BlogID)
                           select viewlog;
            if (categories.Count() > 0) context.Application_Blog_ViewLogs.DeleteAllOnSubmit(viewlogs);

            context.SubmitChanges();
            _return = true;
        }
        context.Dispose();
        return _return;
    }
    [WebMethod]
    public static bool UpdateStatus(string BlogKeys,string Status)
    {
        Status = System.Globalization.CultureInfo.CurrentUICulture.TextInfo.ToTitleCase(Status);
        GMDataCenterDataContext context = new GMDataCenterDataContext();
        var update = context.Application_Blogs.Where(w => BlogKeys.ToLower().Split(',').Contains(w.BlogKey.ToString().ToLower()));
        if (update.Count() > 0)
        {
            foreach (var _update in update)
            {
                _update.BlogStatus = Status;
            }
            context.SubmitChanges();
        }
        context.Dispose();
        return true;
    }
    [WebMethod]
    public static bool SetCommentType(string CommentID,string CommentType)
    {
        bool _return = false;
        CommentType = System.Globalization.CultureInfo.CurrentUICulture.TextInfo.ToTitleCase(CommentType);
        GMDataCenterDataContext context = new GMDataCenterDataContext();
        var update = context.Application_Blog_Comments.Where(w => CommentID.Split(',').Contains(w.CommentID.ToString()));
        if (update.Count() > 0)
        {
            foreach (var _update in update) _update.CommentType = CommentType;
            context.SubmitChanges();
            _return = true;
        }
        context.Dispose();
        return _return;
    }
    [WebMethod]
    public static bool DeleteComment(string CommentIDs)
    {
        bool _return = false;        
        GMDataCenterDataContext context = new GMDataCenterDataContext();
        var delete = context.Application_Blog_Comments.Where(w => CommentIDs.Split(',').Contains(w.CommentID.ToString()));
        if (delete.Count() > 0)
        {
            context.Application_Blog_Comments.DeleteAllOnSubmit(delete);
            context.SubmitChanges();
            _return = true;
        }
        context.Dispose();
        return _return;
    }
    [WebMethod]
    public static bool SessionExpired()
    {
        return Blog.Session.IsExpired();
    }
    [WebMethod]
    public static bool UpdatePrivate(string BlogKeys, string Status)
    {        
        GMDataCenterDataContext context = new GMDataCenterDataContext();
        var update = context.Application_Blogs.Where(w => BlogKeys.ToLower().Split(',').Contains(w.BlogKey.ToString().ToLower()));
        if (update.Count() > 0)
        {
            foreach (var _update in update)
            {
                _update.IsPrivate = Status.ToLower().Equals("private");
            }
            context.SubmitChanges();
        }
        context.Dispose();
        return true;
    }
    void initialize()
    {
        getProfile();
        ltrMenus.Text = GenerateManagementBlogMenus();        
    }
    void getProfile()
    {
        string userID = Blog.Session.GetCurrentUserID();
        var profile = Blog.Database.UserProfile(userID);
        lblBlogName.Text = profile.UserName+"'s blogs";
    }
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack) initialize();
    }
}