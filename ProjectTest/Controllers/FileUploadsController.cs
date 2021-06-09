using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectTest.Models;

namespace ProjectTest.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class FileUploadsController : ControllerBase
  {
    public static IWebHostEnvironment _webHostEnvironment;
    private readonly UserContext _context;

    public FileUploadsController(IWebHostEnvironment webHostEnvironment, UserContext context)
    {
      _webHostEnvironment = webHostEnvironment;
      _context = context;
    }

    [HttpPost("/api/FileUploads/{id}")]
    public Object Post(IFormFile postedFile, int id)
    {
      var newfile = new FileInfo(postedFile.FileName);

      var fileExtension = newfile.Extension;
      var response = _context.Users.FirstOrDefault(x => x.User_ID == id);
      

      try
      {
        if (postedFile.Length > 0)
        {
          string path = _webHostEnvironment.WebRootPath + "\\uploads\\";
          if (!Directory.Exists(path))
          {
            Directory.CreateDirectory(path);
          }
          using (FileStream fileStream = System.IO.File.Create(path + postedFile.FileName))
          {
            postedFile.CopyTo(fileStream);
            response.User_Image = path + postedFile.FileName;
            _context.SaveChangesAsync();
            fileStream.Flush();

          }
          var paths = response.User_Image;
          var imgpath = paths;
          byte[] b = System.IO.File.ReadAllBytes(imgpath);
          Convert.ToBase64String(b);
          var imgBase64String = "data:image/png;base64," + Convert.ToBase64String(b);
          
          return Ok(new
          {
            uploaded = "Uploaded",
            UserName = response.User_Name,
            Email = response.User_Email,
            Id = response.User_ID,
            Contact = response.User_Contact,
            password = response.User_Password,
            Role_ID = response.Role_id,
            isLocked = response.isLock,
            User_Image = imgBase64String,
          });
        }
       
        else
        {
          return "Not Uploaded";
        }
      }
      catch (Exception ex)
      {
        return ex.Message;
        throw;
      }
    }

    protected static string GetBase64StringForImage(string imgPath)
    {
      byte[] imageBytes = System.IO.File.ReadAllBytes(imgPath);
      string base64String = Convert.ToBase64String(imageBytes);
      return base64String;


     
    }


  }
}
