
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using E_DataModel.Gimnasio;
using E_DataModel.Common;
using System.Data.SqlClient;

namespace E_DataLayer.Gimnasio
{
	public class HorarioPaqueteData
	{
		
        public List<HorarioPaqueteDTO> uspListarDiasHorarioPaquete_visualizar(HorarioPaqueteDTO oHorarioPaqueteDTO)
		{
			List<HorarioPaqueteDTO> lista = new List<HorarioPaqueteDTO>();
           
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarDiasHorarioPaquete_visualizar", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oHorarioPaqueteDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoPaquete", System.Data.SqlDbType.Int)).Value = oHorarioPaqueteDTO.CodigoPaquete;
                  
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new HorarioPaqueteDTO()
                                {
                                    CodigoHP = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoHP")]),
                                    Dia = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Nro")]),
                                    desDia = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    Check = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Estado")]) == 0 ? "" : "checked"
                                });
                            }
                        }

                    }
                }
            }
            return lista;            
		}
        
		public void Registrar(HorarioPaqueteDTO item)
		{
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspRegistrarHorarioPaquete", conn))
                {                         
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoHP", System.Data.SqlDbType.Int)).Value = item.CodigoHP;
                    cmd.Parameters.Add(new SqlParameter("@CodigoPaquete", System.Data.SqlDbType.Int)).Value = item.CodigoPaquete;
                    cmd.Parameters.Add(new SqlParameter("@Dia", System.Data.SqlDbType.Int)).Value = item.Dia;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;
                    cmd.Parameters.Add(new SqlParameter("@Estado", System.Data.SqlDbType.Int)).Value = item.Estado;

                    cmd.ExecuteNonQuery();
                }
            }
            
		}

        public int verificarExiteDiaSemanaCurso(int CodigoUnidadNegocio,int CodigoPaquete, int dia)
        {
            int? campoRetorno = 0;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspVerificarExiteDiaSemanaCurso", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoPaquete", System.Data.SqlDbType.Int)).Value = CodigoPaquete;
                    cmd.Parameters.Add(new SqlParameter("@dia", System.Data.SqlDbType.Int)).Value = dia;
                    cmd.Parameters.AddWithValue("@Existe", campoRetorno).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                            }
                        }
                    }

                    campoRetorno = !DBNull.Value.Equals(cmd.Parameters["@Existe"].Value) ? Convert.ToInt32(cmd.Parameters["@Existe"].Value) : 0;

                }
            }
            return Convert.ToInt32(campoRetorno);
        }

	}
}
