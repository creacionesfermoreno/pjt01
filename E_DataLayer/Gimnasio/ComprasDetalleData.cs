using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using E_DataModel.Gimnasio;
using E_DataModel.Common;
using System.Data.SqlClient;

namespace E_DataLayer.Gimnasio
{
	public class ComprasDetalleData
	{
        public List<ComprasDetalleDTO> Listar(ComprasDetalleDTO oComprasDetalleDTO)
		{
			List<ComprasDetalleDTO> lista = new List<ComprasDetalleDTO>();
            
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarDetalleControlIngreso", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oComprasDetalleDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oComprasDetalleDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoIngreso", System.Data.SqlDbType.Int)).Value = oComprasDetalleDTO.CodigoIngreso;
                    
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ComprasDetalleDTO()
                                {
                                    CodigoIngreso = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoIngreso")]),
                                    CodigoDetalleIngreso = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoDetalleIngreso")]),
                                    CodigoProducto = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoProducto")]),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    Importe = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Importe")]),
                                    CantidadIngreso = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadIngreso")]),
                                    CantidadSalida = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadSalida")]),
                                    PrecioCompra = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("PrecioCompra")]),
                                    PrecioVenta = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("PrecioVenta")]),
                                    GananciaUnitaria = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("GananciaUnitaria")]),
                                    GananciaTotal = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("GananciaTotal")]),
                                    FechaCompra = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCompra")])
                                });
                            }
                        }

                    }
                }
            }
            
			return lista;
		}

        public List<ComprasDetalleDTO> uspListarControlDetalleCIngresosRangoFechas(ComprasDetalleDTO oComprasDetalleDTO)
		{
                List<ComprasDetalleDTO> lista = new List<ComprasDetalleDTO>();

                using (var conn = new SqlConnection(Helper.Conexion()))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("uspListarControlDetalleCIngresosRangoFechas", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oComprasDetalleDTO.CodigoUnidadNegocio;
                        cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oComprasDetalleDTO.CodigoSede;
                        cmd.Parameters.Add(new SqlParameter("@CodigoCategoria", System.Data.SqlDbType.Int)).Value = oComprasDetalleDTO.CodigoCategoria;

                        cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = oComprasDetalleDTO.FechaInicio;
                        cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oComprasDetalleDTO.FechaFin;
                        using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                        {
                            if (oIDataReader.HasRows)
                            {
                                while (oIDataReader.Read())
                                {
                                    lista.Add(new ComprasDetalleDTO()
                                    {
                                        CodigoDetalleIngreso = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoDetalleIngreso")]),
                                        CodigoProducto = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoProducto")]),
                                        Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                        Importe = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Importe")]),
                                        CantidadIngreso = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadIngreso")]),
                                        CantidadSalida = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadSalida")]),
                                        PrecioCompra = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("PrecioCompra")]),
                                        PrecioVenta = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("PrecioVenta")]),
                                        GananciaUnitaria = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("GananciaUnitaria")]),
                                        GananciaTotal = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("GananciaTotal")]),
                                        FechaCompra = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCompra")])
                                    });
                                }
                            }

                        }
                    }
                }

                return lista;
		}

        public List<ComprasDetalleDTO> uspListarComprasProductos_Paginacion(ComprasDetalleDTO oComprasDetalleDTO, Paging paging)
		{
            List<ComprasDetalleDTO> lista = new List<ComprasDetalleDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarComprasProductos_Paginacion", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oComprasDetalleDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oComprasDetalleDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoCategoria", System.Data.SqlDbType.Int)).Value = oComprasDetalleDTO.CodigoCategoria;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = oComprasDetalleDTO.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oComprasDetalleDTO.FechaFin;

                    cmd.Parameters.Add(new SqlParameter("@PaginaActual", System.Data.SqlDbType.Int)).Value = paging.PageNumber;
                    cmd.Parameters.Add(new SqlParameter("@TamanioPagina", System.Data.SqlDbType.Int)).Value = paging.PageRecords;
                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ComprasDetalleDTO()
                                {
                                    CodigoDetalleIngreso = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoDetalleIngreso")]),
                                    CodigoProducto = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoProducto")]),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    Importe = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Importe")]),
                                    CantidadIngreso = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadIngreso")]),
                                    CantidadSalida = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadSalida")]),
                                    PrecioCompra = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("PrecioCompra")]),
                                    PrecioVenta = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("PrecioVenta")]),
                                    GananciaUnitaria = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("GananciaUnitaria")]),
                                    GananciaTotal = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("GananciaTotal")]),
                                    FechaCompra = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCompra")])
                                });
                            }
                        }

                    }
                }
            }

            return lista;            
		}
        
        public ComprasDetalleDTO uspListarComprasProductos_NumeroRegistros(ComprasDetalleDTO oComprasSuplementosDTO)
        {
            ComprasDetalleDTO itemDTO = new ComprasDetalleDTO();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarComprasProductos_NumeroRegistros", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oComprasSuplementosDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oComprasSuplementosDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoCategoria", System.Data.SqlDbType.Int)).Value = oComprasSuplementosDTO.CodigoCategoria;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = oComprasSuplementosDTO.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oComprasSuplementosDTO.FechaFin;
                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new ComprasDetalleDTO()
                                {
                                    CantTotal = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadRegistros")])                                 
                                };
                            }
                        }
                    }
                }
            }

            return itemDTO;
        }

        public void Registrar(ComprasDetalleDTO item)
		{
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspRegistrarDetalleCIngreso", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoIngreso", System.Data.SqlDbType.Int)).Value = item.CodigoIngreso;
                    cmd.Parameters.Add(new SqlParameter("@CodigoDetalleIngreso", System.Data.SqlDbType.Int)).Value = item.CodigoDetalleIngreso;
                    cmd.Parameters.Add(new SqlParameter("@CodigoProducto", System.Data.SqlDbType.Int)).Value = item.CodigoProducto;
                    cmd.Parameters.Add(new SqlParameter("@Descripcion", System.Data.SqlDbType.VarChar,200)).Value = item.Descripcion;
                    cmd.Parameters.Add(new SqlParameter("@Importe", System.Data.SqlDbType.Decimal)).Value = item.Importe;

                    cmd.Parameters.Add(new SqlParameter("@CantidadIngreso", System.Data.SqlDbType.Int)).Value = item.CantidadIngreso;
                    cmd.Parameters.Add(new SqlParameter("@CantidadSalida", System.Data.SqlDbType.Int)).Value = item.CantidadSalida;
                    cmd.Parameters.Add(new SqlParameter("@PrecioCompra", System.Data.SqlDbType.Decimal)).Value = item.PrecioCompra;
                    cmd.Parameters.Add(new SqlParameter("@PrecioVenta", System.Data.SqlDbType.Decimal)).Value = item.PrecioVenta;
                    cmd.Parameters.Add(new SqlParameter("@GananciaUnitaria", System.Data.SqlDbType.Decimal)).Value = item.GananciaUnitaria;

                    cmd.Parameters.Add(new SqlParameter("@GananciaTotal", System.Data.SqlDbType.Decimal)).Value = item.GananciaTotal;
                    cmd.Parameters.Add(new SqlParameter("@FechaCompra", System.Data.SqlDbType.DateTime)).Value = item.FechaCompra;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar,100)).Value = item.UsuarioCreacion;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;

                    cmd.ExecuteNonQuery();
                }
            }
		}
        
        public void ActualizarCantidadSalida(ComprasDetalleDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspActualizarDetalleCIngresoCantidad", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoDetalle", System.Data.SqlDbType.Int)).Value = item.CodigoDetalleIngreso;
                    cmd.Parameters.Add(new SqlParameter("@Cantidad", System.Data.SqlDbType.Int)).Value = item.CantidadSalida;
                    cmd.Parameters.Add(new SqlParameter("@flag", System.Data.SqlDbType.Int)).Value = item.Param_Flag;

                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar,100)).Value = item.UsuarioCreacion;
                    cmd.Parameters.Add(new SqlParameter("@CodigoInicioSesion", System.Data.SqlDbType.Int)).Value = item.CodigoInicioSesion;
                   
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Eliminar(ComprasDetalleDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspEliminarComprasDetalleCIngreso", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoDetalleCIngreso", System.Data.SqlDbType.Int)).Value = item.CodigoDetalleIngreso;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar,100)).Value = item.UsuarioCreacion;
                    cmd.Parameters.Add(new SqlParameter("@CodigoInicioSesion", System.Data.SqlDbType.Int)).Value = item.CodigoInicioSesion;
                    
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
