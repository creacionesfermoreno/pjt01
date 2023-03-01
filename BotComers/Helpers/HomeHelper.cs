using BotComers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BotComers.Helpers
{
    public class HomeHelper
    {

        //list split
        static public  List<List<FilePeople>> SplitList(List<FilePeople> locations, int nSize = 30)
        {
            var list = new List<List<FilePeople>>();

            for (int i = 0; i < locations.Count; i += nSize)
            {
                list.Add(locations.GetRange(i, Math.Min(nSize, locations.Count - i)));
            }
            return list;
        }
    }
}