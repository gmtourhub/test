using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DisplayComment : System.Web.UI.Page
{
    public string userID = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Blog.Session.IsExpired()) return;
        GMDataCenterDataContext context = new GMDataCenterDataContext();
        TicketVendorDataContext tvdContext = new TicketVendorDataContext();
        var users = tvdContext.mt_users.ToList();        
        string key = Request.QueryString["key"] != null ? Request.QueryString["key"].ToString() : string.Empty;
        userID = Blog.Session.GetCurrentUserID();
        List<Blog.Structure.DisplayComment> comments = (from comment in context.Application_Blog_Comments.ToList()
                                                       from blog in context.Application_Blogs.ToList()
                                                       from user in users
                                                       where comment.BlogID.Equals(blog.BlogID)
                                                       && comment.UserID.Equals(user.uid)
                                                       && blog.BlogKey.ToString().ToLower().Equals(key.ToLower())
                                                       && (comment.CommentType.Equals("Published") || blog.CreateBy.Equals(userID) || comment.UserID.Equals(userID))
                                                       select new Blog.Structure.DisplayComment()
                                                       {
                                                           CommentID = comment.CommentID,
                                                           BlogID = comment.BlogID,
                                                           BlogKey = blog.BlogKey.ToString(),
                                                           Comment = comment.Comment,
                                                           UserID = comment.UserID,
                                                           Username = user.uname.ToUpper(),
                                                           CommentType = comment.CommentType,
                                                           CommentDate = comment.CommentDate,
                                                           BlogCreateBy = blog.CreateBy
                                                       }).ToList();
        dtlComment.DataSource = comments;
        dtlComment.DataBind();
        context.Dispose();
        tvdContext.Dispose();
    }
    protected void dtlComment_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        Blog.Structure.DisplayComment item = (Blog.Structure.DisplayComment)e.Item.DataItem;
        if (item.UserID.ToLower().Equals(userID.ToLower()) && !item.BlogCreateBy.ToLower().Equals(userID.ToLower()) && item.CommentType.Equals("Spam"))
        {
            ((Panel)e.Item.FindControl("panelCommentBox")).Attributes.Add("this_spam","true");
        }
    }
}