using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTest.Models
{
  public class AuthenticateUserViewModel
  {
    public string User_Email { get; set; }

    public string User_Password { get; set; }
    public int Role_id { get; set; }

    public Boolean isLock { get; set; }
  }
}
