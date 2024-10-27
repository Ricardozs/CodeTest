using CodeTestApi.Application.Base_Handlers;
using CodeTestApi.Domain.Entities;
using CodeTestApi.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeTestApi.Application.Queries.Users
{
    public class GetUsersQuery : IRequest<IEnumerable<User>>
    {

    }
    public class GetUsersQueryHandler : BaseUserHandler<GetUsersQuery, IEnumerable<User>>
    {
        public GetUsersQueryHandler(IUserRepository userRepository) : base(userRepository)
        {
            
        }

        public override async Task<IEnumerable<User>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            return await _userRepository.GetAllUsersAsync();
        }
    }
}
