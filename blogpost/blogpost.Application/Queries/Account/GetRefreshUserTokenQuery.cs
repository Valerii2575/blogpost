using blogpost.Application.DTOs;
using MediatR;

namespace blogpost.Application.Queries.Account
{
    public class GetRefreshUserTokenQuery() : IRequest<GetRefreshUserTokenQueryResult>
    {
    }

    public class GetRefreshUserTokenQueryResult()
    {
        public UserDto UserDto { get; set; }
    }
}
