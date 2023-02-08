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
    public class EnvioGratisData
    {
        public EnvioGratisDTO ecommerce_uspBuscarEnvioGratis(EnvioGratisDTO oFiltro)
        {
            EnvioGratisDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("ecommerce_uspBuscarEnvioGratis", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoSede;
                  
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new EnvioGratisDTO()
                                {
                                    FechaInicio = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaInicio")]),
                                    FechaFin = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaFin")]),                                    
                                    Valor = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Valor")]),
                                    Estado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Estado")])                                    
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;
        }

        public EnvioGratisDTO ecommerce_uspBuscarEnvioGratisXtienda(EnvioGratisDTO oFiltro)
        {
            EnvioGratisDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("ecommerce_uspBuscarEnvioGratisXtienda", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoSede;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new EnvioGratisDTO()
                                {                                    
                                    Valor = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Valor")])                                    
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;
        }
        
        public void ecommerce_uspRegistrarEnvioGratis(EnvioGratisDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("ecommerce_uspRegistrarEnvioGratis", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = item.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = item.FechaFin;
                    cmd.Parameters.Add(new SqlParameter("@Valor", System.Data.SqlDbType.Decimal)).Value = item.Valor;
                    cmd.Parameters.Add(new SqlParameter("@Estado", System.Data.SqlDbType.Bit)).Value = item.Estado;
                    
                    cmd.ExecuteNonQuery();
                    //ID = cmd.Parameters["@po_CodigoProducto"].Value.ToString();
                }

            }
        }


    }
}
