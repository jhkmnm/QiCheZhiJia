using System;
using Dos.ORM;

namespace Model
{
    /// <summary>
    /// 实体类Job。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Table("Job")]
    [Serializable]
    public partial class Job : Entity
    {
        #region Model
        private int _ID;
        private string _JobName;
        private string _Time;

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
        [Field("JobName")]
        public string JobName
        {
            get { return _JobName; }
            set
            {
                this.OnPropertyValueChange("JobName");
                this._JobName = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Field("Time")]
        public string Time
        {
            get { return _Time; }
            set
            {
                this.OnPropertyValueChange("Time");
                this._Time = value;
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
				_.JobName,
				_.Time,
			};
        }
        /// <summary>
        /// 获取值信息
        /// </summary>
        public override object[] GetValues()
        {
            return new object[] {
				this._ID,
				this._JobName,
				this._Time,
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
            public readonly static Field All = new Field("*", "Job");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field ID = new Field("ID", "Job", "");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field JobName = new Field("JobName", "Job", "");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field Time = new Field("Time", "Job", "");
        }
        #endregion
    }
}
