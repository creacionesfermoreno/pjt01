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
    public class ComprobantePagoData
    {

        public List<ComprobantePagoDTO> CentroEntrenamiento_uspTotalPagosProductosRangoFechas(ComprobantePagoDTO oFiltro)
        {
            List<ComprobantePagoDTO> lista = new List<ComprobantePagoDTO>();
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("CentroEntrenamiento_uspTotalPagosProductosRangoFechas", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Fecha", System.Data.SqlDbType.DateTime)).Value = oFiltro.request_FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oFiltro.request_Fin;
                    cmd.Parameters.Add(new SqlParameter("@Vendedores", System.Data.SqlDbType.VarChar,100)).Value = oFiltro.request_Vendedor;
                    cmd.Parameters.Add(new SqlParameter("@Counter", System.Data.SqlDbType.VarChar, 100)).Value = oFiltro.request_Counter;
                    cmd.Parameters.Add(new SqlParameter("@Tipo", System.Data.SqlDbType.Int)).Value = oFiltro.request_Tipo;
                    cmd.Parameters.Add(new SqlParameter("@Turno", System.Data.SqlDbType.Int)).Value = oFiltro.request_Turno;
                    
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ComprobantePagoDTO()
                                {
                                    CodigoMetodoPago = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoMetodoPago")]),
                                    Monto = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Total")].ToString())                                   
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }

        public void ecommerce_uspRegistrarComprobantePago(ComprobantePagoDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("ecommerce_uspRegistrarComprobantePago", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
	                cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
	                cmd.Parameters.Add(new SqlParameter("@CodigoComprobantePago", System.Data.SqlDbType.Int)).Value = item.CodigoComprobantePago;
	                cmd.Parameters.Add(new SqlParameter("@CodigoComprobante", System.Data.SqlDbType.Int)).Value = item.CodigoComprobante;
                    cmd.Parameters.Add(new SqlParameter("@CodigoComprobanteDetalle", System.Data.SqlDbType.Int)).Value = item.CodigoComprobanteDetalle;
	                cmd.Parameters.Add(new SqlParameter("@CodigoCuentaBancaria", System.Data.SqlDbType.Int)).Value = item.CodigoCuentaBancaria;
	                cmd.Parameters.Add(new SqlParameter("@CodigoMetodoPago", System.Data.SqlDbType.Int)).Value = item.CodigoMetodoPago;
	                cmd.Parameters.Add(new SqlParameter("@TipoMoneda", System.Data.SqlDbType.Int)).Value = item.TipoMoneda;
	                cmd.Parameters.Add(new SqlParameter("@Monto", System.Data.SqlDbType.Decimal)).Value = item.Monto;
                    cmd.Parameters.Add(new SqlParameter("@FechaCreacion", System.Data.SqlDbType.DateTime)).Value = item.FechaCreacion;

                    if (item.Nota == null)
                    {
                        cmd.Parameters.Add(new SqlParameter("@Nota", System.Data.SqlDbType.VarChar, 100)).Value = string.Empty;
                    }
                    else
                    {
                        cmd.Parameters.Add(new SqlParameter("@Nota", System.Data.SqlDbType.VarChar, 100)).Value = item.Nota;
                    }
	                                  ;
	                cmd.Parameters.Add(new SqlParameter("@Estado", System.Data.SqlDbType.Int)).Value = item.Estado;

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
