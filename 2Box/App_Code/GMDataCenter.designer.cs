﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.0
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;



[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="GMDataCenter")]
public partial class GMDataCenterDataContext : System.Data.Linq.DataContext
{
	
	private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
	
  #region Extensibility Method Definitions
  partial void OnCreated();
  partial void InsertApplication_Blog_Category(Application_Blog_Category instance);
  partial void UpdateApplication_Blog_Category(Application_Blog_Category instance);
  partial void DeleteApplication_Blog_Category(Application_Blog_Category instance);
  partial void InsertApplication_Blog_SelectCategory(Application_Blog_SelectCategory instance);
  partial void UpdateApplication_Blog_SelectCategory(Application_Blog_SelectCategory instance);
  partial void DeleteApplication_Blog_SelectCategory(Application_Blog_SelectCategory instance);
  partial void InsertApplication_Blog_Grant(Application_Blog_Grant instance);
  partial void UpdateApplication_Blog_Grant(Application_Blog_Grant instance);
  partial void DeleteApplication_Blog_Grant(Application_Blog_Grant instance);
  partial void InsertApplication_Blog_Comment(Application_Blog_Comment instance);
  partial void UpdateApplication_Blog_Comment(Application_Blog_Comment instance);
  partial void DeleteApplication_Blog_Comment(Application_Blog_Comment instance);
  partial void InsertApplication_Blog(Application_Blog instance);
  partial void UpdateApplication_Blog(Application_Blog instance);
  partial void DeleteApplication_Blog(Application_Blog instance);
  partial void InsertApplication_Blog_ViewLog(Application_Blog_ViewLog instance);
  partial void UpdateApplication_Blog_ViewLog(Application_Blog_ViewLog instance);
  partial void DeleteApplication_Blog_ViewLog(Application_Blog_ViewLog instance);
  #endregion
	
	public GMDataCenterDataContext() : 
			base(global::System.Configuration.ConfigurationManager.ConnectionStrings["GMDataCenterConnectionString"].ConnectionString, mappingSource)
	{
		OnCreated();
	}
	
	public GMDataCenterDataContext(string connection) : 
			base(connection, mappingSource)
	{
		OnCreated();
	}
	
	public GMDataCenterDataContext(System.Data.IDbConnection connection) : 
			base(connection, mappingSource)
	{
		OnCreated();
	}
	
	public GMDataCenterDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
			base(connection, mappingSource)
	{
		OnCreated();
	}
	
	public GMDataCenterDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
			base(connection, mappingSource)
	{
		OnCreated();
	}
	
	public System.Data.Linq.Table<Application_Blog_Category> Application_Blog_Categories
	{
		get
		{
			return this.GetTable<Application_Blog_Category>();
		}
	}
	
	public System.Data.Linq.Table<Application_Blog_SelectCategory> Application_Blog_SelectCategories
	{
		get
		{
			return this.GetTable<Application_Blog_SelectCategory>();
		}
	}
	
	public System.Data.Linq.Table<Application_Blog_Grant> Application_Blog_Grants
	{
		get
		{
			return this.GetTable<Application_Blog_Grant>();
		}
	}
	
	public System.Data.Linq.Table<Application_Blog_Comment> Application_Blog_Comments
	{
		get
		{
			return this.GetTable<Application_Blog_Comment>();
		}
	}
	
	public System.Data.Linq.Table<Application_Blog> Application_Blogs
	{
		get
		{
			return this.GetTable<Application_Blog>();
		}
	}
	
	public System.Data.Linq.Table<Application_Blog_ViewLog> Application_Blog_ViewLogs
	{
		get
		{
			return this.GetTable<Application_Blog_ViewLog>();
		}
	}
}

