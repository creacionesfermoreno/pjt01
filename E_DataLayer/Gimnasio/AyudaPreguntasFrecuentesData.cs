
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using E_DataModel.Gimnasio;
using E_DataModel.Common;
using System.Data.SqlClient;

namespace E_DataLayer.Gimnasio
{
	public class AyudaPreguntasFrecuentesData
	{
		
        public List<AyudaPreguntasFrecuentesDTO> uspListarAyudaPreguntasFrecuentes()
		{
			List<AyudaPreguntasFrecuentesDTO> lista = new List<AyudaPreguntasFrecuentesDTO>();
           
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarAyudaPreguntasFrecuentes", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                   
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new AyudaPreguntasFrecuentesDTO()
                                {
                                    Codigo = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Codigo")]),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    LinckRespuesta = oIDataReader[oIDataReader.GetOrdinal("LinckRespuesta")].ToString()                                                                    
                                });
                            }
                        }
                    }
                }

            }
            return lista;            
		}
		
	
		public AyudaPreguntasFrecuentesDTO BuscarPorCodigoAyudaPreguntasFrecuentes(AyudaPreguntasFrecuentesDTO oAyudaPreguntasFrecuentes)
		{
			AyudaPreguntasFrecuentesDTO itemDTO = null;
          
			return itemDTO;
		}		
		
		public void Registrar(AyudaPreguntasFrecuentesDTO item)
		{
		
		}

		
		public void Actualizar(AyudaPreguntasFrecuentesDTO item)
		{
		  
		}

		
		public void Eliminar(AyudaPreguntasFrecuentesDTO item)
		{
			
		}
	}
}
