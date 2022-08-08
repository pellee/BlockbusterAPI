using BlockbusterAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockbusterAPI.DTOs
{
    public static class ExtensionDto
    {
        public static UserDto AsDto(this User user)
        {
            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                LastName = user.LastName,
                Username = user.Username,
                Email = user.Email
            };
        }
        public static ConflictMessageErrorDto AsDto(this ConflictMessageError error)
        {
            return new ConflictMessageErrorDto
            {
                Message = error.Message
            };
        }
    }
}
