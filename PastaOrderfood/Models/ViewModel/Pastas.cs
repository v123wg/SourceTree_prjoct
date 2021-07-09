using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PastaOrderfood.Models.ViewModel
{
    [MetadataType(typeof(PastasMetaData))]
    public partial class Pastas
    {
        private class PastasMetaData
        {
            [Key]
            public int rowid { get; set; }
            [Display(Name = "品名")]
            [Required(ErrorMessage = "資料請輸入完整")]
            public string pasta_name { get; set; }
            [Display(Name = "種類")]
            [Required(ErrorMessage = "資料請輸入完整")]
            public string category_id { get; set; }
            [Display(Name = "數量")]
            [Required(ErrorMessage = "資料請輸入完整")]
            public Nullable<int> pasta_quantity { get; set; }
            [Display(Name = "說明")]
            [Required(ErrorMessage = "資料請輸入完整")]
            public string pasta_detail { get; set; }
            [Display(Name = "價錢")]
            [Required(ErrorMessage = "資料請輸入完整")]
            public Nullable<int> pasta_price { get; set; }
            [Display(Name = "圖片")]
            public string pasta_img { get; set; }

            //存圖片名稱
            public HttpPostedFileBase ImageFile { get; set; }
        }
    }


    
}