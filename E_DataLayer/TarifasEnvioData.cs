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
    public class TarifasEnvioData
    {
        public List<TarifasEnvioDTO> ecommerce_uspListarAdminTarifasEnvio(TarifasEnvioDTO oFiltro)
        {
            List<TarifasEnvioDTO> lista = new List<TarifasEnvioDTO>();
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("ecommerce_uspListarAdminTarifasEnvio", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Tipo", System.Data.SqlDbType.Int)).Value = oFiltro.TipoUbigeo;
                    cmd.Parameters.Add(new SqlParameter("@Ubigeo", System.Data.SqlDbType.VarChar,100)).Value = oFiltro.Ubigeo;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new TarifasEnvioDTO()
                                {
                                    CodigoUbigeo = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUbigeo")]),
                                    CodigoTarifaEnvio = oIDataReader[oIDataReader.GetOrdinal("CodigoTarifaEnvio")].ToString(),   
                                    Departamento = oIDataReader[oIDataReader.GetOrdinal("Departamento")].ToString(),
                                    Ubigeo = oIDataReader[oIDataReader.GetOrdinal("Ubigeo")].ToString(),                                    
                                    PrecioEnvio = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("PrecioEnvio")]),
                                    TiempoEntrega = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("TiempoEntrega")]),
                                    TipoTiempoEntrega = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("TipoTiempoEntrega")]),
                                    Estado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Estado")])                                    
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }

        public void ecommerce_uspRegistrarAdminTarifasEnvio(TarifasEnvioDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("ecommerce_uspRegistrarAdminTarifasEnvio", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    if (string.IsNullOrEmpty(item.CodigoTarifaEnvio))
                    {
                        cmd.Parameters.Add(new SqlParameter("@CodigoTarifaEnvio", System.Data.SqlDbType.VarChar, 100)).Value = string.Empty;
                    }
                    else
                    {
                        cmd.Parameters.Add(new SqlParameter("@CodigoTarifaEnvio", System.Data.SqlDbType.VarChar, 100)).Value = item.CodigoTarifaEnvio;
                    }

                    cmd.Parameters.Add(new SqlParameter("@Ubigeo", System.Data.SqlDbType.VarChar, 100)).Value = item.Ubigeo;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUbigeo", System.Data.SqlDbType.Int)).Value = item.CodigoUbigeo;
                    cmd.Parameters.Add(new SqlParameter("@PrecioEnvio", System.Data.SqlDbType.Decimal)).Value = item.PrecioEnvio;
                    
                    cmd.Parameters.Add(new SqlParameter("@TiempoEntrega", System.Data.SqlDbType.Int)).Value = item.TiempoEntrega;
                    cmd.Parameters.Add(new SqlParameter("@TipoTiempoEntrega", System.Data.SqlDbType.Int)).Value = item.TipoTiempoEntrega;                    
                    cmd.Parameters.Add(new SqlParameter("@Estado", System.Data.SqlDbType.Bit)).Value = item.Estado;
                    if (!string.IsNullOrEmpty(item.UsuarioCreacion))
                    {
                        cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;
                    }
                    cmd.ExecuteNonQuery();                    
                }

            }
        }

    }
}
