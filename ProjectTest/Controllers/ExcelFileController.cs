using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using ProjectTest.Models;

namespace ProjectTest.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  
  public class ExcelFileController : ControllerBase
  {
    private IHostingEnvironment _iHost;
    public static IWebHostEnvironment _webHostEnvironment;
    public string folderName = "Excel";
    private readonly UserContext _context;


    public ExcelFileController(IHostingEnvironment iHost, UserContext context)
    {
      _context = context;
      _iHost = iHost;
    }

    [HttpPost("/api/ExportExcel/")]
    public async Task<IActionResult> Import(IFormFile file)
    {
      bool isSuccess = false;
      var list = new List<UserExcelViewModel>();
      try
      {


        IFormFile file1 = Request.Form.Files[0];

        string webRootPath = _iHost.WebRootPath;
        string newPath = Path.Combine(webRootPath, folderName);
        

        //string path = _webHostEnvironment.WebRootPath + "\\ExcelFiles\\";
        if (!Directory.Exists(newPath))
        {
          Directory.CreateDirectory(newPath);
        }
        string fullPath = Path.Combine(newPath, file1.FileName);
        using (var stream = new FileStream(fullPath, FileMode.Create))
        {
          await file1.CopyToAsync(stream);
          using (var package = new ExcelPackage(stream))
          {
            ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
            var rowCount = worksheet.Dimension.Rows;

          

           
            for (int row = 2; row <= rowCount; row++)
            {
                var action = worksheet.Cells[row, 9].Value.ToString().ToUpper().Trim();
              if (action.Equals("I") )
              {
                var model = new User();
                // model.User_ID = Convert.ToInt32(worksheet.Cells[row, 1].Value);
                model.User_Name = worksheet.Cells[row, 2].Value.ToString().Trim();
                model.User_Contact = worksheet.Cells[row, 3].Value.ToString().Trim();
                model.User_Email = worksheet.Cells[row, 4].Value.ToString().Trim();
                model.User_Password = worksheet.Cells[row, 5].Value.ToString().Trim();
                model.Role_id = Convert.ToInt32(worksheet.Cells[row, 6].Value);
                model.isLock = Convert.ToBoolean(worksheet.Cells[row, 7].Value);
                model.User_Image = worksheet.Cells[row, 8].Value.ToString().Trim();
                _context.Users.Add(model);
                _context.SaveChanges();
                isSuccess = true;
              }

             else if (action.Equals("D"))
              {
                
                 int userID = Convert.ToInt32(worksheet.Cells[row, 1].Value);
                var exists = GetUserByID(userID);
                if (exists != null)
                {
                  _context.Users.Remove(exists);
                  _context.SaveChanges();
                  isSuccess = true;
                }
              }

            else if (action.Equals("U"))
              {
                int userID = Convert.ToInt32(worksheet.Cells[row, 1].Value);
                var exists = GetUserByID(userID);
                if (exists != null)
                {
                  exists.User_Name = worksheet.Cells[row, 2].Value.ToString().Trim();
                  exists.User_Contact = worksheet.Cells[row, 3].Value.ToString().Trim();
                  exists.User_Email = worksheet.Cells[row, 4].Value.ToString().Trim();
                  exists.User_Password = worksheet.Cells[row, 5].Value.ToString().Trim();
                  exists.Role_id = Convert.ToInt32(worksheet.Cells[row, 6].Value);
                  exists.isLock = Convert.ToBoolean(worksheet.Cells[row, 7].Value);
                  exists.User_Image = worksheet.Cells[row, 8].Value.ToString().Trim();
                  _context.SaveChanges();
                  isSuccess = true;
                }
              }
              }

            }
          }

      }
      catch (Exception ex) {
        
      }
      return Ok(new {Success = isSuccess });
    }

    public User GetUserByID(int id)
    {
      return _context.Users.FirstOrDefault(x => x.User_ID == id);
    }
  }
}
