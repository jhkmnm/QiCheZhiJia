using System;
using Dos.ORM;

namespace Model
{
	/// <summary>
	/// 实体类SendLog。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Table("SendLog")]
	[Serializable]
	public partial class SendLog : Entity
	{
		#region Model
		private int _ID;
		private string _SendTime;
		private string _NickID;
		private int _OrderCount;

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
		[Field("SendTime")]
		public string SendTime
		{
			get { return _SendTime; }
			set
			{
				this.OnPropertyValueChange("SendTime");
				this._SendTime = value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		[Field("NickID")]
		public string NickID
		{
			get { return _NickID; }
			set
			{
				this.OnPropertyValueChange("NickID");
				this._NickID = value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		[Field("OrderCount")]
        public int OrderCount
		{
			get { return _OrderCount; }
			set
			{
				this.OnPropertyValueChange("OrderCount");
				this._OrderCount = value;
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
				_.SendTime,
				_.NickID,
				_.OrderCount,
			};
		}
		/// <summary>
		/// 获取值信息
		/// </summary>
		public override object[] GetValues()
		{
			return new object[] {
				this._ID,
				this._SendTime,
				this._NickID,
				this._OrderCount,
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
			public readonly static Field All = new Field("*", "SendLog");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field ID = new Field("ID", "SendLog", "");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field SendTime = new Field("SendTime", "SendLog", "");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field NickID = new Field("NickID", "SendLog", "");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field OrderCount = new Field("OrderCount", "SendLog", "");
		}
		#endregion
	}
}
