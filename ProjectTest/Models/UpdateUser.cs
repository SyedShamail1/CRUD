using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTest.Models
{
  public class UpdateUser
  {
    public int User_ID { get; set; }
    public string User_Name { get; set; }

    public string User_Contact { get; set; }

    public string User_Email { get; set; }

    public string User_Password { get; set; }
  }
}
