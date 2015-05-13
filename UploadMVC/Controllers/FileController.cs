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
            var fileInstance = file.FileInstance;
            
                if (fileInstance != null && fileInstance.ContentLength != 0)
                {
                    string pathForSaving = Server.MapPath("~/Uploads"); 
                    if (this.CreateFolderIfNeeded(pathForSaving)) 
                    {
                        try
                        {
                            fileInstance.SaveAs(Path.Combine(pathForSaving, fileInstance.FileName));
                            isUploaded = true;
                            message = "File uploaded successfully!";
                            using (FileContext db = new FileContext())
                            {
                                file.Path = @"/Uploads/" + fileInstance.FileName; 
                                file.ContentLenght = fileInstance.ContentLength; 
                                file.Date = DateTime.Now; 
                                file.Type = Path.GetExtension(fileInstance.FileName); 
                                file.Name = fileInstance.FileName; 

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
            RemoveFileFromServer(file); 
            _cx.SaveChanges();  
            return Json(new { message = "Deleted" }, "text/html", JsonRequestBehavior.AllowGet);
        }

        private void RemoveFileFromServer(File file)  
        {
            string fullPath = Request.MapPath("~/Uploads/" + file.Name);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
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
