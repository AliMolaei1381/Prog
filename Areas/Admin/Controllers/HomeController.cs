using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Prog.Models;
using Prog.Models.Context;
using Prog.Models.Entities;
using Prog.Models.Models;

namespace Prog.areas.admin.Controllers;
[Area ("Admin")]
public class HomeController : Controller
{
   private readonly Context_Db db;
   private readonly IWebHostEnvironment env;

    public HomeController(Context_Db _db, IWebHostEnvironment _env)
  {
    db=_db;
    env=_env;
  }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult adduser()
    {
        
        return View();
    }
    
    public async Task<IActionResult> addusers(Vm_User u)
    {  
        if (u != null)
        { 
            if (u.Password == u.Re_Password)
            {  string NewFileName;
            if (u.Img != null)
            { 
                 string FileExtension1 = Path.GetExtension (u.Img.FileName);
                NewFileName = String.Concat (Guid.NewGuid ().ToString (), FileExtension1);
                var path = $"{env.WebRootPath}\\fileupload\\{NewFileName}";
                    using (var stream = new FileStream (path, FileMode.Create))
                    {
                        await u.Img.CopyToAsync (stream);
                    }
                    //
                
            }
            else{
                NewFileName = "";
            }

               Tbl_User user= new Tbl_User();
           user.Name = u.Name;
           user.Family = u.Family;
           user.Age = u.Age;
           user.Password = u.Password;
           user.Status = true;
           user.Avatar = NewFileName;

        db.tbl_Users.Add(user);
        db.SaveChanges();

        return RedirectToAction("ShowUser");
                
            }
        return RedirectToAction("adduser");
        }
        
        return RedirectToAction("adduser");

    }

    public IActionResult ShowUser()
    {
        var find = db.tbl_Users.OrderByDescending(p=>p.Id).ToList();
        if (find != null)
        {
            ViewBag.user = find;
        }
        return View();
        
    }

     public IActionResult userdelete(int id)
    {
        // find 
        var find = db.tbl_Users.SingleOrDefault(p=>p.Id == id);
        // delete image file froem root
        var path1 = $"{env.WebRootPath}\\fileupload\\{find.Avatar}";
        FileInfo fi = new FileInfo(path1);
        if(fi.Exists)
        {
            fi.Delete();
        }
        // delete freo sql
        db.tbl_Users.Remove(find);
        db.SaveChanges();
        return RedirectToAction("ShowUser");
    }
    public IActionResult userstatus(int id)
    {
        var find = db.tbl_Users.SingleOrDefault(p=>p.Id == id);
        if (find.Status == true)
        {
            find.Status = false;
            db.tbl_Users.Update(find);
            db.SaveChanges();
            return RedirectToAction("ShowUser");
        }else
        {
            find.Status = true;
            db.tbl_Users.Update(find);
            db.SaveChanges();
            return RedirectToAction("ShowUser");
        }
    }
    public IActionResult edituser(int id)
    {
        var find = db.tbl_Users.SingleOrDefault(p=>p.Id == id);
        Vm_User userfind = new Vm_User()
        {
            Id = find.Id,
            Name = find.Name,
            Family = find.Family,
            Age = find.Age,
            Password = find.Password,
            Avatar = find.Avatar,
            Status = find.Status
        };
        return View(userfind);
    }
    public async Task<IActionResult> editusers(Vm_User u)
    {
        if (u != null)
        {
           if (u.Password == u.Re_Password)
           {
                var find = db.tbl_Users.SingleOrDefault(p=>p.Id == u.Id);
                
                string NewFileName;
                if (u.Img != null)
                {
                    ///upload file start
                    // find file name
                    string FileExtension1 = Path.GetExtension (u.Img.FileName);
                    // create new file name with guid
                    NewFileName = String.Concat (Guid.NewGuid ().ToString (), FileExtension1);
                    // path upload location
                    var path = $"{env.WebRootPath}\\fileupload\\{NewFileName}";
                    using (var stream = new FileStream (path, FileMode.Create))
                    {
                        await u.Img.CopyToAsync (stream);
                    }
                    ///end upload file  
                }else
                {
                    NewFileName = find.Avatar;
                }

                find.Name = u.Name;
                find.Family = u.Family;
                find.Age = u .Age;
                find.Avatar = NewFileName;
                find.Status = u.Status;
                find.Password = u.Password;
                db.tbl_Users.Update(find);
                db.SaveChanges();
           }
        }
        return RedirectToAction("ShowUser");
    }
    
    

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
