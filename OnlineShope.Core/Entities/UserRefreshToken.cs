using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShope.Core.Entities
{
    public class UserRefreshToken
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public string RefreshToken { get; set; }
        public int RefreshTokenTimeout { get; set; }

    }
}
