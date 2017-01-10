using System;
using Dos.ORM;
using System.Collections.Generic;


namespace Model
{
    /// <summary>
    /// 实体类Nicks。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Table("Nicks")]
    [Serializable]
    public partial class Nicks : Entity
    {
        #region Model
        private string _Id;
        private string _Nick;
        private object _Check;
        private int? _Send;

        /// <summary>
        /// 
        /// </summary>
        [Field("Id")]
        public string Id
        {
            get { return _Id; }
            set
            {
                this.OnPropertyValueChange("Id");
                this._Id = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Field("Nick")]
        public string Nick
        {
            get { return _Nick; }
            set
            {
                this.OnPropertyValueChange("Nick");
                this._Nick = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Field("Check")]
        public object Check
        {
            get { return _Check; }
            set
            {
                this.OnPropertyValueChange("Check");
                this._Check = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Field("Send")]
        public int? Send
        {
            get { return _Send; }
            set
            {
                this.OnPropertyValueChange("Send");
                this._Send = value;
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
        /// 获取列信息
        /// </summary>
        public override Field[] GetFields()
        {
            return new Field[] {
				_.Id,
				_.Nick,
				_.Check,
				_.Send,
			};
        }
        /// <summary>
        /// 获取值信息
        /// </summary>
        public override object[] GetValues()
        {
            return new object[] {
				this._Id,
				this._Nick,
				this._Check,
				this._Send,
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
            public readonly static Field All = new Field("*", "Nicks");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field Id = new Field("Id", "Nicks", "");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field Nick = new Field("Nick", "Nicks", "");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field Check = new Field("Check", "Nicks", "");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field Send = new Field("Send", "Nicks", "");
        }
        #endregion
    }

    public class NicksResult
    {
        public int returncode { get; set; }
        public string message { get; set; }
        public List<Rows> rows { get; set; }
    }

    public class Rows
    {
        public int saleID{get;set;}
        public string saleName{get;set;}
    }
}