using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace MenusService.Models
{
    public class Helper
    {
        public static void CopyToAppimages(string srcPath,string fileName,string destPath) {
            FileInfo fInfo = new FileInfo(srcPath + fileName);
            fInfo.CopyTo(destPath + fileName,true);
        }

        public static string removeAllSpaces(string str)
        {

            var items = str.Split(' ');
            var retStr = "";
            foreach (var item in items)
            {
                retStr += item;
            }
            return retStr;
        }
    }
}