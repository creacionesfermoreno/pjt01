using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Configuration;
using E_DataLayer;
using E_DataModel;
using E_DataModel.Common;

namespace E_BusinessLayer
{
    public class MaestroLogic : IDisposable
    {
        MaestroData oMaestroData = null;
        public MaestroLogic()
        {
            oMaestroData = new MaestroData();
        }

        public List<MaestroDTO> ecommerce_uspListarMaestro(MaestroDTO oMaestroDTO)
        {
            return oMaestroData.ecommerce_uspListarMaestro(oMaestroDTO);
        }


        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
