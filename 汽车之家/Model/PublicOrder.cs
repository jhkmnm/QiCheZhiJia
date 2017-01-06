using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class PublicOrder
    {
        public int Id { get; set; }

        public string CustomerName { get; set; }


        public string CustomerPhone { get; set; }


        public string IntentionCityName { get; set; }
    }

    public class Result
    {
        public int RowCount { get; set; }

        public int Pagesize { get; set; }

        public int Pageindex { get; set; }

        public List<PublicOrder> List { get; set; }
    }

    public class ReturnResult
    {
        public int Returncode { get; set; }

        public string Message { get; set; }

        public Result Result { get; set; }
    }
}
