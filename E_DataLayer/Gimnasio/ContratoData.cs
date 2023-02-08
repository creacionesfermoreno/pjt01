using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using E_DataModel.Gimnasio;
using E_DataModel.Common;
using System.Data.SqlClient;

namespace E_DataLayer.Gimnasio
{
    public class ContratoData
    {
        public List<ContratoDTO> uspListarMembresiasDeudasPorDiaRangoFechas_Paginacion(ContratoDTO oContratoDTO, Paging paging)
        {
            List<ContratoDTO> lista = new List<ContratoDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarMembresiasDeudasPorDiaRangoFechas_Paginacion", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oContratoDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@fecha", System.Data.SqlDbType.DateTime)).Value = oContratoDTO.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@fechaFin", System.Data.SqlDbType.DateTime)).Value = oContratoDTO.FechaFin;
                    cmd.Parameters.Add(new SqlParameter("@Vendedores", System.Data.SqlDbType.VarChar, 100)).Value = oContratoDTO.AsesorComercial;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oContratoDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@TipoIngreso", System.Data.SqlDbType.Int)).Value = oContratoDTO.TipoIngreso;
                    cmd.Parameters.Add(new SqlParameter("@TiempoMembresia", System.Data.SqlDbType.Int)).Value = oContratoDTO.CodTiempoMenbresia;

