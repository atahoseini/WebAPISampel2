using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShope.Applicaition.Models
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string? ProductName { get; set; }
        public long Price { get; set; }
        public string? PriceWithComma { get; set; }
        public IFormFile Thumbnail { get; set; }

        public string ThumbnailBase64 { get; set; }
        public string ThumbnailURL { get; set; }

    }   
}
