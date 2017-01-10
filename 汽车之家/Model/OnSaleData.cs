using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class OnSaleData
    {
        public int Result { get; set; }

        public bool Reload { get; set; }

        public string Message { get; set; }

        public List<SaleData> Data { get; set; }

        public int RecordCount { get; set; }
    }

    public class SaleData
    {
        public int SpecId { get; set; }

        public int Price { get; set; }

        public int MinPrice { get; set; }

        //public string SeriesName { get; set; }
        //public string SpecName { get; set; }

        //public int DealerId { get; set; }

        //public int SeriesId { get; set; }

        //public int OriginalPrice { get; set; }

        //public int MinOriginalPrice { get; set; }

        //public int IsRecommend { get; set; }

        //public string ImgUrl { get; set; }

        //public int ScaleTop { get; set; }

        //public int ScaleBottom { get; set; }
    }
}
