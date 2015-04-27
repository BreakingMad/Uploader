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
        public virtual ActionResult UploadFile(File file) //метод, загружающий файл на сервер
        {
            bool isUploaded = false;
            string message = "File upload failed";
            var fileInstance = file.FileInstance;
            
                if (fileInstance != null && fileInstance.ContentLength != 0)
                {
                    string pathForSaving = Server.MapPath("~/Uploads"); //записываем путь к папке загрузок на сервере
                    if (this.CreateFolderIfNeeded(pathForSaving)) //создаем папку Uploads, если таковой нет
                    {
                        try
                        {
                            fileInstance.SaveAs(Path.Combine(pathForSaving, fileInstance.FileName));
                                //сохраняем файл на сервере по заданному пути
                            isUploaded = true;
                            message = "File uploaded successfully!";
                            using (FileContext db = new FileContext()) //открываем подключение к бд
                            {
                                file.Path = @"/Uploads/" + fileInstance.FileName; //записываем путь
                                file.ContentLenght = fileInstance.ContentLength; //           размер
                                file.Date = DateTime.Now; //           дату загрузки
                                file.Type = Path.GetExtension(fileInstance.FileName); //        расширение
                                file.Name = fileInstance.FileName; //           имя

                                db.Files.Add(file); //добавляем в бд
                                db.SaveChanges(); //сохраняем изменения
                                message += "[Added in DB]";
                            }
                        }
                        catch (Exception ex)
                        {
                            message = string.Format("File upload failed: {0}", ex.Message);
                        }
                    }
                }
            

            return Json(new { isUploaded = isUploaded, message = message }, "text/html"); //возвращем объект в фомате Json который получит клиент
        }

        FileContext _cx = new FileContext();  //октрыаем контекс(подключение) к бд
        public ActionResult Browse()   //метод, который вызывает представление Browse (страница просмотра текущих файлов)
        {
            return View(_cx.Files);    //предаем в представление набор данных
        }

        [HttpGet]
        public ActionResult Delete(int id)  //метод удаления файла из бд (передаем id файла)
        {
            File file = _cx.Files.Find(id); //находим файл по id
            _cx.Files.Remove(file);   //удалеям этот соответсвующую запись из бд
            RemoveFileFromServer(file); //удаляем файл с сервера
            _cx.SaveChanges();  //сохраняем измения
            return Json(new { message = "Deleted" }, "text/html", JsonRequestBehavior.AllowGet);
        }

        private void RemoveFileFromServer(File file)  //метод удаления файла  сервера
        {
            string fullPath = Request.MapPath("~/Uploads/" + file.Name);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
        }
        private bool CreateFolderIfNeeded(string path) //создание папки на сервере, если таковой нет
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
