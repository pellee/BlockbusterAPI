using BlockbusterAPI.DTOs;
using BlockbusterAPI.Entities;
using BlockbusterAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockbusterAPI.Controllers
{
    [ApiController]
    [Route("/blockbuster/v1/users")]
    public class UserController: ControllerBase
    {
        readonly IUserRepository repository;

        public UserController(IUserRepository repository)
        {
            this.repository = repository;
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> CreateUserAsync (CreateUserDto userDto)
        {
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
        public async Task<ActionResult> DeleteUserAsync(Guid id)
        {
            var user = await GetUserAsync(id);

            if (user is null)
                return NotFound();

            await repository.DeleteUserAsync(id);

            return NoContent();
        }

        [HttpPut("{id}")]
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
