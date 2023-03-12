using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NoteTakingSystem.Models;
using System.Net.Http;
using System.Net;

namespace NoteTakingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly NotesContext _context;

        public UsersController(NotesContext context)
        {
            _context = context;
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Users>> GetUsers(int id)
        {
            var users = await _context.Users.FindAsync(id);

            if (users == null)
            {
                return NotFound();
            }

            return users;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsers(int id, Users users)
        {
            if (id != users.userId)
            {
                return BadRequest();
            }
            _context.Entry(users).State = EntityState.Modified;
            try
            {
                byte[] encData_byte = new byte[users.Password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(users.Password);
                users.Password = Convert.ToBase64String(encData_byte);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsersExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("signup")]
        public async Task<ActionResult<Users>> PostUsers(Users users)
        {
            bool usernameAlreadyExists = _context.Users.Any(x => x.Username == users.Username || x.Email == users.Email);
            if (!usernameAlreadyExists)
            {
                byte[] encData_byte = new byte[users.Password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(users.Password);
                users.Password = Convert.ToBase64String(encData_byte);
                _context.Users.Add(users);
                await _context.SaveChangesAsync();
                return users;
            }
            else
            {
                return BadRequest();
            }
        }

        // POST api/<usersController>
        [HttpPost("login")]
        public async Task<ActionResult<int>> Login(Users user)
        {
            Users users = _context.Users.SingleOrDefault(u =>  u.Email == user.Email);
            if (users == null)
            {
                return NotFound();
            }

            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            System.Text.Decoder utf8Decoder = encoder.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(users.Password);
            int charCount = utf8Decoder.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            utf8Decoder.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            string result = new string(decoded_char);

            
            if (user.Password == result)
            {
                return users.userId;
            }
            else
            {
                return BadRequest();
            }
            
        }

        private bool UsersExists(int id)
        {
            return _context.Users.Any(e => e.userId == id);
        }
    }
}
