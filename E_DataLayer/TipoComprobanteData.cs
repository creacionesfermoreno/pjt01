using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using E_DataModel;
using E_DataModel.Common;

namespace E_DataLayer
{
    public class TipoComprobanteData
    {
        public List<TipoComprobanteDTO> ecommerce_uspListarTipoComprobante(TipoComprobanteDTO oFiltro)
        {
            List<TipoComprobanteDTO> lista = new List<TipoComprobanteDTO>();
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("ecommerce_uspListarTipoComprobante", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoTipoDocumentoEmpresa", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoTipoDocumentoEmpresa;
                   
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new TipoComprobanteDTO()
                                {
                                    CodigoTipoComprobante = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoTipoComprobante")]),
                                    CodigoTipoDocumentoEmpresa = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoTipoDocumentoEmpresa")]),
                                    Serie = oIDataReader[oIDataReader.GetOrdinal("Serie")].ToString(),
                                    Nombre = oIDataReader[oIDataReader.GetOrdinal("Nombre")].ToString()
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
