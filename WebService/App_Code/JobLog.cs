using System;
using Dos.ORM;

namespace Model
{
	/// <summary>
	/// 实体类JobLog。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Table("JobLog")]
	[Serializable]
	public partial class JobLog : Entity
	{
		#region Model
		private int _ID;
		private int _UserID;
		private string _JobType;
		private DateTime? _JobTime;

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
		[Field("ID")]
		public int UserID
		{
			get { return _UserID; }
			set
			{
				this.OnPropertyValueChange("UserID");
				this._UserID = value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		[Field("JobType")]
		public string JobType
		{
			get { return _JobType; }
			set
			{
				this.OnPropertyValueChange("JobType");
				this._JobType = value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		[Field("JobTime")]
		public DateTime? JobTime
		{
			get { return _JobTime; }
			set
			{
				this.OnPropertyValueChange("JobTime");
				this._JobTime = value;
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
				_.JobType,
				_.JobTime,
			};
		}
		/// <summary>
		/// 获取值信息
		/// </summary>
		public override object[] GetValues()
		{
			return new object[] {
				this._ID,
                this._UserID,
				this._JobType,
				this._JobTime,
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
			public readonly static Field All = new Field("*", "JobLog");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field ID = new Field("ID", "JobLog", "");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field UserID = new Field("UserID", "JobLog", "");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field JobType = new Field("JobType", "JobLog", "");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field JobTime = new Field("JobTime", "JobLog", "");
		}
		#endregion
	}
}