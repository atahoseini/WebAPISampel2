using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShope.Applicaition.CQRS.AuthenticateCommandQuery.Command
{
    public class LoginCommand: IRequest<LoginCommandResponse>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class LoginCommandResponse
    {
        public string UserName { get; set; }
        public string Token { get; set; }
        public int ExpireTime { get; set; }
    }

    //public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginCommandResponse>
    //{
    //    //public Task<LoginCommandResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    //    //{
    //    //    //با جدول user کار داریم
    //    //}
    //}
}
