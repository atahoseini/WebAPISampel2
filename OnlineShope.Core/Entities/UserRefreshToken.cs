using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShope.Core.Entities;

public class UserRefreshToken
{
    // max value 2.147.xxx.xxx
    public int Id { get; set; }
    public Guid UserId { get; set; }
    public virtual User User { get; set; }
    //unique
    public string RefreshToken { get; set; }
    public int RefreshTokenTimeout { get; set; }
    public DateTime CreateDate { get; set; }
    //تمام کاربران را میتوان با این کزینه بیرون انداخت
    public bool IsValid { get; set; }
}
