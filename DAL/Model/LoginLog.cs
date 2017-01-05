using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System;
using Dos.ORM;

namespace Aide
{
	/// <summary>
	/// 实体类LoginLog。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Table("LoginLog")]
	[Serializable]
	public partial class LoginLog : Entity
	{
		#region Model
		private int _Id;
		private int _UserId;		
		private string _ToDay;
		private DateTime _LastLoginTime;
		private float? _LoginTime;

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
		[Field("UserId")]
		public int UserId
		{
			get { return _UserId; }
			set
			{
				this.OnPropertyValueChange("UserId");
				this._UserId = value;
			}
		}		
		/// <summary>
		/// 
		/// </summary>
		[Field("ToDay")]
		public string ToDay
		{
			get { return _ToDay; }
			set
			{
				this.OnPropertyValueChange("ToDay");
				this._ToDay = value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		[Field("LastLoginTime")]
		public DateTime LastLoginTime
		{
			get { return _LastLoginTime; }
			set
			{
				this.OnPropertyValueChange("LastLoginTime");
				this._LastLoginTime = value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		[Field("LoginTime")]
		public float? LoginTime
		{
			get { return _LoginTime; }
			set
			{
				this.OnPropertyValueChange("LoginTime");
				this._LoginTime = value;
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
		/// 获取实体中的标识列
		/// </summary>
		public override Field GetIdentityField()
		{
			return _.Id;
		}
		/// <summary>
		/// 获取列信息
		/// </summary>
		public override Field[] GetFields()
		{
			return new Field[] {
				_.Id,
				_.UserId,				
				_.ToDay,
				_.LastLoginTime,
				_.LoginTime,
			};
		}
		/// <summary>
		/// 获取值信息
		/// </summary>
		public override object[] GetValues()
		{
			return new object[] {
				this._Id,
				this._UserId,				
				this._ToDay,
				this._LastLoginTime,
				this._LoginTime,
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
			public readonly static Field All = new Field("*", "LoginLog");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field Id = new Field("Id", "LoginLog", "");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field UserId = new Field("UserId", "LoginLog", "");			
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field ToDay = new Field("ToDay", "LoginLog", "");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field LastLoginTime = new Field("LastLoginTime", "LoginLog", "");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field LoginTime = new Field("LoginTime", "LoginLog", "");
		}
		#endregion
	}
}
