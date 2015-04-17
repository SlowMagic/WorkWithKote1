using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorkWithKOTE.Models;


namespace WorkWithKOTE.Code
{
    public static class UploadImages
    {
        public static string UploadImg(HttpPostedFileBase file, string path)
        {
            if (file != null)
            {
                string fullPath = null;
                string file1 = Guid.NewGuid().ToString();
                string extension = Path.GetExtension(file.FileName);
                file1 += extension;
                List<string> extensions = new List<string>() { ".png", ".jpg", ".gif" };
                if (extensions.Contains(extension))
                {
                    file.SaveAs(HttpContext.Current.Server.MapPath(path + file1));
                    fullPath = path + file1;
                }
                return fullPath;
            }
            return null;
        }
    }
}