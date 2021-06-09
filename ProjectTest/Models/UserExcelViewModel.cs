using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTest.Models
{
  public class UserExcelViewModel
  {
    public int User_ID { get; set; }
    public string User_Name { get; set; }

    public string User_Contact { get; set; }

    public string User_Email { get; set; }

    public string User_Password { get; set; }

    public int Role_id { get; set; }

    public Boolean isLock { get; set; }

    public string? User_Image { get; set; }
  
}
}
