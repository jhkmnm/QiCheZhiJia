using System;
using Dos.ORM;

namespace Model
{
    /// <summary>
    /// 实体类Area。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Table("Area")]
    [Serializable]
    public partial class Area : Entity
    {
        #region Model
        private string _Site;
        private string _ProId;
        private string _Pro;
        private string _City;
        private string _CityId;
        private bool _IsChecked;

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
        [Field("ProId")]
        public string ProId
        {
            get { return _ProId; }
            set
            {
                this.OnPropertyValueChange("ProId");
                this._ProId = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Field("Pro")]
        public string Pro
        {
            get { return _Pro; }
            set
            {
                this.OnPropertyValueChange("Pro");
                this._Pro = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Field("City")]
        public string City
        {
            get { return _City; }
            set
            {
                this.OnPropertyValueChange("City");
                this._City = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Field("CityId")]
        public string CityId
        {
            get { return _CityId; }
            set
            {
                this.OnPropertyValueChange("CityId");
                this._CityId = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Field("IsChecked")]
        public bool IsChecked
        {
            get { return _IsChecked; }
            set
            {
                this.OnPropertyValueChange("IsChecked");
                this._IsChecked = value;
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
                _.Site,
                _.CityId,
            };
        }
        /// <summary>
        /// 获取列信息
        /// </summary>
        public override Field[] GetFields()
        {
            return new Field[] {
                _.Site,
                _.ProId,
                _.Pro,
                _.City,
                _.CityId,
                _.IsChecked,
            };
        }
        /// <summary>
        /// 获取值信息
        /// </summary>
        public override object[] GetValues()
        {
            return new object[] {
                this._Site,
                this._ProId,
                this._Pro,
                this._City,
                this._CityId,
                this._IsChecked,
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
            public readonly static Field All = new Field("*", "Area");
            /// <summary>
			/// 
			/// </summary>
			public readonly static Field Site = new Field("Site", "Area", "");
            /// <summary>
			/// 
			/// </summary>
			public readonly static Field ProId = new Field("ProId", "Area", "");
            /// <summary>
			/// 
			/// </summary>
			public readonly static Field Pro = new Field("Pro", "Area", "");
            /// <summary>
			/// 
			/// </summary>
			public readonly static Field City = new Field("City", "Area", "");
            /// <summary>
			/// 
			/// </summary>
			public readonly static Field CityId = new Field("CityId", "Area", "");
            /// <summary>
			/// 
			/// </summary>
			public readonly static Field IsChecked = new Field("IsChecked", "Area", "");
        }
        #endregion
    }
}
