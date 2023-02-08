using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using E_DataModel.Gimnasio;
using E_DataModel.Common;
using System.Data.SqlClient;

namespace E_DataLayer.Gimnasio
{
	public class ClientesData
	{
        //si se usa
        public List<ClientesDTO> uspListarSocios_PorVendedor_Paginacion(ClientesDTO oClientesDTO, Paging paging)
        {
            List<ClientesDTO> lista = new List<ClientesDTO>();
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarSocios_PorVendedor_Paginacion", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@AsesorVenta", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.Vendedor;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@NombreCliente", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.NombreCompleto;

                    cmd.Parameters.Add(new SqlParameter("@PaginaActual", System.Data.SqlDbType.Int)).Value = paging.PageNumber;
                    cmd.Parameters.Add(new SqlParameter("@TamanioPagina", System.Data.SqlDbType.Int)).Value = paging.PageRecords;
                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ClientesDTO()
                                {
                                    CodigoSocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSocio")]),
                                    Nombres = oIDataReader[oIDataReader.GetOrdinal("Nombres")].ToString(),
                                    Apellidos = oIDataReader[oIDataReader.GetOrdinal("Apellidos")].ToString(),
                                    NombreCompleto = oIDataReader[oIDataReader.GetOrdinal("NombreCompleto")].ToString(),
                                    DNI = oIDataReader[oIDataReader.GetOrdinal("DNI")].ToString(),
                                    Telefono = oIDataReader[oIDataReader.GetOrdinal("Telefono")].ToString(),
                                    Celular = oIDataReader[oIDataReader.GetOrdinal("Celular")].ToString(),
                                    Vendedor = oIDataReader[oIDataReader.GetOrdinal("Vendedor")].ToString()
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }

        //si se usa
        public void ActualizarAsesorComercial_Cliente(ClientesDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspActualizarAsesorComercial_Cliente", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoCliente", System.Data.SqlDbType.Int)).Value = item.CodigoSocio;
                    cmd.Parameters.Add(new SqlParameter("@AsesorComercial", System.Data.SqlDbType.VarChar, 50)).Value = item.Vendedor;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;
                    cmd.Parameters.Add(new SqlParameter("@CodigoInicioSesion", System.Data.SqlDbType.Int)).Value = item.CodigoInicioSesion;

