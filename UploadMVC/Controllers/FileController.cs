using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using UploadMVC.Models;
using File = UploadMVC.Models.File;

namespace UploadMVC.Controllers
{
    public class FileController : Controller
    {
        
        [HttpPost]
        public virtual ActionResult UploadFile(File file)
        {
            bool isUploaded = false;
            string message = "File upload failed";
            HttpPostedFileBase FileInstance = file.FileInstance;

            if (FileInstance != null && FileInstance.ContentLength != 0)
            {
                string pathForSaving = Server.MapPath("~/Uploads");
                if (this.CreateFolderIfNeeded(pathForSaving))
                {
                    try
                    {
                        FileInstance.SaveAs(Path.Combine(pathForSaving, FileInstance.FileName));
                        isUploaded = true;
                        message = "File uploaded successfully!";
                        using (FileContext db = new FileContext())
                        {
                            file.Path = @"/Uploads/" + FileInstance.FileName;
                            file.ContentLenght = FileInstance.ContentLength;
                            file.Date = DateTime.Now;
                            file.Type = Path.GetExtension(FileInstance.FileName);
                            file.Name = FileInstance.FileName;
                            
                            db.Files.Add(file);
                            db.SaveChanges();
                            message += "[Added in DB]";
                        }
                    }
                    catch (Exception ex)
                    {
                        message = string.Format("File upload failed: {0}", ex.Message);
                    }
                }
            }
            return Json(new { isUploaded = isUploaded, message = message }, "text/html");
        }

        FileContext _cx = new FileContext();
        public ActionResult Browse()
        {
            return View(_cx.Files);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            File file = _cx.Files.Find(id);
            _cx.Files.Remove(file);
            _cx.SaveChanges();
            return Json(new { message = "Deleted" }, "text/html", JsonRequestBehavior.AllowGet);
        }
        private bool CreateFolderIfNeeded(string path)
        {
            bool result = true;
            if (!Directory.Exists(path))
            {
                try
                {
                    Directory.CreateDirectory(path);
                }
                catch (Exception)
                {
                    
                    result = false;
                }
            }
            return result;
        }

    }
}
