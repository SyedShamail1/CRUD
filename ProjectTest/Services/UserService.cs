using ProjectTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTest.Services
{
  public class UserService : IUser
  {
    private readonly UserContext _context;
    public UserService(UserContext context)
    {
      _context = context;
    }
    public User createUser(UserViewModel user)
    {
      User u = new User();
      u.User_Email = user.User_Email;
      u.User_ID = user.User_ID;
      u.User_Name = user.User_Name;
      u.User_Password = user.User_Password;
      u.User_Contact = user.User_Password;

      var userData = _context.Users.Add(u);
      _context.SaveChangesAsync();
      return userData.Entity;
    }

    public User getUser(int id)
    {
      throw new NotImplementedException();
    }

    public void updateUser(Models.UpdateUser user)
    {
      throw new NotImplementedException();
    }
  }
}
