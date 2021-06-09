using ProjectTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTest.Services
{
 public interface IUser
  {
    User getUser(int id);

    void updateUser(UpdateUser user);

    User createUser(UserViewModel user);
  }
}
