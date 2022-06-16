namespace OnlineShope.Infrastructure.Model
{
    public class ShopActionResult<T> //t = type
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public int Total { get; set; } //=> masalan 1469
        public int Page { get; set; } // => 3
        //public int PageCount { get; set; } // (total /size )=>73.45? => باید رند به از => Math.Ceiling(74)
        public int PageCount
        {
            get
            {
                if(Total == 0)  return 0;
                return Convert.ToInt32(Math.Ceiling(Total/(double)Size));
            }
        } // (total /size )=>73.45? => باید رند به از => Math.Ceiling(74)
        public int Size { get; set; } //=>20
    }
}
