using System;
using Dos.ORM;

namespace Model
{
    /// <summary>
    /// 实体类News。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Table("News")]
    [Serializable]
    public partial class News : Entity
    {
        #region Model
        private int _ID;
        private string _Title;
        private string _Content;
        private string _SendContent;

        /// <summary>
        /// 
        /// </summary>
        [Field("ID")]
        public int ID
        {
            get { return _ID; }
            set
            {
                this.OnPropertyValueChange("ID");
                this._ID = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Field("Title")]
        public string Title
        {
            get { return _Title; }
            set
            {
                this.OnPropertyValueChange("Title");
                this._Title = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Field("Content")]
        public string Content
        {
            get { return _Content; }
            set
            {
                this.OnPropertyValueChange("Content");
                this._Content = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Field("SendContent")]
        public string SendContent
        {
            get { return _SendContent; }
            set
            {
                this.OnPropertyValueChange("SendContent");
                this._SendContent = value;
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
				_.ID,
			};
        }
        /// <summary>
        /// 获取实体中的标识列
        /// </summary>
        public override Field GetIdentityField()
        {
            return _.ID;
        }
        /// <summary>
        /// 获取列信息
        /// </summary>
        public override Field[] GetFields()
        {
            return new Field[] {
				_.ID,
				_.Title,
				_.Content,
                _.SendContent,
			};
        }
        /// <summary>
        /// 获取值信息
        /// </summary>
        public override object[] GetValues()
        {
            return new object[] {
				this._ID,
				this._Title,
				this._Content,
                this._SendContent,
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
            public readonly static Field All = new Field("*", "News");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field ID = new Field("ID", "News", "");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field Title = new Field("Title", "News", "");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field Content = new Field("Content", "News", "");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field SendContent = new Field("SendContent", "News", "");            
        }
        #endregion
    }
}