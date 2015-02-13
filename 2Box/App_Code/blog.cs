using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Blog
{
    //test
    public class Database
    {
        public static bool UserAuthenticate(string uname,string pwd,bool checkPassword)
        {
            TicketVendorDataContext context = new TicketVendorDataContext();
            var authen = context.mt_users.Where(w => w.uid.ToLower().Equals(uname.ToLower()) && (w.pwd.Equals(pwd) || !checkPassword) && w.user_status.Value.Equals('A'));
            bool _return = authen.Count()>0;
            context.Dispose();
            return _return;
        }
        public static Structure.UserProfile UserProfile(string UserID)
        {
            GMDataCenterDataContext context = new GMDataCenterDataContext();
            Structure.UserProfile _return = new Structure.UserProfile();
            var user = context.Application_Blog_Grants.Where(w => w.UserID.ToLower().Equals(UserID.ToLower())).FirstOrDefault();
            if (user != null)
            {
                _return.UserID = user.UserID.ToUpper();
                _return.UserName = user.BlogerName.ToUpper();
                _return.IsAdmin = context.Application_Blog_Grants.Where(w => w.UserID.ToLower().Equals(UserID.ToLower())).Count() > 0;
            }
            context.Dispose();
            return _return;
        }
        public static Structure.UserProfile AllUserProfile(string UserID)
        {
            TicketVendorDataContext context = new TicketVendorDataContext();
            GMDataCenterDataContext dataCenterContext = new GMDataCenterDataContext();
            Structure.UserProfile _return = new Structure.UserProfile();
            var user = context.mt_users.Where(w => w.uid.ToLower().Equals(UserID.ToLower())).FirstOrDefault();
            if (user != null)
            {
                _return.UserID = user.uid.ToUpper();
                _return.UserName = user.uname.ToUpper();
                _return.IsAdmin = dataCenterContext.Application_Blog_Grants.Where(w => w.UserID.ToLower().Equals(UserID.ToLower())).Count() > 0;
            }
            dataCenterContext.Dispose();
            context.Dispose();
            return _return;
        }         
    }
    public class Session
    {
        public static bool IsExpired()
        {
            return HttpContext.Current.Session["userid"] == null;
        }
        public static string GetCurrentUserID()
        {
            return HttpContext.Current.Session["userid"] == null ? string.Empty : HttpContext.Current.Session["userid"].ToString();
        }
    }

    namespace Structure
    {
        public class UserProfile
        {
            public string UserID { get; set; }
            public string UserName { get; set; }
            public bool IsAdmin { get; set; }
        }
        public class Blog
        {
            public int BlogID { get; set; }
            public string BlogKey { get; set; }
            public string BlogName { get; set; }
            public string BlogContent { get; set; }
            public string BlogStatus { get; set; }
            public string BlogSchedule { get; set; }
            public bool IsPrivate { get; set; }
            string _blogScheduleDateString;
            public string BlogScheduleDateString 
            { 
                get {
                    return _blogScheduleDateString;
                } 
                set{
                    _blogScheduleDateString = value;
                    DateTime.TryParseExact(_blogScheduleDateString, "dd/MM/yyyy HH:mm", new CultureInfo("en-US"), DateTimeStyles.None, out _blogScheduleDate);                       
                }
            }
            DateTime _blogScheduleDate;
            public DateTime? BlogScheduleDate { get { return _blogScheduleDate; } }
            public string CreateBy { get; set; }
            public string CreateDate { get; set; }
            public string UpdateBy { get; set; }
            public DateTime? UpdateDate { get; set; }
            public List<Category> Categories { get; set; }
        }
        public class Category
        {
            public int CategoryID { get; set; }
            public string CategoryName { get; set; }
        }
        public class PostBlogResponse
        {
            public bool Status { get; set; }
            public string Message { get; set; }
        }
        public class DisplayBlog
        {
            public int BlogID { get; set; }
            public string BlogKey { get; set; }
            public string BlogName { get; set; }
            public string BlogContent { get; set; }
            public string CreateBy { get; set; }
            public string CreateByName { get;set;}
            public DateTime PostDate { get; set; }
            public string FullContent { get; set; }
        }
        public class DisplayComment
        {
            public int CommentID { get; set; }
            public int BlogID { get; set; }
            public string BlogKey { get; set; }
            public string Comment { get; set; }
            public string CommentType { get; set; }
            public string UserID { get; set; }
            public string Username { get; set; }
            public string BlogCreateBy { get; set; }
            public DateTime CommentDate { get; set; }
        }
    }
}