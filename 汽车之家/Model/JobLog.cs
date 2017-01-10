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
		private int _JobID;
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
		[Field("JobID")]
		public int JobID
		{
			get { return _JobID; }
			set
			{
				this.OnPropertyValueChange("JobID");
				this._JobID = value;
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
				_.JobID,
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
				this._JobID,
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
			public readonly static Field All = new Field("*", "JobLog");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field ID = new Field("ID", "JobLog", "");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field JobID = new Field("JobID", "JobLog", "");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field Time = new Field("Time", "JobLog", "");
		}
		#endregion
	}
}