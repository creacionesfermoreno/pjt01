using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Data.SqlClient;
using E_DataModel;
using E_DataModel.Corporativo;
using E_DataModel.Common;

namespace E_DataLayer
{
    public class MaestroData
    {
        public List<MaestroDTO> ecommerce_uspListarMaestro(MaestroDTO oMaestroDTO)
        {
            List<MaestroDTO> lista = new List<MaestroDTO>();
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("ecommerce_uspListarMaestro", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Filtermaster", System.Data.SqlDbType.VarChar)).Value = oMaestroDTO.Filter;
                  
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new MaestroDTO()
                                {
                                    Codigo = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Codigo")]),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    valor = oIDataReader[oIDataReader.GetOrdinal("valor")].ToString(),
                                    urlImagen = oIDataReader[oIDataReader.GetOrdinal("urlImagen")].ToString()                               
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }

    }
}
