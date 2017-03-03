using Dos.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 实体类Spec。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Table("Spec")]
    [Serializable]
    public partial class Spec : Entity
    {
        #region Model
        private int _ID;
        private string _SPecName;
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
        [Field("SPecName")]
        public string SPecName
        {
            get { return _SPecName; }
            set
            {
                this.OnPropertyValueChange("SPecName");
                this._SPecName = value;
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
				_.SPecName,
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
				this._SPecName,
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
            public readonly static Field All = new Field("*", "Spec");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field ID = new Field("ID", "Spec", "");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field SPecName = new Field("SPecName", "Spec", "");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field IsCheck = new Field("IsCheck", "Spec", "");
        }
        #endregion
    }

}
