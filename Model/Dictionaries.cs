using System;
using Dos.ORM;

namespace AideM
{
    /// <summary>
    /// 实体类Dictionaries。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Table("Dictionaries")]
    [Serializable]
    public partial class Dictionaries : Entity
    {
        #region Model
        private string _Key;
        private string _Value;

        /// <summary>
        /// 
        /// </summary>
        [Field("Key")]
        public string Key
        {
            get { return _Key; }
            set
            {
                this.OnPropertyValueChange("Key");
                this._Key = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Field("Value")]
        public string Value
        {
            get { return _Value; }
            set
            {
                this.OnPropertyValueChange("Value");
                this._Value = value;
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
				_.Key,
			};
        }
        /// <summary>
        /// 获取列信息
        /// </summary>
        public override Field[] GetFields()
        {
            return new Field[] {
				_.Key,
				_.Value,
			};
        }
        /// <summary>
        /// 获取值信息
        /// </summary>
        public override object[] GetValues()
        {
            return new object[] {
				this._Key,
				this._Value,
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
            public readonly static Field All = new Field("*", "Dictionaries");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field Key = new Field("Key", "Dictionaries", "");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field Value = new Field("Value", "Dictionaries", "");
        }
        #endregion
    }
}
