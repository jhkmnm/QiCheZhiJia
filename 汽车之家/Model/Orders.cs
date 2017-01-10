using System;
using Dos.ORM;

namespace Model
{
	/// <summary>
	/// 实体类Orders。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Table("Orders")]
	[Serializable]
	public partial class Orders : Entity
	{
		#region Model
		private int _Id;
		private string _CustomerName;
		private string _SendTo;
		private DateTime? _SendTime;

		/// <summary>
		/// 
		/// </summary>
		[Field("Id")]
        public int Id
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
		[Field("CustomerName")]
		public string CustomerName
		{
			get { return _CustomerName; }
			set
			{
				this.OnPropertyValueChange("CustomerName");
				this._CustomerName = value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		[Field("SendTo")]
		public string SendTo
		{
			get { return _SendTo; }
			set
			{
				this.OnPropertyValueChange("SendTo");
				this._SendTo = value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		[Field("SendTime")]
		public DateTime? SendTime
		{
			get { return _SendTime; }
			set
			{
				this.OnPropertyValueChange("SendTime");
				this._SendTime = value;
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
				_.CustomerName,
				_.SendTo,
				_.SendTime,
			};
		}
		/// <summary>
		/// 获取值信息
		/// </summary>
		public override object[] GetValues()
		{
			return new object[] {
				this._Id,
				this._CustomerName,
				this._SendTo,
				this._SendTime,
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
			public readonly static Field All = new Field("*", "Orders");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field Id = new Field("Id", "Orders", "");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field CustomerName = new Field("CustomerName", "Orders", "");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field SendTo = new Field("SendTo", "Orders", "");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field SendTime = new Field("SendTime", "Orders", "");
		}
		#endregion
	}
}