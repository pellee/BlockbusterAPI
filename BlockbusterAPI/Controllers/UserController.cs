using BlockbusterAPI.DTOs;
using BlockbusterAPI.Entities;
using BlockbusterAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlockbusterAPI.Controllers
{
    [ApiController]
    [Route("/blockbuster/v1/users")]
    public class UserController : ControllerBase
    {
        readonly IUserRepository repository;

        public UserController(IUserRepository repository)
        {
            this.repository = repository;
        }

        private object BuildConflictError(int error)
        {
            if (error == 3)
                return Conflict(new ConflictMessageErrorDto() { Message = "Username and Email already exists." });
            else if (error == 2)
                return Conflict(new ConflictMessageErrorDto() { Message = "Email already exists." });

            return Conflict(new ConflictMessageErrorDto() { Message = "Username already exists." });
        }

        private async Task<int> AlreadyExistsEmailOrUsername(string email, string username)
        {
            int existsMailOrUser = -1;

            var users = (await repository.GetUsersAsync());

            foreach (var user in users)
            {
                if (user.Email == email && user.Username == username)
                {
                    existsMailOrUser = 3;
                    break;
                }
                else if (user.Email == email && user.Username != username)
                {
                    existsMailOrUser = 2;
                    break;
                }
                else if (user.Username == username && user.Email != email)
                {
                    existsMailOrUser = 1;
                    break;
                }
            }

            return existsMailOrUser;
        }

        [HttpPost]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ConflictMessageErrorDto), StatusCodes.Status409Conflict)]
        public async Task<ActionResult<UserDto>> CreateUserAsync (CreateUserDto userDto)
        {
            int existingUser = await AlreadyExistsEmailOrUsername(userDto.Email, userDto.Username);

            if (existingUser > 0)
                return Conflict(BuildConflictError(existingUser));

            User user = new()
            {
                Id = Guid.NewGuid(),
                Email = userDto.Email,
                LastName = userDto.LastName,
                Name = userDto.Name,
                Username = userDto.Username
            };

            await repository.CreateUserAsync(user);

            return CreatedAtAction(nameof(GetUserAsync), new { id = user.Id }, user.AsDto());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUserAsync (Guid id)
        {
            var user = await repository.GetUserAsync(id);

            if (user is null)
                return NotFound();

            return Ok(user.AsDto());
        }

        [HttpGet]
        public async Task<IEnumerable<UserDto>> GetUsersAsync()
        {
            return (await repository.GetUsersAsync()).Select(user => user.AsDto());
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> DeleteUserAsync(Guid id)
        {
            var user = await GetUserAsync(id);

            if (user is null)
                return NotFound();

            await repository.DeleteUserAsync(id);

            return NoContent();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> UpdateUserAsync (Guid id, UpdateUserDto updateUser)
        {
            var existingUser = await repository.GetUserAsync(id);

            if (existingUser is null)
                return NotFound();

            var user = existingUser with
            {
                Name = updateUser.Name,
                LastName = updateUser.LastName,
                Email = updateUser.Email,
                Username = updateUser.Username
            };

            await repository.UpdateUserAsync(user);

            return NoContent();
        }
    }
}
