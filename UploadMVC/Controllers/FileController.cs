﻿using System;
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
        public virtual ActionResult UploadFile(File file)  //метод, загружающий файл на сервер
        {
            bool isUploaded = false;
            string message = "File upload failed";
            HttpPostedFileBase FileInstance = file.FileInstance;

            if (FileInstance != null && FileInstance.ContentLength != 0)
            {
                string pathForSaving = Server.MapPath("~/Uploads"); //записываем путь к папке загрузок на сервере
                if (this.CreateFolderIfNeeded(pathForSaving)) //создаем папку Uploads, если таковой нет
                {
                    try
                    {
                        FileInstance.SaveAs(Path.Combine(pathForSaving, FileInstance.FileName)); //сохраняем файл на сервере по заданному пути
                        isUploaded = true;
                        message = "File uploaded successfully!";
                        using (FileContext db = new FileContext())  //открываем подключение к бд
                        {
                            file.Path = @"/Uploads/" + FileInstance.FileName; //записываем путь
                            file.ContentLenght = FileInstance.ContentLength;  //           размер
                            file.Date = DateTime.Now;                         //           дату загрузки
                            file.Type = Path.GetExtension(FileInstance.FileName);//        расширение
                            file.Name = FileInstance.FileName;                //           имя
                            
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
            _cx.SaveChanges();  //сохраняем измения
            return Json(new { message = "Deleted" }, "text/html", JsonRequestBehavior.AllowGet);
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
