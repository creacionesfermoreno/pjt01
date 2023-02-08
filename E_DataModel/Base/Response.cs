using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace E_DataModel.Base
{

    public class Response
    {
        public bool Success { get; set; }
        public string User { get; set; }
        public List<Common.Mensaje> MessageList { get; set; }  
    }
}
