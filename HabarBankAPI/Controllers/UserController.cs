using HabarBankAPI.Data;
using HabarBankAPI.Data.DTO;
using HabarBankAPI.Data.DTO.UserDTOs;
using HabarBankAPI.Data.Entities;
using HabarBankAPI.Data.EntityDTOs.UserDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace HabarBankAPI.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserController()
        {
            this._context = new AppDbContext();
        }

        private UserDTO UserToDTO(User user)
        {
            return new UserDTO
            {
                UserId = user.UserId,
                UserLogin = user.UserLogin,
                UserPassword = user.UserPassword,
                UserName = user.UserName,
                UserSurname = user.UserSurname,
                UserPatronymic = user.UserPatronymic,
                UserPhone = user.UserPhone,
                UserEnabled = user.UserEnabled,
                UserUserLevelId = user.UserUserLevelId
            };
        }

        // GET: api/users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> TakeUsers()
        {
            IList<User> users = await this._context.Users.ToListAsync();

            IList<UserDTO> userDTOs = users.Select(x => UserToDTO(x)).ToList();

            return Ok(userDTOs);
        }

        // GET api/users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUserById(int id)
        {
            List<User> users = await this._context.Users.ToListAsync();

            if (!users.Any())
            {
                return NoContent();
            }

            User user = users.FirstOrDefault(x => x.UserId == id && x.UserEnabled is true);

            UserDTO userDTO = UserToDTO(user);

            return userDTO is null ? (ActionResult<UserDTO>)NotFound() : (ActionResult<UserDTO>)Ok(userDTO);
        }

        // POST api/users
        [HttpPost]
        public async Task<ActionResult<UserDTO>> AddNewUser([FromBody] UserDTO userDTO)
        {
            User user = new()
            {
                UserLogin = userDTO.UserLogin,
                UserPhone = userDTO.UserPhone,
                UserPassword = userDTO.UserPassword,
                UserName = userDTO.UserName,
                UserSurname = userDTO.UserSurname,
                UserPatronymic = userDTO.UserPatronymic,
                UserEnabled = userDTO.UserEnabled,
                UserUserLevelId = userDTO.UserUserLevelId,
            };

            this._context.Users.Add(user);

            await Task.Run(() => this._context.SaveChangesAsync());

            return CreatedAtAction(
               nameof(GetUserById),
               new { id = userDTO.UserId },
               UserToDTO(user));
        }

        // PUT api/users/5/status
        [HttpPut("{id}/status")]
        public async Task<ActionResult> EditUserStatus(int id, [FromBody] UserStatusDTO userStatusDTO)
        {
            if (userStatusDTO.UserId != id)
            {
                return BadRequest();
            }

            User last = await this._context.Users.FirstOrDefaultAsync(x => x.UserId == id && x.UserEnabled == true);

            if (last is null)
            {
                return NotFound();
            }

            last.UserEnabled = userStatusDTO.UserEnabled;

            last.UserUserLevelId = userStatusDTO.UserUserLevelId;

            try
            {
                _ = await this._context.SaveChangesAsync();
            }

            catch (Exception)
            {
                return NotFound();
            }

            return NoContent();
        }

        // PUT api/users/5/profile
        [HttpPut("{id}/profile")]
        public async Task<ActionResult> EditUserProfile(int id, [FromBody] UserProfileDTO userProfileDTO)
        {
            if (userProfileDTO.UserId != id)
            {
                return BadRequest();
            }

            User last = await this._context.Users.FirstOrDefaultAsync(x => x.UserId == id && x.UserEnabled == true);

            if (last is null)
            {
                return NotFound();
            }

            last.UserLogin = userProfileDTO.UserLogin;

            last.UserPassword = userProfileDTO.UserPassword;

            last.UserPhone = userProfileDTO.UserPhone;

            last.UserName = userProfileDTO.UserName;

            last.UserSurname = userProfileDTO.UserSurname;

            last.UserPatronymic = userProfileDTO.UserPatronymic;

            try
            {
                _ = await this._context.SaveChangesAsync();
            }

            catch (Exception)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE api/users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveUserById(int id)
        {
            User user = await this._context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            this._context.Users.Remove(user);

            await Task.Run(() => this._context.SaveChanges());

            return NoContent();
        }
    }
}
