using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace UploadMVC.Models
{
    public class File    //модель файла (соответсвует таблице в бд)
    {
        public int Id { get; set; }
        public string Path { get; set; }  //путь на сервере, где будет лежать файл
        public int ContentLenght { get; set;}  //размер файла
        public string Type { get; set; } //разрешение/тип файла
        public string Description { get; set; } //описание/комментраий
        public string Name { get; set; } //имя файла
        public DateTime Date { get; set; }  //дата загрузки

        [NotMapped]
        public IEnumerable<HttpPostedFileBase> FileInstance { get; set; } //это свойство не будет заносится в бд (помечено как [NotMapped])
                                                   //хранит сам файл (который бкдет предаваться в запосе от клиента к серверу)
    }
}