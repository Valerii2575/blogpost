using blogpost.Application.DTOs;
using blogpost.Domain.Entities;

namespace blogpost.Application.Mapping.UserAccount
{
    public static class MappingUser
    {
        public static UserDto ToData(this User user)
        {
            return new UserDto
            {
                Id = user.Id,
                Name = user.UserName,
                Email = user.Email,
                EmailConfirmed = user.EmailConfirmed,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
        }
    }
}
