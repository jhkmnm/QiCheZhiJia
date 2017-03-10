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
        private int? _JobType;
        private string _JobDate;
        private string _Time;
        private int? _Space;
        private string _StartTime;
        private string _EndTime;
        private string _ExecTime;

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
        [Field("JobType")]
        public int? JobType
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
        [Field("JobDate")]
        public string JobDate
        {
            get { return _JobDate; }
            set
            {
                this.OnPropertyValueChange("JobDate");
                this._JobDate = value;
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
        /// <summary>
        /// 
        /// </summary>
        [Field("Space")]
        public int? Space
        {
            get { return _Space; }
            set
            {
                this.OnPropertyValueChange("Space");
                this._Space = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Field("StartTime")]
        public string StartTime
        {
            get { return _StartTime; }
            set
            {
                this.OnPropertyValueChange("StartTime");
                this._StartTime = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Field("EndTime")]
        public string EndTime
        {
            get { return _EndTime; }
            set
            {
                this.OnPropertyValueChange("EndTime");
                this._EndTime = value;
            }
        }

        [Field("ExecTime")]
        public string ExecTime
        {
            get { return _ExecTime; }
            set
            {
                this.OnPropertyValueChange("ExecTime");
                this._ExecTime = value;
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
				_.JobType,
				_.JobDate,
				_.Time,
				_.Space,
				_.StartTime,
				_.EndTime,
                _.ExecTime,
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
				this._JobType,
				this._JobDate,
				this._Time,
				this._Space,
				this._StartTime,
				this._EndTime,
                this._ExecTime,
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
            public readonly static Field JobType = new Field("JobType", "Job", "");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field JobDate = new Field("JobDate", "Job", "");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field Time = new Field("Time", "Job", "");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field Space = new Field("Space", "Job", "");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field StartTime = new Field("StartTime", "Job", "");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field EndTime = new Field("EndTime", "Job", "");

            public readonly static Field ExecTime = new Field("ExecTime", "Job", "");
        }
        #endregion
    }    
}

public static class FuncExtend
{
    public static string Message(this Model.Job job)
    {
        DateTime dtnow = DateTime.Now;
        DateTime dt = Convert.ToDateTime(job.JobDate + " " + job.Time);
        if (job.JobType == 1)
        {
            if (!string.IsNullOrWhiteSpace(job.ExecTime))
                return "已执行";
            else if ((dtnow - dt).TotalSeconds > 0)
                return "已过期";
            else
                return "只执行一次，时间是：" + dt.ToString();
        }
        else
        {
            if (!string.IsNullOrWhiteSpace(job.Time))
            {
                dt = Convert.ToDateTime(job.Time);
                if (!string.IsNullOrWhiteSpace(job.ExecTime))
                    return "已执行";
                else if ((dtnow - dt).TotalSeconds > 0)
                    return "已过期";
                else
                    return "每天执行一次，时间是：" + job.Time;
            }
            else
            {
                var index = job.Space.Value / 1000 / 60 / 60 >= 1 ? 1 : 0;
                var space = index == 1 ? job.Space.Value / 1000 / 60 / 60 : job.Space.Value / 1000 / 60;
                dt = Convert.ToDateTime(job.EndTime);
                if ((dtnow - dt).TotalSeconds > 0)
                    return "已过期";
                else
                    return string.Format("在每天的{0}到{1}，每隔{2}{3}执行一次", job.StartTime, job.EndTime, space, index == 1 ? "小时" : "分钟");
            }
        }
    }
}