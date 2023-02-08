
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using E_DataModel.Gimnasio;
using E_DataModel.Common;
using System.Data.SqlClient;

namespace E_DataLayer.Gimnasio
{
	public class PagosRopasData
	{
        
        public List<PagosRopasDTO> uspListarPagosRopasPorCodigoSalida(PagosRopasDTO oitem)
        {
            List<PagosRopasDTO> lista = new List<PagosRopasDTO>();
            
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarPagosRopasPorCodigoSalida", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oitem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSalida", System.Data.SqlDbType.Int)).Value = oitem.CodigoSalida;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new PagosRopasDTO()
                                {
                                    CodigoPago = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoPago")]),
                                    CodigoSalida = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSalida")]),
                                    TipoMoneda = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("TipoMoneda")]),
                                    Total = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Total")]),
                                    Saldo = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Debe")]),
                                    Monto = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Monto")]),
                                    TipoCambio = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("TipoCambio")]),
                                    FormaPago = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("FormaPago")]),
                                    SubFormaPago = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("SubFormaPago")]),
                                    NroBoucher = oIDataReader[oIDataReader.GetOrdinal("NroBoucher")].ToString()                                    
                                });
                            }
                        }

                    }
                }
            }
            return lista;            
        }
        
        public List<PagosRopasDTO> uspListarDeudasRopasTotalesDiaRangoFechas_Paginacion(PagosRopasDTO oitem, Paging paging)
        {
            List<PagosRopasDTO> lista = new List<PagosRopasDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarDeudasRopasTotalesDiaRangoFechas_Paginacion", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oitem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oitem.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Fecha", System.Data.SqlDbType.DateTime)).Value = oitem.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oitem.FechaFin;
                    cmd.Parameters.Add(new SqlParameter("@Vendedores", System.Data.SqlDbType.VarChar,100)).Value = oitem.UsuarioCreacion;

                    cmd.Parameters.Add(new SqlParameter("@PaginaActual", System.Data.SqlDbType.Int)).Value = paging.PageNumber;
                    cmd.Parameters.Add(new SqlParameter("@TamanioPagina", System.Data.SqlDbType.Int)).Value = paging.PageRecords;
                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;


                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new PagosRopasDTO()
                                {
                                    CodigoSocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSocio")]),
                                    Cliente = oIDataReader[oIDataReader.GetOrdinal("Cliente")].ToString(),
                                    Telefono = oIDataReader[oIDataReader.GetOrdinal("Telefono")].ToString(),
                                    Dni = oIDataReader[oIDataReader.GetOrdinal("DNI")].ToString(),
                                    Distrito = oIDataReader[oIDataReader.GetOrdinal("Distrito")].ToString(),
                                    Direccion = oIDataReader[oIDataReader.GetOrdinal("Direccion")].ToString(),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    Debe = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("debe")]),
                                    Responsable = oIDataReader[oIDataReader.GetOrdinal("Responsable")].ToString()                                   
                                });
                            }
                        }

                    }
                }
            }
            return lista;            
        }
        
        public PagosRopasDTO uspListarDeudasRopasTotalesDiaRangoFechas_NumeroRegistros(PagosRopasDTO oitem)
        {
            PagosRopasDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarDeudasRopasTotalesDiaRangoFechas_NumeroRegistros", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oitem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oitem.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Fecha", System.Data.SqlDbType.DateTime)).Value = oitem.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oitem.FechaFin;
                    cmd.Parameters.Add(new SqlParameter("@Vendedores", System.Data.SqlDbType.VarChar, 100)).Value = oitem.UsuarioCreacion;
                    
                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                itemDTO = new PagosRopasDTO()
                                {
                                    CantidadRegistros = Convert.ToInt32(reader[reader.GetOrdinal("CantidadRegistros")]),
                                    TotalDeuda = Convert.ToDecimal(reader[reader.GetOrdinal("TotalDeuda")])
                                };
                            }
                        }
                    }
                }
            }

            return itemDTO;            
        }
        
        public void Registrar(PagosRopasDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspRegistrarPagosRopas", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoPago", System.Data.SqlDbType.Int)).Value = item.CodigoPago;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSalida", System.Data.SqlDbType.Int)).Value = item.CodigoSalida;
                    cmd.Parameters.Add(new SqlParameter("@Monto", System.Data.SqlDbType.Decimal)).Value = item.Monto;

                    cmd.Parameters.Add(new SqlParameter("@FechaPago", System.Data.SqlDbType.DateTime)).Value = item.FechaPago;
                    cmd.Parameters.Add(new SqlParameter("@NroComprobante", System.Data.SqlDbType.VarChar, 50)).Value = item.NroComprobante;
                    cmd.Parameters.Add(new SqlParameter("@FormaPago", System.Data.SqlDbType.Int)).Value = item.FormaPago;
                    cmd.Parameters.Add(new SqlParameter("@SubFormaPago", System.Data.SqlDbType.Int)).Value = item.SubFormaPago;
                    cmd.Parameters.Add(new SqlParameter("@NroBoucher", System.Data.SqlDbType.VarChar,100)).Value = item.NroBoucher;

                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar,100)).Value = item.UsuarioCreacion;
                    cmd.Parameters.Add(new SqlParameter("@CodigoTipoComprobante", System.Data.SqlDbType.Int)).Value = item.CodigoTipoComprobante;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSubTipoComprobante", System.Data.SqlDbType.Int)).Value = item.CodigoSubTipoComprobante;
                    cmd.Parameters.Add(new SqlParameter("@CodigoInicioSesion", System.Data.SqlDbType.Int)).Value = item.CodigoInicioSesion;

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void uspActualizarPagoRopasEstado(PagosRopasDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspActualizarPagoRopasEstado", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSalida", System.Data.SqlDbType.Int)).Value = item.CodigoSalida;
                    cmd.Parameters.Add(new SqlParameter("@CodigoPago", System.Data.SqlDbType.Int)).Value = item.CodigoPago;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar,100)).Value = item.UsuarioCreacion;

                    cmd.Parameters.Add(new SqlParameter("@CodigoInicioSesion", System.Data.SqlDbType.Int)).Value = item.CodigoInicioSesion;
                    
                    cmd.ExecuteNonQuery();
                }
            }
        }
        
    }
}
