
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using E_DataModel.Common;
using E_DataModel.Gimnasio;
using E_DataLayer;
using System.Data.SqlClient;

namespace E_DataLayer.Gimnasio
{
	public class CategoriaSuplementoData
	{
		
		public List<CategoriaSuplementoDTO> Listar(CategoriaSuplementoDTO oCategoriaSuplementoDTO)
		{
			List<CategoriaSuplementoDTO> lista = new List<CategoriaSuplementoDTO>();
            
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarCategoriaSuplemento", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oCategoriaSuplementoDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oCategoriaSuplementoDTO.CodigoSede;
                 
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new CategoriaSuplementoDTO()
                                {
                                    CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    CodigoCateSuplemento = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoCateSuplemento")]),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString()
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
