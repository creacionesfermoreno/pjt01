
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using E_DataModel.Gimnasio;
using E_DataModel.Common;
using System.Data.SqlClient;

namespace E_DataLayer.Gimnasio
{
	public class TiempoMembresiaData
	{
		
        public List<TiempoMembresiaDTO> Listar(TiempoMembresiaDTO oItem)
		{
			List<TiempoMembresiaDTO> lista = new List<TiempoMembresiaDTO>();
           
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarTiempoMembresia", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Descripcion", System.Data.SqlDbType.VarChar, 100)).Value = oItem.Descripcion;
                 
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new TiempoMembresiaDTO()
                                {
                                    CodigoTiempo = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoTiempo")]),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString(),
                                    FechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")]),
                                    Valor = oIDataReader[oIDataReader.GetOrdinal("CodigoTiempo")].ToString() + "|" + Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("ValorDias")]),
                                    ValorDias = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("ValorDias")])                                   
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
