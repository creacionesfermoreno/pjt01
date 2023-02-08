using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using E_DataModel.Gimnasio;
using E_DataModel.Common;
using System.Data.SqlClient;

namespace E_DataLayer.Gimnasio
{
	public class TipoAgendaClienteData
	{
        public List<TipoAgendaClienteDTO> uspListarTipoAgendaCliente()
		{
			List<TipoAgendaClienteDTO> lista = new List<TipoAgendaClienteDTO>();
           
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarTipoAgendaCliente", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                 
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new TipoAgendaClienteDTO()
                                {
                                    Codigo = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Codigo")]),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    Color = oIDataReader[oIDataReader.GetOrdinal("Color")].ToString()                                 
                                });
                            }
                        }

                    }
                }
            }
            return lista;            
		}
		
        public TipoAgendaClienteDTO uspBuscarTipoAgendaClientePorCodigo(TipoAgendaClienteDTO oItem)
		{
			TipoAgendaClienteDTO itemDTO = null;
            
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspBuscarTipoAgendaClientePorCodigo", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Codigo", System.Data.SqlDbType.Int)).Value = oItem.Codigo;
                  
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new TipoAgendaClienteDTO()
                                {
                                    Codigo = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Codigo")]),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString()
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;          
		}
		
		public void Registrar(TipoAgendaClienteDTO item)
		{
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspRegistrarTipoAgendaCliente", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Codigo ", System.Data.SqlDbType.Int)).Value = item.Codigo;                  
                    cmd.Parameters.Add(new SqlParameter("@Descripcion", System.Data.SqlDbType.VarChar, 100)).Value = item.Descripcion;
                    cmd.Parameters.Add(new SqlParameter("@Color ", System.Data.SqlDbType.VarChar,25)).Value = item.Color;

                    cmd.ExecuteNonQuery();
                }
            }
		}


		public void Eliminar(TipoAgendaClienteDTO item)
		{
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspEliminarTipoAgendaCliente", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Codigo ", System.Data.SqlDbType.Int)).Value = item.Codigo;
                 
                    cmd.ExecuteNonQuery();
                }
            }
		}

	}
}
