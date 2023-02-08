using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace E_DataModel.Common
{
    public class Paging
    {
        public bool All { get; set; }
        public uint PageNumber { get; set; }
        public uint PageRecords { get; set; }
        public uint TotalRecord { get; set; }

        public decimal OutValue_Deciaml1 { get; set; }
        public decimal OutValue_Deciaml2 { get; set; }
        public decimal OutValue_Deciaml3 { get; set; }
        public decimal OutValue_Deciaml4 { get; set; }

        public int OutValue_Int1 { get; set; }
        public int OutValue_Int2 { get; set; }
        public int OutValue_Int3 { get; set; }
        public int OutValue_Int4 { get; set; }

    }
}