[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Application_Blog_Category")]
public partial class Application_Blog_Category : INotifyPropertyChanging, INotifyPropertyChanged
{
	
	private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
	
	private int _CategoryID;
	
	private string _CategoryName;
	
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnCategoryIDChanging(int value);
    partial void OnCategoryIDChanged();
    partial void OnCategoryNameChanging(string value);
    partial void OnCategoryNameChanged();
    #endregion
	
	public Application_Blog_Category()
	{
		OnCreated();
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CategoryID", DbType="Int NOT NULL", IsPrimaryKey=true)]
	public int CategoryID
	{
		get
		{
			return this._CategoryID;
		}
		set
		{
			if ((this._CategoryID != value))
			{
				this.OnCategoryIDChanging(value);
				this.SendPropertyChanging();
				this._CategoryID = value;
				this.SendPropertyChanged("CategoryID");
				this.OnCategoryIDChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CategoryName", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
	public string CategoryName
	{
		get
		{
			return this._CategoryName;
		}
		set
		{
			if ((this._CategoryName != value))
			{
				this.OnCategoryNameChanging(value);
				this.SendPropertyChanging();
				this._CategoryName = value;
				this.SendPropertyChanged("CategoryName");
				this.OnCategoryNameChanged();
			}
		}
	}
	
	public event PropertyChangingEventHandler PropertyChanging;
	
	public event PropertyChangedEventHandler PropertyChanged;
	
	protected virtual void SendPropertyChanging()
	{
		if ((this.PropertyChanging != null))
		{
			this.PropertyChanging(this, emptyChangingEventArgs);
		}
	}
	
	protected virtual void SendPropertyChanged(String propertyName)
	{
		if ((this.PropertyChanged != null))
		{
			this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}

[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Application_Blog_SelectCategory")]
public partial class Application_Blog_SelectCategory : INotifyPropertyChanging, INotifyPropertyChanged
{
	
	private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
	
	private int _ID;
	
	private int _BlogID;
	
	private int _CategoryID;
	
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIDChanging(int value);
    partial void OnIDChanged();
    partial void OnBlogIDChanging(int value);
    partial void OnBlogIDChanged();
    partial void OnCategoryIDChanging(int value);
    partial void OnCategoryIDChanged();
    #endregion
	
	public Application_Blog_SelectCategory()
	{
		OnCreated();
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
	public int ID
	{
		get
		{
			return this._ID;
		}
		set
		{
			if ((this._ID != value))
			{
				this.OnIDChanging(value);
				this.SendPropertyChanging();
				this._ID = value;
				this.SendPropertyChanged("ID");
				this.OnIDChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_BlogID", DbType="Int NOT NULL")]
	public int BlogID
	{
		get
		{
			return this._BlogID;
		}
		set
		{
			if ((this._BlogID != value))
			{
				this.OnBlogIDChanging(value);
				this.SendPropertyChanging();
				this._BlogID = value;
				this.SendPropertyChanged("BlogID");
				this.OnBlogIDChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CategoryID", DbType="Int NOT NULL")]
	public int CategoryID
	{
		get
		{
			return this._CategoryID;
		}
		set
		{
			if ((this._CategoryID != value))
			{
				this.OnCategoryIDChanging(value);
				this.SendPropertyChanging();
				this._CategoryID = value;
				this.SendPropertyChanged("CategoryID");
				this.OnCategoryIDChanged();
			}
		}
	}
	
	public event PropertyChangingEventHandler PropertyChanging;
	
	public event PropertyChangedEventHandler PropertyChanged;
	
	protected virtual void SendPropertyChanging()
	{
		if ((this.PropertyChanging != null))
		{
			this.PropertyChanging(this, emptyChangingEventArgs);
		}
	}
	
	protected virtual void SendPropertyChanged(String propertyName)
	{
		if ((this.PropertyChanged != null))
		{
			this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}

[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Application_Blog_Grant")]
public partial class Application_Blog_Grant : INotifyPropertyChanging, INotifyPropertyChanged
{
	
	private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
	
	private string _UserID;
	
	private string _BlogerName;
	
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnUserIDChanging(string value);
    partial void OnUserIDChanged();
    partial void OnBlogerNameChanging(string value);
    partial void OnBlogerNameChanged();
    #endregion
	
	public Application_Blog_Grant()
	{
		OnCreated();
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UserID", DbType="VarChar(20) NOT NULL", CanBeNull=false, IsPrimaryKey=true)]
	public string UserID
	{
		get
		{
			return this._UserID;
		}
		set
		{
			if ((this._UserID != value))
			{
				this.OnUserIDChanging(value);
				this.SendPropertyChanging();
				this._UserID = value;
				this.SendPropertyChanged("UserID");
				this.OnUserIDChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_BlogerName", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
	public string BlogerName
	{
		get
		{
			return this._BlogerName;
		}
		set
		{
			if ((this._BlogerName != value))
			{
				this.OnBlogerNameChanging(value);
				this.SendPropertyChanging();
				this._BlogerName = value;
				this.SendPropertyChanged("BlogerName");
				this.OnBlogerNameChanged();
			}
		}
	}
	
	public event PropertyChangingEventHandler PropertyChanging;
	
	public event PropertyChangedEventHandler PropertyChanged;
	
	protected virtual void SendPropertyChanging()
	{
		if ((this.PropertyChanging != null))
		{
			this.PropertyChanging(this, emptyChangingEventArgs);
		}
	}
	
	protected virtual void SendPropertyChanged(String propertyName)
	{
		if ((this.PropertyChanged != null))
		{
			this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}

[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Application_Blog_Comment")]
public partial class Application_Blog_Comment : INotifyPropertyChanging, INotifyPropertyChanged
{
	
	private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
	
	private int _CommentID;
	
	private int _BlogID;
	
	private string _Comment;
	
	private string _CommentType;
	
	private string _UserID;
	
	private System.DateTime _CommentDate;
	
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnCommentIDChanging(int value);
    partial void OnCommentIDChanged();
    partial void OnBlogIDChanging(int value);
    partial void OnBlogIDChanged();
    partial void OnCommentChanging(string value);
    partial void OnCommentChanged();
    partial void OnCommentTypeChanging(string value);
    partial void OnCommentTypeChanged();
    partial void OnUserIDChanging(string value);
    partial void OnUserIDChanged();
    partial void OnCommentDateChanging(System.DateTime value);
    partial void OnCommentDateChanged();
    #endregion
	
	public Application_Blog_Comment()
	{
		OnCreated();
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CommentID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
	public int CommentID
	{
		get
		{
			return this._CommentID;
		}
		set
		{
			if ((this._CommentID != value))
			{
				this.OnCommentIDChanging(value);
				this.SendPropertyChanging();
				this._CommentID = value;
				this.SendPropertyChanged("CommentID");
				this.OnCommentIDChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_BlogID", DbType="Int NOT NULL")]
	public int BlogID
	{
		get
		{
			return this._BlogID;
		}
		set
		{
			if ((this._BlogID != value))
			{
				this.OnBlogIDChanging(value);
				this.SendPropertyChanging();
				this._BlogID = value;
				this.SendPropertyChanged("BlogID");
				this.OnBlogIDChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Comment", DbType="VarChar(500) NOT NULL", CanBeNull=false)]
	public string Comment
	{
		get
		{
			return this._Comment;
		}
		set
		{
			if ((this._Comment != value))
			{
				this.OnCommentChanging(value);
				this.SendPropertyChanging();
				this._Comment = value;
				this.SendPropertyChanged("Comment");
				this.OnCommentChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CommentType", DbType="VarChar(10) NOT NULL", CanBeNull=false)]
	public string CommentType
	{
		get
		{
			return this._CommentType;
		}
		set
		{
			if ((this._CommentType != value))
			{
				this.OnCommentTypeChanging(value);
				this.SendPropertyChanging();
				this._CommentType = value;
				this.SendPropertyChanged("CommentType");
				this.OnCommentTypeChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UserID", DbType="VarChar(20) NOT NULL", CanBeNull=false)]
	public string UserID
	{
		get
		{
			return this._UserID;
		}
		set
		{
			if ((this._UserID != value))
			{
				this.OnUserIDChanging(value);
				this.SendPropertyChanging();
				this._UserID = value;
				this.SendPropertyChanged("UserID");
				this.OnUserIDChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CommentDate", DbType="DateTime NOT NULL")]
	public System.DateTime CommentDate
	{
		get
		{
			return this._CommentDate;
		}
		set
		{
			if ((this._CommentDate != value))
			{
				this.OnCommentDateChanging(value);
				this.SendPropertyChanging();
				this._CommentDate = value;
				this.SendPropertyChanged("CommentDate");
				this.OnCommentDateChanged();
			}
		}
	}
	
	public event PropertyChangingEventHandler PropertyChanging;
	
	public event PropertyChangedEventHandler PropertyChanged;
	
	protected virtual void SendPropertyChanging()
	{
		if ((this.PropertyChanging != null))
		{
			this.PropertyChanging(this, emptyChangingEventArgs);
		}
	}
	
	protected virtual void SendPropertyChanged(String propertyName)
	{
		if ((this.PropertyChanged != null))
		{
			this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}

[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Application_Blog")]
public partial class Application_Blog : INotifyPropertyChanging, INotifyPropertyChanged
{
	
	private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
	
	private int _BlogID;
	
	private System.Guid _BlogKey;
	
	private string _BlogName;
	
	private string _BlogContent;
	
	private string _BlogStatus;
	
	private string _BlogSchedule;
	
	private System.Nullable<System.DateTime> _BlogScheduleDate;
	
	private bool _IsPrivate;
	
	private string _CreateBy;
	
	private System.DateTime _CreateDate;
	
	private string _UpdateBy;
	
	private System.Nullable<System.DateTime> _UpdateDate;
	
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnBlogIDChanging(int value);
    partial void OnBlogIDChanged();
    partial void OnBlogKeyChanging(System.Guid value);
    partial void OnBlogKeyChanged();
    partial void OnBlogNameChanging(string value);
    partial void OnBlogNameChanged();
    partial void OnBlogContentChanging(string value);
    partial void OnBlogContentChanged();
    partial void OnBlogStatusChanging(string value);
    partial void OnBlogStatusChanged();
    partial void OnBlogScheduleChanging(string value);
    partial void OnBlogScheduleChanged();
    partial void OnBlogScheduleDateChanging(System.Nullable<System.DateTime> value);
    partial void OnBlogScheduleDateChanged();
    partial void OnIsPrivateChanging(bool value);
    partial void OnIsPrivateChanged();
    partial void OnCreateByChanging(string value);
    partial void OnCreateByChanged();
    partial void OnCreateDateChanging(System.DateTime value);
    partial void OnCreateDateChanged();
    partial void OnUpdateByChanging(string value);
    partial void OnUpdateByChanged();
    partial void OnUpdateDateChanging(System.Nullable<System.DateTime> value);
    partial void OnUpdateDateChanged();
    #endregion
	
	public Application_Blog()
	{
		OnCreated();
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_BlogID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
	public int BlogID
	{
		get
		{
			return this._BlogID;
		}
		set
		{
			if ((this._BlogID != value))
			{
				this.OnBlogIDChanging(value);
				this.SendPropertyChanging();
				this._BlogID = value;
				this.SendPropertyChanged("BlogID");
				this.OnBlogIDChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_BlogKey", DbType="UniqueIdentifier NOT NULL", IsDbGenerated=true)]
	public System.Guid BlogKey
	{
		get
		{
			return this._BlogKey;
		}
		set
		{
			if ((this._BlogKey != value))
			{
				this.OnBlogKeyChanging(value);
				this.SendPropertyChanging();
				this._BlogKey = value;
				this.SendPropertyChanged("BlogKey");
				this.OnBlogKeyChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_BlogName", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
	public string BlogName
	{
		get
		{
			return this._BlogName;
		}
		set
		{
			if ((this._BlogName != value))
			{
				this.OnBlogNameChanging(value);
				this.SendPropertyChanging();
				this._BlogName = value;
				this.SendPropertyChanged("BlogName");
				this.OnBlogNameChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_BlogContent", DbType="Text NOT NULL", CanBeNull=false, UpdateCheck=UpdateCheck.Never)]
	public string BlogContent
	{
		get
		{
			return this._BlogContent;
		}
		set
		{
			if ((this._BlogContent != value))
			{
				this.OnBlogContentChanging(value);
				this.SendPropertyChanging();
				this._BlogContent = value;
				this.SendPropertyChanged("BlogContent");
				this.OnBlogContentChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_BlogStatus", DbType="VarChar(10) NOT NULL", CanBeNull=false)]
	public string BlogStatus
	{
		get
		{
			return this._BlogStatus;
		}
		set
		{
			if ((this._BlogStatus != value))
			{
				this.OnBlogStatusChanging(value);
				this.SendPropertyChanging();
				this._BlogStatus = value;
				this.SendPropertyChanged("BlogStatus");
				this.OnBlogStatusChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_BlogSchedule", DbType="VarChar(10) NOT NULL", CanBeNull=false)]
	public string BlogSchedule
	{
		get
		{
			return this._BlogSchedule;
		}
		set
		{
			if ((this._BlogSchedule != value))
			{
				this.OnBlogScheduleChanging(value);
				this.SendPropertyChanging();
				this._BlogSchedule = value;
				this.SendPropertyChanged("BlogSchedule");
				this.OnBlogScheduleChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_BlogScheduleDate", DbType="DateTime")]
	public System.Nullable<System.DateTime> BlogScheduleDate
	{
		get
		{
			return this._BlogScheduleDate;
		}
		set
		{
			if ((this._BlogScheduleDate != value))
			{
				this.OnBlogScheduleDateChanging(value);
				this.SendPropertyChanging();
				this._BlogScheduleDate = value;
				this.SendPropertyChanged("BlogScheduleDate");
				this.OnBlogScheduleDateChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IsPrivate", DbType="Bit NOT NULL")]
	public bool IsPrivate
	{
		get
		{
			return this._IsPrivate;
		}
		set
		{
			if ((this._IsPrivate != value))
			{
				this.OnIsPrivateChanging(value);
				this.SendPropertyChanging();
				this._IsPrivate = value;
				this.SendPropertyChanged("IsPrivate");
				this.OnIsPrivateChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CreateBy", DbType="VarChar(20) NOT NULL", CanBeNull=false)]
	public string CreateBy
	{
		get
		{
			return this._CreateBy;
		}
		set
		{
			if ((this._CreateBy != value))
			{
				this.OnCreateByChanging(value);
				this.SendPropertyChanging();
				this._CreateBy = value;
				this.SendPropertyChanged("CreateBy");
				this.OnCreateByChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CreateDate", DbType="DateTime NOT NULL")]
	public System.DateTime CreateDate
	{
		get
		{
			return this._CreateDate;
		}
		set
		{
			if ((this._CreateDate != value))
			{
				this.OnCreateDateChanging(value);
				this.SendPropertyChanging();
				this._CreateDate = value;
				this.SendPropertyChanged("CreateDate");
				this.OnCreateDateChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UpdateBy", DbType="VarChar(20)")]
	public string UpdateBy
	{
		get
		{
			return this._UpdateBy;
		}
		set
		{
			if ((this._UpdateBy != value))
			{
				this.OnUpdateByChanging(value);
				this.SendPropertyChanging();
				this._UpdateBy = value;
				this.SendPropertyChanged("UpdateBy");
				this.OnUpdateByChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UpdateDate", DbType="DateTime")]
	public System.Nullable<System.DateTime> UpdateDate
	{
		get
		{
			return this._UpdateDate;
		}
		set
		{
			if ((this._UpdateDate != value))
			{
				this.OnUpdateDateChanging(value);
				this.SendPropertyChanging();
				this._UpdateDate = value;
				this.SendPropertyChanged("UpdateDate");
				this.OnUpdateDateChanged();
			}
		}
	}
	
	public event PropertyChangingEventHandler PropertyChanging;
	
	public event PropertyChangedEventHandler PropertyChanged;
	
	protected virtual void SendPropertyChanging()
	{
		if ((this.PropertyChanging != null))
		{
			this.PropertyChanging(this, emptyChangingEventArgs);
		}
	}
	
	protected virtual void SendPropertyChanged(String propertyName)
	{
		if ((this.PropertyChanged != null))
		{
			this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}

[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Application_Blog_ViewLog")]
public partial class Application_Blog_ViewLog : INotifyPropertyChanging, INotifyPropertyChanged
{
	
	private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
	
	private int _ViewID;
	
	private int _BlogID;
	
	private string _ViewType;
	
	private string _UserID;
	
	private System.DateTime _ViewDate;
	
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnViewIDChanging(int value);
    partial void OnViewIDChanged();
    partial void OnBlogIDChanging(int value);
    partial void OnBlogIDChanged();
    partial void OnViewTypeChanging(string value);
    partial void OnViewTypeChanged();
    partial void OnUserIDChanging(string value);
    partial void OnUserIDChanged();
    partial void OnViewDateChanging(System.DateTime value);
    partial void OnViewDateChanged();
    #endregion
	
	public Application_Blog_ViewLog()
	{
		OnCreated();
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ViewID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
	public int ViewID
	{
		get
		{
			return this._ViewID;
		}
		set
		{
			if ((this._ViewID != value))
			{
				this.OnViewIDChanging(value);
				this.SendPropertyChanging();
				this._ViewID = value;
				this.SendPropertyChanged("ViewID");
				this.OnViewIDChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_BlogID", DbType="Int NOT NULL")]
	public int BlogID
	{
		get
		{
			return this._BlogID;
		}
		set
		{
			if ((this._BlogID != value))
			{
				this.OnBlogIDChanging(value);
				this.SendPropertyChanging();
				this._BlogID = value;
				this.SendPropertyChanged("BlogID");
				this.OnBlogIDChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ViewType", DbType="VarChar(10) NOT NULL", CanBeNull=false)]
	public string ViewType
	{
		get
		{
			return this._ViewType;
		}
		set
		{
			if ((this._ViewType != value))
			{
				this.OnViewTypeChanging(value);
				this.SendPropertyChanging();
				this._ViewType = value;
				this.SendPropertyChanged("ViewType");
				this.OnViewTypeChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UserID", DbType="VarChar(20) NOT NULL", CanBeNull=false)]
	public string UserID
	{
		get
		{
			return this._UserID;
		}
		set
		{
			if ((this._UserID != value))
			{
				this.OnUserIDChanging(value);
				this.SendPropertyChanging();
				this._UserID = value;
				this.SendPropertyChanged("UserID");
				this.OnUserIDChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ViewDate", DbType="DateTime NOT NULL")]
	public System.DateTime ViewDate
	{
		get
		{
			return this._ViewDate;
		}
		set
		{
			if ((this._ViewDate != value))
			{
				this.OnViewDateChanging(value);
				this.SendPropertyChanging();
				this._ViewDate = value;
				this.SendPropertyChanged("ViewDate");
				this.OnViewDateChanged();
			}
		}
	}
	
	public event PropertyChangingEventHandler PropertyChanging;
	
	public event PropertyChangedEventHandler PropertyChanged;
	
	protected virtual void SendPropertyChanging()
	{
		if ((this.PropertyChanging != null))
		{
			this.PropertyChanging(this, emptyChangingEventArgs);
		}
	}
	
	protected virtual void SendPropertyChanged(String propertyName)
	{
		if ((this.PropertyChanged != null))
		{
			this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
#pragma warning restore 1591
