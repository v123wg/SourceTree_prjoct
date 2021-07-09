using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PastaOrderfood.Models
{
    using System;
    using System.Collections.Generic;
    using System.Web;
    public class Cart
    {
        public int rowid { get; set; }
        public int itemId { get; set; }
        public string pasta_name { get; set; }
        public string pasta_img { get; set; }
        public int quantity { get; set; }

        public int unitprice { get; set; }

        public int total { get; set; }

    }
}