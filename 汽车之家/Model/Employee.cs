using System;
using System.Collections.Generic;

namespace Model
{
    public class LinkResult
    {
        public int Result { get; set; }
        public bool Reload { get; set; }

        public string Message { get; set; }

        public Data Data { get; set; }
    }

    public class Data
    {
        public bool IsAdmin { get; set; }

        public List<Sale> SaleList { get; set; }
    }

    public class Sale
    {
        public string Name { get; set; }
        
        public string Phone { get; set; }

        public string TelPhone { get; set; }

        public string Sex { get; set; }

        public string RoleName { get; set; }

        public string CompanyString { get; set; }
    }
}
