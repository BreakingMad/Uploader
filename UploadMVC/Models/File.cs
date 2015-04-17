using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace UploadMVC.Models
{
    public class File
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public int ContentLenght { get; set;}
        public string Type { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        [NotMapped]
        public HttpPostedFileBase FileInstance { get; set; }
    }
}