                    cmd.Parameters.Add(new SqlParameter("@PaginaActual", System.Data.SqlDbType.Int)).Value = paging.PageNumber;
                    cmd.Parameters.Add(new SqlParameter("@TamanioPagina", System.Data.SqlDbType.Int)).Value = paging.PageRecords;
                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ContratoDTO()
                                {
                                    CodigoMenbresia = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoMenbresia")]),
                                    CodigoSocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSocio")]),
                                    NombreSocio = oIDataReader[oIDataReader.GetOrdinal("NomSocio")].ToString(),
                                    imageURL = oIDataReader[oIDataReader.GetOrdinal("ImagenUrl")].ToString(),
                                    CodigoPaquete = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoPaquete")]),
                                    NombrePaquete = oIDataReader[oIDataReader.GetOrdinal("NombrePaquete")].ToString(),
                                    Telefono = oIDataReader[oIDataReader.GetOrdinal("Telefono")].ToString(),
                                    DNI = oIDataReader[oIDataReader.GetOrdinal("DNI")].ToString(),
                                    Distrito = oIDataReader[oIDataReader.GetOrdinal("Distrito")].ToString(),
                                    Direccion = oIDataReader[oIDataReader.GetOrdinal("Direccion")].ToString(),
                                    DesFechaInicio = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaInicio")]).ToString("dd/MM/yyyy"),
                                    DesFechaFin = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaFin")]).ToString("dd/MM/yyyy"),
                                    Costo = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Costo")]),
                                    MontoTotal = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("MontoTotal")]),
                                    Debe = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Costo")]) - Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("MontoTotal")]),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString(),
                                    AsesorComercial = oIDataReader[oIDataReader.GetOrdinal("AsesorComercial")].ToString()
                                });

                            }
                        }

                    }
                }
            }

            return lista;
        }

        public List<ContratoDTO> uspListarDeudasTotalesPorTipoDiaRangoFechas_Paginacion(ContratoDTO oContratoDTO, Paging paging)
        {
            List<ContratoDTO> lista = new List<ContratoDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarDeudasTotalesPorTipoDiaRangoFechas_Paginacion", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oContratoDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@fecha", System.Data.SqlDbType.DateTime)).Value = oContratoDTO.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@fechaFin", System.Data.SqlDbType.DateTime)).Value = oContratoDTO.FechaFin;
                    cmd.Parameters.Add(new SqlParameter("@Vendedores", System.Data.SqlDbType.VarChar, 100)).Value = oContratoDTO.AsesorComercial;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oContratoDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@TipoIngreso", System.Data.SqlDbType.Int)).Value = oContratoDTO.TipoIngreso;
                    cmd.Parameters.Add(new SqlParameter("@TiempoMembresia", System.Data.SqlDbType.Int)).Value = oContratoDTO.CodTiempoMenbresia;
                    cmd.Parameters.Add(new SqlParameter("@Tipo", System.Data.SqlDbType.Int)).Value = oContratoDTO.Tipo;

                    cmd.Parameters.Add(new SqlParameter("@PaginaActual", System.Data.SqlDbType.Int)).Value = paging.PageNumber;
                    cmd.Parameters.Add(new SqlParameter("@TamanioPagina", System.Data.SqlDbType.Int)).Value = paging.PageRecords;
                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ContratoDTO()
                                {
                                    CodigoMenbresia = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoMenbresia")]),
                                    CodigoSocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSocio")]),
                                    NombreSocio = oIDataReader[oIDataReader.GetOrdinal("NomSocio")].ToString(),
                                    imageURL = oIDataReader[oIDataReader.GetOrdinal("ImagenUrl")].ToString(),
                                    CodigoPaquete = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoPaquete")]),
                                    NombrePaquete = oIDataReader[oIDataReader.GetOrdinal("NombrePaquete")].ToString(),
                                    Telefono = oIDataReader[oIDataReader.GetOrdinal("Telefono")].ToString(),
                                    DNI = oIDataReader[oIDataReader.GetOrdinal("DNI")].ToString(),
                                    Distrito = oIDataReader[oIDataReader.GetOrdinal("Distrito")].ToString(),
                                    Direccion = oIDataReader[oIDataReader.GetOrdinal("Direccion")].ToString(),
                                    DesFechaInicio = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaInicio")]).ToString("dd/MM/yyyy"),
                                    DesFechaFin = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaFin")]).ToString("dd/MM/yyyy"),
                                    Costo = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Costo")]),
                                    MontoTotal = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("MontoTotal")]),
                                    Debe = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Costo")]) - Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("MontoTotal")]),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString(),
                                    AsesorComercial = oIDataReader[oIDataReader.GetOrdinal("AsesorComercial")].ToString()
                                });

                            }
                        }

                    }
                }
            }

            return lista;
        }

        public ContratoDTO uspListarMembresiasDeudasPorDiaRangoFechas_NumeroRegistros(ContratoDTO oContratoDTO)
        {
            ContratoDTO itemDTO = new ContratoDTO();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarMembresiasDeudasPorDiaRangoFechas_NumeroRegistros", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oContratoDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@fecha", System.Data.SqlDbType.Int)).Value = oContratoDTO.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@fechaFin", System.Data.SqlDbType.Int)).Value = oContratoDTO.FechaFin;
                    cmd.Parameters.Add(new SqlParameter("@Vendedores", System.Data.SqlDbType.Int)).Value = oContratoDTO.AsesorComercial;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oContratoDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@TipoIngreso", System.Data.SqlDbType.Int)).Value = oContratoDTO.TipoIngreso;
                    cmd.Parameters.Add(new SqlParameter("@TiempoMembresia", System.Data.SqlDbType.Int)).Value = oContratoDTO.CodTiempoMenbresia;
                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new ContratoDTO()
                                {
                                    CantidadRegistros = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadRegistros")]),
                                    TotalDeuda = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("TotalDeuda")])
                                };
                            }
                        }
                    }
                }
            }

            return itemDTO;
        }

        public ContratoDTO uspListarDeudasTotalesPorTipoDiaRangoFechas_NumeroRegistros(ContratoDTO oContratoDTO)
        {
            ContratoDTO itemDTO = new ContratoDTO();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarDeudasTotalesPorTipoDiaRangoFechas_NumeroRegistros", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oContratoDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@fecha", System.Data.SqlDbType.DateTime)).Value = oContratoDTO.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@fechaFin", System.Data.SqlDbType.DateTime)).Value = oContratoDTO.FechaFin;
                    cmd.Parameters.Add(new SqlParameter("@Vendedores", System.Data.SqlDbType.VarChar, 100)).Value = oContratoDTO.AsesorComercial;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oContratoDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@TipoIngreso", System.Data.SqlDbType.Int)).Value = oContratoDTO.TipoIngreso;
                    cmd.Parameters.Add(new SqlParameter("@TiempoMembresia", System.Data.SqlDbType.Int)).Value = oContratoDTO.CodTiempoMenbresia;
                    cmd.Parameters.Add(new SqlParameter("@Tipo", System.Data.SqlDbType.Int)).Value = oContratoDTO.Tipo;
                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new ContratoDTO()
                                {
                                    CantidadRegistros = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadRegistros")]),
                                    TotalDeuda = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("TotalDeuda")])
                                };
                            }
                        }
                    }
                }
            }

            return itemDTO;
        }

        public List<ContratoDTO> uspListarMembresiasSociosAcuenta_Paginacion(ContratoDTO oContratoDTO, Paging paging)
        {
            List<ContratoDTO> lista = new List<ContratoDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarMembresiasSociosAcuenta_Paginacion", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oContratoDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@Vendedor", System.Data.SqlDbType.VarChar)).Value = oContratoDTO.UsuarioCreacion;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oContratoDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = oContratoDTO.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oContratoDTO.FechaFin;
                    cmd.Parameters.Add(new SqlParameter("@Buscador", System.Data.SqlDbType.VarChar)).Value = oContratoDTO.Nombres;

                    cmd.Parameters.Add(new SqlParameter("@PaginaActual", System.Data.SqlDbType.Int)).Value = paging.PageNumber;
                    cmd.Parameters.Add(new SqlParameter("@TamanioPagina", System.Data.SqlDbType.Int)).Value = paging.PageRecords;
                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ContratoDTO()
                                {
                                    CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    CodigoSocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSocio")]),
                                    Nombres = oIDataReader[oIDataReader.GetOrdinal("Nombres")].ToString(),
                                    ImagenUrl = oIDataReader[oIDataReader.GetOrdinal("ImagenUrl")].ToString(),
                                    Celular = oIDataReader[oIDataReader.GetOrdinal("Celular")].ToString(),
                                    EstadoCelular = oIDataReader[oIDataReader.GetOrdinal("EstadoCelular")].ToString(),
                                    Correo = oIDataReader[oIDataReader.GetOrdinal("Correo")].ToString(),
                                    CodigoMembresia = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoMembresia")]),
                                    Costo = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Costo")]),
                                    MontoTotal = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("MontoTotal")]),
                                    Debe = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Debe")]),
                                    DNI = oIDataReader[oIDataReader.GetOrdinal("DNI")].ToString(),
                                    FechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")]),
                                    FechaInicio = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaInicio")]),
                                    FechaFin = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaFin")]),
                                    Vendedor = oIDataReader[oIDataReader.GetOrdinal("Vendedor")].ToString(),
                                    desTiempoPaquete = oIDataReader[oIDataReader.GetOrdinal("DesPaquete")].ToString(),
                                    DesCalificacion = oIDataReader[oIDataReader.GetOrdinal("DesCalificacion")].ToString(),
                                    DesTipoIngreso = oIDataReader[oIDataReader.GetOrdinal("DesTipoIngreso")].ToString(),
                                    DesTipoPaquete = oIDataReader[oIDataReader.GetOrdinal("DesTipoPaquete")].ToString()
                                });
                            }
                        }
                    }
                }

            }

            return lista;
        }

        public ContratoDTO uspListarMembresiasSociosAcuenta_NumeroRegistro(ContratoDTO oContratoDTO)
        {
            ContratoDTO itemDTO = new ContratoDTO();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarMembresiasSociosAcuenta_NumeroRegistro", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oContratoDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@Vendedor", System.Data.SqlDbType.VarChar, 100)).Value = oContratoDTO.UsuarioCreacion;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oContratoDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = oContratoDTO.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oContratoDTO.FechaFin;
                    cmd.Parameters.Add(new SqlParameter("@Buscador", System.Data.SqlDbType.VarChar, 100)).Value = oContratoDTO.Nombres;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new ContratoDTO()
                                {
                                    CantidadAsistencia = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadRegistros")]),
                                    Debe = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Debe")])
                                };
                            }
                        }
                    }
                }
            }

            return itemDTO;
        }

        public List<ContratoDTO> uspListarMatriculadorAgendaComercial_paginacion(ContratoDTO oContratoDTO, Paging paging, ref int NumeroRegistros, ref int CantidadNuevos, ref int CantidadRenovaciones, ref int CantidadReinscripciones, ref decimal VentaNuevos, ref decimal VentaRenovaciones, ref decimal VentaReinscripciones)
        {
            List<ContratoDTO> lista = new List<ContratoDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarMatriculadorAgendaComercial_paginacion", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oContratoDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oContratoDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@OrigenMembresia", System.Data.SqlDbType.Int)).Value = oContratoDTO.CodigoMebresiaOrigen;
                    cmd.Parameters.Add(new SqlParameter("@Buscador", System.Data.SqlDbType.VarChar)).Value = oContratoDTO.Nombres;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = oContratoDTO.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oContratoDTO.FechaFin;
                    cmd.Parameters.Add(new SqlParameter("@CodTiempoMenbresia", System.Data.SqlDbType.Int)).Value = oContratoDTO.CodTiempoMenbresia;
                    cmd.Parameters.Add(new SqlParameter("@AsesorComercial", System.Data.SqlDbType.VarChar)).Value = oContratoDTO.UsuarioCreacion;

                    cmd.Parameters.Add(new SqlParameter("@PaginaActual", System.Data.SqlDbType.Int)).Value = paging.PageNumber;
                    cmd.Parameters.Add(new SqlParameter("@TamanioPagina", System.Data.SqlDbType.Int)).Value = paging.PageRecords;

                    SqlParameter outputNumeroRegistros = new SqlParameter("@NumeroRegistros", System.Data.SqlDbType.Int)
                    {
                        Direction = System.Data.ParameterDirection.Output
                    };
                    cmd.Parameters.Add(outputNumeroRegistros);

                    SqlParameter outputVentaNuevos = new SqlParameter("@VentaNuevos", System.Data.SqlDbType.Decimal)
                    {
                        Direction = System.Data.ParameterDirection.Output
                    };
                    cmd.Parameters.Add(outputVentaNuevos);

                    SqlParameter outputVentaRenovaciones = new SqlParameter("@VentaRenovaciones", System.Data.SqlDbType.Decimal)
                    {
                        Direction = System.Data.ParameterDirection.Output
                    };
                    cmd.Parameters.Add(outputVentaRenovaciones);

                    SqlParameter outputVentaReinscripciones = new SqlParameter("@VentaReinscripciones", System.Data.SqlDbType.Decimal)
                    {
                        Direction = System.Data.ParameterDirection.Output
                    };
                    cmd.Parameters.Add(outputVentaReinscripciones);

                    SqlParameter outputCantidadNuevos = new SqlParameter("@CantidadNuevos", System.Data.SqlDbType.Int)
                    {
                        Direction = System.Data.ParameterDirection.Output
                    };
                    cmd.Parameters.Add(outputCantidadNuevos);

                    SqlParameter outputCantidadRenovaciones = new SqlParameter("@CantidadRenovaciones", System.Data.SqlDbType.Int)
                    {
                        Direction = System.Data.ParameterDirection.Output
                    };
                    cmd.Parameters.Add(outputCantidadRenovaciones);

                    SqlParameter outputCantidadReinscripciones = new SqlParameter("@CantidadReinscripciones", System.Data.SqlDbType.Int)
                    {
                        Direction = System.Data.ParameterDirection.Output
                    };
                    cmd.Parameters.Add(outputCantidadReinscripciones);


                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ContratoDTO()
                                {
                                    CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    CodigoSocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSocio")]),
                                    Nombres = oIDataReader[oIDataReader.GetOrdinal("Nombres")].ToString(),
                                    Apellidos = oIDataReader[oIDataReader.GetOrdinal("Apellidos")].ToString(),
                                    DNI = oIDataReader[oIDataReader.GetOrdinal("DNI")].ToString(),
                                    Celular = oIDataReader[oIDataReader.GetOrdinal("Celular")].ToString(),
                                    EstadoCelular = oIDataReader[oIDataReader.GetOrdinal("EstadoCelular")].ToString(),
                                    FechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")]),
                                    FechaInicio = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaInicio")]),
                                    FechaFin = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaFin")]),
                                    Costo = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Costo")]),
                                    Pago = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("PagoTotal")]),
                                    Debe = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Debe")]),
                                    AsesorComercial = oIDataReader[oIDataReader.GetOrdinal("AsesorComercial")].ToString(),
                                    desTiempoPaquete = oIDataReader[oIDataReader.GetOrdinal("DesTiempoMembresia")].ToString(),
                                    desPlanMembresia = oIDataReader[oIDataReader.GetOrdinal("DesPlanMembresia")].ToString(),
                                    desOrigenMembresia = oIDataReader[oIDataReader.GetOrdinal("DesOrigenMembresia")].ToString(),
                                    desTipoMembresia = oIDataReader[oIDataReader.GetOrdinal("DesTipoMembresia")].ToString()
                                });
                            }
                        }
                    }

                    NumeroRegistros = Convert.ToInt32(outputNumeroRegistros.Value);

                    CantidadNuevos = Convert.ToInt32(outputCantidadNuevos.Value);
                    CantidadRenovaciones = Convert.ToInt32(outputCantidadRenovaciones.Value);
                    CantidadReinscripciones = Convert.ToInt32(outputCantidadReinscripciones.Value);

                    VentaNuevos = Convert.ToDecimal(outputVentaNuevos.Value);
                    VentaRenovaciones = Convert.ToDecimal(outputVentaRenovaciones.Value);
                    VentaReinscripciones = Convert.ToDecimal(outputVentaReinscripciones.Value);

                }

            }

            return lista;
        }

        public List<ContratoDTO> ExportaruspListarMatriculadorAgendaComercial_paginacion(ContratoDTO oContratoDTO)
        {
            List<ContratoDTO> lista = new List<ContratoDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarMatriculadorAgendaComercial_ExportarExcel", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oContratoDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oContratoDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@OrigenMembresia", System.Data.SqlDbType.Int)).Value = oContratoDTO.CodigoMebresiaOrigen;
                    cmd.Parameters.Add(new SqlParameter("@Buscador", System.Data.SqlDbType.VarChar)).Value = oContratoDTO.Nombres;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = oContratoDTO.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oContratoDTO.FechaFin;
                    cmd.Parameters.Add(new SqlParameter("@CodTiempoMenbresia", System.Data.SqlDbType.Int)).Value = oContratoDTO.CodTiempoMenbresia;
                    cmd.Parameters.Add(new SqlParameter("@AsesorComercial", System.Data.SqlDbType.VarChar)).Value = oContratoDTO.UsuarioCreacion;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ContratoDTO()
                                {
                                    CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    CodigoSocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSocio")]),
                                    Nombres = oIDataReader[oIDataReader.GetOrdinal("Nombres")].ToString(),
                                    Apellidos = oIDataReader[oIDataReader.GetOrdinal("Apellidos")].ToString(),
                                    DNI = oIDataReader[oIDataReader.GetOrdinal("DNI")].ToString(),
                                    Celular = oIDataReader[oIDataReader.GetOrdinal("Celular")].ToString(),
                                    FechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")]),
                                    FechaInicio = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaInicio")]),
                                    FechaFin = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaFin")]),
                                    Costo = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Costo")]),
                                    Pago = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("PagoTotal")]),
                                    Debe = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Debe")]),
                                    AsesorComercial = oIDataReader[oIDataReader.GetOrdinal("AsesorComercial")].ToString(),
                                    desTiempoPaquete = oIDataReader[oIDataReader.GetOrdinal("DesTiempoMembresia")].ToString(),
                                    desPlanMembresia = oIDataReader[oIDataReader.GetOrdinal("DesPlanMembresia")].ToString(),
                                    desOrigenMembresia = oIDataReader[oIDataReader.GetOrdinal("DesOrigenMembresia")].ToString(),
                                    EstadoCelular = oIDataReader[oIDataReader.GetOrdinal("Celular")].ToString() == "" ? "none" : "block"
                                });
                            }
                        }
                    }
                }

            }

            return lista;
        }

        public ContratoDTO uspListarMatriculadorAgendaComercial_NumeroRegistros(ContratoDTO oContratoDTO)
        {
            ContratoDTO itemDTO = new ContratoDTO();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarMatriculadorAgendaComercial_NumeroRegistros", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oContratoDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oContratoDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@OrigenMembresia", System.Data.SqlDbType.Int)).Value = oContratoDTO.CodigoMebresiaOrigen;
                    cmd.Parameters.Add(new SqlParameter("@Buscador", System.Data.SqlDbType.VarChar)).Value = oContratoDTO.Nombres;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = oContratoDTO.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oContratoDTO.FechaFin;
                    cmd.Parameters.Add(new SqlParameter("@CodTiempoMenbresia", System.Data.SqlDbType.Int)).Value = oContratoDTO.CodTiempoMenbresia;
                    cmd.Parameters.Add(new SqlParameter("@AsesorComercial", System.Data.SqlDbType.VarChar)).Value = oContratoDTO.UsuarioCreacion;

                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new ContratoDTO()
                                {
                                    CantidadRegistros = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadRegistros")]),
                                };
                            }
                        }
                    }
                }
            }

            return itemDTO;
        }


        public List<ContratoDTO> uspListarMembresiasSocios(ContratoDTO oContratoDTO)
        {
            List<ContratoDTO> lista = new List<ContratoDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarMembresiasSocios", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oContratoDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oContratoDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSocio", System.Data.SqlDbType.Int)).Value = oContratoDTO.CodigoSocio;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ContratoDTO()
                                {
                                    CodigoMenbresia = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoMenbresia")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    CodigoSocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSocio")]),
                                    CodigoResponsable = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoResponsable")]),
                                    CodigoPaquete = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoPaquete")]),
                                    desTipoPaquete = oIDataReader[oIDataReader.GetOrdinal("desTipoPaquete")].ToString(),
                                    NombrePaquete = oIDataReader[oIDataReader.GetOrdinal("NombrePaquete")].ToString(),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("NombreMembresia")].ToString(),
                                    FechaInicio = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaInicio")]),
                                    FechaFin = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaFin")]),
                                    Costo = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Costo")]),
                                    MontoTotal = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("MontoTotal")]),
                                    Debe = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Debe")]),
                                    colorEstado = oIDataReader[oIDataReader.GetOrdinal("ColorDeuda")].ToString(),
                                    NroIngreso = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("NroIngresos")]),
                                    NroIngresoActual = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("NroIngresosActual")]),
                                    NroContrato = oIDataReader[oIDataReader.GetOrdinal("NroContrato")].ToString(),
                                    MatriculadoPor = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("MatriculadoPor")]),
                                    FrezenDisponibles = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CongelamientoVigente")]),
                                    DesMatriculadoPor = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("MatriculadoPor")]) == 1 ? "Socio" : (Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("MatriculadoPor")]) == 2 ? "Entrenador" : (Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("MatriculadoPor")]) == 3 ? "Counter" : (Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("MatriculadoPor")]) == 4 ? "Free pass" : (Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("MatriculadoPor")]) == 5 ? "Trabajo de Campo" : "Facebook")))),
                                    Estado = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Estado")]),
                                    desEstado = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Estado")]) == 1 ? "Activo" : Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Estado")]) == 0 ? "Congelado" : Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Estado")]) == 3 ? "Traspaso" : Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Estado")]) == 10 ? "Activo" : Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Estado")]) == 11 ? "Activo" : Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Estado")]) == 12 ? "Activo" : Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Estado")]) == 5 ? "Activo" : "Inactivo",
                                    CantidadFreezing = (Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantFreezing")]) + Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CongelamientoVigente")])),
                                    CantidadFreezingTomados = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantFreezing")]),
                                    EstadoCogelado = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Estado")]) == 1 ? "none" : "",
                                    colorInputCongelado = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Estado")]) == 1 ? "btn-default" : "btn-danger",
                                    strFechaCuota = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCuota")]).ToString("dd/MM/yyyy"),
                                    MontoCuota = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("MontoCuota")]),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString(),
                                    flagTermino = DateTime.Now > Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaFin")]) ? "#C0C0C0" : "",
                                    FechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")]),
                                    EstadoColor = oIDataReader[oIDataReader.GetOrdinal("Color")].ToString(), //item.Estado == 1 ? "#009900" : (item.Estado == 2 ? "#333333" : (item.Estado == 3 ? "#8451D3" : "#2E9AFE"))
                                    DesFechaInicio = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaInicio")]).ToString("dd/MM/yyyy"),
                                    DesFechaFin = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaFin")]).ToString("dd/MM/yyyy"),
                                    DescFechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")]).ToString("dd/MM/yyyy"),
                                    EstadoPaquete = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("EstadoPaquete")]),
                                    desProfesor = oIDataReader[oIDataReader.GetOrdinal("desProfesor")].ToString(),
                                    AsesorComercial = oIDataReader[oIDataReader.GetOrdinal("AsesorComercial")].ToString(),
                                    TipoIngreso = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("TipoIngreso")]),
                                    flagPaqueteSedePermiso = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("flagPaqueteSedePermiso")]),
                                    ObtenerTiempoVencimiento = oIDataReader[oIDataReader.GetOrdinal("ObtenerTiempoVencimiento")].ToString(),
                                    ObtenerEstadoCitaNutrional = oIDataReader[oIDataReader.GetOrdinal("ObtenerEstadoCitaNutrional")].ToString(),
                                    ObtenerDisponibilidadHorarioPaquete = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("ObtenerDisponibilidadHorarioPaquete")])
                                });
                            }
                        }
                    }
                }
            }
            return lista;
        }

        public List<ContratoDTO> appsfit_uspListarMembresiasSocios(ContratoDTO oContratoDTO)
        {
            List<ContratoDTO> lista = new List<ContratoDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("appsfit_uspListarMembresiasSocios", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@DefaultKeyUser", System.Data.SqlDbType.VarChar, 50)).Value = oContratoDTO.DefaultKeyUser;
                    cmd.Parameters.Add(new SqlParameter("@DefaultKeyEmpresa", System.Data.SqlDbType.VarChar, 50)).Value = oContratoDTO.DefaultKeyEmpresa;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ContratoDTO()
                                {
                                    CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoMenbresia = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoMenbresia")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    CodigoSocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSocio")]),
                                    Nombres = oIDataReader[oIDataReader.GetOrdinal("Nombres")].ToString(),
                                    Apellidos = oIDataReader[oIDataReader.GetOrdinal("Apellidos")].ToString(),
                                    DNI = oIDataReader[oIDataReader.GetOrdinal("DNI")].ToString(),
                                    imageURL = oIDataReader[oIDataReader.GetOrdinal("ImagenUrl")].ToString(),
                                    CodigoPaquete = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoPaquete")]),
                                    desTipoPaquete = oIDataReader[oIDataReader.GetOrdinal("desTipoPaquete")].ToString(),
                                    NombrePaquete = oIDataReader[oIDataReader.GetOrdinal("NombrePaquete")].ToString(),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("NombreMembresia")].ToString(),

                                    FechaInicio = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaInicio")]),
                                    FechaFin = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaFin")]),
                                    DesFechaInicio = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaInicio")]).ToString("dd/MM/yyyy"),
                                    DesFechaFin = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaFin")]).ToString("dd/MM/yyyy"),

                                    Costo = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Costo")]),
                                    MontoTotal = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("MontoTotal")]),
                                    Debe = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Debe")]),
                                    NroIngreso = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("NroIngresos")]),
                                    NroIngresoActual = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("NroIngresosActual")]),
                                    NroContrato = oIDataReader[oIDataReader.GetOrdinal("NroContrato")].ToString(),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString(),
                                    FechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")]),

                                    DescFechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")]).ToString("dd/MM/yyyy"),
                                    Estado = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Estado")]),
                                    EstadoColor = oIDataReader[oIDataReader.GetOrdinal("Color")].ToString(),
                                    NombreEstado = oIDataReader[oIDataReader.GetOrdinal("NombreEstado")].ToString(),
                                    ObtenerTiempoVencimiento = oIDataReader[oIDataReader.GetOrdinal("ObtenerTiempoVencimiento")].ToString(),
                                    ObtenerEstadoCitaNutrional = oIDataReader[oIDataReader.GetOrdinal("ObtenerEstadoCitaNutrional")].ToString(),
                                    ObtenerDisponibilidadHorarioPaquete = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("ObtenerDisponibilidadHorarioPaquete")]),
                                    AsesorComercial = oIDataReader[oIDataReader.GetOrdinal("AsesorComercial")].ToString(),
                                    flagPaqueteSedePermiso = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("flagPaqueteSedePermiso")]),
                                    CantidadFreezing = (Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantFreezing")]) + Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CongelamientoVigente")])),
                                    CantidadFreezingTomados = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantFreezing")]),
                                    FrezenDisponibles = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CongelamientoVigente")])
                                });
                            }
                        }
                    }
                }
            }
            return lista;
        }


        public List<ContratoDTO> CentroEntrenamiento_uspListarPlataformaPersonasFit_MembresiasCorreo(ContratoDTO oContratoDTO)
        {
            List<ContratoDTO> lista = new List<ContratoDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("CentroEntrenamiento_uspListarPlataformaPersonasFit_MembresiasCorreo", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oContratoDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oContratoDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Correo", System.Data.SqlDbType.VarChar, 100)).Value = oContratoDTO.Correo;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ContratoDTO()
                                {
                                    CodigoMenbresia = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoMenbresia")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    CodigoSocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSocio")]),
                                    CodigoResponsable = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoResponsable")]),
                                    CodigoPaquete = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoPaquete")]),
                                    NombrePaquete = oIDataReader[oIDataReader.GetOrdinal("NombrePaquete")].ToString(),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("NombreMembresia")].ToString(),
                                    FechaInicio = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaInicio")]),
                                    FechaFin = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaFin")]),
                                    Costo = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Costo")]),
                                    MontoTotal = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("MontoTotal")]),
                                    Debe = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Debe")]),
                                    colorEstado = oIDataReader[oIDataReader.GetOrdinal("ColorDeuda")].ToString(),
                                    NroIngreso = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("NroIngresos")]),
                                    NroIngresoActual = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("NroIngresosActual")]),
                                    NroContrato = oIDataReader[oIDataReader.GetOrdinal("NroContrato")].ToString(),
                                    MatriculadoPor = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("MatriculadoPor")]),
                                    FrezenDisponibles = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CongelamientoVigente")]),
                                    DesMatriculadoPor = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("MatriculadoPor")]) == 1 ? "Socio" : (Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("MatriculadoPor")]) == 2 ? "Entrenador" : (Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("MatriculadoPor")]) == 3 ? "Counter" : (Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("MatriculadoPor")]) == 4 ? "Free pass" : (Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("MatriculadoPor")]) == 5 ? "Trabajo de Campo" : "Facebook")))),
                                    Estado = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Estado")]),
                                    desEstado = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Estado")]) == 1 ? "Activo" : Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Estado")]) == 0 ? "Congelado" : Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Estado")]) == 3 ? "Traspaso" : Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Estado")]) == 10 ? "Activo" : Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Estado")]) == 11 ? "Activo" : Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Estado")]) == 12 ? "Activo" : Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Estado")]) == 5 ? "Activo" : "Inactivo",
                                    CantidadFreezing = (Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantFreezing")]) + Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CongelamientoVigente")])),
                                    CantidadFreezingTomados = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantFreezing")]),
                                    EstadoCogelado = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Estado")]) == 1 ? "none" : "",
                                    colorInputCongelado = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Estado")]) == 1 ? "btn-default" : "btn-danger",
                                    MontoCuota = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("MontoCuota")]),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString(),
                                    flagTermino = DateTime.Now > Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaFin")]) ? "#C0C0C0" : "",
                                    FechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")]),
                                    EstadoColor = oIDataReader[oIDataReader.GetOrdinal("Color")].ToString(), //item.Estado == 1 ? "#009900" : (item.Estado == 2 ? "#333333" : (item.Estado == 3 ? "#8451D3" : "#2E9AFE"))
                                    DesFechaInicio = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaInicio")]).ToString("dd/MM/yyyy"),
                                    DesFechaFin = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaFin")]).ToString("dd/MM/yyyy"),
                                    DescFechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")]).ToString("dd/MM/yyyy"),
                                    EstadoPaquete = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("EstadoPaquete")]),
                                    desProfesor = oIDataReader[oIDataReader.GetOrdinal("desProfesor")].ToString(),
                                    AsesorComercial = oIDataReader[oIDataReader.GetOrdinal("AsesorComercial")].ToString(),
                                    TipoIngreso = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("TipoIngreso")]),
                                    flagPaqueteSedePermiso = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("flagPaqueteSedePermiso")]),
                                    NombreComercial = oIDataReader[oIDataReader.GetOrdinal("NombreComercial")].ToString(),
                                    EstadoPermisoReservar = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("EstadoPermisoReservar")])
                                });
                            }
                        }
                    }
                }
            }
            return lista;
        }


        public List<ContratoDTO> uspListarMembresiasContrato(ContratoDTO oContratoDTO)
        {
            List<ContratoDTO> lista = new List<ContratoDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarMembresiasContrato", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oContratoDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oContratoDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSocio", System.Data.SqlDbType.Int)).Value = oContratoDTO.CodigoSocio;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ContratoDTO()
                                {
                                    CodigoDetalle = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoDetalle")]),
                                    CodigoMenbresia = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoMenbresia")]),
                                    CodigoSocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSocio")]),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    Cantidad = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Cantidad")]),
                                    Modelo = oIDataReader[oIDataReader.GetOrdinal("Modelo")].ToString(),
                                    tipoProducto = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Tipo")]),
                                    CodigoPaquete = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoPaquete")]),
                                    NombrePaquete = oIDataReader[oIDataReader.GetOrdinal("NombrePaquete")].ToString(),
                                    FechaInicio = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaInicio")]),
                                    FechaFin = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaFin")]),
                                    Costo = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Costo")]),
                                    MontoTotal = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("MontoTotal")]),
                                    Debe = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Debe")]),
                                    FechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")]),
                                    DesFechaInicio = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaInicio")]).ToString("dd/MM/yyyy"),
                                    DesFechaFin = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaFin")]).ToString("dd/MM/yyyy"),
                                    DescFechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")]).ToString("dd/MM/yyyy"),
                                    AsesorComercial = oIDataReader[oIDataReader.GetOrdinal("AsesorComercial")].ToString(),
                                    TipoIngreso = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("TipoIngreso")])
                                });
                            }
                        }
                    }
                }

            }

            return lista;
        }

        public List<ContratoDTO> uspListarMembresiasTraspasoSocios(ContratoDTO oContratoDTO)
        {
            List<ContratoDTO> lista = new List<ContratoDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarMembresiasTraspasoSocios", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oContratoDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oContratoDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSocio", System.Data.SqlDbType.Int)).Value = oContratoDTO.CodigoSocio;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ContratoDTO()
                                {
                                    CodigoMenbresia = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoMenbresia")]),
                                    CodigoSocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSocio")]),
                                    CodigoResponsable = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoResponsable")]),
                                    CodigoPaquete = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoPaquete")]),
                                    NombrePaquete = oIDataReader[oIDataReader.GetOrdinal("NombrePaquete")].ToString(),
                                    FechaInicio = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaInicio")]),
                                    FechaFin = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaFin")]),
                                    Costo = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Costo")]),
                                    MontoTotal = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("MontoTotal")]),
                                    Debe = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Debe")]),
                                    colorEstado = oIDataReader[oIDataReader.GetOrdinal("ColorDeuda")].ToString(),
                                    NroIngreso = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("NroIngresos")]),
                                    NroIngresoActual = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("NroIngresosActual")]),
                                    NroContrato = oIDataReader[oIDataReader.GetOrdinal("NroContrato")].ToString(),
                                    MatriculadoPor = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("MatriculadoPor")]),
                                    FrezenDisponibles = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CongelamientoVigente")]),
                                    DesMatriculadoPor = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("MatriculadoPor")]) == 1 ? "Socio" : (Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("MatriculadoPor")]) == 2 ? "Entrenador" : (Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("MatriculadoPor")]) == 3 ? "Counter" : (Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("MatriculadoPor")]) == 4 ? "Free pass" : (Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("MatriculadoPor")]) == 5 ? "Trabajo de Campo" : "Facebook")))),
                                    Estado = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Estado")]),
                                    desEstado = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Estado")]) == 1 ? "Activo" : Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Estado")]) == 0 ? "Activo" : Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Estado")]) == 3 ? "Activo" : Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Estado")]) == 10 ? "Activo" : Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Estado")]) == 11 ? "Activo" : Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Estado")]) == 12 ? "Activo" : Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Estado")]) == 5 ? "Activo" : "Inactivo",
                                    CantidadFreezing = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantFreezing")]),
                                    EstadoCogelado = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Estado")]) == 1 ? "none" : "",
                                    colorInputCongelado = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Estado")]) == 1 ? "btn-default" : "btn-danger",

                                    MontoCuota = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("MontoCuota")]),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString(),
                                    flagTermino = DateTime.Now > Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaFin")]) ? "#C0C0C0" : "",
                                    FechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")]),
                                    EstadoColor = oIDataReader[oIDataReader.GetOrdinal("Color")].ToString(),
                                    DesFechaInicio = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaInicio")]).ToString("dd/MM/yyyy"),
                                    DesFechaFin = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaFin")]).ToString("dd/MM/yyyy"),
                                    DescFechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")]).ToString("dd/MM/yyyy"),
                                    EstadoPaquete = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("EstadoPaquete")])
                                });
                            }
                        }
                    }
                }

            }

            return lista;
        }

        public ContratoDTO BuscarPorCodigoMenbresias(ContratoDTO oContratoDTO)
        {
            ContratoDTO itemDTO = new ContratoDTO();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspBuscarMenbresiasPorCodigo", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oContratoDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.VarChar, 100)).Value = oContratoDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoMenbresia", System.Data.SqlDbType.Int)).Value = oContratoDTO.CodigoMenbresia;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO.CodigoMenbresia = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoMenbresia")]);
                                itemDTO.CodigoSocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSocio")]);
                                itemDTO.CodigoPaquete = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoPaquete")]);
                                itemDTO.codigoValorPaquete = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoPaquete")]) + "|" + Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("valorPaquete")]) + "|" + Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Costo")]);
                                itemDTO.FechaInicio = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaInicio")]);
                                itemDTO.FechaFin = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaFin")]);
                                itemDTO.NroIngreso = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("NroIngresos")]);
                                itemDTO.NroIngresoActual = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("NroIngresosActual")]);
                                itemDTO.NroContrato = oIDataReader[oIDataReader.GetOrdinal("NroContrato")].ToString();
                                itemDTO.MatriculadoPor = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("MatriculadoPor")]);
                                itemDTO.FrezenDisponibles = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CongelamientoVigente")]);
                                itemDTO.Costo = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Costo")]);
                                itemDTO.Estado = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Estado")]);
                                itemDTO.FechaEdicion = oIDataReader[oIDataReader.GetOrdinal("FechaEdicion")] == null ? DateTime.Now : Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaEdicion")]);
                                itemDTO.UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString();
                                itemDTO.FechaCongelacionProgramada = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCongelacionProgramada")]);

                                itemDTO.FechaDesCongelacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaDesCongelacion")]);
                                itemDTO.TipoMembresia = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("TipoMembresia")]);
                                itemDTO.CodigoMebresiaOrigen = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("OrigenMembresia")]);
                                itemDTO.ObservacionTraspaso = oIDataReader[oIDataReader.GetOrdinal("ObservacionTraspaso")].ToString();
                                itemDTO.TipoDescuento = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("TipoDescuento")]);
                                itemDTO.MontoDescuento = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("MontoDescuento")]);
                                itemDTO.Observacion = oIDataReader[oIDataReader.GetOrdinal("observacion")].ToString();
                                itemDTO.AsesorComercial = oIDataReader[oIDataReader.GetOrdinal("AsesorComercial")].ToString();
                                itemDTO.TipoIngreso = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("TipoIngreso")]);
                                itemDTO.TipoContrato = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("TipoContrato")]);
                                itemDTO.CodigoProfesor = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoProfesor")]);
                                itemDTO.CodTiempoMenbresia = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("TiempoMenbresia")]);
                            }
                        }
                    }
                }
            }

            return itemDTO;

        }

        public ContratoDTO ValidarIngresoDiaPaquete(ContratoDTO oContratoDTO)
        {
            ContratoDTO itemDTO = new ContratoDTO();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspValidarIngresoDiaPaquete", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oContratoDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.VarChar, 100)).Value = oContratoDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoMenbresia", System.Data.SqlDbType.Int)).Value = oContratoDTO.CodigoMenbresia;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new ContratoDTO()
                                {
                                    CantidadAsistencia = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadAsistencia")]),
                                    NroIngresoDia = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("NroIngresoDia")])
                                };
                            }
                        }
                    }
                }
            }

            return itemDTO;
        }

        public ContratoDTO VerInformacionMenbresias(ContratoDTO oContratoDTO)
        {
            ContratoDTO itemDTO = new ContratoDTO();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspVerInformacionMenbresias", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oContratoDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.VarChar, 100)).Value = oContratoDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoMembresia", System.Data.SqlDbType.Int)).Value = oContratoDTO.CodigoMenbresia;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new ContratoDTO();

                                itemDTO.CodigoMenbresia = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoMenbresia")]);
                                itemDTO.CodigoSocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSocio")]);
                                itemDTO.CodigoResponsable = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoResponsable")]);
                                itemDTO.NombreResponsable = oIDataReader[oIDataReader.GetOrdinal("NomColaborador")].ToString();
                                itemDTO.CodigoPaquete = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoPaquete")]);
                                itemDTO.NombrePaquete = oIDataReader[oIDataReader.GetOrdinal("NombrePaquete")].ToString();
                                itemDTO.DesFechaInicio = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaInicio")]).ToString("dd/MM/yyyy");
                                itemDTO.DesFechaFin = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaFin")]).ToString("dd/MM/yyyy");
                                itemDTO.Costo = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Costo")]);
                                itemDTO.MontoTotal = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("MontoTotal")]);
                                itemDTO.Debe = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Debe")]);
                                itemDTO.colorEstado = oIDataReader[oIDataReader.GetOrdinal("ColorDeuda")].ToString();
                                itemDTO.NroIngreso = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("NroIngresos")]);
                                itemDTO.NroIngresoActual = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("NroIngresosActual")]);
                                itemDTO.NroContrato = oIDataReader[oIDataReader.GetOrdinal("NroContrato")].ToString();
                                itemDTO.MatriculadoPor = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("MatriculadoPor")]);
                                itemDTO.FrezenDisponibles = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CongelamientoVigente")]);
                                itemDTO.DesMatriculadoPor = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("MatriculadoPor")]) == 1 ? "Socio" : (Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("MatriculadoPor")]) == 2 ? "Entrenador" : (Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("MatriculadoPor")]) == 3 ? "Counter" : (Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("MatriculadoPor")]) == 4 ? "Free pass" : (Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("MatriculadoPor")]) == 5 ? "Trabajo de Campo" : "Facebook"))));
                                itemDTO.Estado = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Estado")]);
                                itemDTO.EstadoCogelado = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Estado")]) == 1 ? "none" : "";
                                itemDTO.EstadoInfoCogelado = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Estado")]);
                                itemDTO.EstadoColor = oIDataReader[oIDataReader.GetOrdinal("Color")].ToString();
                                itemDTO.EstadoDescripcion = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Estado")]) == 1 ? "Activo" : (Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Estado")]) == 2 ? "Finalizado" : (Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Estado")]) == 3 ? "Traspasado" : "Congelado"));
                                itemDTO.colorInputCongelado = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Estado")]) == 1 ? "btn-default" : "btn-danger";
                                itemDTO.MontoCuota = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("MontoCuota")]);
                                itemDTO.UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString();
                                itemDTO.flagTermino = DateTime.Now > Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaFin")]) ? "#C0C0C0" : "";
                                itemDTO.FechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")]);
                                itemDTO.DescFechaCongelacionProgramada = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCongelacionProgramada")]).ToString("dd/MM/yyyy");
                                itemDTO.DescFechaDesCongelacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaDesCongelacion")]).ToString("dd/MM/yyyy");
                                itemDTO.TipoMembresia = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("TipoMembresia")]);
                                itemDTO.ObservacionTraspaso = oIDataReader[oIDataReader.GetOrdinal("ObservacionTraspaso")].ToString();
                                itemDTO.SocioTraspasoReceptor = oIDataReader[oIDataReader.GetOrdinal("SocioTraspasoReceptor")].ToString();
                                itemDTO.EstadoPaquete = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("EstadoPaquete")]);
                                itemDTO.FechaInicio = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaInicio")]);
                                itemDTO.FechaFin = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaFin")]);
                                itemDTO.Hoy = DateTime.Now;
                                itemDTO.Observacion = oIDataReader[oIDataReader.GetOrdinal("observacion")].ToString();
                                itemDTO.AsesorComercial = oIDataReader[oIDataReader.GetOrdinal("AsesorComercial")].ToString();
                                itemDTO.MotivoCongelamiento = oIDataReader[oIDataReader.GetOrdinal("MotivoCongelamiento")].ToString();


                                //itemDTO = new ContratoDTO()
                                //{
                                //    CodigoMenbresia = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoMenbresia")]),
                                //    CodigoSocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSocio")]),
                                //    CodigoResponsable = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoResponsable")]),
                                //    NombreResponsable = oIDataReader[oIDataReader.GetOrdinal("NomColaborador")].ToString(),
                                //    CodigoPaquete = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoPaquete")]),
                                //    NombrePaquete = oIDataReader[oIDataReader.GetOrdinal("NombrePaquete")].ToString(),
                                //    DesFechaInicio = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaInicio")]).ToString("dd/MM/yyyy"),
                                //    DesFechaFin = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaFin")]).ToString("dd/MM/yyyy"),
                                //    Costo = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Costo")]),
                                //    MontoTotal = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("MontoTotal")]),
                                //    Debe = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Debe")]),
                                //    colorEstado = oIDataReader[oIDataReader.GetOrdinal("ColorDeuda")].ToString(),
                                //    NroIngreso = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("NroIngresos")]),
                                //    NroIngresoActual = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("NroIngresosActual")]),
                                //    NroContrato = oIDataReader[oIDataReader.GetOrdinal("NroContrato")].ToString(),
                                //    MatriculadoPor = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("MatriculadoPor")]),
                                //    FrezenDisponibles = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CongelamientoVigente")]),
                                //    DesMatriculadoPor = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("MatriculadoPor")]) == 1 ? "Socio" : (Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("MatriculadoPor")]) == 2 ? "Entrenador" : (Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("MatriculadoPor")]) == 3 ? "Counter" : (Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("MatriculadoPor")]) == 4 ? "Free pass" : (Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("MatriculadoPor")]) == 5 ? "Trabajo de Campo" : "Facebook")))),
                                //    Estado = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Estado")]),
                                //    EstadoCogelado = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Estado")]) == 1 ? "none" : "",
                                //    EstadoInfoCogelado = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Estado")]),
                                //    EstadoColor = oIDataReader[oIDataReader.GetOrdinal("Color")].ToString(),
                                //    EstadoDescripcion = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Estado")]) == 1 ? "Activo" : (Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Estado")]) == 2 ? "Finalizado" : (Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Estado")]) == 3 ? "Traspasado" : "Congelado")),
                                //    colorInputCongelado = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Estado")]) == 1 ? "btn-default" : "btn-danger",
                                //    MontoCuota = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("MontoCuota")]),
                                //    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString(),
                                //    flagTermino = DateTime.Now > Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaFin")]) ? "#C0C0C0" : "",
                                //    FechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")]),
                                //    DescFechaCongelacionProgramada = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCongelacionProgramada")]).ToString("dd/MM/yyyy"),
                                //    DescFechaDesCongelacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaDesCongelacion")]).ToString("dd/MM/yyyy"),
                                //    TipoMembresia = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("TipoMembresia")]),
                                //    ObservacionTraspaso = oIDataReader[oIDataReader.GetOrdinal("ObservacionTraspaso")].ToString(),
                                //    SocioTraspasoReceptor = oIDataReader[oIDataReader.GetOrdinal("SocioTraspasoReceptor")].ToString(),
                                //    EstadoPaquete = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("EstadoPaquete")]),
                                //    FechaInicio = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaInicio")]),
                                //    FechaFin = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaFin")]),
                                //    Hoy = DateTime.Now,
                                //    Observacion = oIDataReader[oIDataReader.GetOrdinal("observacion")].ToString(),
                                //    AsesorComercial = oIDataReader[oIDataReader.GetOrdinal("AsesorComercial")].ToString()
                                //};
                            }
                        }
                    }
                }
            }

            return itemDTO;
        }

        public List<ContratoDTO> uspListarMembresiasSociosClasesGrupales(ContratoDTO oContratoDTO)
        {
            List<ContratoDTO> lista = new List<ContratoDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarMembresiasSociosClasesGrupales", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oContratoDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oContratoDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSocio", System.Data.SqlDbType.Int)).Value = oContratoDTO.CodigoSocio;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ContratoDTO()
                                {
                                    CodigoMenbresia = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoMenbresia")]),
                                    CodigoSocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSocio")]),
                                    CodigoResponsable = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoResponsable")]),
                                    CodigoPaquete = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoPaquete")]),
                                    NombrePaquete = oIDataReader[oIDataReader.GetOrdinal("NombrePaquete")].ToString(),
                                    //Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    FechaInicio = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaInicio")]),
                                    FechaFin = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaFin")]),
                                    Costo = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Costo")]),
                                    MontoTotal = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("MontoTotal")]),
                                    Debe = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Debe")]),
                                    colorEstado = oIDataReader[oIDataReader.GetOrdinal("ColorDeuda")].ToString(),
                                    NroIngreso = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("NroIngresos")]),
                                    NroIngresoActual = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("NroIngresosActual")]),
                                    NroContrato = oIDataReader[oIDataReader.GetOrdinal("NroContrato")].ToString(),
                                    MatriculadoPor = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("MatriculadoPor")]),
                                    FrezenDisponibles = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CongelamientoVigente")]),
                                    DesMatriculadoPor = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("MatriculadoPor")]) == 1 ? "Socio" : (Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("MatriculadoPor")]) == 2 ? "Entrenador" : (Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("MatriculadoPor")]) == 3 ? "Counter" : (Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("MatriculadoPor")]) == 4 ? "Free pass" : (Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("MatriculadoPor")]) == 5 ? "Trabajo de Campo" : "Facebook")))),
                                    Estado = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Estado")]),
                                    desEstado = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Estado")]) == 1 ? "Activo" : Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Estado")]) == 0 ? "Congelado" : Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Estado")]) == 3 ? "Traspaso" : Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Estado")]) == 10 ? "Activo" : Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Estado")]) == 11 ? "Activo" : Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Estado")]) == 12 ? "Activo" : Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Estado")]) == 5 ? "Activo" : "Inactivo",
                                    CantidadFreezing = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantFreezing")]),
                                    EstadoCogelado = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Estado")]) == 1 ? "none" : "",
                                    colorInputCongelado = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Estado")]) == 1 ? "btn-default" : "btn-danger",
                                    MontoCuota = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("MontoCuota")]),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString(),
                                    flagTermino = DateTime.Now > Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaFin")]) ? "#C0C0C0" : "",
                                    FechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")]),
                                    EstadoColor = oIDataReader[oIDataReader.GetOrdinal("Color")].ToString(),//item.Estado == 1 ? "#009900" : (item.Estado == 2 ? "#333333" : (item.Estado == 3 ? "#8451D3" : "#2E9AFE"))
                                    DesFechaInicio = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaInicio")]).ToString("dd/MM/yyyy"),
                                    DesFechaFin = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaFin")]).ToString("dd/MM/yyyy"),
                                    DescFechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")]).ToString("dd/MM/yyyy"),
                                    EstadoPaquete = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("EstadoPaquete")]),
                                    desProfesor = oIDataReader[oIDataReader.GetOrdinal("desProfesor")].ToString(),
                                    Mensaje = oIDataReader[oIDataReader.GetOrdinal("TextoMembresiaVencto")].ToString()
                                });
                            }
                        }
                    }
                }

            }

            return lista;
        }

        public int Registrar(ContratoDTO item)
        {
            int? campoRetorno = 0;

            var sscsb = new SqlConnectionStringBuilder(Helper.Conexion());
            sscsb.ConnectTimeout = 180;
            using (var conn = new SqlConnection(sscsb.ConnectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspRegistrarMenbresias", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.AddWithValue("@CodigoMenbresia", 0).Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSocio", System.Data.SqlDbType.Int)).Value = item.CodigoSocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoResponsable", System.Data.SqlDbType.Int)).Value = item.CodigoResponsable;
                    cmd.Parameters.Add(new SqlParameter("@CodigoPaquete", System.Data.SqlDbType.Int)).Value = item.CodigoPaquete;

                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = item.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = item.FechaFin;
                    cmd.Parameters.Add(new SqlParameter("@Costo", System.Data.SqlDbType.Decimal)).Value = item.Costo;
                    cmd.Parameters.Add(new SqlParameter("@NroIngresos", System.Data.SqlDbType.Int)).Value = item.NroIngreso;
                    cmd.Parameters.Add(new SqlParameter("@NroIngresosActual", System.Data.SqlDbType.Int)).Value = item.NroIngresoActual;

                    cmd.Parameters.Add(new SqlParameter("@NroContrato", System.Data.SqlDbType.VarChar, 50)).Value = item.NroContrato;
                    cmd.Parameters.Add(new SqlParameter("@CongelamientoVigente", System.Data.SqlDbType.Int)).Value = item.FrezenDisponibles;
                    cmd.Parameters.Add(new SqlParameter("@MatriculadoPor", System.Data.SqlDbType.Int)).Value = item.MatriculadoPor;
                    cmd.Parameters.Add(new SqlParameter("@Estado", System.Data.SqlDbType.Int)).Value = item.Estado;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;

                    cmd.Parameters.Add(new SqlParameter("@TipoMembresia", System.Data.SqlDbType.Int)).Value = item.TipoMembresia;
                    cmd.Parameters.Add(new SqlParameter("@CodigoMebresiaOrigen", System.Data.SqlDbType.Int)).Value = item.CodigoMebresiaOrigen;
                    cmd.Parameters.Add(new SqlParameter("@ObservacionTraspaso", System.Data.SqlDbType.VarChar, 200)).Value = item.ObservacionTraspaso;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@TipoDescuento", System.Data.SqlDbType.Int)).Value = item.TipoDescuento;
                    cmd.Parameters.Add(new SqlParameter("@MontoDescuento", System.Data.SqlDbType.Decimal)).Value = item.MontoDescuento;

                    cmd.Parameters.Add(new SqlParameter("@Observacion", System.Data.SqlDbType.VarChar, 200)).Value = item.Observacion;
                    cmd.Parameters.Add(new SqlParameter("@AsesorComercial", System.Data.SqlDbType.VarChar, 100)).Value = item.AsesorComercial;
                    cmd.Parameters.Add(new SqlParameter("@TipoIngreso", System.Data.SqlDbType.Int)).Value = item.TipoIngreso;
                    cmd.Parameters.Add(new SqlParameter("@IndTraspaso", System.Data.SqlDbType.VarChar, 5)).Value = item.IndTraspaso;
                    cmd.Parameters.Add(new SqlParameter("@TipoContrato", System.Data.SqlDbType.Int)).Value = item.TipoContrato;

                    cmd.Parameters.Add(new SqlParameter("@CodigoSocioTraspasado", System.Data.SqlDbType.Int)).Value = item.OrigenSociosTraspaso;
                    cmd.Parameters.Add(new SqlParameter("@MembresiaTraspasada", System.Data.SqlDbType.Int)).Value = item.OrigenMembresiaTraspaso;
                    cmd.Parameters.Add(new SqlParameter("@CodigoProfesor", System.Data.SqlDbType.Int)).Value = item.CodigoProfesor;
                    cmd.Parameters.Add(new SqlParameter("@CodTiempoMenbresia", System.Data.SqlDbType.Int)).Value = item.CodTiempoMenbresia;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioEmisor", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioEmisor;

                    cmd.Parameters.Add(new SqlParameter("@CodigoInicioSesion", System.Data.SqlDbType.Int)).Value = item.CodigoInicioSesion;
                    cmd.CommandTimeout = 180;
                    cmd.ExecuteNonQuery();
                    campoRetorno = Convert.ToInt32(cmd.Parameters["@CodigoMenbresia"].Value);
                }
            }
            return Convert.ToInt32(campoRetorno);
        }

        public int RegistrarConfirmarMenbresiasTraspaso(ContratoDTO item)
        {
            int? campoRetorno = 0;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspRegistrarConfirmarMenbresiasTraspaso", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Codigo", 0).Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@CodigoMenbresia", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSocio", System.Data.SqlDbType.Int)).Value = item.CodigoSocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoResponsable", System.Data.SqlDbType.Int)).Value = item.CodigoResponsable;
                    cmd.Parameters.Add(new SqlParameter("@CodigoPaquete", System.Data.SqlDbType.Int)).Value = item.CodigoPaquete;

                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = item.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = item.FechaFin;
                    cmd.Parameters.Add(new SqlParameter("@Costo", System.Data.SqlDbType.Decimal)).Value = item.Costo;
                    cmd.Parameters.Add(new SqlParameter("@NroIngresos", System.Data.SqlDbType.Int)).Value = item.NroIngreso;
                    cmd.Parameters.Add(new SqlParameter("@NroIngresosActual", System.Data.SqlDbType.Int)).Value = item.NroIngresoActual;

                    cmd.Parameters.Add(new SqlParameter("@NroContrato", System.Data.SqlDbType.VarChar, 50)).Value = item.NroContrato;
                    cmd.Parameters.Add(new SqlParameter("@CongelamientoVigente", System.Data.SqlDbType.Int)).Value = item.FrezenDisponibles;
                    cmd.Parameters.Add(new SqlParameter("@MatriculadoPor", System.Data.SqlDbType.Int)).Value = item.MatriculadoPor;
                    cmd.Parameters.Add(new SqlParameter("@Estado", System.Data.SqlDbType.Int)).Value = item.Estado;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;

                    cmd.Parameters.Add(new SqlParameter("@TipoMembresia", System.Data.SqlDbType.Int)).Value = item.TipoMembresia;
                    cmd.Parameters.Add(new SqlParameter("@ObservacionTraspaso", System.Data.SqlDbType.VarChar, 200)).Value = item.ObservacionTraspaso;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@TipoDescuento", System.Data.SqlDbType.Int)).Value = item.TipoDescuento;
                    cmd.Parameters.Add(new SqlParameter("@MontoDescuento", System.Data.SqlDbType.Decimal)).Value = item.MontoDescuento;

                    cmd.Parameters.Add(new SqlParameter("@Observacion", System.Data.SqlDbType.VarChar, 200)).Value = item.Observacion;
                    cmd.Parameters.Add(new SqlParameter("@AsesorComercial", System.Data.SqlDbType.VarChar, 100)).Value = item.AsesorComercial;
                    cmd.Parameters.Add(new SqlParameter("@TipoIngreso", System.Data.SqlDbType.Int)).Value = item.TipoIngreso;
                    cmd.Parameters.Add(new SqlParameter("@IndTraspaso", System.Data.SqlDbType.VarChar, 5)).Value = item.IndTraspaso;
                    cmd.Parameters.Add(new SqlParameter("@TipoContrato", System.Data.SqlDbType.Int)).Value = item.TipoContrato;

                    cmd.Parameters.Add(new SqlParameter("@CodigoSocioTraspasado", System.Data.SqlDbType.Int)).Value = item.OrigenSociosTraspaso;
                    cmd.Parameters.Add(new SqlParameter("@MembresiaTraspasada", System.Data.SqlDbType.Int)).Value = item.OrigenMembresiaTraspaso;
                    cmd.Parameters.Add(new SqlParameter("@EstadoTraspaso", System.Data.SqlDbType.Int)).Value = item.EstadoTraspaso;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioEmisor", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioEmisor;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.VarChar, 100)).Value = item.CodigoUnidadNegocio;

                    cmd.ExecuteNonQuery();
                    campoRetorno = Convert.ToInt32(cmd.Parameters["@Codigo"].Value);
                }
            }
            return Convert.ToInt32(campoRetorno);
        }

        public ContratoDTO ObtenerTiempoVencimiento(ContratoDTO item)
        {
            string campoRetorno = string.Empty;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspObtenerTiempoVencimiento", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@codigo", System.Data.SqlDbType.Int)).Value = item.CodigoMenbresia;

                    cmd.Parameters.AddWithValue("@mensaje", campoRetorno).Direction = System.Data.ParameterDirection.Output;


                    cmd.ExecuteNonQuery();
                    campoRetorno = cmd.Parameters["@mensaje"].Value.ToString();
                }
            }

            item.Mensaje = campoRetorno;

            return item;
        }

        public void Actualizar(ContratoDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspActualizarMenbresias", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoMenbresia", System.Data.SqlDbType.Int)).Value = item.CodigoMenbresia;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSocio", System.Data.SqlDbType.Int)).Value = item.CodigoSocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoResponsable", System.Data.SqlDbType.Int)).Value = item.CodigoResponsable;
                    cmd.Parameters.Add(new SqlParameter("@CodigoPaquete", System.Data.SqlDbType.Int)).Value = item.CodigoPaquete;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = item.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = item.FechaFin;
                    cmd.Parameters.Add(new SqlParameter("@Costo", System.Data.SqlDbType.Decimal)).Value = item.Costo;
                    cmd.Parameters.Add(new SqlParameter("@NroIngresos", System.Data.SqlDbType.Int)).Value = item.NroIngreso;
                    cmd.Parameters.Add(new SqlParameter("@NroIngresosActual", System.Data.SqlDbType.Int)).Value = item.NroIngresoActual;
                    cmd.Parameters.Add(new SqlParameter("@MatriculadoPor", System.Data.SqlDbType.Int)).Value = item.MatriculadoPor;

                    cmd.Parameters.Add(new SqlParameter("@CongelamientoVigente", System.Data.SqlDbType.Int)).Value = item.FrezenDisponibles;
                    cmd.Parameters.Add(new SqlParameter("@Estado", System.Data.SqlDbType.Int)).Value = item.Estado;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioEdicion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioEdicion;
                    cmd.Parameters.Add(new SqlParameter("@NroContrato", System.Data.SqlDbType.VarChar, 50)).Value = item.NroContrato;
                    cmd.Parameters.Add(new SqlParameter("@ObservacionTraspaso", System.Data.SqlDbType.VarChar, 200)).Value = item.ObservacionTraspaso;
                    cmd.Parameters.Add(new SqlParameter("@TipoDescuento", System.Data.SqlDbType.Int)).Value = item.TipoDescuento;
                    cmd.Parameters.Add(new SqlParameter("@MontoDescuento", System.Data.SqlDbType.Decimal)).Value = item.MontoDescuento;
                    cmd.Parameters.Add(new SqlParameter("@Observacion", System.Data.SqlDbType.VarChar, 200)).Value = item.Observacion;
                    cmd.Parameters.Add(new SqlParameter("@AsesorComercial", System.Data.SqlDbType.VarChar, 100)).Value = item.AsesorComercial;
                    cmd.Parameters.Add(new SqlParameter("@TipoIngreso", System.Data.SqlDbType.Int)).Value = item.TipoIngreso;

                    cmd.Parameters.Add(new SqlParameter("@TipoContrato", System.Data.SqlDbType.Int)).Value = item.TipoContrato;
                    cmd.Parameters.Add(new SqlParameter("@CodigoMebresiaOrigen", System.Data.SqlDbType.Int)).Value = item.CodigoMebresiaOrigen;
                    cmd.Parameters.Add(new SqlParameter("@CodigoProfesor", System.Data.SqlDbType.Int)).Value = item.CodigoProfesor;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioEmisor;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoInicioSesion", System.Data.SqlDbType.Int)).Value = item.CodigoInicioSesion;

                    cmd.ExecuteNonQuery();
                }
            }

        }

        public void uspActualizarMenbresiasFechaInicio(ContratoDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspActualizarMenbresiasFechaInicio", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoMenbresia", System.Data.SqlDbType.Int)).Value = item.CodigoMenbresia;
                    cmd.Parameters.Add(new SqlParameter("@CodigoInicioSesion", System.Data.SqlDbType.Int)).Value = item.CodigoInicioSesion;

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void ActualizarFrezing(ContratoDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspActualizarMenbresiasFreezing", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoMenbresia", System.Data.SqlDbType.Int)).Value = item.CodigoMenbresia;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = item.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = item.FechaFin;
                    cmd.Parameters.Add(new SqlParameter("@CongelamientoVigente", System.Data.SqlDbType.Int)).Value = item.FrezenDisponibles;
                    cmd.Parameters.Add(new SqlParameter("@FechaCongelacionProgramada", System.Data.SqlDbType.DateTime)).Value = item.FechaCongelacionProgramada;
                    cmd.Parameters.Add(new SqlParameter("@FechaDesCongelacion", System.Data.SqlDbType.DateTime)).Value = item.FechaDesCongelacion;
                    cmd.Parameters.Add(new SqlParameter("@Estado", System.Data.SqlDbType.Int)).Value = item.Estado;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioEdicion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioEdicion;
                    cmd.Parameters.Add(new SqlParameter("@CodigoHistorialMenbresia", System.Data.SqlDbType.Int)).Value = item.CodigoHistorialMenbresia;

                    cmd.ExecuteNonQuery();
                }
            }

        }

        public string ActualizarNroIngreso(ContratoDTO item)
        {

            int? CantidadNroIngresos = 0;
            string Mensaje = "";

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspActualizarMenbresiasNroIngreso", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoMenbresia", System.Data.SqlDbType.Int)).Value = item.CodigoMenbresia;
                    cmd.Parameters.Add(new SqlParameter("@CodigoPersona", System.Data.SqlDbType.Int)).Value = item.CodigoSocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;

                    SqlParameter PmtrNroIngresosActual = new SqlParameter("@NroIngresosActual", System.Data.SqlDbType.Int);
                    PmtrNroIngresosActual.Direction = System.Data.ParameterDirection.Output;

                    SqlParameter PmtrMensaje = new SqlParameter("@Mensaje", System.Data.SqlDbType.VarChar, 200);
                    PmtrMensaje.Direction = System.Data.ParameterDirection.Output;

                    cmd.Parameters.Add(PmtrNroIngresosActual);
                    cmd.Parameters.Add(PmtrMensaje);

                    //cmd.Parameters.AddWithValue("@NroIngresosActual", CantidadNroIngresos).Direction = System.Data.ParameterDirection.Output;
                    //cmd.Parameters.AddWithValue("@Mensaje", Mensaje).Direction = System.Data.ParameterDirection.Output;

                    cmd.ExecuteNonQuery();
                    CantidadNroIngresos = Convert.ToInt32(cmd.Parameters["@NroIngresosActual"].Value);
                    Mensaje = cmd.Parameters["@Mensaje"].Value.ToString();
                }
            }

            return CantidadNroIngresos + "|" + Mensaje;
        }

        public void ActualizarEstadosMembresia(ContratoDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspActualizarMenbresiasEstados", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoMenbresia", System.Data.SqlDbType.Int)).Value = item.CodigoMenbresia;
                    cmd.Parameters.Add(new SqlParameter("@Estado", System.Data.SqlDbType.Int)).Value = item.Estado;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSocioReceptor", System.Data.SqlDbType.Int)).Value = item.CodigoSocioReceptor;
                    cmd.Parameters.Add(new SqlParameter("@ResponsableTraspaso", System.Data.SqlDbType.VarChar, 200)).Value = item.UsuarioResponsable;

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Eliminar(ContratoDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspEliminarMenbresias", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoMenbresia", System.Data.SqlDbType.Int)).Value = item.CodigoMenbresia;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;
                    cmd.Parameters.Add(new SqlParameter("@CodigoInicioSesion", System.Data.SqlDbType.Int)).Value = item.CodigoInicioSesion;

                    cmd.ExecuteNonQuery();
                }
            }

        }

        public void uspUpdateEstadoMembresias_Congelacion_Descongelacion_Activo_Inactivo(ContratoDTO oContratoDTO)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspUpdateEstadoMembresias_Congelacion_Descongelacion_Activo_Inactivo", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oContratoDTO.CodigoUnidadNegocio;

                    cmd.ExecuteNonQuery();
                }
            }

        }






        //********************************************** FOR API ****************************************************

        public int RegistrarContratoApi(ContratoDTO item)
        {
            int? campoRetorno = 0;

            var sscsb = new SqlConnectionStringBuilder(Helper.Conexion());
            sscsb.ConnectTimeout = 180;
            using (var conn = new SqlConnection(sscsb.ConnectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspRegistrarMenbresiasApp", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CodigoMenbresia", 0).Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@DefaultKeyEmpresa", System.Data.SqlDbType.VarChar)).Value = item.DefaultKeyEmpresa;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSocio", System.Data.SqlDbType.Int)).Value = item.CodigoSocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoPaquete", System.Data.SqlDbType.Int)).Value = item.CodigoPaquete;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = item.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = item.FechaFin;
                    cmd.Parameters.Add(new SqlParameter("@Costo", System.Data.SqlDbType.Decimal)).Value = item.Costo;
                    cmd.Parameters.Add(new SqlParameter("@NroIngresos", System.Data.SqlDbType.Int)).Value = item.NroIngreso;
                    cmd.Parameters.Add(new SqlParameter("@CongelamientoVigente", System.Data.SqlDbType.Int)).Value = item.FrezenDisponibles;
                    cmd.Parameters.Add(new SqlParameter("@CodigoMebresiaOrigen", System.Data.SqlDbType.Int)).Value = item.CodigoMebresiaOrigen;
                    cmd.Parameters.Add(new SqlParameter("@Observacion", System.Data.SqlDbType.VarChar)).Value = item.Observacion;
                    cmd.Parameters.Add(new SqlParameter("@AsesorComercial", System.Data.SqlDbType.VarChar)).Value = "SIN VENDEDOR";
                    cmd.Parameters.Add(new SqlParameter("@TipoIngreso", System.Data.SqlDbType.Int)).Value = item.TipoIngreso;
                    
                    cmd.CommandTimeout = 180;
                    cmd.ExecuteNonQuery();
                    campoRetorno = Convert.ToInt32(cmd.Parameters["@CodigoMenbresia"].Value);
                }
            }
            return Convert.ToInt32(campoRetorno);
        }


    }

}

