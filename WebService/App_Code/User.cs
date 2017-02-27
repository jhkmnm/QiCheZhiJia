using System;
using Dos.ORM;

namespace Model
{
    /// <summary>
    /// 实体类User。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Table("User")]
    [Serializable]
    public partial class User : Entity
    {
        #region Model
        private int _Id;
        private string _SiteName;
        private string _UserName;
        private string _PassWord;
        private string _Company;
        private string _CompanyID;
        private string _LinkInfo;
        private int _UserType;
        private bool _SendOrder;
        private bool _Query;
        private bool _News;
        private DateTime? _DueTime;
        private int _Status;
        private DateTime? _LastAllotTime;
        private DateTime? _LastQuoteTime;
        private DateTime? _LastNewsTime;
        private int _QueryNum;
        private int _NewsNum;

        /// <summary>
        /// 
        /// </summary>
        [Field("Id")]
        public int Id
        {
            get { return _Id; }
            set
            {
                this.OnPropertyValueChange("Id");
                this._Id = value;
            }
        }
        /// <summary>
        /// 来源网站
        /// </summary>
        [Field("SiteName")]
        public string SiteName
        {
            get { return _SiteName; }
            set
            {
                this.OnPropertyValueChange("SiteName");
                this._SiteName = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Field("UserName")]
        public string UserName
        {
            get { return _UserName; }
            set
            {
                this.OnPropertyValueChange("UserName");
                this._UserName = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Field("PassWord")]
        public string PassWord
        {
            get { return _PassWord; }
            set
            {
                this.OnPropertyValueChange("PassWord");
                this._PassWord = value;
            }
        }
        /// <summary>
        /// 企业名称
        /// </summary>
        [Field("Company")]
        public string Company
        {
            get { return _Company; }
            set
            {
                this.OnPropertyValueChange("Company");
                this._Company = value;
            }
        }
        /// <summary>
        /// 企业ID
        /// </summary>
        [Field("CompanyID")]
        public string CompanyID
        {
            get { return _CompanyID; }
            set
            {
                this.OnPropertyValueChange("CompanyID");
                this._CompanyID = value;
            }
        }
        /// <summary>
        /// 联系信息
        /// </summary>
        [Field("LinkInfo")]
        public string LinkInfo
        {
            get { return _LinkInfo; }
            set
            {
                this.OnPropertyValueChange("LinkInfo");
                this._LinkInfo = value;
            }
        }
        /// <summary>
        /// 用户类型 0试用 1付费
        /// </summary>
        [Field("UserType")]
        public int UserType
        {
            get { return _UserType; }
            set
            {
                this.OnPropertyValueChange("UserType");
                this._UserType = value;
            }
        }
        /// <summary>
        /// 付费标记, true已付费
        /// </summary>
        [Field("SendOrder")]
        public bool SendOrder
        {
            get { return _SendOrder; }
            set
            {
                this.OnPropertyValueChange("SendOrder");
                this._SendOrder = value;
            }
        }
        /// <summary>
        /// 付费标记, true已付费
        /// </summary>
        [Field("Query")]
        public bool Query
        {
            get { return _Query; }
            set
            {
                this.OnPropertyValueChange("Query");
                this._Query = value;
            }
        }
        /// <summary>
        /// 付费标记, true已付费
        /// </summary>
        [Field("News")]
        public bool News
        {
            get { return _News; }
            set
            {
                this.OnPropertyValueChange("News");
                this._News = value;
            }
        }
        /// <summary>
        /// 到期时间 付费服务全部使用这个时间进行判断
        /// </summary>
        [Field("DueTime")]
        public DateTime? DueTime
        {
            get { return _DueTime; }
            set
            {
                this.OnPropertyValueChange("DueTime");
                this._DueTime = value;
            }
        }
        /// <summary>
        /// 运行状态
        /// </summary>
        [Field("Status")]
        public int Status
        {
            get { return _Status; }
            set
            {
                this.OnPropertyValueChange("Status");
                this._Status = value;
            }
        }
        /// <summary>
        /// 最后抢单时间
        /// </summary>
        [Field("LastAllotTime")]
        public DateTime? LastAllotTime
        {
            get { return _LastAllotTime; }
            set
            {
                this.OnPropertyValueChange("LastAllotTime");
                this._LastAllotTime = value;
            }
        }
        /// <summary>
        /// 最后报价时间
        /// </summary>
        [Field("LastQuoteTime")]
        public DateTime? LastQuoteTime
        {
            get { return _LastQuoteTime; }
            set
            {
                this.OnPropertyValueChange("LastQuoteTime");
                this._LastQuoteTime = value;
            }
        }
        /// <summary>
        /// 最后发布新闻时间
        /// </summary>
        [Field("LastNewsTime")]
        public DateTime? LastNewsTime
        {
            get { return _LastNewsTime; }
            set
            {
                this.OnPropertyValueChange("LastNewsTime");
                this._LastNewsTime = value;
            }
        }
        /// <summary>
        /// 报价剩余次数
        /// </summary>
        [Field("QueryNum")]
        public int QueryNum
        {
            get { return _QueryNum; }
            set
            {
                this.OnPropertyValueChange("QueryNum");
                this._QueryNum = value;
            }
        }
        /// <summary>
        /// 发布新闻剩余次数
        /// </summary>
        [Field("NewsNum")]
        public int NewsNum
        {
            get { return _NewsNum; }
            set
            {
                this.OnPropertyValueChange("NewsNum");
                this._NewsNum = value;
            }
        }
        #endregion

        #region Method
        /// <summary>
        /// 获取实体中的主键列
        /// </summary>
        public override Field[] GetPrimaryKeyFields()
        {
            return new Field[] {
				_.Id,
			};
        }
        /// <summary>
        /// 获取实体中的标识列
        /// </summary>
        public override Field GetIdentityField()
        {
            return _.Id;
        }
        /// <summary>
        /// 获取列信息
        /// </summary>
        public override Field[] GetFields()
        {
            return new Field[] {
				_.Id,
				_.SiteName,
				_.UserName,
				_.PassWord,
				_.Company,
				_.CompanyID,
				_.LinkInfo,
				_.UserType,
				_.SendOrder,
				_.Query,
				_.News,
				_.DueTime,
				_.Status,
				_.LastAllotTime,
				_.LastQuoteTime,
				_.LastNewsTime,
				_.QueryNum,
				_.NewsNum,
			};
        }
        /// <summary>
        /// 获取值信息
        /// </summary>
        public override object[] GetValues()
        {
            return new object[] {
				this._Id,
				this._SiteName,
				this._UserName,
				this._PassWord,
				this._Company,
				this._CompanyID,
				this._LinkInfo,
				this._UserType,
				this._SendOrder,
				this._Query,
				this._News,
				this._DueTime,
				this._Status,
				this._LastAllotTime,
				this._LastQuoteTime,
				this._LastNewsTime,
				this._QueryNum,
				this._NewsNum,
			};
        }
        /// <summary>
        /// 是否是v1.10.5.6及以上版本实体。
        /// </summary>
        /// <returns></returns>
        public override bool V1_10_5_6_Plus()
        {
            return true;
        }
        #endregion

        #region _Field
        /// <summary>
        /// 字段信息
        /// </summary>
        public class _
        {
            /// <summary>
            /// * 
            /// </summary>
            public readonly static Field All = new Field("*", "User");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field Id = new Field("Id", "User", "");
            /// <summary>
            /// 来源网站
            /// </summary>
            public readonly static Field SiteName = new Field("SiteName", "User", "来源网站");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field UserName = new Field("UserName", "User", "");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field PassWord = new Field("PassWord", "User", "");
            /// <summary>
            /// 企业名称
            /// </summary>
            public readonly static Field Company = new Field("Company", "User", "企业名称");
            /// <summary>
            /// 企业ID
            /// </summary>
            public readonly static Field CompanyID = new Field("CompanyID", "User", "企业ID");
            /// <summary>
            /// 联系信息
            /// </summary>
            public readonly static Field LinkInfo = new Field("LinkInfo", "User", "联系信息");
            /// <summary>
            /// 用户类型 0试用 1付费
            /// </summary>
            public readonly static Field UserType = new Field("UserType", "User", "用户类型 0试用 1付费");
            /// <summary>
            /// 付费标记, true已付费
            /// </summary>
            public readonly static Field SendOrder = new Field("SendOrder", "User", "付费标记, true已付费");
            /// <summary>
            /// 付费标记, true已付费
            /// </summary>
            public readonly static Field Query = new Field("Query", "User", "付费标记, true已付费");
            /// <summary>
            /// 付费标记, true已付费
            /// </summary>
            public readonly static Field News = new Field("News", "User", "付费标记, true已付费");
            /// <summary>
            /// 到期时间 付费服务全部使用这个时间进行判断
            /// </summary>
            public readonly static Field DueTime = new Field("DueTime", "User", "到期时间 付费服务全部使用这个时间进行判断");
            /// <summary>
            /// 运行状态
            /// </summary>
            public readonly static Field Status = new Field("Status", "User", "运行状态");
            /// <summary>
            /// 最后抢单时间
            /// </summary>
            public readonly static Field LastAllotTime = new Field("LastAllotTime", "User", "最后抢单时间");
            /// <summary>
            /// 最后报价时间
            /// </summary>
            public readonly static Field LastQuoteTime = new Field("LastQuoteTime", "User", "最后报价时间");
            /// <summary>
            /// 最后发布新闻时间
            /// </summary>
            public readonly static Field LastNewsTime = new Field("LastNewsTime", "User", "最后发布新闻时间");
            /// <summary>
            /// 报价剩余次数
            /// </summary>
            public readonly static Field QueryNum = new Field("QueryNum", "User", "报价剩余次数");
            /// <summary>
            /// 发布新闻剩余次数
            /// </summary>
            public readonly static Field NewsNum = new Field("NewsNum", "User", "发布新闻剩余次数");
        }
        #endregion
    }

    public class UserResult
    {
        public bool Result { get; set; }

        public string Message { get; set; }

        public User Data { get; set; }
    }
}