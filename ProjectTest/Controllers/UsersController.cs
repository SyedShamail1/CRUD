using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProjectTest.Models;

namespace ProjectTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserContext _context;
    private object _config;

    public UsersController(UserContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()

    {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        

        // POST: api/Users
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(UserViewModel user)
        {
          User u = new User();
         u.User_Email = user.User_Email;
         u.User_ID = user.User_ID;
         u.User_Name = user.User_Name;
          u.User_Password = user.User_Password;
         u.User_Contact = user.User_Password;


            _context.Users.Add(u);
            await _context.SaveChangesAsync();
      
            return CreatedAtAction("GetUser", new { id = user.User_ID }, user);
        }
     //Updating User from the User Profile Form
      [HttpPut("/api/users/update")]
      public async Task<IActionResult> UpdateUser(UpdateUser user)
    {
        
      var response = _context.Users.FirstOrDefault(x => x.User_ID == user.User_ID);
      if (response == null)
      {
        return Ok(new { doesExist = false });
      }

      response.User_Email = user.User_Email;
      response.User_Password = user.User_Password;
      response.User_Contact = user.User_Contact;
      response.User_Name = user.User_Name;

      
      await _context.SaveChangesAsync();

      return Ok(new
      {
        doesExist = true,
        UserName = response.User_Name,
        Email = response.User_Email,
        Id = response.User_ID,
        Contact = response.User_Contact,
        Password = response.User_Password,
        Role_ID = response.Role_id

      });

    }

    //Locking the User
    [HttpPut("/api/users/lockUser/{id}")]
    public async Task<IActionResult> LockUser(int id)
    {
      var response = _context.Users.FirstOrDefault(x => x.User_ID == id);

      if (response == null)
      {
        return Ok(new { notFound = "User not found" });
      }
      response.isLock = !response.isLock;
      await _context.SaveChangesAsync();
     
      return Ok(new
      {
        UserName = response.User_Name,
        Email = response.User_Email,
        Id = response.User_ID,
        Contact = response.User_Contact,
        password = response.User_Password,
        Role_ID = response.Role_id,
        isLocked = response.isLock
      });

      
    }

    //Sending Email for Blocked User
    [HttpGet("/api/users/sendEmail/{SendMail}")]
    public IActionResult SendMail(bool SendMail)
    {
      if (SendMail == true) {
        var msg = "<h1>Please Unlock My Account!</h1>";
        Send_Email.SendMail("syedshamail3@gmail.com", "Testing", msg);
        return Ok(new { SentMailStatus = true });
      }

      else
      {
        return Ok(new { SentMailStatus = false });
      }

     
    }

    //Authentication Method for user
    [HttpPost("/api/users/auth")]
      public IActionResult AuthenticateUser(AuthenticateUserViewModel user)
    {
      var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ThisismySecretKey"));
      var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

      var token = new JwtSecurityToken("ThisismySecretKey",
        "Test.com",
        null,
        expires: DateTime.Now.AddMinutes(60),
        signingCredentials: credentials);


      var createdToken = new JwtSecurityTokenHandler().WriteToken(token);
      var response = _context.Users.FirstOrDefault(x => x.User_Email == user.User_Email && x.User_Password == user.User_Password);


      if (response.User_Image != null)
      {
        var paths = response.User_Image;
        var imgpath = paths;
        byte[] b = System.IO.File.ReadAllBytes(imgpath);
        Convert.ToBase64String(b);
        var imgBase64String = "data:image/png;base64," + Convert.ToBase64String(b);
        return Ok(new
        {
          IsSuccess = true,
          UserName = response.User_Name,
          Email = response.User_Email,
          Id = response.User_ID,
          Contact = response.User_Contact,
          JWTToken = createdToken,
          password = response.User_Password,
          Role_ID = response.Role_id,
          isLocked = response.isLock,
          User_Image = imgBase64String,
        });
      }
      

      if (response == null) {
        return Ok(new { isSuccess = false , });
      }


     
        return Ok(new { IsSuccess = true,
          UserName = response.User_Name,
          Email = response.User_Email,
          Id = response.User_ID,
          Contact = response.User_Contact,
          JWTToken = createdToken,
          password = response.User_Password,
          Role_ID = response.Role_id,
          isLocked = response.isLock,
          User_Image = response.User_Image,
        });

     
    }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return user;
        }

   
        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.User_ID == id);
        }
    }
}
