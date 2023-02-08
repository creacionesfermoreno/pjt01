
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using E_DataModel.Gimnasio;
using E_DataModel.Common;
using System.Data.SqlClient;

namespace E_DataLayer.Gimnasio
{
	public class ContratoMensajeData
	{
		
        public List<ContratoMensajeDTO> ListarMensajesMenbresia(ContratoMensajeDTO oContratoMensajeDTO)
		{
            List<ContratoMensajeDTO> lista = new List<ContratoMensajeDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarMensajesPorCodigoMenbresia", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oContratoMensajeDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoMenbresia", System.Data.SqlDbType.Int)).Value = oContratoMensajeDTO.CodigoMenbresia;
                   
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ContratoMensajeDTO()
                                {
                                    Codigo = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Codigo")]),
                                    CodigoMenbresia = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoMenbresia")]),
                                    Ocurrencia = oIDataReader[oIDataReader.GetOrdinal("Ocurrencia")].ToString(),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString(),
                                    FechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")])
                                });
                            }
                        }

                    }
                }
            }

            return lista;            
		}
        
		public void Registrar(ContratoMensajeDTO item)
		{
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspRegistrarMensajesMenbresia", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@Codigo", System.Data.SqlDbType.Int)).Value = item.Codigo;
                    cmd.Parameters.Add(new SqlParameter("@CodigoMenbresia", System.Data.SqlDbType.Int)).Value = item.CodigoMenbresia;
                    cmd.Parameters.Add(new SqlParameter("@Ocurrencia", System.Data.SqlDbType.VarChar, 200)).Value = item.Ocurrencia;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar,100)).Value = item.UsuarioCreacion;

                    cmd.Parameters.Add(new SqlParameter("@CodigoSocio", System.Data.SqlDbType.Int)).Value = item.CodigoSocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoInicioSesion", System.Data.SqlDbType.Int)).Value = item.CodigoInicioSesion;
                   
                    cmd.ExecuteNonQuery();
                }
            }
		}
        
	}
}
