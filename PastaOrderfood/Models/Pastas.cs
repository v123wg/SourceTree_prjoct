//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace PastaOrderfood.Models
{
    using System;
    using System.Collections.Generic;
    using System.Web;

    public partial class Pastas
    {
        public int rowid { get; set; }
        public string pasta_name { get; set; }
        public Nullable<int> category_id { get; set; }
        public Nullable<int> pasta_quantity { get; set; }
        public string pasta_detail { get; set; }
        public Nullable<int> pasta_price { get; set; }
        public string pasta_img { get; set; }
        public Nullable<int> pasta_sort { get; set; }
        public Nullable<int> pasta_Top { get; set; }
    
        public  Categories Categories { get; set; }

        public HttpPostedFileBase ImageFile { get; set; }
    }
}
