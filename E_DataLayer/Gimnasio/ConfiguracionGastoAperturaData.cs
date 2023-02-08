using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using E_DataModel.Gimnasio;
using E_DataModel.Common;
using System.Data.SqlClient;

namespace E_DataLayer.Gimnasio
{
	public class ConfiguracionGastoAperturaData
	{
		
        public ConfiguracionGastoAperturaDTO BuscarPorCodigoConfiguracionCaja(ConfiguracionGastoAperturaDTO oConfiguracionGastoApertura)
		{
            ConfiguracionGastoAperturaDTO itemDTO = new ConfiguracionGastoAperturaDTO();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspBuscarPorCodigoConfiguracionCaja", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oConfiguracionGastoApertura.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oConfiguracionGastoApertura.CodigoSede;
                   
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new ConfiguracionGastoAperturaDTO()
                                {
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    CodigoTipoGastoCorporativo = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoTipoGastoCorporativo")]),
                                    ActivarCaja = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("ActivarCaja")])
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;
		}
		
		public void Registrar(ConfiguracionGastoAperturaDTO item)
		{
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspRegistrarConfiguracionGastoApertura", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoTipoGastoCorporativo", System.Data.SqlDbType.Int)).Value = item.CodigoTipoGastoCorporativo;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    
                    cmd.ExecuteNonQuery();
                }
            }           
		}
        
		public void Actualizar(ConfiguracionGastoAperturaDTO item)
		{
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspActualizarConfiguracionGastoApertura", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoTipoGastoCorporativo", System.Data.SqlDbType.Int)).Value = item.CodigoTipoGastoCorporativo;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;

                    cmd.ExecuteNonQuery();
                }
            }           
		}

        public void UpdateConfiguracionCaja(ConfiguracionGastoAperturaDTO item)
		{
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspActualizarConfiguracionActivarCaja", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@ActivarCaja", System.Data.SqlDbType.Int)).Value = item.ActivarCaja;

                    cmd.ExecuteNonQuery();
                }
            }
		}
        
	}
}
