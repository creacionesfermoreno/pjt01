using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BotComers.Models
{
    public class HomeModel
    {
    }
    //************************************ FILES ************************************/

    public class FilePeople
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public bool Valid { get; set; }
    }
    public class FileSendModel
    {
        public string Origin { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string MimeType { get; set; }
    }
    //************************************ FILES ************************************/
}