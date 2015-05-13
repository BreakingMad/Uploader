using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UploadMVC.Models
{
    public class BrowseHelper  
    {
        public static String Define(String extension)
        {
            String ext = extension.ToLower();
            if (ext == ".jpg" || ext == ".png" || ext == ".bmb")
                return "IMAGE";
            if (ext == ".mp3" || ext == ".wav")
                return "MUSIC";
            if (ext == ".mp4" || ext == ".flv" || ext == ".avi")
                return "VIDEO";
            if (ext == ".doc" || ext == ".docx")
                return "DOC";
            if (ext == ".txt")
                return "TXT";

            return "UNDEFINED";
        }

        public static String NonImageBrowsePath(String type)
        {
            switch (type)
            {
                case "MUSIC":
                    return "/Content/img/Music.png";
                case "VIDEO":
                    return "/Content/img/video_logo.jpg";
                case "DOC":
                    return "/Content/img/Word.png";
                case "TXT":
                    return "/Content/img/Notepad.png";
                case "UNDEFINED":
                    return "/Content/img/question.jpg";
            }
            return "/Content/img/question.jpg";
        }
    }
}