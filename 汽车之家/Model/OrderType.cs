using Dos.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 实体类OrderType。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Table("OrderType")]
    [Serializable]
    public partial class OrderType : Entity
    {
        #region Model
        private int _ID;
        private string _Site;
        private string _TypeName;
        private bool _IsCheck;

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
        [Field("Site")]
        public string Site
        {
            get { return _Site; }
            set
            {
                this.OnPropertyValueChange("Site");
                this._Site = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Field("TypeName")]
        public string TypeName
        {
            get { return _TypeName; }
            set
            {
                this.OnPropertyValueChange("TypeName");
                this._TypeName = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Field("IsCheck")]
        public bool IsCheck
        {
            get { return _IsCheck; }
            set
            {
                this.OnPropertyValueChange("IsCheck");
                this._IsCheck = value;
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
        /// 获取列信息
        /// </summary>
        public override Field[] GetFields()
        {
            return new Field[] {
				_.ID,
                _.Site,
                _.TypeName,
				_.IsCheck,
			};
        }
        /// <summary>
        /// 获取值信息
        /// </summary>
        public override object[] GetValues()
        {
            return new object[] {
				this._ID,
                this._Site,
				this._TypeName,
				this._IsCheck,
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
            public readonly static Field All = new Field("*", "OrderType");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field ID = new Field("ID", "OrderType", "");
            public readonly static Field Site = new Field("Site", "OrderType", "");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field TypeName = new Field("TypeName", "OrderType", "");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field IsCheck = new Field("IsCheck", "OrderType", "");
        }
        #endregion
    }

}