                    cmd.ExecuteNonQuery();
                }
            }

        }

        //si se usa
        public List<ClientesDTO> uspTotalPagosVentasRangoFechas_Appsfit(ClientesDTO oClientesDTO)
        {
            List<ClientesDTO> lista = new List<ClientesDTO>();
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspTotalPagosVentasRangoFechas_Appsfit", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@Fecha", System.Data.SqlDbType.DateTime)).Value = oClientesDTO.Fecha;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oClientesDTO.FechaFinStr;
                    cmd.Parameters.Add(new SqlParameter("@Vendedores", System.Data.SqlDbType.VarChar, 100)).Value = oClientesDTO.Vendedor;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Turno", System.Data.SqlDbType.Int)).Value = oClientesDTO.Turno;
                    cmd.Parameters.Add(new SqlParameter("@CodigoTiempoMembresia", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodTiempoPaquete;
                    cmd.Parameters.Add(new SqlParameter("@AsesorVentas", System.Data.SqlDbType.VarChar, 100)).Value = oClientesDTO.AsesorComercial;
                    cmd.Parameters.Add(new SqlParameter("@TipoIngresoMembresia", System.Data.SqlDbType.VarChar, 50)).Value = oClientesDTO.TipoIngreso;
                    cmd.Parameters.Add(new SqlParameter("@Tipo", System.Data.SqlDbType.Int)).Value = oClientesDTO.Tipo;


                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ClientesDTO()
                                {
                                    Tipo = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Tipo")]),
                                    FormaPago = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("FormaPago")]),
                                    MontoTotalSalida = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Total")])
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }

        public List<ClientesDTO> CentroEntrenamiento_uspConsumoTotalPorCliente(ClientesDTO oClientesDTO)
        {
            List<ClientesDTO> lista = new List<ClientesDTO>();
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("CentroEntrenamiento_uspConsumoTotalPorCliente", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoUnidadNegocio;                  
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSocio", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoSocio;                    
                    cmd.Parameters.Add(new SqlParameter("@DNI", System.Data.SqlDbType.VarChar, 100)).Value = oClientesDTO.DNI;
                    
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ClientesDTO()
                                {
                                    Tipo = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Tipo")]),
                                    FormaPago = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("FormaPago")]),
                                    MontoTotalSalida = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Total")])
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }

        public List<ClientesDTO> CentroEntrenamiento_uspConsumoDetalladoPorCliente(ClientesDTO oClientesDTO)
        {
            List<ClientesDTO> lista = new List<ClientesDTO>();
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("CentroEntrenamiento_uspConsumoDetalladoPorCliente", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSocio", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoSocio;
                    cmd.Parameters.Add(new SqlParameter("@DNI", System.Data.SqlDbType.VarChar, 100)).Value = oClientesDTO.DNI;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ClientesDTO()
                                {
                                    Tipo = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Tipo")]),
                                    DescTipo = oIDataReader[oIDataReader.GetOrdinal("DesTipo")].ToString(),
                                    CodigoSocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSocio")]),
                                    Nombre = oIDataReader[oIDataReader.GetOrdinal("RazonSocial_Sr")].ToString(),
                                    DNI = oIDataReader[oIDataReader.GetOrdinal("RUC_DNI")].ToString(),
                                    Fecha = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaVenta")]),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    Costo = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Costo")]),
                                    Total = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Importe")]),
                                    strFormaPago = oIDataReader[oIDataReader.GetOrdinal("FormaPago")].ToString(),
                                    NroComprobante = oIDataReader[oIDataReader.GetOrdinal("NroComprobante")].ToString(),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString(),
                                    Vendedor = oIDataReader[oIDataReader.GetOrdinal("AsesorDeVentas")].ToString()
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }


        public List<ClientesDTO> uspTotalVentasTurnos_RangoFechas_Appsfit(ClientesDTO oClientesDTO)
        {
            List<ClientesDTO> lista = new List<ClientesDTO>();
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspTotalVentasTurnos_RangoFechas_Appsfit", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Fecha", System.Data.SqlDbType.DateTime)).Value = oClientesDTO.Fecha;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oClientesDTO.FechaFinStr;
                    cmd.Parameters.Add(new SqlParameter("@Vendedores", System.Data.SqlDbType.VarChar, 100)).Value = oClientesDTO.Vendedor;
                    
                    cmd.Parameters.Add(new SqlParameter("@CodigoTiempoMembresia", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodTiempoPaquete;
                    cmd.Parameters.Add(new SqlParameter("@AsesorVentas", System.Data.SqlDbType.VarChar, 100)).Value = oClientesDTO.AsesorComercial;
                    cmd.Parameters.Add(new SqlParameter("@TipoIngresoMembresia", System.Data.SqlDbType.VarChar, 50)).Value = oClientesDTO.TipoIngreso;                    

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ClientesDTO()
                                {
                                    Tema = oIDataReader[oIDataReader.GetOrdinal("Tema")].ToString(),
                                    Tipo = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Tipo")]),
                                    Total = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Total")])
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }


        public ClientesDTO TotalPagosVentas(ClientesDTO oClientesDTO)
        {
            ClientesDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspTotalPagosVentasRangoFechas", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@Fecha", System.Data.SqlDbType.DateTime)).Value = oClientesDTO.Fecha;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oClientesDTO.FechaFinStr;
                    cmd.Parameters.Add(new SqlParameter("@Vendedores", System.Data.SqlDbType.VarChar, 100)).Value = oClientesDTO.Vendedor;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Turno", System.Data.SqlDbType.Int)).Value = oClientesDTO.Turno;
                    cmd.Parameters.Add(new SqlParameter("@CodigoTiempoMembresia", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodTiempoPaquete;
                    cmd.Parameters.Add(new SqlParameter("@AsesorVentas", System.Data.SqlDbType.VarChar,100)).Value = oClientesDTO.AsesorComercial;
                    cmd.Parameters.Add(new SqlParameter("@TipoIngresoMembresia", System.Data.SqlDbType.VarChar,50)).Value = oClientesDTO.TipoIngreso;
                    cmd.Parameters.Add(new SqlParameter("@Tipo", System.Data.SqlDbType.Int)).Value = oClientesDTO.Tipo;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                itemDTO = new ClientesDTO()
                                {
                                    FormaPagoEfectivo = reader[reader.GetOrdinal("FormaPagoEfectivo")].ToString(),
                                    TotalEfectivo = Convert.ToDecimal(reader[reader.GetOrdinal("TotalEfectivo")]),

                                    FormaPagoDebito = reader[reader.GetOrdinal("FormaPagoDebito")].ToString(),
                                    TotalDebito = Convert.ToDecimal(reader[reader.GetOrdinal("TotalDebito")]),
                                    
                                    FormaPagoCredito = reader[reader.GetOrdinal("FormaPagoCredito")].ToString(),
                                    TotalCredito = Convert.ToDecimal(reader[reader.GetOrdinal("TotalCredito")]),

                                    FormaPagoDeposito = reader[reader.GetOrdinal("FormaPagoDeposito")].ToString(),
                                    TotalDeposito = Convert.ToDecimal(reader[reader.GetOrdinal("TotalDeposito")]),

                                    FormaPagoWeb = reader[reader.GetOrdinal("FormaPagoWeb")].ToString(),
                                    TotalWeb = Convert.ToDecimal(reader[reader.GetOrdinal("TotalWeb")]),

                                    TotalVentaDia = Convert.ToDecimal(reader[reader.GetOrdinal("TotalVentaDia")]),
                                    TotalVentaTarde = Convert.ToDecimal(reader[reader.GetOrdinal("TotalVentaTarde")]),
                                    TotalVentaNoche = Convert.ToDecimal(reader[reader.GetOrdinal("TotalVentaNoche")])
                                    
                                };
                            }
                        }
                    }
                }
            }

            return itemDTO;            
        }

        //si se usa
        public ClientesDTO uspTotalPagosSuplementosRangoFechas(ClientesDTO oClientesDTO)
        {
            ClientesDTO itemDTO = null;
            
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspTotalPagosSuplementosRangoFechas", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Fecha", System.Data.SqlDbType.DateTime)).Value = oClientesDTO.Fecha;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oClientesDTO.FechaFinStr;
                    cmd.Parameters.Add(new SqlParameter("@Vendedores", System.Data.SqlDbType.VarChar, 100)).Value = oClientesDTO.Vendedor;
                    cmd.Parameters.Add(new SqlParameter("@Counter", System.Data.SqlDbType.VarChar, 100)).Value = oClientesDTO.Counter;
                    cmd.Parameters.Add(new SqlParameter("@Tipo", System.Data.SqlDbType.Int)).Value = oClientesDTO.Tipo;
                    cmd.Parameters.Add(new SqlParameter("@Turno", System.Data.SqlDbType.Int)).Value = oClientesDTO.Turno;
                    
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                itemDTO = new ClientesDTO()
                                {
                                    FormaPagoEfectivo = reader[reader.GetOrdinal("FormaPagoEfectivo")].ToString(),
                                    TotalEfectivo = Convert.ToDecimal(reader[reader.GetOrdinal("TotalEfectivo")]),

                                    FormaPagoDebito = reader[reader.GetOrdinal("FormaPagoDebito")].ToString(),
                                    TotalDebito = Convert.ToDecimal(reader[reader.GetOrdinal("TotalDebito")]),

                                    FormaPagoCredito = reader[reader.GetOrdinal("FormaPagoCredito")].ToString(),
                                    TotalCredito = Convert.ToDecimal(reader[reader.GetOrdinal("TotalCredito")]),

                                    FormaPagoDeposito = reader[reader.GetOrdinal("FormaPagoDeposito")].ToString(),
                                    TotalDeposito = Convert.ToDecimal(reader[reader.GetOrdinal("TotalDeposito")]),

                                    FormaPagoWeb = reader[reader.GetOrdinal("FormaPagoWeb")].ToString(),
                                    TotalWeb = Convert.ToDecimal(reader[reader.GetOrdinal("TotalWeb")]),

                                    TotalVentaDia = Convert.ToDecimal(reader[reader.GetOrdinal("TotalVentaDia")]),
                                    TotalVentaTarde = Convert.ToDecimal(reader[reader.GetOrdinal("TotalVentaTarde")]),
                                    TotalVentaNoche = Convert.ToDecimal(reader[reader.GetOrdinal("TotalVentaNoche")])

                                };
                            }
                        }
                    }
                }
            }

            return itemDTO;
            
        }

        //si se usa
        public ClientesDTO uspTotalPagosRopasRangoFechas(ClientesDTO oClientesDTO)
        {
            ClientesDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspTotalPagosRopasRangoFechas", conn))
                {                    
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Fecha", System.Data.SqlDbType.DateTime)).Value = oClientesDTO.Fecha;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oClientesDTO.FechaFinStr;
                    cmd.Parameters.Add(new SqlParameter("@Vendedores", System.Data.SqlDbType.VarChar, 100)).Value = oClientesDTO.Vendedor;
                    cmd.Parameters.Add(new SqlParameter("@Counter", System.Data.SqlDbType.VarChar, 100)).Value = oClientesDTO.Counter;
                    cmd.Parameters.Add(new SqlParameter("@Tipo", System.Data.SqlDbType.Int)).Value = oClientesDTO.Tipo;
                    cmd.Parameters.Add(new SqlParameter("@Turno", System.Data.SqlDbType.Int)).Value = oClientesDTO.Turno;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                itemDTO = new ClientesDTO()
                                {
                                    FormaPagoEfectivo = reader[reader.GetOrdinal("FormaPagoEfectivo")].ToString(),
                                    TotalEfectivo = Convert.ToDecimal(reader[reader.GetOrdinal("TotalEfectivo")]),

                                    FormaPagoDebito = reader[reader.GetOrdinal("FormaPagoDebito")].ToString(),
                                    TotalDebito = Convert.ToDecimal(reader[reader.GetOrdinal("TotalDebito")]),

                                    FormaPagoCredito = reader[reader.GetOrdinal("FormaPagoCredito")].ToString(),
                                    TotalCredito = Convert.ToDecimal(reader[reader.GetOrdinal("TotalCredito")]),

                                    FormaPagoDeposito = reader[reader.GetOrdinal("FormaPagoDeposito")].ToString(),
                                    TotalDeposito = Convert.ToDecimal(reader[reader.GetOrdinal("TotalDeposito")]),

                                    FormaPagoWeb = reader[reader.GetOrdinal("FormaPagoWeb")].ToString(),
                                    TotalWeb = Convert.ToDecimal(reader[reader.GetOrdinal("TotalWeb")]),

                                    TotalVentaDia = Convert.ToDecimal(reader[reader.GetOrdinal("TotalVentaDia")]),
                                    TotalVentaTarde = Convert.ToDecimal(reader[reader.GetOrdinal("TotalVentaTarde")]),
                                    TotalVentaNoche = Convert.ToDecimal(reader[reader.GetOrdinal("TotalVentaNoche")])

                                };
                            }
                        }
                    }
                }
            }

            return itemDTO;
        }

        //si se usa
        public ClientesDTO TotalPagosVentasCafeteria(ClientesDTO oClientesDTO)
        {
            ClientesDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspTotalPagosVentasCafeteriaRangoFechas", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@Fecha", System.Data.SqlDbType.DateTime)).Value = oClientesDTO.Fecha;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oClientesDTO.FechaFinStr;
                    cmd.Parameters.Add(new SqlParameter("@Vendedores", System.Data.SqlDbType.VarChar, 100)).Value = oClientesDTO.Vendedor;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Turno", System.Data.SqlDbType.Int)).Value = oClientesDTO.Turno;
                    cmd.Parameters.Add(new SqlParameter("@CodigoTiempoMembresia", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodTiempoPaquete;
                    cmd.Parameters.Add(new SqlParameter("@AsesorVentas", System.Data.SqlDbType.VarChar, 100)).Value = oClientesDTO.AsesorComercial;
                    cmd.Parameters.Add(new SqlParameter("@TipoIngresoMembresia", System.Data.SqlDbType.VarChar, 50)).Value = oClientesDTO.TipoIngreso;
                    cmd.Parameters.Add(new SqlParameter("@Tipo", System.Data.SqlDbType.Int)).Value = oClientesDTO.Tipo;
                    
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                itemDTO = new ClientesDTO()
                                {
                                    FormaPagoEfectivo = reader[reader.GetOrdinal("FormaPagoEfectivo")].ToString(),
                                    TotalEfectivo = Convert.ToDecimal(reader[reader.GetOrdinal("TotalEfectivo")]),

                                    FormaPagoDebito = reader[reader.GetOrdinal("FormaPagoDebito")].ToString(),
                                    TotalDebito = Convert.ToDecimal(reader[reader.GetOrdinal("TotalDebito")]),

                                    FormaPagoCredito = reader[reader.GetOrdinal("FormaPagoCredito")].ToString(),
                                    TotalCredito = Convert.ToDecimal(reader[reader.GetOrdinal("TotalCredito")]),

                                    FormaPagoDeposito = reader[reader.GetOrdinal("FormaPagoDeposito")].ToString(),
                                    TotalDeposito = Convert.ToDecimal(reader[reader.GetOrdinal("TotalDeposito")]),

                                    FormaPagoWeb = reader[reader.GetOrdinal("FormaPagoWeb")].ToString(),
                                    TotalWeb = Convert.ToDecimal(reader[reader.GetOrdinal("TotalWeb")]),

                                    TotalVentaDia = Convert.ToDecimal(reader[reader.GetOrdinal("TotalVentaDia")]),
                                    TotalVentaTarde = Convert.ToDecimal(reader[reader.GetOrdinal("TotalVentaTarde")]),
                                    TotalVentaNoche = Convert.ToDecimal(reader[reader.GetOrdinal("TotalVentaNoche")])

                                };
                            }
                        }
                    }
                }
            }

            return itemDTO;
        }

        //si se usa
        public List<ClientesDTO> ListarSociosLibresAsesores_Paginacion(ClientesDTO oClientesDTO, Paging paging)
        {
            List<ClientesDTO> lista = new List<ClientesDTO>();
            
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarSociosLibresAsesores_Paginacion", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@FiltroBusqueda", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.FiltroBusqueda;
                    cmd.Parameters.Add(new SqlParameter("@FechaCaidaDesde", System.Data.SqlDbType.DateTime)).Value = oClientesDTO.FechaCaidaDesde;
                    cmd.Parameters.Add(new SqlParameter("@FechaCaidaHasta", System.Data.SqlDbType.DateTime)).Value = oClientesDTO.FechaCaidaHasta;
                    
                    cmd.Parameters.Add(new SqlParameter("@PaginaActual", System.Data.SqlDbType.Int)).Value = paging.PageNumber;
                    cmd.Parameters.Add(new SqlParameter("@TamanioPagina", System.Data.SqlDbType.Int)).Value = paging.PageRecords;
                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ClientesDTO()
                                {
                                    CodigoS = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Codigo")]),
                                    CodigoSocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSocio")]),
                                    TipoAgenda = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("TipoAgenda")]),
                                    Nombre = oIDataReader[oIDataReader.GetOrdinal("Nombres")].ToString(),
                                    Apellidos = oIDataReader[oIDataReader.GetOrdinal("Apellidos")].ToString(),
                                    Telefono = oIDataReader[oIDataReader.GetOrdinal("Telefono")].ToString(),
                                    Celular = oIDataReader[oIDataReader.GetOrdinal("Celular")].ToString(),
                                    EstadoCelular = oIDataReader[oIDataReader.GetOrdinal("EstadoCelular")].ToString(),
                                    DescTipoAgenda = oIDataReader[oIDataReader.GetOrdinal("desAgenda")].ToString(),
                                    AsuntoAgenda = oIDataReader[oIDataReader.GetOrdinal("Asunto")].ToString(),
                                    NombreMembresia = oIDataReader[oIDataReader.GetOrdinal("DesMembresia")].ToString(),
                                    CodTiempoPaquete = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("TiempoPaquete")]),
                                    desTiempoPaquete = oIDataReader[oIDataReader.GetOrdinal("DesTiempoPaquete")].ToString(),
                                    Costo = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Costo")]),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString(),
                                    DescTipoCliente = oIDataReader[oIDataReader.GetOrdinal("DesTipoCliente")].ToString(),
                                    DescFechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")]).ToString("dd/MM/yyyy HH:mm tt"),
                                    DescFechaCaida = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCaida")]).ToString("dd/MM/yyyy"),
                                    CantidadCitas = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadCitas")])
                                  
                                });
                                
                            }
                        }

                    }
                }
            }
            return lista;
        }

        //si se usa
        public List<ClientesDTO> Listar(ClientesDTO oClientesDTO)
		{
            if (oClientesDTO == null)
            {
                oClientesDTO = new ClientesDTO();
                oClientesDTO.Genero = string.Empty;
            }
            if (oClientesDTO.Nombres == null)
            {
                oClientesDTO.Nombres = string.Empty;
            }

            List<ClientesDTO> lista = new List<ClientesDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarSocios", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Genero", System.Data.SqlDbType.VarChar,1)).Value = oClientesDTO.Genero;
                    cmd.Parameters.Add(new SqlParameter("@filterNombre", System.Data.SqlDbType.VarChar,100)).Value = oClientesDTO.Nombres;
                    cmd.Parameters.Add(new SqlParameter("@flag", System.Data.SqlDbType.Int)).Value = oClientesDTO.flag;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@AsesorVenta", System.Data.SqlDbType.VarChar, 100)).Value = oClientesDTO.UserAsesorVenta;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoUnidadNegocio;
                    
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ClientesDTO()
                                {
                                    CodigoSocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSocio")]),
                                    Nombres = oIDataReader[oIDataReader.GetOrdinal("Nombres")].ToString(),
                                    Apellidos = oIDataReader[oIDataReader.GetOrdinal("Apellidos")].ToString(),
                                    FechaNacimiento = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaNacimiento")].ToString()),
                                    flagCumpleanios = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaNacimiento")].ToString()).Day == DateTime.Now.Day && Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaNacimiento")].ToString()).Month  == DateTime.Now.Month ? "" : "none",
                                    ImagenUrl = oIDataReader[oIDataReader.GetOrdinal("ImagenUrl")].ToString(),
                                    DescSede = oIDataReader[oIDataReader.GetOrdinal("RazonSocial")].ToString(),
                                    NombreApellido = oIDataReader[oIDataReader.GetOrdinal("Nombres")].ToString() + " " + oIDataReader[oIDataReader.GetOrdinal("Apellidos")].ToString(),
                                    DNI = oIDataReader[oIDataReader.GetOrdinal("DNI")].ToString()
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }

        //si se usa
        public List<ClientesDTO> listaTodosClientesPorTipoAgenda(ClientesDTO oClientesDTO)
		{ 
            List<ClientesDTO> lista = new List<ClientesDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarTodosClientesPorTipoAgenda", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@filterNombre", System.Data.SqlDbType.VarChar, 100)).Value = oClientesDTO.Nombres;
                    cmd.Parameters.Add(new SqlParameter("@Vendedor", System.Data.SqlDbType.VarChar, 100)).Value = oClientesDTO.Vendedor;
                    cmd.Parameters.Add(new SqlParameter("@CodigoOrigen", System.Data.SqlDbType.Int)).Value = oClientesDTO.Origen;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoUnidadNegocio;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ClientesDTO()
                                {
                                    CodigoSocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoCliente")]),
                                    Nombres = oIDataReader[oIDataReader.GetOrdinal("Nombres")].ToString(),
                                    Apellidos = oIDataReader[oIDataReader.GetOrdinal("Apellidos")].ToString(),
                                    NombreCompleto = oIDataReader[oIDataReader.GetOrdinal("Nombres")].ToString() + ", " + oIDataReader[oIDataReader.GetOrdinal("Apellidos")].ToString() + ", su codigo es: " + Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoCliente")]) + " y su DNI es: " + oIDataReader[oIDataReader.GetOrdinal("DNI")].ToString(),
                                    NombreApellido = oIDataReader[oIDataReader.GetOrdinal("Nombres")].ToString() + ", " + oIDataReader[oIDataReader.GetOrdinal("Apellidos")].ToString(),
                                    DNI = oIDataReader[oIDataReader.GetOrdinal("DNI")].ToString(),
                                    Telefono = oIDataReader[oIDataReader.GetOrdinal("Telefono")].ToString(),
                                    Celular = oIDataReader[oIDataReader.GetOrdinal("Celular")].ToString(),
                                    Correo = oIDataReader[oIDataReader.GetOrdinal("Correo")].ToString(),
                                    FechaNacimiento = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaNacimiento")].ToString()),
                                    flagCumpleanios = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaNacimiento")].ToString()).Day == DateTime.Now.Day && Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaNacimiento")].ToString()).Month == DateTime.Now.Month ? "" : "none",
                                    ImagenUrl = oIDataReader[oIDataReader.GetOrdinal("ImagenUrl")].ToString(),
                                    Genero = oIDataReader[oIDataReader.GetOrdinal("Genero")].ToString(),
                                    desCita = oIDataReader[oIDataReader.GetOrdinal("desCita")].ToString()
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }

        //si se usa
        public List<ClientesDTO> uspListarClientesHombres_MujeresEstadistica(ClientesDTO oClientesDTO)
		{
            List<ClientesDTO> lista = new List<ClientesDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarClientesHombres_MujeresEstadistica", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = oClientesDTO.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oClientesDTO.FechaFinaliza;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ClientesDTO()
                                {
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    Cantidad = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Cantidad")]),
                                    porCentajeTotal = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("TotalPorcentaje")])                                    
                                });
                            }
                        }

                    }
                }
            }
            return lista;
		}

        //si se usa
        public List<ClientesDTO> uspListarTotalDia_TardeEstadistica(ClientesDTO oClientesDTO)
		{
            List<ClientesDTO> lista = new List<ClientesDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarTotalDia_TardeEstadistica", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = oClientesDTO.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oClientesDTO.FechaFinaliza;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ClientesDTO()
                                {
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    porCentajeTotal = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("PorcentajeTotal")])
                                });
                            }
                        }

                    }
                }
            }
            return lista;
		}

        //si se usa
        public List<ClientesDTO> uspListarClientesAsistenciaEfectiva_Estadistica(ClientesDTO oClientesDTO)
		{
            List<ClientesDTO> lista = new List<ClientesDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarClientesAsistenciaEfectiva_Estadistica", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = oClientesDTO.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oClientesDTO.FechaFinaliza;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ClientesDTO()
                                {
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    porCentajeTotal = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("PorcentajeTotal")])
                                });
                            }
                        }

                    }
                }
            }
            return lista;
		}

        //si se usa
        public List<ClientesDTO> uspListarEstadisticaTipoContrato(ClientesDTO oClientesDTO)
		{
            List<ClientesDTO> lista = new List<ClientesDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarEstadisticaTipoContrato", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = oClientesDTO.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oClientesDTO.FechaFinaliza;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ClientesDTO()
                                {
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    porCentajeTotal = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("TotalPorcentaje")])
                                });
                            }
                        }

                    }
                }
            }
            return lista;
            
		}

        //si se usa
        public List<ClientesDTO> uspListarEstadisticaTiempoMenbresia(ClientesDTO oClientesDTO)
		{
            List<ClientesDTO> lista = new List<ClientesDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarEstadisticaTiempoMenbresia", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = oClientesDTO.FechaInicio;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ClientesDTO()
                                {
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    porCentajeTotal = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("TotalPorcentaje")])
                                });
                            }
                        }

                    }
                }
            }
            return lista;            
		}


        //si se usa
        public List<ClientesDTO> uspListarEstadistica_AsistenciaporRangoEdades(ClientesDTO oClientesDTO)
        {
            List<ClientesDTO> lista = new List<ClientesDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarEstadistica_AsistenciaporRangoEdades", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicioAsistencia", System.Data.SqlDbType.DateTime)).Value = oClientesDTO.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFinAsistencia", System.Data.SqlDbType.DateTime)).Value = oClientesDTO.FechaFinaliza;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ClientesDTO()
                                {
                                    GrupoEdades = oIDataReader[oIDataReader.GetOrdinal("GrupoEdades")].ToString(),
                                    Genero = oIDataReader[oIDataReader.GetOrdinal("Genero")].ToString(),
                                    Cantidad = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Cantidad")])
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }

        //si se usa
        public List<ClientesDTO> uspListarEstadistica_AsistenciaporHorarios(ClientesDTO oClientesDTO)
        {
            List<ClientesDTO> lista = new List<ClientesDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarEstadistica_AsistenciaporHorarios", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicioAsistencia", System.Data.SqlDbType.DateTime)).Value = oClientesDTO.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFinAsistencia", System.Data.SqlDbType.DateTime)).Value = oClientesDTO.FechaFinaliza;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ClientesDTO()
                                {
                                    IdHorario = oIDataReader[oIDataReader.GetOrdinal("IdHorario")].ToString(),                                    
                                    Cantidad = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Cantidad")])
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }

        //si se usa
        public List<ClientesDTO> uspListarEstadistica_AsistenciaporSemana(ClientesDTO oClientesDTO)
        {
            List<ClientesDTO> lista = new List<ClientesDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarEstadistica_AsistenciaporSemana", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicioAsistencia", System.Data.SqlDbType.DateTime)).Value = oClientesDTO.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFinAsistencia", System.Data.SqlDbType.DateTime)).Value = oClientesDTO.FechaFinaliza;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ClientesDTO()
                                {
                                    IdHorario = oIDataReader[oIDataReader.GetOrdinal("DesSemana")].ToString(),
                                    Cantidad = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Cantidad")])
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }


        //si se usa
        public ClientesDTO uspBuscarSociosConFiltro_Contrato(ClientesDTO oSocios)
		{
            ClientesDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspBuscarSociosConFiltro_Contrato", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oSocios.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oSocios.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSocio", System.Data.SqlDbType.Int)).Value = oSocios.CodigoSocio;
                    cmd.Parameters.Add(new SqlParameter("@Nombres", System.Data.SqlDbType.VarChar, 100)).Value = oSocios.Nombre;
                    cmd.Parameters.Add(new SqlParameter("@Apellidos", System.Data.SqlDbType.VarChar, 100)).Value = oSocios.Apellidos;
                    cmd.Parameters.Add(new SqlParameter("@Dni", System.Data.SqlDbType.VarChar, 100)).Value = oSocios.DNI;
                    
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new ClientesDTO()
                                {
                                    CodigoSocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSocio")]),
                                    Nombres = oIDataReader[oIDataReader.GetOrdinal("Nombres")].ToString(),
                                    Apellidos = oIDataReader[oIDataReader.GetOrdinal("Apellidos")].ToString(),
                                    DNI = oIDataReader[oIDataReader.GetOrdinal("DNI")].ToString(),
                                    Telefono = oIDataReader[oIDataReader.GetOrdinal("Telefono")].ToString(),
                                    Celular = oIDataReader[oIDataReader.GetOrdinal("Celular")].ToString(),
                                    Correo = oIDataReader[oIDataReader.GetOrdinal("Correo")].ToString(),
                                    FechaNacimiento = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaNacimiento")].ToString()),
                                    ImagenUrl = oIDataReader[oIDataReader.GetOrdinal("ImagenUrl")].ToString(),
                                    Estado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Estado")]),
                                    Genero = oIDataReader[oIDataReader.GetOrdinal("Genero")].ToString(),
                                    UrlFacebook = oIDataReader[oIDataReader.GetOrdinal("Facebook")].ToString(),
                                    ReferidoPor = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("ReferidoPor")]),
                                    Direccion = oIDataReader[oIDataReader.GetOrdinal("Direccion")].ToString(),
                                    Distrito = oIDataReader[oIDataReader.GetOrdinal("Distrito")].ToString(),
                                    Ocupacion = oIDataReader[oIDataReader.GetOrdinal("Ocupacion")].ToString(),
                                    DireccionTrabajo = oIDataReader[oIDataReader.GetOrdinal("DireccionTrabajo")].ToString(),
                                    Ubicaciones = oIDataReader[oIDataReader.GetOrdinal("Ubigeo")].ToString(),
                                    TipoCliente = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("TipoCliente")]),
                                    TipoDocumento = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("desTipoDocumento")]),
                                    DescFechaNacimiento =  oIDataReader[oIDataReader.GetOrdinal("DescFechaNacimiento")].ToString(),
                                    EstadoCivil = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("EstadoCivil")]),
                                    EstadoHijos = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("EstadoHijo")]),
                                    TelefonoTrabajo = oIDataReader[oIDataReader.GetOrdinal("Telef_Trabajo")].ToString(),
                                    TipoClienteAgenda = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("NivelSocio")]),
                                    Vendedor = oIDataReader[oIDataReader.GetOrdinal("Vendedor")].ToString(),
                                    OrigenMembresia = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("OrigenMembresia")]),
                                    Nota = oIDataReader[oIDataReader.GetOrdinal("PrimeraNota")].ToString(),
                                    DescFechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")]).ToString("dd/MM/yyyy tt hh:mm"),
                                };
                            }
                        }
                    }
                }
            }

            return itemDTO;
			
		}

        //si se usa
        public ClientesDTO uspBuscarSociosConFiltrosTransferenciaContrato(ClientesDTO oSocios)
		{
            ClientesDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspBuscarSociosConFiltrosTransferenciaContrato", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSocio", System.Data.SqlDbType.Int)).Value = oSocios.CodigoSocio;
                    cmd.Parameters.Add(new SqlParameter("@Nombres", System.Data.SqlDbType.VarChar, 100)).Value = oSocios.Nombre;
                    cmd.Parameters.Add(new SqlParameter("@Apellidos", System.Data.SqlDbType.VarChar, 100)).Value = oSocios.Apellidos;
                    cmd.Parameters.Add(new SqlParameter("@Dni", System.Data.SqlDbType.VarChar, 100)).Value = oSocios.DNI;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oSocios.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oSocios.CodigoUnidadNegocio;
                    
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new ClientesDTO()
                                {
                                    CodigoSocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSocio")]),
                                    Nombres = oIDataReader[oIDataReader.GetOrdinal("Nombres")].ToString(),
                                    Apellidos = oIDataReader[oIDataReader.GetOrdinal("Apellidos")].ToString(),
                                    DNI = oIDataReader[oIDataReader.GetOrdinal("DNI")].ToString(),
                                    Telefono = oIDataReader[oIDataReader.GetOrdinal("Telefono")].ToString(),
                                    Celular = oIDataReader[oIDataReader.GetOrdinal("Celular")].ToString(),
                                    Correo = oIDataReader[oIDataReader.GetOrdinal("Correo")].ToString(),
                                    FechaNacimiento = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaNacimiento")].ToString()),
                                    ImagenUrl = oIDataReader[oIDataReader.GetOrdinal("ImagenUrl")].ToString(),
                                    Estado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Estado")]),
                                    Genero = oIDataReader[oIDataReader.GetOrdinal("Genero")].ToString(),
                                    UrlFacebook = oIDataReader[oIDataReader.GetOrdinal("Facebook")].ToString(),
                                    ReferidoPor = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("ReferidoPor")]),
                                    Direccion = oIDataReader[oIDataReader.GetOrdinal("Direccion")].ToString(),
                                    Distrito = oIDataReader[oIDataReader.GetOrdinal("Distrito")].ToString(),
                                    Ocupacion = oIDataReader[oIDataReader.GetOrdinal("Ocupacion")].ToString(),
                                    DireccionTrabajo = oIDataReader[oIDataReader.GetOrdinal("DireccionTrabajo")].ToString(),
                                    Ubicaciones = oIDataReader[oIDataReader.GetOrdinal("Ubigeo")].ToString(),
                                    TipoCliente = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("TipoCliente")]),
                                    TipoDocumento = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("desTipoDocumento")]),                              
                                    DescFechaNacimiento = oIDataReader[oIDataReader.GetOrdinal("DescFechaNacimiento")].ToString(),
                                    EstadoCivil = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("EstadoCivil")]),
                                    EstadoHijos = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("EstadoHijo")]),
                                    TelefonoTrabajo = oIDataReader[oIDataReader.GetOrdinal("Telef_Trabajo")].ToString(),
                                    NomEstadoCliente = oIDataReader[oIDataReader.GetOrdinal("NomEstadoCliente")].ToString()
                                };
                            }
                        }
                    }
                }
            }

            return itemDTO;            
		}

        //si se usa
        public ClientesDTO BuscarInformacionSociosPorCodigo(ClientesDTO oSocios)
        {
            ClientesDTO itemDTO = null;
           
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspBuscarInformacionSociosPorCodigo", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oSocios.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oSocios.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSocio", System.Data.SqlDbType.Int)).Value = oSocios.CodigoSocio;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new ClientesDTO()
                                {
                                    CodigoSocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSocio")]),
                                    Nombres = oIDataReader[oIDataReader.GetOrdinal("Nombres")].ToString(),
                                    Apellidos = oIDataReader[oIDataReader.GetOrdinal("Apellidos")].ToString(),
                                    DNI = oIDataReader[oIDataReader.GetOrdinal("DNI")].ToString(),
                                    Telefono = oIDataReader[oIDataReader.GetOrdinal("Telefono")].ToString(),
                                    Celular = oIDataReader[oIDataReader.GetOrdinal("Celular")].ToString(),
                                    Correo = oIDataReader[oIDataReader.GetOrdinal("Correo")].ToString(),
                                    DescFechaNacimiento = oIDataReader[oIDataReader.GetOrdinal("DescFechaNacimiento")].ToString(),
                                    ImagenUrl = oIDataReader[oIDataReader.GetOrdinal("ImagenUrl")].ToString(),
                                    ImagenUrlCarnetVacunacion = oIDataReader[oIDataReader.GetOrdinal("ImagenUrlCarnetVacunacion")].ToString(),
                                    Genero = oIDataReader[oIDataReader.GetOrdinal("DescGenero")].ToString(),
                                    UrlFacebook = oIDataReader[oIDataReader.GetOrdinal("Facebook")].ToString(),
                                    DesReferidoPor = oIDataReader[oIDataReader.GetOrdinal("DesReferidoPor")].ToString(),
                                    Direccion = oIDataReader[oIDataReader.GetOrdinal("Direccion")].ToString(),
                                    Distrito = oIDataReader[oIDataReader.GetOrdinal("Distrito")].ToString(),
                                    flagCumpleanios = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaNacimiento")].ToString()).Day == DateTime.Now.Day && Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaNacimiento")].ToString()).Month == DateTime.Now.Month ? "" : "none",
                                    Ocupacion = oIDataReader[oIDataReader.GetOrdinal("Ocupacion")].ToString(),
                                    DesTipoCliente = oIDataReader[oIDataReader.GetOrdinal("DescTipoCliente")].ToString(),
                                    Edad = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Edad")]),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString(),
                                    UserAsesorVenta = oIDataReader[oIDataReader.GetOrdinal("AsesorVenta")].ToString(),
                                    DescFechaCreacion = oIDataReader[oIDataReader.GetOrdinal("DescFechaCreacion")].ToString(),
                                    EstadoCivil = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("EstadoCivil")]),
                                    EstadoHijos = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("EstadoHijo")]),
                                    TelefonoTrabajo = oIDataReader[oIDataReader.GetOrdinal("Telef_Trabajo")].ToString(),
                                    DireccionTrabajo = oIDataReader[oIDataReader.GetOrdinal("DireccionTrabajo")].ToString(),
                                    desSede = oIDataReader[oIDataReader.GetOrdinal("desSede")].ToString().Length < 10 ? oIDataReader[oIDataReader.GetOrdinal("desSede")].ToString() : oIDataReader[oIDataReader.GetOrdinal("desSede")].ToString().Substring(0, 10),
                                    DeudaFiado = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("DeudaFiado")]),
                                    DeudaSuplemento = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("DeudaSuplemento")]),
                                    DeudaRopa = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("DeudaRopa")]),

                                };
                            }
                        }
                    }
                }
            }

            return itemDTO;
        }

        //si se usa
        public ClientesDTO BuscarInfoPorCodSocioFiltro(ClientesDTO oSocios)
        {
            ClientesDTO itemDTO = null;
            
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspBuscarInfoPorCodSocioFiltro", conn))
                {
                    
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oSocios.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oSocios.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@filtro", System.Data.SqlDbType.VarChar,200)).Value = oSocios.Filtro;
                    cmd.CommandTimeout = 120;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new ClientesDTO()
                                {
                                    CodigoSocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSocio")]),
                                    Nombres = oIDataReader[oIDataReader.GetOrdinal("Nombres")].ToString(),
                                    Apellidos = oIDataReader[oIDataReader.GetOrdinal("Apellidos")].ToString(),
                                    DNI = oIDataReader[oIDataReader.GetOrdinal("DNI")].ToString(),
                                    Telefono = oIDataReader[oIDataReader.GetOrdinal("Telefono")].ToString(),
                                    Celular = oIDataReader[oIDataReader.GetOrdinal("Celular")].ToString(),
                                    Correo = oIDataReader[oIDataReader.GetOrdinal("Correo")].ToString(),
                                    DescFechaNacimiento = oIDataReader[oIDataReader.GetOrdinal("DescFechaNacimiento")].ToString(),
                                    ImagenUrl = oIDataReader[oIDataReader.GetOrdinal("ImagenUrl")].ToString(),
                                    ImagenUrlCarnetVacunacion = oIDataReader[oIDataReader.GetOrdinal("ImagenUrlCarnetVacunacion")].ToString(),
                                    Genero = oIDataReader[oIDataReader.GetOrdinal("DescGenero")].ToString(),
                                    UrlFacebook = oIDataReader[oIDataReader.GetOrdinal("Facebook")].ToString(),
                                    DesReferidoPor = oIDataReader[oIDataReader.GetOrdinal("DesReferidoPor")].ToString(),
                                    Direccion = oIDataReader[oIDataReader.GetOrdinal("Direccion")].ToString(),
                                    Distrito = oIDataReader[oIDataReader.GetOrdinal("Distrito")].ToString(),
                                    flagCumpleanios = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaNacimiento")].ToString()).Day == DateTime.Now.Day && Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaNacimiento")].ToString()).Month == DateTime.Now.Month ? "" : "none",
                                    Ocupacion = oIDataReader[oIDataReader.GetOrdinal("Ocupacion")].ToString(),
                                    DesTipoCliente = oIDataReader[oIDataReader.GetOrdinal("DescTipoCliente")].ToString(),
                                    Edad = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Edad")]),
                                    UserAsesorVenta = oIDataReader[oIDataReader.GetOrdinal("AsesorVenta")].ToString(),
                                    DescFechaCreacion = oIDataReader[oIDataReader.GetOrdinal("DescFechaCreacion")].ToString(),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString(),
                                    EstadoCivil = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("EstadoCivil")]),
                                    EstadoHijos = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("EstadoHijo")]),
                                    TelefonoTrabajo = oIDataReader[oIDataReader.GetOrdinal("Telef_Trabajo")].ToString(),
                                    DireccionTrabajo = oIDataReader[oIDataReader.GetOrdinal("DireccionTrabajo")].ToString(),
                                    desSede = oIDataReader[oIDataReader.GetOrdinal("desSede")].ToString(),                                    
                                    DeudaFiado = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("DeudaFiado")]),
                                    DeudaSuplemento = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("DeudaSuplemento")]),
                                    DeudaRopa = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("DeudaRopa")]),
                                };
                            }
                        }
                    }
                }
            }
            
            return itemDTO;
        }
        public ClientesDTO CentroEntrenamiento_uspBuscarPlataformaPersonasFit_InformacionSocioPorCorreo(ClientesDTO oSocios)
        {
            ClientesDTO itemDTO = null;
            
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("CentroEntrenamiento_uspBuscarPlataformaPersonasFit_InformacionSocioPorCorreo", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oSocios.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oSocios.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Correo", System.Data.SqlDbType.VarChar,100)).Value = oSocios.Correo;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new ClientesDTO()
                                {
                                    CodigoSocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSocio")]),
                                    Nombres = oIDataReader[oIDataReader.GetOrdinal("Nombres")].ToString(),
                                    Apellidos = oIDataReader[oIDataReader.GetOrdinal("Apellidos")].ToString(),
                                    DNI = oIDataReader[oIDataReader.GetOrdinal("DNI")].ToString(),
                                    Telefono = oIDataReader[oIDataReader.GetOrdinal("Telefono")].ToString(),
                                    Celular = oIDataReader[oIDataReader.GetOrdinal("Celular")].ToString(),
                                    Correo = oIDataReader[oIDataReader.GetOrdinal("Correo")].ToString(),
                                    DescFechaNacimiento = oIDataReader[oIDataReader.GetOrdinal("DescFechaNacimiento")].ToString(),
                                    ImagenUrl = oIDataReader[oIDataReader.GetOrdinal("ImagenUrl")].ToString(),                                   
                                    desSede = oIDataReader[oIDataReader.GetOrdinal("desSede")].ToString().Length < 10 ? oIDataReader[oIDataReader.GetOrdinal("desSede")].ToString() : oIDataReader[oIDataReader.GetOrdinal("desSede")].ToString().Substring(0, 10)                                   
                                };
                            }
                        }
                    }
                }
            }
            
            return itemDTO;
        }
        //si se usa
		public int Registrar(ClientesDTO item)
		{
            int? campoRetorno = 0;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspRegistrarSociosContrato", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.AddWithValue("@Codigo", campoRetorno).Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSocio", System.Data.SqlDbType.Int)).Value = item.CodigoSocio;
                    cmd.Parameters.Add(new SqlParameter("@Nombres", System.Data.SqlDbType.VarChar,100)).Value = item.Nombres;
                    cmd.Parameters.Add(new SqlParameter("@Apellidos", System.Data.SqlDbType.VarChar,100)).Value = item.Apellidos;
                    
                    cmd.Parameters.Add(new SqlParameter("@DNI", System.Data.SqlDbType.VarChar, 20)).Value = item.DNI;
                    cmd.Parameters.Add(new SqlParameter("@Telefono", System.Data.SqlDbType.VarChar,20)).Value = item.Telefono;
                    cmd.Parameters.Add(new SqlParameter("@Celular", System.Data.SqlDbType.VarChar,20)).Value = item.Celular;
                    cmd.Parameters.Add(new SqlParameter("@Correo", System.Data.SqlDbType.VarChar,100)).Value = item.Correo;
                    cmd.Parameters.Add(new SqlParameter("@FechaNacimiento", System.Data.SqlDbType.DateTime)).Value = item.FechaNacimiento;

                    cmd.Parameters.Add(new SqlParameter("@ImagenUrl", System.Data.SqlDbType.VarChar,200)).Value = item.ImagenUrl;
                    cmd.Parameters.Add(new SqlParameter("@Estado", System.Data.SqlDbType.Bit)).Value = item.Estado;
                    cmd.Parameters.Add(new SqlParameter("@Genero", System.Data.SqlDbType.VarChar, 1)).Value = item.Genero;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;
                    cmd.Parameters.Add(new SqlParameter("@Facebook", System.Data.SqlDbType.VarChar,100)).Value = item.UrlFacebook;

                    cmd.Parameters.Add(new SqlParameter("@ReferidoPor", System.Data.SqlDbType.Int)).Value = item.ReferidoPor;
                    cmd.Parameters.Add(new SqlParameter("@Direccion", System.Data.SqlDbType.VarChar,100)).Value = item.Direccion;
                    cmd.Parameters.Add(new SqlParameter("@Distrito", System.Data.SqlDbType.VarChar, 100)).Value = item.Distrito;
                    cmd.Parameters.Add(new SqlParameter("@Ocupacion", System.Data.SqlDbType.VarChar, 100)).Value = item.Ocupacion;
                    cmd.Parameters.Add(new SqlParameter("@TipoCliente", System.Data.SqlDbType.Int)).Value = item.TipoCliente;

                    cmd.Parameters.Add(new SqlParameter("@Ubigeo", System.Data.SqlDbType.VarChar,50)).Value = item.Ubicaciones;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@TipoDocumento", System.Data.SqlDbType.Int)).Value = item.TipoDocumento;
                    cmd.Parameters.Add(new SqlParameter("@HuellaStr", System.Data.SqlDbType.VarChar)).Value = string.Empty;
                    cmd.Parameters.Add(new SqlParameter("@DireccionTrabajo", System.Data.SqlDbType.VarChar, 200)).Value = item.DireccionTrabajo;

                    cmd.Parameters.Add(new SqlParameter("@EstadoCivil", System.Data.SqlDbType.Int)).Value = item.EstadoCivil;
                    cmd.Parameters.Add(new SqlParameter("@EstadoHijo", System.Data.SqlDbType.Int)).Value = item.EstadoHijos;
                    cmd.Parameters.Add(new SqlParameter("@Telef_Trabajo", System.Data.SqlDbType.VarChar,20)).Value = item.TelefonoTrabajo;
                    cmd.Parameters.Add(new SqlParameter("@NivelSocio", System.Data.SqlDbType.Int)).Value = item.TipoClienteAgenda;
                    cmd.Parameters.Add(new SqlParameter("@CodigoInicioSesion", System.Data.SqlDbType.Int)).Value = item.CodigoInicioSesion;

                    cmd.Parameters.Add(new SqlParameter("@Nota", System.Data.SqlDbType.VarChar, 200)).Value = item.Nota;
                    cmd.ExecuteNonQuery();
                    campoRetorno = Convert.ToInt32(cmd.Parameters["@Codigo"].Value);
                }
            }
		    return Convert.ToInt32(campoRetorno);
		}

        public int uspRegistrarSocios_ImportarExcel(ClientesDTO item)
        {
            var sscsb = new SqlConnectionStringBuilder(Helper.Conexion());
            sscsb.ConnectTimeout = 180;
            using (var conn = new SqlConnection(sscsb.ConnectionString))
            {
                conn.Open();                
                using (var cmd = new SqlCommand("uspRegistrarSocios_ImportarExcel", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.AddWithValue("@CodigoSocio", item.CodigoSocio).Direction = System.Data.ParameterDirection.Output;
                    //cmd.Parameters.Add(new SqlParameter("@CodigoSocio", System.Data.SqlDbType.Int)).Value = item.CodigoSocio;
                    cmd.Parameters.Add(new SqlParameter("@Nombres", System.Data.SqlDbType.VarChar, 100)).Value = item.Nombres;
                    cmd.Parameters.Add(new SqlParameter("@Apellidos", System.Data.SqlDbType.VarChar, 100)).Value = item.Apellidos;
                    cmd.Parameters.Add(new SqlParameter("@Celular", System.Data.SqlDbType.VarChar, 20)).Value = item.Celular;
                    cmd.Parameters.Add(new SqlParameter("@Correo", System.Data.SqlDbType.VarChar, 100)).Value = item.Correo;
                    cmd.Parameters.Add(new SqlParameter("@FechaNacimiento", System.Data.SqlDbType.DateTime)).Value = item.FechaNacimiento;
                    cmd.Parameters.Add(new SqlParameter("@DNI", System.Data.SqlDbType.VarChar, 100)).Value = item.DNI;
                    cmd.Parameters.Add(new SqlParameter("@Direccion", System.Data.SqlDbType.VarChar, 100)).Value = item.Direccion;
                    cmd.Parameters.Add(new SqlParameter("@Ubigeo", System.Data.SqlDbType.VarChar, 100)).Value = item.Ubicaciones;
                    cmd.Parameters.Add(new SqlParameter("@Vendedor", System.Data.SqlDbType.VarChar, 100)).Value = item.Vendedor;
                    cmd.CommandTimeout = 180;
                    cmd.ExecuteNonQuery();
                    item.CodigoSocio = Convert.ToInt32(cmd.Parameters["@CodigoSocio"].Value);
                }
            }
            return item.CodigoSocio;
        }

        //si se usa
        public ClientesDTO GetCantidadSociosPorVendedor(ClientesDTO item)
        {
            int? campoRetorno = 0;
            ClientesDTO dato = new ClientesDTO();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspObtenerCantidadSociosPorVendedor", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Vendedor", System.Data.SqlDbType.Char, 100)).Value = item.UserAsesorVenta;
                    cmd.Parameters.AddWithValue("@CantidadFilas", 0).Direction = System.Data.ParameterDirection.Output;
                  
                    cmd.ExecuteNonQuery();
                    campoRetorno = Convert.ToInt32(cmd.Parameters["@CantidadFilas"].Value);
                }
            }
            
            dato.CantidadSociosMigracion = Convert.ToInt32(campoRetorno);
            
            return dato;
        }
        //si se usa
        public void ActualizarFoto(ClientesDTO item)
        {            
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspActualizarSociosFoto", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSocio", System.Data.SqlDbType.Int)).Value = item.CodigoSocio;
                    cmd.Parameters.Add(new SqlParameter("@ImagenUrl", System.Data.SqlDbType.VarChar, 200)).Value = item.ImagenUrl;
                   
                    cmd.ExecuteNonQuery();
                }
            }
        }
        //si se usa
        public void ActualizarFotoCarnetVacunacion(ClientesDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspActualizarSociosFotoCarnetVacunacion", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSocio", System.Data.SqlDbType.Int)).Value = item.CodigoSocio;
                    cmd.Parameters.Add(new SqlParameter("@ImagenUrl", System.Data.SqlDbType.VarChar, 200)).Value = item.ImagenUrl;

                    cmd.ExecuteNonQuery();
                }
            }
        }
        //si se usa
        public void uspEnviarSocioANuevo(ClientesDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspEnviarSocioANuevo", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSocio", System.Data.SqlDbType.Int)).Value = item.CodigoSocio;
                    cmd.Parameters.Add(new SqlParameter("@Vendedor", System.Data.SqlDbType.VarChar,100)).Value = item.Vendedor;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;
                    cmd.Parameters.Add(new SqlParameter("@CodigoInicioSesion", System.Data.SqlDbType.Int)).Value = item.CodigoInicioSesion;

                    cmd.ExecuteNonQuery();
                }
            }
        }
        //si se usa
        public void RegistrarMigracionSocios(ClientesDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspMigrarSociosAgenda", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Migrador", System.Data.SqlDbType.Char, 100)).Value = item.UsuMigrador;
                    cmd.Parameters.Add(new SqlParameter("@Receptor", System.Data.SqlDbType.Char,100)).Value = item.UsuReceptor;
                    cmd.Parameters.Add(new SqlParameter("@TipMigracion", System.Data.SqlDbType.Int)).Value = item.TipMigracion;
                    cmd.Parameters.Add(new SqlParameter("@cantParcial", System.Data.SqlDbType.Int)).Value = item.CantParcial;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar,100)).Value = item.UsuarioCreacion;
                    cmd.Parameters.Add(new SqlParameter("@CodigoInicioSesion", System.Data.SqlDbType.Int)).Value = item.CodigoInicioSesion;

                    cmd.ExecuteNonQuery();
                }
            }
        }
        //si se usa
        public int Eliminar(ClientesDTO item)
		{
            int? cantidad = 0;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspEliminarSocios", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSocio", System.Data.SqlDbType.Int)).Value = item.CodigoSocio;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar,100)).Value = item.UsuarioCreacion;
                    cmd.Parameters.Add(new SqlParameter("@CodigoInicioSesion", System.Data.SqlDbType.Int)).Value = item.CodigoInicioSesion;
                    cmd.Parameters.AddWithValue("@Cantidad", 0).Direction = System.Data.ParameterDirection.Output;

                    cmd.ExecuteNonQuery();
                    cantidad = Convert.ToInt32(cmd.Parameters["@Cantidad"].Value);
                }
            }
            
            return Convert.ToInt32(cantidad);
		}
        //si se usa
        public int ValidarDNIProspecto(int CodigoUnidadNegocio,string DNI)
        {
            int? campoRetorno = 0;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspValidarDNIProspecto", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@DNI", System.Data.SqlDbType.VarChar, 10)).Value = DNI;
                    cmd.Parameters.AddWithValue("@Existe", 0).Direction = System.Data.ParameterDirection.Output;

                    cmd.ExecuteNonQuery();
                    if (string.IsNullOrEmpty(cmd.Parameters["@Existe"].Value.ToString()))
                    {
                        campoRetorno = 0;
                    }
                    else
                    {
                        campoRetorno = Convert.ToInt32(cmd.Parameters["@Existe"].Value);
                    }
                }
            }            
            return Convert.ToInt32(campoRetorno);
        }
        
        public List<ClientesDTO> uspListarClientesActivos(ClientesDTO oClientesDTO, Paging paging, ref uint recordCount)
        {
            List<ClientesDTO> lista = new List<ClientesDTO>();
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarClientesActivos", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodTiempoPaquete", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodTiempoPaquete;
                    cmd.Parameters.Add(new SqlParameter("@Sexo", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.Genero;
                    cmd.Parameters.Add(new SqlParameter("@EdadRango1", System.Data.SqlDbType.Int)).Value = oClientesDTO.EdadRango1;
                    cmd.Parameters.Add(new SqlParameter("@EdadRango2", System.Data.SqlDbType.Int)).Value = oClientesDTO.EdadRango2;
                    cmd.Parameters.Add(new SqlParameter("@EstadoDeuda", System.Data.SqlDbType.Int)).Value = oClientesDTO.EstadoDeuda;
                    cmd.Parameters.Add(new SqlParameter("@EstadoAsistencia", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.EstadoAsistencia;
                    cmd.Parameters.Add(new SqlParameter("@Ubigeo", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.Ubicaciones;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@AsesorVenta", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.AsesorComercial;
                    cmd.Parameters.Add(new SqlParameter("@Nombre", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.Nombre;
                    cmd.Parameters.Add(new SqlParameter("@Apellidos", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.Apellidos;
                    cmd.Parameters.Add(new SqlParameter("@Codigo", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoS;
                    cmd.Parameters.Add(new SqlParameter("@Dni", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.DNI;
                    cmd.Parameters.Add(new SqlParameter("@Telefono", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.Telefono;
                    cmd.Parameters.Add(new SqlParameter("@Celular", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.Celular;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = oClientesDTO.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oClientesDTO.FechaFinal;
                    //cmd.Parameters.Add(new SqlParameter("@checkTodos", System.Data.SqlDbType.Int)).Value = oClientesDTO.CheckTodos;
                    cmd.Parameters.Add(new SqlParameter("@OrdenAlfabetico", System.Data.SqlDbType.Int)).Value = 1;

                    cmd.Parameters.Add(new SqlParameter("@PaginaActual", System.Data.SqlDbType.Int)).Value = paging.PageNumber;
                    cmd.Parameters.Add(new SqlParameter("@TamanioPagina", System.Data.SqlDbType.Int)).Value = paging.PageRecords;
                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ClientesDTO()
                                {
                                    CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    CodigoMembresia = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoMembresia")]),
                                    CodigoSocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSocio")]),
                                    Nombres = oIDataReader[oIDataReader.GetOrdinal("Nombres")].ToString(),
                                    Apellidos = oIDataReader[oIDataReader.GetOrdinal("Apellidos")].ToString(),
                                    Genero = oIDataReader[oIDataReader.GetOrdinal("Genero")].ToString(),
                                    Ubicaciones = oIDataReader[oIDataReader.GetOrdinal("Ubigeo")].ToString(),
                                    Costo = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Costo")]),
                                    Pago = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("MontoTotal")]),
                                    DNI = oIDataReader[oIDataReader.GetOrdinal("DNI")].ToString(),
                                    Telefono = oIDataReader[oIDataReader.GetOrdinal("Telefono")].ToString(),
                                    Celular = oIDataReader[oIDataReader.GetOrdinal("Celular")].ToString(),
                                    DesFechaInicio = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaInicio")]).ToString("dd/MM/yyyy"),
                                    DesFechaFin = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaFin")]).ToString("dd/MM/yyyy"),
                                    Vendedor = oIDataReader[oIDataReader.GetOrdinal("Vendedor")].ToString(),
                                    VendedorRepartido = oIDataReader[oIDataReader.GetOrdinal("VendedorRepartido")].ToString(),
                                    Correo = oIDataReader[oIDataReader.GetOrdinal("Correo")].ToString(),
                                    desTiempoPaquete = oIDataReader[oIDataReader.GetOrdinal("DesPaquete")].ToString(),
                                    DesCalificacion = oIDataReader[oIDataReader.GetOrdinal("DesCalificacion")].ToString(),
                                    NroIngresoActual = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("NroIngresosActual")])
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }

        public List<ClientesDTO> uspListarClientesActivos_ExportarExcel(ClientesDTO oClientesDTO, Paging paging, ref uint recordCount)
        {
            List<ClientesDTO> lista = new List<ClientesDTO>();
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarClientesActivos_ExportarExcel", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodTiempoPaquete", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodTiempoPaquete;
                    cmd.Parameters.Add(new SqlParameter("@Sexo", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.Genero;
                    cmd.Parameters.Add(new SqlParameter("@EdadRango1", System.Data.SqlDbType.Int)).Value = oClientesDTO.EdadRango1;
                    cmd.Parameters.Add(new SqlParameter("@EdadRango2", System.Data.SqlDbType.Int)).Value = oClientesDTO.EdadRango2;
                    cmd.Parameters.Add(new SqlParameter("@EstadoDeuda", System.Data.SqlDbType.Int)).Value = oClientesDTO.EstadoDeuda;
                    cmd.Parameters.Add(new SqlParameter("@EstadoAsistencia", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.EstadoAsistencia;
                    cmd.Parameters.Add(new SqlParameter("@Ubigeo", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.Ubicaciones;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@AsesorVenta", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.AsesorComercial;
                    cmd.Parameters.Add(new SqlParameter("@Nombre", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.Nombre;
                    cmd.Parameters.Add(new SqlParameter("@Apellidos", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.Apellidos;
                    cmd.Parameters.Add(new SqlParameter("@Codigo", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoS;
                    cmd.Parameters.Add(new SqlParameter("@Dni", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.DNI;
                    cmd.Parameters.Add(new SqlParameter("@Telefono", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.Telefono;
                    cmd.Parameters.Add(new SqlParameter("@Celular", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.Celular;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = oClientesDTO.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oClientesDTO.FechaFinal;
                    //cmd.Parameters.Add(new SqlParameter("@checkTodos", System.Data.SqlDbType.Int)).Value = oClientesDTO.CheckTodos;
                    cmd.Parameters.Add(new SqlParameter("@OrdenAlfabetico", System.Data.SqlDbType.Int)).Value = 1;

                    cmd.Parameters.Add(new SqlParameter("@PaginaActual", System.Data.SqlDbType.Int)).Value = paging.PageNumber;
                    cmd.Parameters.Add(new SqlParameter("@TamanioPagina", System.Data.SqlDbType.Int)).Value = paging.PageRecords;
                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ClientesDTO()
                                {
                                    CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    CodigoMembresia = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoMembresia")]),
                                    CodigoSocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSocio")]),
                                    Nombres = oIDataReader[oIDataReader.GetOrdinal("Nombres")].ToString(),
                                    Apellidos = oIDataReader[oIDataReader.GetOrdinal("Apellidos")].ToString(),
                                    FechaNacimiento = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaNacimiento")]),
                                    Genero = oIDataReader[oIDataReader.GetOrdinal("Genero")].ToString(),
                                    Ubicaciones = oIDataReader[oIDataReader.GetOrdinal("Ubigeo")].ToString(),
                                    Direccion = oIDataReader[oIDataReader.GetOrdinal("Direccion")].ToString(),
                                    Costo = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Costo")]),
                                    Pago = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("MontoTotal")]),
                                    DNI = oIDataReader[oIDataReader.GetOrdinal("DNI")].ToString(),
                                    Telefono = oIDataReader[oIDataReader.GetOrdinal("Telefono")].ToString(),
                                    Celular = oIDataReader[oIDataReader.GetOrdinal("Celular")].ToString(),
                                    DesFechaInicio = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaInicio")]).ToString("dd/MM/yyyy"),
                                    DesFechaFin = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaFin")]).ToString("dd/MM/yyyy"),
                                    Vendedor = oIDataReader[oIDataReader.GetOrdinal("Vendedor")].ToString(),
                                    VendedorRepartido = oIDataReader[oIDataReader.GetOrdinal("VendedorRepartido")].ToString(),
                                    Correo = oIDataReader[oIDataReader.GetOrdinal("Correo")].ToString(),
                                    desTiempoPaquete = oIDataReader[oIDataReader.GetOrdinal("DesPaquete")].ToString(),
                                    DesCalificacion = oIDataReader[oIDataReader.GetOrdinal("DesCalificacion")].ToString(),
                                    NroIngresoActual = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("NroIngresosActual")])
                                    //DesFechaIngreso = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaIngreso")]).ToString("dd/MM/yyyy"),
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }

        public ClientesDTO uspListarClientesActivos_NumeroRegistros(ClientesDTO oClientesDTO)
        {
            ClientesDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarClientesActivos_NumeroRegistros", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodTiempoPaquete", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodTiempoPaquete;
                    cmd.Parameters.Add(new SqlParameter("@Sexo", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.Genero;
                    cmd.Parameters.Add(new SqlParameter("@EdadRango1", System.Data.SqlDbType.Int)).Value = oClientesDTO.EdadRango1;
                    cmd.Parameters.Add(new SqlParameter("@EdadRango2", System.Data.SqlDbType.Int)).Value = oClientesDTO.EdadRango2;

                    cmd.Parameters.Add(new SqlParameter("@EstadoDeuda", System.Data.SqlDbType.Int)).Value = oClientesDTO.EstadoDeuda;
                    cmd.Parameters.Add(new SqlParameter("@EstadoAsistencia", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.EstadoAsistencia;
                    cmd.Parameters.Add(new SqlParameter("@Ubigeo", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.Ubicaciones;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@AsesorVenta", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.AsesorComercial;

                    cmd.Parameters.Add(new SqlParameter("@Nombre", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.Nombre;
                    cmd.Parameters.Add(new SqlParameter("@Apellidos", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.Apellidos;
                    cmd.Parameters.Add(new SqlParameter("@Codigo", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoS;
                    cmd.Parameters.Add(new SqlParameter("@Dni", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.DNI;
                    cmd.Parameters.Add(new SqlParameter("@Telefono", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.Telefono;

                    cmd.Parameters.Add(new SqlParameter("@Celular", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.Celular;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = oClientesDTO.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oClientesDTO.FechaFinal;
                  
                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new ClientesDTO()
                                {
                                    CantidadTotalFilas = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadRegistros")])                                
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;            
        }
        
        public ClientesDTO uspListarSociosLibresAsesores_NumeroRegistros(ClientesDTO oClientesDTO)
        {
            ClientesDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarSociosLibresAsesores_NumeroRegistros", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@FiltroBusqueda", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.FiltroBusqueda;
                    cmd.Parameters.Add(new SqlParameter("@FechaCaidaDesde", System.Data.SqlDbType.DateTime)).Value = oClientesDTO.FechaCaidaDesde;
                    cmd.Parameters.Add(new SqlParameter("@FechaCaidaHasta", System.Data.SqlDbType.DateTime)).Value = oClientesDTO.FechaCaidaHasta;
                    
                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;
                    
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new ClientesDTO()
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

        public ClientesDTO uspListarSocios_PorVendedor_NumeroRegistros(ClientesDTO oClientesDTO)
        {
            ClientesDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarSocios_PorVendedor_NumeroRegistros", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@AsesorVenta", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.Vendedor;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@NombreCliente", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.NombreCompleto;

                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new ClientesDTO()
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

        public ClientesDTO uspNotificacionCumpleaniosSocios_NumeroRegistros(ClientesDTO oClientesDTO)
        {
            ClientesDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspNotificacionCumpleaniosSocios_NumeroRegistros", conn))
                {   
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@flag", System.Data.SqlDbType.Int)).Value = oClientesDTO.flag;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoSede;

                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new ClientesDTO()
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

        public List<ClientesDTO> uspListarClientesInactivos(ClientesDTO oClientesDTO, Paging paging, ref uint recordCount)
        {
            List<ClientesDTO> lista = new List<ClientesDTO>();
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarClientesInactivos", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodTiempoPaquete", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodTiempoPaquete;
                    cmd.Parameters.Add(new SqlParameter("@Sexo", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.Genero;
                    cmd.Parameters.Add(new SqlParameter("@EdadRango1", System.Data.SqlDbType.Int)).Value = oClientesDTO.EdadRango1;
                    cmd.Parameters.Add(new SqlParameter("@EdadRango2", System.Data.SqlDbType.Int)).Value = oClientesDTO.EdadRango2;
                    cmd.Parameters.Add(new SqlParameter("@EstadoDeuda", System.Data.SqlDbType.Int)).Value = oClientesDTO.EstadoDeuda;
                    cmd.Parameters.Add(new SqlParameter("@EstadoAsistencia", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.EstadoAsistencia;
                    cmd.Parameters.Add(new SqlParameter("@Ubigeo", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.Ubicaciones;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@AsesorVenta", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.AsesorComercial;
                    cmd.Parameters.Add(new SqlParameter("@Nombre", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.Nombre;
                    cmd.Parameters.Add(new SqlParameter("@Apellidos", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.Apellidos;
                    cmd.Parameters.Add(new SqlParameter("@Codigo", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoS;
                    cmd.Parameters.Add(new SqlParameter("@Dni", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.DNI;
                    cmd.Parameters.Add(new SqlParameter("@Telefono", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.Telefono;
                    cmd.Parameters.Add(new SqlParameter("@Celular", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.Celular;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = oClientesDTO.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oClientesDTO.FechaFinal;
                    cmd.Parameters.Add(new SqlParameter("@checkTodos", System.Data.SqlDbType.Int)).Value = oClientesDTO.CheckTodos;
                    cmd.Parameters.Add(new SqlParameter("@OrdenAlfabetico", System.Data.SqlDbType.Int)).Value = 1;

                    cmd.Parameters.Add(new SqlParameter("@PaginaActual", System.Data.SqlDbType.Int)).Value = paging.PageNumber;
                    cmd.Parameters.Add(new SqlParameter("@TamanioPagina", System.Data.SqlDbType.Int)).Value = paging.PageRecords;
                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ClientesDTO()
                                {
                                    CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    CodigoMembresia = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoMembresia")]),
                                    CodigoSocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSocio")]),
                                    Nombres = oIDataReader[oIDataReader.GetOrdinal("Nombres")].ToString(),
                                    Apellidos = oIDataReader[oIDataReader.GetOrdinal("Apellidos")].ToString(),
                                    Genero = oIDataReader[oIDataReader.GetOrdinal("Genero")].ToString(),
                                    FechaNacimiento = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaNacimiento")]),
                                    //DescFechaNacimiento = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaNacimiento")]).ToString("dd/MM/yyyy"),
                                    Ubicaciones = oIDataReader[oIDataReader.GetOrdinal("Ubigeo")].ToString(),
                                    Direccion = oIDataReader[oIDataReader.GetOrdinal("Direccion")].ToString(),
                                    Costo = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Costo")]),
                                    Pago = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("MontoTotal")]),
                                    DNI = oIDataReader[oIDataReader.GetOrdinal("DNI")].ToString(),
                                    Telefono = oIDataReader[oIDataReader.GetOrdinal("Telefono")].ToString(),
                                    Celular = oIDataReader[oIDataReader.GetOrdinal("Celular")].ToString(),
                                    EstadoCelular = oIDataReader[oIDataReader.GetOrdinal("EstadoCelular")].ToString(),
                                    DesFechaInicio = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaInicio")]).ToString("dd/MM/yyyy"),
                                    DesFechaFin = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaFin")]).ToString("dd/MM/yyyy"),
                                    Vendedor = oIDataReader[oIDataReader.GetOrdinal("Vendedor")].ToString(),
                                    VendedorRepartido = oIDataReader[oIDataReader.GetOrdinal("VendedorRepartido")].ToString(),
                                    Correo = oIDataReader[oIDataReader.GetOrdinal("Correo")].ToString(),
                                    desTiempoPaquete = oIDataReader[oIDataReader.GetOrdinal("DesPaquete")].ToString(),
                                    DesCalificacion = oIDataReader[oIDataReader.GetOrdinal("DesCalificacion")].ToString(),
                                    NroIngresoActual = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("NroIngresosActual")])
                                });
                            }
                        }

                    }
                }
            }
            return lista;

        }

        public ClientesDTO uspListarClientesInactivos_NumeroRegistros(ClientesDTO oClientesDTO)
        {
            ClientesDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarClientesInactivos_NumeroRegistros", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodTiempoPaquete", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodTiempoPaquete;
                    cmd.Parameters.Add(new SqlParameter("@Sexo", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.Genero;
                    cmd.Parameters.Add(new SqlParameter("@EdadRango1", System.Data.SqlDbType.Int)).Value = oClientesDTO.EdadRango1;
                    cmd.Parameters.Add(new SqlParameter("@EdadRango2", System.Data.SqlDbType.Int)).Value = oClientesDTO.EdadRango2;
                    cmd.Parameters.Add(new SqlParameter("@EstadoDeuda", System.Data.SqlDbType.Int)).Value = oClientesDTO.EstadoDeuda;
                    cmd.Parameters.Add(new SqlParameter("@EstadoAsistencia", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.EstadoAsistencia;
                    cmd.Parameters.Add(new SqlParameter("@Ubigeo", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.Ubicaciones;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@AsesorVenta", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.AsesorComercial;
                    cmd.Parameters.Add(new SqlParameter("@Nombre", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.Nombre;
                    cmd.Parameters.Add(new SqlParameter("@Apellidos", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.Apellidos;
                    cmd.Parameters.Add(new SqlParameter("@Codigo", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoS;
                    cmd.Parameters.Add(new SqlParameter("@Dni", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.DNI;
                    cmd.Parameters.Add(new SqlParameter("@Telefono", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.Telefono;
                    cmd.Parameters.Add(new SqlParameter("@Celular", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.Celular;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = oClientesDTO.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oClientesDTO.FechaFinal;
                    cmd.Parameters.Add(new SqlParameter("@checkTodos", System.Data.SqlDbType.Int)).Value = oClientesDTO.CheckTodos;
                  
                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new ClientesDTO()
                                {
                                    CantidadTotalFilas = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadRegistros")])
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;           
        }

        public List<ClientesDTO> uspListarClientesInactivosSinCita(ClientesDTO oClientesDTO, Paging paging, ref uint recordCount)
        {
            List<ClientesDTO> lista = new List<ClientesDTO>();
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarClientesInactivosSinCita", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodTiempoPaquete", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodTiempoPaquete;
                    cmd.Parameters.Add(new SqlParameter("@Sexo", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.Genero;
                    cmd.Parameters.Add(new SqlParameter("@EdadRango1", System.Data.SqlDbType.Int)).Value = oClientesDTO.EdadRango1;
                    cmd.Parameters.Add(new SqlParameter("@EdadRango2", System.Data.SqlDbType.Int)).Value = oClientesDTO.EdadRango2;
                    cmd.Parameters.Add(new SqlParameter("@EstadoDeuda", System.Data.SqlDbType.Int)).Value = oClientesDTO.EstadoDeuda;
                    cmd.Parameters.Add(new SqlParameter("@EstadoAsistencia", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.EstadoAsistencia;
                    cmd.Parameters.Add(new SqlParameter("@Ubigeo", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.Ubicaciones;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@AsesorVenta", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.AsesorComercial;
                    cmd.Parameters.Add(new SqlParameter("@Nombre", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.Nombre;
                    cmd.Parameters.Add(new SqlParameter("@Apellidos", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.Apellidos;
                    cmd.Parameters.Add(new SqlParameter("@Codigo", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoS;
                    cmd.Parameters.Add(new SqlParameter("@Dni", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.DNI;
                    cmd.Parameters.Add(new SqlParameter("@Telefono", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.Telefono;
                    cmd.Parameters.Add(new SqlParameter("@Celular", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.Celular;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = oClientesDTO.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oClientesDTO.FechaFinal;
                    cmd.Parameters.Add(new SqlParameter("@checkTodos", System.Data.SqlDbType.Int)).Value = oClientesDTO.CheckTodos;
                    cmd.Parameters.Add(new SqlParameter("@OrdenAlfabetico", System.Data.SqlDbType.Int)).Value = 1;

                    cmd.Parameters.Add(new SqlParameter("@PaginaActual", System.Data.SqlDbType.Int)).Value = paging.PageNumber;
                    cmd.Parameters.Add(new SqlParameter("@TamanioPagina", System.Data.SqlDbType.Int)).Value = paging.PageRecords;
                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ClientesDTO()
                                {
                                    CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    CodigoMembresia = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoMembresia")]),
                                    CodigoSocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSocio")]),
                                    Nombres = oIDataReader[oIDataReader.GetOrdinal("Nombres")].ToString(),
                                    Apellidos = oIDataReader[oIDataReader.GetOrdinal("Apellidos")].ToString(),
                                    Genero = oIDataReader[oIDataReader.GetOrdinal("Genero")].ToString(),
                                    Ubicaciones = oIDataReader[oIDataReader.GetOrdinal("Ubigeo")].ToString(),
                                    Costo = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Costo")]),
                                    Pago = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("MontoTotal")]),
                                    DNI = oIDataReader[oIDataReader.GetOrdinal("DNI")].ToString(),
                                    Telefono = oIDataReader[oIDataReader.GetOrdinal("Telefono")].ToString(),
                                    Celular = oIDataReader[oIDataReader.GetOrdinal("Celular")].ToString(),
                                    EstadoCelular = oIDataReader[oIDataReader.GetOrdinal("EstadoCelular")].ToString(),
                                    DesFechaInicio = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaInicio")]).ToString("dd/MM/yyyy"),
                                    DesFechaFin = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaFin")]).ToString("dd/MM/yyyy"),
                                    Vendedor = oIDataReader[oIDataReader.GetOrdinal("Vendedor")].ToString(),
                                    VendedorRepartido = oIDataReader[oIDataReader.GetOrdinal("VendedorRepartido")].ToString(),
                                    Correo = oIDataReader[oIDataReader.GetOrdinal("Correo")].ToString(),
                                    desTiempoPaquete = oIDataReader[oIDataReader.GetOrdinal("DesPaquete")].ToString(),
                                    EstadoCliente = oIDataReader[oIDataReader.GetOrdinal("flagReinslibre")].ToString() == "no" ? "ocupado" : "liberado",
                                    ColorAgenda = oIDataReader[oIDataReader.GetOrdinal("flagReinslibre")].ToString() == "no" ? "rgb(255,0,0)" : "rgb(6,140,49)",
                                    flagReinslibre = oIDataReader[oIDataReader.GetOrdinal("flagReinslibre")].ToString(),
                                    NroIngresoActual = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("NroIngresosActual")])
                                }); 
                            }
                        }

                    }
                }
            }
            return lista;

        }

        public ClientesDTO uspListarClientesInactivosSinCita_NumeroRegistros(ClientesDTO oClientesDTO)
        {
            ClientesDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarClientesInactivosSinCita_NumeroRegistros", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodTiempoPaquete", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodTiempoPaquete;
                    cmd.Parameters.Add(new SqlParameter("@Sexo", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.Genero;
                    cmd.Parameters.Add(new SqlParameter("@EdadRango1", System.Data.SqlDbType.Int)).Value = oClientesDTO.EdadRango1;
                    cmd.Parameters.Add(new SqlParameter("@EdadRango2", System.Data.SqlDbType.Int)).Value = oClientesDTO.EdadRango2;
                    cmd.Parameters.Add(new SqlParameter("@EstadoDeuda", System.Data.SqlDbType.Int)).Value = oClientesDTO.EstadoDeuda;
                    cmd.Parameters.Add(new SqlParameter("@EstadoAsistencia", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.EstadoAsistencia;
                    cmd.Parameters.Add(new SqlParameter("@Ubigeo", System.Data.SqlDbType.VarChar,100)).Value = oClientesDTO.Ubicaciones;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@AsesorVenta", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.AsesorComercial;
                    cmd.Parameters.Add(new SqlParameter("@Nombre", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.Nombre;
                    cmd.Parameters.Add(new SqlParameter("@Apellidos", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.Apellidos;
                    cmd.Parameters.Add(new SqlParameter("@Codigo", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoS;
                    cmd.Parameters.Add(new SqlParameter("@Dni", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.DNI;
                    cmd.Parameters.Add(new SqlParameter("@Telefono", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.Telefono;
                    cmd.Parameters.Add(new SqlParameter("@Celular", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.Celular;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = oClientesDTO.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oClientesDTO.FechaFinal;
                    cmd.Parameters.Add(new SqlParameter("@checkTodos", System.Data.SqlDbType.Int)).Value = oClientesDTO.CheckTodos;
                    
                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new ClientesDTO()
                                {
                                    CantidadTotalFilas = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadRegistros")]),
                                    CantidadVendedoresActivos = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadVendedores")]),
                                    CantidadRepartidoInactivosPorVendedor = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadRepartido")])                   
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;            
        }
        
        public List<ClientesDTO> uspListarClientesPorVencer(ClientesDTO oClientesDTO, Paging paging, ref uint recordCount)
        {
            List<ClientesDTO> lista = new List<ClientesDTO>();
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarClientesPorVencer", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodTiempoPaquete", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodTiempoPaquete;
                    cmd.Parameters.Add(new SqlParameter("@Sexo", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.Genero;
                    cmd.Parameters.Add(new SqlParameter("@EdadRango1", System.Data.SqlDbType.Int)).Value = oClientesDTO.EdadRango1;
                    cmd.Parameters.Add(new SqlParameter("@EdadRango2", System.Data.SqlDbType.Int)).Value = oClientesDTO.EdadRango2;
                    cmd.Parameters.Add(new SqlParameter("@EstadoDeuda", System.Data.SqlDbType.Int)).Value = oClientesDTO.EstadoDeuda;
                    cmd.Parameters.Add(new SqlParameter("@EstadoAsistencia", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.EstadoAsistencia;
                    cmd.Parameters.Add(new SqlParameter("@Ubigeo", System.Data.SqlDbType.VarChar,100)).Value = oClientesDTO.Ubicaciones;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@AsesorVenta", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.AsesorComercial;
                    cmd.Parameters.Add(new SqlParameter("@Nombre", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.Nombre;
                    cmd.Parameters.Add(new SqlParameter("@Apellidos", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.Apellidos;
                    cmd.Parameters.Add(new SqlParameter("@Codigo", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoS;
                    cmd.Parameters.Add(new SqlParameter("@Dni", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.DNI;
                    cmd.Parameters.Add(new SqlParameter("@Telefono", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.Telefono;
                    cmd.Parameters.Add(new SqlParameter("@Celular", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.Celular;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = oClientesDTO.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oClientesDTO.FechaFinal;
                    cmd.Parameters.Add(new SqlParameter("@checkTodos", System.Data.SqlDbType.Int)).Value = oClientesDTO.CheckTodos;
                    cmd.Parameters.Add(new SqlParameter("@OrdenAlfabetico", System.Data.SqlDbType.Int)).Value = 1;

                    cmd.Parameters.Add(new SqlParameter("@PaginaActual", System.Data.SqlDbType.Int)).Value = paging.PageNumber;
                    cmd.Parameters.Add(new SqlParameter("@TamanioPagina", System.Data.SqlDbType.Int)).Value = paging.PageRecords;
                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ClientesDTO()
                                {
                                    CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    CodigoMembresia = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoMembresia")]),
                                    CodigoSocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSocio")]),
                                    Nombres = oIDataReader[oIDataReader.GetOrdinal("Nombres")].ToString(),
                                    Apellidos = oIDataReader[oIDataReader.GetOrdinal("Apellidos")].ToString(),
                                    Genero = oIDataReader[oIDataReader.GetOrdinal("Genero")].ToString(),
                                    FechaNacimiento = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaNacimiento")]),
                                    //DescFechaNacimiento = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaNacimiento")]).ToString("dd/MM/yyyy"),
                                    Ubicaciones = oIDataReader[oIDataReader.GetOrdinal("Ubigeo")].ToString(),
                                    Costo = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Costo")]),
                                    Pago = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("MontoTotal")]),
                                    DNI = oIDataReader[oIDataReader.GetOrdinal("DNI")].ToString(),
                                    Telefono = oIDataReader[oIDataReader.GetOrdinal("Telefono")].ToString(),
                                    Celular = oIDataReader[oIDataReader.GetOrdinal("Celular")].ToString(),
                                    DesFechaInicio = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaInicio")]).ToString("dd/MM/yyyy"),
                                    DesFechaFin = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaFin")]).ToString("dd/MM/yyyy"),
                                    Vendedor = oIDataReader[oIDataReader.GetOrdinal("Vendedor")].ToString(),
                                    VendedorRepartido = oIDataReader[oIDataReader.GetOrdinal("VendedorRepartido")].ToString(),
                                    Correo = oIDataReader[oIDataReader.GetOrdinal("Correo")].ToString(),
                                    desTiempoPaquete = oIDataReader[oIDataReader.GetOrdinal("DesPaquete")].ToString(),
                                    DesCalificacion = oIDataReader[oIDataReader.GetOrdinal("DesCalificacion")].ToString(),
                                    NroIngresoActual = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("NroIngresosActual")])
                                });
                            }
                        }

                    }
                }
            }
            return lista;

        }

        public ClientesDTO uspListarClientesPorVencer_NumeroRegistros(ClientesDTO oClientesDTO)
        {
            ClientesDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarClientesPorVencer_NumeroRegistros", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodTiempoPaquete", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodTiempoPaquete;
                    cmd.Parameters.Add(new SqlParameter("@Sexo", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.Genero;
                    cmd.Parameters.Add(new SqlParameter("@EdadRango1", System.Data.SqlDbType.Int)).Value = oClientesDTO.EdadRango1;
                    cmd.Parameters.Add(new SqlParameter("@EdadRango2", System.Data.SqlDbType.Int)).Value = oClientesDTO.EdadRango2;
                    cmd.Parameters.Add(new SqlParameter("@EstadoDeuda", System.Data.SqlDbType.Int)).Value = oClientesDTO.EstadoDeuda;
                    cmd.Parameters.Add(new SqlParameter("@EstadoAsistencia", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.EstadoAsistencia;
                    cmd.Parameters.Add(new SqlParameter("@Ubigeo", System.Data.SqlDbType.VarChar, 100)).Value = oClientesDTO.Ubicaciones;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@AsesorVenta", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.AsesorComercial;
                    cmd.Parameters.Add(new SqlParameter("@Nombre", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.Nombre;
                    cmd.Parameters.Add(new SqlParameter("@Apellidos", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.Apellidos;
                    cmd.Parameters.Add(new SqlParameter("@Codigo", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoS;
                    cmd.Parameters.Add(new SqlParameter("@Dni", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.DNI;
                    cmd.Parameters.Add(new SqlParameter("@Telefono", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.Telefono;
                    cmd.Parameters.Add(new SqlParameter("@Celular", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.Celular;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = oClientesDTO.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oClientesDTO.FechaFinal;
                    cmd.Parameters.Add(new SqlParameter("@checkTodos", System.Data.SqlDbType.Int)).Value = oClientesDTO.CheckTodos;
                   
                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new ClientesDTO()
                                {
                                    CantidadTotalFilas = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadRegistros")])
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;            
        }

        public List<ClientesDTO> uspListarProspectosPostVenta_Paginacion(ClientesDTO oClientesDTO, Paging paging, ref uint recordCount)
        {
            List<ClientesDTO> lista = new List<ClientesDTO>();
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarProspectosPostVenta_Paginacion", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = oClientesDTO.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oClientesDTO.FechaFinal;
                    cmd.Parameters.Add(new SqlParameter("@Nombre", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.Nombre;
                    cmd.Parameters.Add(new SqlParameter("@PaginaActual", System.Data.SqlDbType.Int)).Value = paging.PageNumber;
                    cmd.Parameters.Add(new SqlParameter("@TamanioPagina", System.Data.SqlDbType.Int)).Value = paging.PageRecords;
                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ClientesDTO()
                                {
                                    CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    CodigoProspecto = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoProspecto")]),
                                    Nombres = oIDataReader[oIDataReader.GetOrdinal("Nombres")].ToString(),
                                    Apellidos = oIDataReader[oIDataReader.GetOrdinal("Apellidos")].ToString(),
                                    DNI = oIDataReader[oIDataReader.GetOrdinal("DNI")].ToString(),
                                    Telefono = oIDataReader[oIDataReader.GetOrdinal("Telefono")].ToString(),
                                    Celular = oIDataReader[oIDataReader.GetOrdinal("Celular")].ToString(),
                                    MembresiaDescripcion = oIDataReader[oIDataReader.GetOrdinal("DesPaquete")].ToString(),
                                    Precio = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Precio")]),
                                    DesTipoCliente  = oIDataReader[oIDataReader.GetOrdinal("TipoCliente")].ToString(),
                                    Vendedor = oIDataReader[oIDataReader.GetOrdinal("Vendedor")].ToString(),
                                    DesTipoSocio = oIDataReader[oIDataReader.GetOrdinal("Tipo")].ToString(),
                                    FechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")]),
                                    DesCalificacion = oIDataReader[oIDataReader.GetOrdinal("DesCalificacion")].ToString()
                                });
                            }
                        }

                    }
                }
            }
            return lista;

        }

        public ClientesDTO uspListarProspectosPostVenta_NumeroRegistros(ClientesDTO oClientesDTO)
        {
            ClientesDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarProspectosPostVenta_NumeroRegistros", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = oClientesDTO.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oClientesDTO.FechaFinal;
                    cmd.Parameters.Add(new SqlParameter("@Nombre", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.Nombre;
                 
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new ClientesDTO()
                                {
                                    CantidadTotalFilas = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadRegistros")])
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;           
        }
        
        public ClientesDTO uspListarClientesAgendaComercialReinscripcion_NumeroRegistros(ClientesDTO oClientesDTO)
        {
            ClientesDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarClientesAgendaComercialReinscripcion_NumeroRegistros", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodTiempoPaquete", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodTiempoPaquete;
                    cmd.Parameters.Add(new SqlParameter("@Sexo", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.Genero;
                    cmd.Parameters.Add(new SqlParameter("@EdadRango1", System.Data.SqlDbType.Int)).Value = oClientesDTO.EdadRango1;
                    cmd.Parameters.Add(new SqlParameter("@EdadRango2", System.Data.SqlDbType.Int)).Value = oClientesDTO.EdadRango2;
                    cmd.Parameters.Add(new SqlParameter("@EstadoDeuda", System.Data.SqlDbType.Int)).Value = oClientesDTO.EstadoDeuda;
                    cmd.Parameters.Add(new SqlParameter("@EstadoAsistencia", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.EstadoAsistencia;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@AsesorVenta", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.AsesorComercial;
                    cmd.Parameters.Add(new SqlParameter("@Buscador", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.Nombre;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = oClientesDTO.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oClientesDTO.FechaFinal;
                    
                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new ClientesDTO()
                                {
                                    CantidadTotalFilas = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadRegistros")])
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;            
        }

        public ClientesDTO uspListarClientesAgendaComercialRenovacion_NumeroRegistros(ClientesDTO oClientesDTO)
        {
            ClientesDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarClientesAgendaComercialRenovacion_NumeroRegistros", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodTiempoPaquete", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodTiempoPaquete;
                    cmd.Parameters.Add(new SqlParameter("@Sexo", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.Genero;
                    cmd.Parameters.Add(new SqlParameter("@EdadRango1", System.Data.SqlDbType.Int)).Value = oClientesDTO.EdadRango1;
                    cmd.Parameters.Add(new SqlParameter("@EdadRango2", System.Data.SqlDbType.Int)).Value = oClientesDTO.EdadRango2;
                    cmd.Parameters.Add(new SqlParameter("@EstadoDeuda", System.Data.SqlDbType.Int)).Value = oClientesDTO.EstadoDeuda;
                    cmd.Parameters.Add(new SqlParameter("@EstadoAsistencia", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.EstadoAsistencia;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@AsesorVenta", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.AsesorComercial;
                    cmd.Parameters.Add(new SqlParameter("@Buscador", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.Nombre;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = oClientesDTO.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oClientesDTO.FechaFinal;
                    
                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new ClientesDTO()
                                {
                                    CantidadTotalFilas = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadRegistros")])
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;
         
        }

        public ClientesDTO uspListarClientesAgendaComercialRenovacionInscritos_NumeroRegistros(ClientesDTO oClientesDTO)
        {
            ClientesDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarClientesAgendaComercialRenovacionInscritos_NumeroRegistros", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodTiempoPaquete", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodTiempoPaquete;
                    cmd.Parameters.Add(new SqlParameter("@Sexo", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.Genero;
                    cmd.Parameters.Add(new SqlParameter("@EdadRango1", System.Data.SqlDbType.Int)).Value = oClientesDTO.EdadRango1;
                    cmd.Parameters.Add(new SqlParameter("@EdadRango2", System.Data.SqlDbType.Int)).Value = oClientesDTO.EdadRango2;
                    cmd.Parameters.Add(new SqlParameter("@EstadoDeuda", System.Data.SqlDbType.Int)).Value = oClientesDTO.EstadoDeuda;
                    cmd.Parameters.Add(new SqlParameter("@EstadoAsistencia", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.EstadoAsistencia;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@AsesorVenta", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.AsesorComercial;
                    cmd.Parameters.Add(new SqlParameter("@Buscador", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.Nombre;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = oClientesDTO.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oClientesDTO.FechaFinal;

                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new ClientesDTO()
                                {
                                    CantidadTotalFilas = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadRegistros")])
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;
        }
        
        public List<ClientesDTO> uspNotificacionCumpleaniosSocios_Paginacion(ClientesDTO oClientesDTO, Paging paging)
        {
            List<ClientesDTO> lista = new List<ClientesDTO>();
           
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspNotificacionCumpleaniosSocios_Paginacion", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@flag", System.Data.SqlDbType.Int)).Value = oClientesDTO.flag;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoSede;
                    
                    cmd.Parameters.Add(new SqlParameter("@PaginaActual", System.Data.SqlDbType.Int)).Value = paging.PageNumber;
                    cmd.Parameters.Add(new SqlParameter("@TamanioPagina", System.Data.SqlDbType.Int)).Value = paging.PageRecords;
                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ClientesDTO()
                                {
                                  
                                    CodigoSocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSocio")]),
                                    NombreCompleto = oIDataReader[oIDataReader.GetOrdinal("DesSocio")].ToString(),
                                    Telefono = oIDataReader[oIDataReader.GetOrdinal("Telefono")].ToString(),
                                    Celular = oIDataReader[oIDataReader.GetOrdinal("Celular")].ToString(),
                                    EstadoCelular = oIDataReader[oIDataReader.GetOrdinal("EstadoCelular")].ToString(),
                                    Edad = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Edad")]),
                                    FechaNacimiento = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaNacimiento")]),
                                    DesFechaNacimiento = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaNacimiento")]).ToString("dd/MM/yyyy"),
                                    ImagenUrl = oIDataReader[oIDataReader.GetOrdinal("ImagenUrl")].ToString(),
                                    UrlFacebook = oIDataReader[oIDataReader.GetOrdinal("Facebook")].ToString(),
                                    Direccion = oIDataReader[oIDataReader.GetOrdinal("Direccion")].ToString(),
                                    DesCalificacion = oIDataReader[oIDataReader.GetOrdinal("DesCalificacion")].ToString()
                                });
                            }
                        }

                    }
                }
            }
          
            return lista;
        }
        
        public List<ClientesDTO> uspListarClientesPorTodos(ClientesDTO oClientesDTO, Paging paging, ref uint recordCount)
        {
            List<ClientesDTO> lista = new List<ClientesDTO>();
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarClientesPorTodos", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodTiempoPaquete", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodTiempoPaquete;
                    cmd.Parameters.Add(new SqlParameter("@Sexo", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.Genero;
                    cmd.Parameters.Add(new SqlParameter("@EdadRango1", System.Data.SqlDbType.Int)).Value = oClientesDTO.EdadRango1;
                    cmd.Parameters.Add(new SqlParameter("@EdadRango2", System.Data.SqlDbType.Int)).Value = oClientesDTO.EdadRango2;
                    cmd.Parameters.Add(new SqlParameter("@EstadoDeuda", System.Data.SqlDbType.Int)).Value = oClientesDTO.EstadoDeuda;
                    cmd.Parameters.Add(new SqlParameter("@EstadoAsistencia", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.EstadoAsistencia;
                    cmd.Parameters.Add(new SqlParameter("@Ubigeo", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.Ubicaciones;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@AsesorVenta", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.AsesorComercial;
                    cmd.Parameters.Add(new SqlParameter("@Nombre", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.Nombre;
                    cmd.Parameters.Add(new SqlParameter("@Apellidos", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.Apellidos;
                    cmd.Parameters.Add(new SqlParameter("@Codigo", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoS;
                    cmd.Parameters.Add(new SqlParameter("@Dni", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.DNI;
                    cmd.Parameters.Add(new SqlParameter("@Telefono", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.Telefono;
                    cmd.Parameters.Add(new SqlParameter("@Celular", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.Celular;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = oClientesDTO.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oClientesDTO.FechaFinal;
                    cmd.Parameters.Add(new SqlParameter("@checkTodos", System.Data.SqlDbType.Int)).Value = oClientesDTO.CheckTodos;
                    cmd.Parameters.Add(new SqlParameter("@OrdenAlfabetico", System.Data.SqlDbType.Int)).Value = 1;
                    
                    cmd.Parameters.Add(new SqlParameter("@PaginaActual", System.Data.SqlDbType.Int)).Value = paging.PageNumber;
                    cmd.Parameters.Add(new SqlParameter("@TamanioPagina", System.Data.SqlDbType.Int)).Value = paging.PageRecords;
                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ClientesDTO()
                                {
                                    CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    CodigoMembresia = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoMembresia")]),
                                    CodigoSocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSocio")]),
                                    Nombres = oIDataReader[oIDataReader.GetOrdinal("Nombres")].ToString(),
                                    Apellidos = oIDataReader[oIDataReader.GetOrdinal("Apellidos")].ToString(),
                                    Genero = oIDataReader[oIDataReader.GetOrdinal("Genero")].ToString(),
                                    DescFechaNacimiento = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaNacimiento")]).ToString("dd/MM/yyyy"),
                                    Ubicaciones = oIDataReader[oIDataReader.GetOrdinal("Ubigeo")].ToString(),
                                    Costo = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Costo")]),
                                    Pago = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("MontoTotal")]),
                                    DNI = oIDataReader[oIDataReader.GetOrdinal("DNI")].ToString(),
                                    Telefono = oIDataReader[oIDataReader.GetOrdinal("Telefono")].ToString(),
                                    Celular = oIDataReader[oIDataReader.GetOrdinal("Celular")].ToString(),
                                    DesFechaInicio = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaInicio")]).ToString("dd/MM/yyyy"),
                                    DesFechaFin = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaFin")]).ToString("dd/MM/yyyy"),
                                    Vendedor = oIDataReader[oIDataReader.GetOrdinal("Vendedor")].ToString(),
                                    VendedorRepartido = oIDataReader[oIDataReader.GetOrdinal("VendedorRepartido")].ToString(),
                                    Correo = oIDataReader[oIDataReader.GetOrdinal("Correo")].ToString(),
                                    desTiempoPaquete = oIDataReader[oIDataReader.GetOrdinal("DesPaquete")].ToString(),
                                    DesCalificacion = oIDataReader[oIDataReader.GetOrdinal("DesCalificacion")].ToString()

                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }

        public ClientesDTO uspListarClientesPorTodos_NumeroRegistros(ClientesDTO oClientesDTO)
        {
            ClientesDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarClientesPorTodos_NumeroRegistros", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodTiempoPaquete", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodTiempoPaquete;
                    cmd.Parameters.Add(new SqlParameter("@Sexo", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.Genero;
                    cmd.Parameters.Add(new SqlParameter("@EdadRango1", System.Data.SqlDbType.Int)).Value = oClientesDTO.EdadRango1;
                    cmd.Parameters.Add(new SqlParameter("@EdadRango2", System.Data.SqlDbType.Int)).Value = oClientesDTO.EdadRango2;
                    cmd.Parameters.Add(new SqlParameter("@EstadoDeuda", System.Data.SqlDbType.Int)).Value = oClientesDTO.EstadoDeuda;
                    cmd.Parameters.Add(new SqlParameter("@EstadoAsistencia", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.EstadoAsistencia;
                    cmd.Parameters.Add(new SqlParameter("@Ubigeo", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.Ubicaciones;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@AsesorVenta", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.AsesorComercial;
                    cmd.Parameters.Add(new SqlParameter("@Nombre", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.Nombre;
                    cmd.Parameters.Add(new SqlParameter("@Apellidos", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.Apellidos;
                    cmd.Parameters.Add(new SqlParameter("@Codigo", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoS;
                    cmd.Parameters.Add(new SqlParameter("@Dni", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.DNI;
                    cmd.Parameters.Add(new SqlParameter("@Telefono", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.Telefono;
                    cmd.Parameters.Add(new SqlParameter("@Celular", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.Celular;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = oClientesDTO.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oClientesDTO.FechaFinal;
                    cmd.Parameters.Add(new SqlParameter("@checkTodos", System.Data.SqlDbType.Int)).Value = oClientesDTO.CheckTodos;
                    
                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new ClientesDTO()
                                {
                                    CantidadTotalFilas = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadRegistros")])
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;
        }


        public ClientesDTO uspListarCantidadEstadosClientes(ClientesDTO oClientesDTO)
        {
            ClientesDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarCantidadEstadosClientes", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoSede;
                   
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new ClientesDTO()
                                {
                                    CantidadTodos = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Todos")]),
                                    CantidadActivos = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Activos")]),
                                    CantidadInactivos = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Inactivos")]),
                                    CantidadRenovaciones = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Renovaciones")]),
                                    CantidadMatriculados = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Matriculados")])
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;
        }


        public ClientesDTO uspEstadisticaDashboar(ClientesDTO oClientesDTO)
        {
            ClientesDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspEstadisticaDashboar", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicioBuscador", System.Data.SqlDbType.DateTime)).Value = oClientesDTO.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFinBuscador", System.Data.SqlDbType.DateTime)).Value = oClientesDTO.FechaFinal;
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new ClientesDTO()
                                {

                                    dashboar_TotalVentaMesMembresias = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("TotalVentaMesMembresias")]),
                                    dashboar_TotalVentaMesNutricion = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("TotalVentaMesNutricion")]),
                                    dashboar_TotalVentaMesPersonalizado = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("TotalVentaMesPersonalizado")]),
                                    dashboar_TotalVentaMesDiario = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("TotalVentaMesDiario")]),
                                    dashboar_TotalVentaMesJugueria = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("TotalVentaMesJugueria")]),
                                    dashboar_TotalVentaMesSuplementos = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("TotalVentaMesSuplementos")]),
                                    dashboar_TotalVentaMesRopas = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("TotalVentaMesRopas")]),

                                    dashboar_TotalVentaHoyMembresias = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("TotalVentaHoyMembresias")]),
                                    dashboar_TotalVentaHoyNutricion = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("TotalVentaHoyNutricion")]),
                                    dashboar_TotalVentaHoyPersonalizado = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("TotalVentaHoyPersonalizado")]),
                                    dashboar_TotalVentaHoyDiario = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("TotalVentaHoyDiario")]),
                                    dashboar_TotalVentaHoyJugueria = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("TotalVentaHoyJugueria")]),
                                    dashboar_TotalVentaHoySuplementos = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("TotalVentaHoySuplementos")]),
                                    dashboar_TotalVentaHoyRopas = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("TotalVentaHoyRopas")]),

                                    dashboar_DebeMembresia = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("DebeMembresia")]),
                                    dashboar_TotalGasto = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("TotalGasto")]),

                                    dashboar_CantidadActivos = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadActivos")]),
                                    dashboar_CantidadPorVencer = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadPorVencer")]),
                                    dashboar_CantidadClientesRenovaron = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadClientesRenovaron")]),
                                    dashboar_CantidadClientesNuevos = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadClientesNuevos")]),
                                    dashboar_CantidadClientesTotalInactivos = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadClientesInactivos")]),
                                    dashboar_CantidadClientesVencidosMes = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadClientesVencidosMes")]),
                                    dashboar_CantidadClientesReinscritos = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadClientesReinscritos")]),
                                    dashboar_CantidadClientesPorIniciar = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadClientesPorIniciar")]),
                                    dashboar_CantidadClientesTotalTraspasos = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadClientesTotalTraspasos")]),

                                    dashboar_CantidadLlamadaEntrante = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadLlamadaEntrante")]),
                                    dashboar_CantidadReferido = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadReferido")]),
                                    dashboar_CantidadNuevos = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadNuevos")]),
                                    dashboar_CantidadInvitados = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadInvitados")]),
                                    dashboar_CantidadWeb = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadWeb")])
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;
        }
        
        public List<ClientesDTO> uspEstadisticaDashboar_ListadoporvencerExel(ClientesDTO oClientesDTO)
        {
            List<ClientesDTO> lista = new List<ClientesDTO>();
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspEstadisticaDashboar_ListadoporvencerExel", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoSede;
                  
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ClientesDTO()
                                {
                                    CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    CodigoSocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSocio")]),
                                    Nombres = (oIDataReader[oIDataReader.GetOrdinal("Nombres")]).ToString(),
                                    Apellidos = (oIDataReader[oIDataReader.GetOrdinal("Apellidos")]).ToString(),
                                    Correo = (oIDataReader[oIDataReader.GetOrdinal("Correo")]).ToString(),
                                    FechaNacimiento = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaNacimiento")]),
                                    Celular = (oIDataReader[oIDataReader.GetOrdinal("Celular")]).ToString(),
                                    DNI = (oIDataReader[oIDataReader.GetOrdinal("DNI")]).ToString(),
                                    desTiempoPaquete = (oIDataReader[oIDataReader.GetOrdinal("DesPaquete")]).ToString(),
                                    Costo = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Costo")]),
                                    DesFechaInicio = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaInicio")]).ToString("dd/MM/yyy"),
                                    DesFechaFin = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaFin")]).ToString("dd/MM/yyy"),
                                    Vendedor = (oIDataReader[oIDataReader.GetOrdinal("Vendedor")]).ToString(),
                                    VendedorRepartido = (oIDataReader[oIDataReader.GetOrdinal("VendedorRepartido")]).ToString(),
                                    NroIngresoActual = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("NroIngresosActual")]),
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }

        public List<ClientesDTO> uspEstadisticaDashboar_ListadoclientesrenovaronExel(ClientesDTO oClientesDTO)
        {
            List<ClientesDTO> lista = new List<ClientesDTO>();
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspEstadisticaDashboar_ListadoclientesrenovaronExel", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoSede;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ClientesDTO()
                                {
                                    CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    CodigoSocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSocio")]),
                                    Nombres = (oIDataReader[oIDataReader.GetOrdinal("Nombres")]).ToString(),
                                    Apellidos = (oIDataReader[oIDataReader.GetOrdinal("Apellidos")]).ToString(),
                                    Correo = (oIDataReader[oIDataReader.GetOrdinal("Correo")]).ToString(),
                                    FechaNacimiento = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaNacimiento")]),
                                    Celular = (oIDataReader[oIDataReader.GetOrdinal("Celular")]).ToString(),
                                    DNI = (oIDataReader[oIDataReader.GetOrdinal("DNI")]).ToString(),
                                    DescFechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")]).ToString("dd/MM/yyy"),
                                    desTiempoPaquete = (oIDataReader[oIDataReader.GetOrdinal("DesPaquete")]).ToString(),
                                    Costo = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Costo")]),
                                    DesFechaInicio = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaInicio")]).ToString("dd/MM/yyy"),
                                    DesFechaFin = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaFin")]).ToString("dd/MM/yyy"),
                                    Vendedor = (oIDataReader[oIDataReader.GetOrdinal("Vendedor")]).ToString(),
                                    NroIngresoActual = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("NroIngresosActual")]),
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }

        public List<ClientesDTO> uspEstadisticaDashboar_ListadoclientesreinscribieronExel(ClientesDTO oClientesDTO)
        {
            List<ClientesDTO> lista = new List<ClientesDTO>();
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspEstadisticaDashboar_ListadoclientesreinscribieronExel", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoSede;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ClientesDTO()
                                {
                                    CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    CodigoSocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSocio")]),
                                    Nombres = (oIDataReader[oIDataReader.GetOrdinal("Nombres")]).ToString(),
                                    Apellidos = (oIDataReader[oIDataReader.GetOrdinal("Apellidos")]).ToString(),
                                    Correo = (oIDataReader[oIDataReader.GetOrdinal("Correo")]).ToString(),
                                    FechaNacimiento = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaNacimiento")]),
                                    Celular = (oIDataReader[oIDataReader.GetOrdinal("Celular")]).ToString(),
                                    DNI = (oIDataReader[oIDataReader.GetOrdinal("DNI")]).ToString(),
                                    DescFechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")]).ToString("dd/MM/yyy"),
                                    desTiempoPaquete = (oIDataReader[oIDataReader.GetOrdinal("DesPaquete")]).ToString(),
                                    Costo = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Costo")]),
                                    DesFechaInicio = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaInicio")]).ToString("dd/MM/yyy"),
                                    DesFechaFin = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaFin")]).ToString("dd/MM/yyy"),
                                    Vendedor = (oIDataReader[oIDataReader.GetOrdinal("Vendedor")]).ToString(),
                                    NroIngresoActual = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("NroIngresosActual")]),
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }

        public List<ClientesDTO> uspEstadisticaDashboar_ListadoclientesnuevosExel(ClientesDTO oClientesDTO)
        {
            List<ClientesDTO> lista = new List<ClientesDTO>();
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspEstadisticaDashboar_ListadoclientesnuevosExel", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoSede;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ClientesDTO()
                                {
                                    CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    CodigoSocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSocio")]),
                                    Nombres = (oIDataReader[oIDataReader.GetOrdinal("Nombres")]).ToString(),
                                    Apellidos = (oIDataReader[oIDataReader.GetOrdinal("Apellidos")]).ToString(),
                                    Correo = (oIDataReader[oIDataReader.GetOrdinal("Correo")]).ToString(),
                                    FechaNacimiento = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaNacimiento")]),
                                    Celular = (oIDataReader[oIDataReader.GetOrdinal("Celular")]).ToString(),
                                    DNI = (oIDataReader[oIDataReader.GetOrdinal("DNI")]).ToString(),
                                    DescFechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")]).ToString("dd/MM/yyy"),
                                    desTiempoPaquete = (oIDataReader[oIDataReader.GetOrdinal("DesPaquete")]).ToString(),
                                    Costo = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Costo")]),
                                    DesFechaInicio = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaInicio")]).ToString("dd/MM/yyy"),
                                    DesFechaFin = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaFin")]).ToString("dd/MM/yyy"),
                                    Vendedor = (oIDataReader[oIDataReader.GetOrdinal("Vendedor")]).ToString(),
                                    NroIngresoActual = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("NroIngresosActual")]),
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }


        public List<ClientesDTO> uspVerMasClientesComprometidosPagosCuotas_Paginacion(ClientesDTO oClientesDTO, Paging paging, ref uint recordCount)
        {
            List<ClientesDTO> lista = new List<ClientesDTO>();
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspVerMasClientesComprometidosPagosCuotas_Paginacion", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Tipo", System.Data.SqlDbType.Int)).Value = oClientesDTO.Tipo;
                    cmd.Parameters.Add(new SqlParameter("@Vendedor", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.Vendedor;
                    cmd.Parameters.Add(new SqlParameter("@Buscador", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.Nombre;

                    cmd.Parameters.Add(new SqlParameter("@PaginaActual", System.Data.SqlDbType.Int)).Value = paging.PageNumber;
                    cmd.Parameters.Add(new SqlParameter("@TamanioPagina", System.Data.SqlDbType.Int)).Value = paging.PageRecords;
                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ClientesDTO()
                                {
                                    ImagenUrl = oIDataReader[oIDataReader.GetOrdinal("ImagenUrl")].ToString(),
                                    Telefono = oIDataReader[oIDataReader.GetOrdinal("Telefono")].ToString(),
                                    CodigoSocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSocio")]),
                                    NombreCompleto = oIDataReader[oIDataReader.GetOrdinal("Cliente")].ToString(),
                                    NombreMembresia = oIDataReader[oIDataReader.GetOrdinal("Membresia")].ToString(),
                                    DesFechaInicio = oIDataReader[oIDataReader.GetOrdinal("FechaInicio")].ToString(),
                                    DesFechaFin = oIDataReader[oIDataReader.GetOrdinal("FechaFin")].ToString(),
                                    Precio = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Precio")]),
                                    Pago = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Pago")]),
                                    Debe = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Debe")]),
                                    FechaCuota = oIDataReader[oIDataReader.GetOrdinal("FechaCuota")].ToString(),
                                    MontoComprometido = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("MontoComprometido")]),
                                    Vendedor = oIDataReader[oIDataReader.GetOrdinal("Vendedor")].ToString(),
                                    desTiempoPaquete = oIDataReader[oIDataReader.GetOrdinal("DesTiempoPaquete")].ToString(),
                                    Correo = oIDataReader[oIDataReader.GetOrdinal("Correo")].ToString(),
                                    Celular = oIDataReader[oIDataReader.GetOrdinal("Celular")].ToString(),
                                    EstadoCelular = oIDataReader[oIDataReader.GetOrdinal("EstadoCelular")].ToString(),
                                    DesCalificacion =  oIDataReader[oIDataReader.GetOrdinal("DesCalificacion")].ToString()
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }

        public ClientesDTO uspVerMasClientesComprometidosPagosCuotas_NumeroRegistros(ClientesDTO oClientesDTO)
        {
            ClientesDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspVerMasClientesComprometidosPagosCuotas_NumeroRegistros", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Tipo", System.Data.SqlDbType.Int)).Value = oClientesDTO.Tipo;
                    cmd.Parameters.Add(new SqlParameter("@Vendedor", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.Vendedor;
                    cmd.Parameters.Add(new SqlParameter("@Buscador", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.Nombre;
                    
                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new ClientesDTO()
                                {
                                    CantidadTotalFilas = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("NumeroRegistros")])
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;          
        }
        
        public List<ClientesDTO> uspListarClientesAgendaComercialReinscripcion(ClientesDTO oClientesDTO, Paging paging, ref uint recordCount)
        {
            List<ClientesDTO> lista = new List<ClientesDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarClientesAgendaComercialReinscripcion", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodTiempoPaquete", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodTiempoPaquete;
                    cmd.Parameters.Add(new SqlParameter("@Sexo", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.Genero;
                    cmd.Parameters.Add(new SqlParameter("@EdadRango1", System.Data.SqlDbType.Int)).Value = oClientesDTO.EdadRango1;
                    cmd.Parameters.Add(new SqlParameter("@EdadRango2", System.Data.SqlDbType.Int)).Value = oClientesDTO.EdadRango2;
                    cmd.Parameters.Add(new SqlParameter("@EstadoDeuda", System.Data.SqlDbType.Int)).Value = oClientesDTO.EstadoDeuda;
                    cmd.Parameters.Add(new SqlParameter("@EstadoAsistencia", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.EstadoAsistencia;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@AsesorVenta", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.AsesorComercial;
                    cmd.Parameters.Add(new SqlParameter("@Buscador", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.Nombre;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = oClientesDTO.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oClientesDTO.FechaFinal;

                    cmd.Parameters.Add(new SqlParameter("@PaginaActual", System.Data.SqlDbType.Int)).Value = paging.PageNumber;
                    cmd.Parameters.Add(new SqlParameter("@TamanioPagina", System.Data.SqlDbType.Int)).Value = paging.PageRecords;
                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ClientesDTO()
                                {
                                    CodigoSocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSocio")]),
                                    Nombres = oIDataReader[oIDataReader.GetOrdinal("Nombres")].ToString(),
                                    Apellidos = oIDataReader[oIDataReader.GetOrdinal("Apellidos")].ToString(),
                                    Telefono = oIDataReader[oIDataReader.GetOrdinal("Telefono")].ToString(),
                                    Celular = oIDataReader[oIDataReader.GetOrdinal("Celular")].ToString(),
                                    Correo = oIDataReader[oIDataReader.GetOrdinal("Correo")].ToString(),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("DesPaquete")].ToString(),
                                    Costo = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Costo")]),
                                    DesFechaInicio = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaInicio")]).ToString("dd/MM/yyyy"),
                                    DesFechaFin = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaFin")]).ToString("dd/MM/yyyy"),
                                    Vendedor = oIDataReader[oIDataReader.GetOrdinal("Vendedor")].ToString(),
                                    VendedorRepartido = oIDataReader[oIDataReader.GetOrdinal("VendedorRepartido")].ToString(),
                                    desCita = oIDataReader[oIDataReader.GetOrdinal("desCita")].ToString(),
                                    CodigoMembresia = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoMembresia")]),
                                    ImagenUrl = oIDataReader[oIDataReader.GetOrdinal("ImagenUrl")].ToString(),
                                    flagReinslibre = oIDataReader[oIDataReader.GetOrdinal("flagReinslibre")].ToString()
                                });
                            }
                        }
                        
                    }
                }
            }
            return lista;
        }
        
        public List<ClientesDTO> uspListarClientesAgendaComercialRenovacion(ClientesDTO oClientesDTO, Paging paging, ref uint recordCount)
        {
            List<ClientesDTO> lista = new List<ClientesDTO>();
            
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarClientesAgendaComercialRenovacion", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodTiempoPaquete", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodTiempoPaquete;
                    cmd.Parameters.Add(new SqlParameter("@Sexo", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.Genero;
                    cmd.Parameters.Add(new SqlParameter("@EdadRango1", System.Data.SqlDbType.Int)).Value = oClientesDTO.EdadRango1;
                    cmd.Parameters.Add(new SqlParameter("@EdadRango2", System.Data.SqlDbType.Int)).Value = oClientesDTO.EdadRango2;
                    cmd.Parameters.Add(new SqlParameter("@EstadoDeuda", System.Data.SqlDbType.Int)).Value = oClientesDTO.EstadoDeuda;
                    cmd.Parameters.Add(new SqlParameter("@EstadoAsistencia", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.EstadoAsistencia;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@AsesorVenta", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.AsesorComercial;
                    cmd.Parameters.Add(new SqlParameter("@Buscador", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.Nombre;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = oClientesDTO.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oClientesDTO.FechaFinal;

                    cmd.Parameters.Add(new SqlParameter("@PaginaActual", System.Data.SqlDbType.Int)).Value = paging.PageNumber;
                    cmd.Parameters.Add(new SqlParameter("@TamanioPagina", System.Data.SqlDbType.Int)).Value = paging.PageRecords;
                    cmd.Parameters.AddWithValue("@NumeroRegistros",0).Direction = System.Data.ParameterDirection.Output;
                    
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ClientesDTO()
                                {
                                    CodigoSocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSocio")]),
                                    Nombres = oIDataReader[oIDataReader.GetOrdinal("Nombres")].ToString(),
                                    Apellidos = oIDataReader[oIDataReader.GetOrdinal("Apellidos")].ToString(),
                                    Telefono = oIDataReader[oIDataReader.GetOrdinal("Telefono")].ToString(),
                                    Celular = oIDataReader[oIDataReader.GetOrdinal("Celular")].ToString(),                                    
                                    Correo = oIDataReader[oIDataReader.GetOrdinal("Correo")].ToString(),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("DesPaquete")].ToString(),
                                    Costo = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Costo")]),
                                    DesFechaInicio = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaInicio")]).ToString("dd/MM/yyyy"),
                                    DesFechaFin = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaFin")]).ToString("dd/MM/yyyy"),
                                    Vendedor = oIDataReader[oIDataReader.GetOrdinal("Vendedor")].ToString(),
                                    VendedorRepartido = oIDataReader[oIDataReader.GetOrdinal("VendedorRepartido")].ToString(),
                                    desCita = oIDataReader[oIDataReader.GetOrdinal("desCita")].ToString(),
                                    CodigoMembresia = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoMembresia")]),
                                    ImagenUrl = oIDataReader[oIDataReader.GetOrdinal("ImagenUrl")].ToString(),
                                    NroIngresoActual = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("NroIngresosActual")]),
                                    flagReinslibre = oIDataReader[oIDataReader.GetOrdinal("flagReinslibre")].ToString(),
                                    ColorAgenda = oIDataReader[oIDataReader.GetOrdinal("flagReinslibre")].ToString() == "yes" ? "rgb(6,140,49)" : "rgb(255,0,0)"                                    
                                });
                            }
                        }
                       
                    }
                }
            }
            return lista;
        }

        public List<ClientesDTO> uspListarClientesAgendaComercialRenovacionInscritos(ClientesDTO oClientesDTO, Paging paging, ref uint recordCount)
        {    
            List<ClientesDTO> lista = new List<ClientesDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarClientesAgendaComercialRenovacionInscritos", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodTiempoPaquete", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodTiempoPaquete;
                    cmd.Parameters.Add(new SqlParameter("@Sexo", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.Genero;
                    cmd.Parameters.Add(new SqlParameter("@EdadRango1", System.Data.SqlDbType.Int)).Value = oClientesDTO.EdadRango1;
                    cmd.Parameters.Add(new SqlParameter("@EdadRango2", System.Data.SqlDbType.Int)).Value = oClientesDTO.EdadRango2;
                    cmd.Parameters.Add(new SqlParameter("@EstadoDeuda", System.Data.SqlDbType.Int)).Value = oClientesDTO.EstadoDeuda;
                    cmd.Parameters.Add(new SqlParameter("@EstadoAsistencia", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.EstadoAsistencia;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@AsesorVenta", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.AsesorComercial;
                    cmd.Parameters.Add(new SqlParameter("@Buscador", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.Nombre;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = oClientesDTO.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oClientesDTO.FechaFinal;

                    cmd.Parameters.Add(new SqlParameter("@PaginaActual", System.Data.SqlDbType.Int)).Value = paging.PageNumber;
                    cmd.Parameters.Add(new SqlParameter("@TamanioPagina", System.Data.SqlDbType.Int)).Value = paging.PageRecords;
                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ClientesDTO()
                                {
                                    CodigoSocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSocio")]),
                                    Nombres = oIDataReader[oIDataReader.GetOrdinal("Nombres")].ToString(),
                                    Apellidos = oIDataReader[oIDataReader.GetOrdinal("Apellidos")].ToString(),
                                    Telefono = oIDataReader[oIDataReader.GetOrdinal("Telefono")].ToString(),
                                    Celular = oIDataReader[oIDataReader.GetOrdinal("Celular")].ToString(),
                                    Correo = oIDataReader[oIDataReader.GetOrdinal("Correo")].ToString(),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("DesPaquete")].ToString(),
                                    Costo = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Costo")]),
                                    DesFechaInicio = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaInicio")]).ToString("dd/MM/yyyy"),
                                    DesFechaFin = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaFin")]).ToString("dd/MM/yyyy"),
                                    Vendedor = oIDataReader[oIDataReader.GetOrdinal("Vendedor")].ToString(),
                                    VendedorRepartido = oIDataReader[oIDataReader.GetOrdinal("VendedorRepartido")].ToString(),
                                    desCita = oIDataReader[oIDataReader.GetOrdinal("desCita")].ToString(),
                                    CodigoMembresia = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoMembresia")]),
                                    ImagenUrl = oIDataReader[oIDataReader.GetOrdinal("ImagenUrl")].ToString()

                                });
                            }
                        }
                        
                    }
                }
            }
            return lista;
        }
        
        public ClientesDTO uspBuscarSocioConMembresiaActivaPrimerRegistro(ClientesDTO oClientesDTO)
        {
            ClientesDTO oItem = new ClientesDTO();
            int CodigoSocio = 0;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspBuscarSocioConMembresiaActivaPrimerRegistro", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int, 10)).Value = oClientesDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int, 10)).Value = oClientesDTO.CodigoSede;
                    CodigoSocio = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }

            oItem.CodigoSocio = CodigoSocio;
            return oItem;
        }


        public List<ClientesDTO> uspListarVentasTotal(ClientesDTO oClientesDTO)
        {
            List<ClientesDTO> lista = new List<ClientesDTO>();
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarVentasTotal", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = oClientesDTO.Fecha;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oClientesDTO.FechaFinStr;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ClientesDTO()
                                {
                                    Tema = oIDataReader[oIDataReader.GetOrdinal("Tabla")].ToString(),
                                    Tipo = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Tipo")]),
                                    DescTipo = oIDataReader[oIDataReader.GetOrdinal("TipoDes")].ToString(),
                                    Total = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Total")])
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
