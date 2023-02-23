using E_DataModel.Common;
using E_DataModel.Gimnasio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace E_DataLayer.Gimnasio
{
    public class ConfiguracionData
    {

        private string ConvertMes(int mes)
        {

            string texto = "";
            switch (mes)
            {
                case 1:
                    texto = "Enero";
                    break;
                case 2:
                    texto = "Febrero";
                    break;
                case 3:
                    texto = "Marzo";
                    break;
                case 4:
                    texto = "Abril";
                    break;
                case 5:
                    texto = "Mayo";
                    break;
                case 6:
                    texto = "Junio";
                    break;
                case 7:
                    texto = "Julio";
                    break;
                case 8:
                    texto = "Agosto";
                    break;
                case 9:
                    texto = "Setiembre";
                    break;
                case 10:
                    texto = "Octubre";
                    break;
                case 11:
                    texto = "Noviembre";
                    break;
                case 12:
                    texto = "Diciembre";
                    break;
            }

            return texto;
        }


        public List<ConfiguracionDTO> uspByteFit_ListarTotalVentasPorEmpresa(ConfiguracionDTO oConfiguracion)
        {
            List<ConfiguracionDTO> lista = new List<ConfiguracionDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspByteFit_ListarTotalVentasPorEmpresa", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oConfiguracion.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oConfiguracion.CodigoSede;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ConfiguracionDTO()
                                {
                                    Importe = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Importe")]),
                                    Anio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Anio")]),
                                    Mes = ConvertMes(Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Mes")]))
                                });
                            }
                        }

                    }
                }
            }

            return lista;
        }

        public List<ConfiguracionDTO> uspByteFitVentasPorUN(ConfiguracionDTO oConfiguracion)
        {
            List<ConfiguracionDTO> lista = new List<ConfiguracionDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspByteFitVentasPorUN", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oConfiguracion.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oConfiguracion.CodigoSede;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ConfiguracionDTO()
                                {
                                    CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    FechaPago = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaPago")]),
                                    FechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")]),
                                    MontoAcuenta = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("MontoAcuenta")]),
                                    UrlRecibo = oIDataReader[oIDataReader.GetOrdinal("UrlRecibo")].ToString(),
                                    DesEstadoRecibo = oIDataReader[oIDataReader.GetOrdinal("UrlRecibo")].ToString() == string.Empty ? "Pendiente" : "Emitido",
                                    flafEstadoRecibo = oIDataReader[oIDataReader.GetOrdinal("UrlRecibo")].ToString() == string.Empty ? "none" : "block",
                                });
                            }
                        }

                    }
                }
            }

            return lista;
        }

        public List<ConfiguracionDTO> ListarSedes(ConfiguracionDTO oConfiguracion)
        {
            List<ConfiguracionDTO> lista = new List<ConfiguracionDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarSedes", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oConfiguracion.CodigoUnidadNegocio;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ConfiguracionDTO()
                                {
                                    Codigo = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Codigo")]),
                                    RazonSocial = oIDataReader[oIDataReader.GetOrdinal("RazonSocial")].ToString()
                                });
                            }
                        }

                    }
                }
            }

            return lista;
        }

        public List<ConfiguracionDTO> uspListarConfiguracion_apfitness_Paginacion(ConfiguracionDTO oConfiguracionDTO, Paging paging)
        {
            List<ConfiguracionDTO> lista = new List<ConfiguracionDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarConfiguracion_apfitness_Paginacion", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Busqueda", System.Data.SqlDbType.VarChar, 200)).Value = oConfiguracionDTO.filtro;
                    cmd.Parameters.Add(new SqlParameter("@Estado", System.Data.SqlDbType.Int)).Value = oConfiguracionDTO.Estado;

                    cmd.Parameters.Add(new SqlParameter("@PaginaActual", System.Data.SqlDbType.Int)).Value = paging.PageNumber;
                    cmd.Parameters.Add(new SqlParameter("@TamanioPagina", System.Data.SqlDbType.Int)).Value = paging.PageRecords;
                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ConfiguracionDTO()
                                {
                                    CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    Codigo = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Codigo")]),
                                    RazonSocial = oIDataReader[oIDataReader.GetOrdinal("RazonSocial")].ToString(),
                                    SubDominio = oIDataReader[oIDataReader.GetOrdinal("SubDominio")].ToString(),
                                    DiaPago = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("DiaPago")]),
                                    MontoMensualidad = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("MontoMensualidad")]),
                                    CantidadActivosSocios = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadActivosSocios")]),
                                    CantTotal = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadTodosSocios")]),
                                    Logo = oIDataReader[oIDataReader.GetOrdinal("Logo")].ToString(),
                                    EntidadBancaria = oIDataReader[oIDataReader.GetOrdinal("EntidadBancaria")].ToString(),
                                    NroCuenta = oIDataReader[oIDataReader.GetOrdinal("NroCuenta")].ToString(),
                                    ResponsableCuenta = oIDataReader[oIDataReader.GetOrdinal("ResponsableCuenta")].ToString(),
                                    DesEstado = oIDataReader[oIDataReader.GetOrdinal("EstadoPago")].ToString(),
                                    FechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")]),
                                    FechaVencimiento = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaVencimiento")]),
                                    Pais = oIDataReader[oIDataReader.GetOrdinal("Pais")].ToString(),
                                    TipoMoneda = oIDataReader[oIDataReader.GetOrdinal("TipoMoneda")].ToString(),
                                    Correo = oIDataReader[oIDataReader.GetOrdinal("Correo")].ToString(),
                                    NombreGerente = oIDataReader[oIDataReader.GetOrdinal("NombreGerente")].ToString(),
                                    ContactoCobranza = oIDataReader[oIDataReader.GetOrdinal("ContactoCobranza")].ToString(),
                                    CelularCobranza = oIDataReader[oIDataReader.GetOrdinal("CelularCobranza")].ToString()
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }

        public ConfiguracionDTO uspListarConfiguracion_apfitness_NumeroRegistros(ConfiguracionDTO oConfiguracionDTO)
        {
            ConfiguracionDTO itemDTO = new ConfiguracionDTO();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarConfiguracion_apfitness_NumeroRegistros", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Busqueda", System.Data.SqlDbType.VarChar, 100)).Value = oConfiguracionDTO.filtro;
                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new ConfiguracionDTO()
                                {
                                    CantTotal = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadRegistros")]),
                                    CantidadEmpresasActivas = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadActivos")]),
                                    CantidadEmpresasPrueba = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadPrueba")]),
                                    CantidadEmpresasInactivas = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadInactivos")]),
                                    CantidadEmpresasRetirados = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadRetirados")]),
                                    CantidadEmpresasProspectos = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadProspectos")])
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;
        }

        public List<ConfiguracionDTO> uspListarConfiguracion_Cobranzas_Paginacion(ConfiguracionDTO oConfiguracionDTO, Paging paging)
        {
            List<ConfiguracionDTO> lista = new List<ConfiguracionDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarConfiguracion_Cobranzas_Paginacion", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Busqueda", System.Data.SqlDbType.VarChar, 200)).Value = oConfiguracionDTO.filtro;
                    cmd.Parameters.Add(new SqlParameter("@Anio", System.Data.SqlDbType.Int)).Value = oConfiguracionDTO.Anio;
                    cmd.Parameters.Add(new SqlParameter("@Mes", System.Data.SqlDbType.Int)).Value = oConfiguracionDTO.MesEntero;
                    cmd.Parameters.Add(new SqlParameter("@Tipo", System.Data.SqlDbType.Int)).Value = oConfiguracionDTO.Tipo;

                    cmd.Parameters.Add(new SqlParameter("@PaginaActual", System.Data.SqlDbType.Int)).Value = paging.PageNumber;
                    cmd.Parameters.Add(new SqlParameter("@TamanioPagina", System.Data.SqlDbType.Int)).Value = paging.PageRecords;
                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ConfiguracionDTO()
                                {
                                    CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    Codigo = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Codigo")]),
                                    RazonSocial = oIDataReader[oIDataReader.GetOrdinal("RazonSocial")].ToString(),

                                    DiaPago = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("DiaPago")]),
                                    MontoMensualidad = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("MontoMensualidad")]),

                                    Logo = oIDataReader[oIDataReader.GetOrdinal("Logo")].ToString(),
                                    EntidadBancaria = oIDataReader[oIDataReader.GetOrdinal("EntidadBancaria")].ToString(),
                                    NroCuenta = oIDataReader[oIDataReader.GetOrdinal("NroCuenta")].ToString(),
                                    ResponsableCuenta = oIDataReader[oIDataReader.GetOrdinal("ResponsableCuenta")].ToString(),
                                    DesEstado = oIDataReader[oIDataReader.GetOrdinal("EstadoPago")].ToString(),
                                    MontoAcuenta = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("MontoAcuenta")]),
                                    NroOperacion = oIDataReader[oIDataReader.GetOrdinal("NroOperacion")].ToString(),
                                    FechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")]),
                                    RUC = oIDataReader[oIDataReader.GetOrdinal("RUC")].ToString(),
                                    DesEstadoRecibo = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("EstadoRecibo")]) == 0 ? "Pendiente" : "Listo",
                                    flafEstadoRecibo = oConfiguracionDTO.Tipo == 2 ? "block" : "none",
                                    UrlRecibo = oIDataReader[oIDataReader.GetOrdinal("UrlRecibo")].ToString(),
                                    CodigoPago = oIDataReader[oIDataReader.GetOrdinal("CodigoPago")] == null ? string.Empty : oIDataReader[oIDataReader.GetOrdinal("CodigoPago")].ToString(),
                                    RUCSunat = oIDataReader[oIDataReader.GetOrdinal("RUCSunat")].ToString(),
                                    USUARIO = oIDataReader[oIDataReader.GetOrdinal("USUARIOSunat")].ToString(),
                                    CLAVE = oIDataReader[oIDataReader.GetOrdinal("CLAVESunat")].ToString(),
                                    Pais = oIDataReader[oIDataReader.GetOrdinal("Pais")].ToString(),
                                    TipoMoneda = oIDataReader[oIDataReader.GetOrdinal("TipoMoneda")].ToString(),
                                    CantidadActivosSocios = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadActivosSocios")]),
                                    CantTotal = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadTodosSocios")]),
                                    Correo = oIDataReader[oIDataReader.GetOrdinal("Correo")].ToString(),
                                    ContactoCobranza = oIDataReader[oIDataReader.GetOrdinal("ContactoCobranza")].ToString()
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }

        public ConfiguracionDTO uspListarConfiguracion_Cobranzas_NumeroRegistros(ConfiguracionDTO oConfiguracionDTO)
        {
            ConfiguracionDTO itemDTO = new ConfiguracionDTO();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarConfiguracion_Cobranzas_NumeroRegistros", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Busqueda", System.Data.SqlDbType.VarChar, 200)).Value = oConfiguracionDTO.filtro;
                    cmd.Parameters.Add(new SqlParameter("@Anio", System.Data.SqlDbType.Int)).Value = oConfiguracionDTO.Anio;
                    cmd.Parameters.Add(new SqlParameter("@Mes", System.Data.SqlDbType.Int)).Value = oConfiguracionDTO.MesEntero;
                    cmd.Parameters.Add(new SqlParameter("@Tipo", System.Data.SqlDbType.Int)).Value = oConfiguracionDTO.Tipo;

                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new ConfiguracionDTO()
                                {
                                    CantTotal = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Cantidad")]),
                                    SumaTotalDeuda = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("SumaTotalDeuda")]),
                                    SumaTotalAcuenta = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("SumaTotalAcuenta")]),
                                    TotalCobrar = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("TotalCobrar")]),
                                    SumaMatricula = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("SumaMatricula")]),
                                    SumaGasto = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("SumaGasto")]),
                                    Utilidad = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Utilidad")])
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;
        }

        public List<ConfiguracionDTO> uspListarConfiguracionMatriculas(ConfiguracionDTO oConfiguracionDTO)
        {
            List<ConfiguracionDTO> lista = new List<ConfiguracionDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarConfiguracionMatriculas", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Anio", System.Data.SqlDbType.Int)).Value = oConfiguracionDTO.Anio;
                    cmd.Parameters.Add(new SqlParameter("@Mes", System.Data.SqlDbType.Int)).Value = oConfiguracionDTO.MesEntero;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ConfiguracionDTO()
                                {
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    MontoMatricula = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("MontoMatricula")]),
                                    NroOperacion = oIDataReader[oIDataReader.GetOrdinal("NroOperacion")].ToString(),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString(),
                                    FechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")])
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }

        public List<ConfiguracionDTO> uspListarConfiguracionGastos(ConfiguracionDTO oConfiguracionDTO)
        {
            List<ConfiguracionDTO> lista = new List<ConfiguracionDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarConfiguracionGastos", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Anio", System.Data.SqlDbType.Int)).Value = oConfiguracionDTO.Anio;
                    cmd.Parameters.Add(new SqlParameter("@Mes", System.Data.SqlDbType.Int)).Value = oConfiguracionDTO.MesEntero;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ConfiguracionDTO()
                                {
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    MontoGasto = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("MontoGasto")]),
                                    NroOperacion = oIDataReader[oIDataReader.GetOrdinal("NroOperacion")].ToString(),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString(),
                                    FechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")])
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }

        public List<ConfiguracionDTO> uspListaBusquedaClienteContratoAdFitness(ConfiguracionDTO oConfiguracionDTO)
        {
            if (oConfiguracionDTO == null)
            {
                oConfiguracionDTO = new ConfiguracionDTO();
                oConfiguracionDTO.filtro = string.Empty;
            }

            List<ConfiguracionDTO> lista = new List<ConfiguracionDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListaBusquedaClienteContratoAdFitness", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@filtro", System.Data.SqlDbType.VarChar, 100)).Value = oConfiguracionDTO.filtro;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ConfiguracionDTO()
                                {
                                    CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    Codigo = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Codigo")]),
                                    RazonSocial = oIDataReader[oIDataReader.GetOrdinal("RazonSocial")].ToString()
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }

        public ConfiguracionDTO uspSeguridadObtenerUnidadNegocio(ConfiguracionDTO oConfiguracion)
        {
            ConfiguracionDTO itemDTO = new ConfiguracionDTO();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspSeguridadObtenerUnidadNegocio2", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oConfiguracion.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oConfiguracion.CodigoSede;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new ConfiguracionDTO()
                                {
                                    CodigoUnidadNegocio = oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")] == null ? 0 : Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    Logo = oIDataReader[oIDataReader.GetOrdinal("Logo")].ToString(),
                                    ReservasNormativa = oIDataReader[oIDataReader.GetOrdinal("ReservasNormativa")].ToString(),
                                    ReservasNotas = oIDataReader[oIDataReader.GetOrdinal("ReservasNotas")].ToString(),
                                    ObligatorioMarcarIngresoSalaClase = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("ObligatorioMarcarIngresoSalaClase")])
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;
        }

        public ConfiguracionDTO uspSeguridadObtenerUnidadNegocio_SubDominio(ConfiguracionDTO oConfiguracion)
        {
            ConfiguracionDTO itemDTO = new ConfiguracionDTO();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspSeguridadObtenerUnidadNegocio", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@SubDominio", System.Data.SqlDbType.VarChar, 100)).Value = oConfiguracion.SubDominio;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new ConfiguracionDTO()
                                {
                                    CodigoUnidadNegocio = oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")] == null ? 0 : Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    Logo = oIDataReader[oIDataReader.GetOrdinal("Logo")].ToString()
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;
        }

        public ConfiguracionDTO CentroEntrenamiento_uspBuscarEmpresa_imprimirticket(ConfiguracionDTO oConfiguracion)
        {
            ConfiguracionDTO itemDTO = new ConfiguracionDTO();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("CentroEntrenamiento_uspBuscarEmpresa_imprimirticket", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oConfiguracion.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oConfiguracion.CodigoSede;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new ConfiguracionDTO()
                                {
                                    RazonSocial = oIDataReader[oIDataReader.GetOrdinal("NombreEmpresa")].ToString(),
                                    Direccion = oIDataReader[oIDataReader.GetOrdinal("DireccionEmpresa")].ToString(),
                                    Distrito = oIDataReader[oIDataReader.GetOrdinal("DistritoEmpresa")].ToString(),
                                    Ruc = oIDataReader[oIDataReader.GetOrdinal("RucEmpresa")].ToString(),
                                    Correo = oIDataReader[oIDataReader.GetOrdinal("CorreoEmpresa")].ToString(),
                                    Ticket_Celular = oIDataReader[oIDataReader.GetOrdinal("CelularEmpresa")].ToString(),
                                    Telefono = oIDataReader[oIDataReader.GetOrdinal("TelefonoEmpresa")].ToString(),
                                    Frase = oIDataReader[oIDataReader.GetOrdinal("Frase")].ToString(),
                                    Logo = oIDataReader[oIDataReader.GetOrdinal("Logo")].ToString()
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;
        }



        public ConfiguracionDTO BuscarPorCodigoConfiguracion(ConfiguracionDTO oConfiguracion)
        {
            ConfiguracionDTO itemDTO = new ConfiguracionDTO();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspBuscarConfiguracionPorCodigo", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oConfiguracion.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@Codigo", System.Data.SqlDbType.Int)).Value = oConfiguracion.Codigo;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new ConfiguracionDTO()
                                {
                                    Codigo = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Codigo")]),
                                    Igv = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Igv")]),
                                    TipoDescuento = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("TipoDescuento")]),
                                    GenerarSerie = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("GenerarSerie")]),
                                    GenerarComprobante = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("GenerarComprobante")]),
                                    Int_GenerarComprobante = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("GenerarComprobante")]) == true ? 1 : 0,
                                    ConexionDB = oIDataReader[oIDataReader.GetOrdinal("ConexionDB")].ToString(),
                                    RutaCarpetaImagen = oIDataReader[oIDataReader.GetOrdinal("RutaCarpetaImagen")].ToString(),
                                    LongitudSerie = oIDataReader[oIDataReader.GetOrdinal("LongitudSerie")].ToString(),
                                    NombreTiquetera = oIDataReader[oIDataReader.GetOrdinal("NombreTiquetera")].ToString(),
                                    RazonSocial = oIDataReader[oIDataReader.GetOrdinal("RazonSocial")].ToString(),
                                    Direccion = oIDataReader[oIDataReader.GetOrdinal("Direccion")].ToString(),
                                    Distrito = oIDataReader[oIDataReader.GetOrdinal("Distrito")].ToString(),
                                    Correo = oIDataReader[oIDataReader.GetOrdinal("Correo")].ToString(),
                                    Contrasenia = oIDataReader[oIDataReader.GetOrdinal("Contrasenia")].ToString(),
                                    PermitirMuchasAsistenciaPordia = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("PermitirMuchasAsistenciaPordia")]),
                                    DescontarFreezingDisponiblesFlag = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("DescontarFreezingDisponiblesFlag")]),
                                    DescontarFreezingDisponiblesNumero = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("DescontarFreezingDisponiblesNumero")]),
                                    Ruc = oIDataReader[oIDataReader.GetOrdinal("Ruc")].ToString(),
                                    Telefono = oIDataReader[oIDataReader.GetOrdinal("Telefono")].ToString(),
                                    Frase = oIDataReader[oIDataReader.GetOrdinal("Frase")].ToString(),
                                    NotificarDeudasXDia = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("NotificarDeudasXDia")]),
                                    CantDiasDeuda = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantDiasDeudas")]),
                                    Tipo_Configuracion = oIDataReader[oIDataReader.GetOrdinal("Tipo_Comprobante")] == null ? 0 : Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Tipo_Comprobante")]),
                                    Ticket_RazonSocial = oIDataReader[oIDataReader.GetOrdinal("Ticket_RazonSocial")].ToString(),
                                    Ticket_RUC = oIDataReader[oIDataReader.GetOrdinal("Ticket_RUC")].ToString(),
                                    Ticket_Direccion = oIDataReader[oIDataReader.GetOrdinal("Ticket_Direccion")].ToString(),
                                    Ticket_Celular = oIDataReader[oIDataReader.GetOrdinal("Ticket_Celular")].ToString(),
                                    Ticket_Telefono = oIDataReader[oIDataReader.GetOrdinal("Ticket_Telefono")].ToString(),
                                    Logo = oIDataReader[oIDataReader.GetOrdinal("Logo")].ToString(),
                                    TieneFacturacionElectronica = oIDataReader.GetBoolean(oIDataReader.GetOrdinal("TieneFacturacionElectronica")),
                                    UrlAPISunafact = oIDataReader[oIDataReader.GetOrdinal("UrlAPISunafact")].ToString(),
                                    TokenSunafact = oIDataReader[oIDataReader.GetOrdinal("TokenSunafact")].ToString(),
                                    ObligatorioDNIProspectos = oIDataReader.GetBoolean(oIDataReader.GetOrdinal("ObligatorioDNIProspectos")),
                                    ObligatorioMarcarIngresoSalaClase = oIDataReader.GetBoolean(oIDataReader.GetOrdinal("ObligatorioMarcarIngresoSalaClase")),
                                    CodigoClienteAuto = oIDataReader.GetBoolean(oIDataReader.GetOrdinal("CodigoClienteAuto")),

                                    ConsultasNumeroDocumento_ApiUrl = oIDataReader[oIDataReader.GetOrdinal("ConsultasNumeroDocumento_ApiUrl")].ToString(),
                                    ConsultasNumeroDocumento_ApiToken = oIDataReader[oIDataReader.GetOrdinal("ConsultasNumeroDocumento_ApiToken")].ToString(),
                                    ConsultasNumeroDocumento_FechaPago = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("ConsultasNumeroDocumento_FechaPago")]),
                                    ConsultasNumeroDocumento_FechaPago_Texto = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("ConsultasNumeroDocumento_FechaPago")]).ToString("dd MMMM"),
                                    ConsultasNumeroDocumento_PrecioAnual = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("ConsultasNumeroDocumento_PrecioAnual")]),
                                    ConsultasNumeroDocumento_UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("ConsultasNumeroDocumento_UsuarioCreacion")].ToString(),
                                    ConsultasNumeroDocumento_FechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("ConsultasNumeroDocumento_FechaCreacion")]),
                                    ConsultasNumeroDocumento_FechaCreacion_Texto = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("ConsultasNumeroDocumento_FechaCreacion")]).ToString("dd/MM/yyyy H:mm tt"),
                                    ConsultasNumeroDocumento_Estado = oIDataReader.GetBoolean(oIDataReader.GetOrdinal("ConsultaDocumentoPersonas")),

                                    EmailHost = oIDataReader[oIDataReader.GetOrdinal("email_host")].ToString(),
                                    EmailPort = oIDataReader[oIDataReader.GetOrdinal("email_puerto")].ToString(),
                                    EmailUser = oIDataReader[oIDataReader.GetOrdinal("email_usuario")].ToString(),
                                    EmailKey = oIDataReader[oIDataReader.GetOrdinal("email_clave")].ToString(),


                                    NombreComercial= oIDataReader[oIDataReader.GetOrdinal("NombreComercial")].ToString(),



                                    Pais = oIDataReader[oIDataReader.GetOrdinal("Pais")].ToString(),

                                    ActivarGenerarContratoMembresias = oIDataReader.GetBoolean(oIDataReader.GetOrdinal("ActivarGenerarContratoMembresias"))
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;
        }

        public ConfiguracionDTO BuscarConfiguracionAsistencia(ConfiguracionDTO oConfiguracion)
        {
            ConfiguracionDTO itemDTO = new ConfiguracionDTO();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspBuscarConfiguracionAsistencia", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oConfiguracion.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oConfiguracion.CodigoSede;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new ConfiguracionDTO()
                                {
                                    Codigo = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Codigo")]),
                                    Igv = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Igv")]),
                                    TipoDescuento = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("TipoDescuento")]),
                                    GenerarSerie = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("GenerarSerie")]),
                                    GenerarComprobante = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("GenerarComprobante")]),
                                    Int_GenerarComprobante = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("GenerarComprobante")]) == true ? 1 : 0,
                                    ConexionDB = oIDataReader[oIDataReader.GetOrdinal("ConexionDB")].ToString(),
                                    RutaCarpetaImagen = oIDataReader[oIDataReader.GetOrdinal("RutaCarpetaImagen")].ToString(),
                                    LongitudSerie = oIDataReader[oIDataReader.GetOrdinal("LongitudSerie")].ToString(),
                                    NombreTiquetera = oIDataReader[oIDataReader.GetOrdinal("NombreTiquetera")].ToString(),
                                    RazonSocial = oIDataReader[oIDataReader.GetOrdinal("RazonSocial")].ToString(),
                                    Direccion = oIDataReader[oIDataReader.GetOrdinal("Direccion")].ToString(),
                                    Distrito = oIDataReader[oIDataReader.GetOrdinal("Distrito")].ToString(),
                                    Correo = oIDataReader[oIDataReader.GetOrdinal("Correo")].ToString(),
                                    Contrasenia = oIDataReader[oIDataReader.GetOrdinal("Contrasenia")].ToString(),
                                    PermitirMuchasAsistenciaPordia = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("PermitirMuchasAsistenciaPordia")]),
                                    DescontarFreezingDisponiblesFlag = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("DescontarFreezingDisponiblesFlag")]),
                                    DescontarFreezingDisponiblesNumero = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("DescontarFreezingDisponiblesNumero")]),
                                    Ruc = oIDataReader[oIDataReader.GetOrdinal("Ruc")].ToString(),
                                    Telefono = oIDataReader[oIDataReader.GetOrdinal("Telefono")].ToString(),
                                    Frase = oIDataReader[oIDataReader.GetOrdinal("Frase")].ToString(),
                                    NotificarDeudasXDia = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("NotificarDeudasXDia")]),
                                    CantDiasDeuda = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantDiasDeudas")]),
                                    Tipo_Configuracion = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Tipo_Comprobante")])
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;
        }

        public ConfiguracionDTO BuscarConfiguracionTiempoMarcarAsistencia(ConfiguracionDTO oConfiguracion)
        {
            ConfiguracionDTO itemDTO = new ConfiguracionDTO();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspBuscarConfiguracionTiempoMarcarAsistencia", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oConfiguracion.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oConfiguracion.CodigoSede;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new ConfiguracionDTO()
                                {
                                    Codigo = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Codigo")]),
                                    TiempoMarcarAsistencia = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("TiempoMarcarAsistencia")])
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;
        }

        public ConfiguracionDTO BuscarConfiguracionDiasCitasCaida(ConfiguracionDTO oConfiguracion)
        {
            ConfiguracionDTO itemDTO = new ConfiguracionDTO();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspBuscarConfiguracionDiasCitasCaida", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oConfiguracion.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oConfiguracion.CodigoSede;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new ConfiguracionDTO()
                                {
                                    Codigo = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Codigo")]),
                                    DiaCitasCaida = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("DiasCitasCaida")])
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;
        }

        public ConfiguracionDTO BuscarConfVentaOtrosPorCodigo(ConfiguracionDTO oConfiguracion)
        {
            ConfiguracionDTO itemDTO = new ConfiguracionDTO();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspBuscarConfVentaOtrosPorCodigo", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oConfiguracion.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oConfiguracion.CodigoSede;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new ConfiguracionDTO()
                                {
                                    EstadoMostrarVentaOtros = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("MostrarVentaOtros")])
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;
        }

        public ConfiguracionDTO BuscarConfCorreoBienvenidaPorCodigo(ConfiguracionDTO oConfiguracion)
        {
            ConfiguracionDTO itemDTO = new ConfiguracionDTO();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspBuscarConfCorreoBienvenidaPorCodigo", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oConfiguracion.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oConfiguracion.CodigoSede;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new ConfiguracionDTO()
                                {
                                    EstadoActivarCorreoBienvenida = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("ActivarCorreoBienvenida")])
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;
        }

        public ConfiguracionDTO buscarConfiguracionImprimirContrato(ConfiguracionDTO oConfiguracion)
        {
            ConfiguracionDTO itemDTO = new ConfiguracionDTO();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspBuscarConfiguracionImprimirContrato", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oConfiguracion.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oConfiguracion.CodigoSede;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new ConfiguracionDTO()
                                {
                                    EstadoImprimirContrato = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("ActivarImprimirContrato")])
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;
        }

        public decimal Get_Igv()
        {
            decimal igv = 0;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspGet_Igv", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                igv = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Igv")]);
                            }
                        }
                    }
                }
            }

            return igv;
        }

        public int Get_TipoDescuento(int CodigoUnidadNegocio)
        {
            int tipoDescuento = 0;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspBuscarConfiguracionPorCodigo", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@Codigo", System.Data.SqlDbType.Int)).Value = 1;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                tipoDescuento = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("TipoDescuento")]);
                            }
                        }
                    }
                }
            }

            return tipoDescuento;
        }

        public void Registrar(ConfiguracionDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspRegistrarConfiguracion", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Codigo", System.Data.SqlDbType.Int)).Value = item.Codigo;
                    cmd.Parameters.Add(new SqlParameter("@Igv", System.Data.SqlDbType.Decimal)).Value = item.Igv;
                    cmd.Parameters.Add(new SqlParameter("@TipoDescuento", System.Data.SqlDbType.Int)).Value = item.TipoDescuento;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;

                    cmd.ExecuteNonQuery();
                }
            }

        }

        public void uspRegistrarConfiguracion_adFitness(ConfiguracionDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspRegistrarConfiguracion_adFitness", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@RazonSocial", System.Data.SqlDbType.VarChar, 100)).Value = item.RazonSocial;
                    cmd.Parameters.Add(new SqlParameter("@NombreComercial", System.Data.SqlDbType.VarChar, 100)).Value = item.NombreComercial;
                    cmd.Parameters.Add(new SqlParameter("@Pais", System.Data.SqlDbType.VarChar, 100)).Value = item.Pais;
                    cmd.Parameters.Add(new SqlParameter("@Departamento", System.Data.SqlDbType.VarChar, 100)).Value = item.Departamento;

                    cmd.Parameters.Add(new SqlParameter("@Distrito", System.Data.SqlDbType.VarChar, 100)).Value = item.Distrito;
                    cmd.Parameters.Add(new SqlParameter("@Direccion", System.Data.SqlDbType.VarChar, 100)).Value = item.Direccion;
                    cmd.Parameters.Add(new SqlParameter("@Igv", System.Data.SqlDbType.Decimal)).Value = item.Igv;
                    cmd.Parameters.Add(new SqlParameter("@TipoDescuento", System.Data.SqlDbType.Int)).Value = item.TipoDescuento;
                    cmd.Parameters.Add(new SqlParameter("@GenerarSerie", System.Data.SqlDbType.Int)).Value = item.GenerarSerie;

                    cmd.Parameters.Add(new SqlParameter("@GenerarComprobante", System.Data.SqlDbType.Bit)).Value = item.GenerarComprobante;
                    cmd.Parameters.Add(new SqlParameter("@ConexionDB", System.Data.SqlDbType.VarChar, 100)).Value = item.ConexionDB;
                    cmd.Parameters.Add(new SqlParameter("@RutaCarpetaImagen", System.Data.SqlDbType.VarChar, 100)).Value = item.RutaCarpetaImagen;
                    cmd.Parameters.Add(new SqlParameter("@LongitudSerie", System.Data.SqlDbType.VarChar, 100)).Value = item.LongitudSerie;
                    cmd.Parameters.Add(new SqlParameter("@NombreTiquetera", System.Data.SqlDbType.VarChar, 100)).Value = item.NombreTiquetera;

                    cmd.Parameters.Add(new SqlParameter("@Correo", System.Data.SqlDbType.VarChar, 100)).Value = item.Correo;
                    cmd.Parameters.Add(new SqlParameter("@Contrasenia", System.Data.SqlDbType.VarChar, 100)).Value = item.Contrasenia;
                    cmd.Parameters.Add(new SqlParameter("@Estado", System.Data.SqlDbType.Int)).Value = item.Estado;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioEdicion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioEdicion;

                    cmd.Parameters.Add(new SqlParameter("@FechaVencimiento", System.Data.SqlDbType.DateTime)).Value = item.FechaVencimiento;
                    cmd.Parameters.Add(new SqlParameter("@Tipo", System.Data.SqlDbType.Int)).Value = item.Tipo;
                    cmd.Parameters.Add(new SqlParameter("@PermitirMuchasAsistenciaPordia", System.Data.SqlDbType.Int)).Value = item.PermitirMuchasAsistenciaPordia;
                    cmd.Parameters.Add(new SqlParameter("@DescontarFreezingDisponiblesFlag", System.Data.SqlDbType.Int)).Value = item.DescontarFreezingDisponiblesFlag;
                    cmd.Parameters.Add(new SqlParameter("@DescontarFreezingDisponiblesNumero", System.Data.SqlDbType.Int)).Value = item.DescontarFreezingDisponiblesNumero;

                    cmd.Parameters.Add(new SqlParameter("@RUC", System.Data.SqlDbType.VarChar, 100)).Value = item.Ruc;
                    cmd.Parameters.Add(new SqlParameter("@Telefono", System.Data.SqlDbType.VarChar, 100)).Value = item.Telefono;
                    cmd.Parameters.Add(new SqlParameter("@Frase", System.Data.SqlDbType.VarChar, 200)).Value = item.Frase;
                    cmd.Parameters.Add(new SqlParameter("@ClienteXVendedorAleatorio", System.Data.SqlDbType.Int)).Value = item.ClientesxVendedorAleatorio;
                    cmd.Parameters.Add(new SqlParameter("@NumeroDiaMesEjecucionAleatorio", System.Data.SqlDbType.Int)).Value = item.NumeroDiaMesEjecucionAleatorio;

                    cmd.Parameters.Add(new SqlParameter("@NotificarDeudasXDia", System.Data.SqlDbType.Bit)).Value = item.NotificarDeudasXDia;
                    cmd.Parameters.Add(new SqlParameter("@CantDiasDeuda", System.Data.SqlDbType.Int)).Value = item.CantDiasDeuda;
                    cmd.Parameters.Add(new SqlParameter("@MostrarVentaOtros", System.Data.SqlDbType.Int)).Value = item.EstadoMostrarVentaOtros;
                    cmd.Parameters.Add(new SqlParameter("@ActivarCorreoBienvenida", System.Data.SqlDbType.Int)).Value = item.EstadoActivarCorreoBienvenida;
                    cmd.Parameters.Add(new SqlParameter("@ActivarImprimirContrato", System.Data.SqlDbType.Int)).Value = item.EstadoImprimirContrato;


                    cmd.Parameters.Add(new SqlParameter("@TiempoMarcarAsistencia", System.Data.SqlDbType.Bit)).Value = item.TiempoMarcarAsistencia;
                    cmd.Parameters.Add(new SqlParameter("@DiaCitasCaida", System.Data.SqlDbType.Int)).Value = item.DiaCitasCaida;
                    cmd.Parameters.Add(new SqlParameter("@SubDominio", System.Data.SqlDbType.VarChar, 100)).Value = item.SubDominio;
                    cmd.Parameters.Add(new SqlParameter("@FechaPago", System.Data.SqlDbType.DateTime)).Value = item.FechaPago;
                    cmd.Parameters.Add(new SqlParameter("@Ubigeo", System.Data.SqlDbType.VarChar, 50)).Value = item.Ubicaciones;
                    cmd.Parameters.Add(new SqlParameter("@MontoMensualidad", System.Data.SqlDbType.Decimal)).Value = item.MontoMensualidad;

                    cmd.Parameters.Add(new SqlParameter("@CodigoCuenta", System.Data.SqlDbType.VarChar, 100)).Value = item.CodigoCuenta;

                    cmd.Parameters.Add(new SqlParameter("@NombreGerente", System.Data.SqlDbType.VarChar, 100)).Value = item.NombreGerente;
                    cmd.Parameters.Add(new SqlParameter("@ContactoCobranza", System.Data.SqlDbType.VarChar, 100)).Value = item.ContactoCobranza;
                    cmd.Parameters.Add(new SqlParameter("@CelularCobranza", System.Data.SqlDbType.VarChar, 100)).Value = item.CelularCobranza;

                    cmd.ExecuteNonQuery();
                }
            }

        }

        public void uspRegistrarConfiguracionPagosMensualidades(ConfiguracionDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspRegistrarConfiguracionPagosMensualidades", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@FechaPago", System.Data.SqlDbType.DateTime)).Value = item.FechaPago;

                    cmd.Parameters.Add(new SqlParameter("@MontoMes", System.Data.SqlDbType.Decimal)).Value = item.MontoMes;
                    cmd.Parameters.Add(new SqlParameter("@MontoAcuenta", System.Data.SqlDbType.Decimal)).Value = item.MontoAcuenta;
                    cmd.Parameters.Add(new SqlParameter("@NroOperacion", System.Data.SqlDbType.VarChar, 100)).Value = item.NroOperacion;
                    cmd.Parameters.Add(new SqlParameter("@CodigoCuenta", System.Data.SqlDbType.VarChar, 100)).Value = item.CodigoNroCuenta;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;

                    cmd.ExecuteNonQuery();
                }
            }

        }

        public void uspActualizarConfiguracionPagosMensualidadesRecibos(ConfiguracionDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspActualizarConfiguracionPagosMensualidadesRecibos", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoPago", System.Data.SqlDbType.VarChar)).Value = item.CodigoPago;
                    cmd.Parameters.Add(new SqlParameter("@UrlRecibo", System.Data.SqlDbType.VarChar)).Value = item.UrlRecibo;

                    cmd.ExecuteNonQuery();
                }
            }

        }

        public void uspActualizarConfiguracionDatosFormatoTicket(ConfiguracionDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspActualizarConfiguracionDatosFormatoTicket", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@RazonSocial_Ticket", System.Data.SqlDbType.VarChar, 100)).Value = item.Ticket_RazonSocial ?? string.Empty;
                    cmd.Parameters.Add(new SqlParameter("@RUC_Ticket", System.Data.SqlDbType.VarChar, 100)).Value = item.Ticket_RUC ?? string.Empty;
                    cmd.Parameters.Add(new SqlParameter("@Direccion_Ticket", System.Data.SqlDbType.VarChar, 100)).Value = item.Ticket_Direccion ?? string.Empty;
                    cmd.Parameters.Add(new SqlParameter("@Celular_Ticket", System.Data.SqlDbType.VarChar, 100)).Value = item.Ticket_Celular ?? string.Empty;
                    cmd.Parameters.Add(new SqlParameter("@Telefono_Ticket", System.Data.SqlDbType.VarChar, 100)).Value = item.Ticket_Telefono ?? string.Empty;
                    cmd.Parameters.Add(new SqlParameter("@Frase_Ticket", System.Data.SqlDbType.VarChar, 100)).Value = item.Frase ?? string.Empty;

                    cmd.ExecuteNonQuery();
                }
            }

        }

        public void uspRegistrarConfiguracionMatriculas(ConfiguracionDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspRegistrarConfiguracionMatriculas", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Descripcion", System.Data.SqlDbType.VarChar, 100)).Value = item.Descripcion;
                    cmd.Parameters.Add(new SqlParameter("@MontoMatricula", System.Data.SqlDbType.Decimal)).Value = item.MontoMatricula;
                    cmd.Parameters.Add(new SqlParameter("@NroOperacion", System.Data.SqlDbType.VarChar, 100)).Value = item.NroOperacion;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;

                    cmd.ExecuteNonQuery();
                }
            }

        }

        public void uspRegistrarConfiguracionGastos(ConfiguracionDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspRegistrarConfiguracionGastos", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Descripcion", System.Data.SqlDbType.VarChar, 100)).Value = item.Descripcion;
                    cmd.Parameters.Add(new SqlParameter("@MontoGasto", System.Data.SqlDbType.Decimal)).Value = item.MontoGasto;
                    cmd.Parameters.Add(new SqlParameter("@NroOperacion", System.Data.SqlDbType.VarChar, 100)).Value = item.NroOperacion;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;

                    cmd.ExecuteNonQuery();
                }
            }

        }

        public void uspActualizarConfiguracion_adFitness(ConfiguracionDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspActualizarConfiguracion_adFitness", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@RazonSocial", System.Data.SqlDbType.VarChar, 100)).Value = item.RazonSocial;
                    cmd.Parameters.Add(new SqlParameter("@Pais", System.Data.SqlDbType.VarChar, 100)).Value = item.Pais;
                    cmd.Parameters.Add(new SqlParameter("@Departamento", System.Data.SqlDbType.VarChar, 100)).Value = item.Departamento;

                    cmd.Parameters.Add(new SqlParameter("@Distrito", System.Data.SqlDbType.VarChar, 100)).Value = item.Distrito;
                    cmd.Parameters.Add(new SqlParameter("@Direccion", System.Data.SqlDbType.VarChar, 100)).Value = item.Direccion;
                    cmd.Parameters.Add(new SqlParameter("@Igv", System.Data.SqlDbType.Decimal)).Value = item.Igv;
                    cmd.Parameters.Add(new SqlParameter("@TipoDescuento", System.Data.SqlDbType.Int)).Value = item.TipoDescuento;
                    cmd.Parameters.Add(new SqlParameter("@GenerarSerie", System.Data.SqlDbType.Int)).Value = item.GenerarSerie;

                    cmd.Parameters.Add(new SqlParameter("@GenerarComprobante", System.Data.SqlDbType.Bit)).Value = item.GenerarComprobante;
                    cmd.Parameters.Add(new SqlParameter("@ConexionDB", System.Data.SqlDbType.VarChar, 100)).Value = item.ConexionDB;
                    cmd.Parameters.Add(new SqlParameter("@RutaCarpetaImagen", System.Data.SqlDbType.VarChar, 100)).Value = item.RutaCarpetaImagen;
                    cmd.Parameters.Add(new SqlParameter("@LongitudSerie", System.Data.SqlDbType.VarChar, 100)).Value = item.LongitudSerie;
                    cmd.Parameters.Add(new SqlParameter("@NombreTiquetera", System.Data.SqlDbType.VarChar, 100)).Value = item.NombreTiquetera;

                    cmd.Parameters.Add(new SqlParameter("@Correo", System.Data.SqlDbType.VarChar, 100)).Value = item.Correo;
                    cmd.Parameters.Add(new SqlParameter("@Contrasenia", System.Data.SqlDbType.VarChar, 100)).Value = item.Contrasenia;
                    cmd.Parameters.Add(new SqlParameter("@Estado", System.Data.SqlDbType.Int)).Value = item.Estado;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioEdicion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioEdicion;

                    cmd.Parameters.Add(new SqlParameter("@FechaVencimiento", System.Data.SqlDbType.DateTime)).Value = item.FechaVencimiento;
                    cmd.Parameters.Add(new SqlParameter("@Tipo", System.Data.SqlDbType.Int)).Value = item.Tipo;
                    cmd.Parameters.Add(new SqlParameter("@PermitirMuchasAsistenciaPordia", System.Data.SqlDbType.Int)).Value = item.PermitirMuchasAsistenciaPordia;
                    cmd.Parameters.Add(new SqlParameter("@DescontarFreezingDisponiblesFlag", System.Data.SqlDbType.Int)).Value = item.DescontarFreezingDisponiblesFlag;
                    cmd.Parameters.Add(new SqlParameter("@DescontarFreezingDisponiblesNumero", System.Data.SqlDbType.Int)).Value = item.DescontarFreezingDisponiblesNumero;

                    cmd.Parameters.Add(new SqlParameter("@RUC", System.Data.SqlDbType.VarChar, 100)).Value = item.Ruc;
                    cmd.Parameters.Add(new SqlParameter("@Telefono", System.Data.SqlDbType.VarChar, 100)).Value = item.Telefono;
                    cmd.Parameters.Add(new SqlParameter("@NombreComercial", System.Data.SqlDbType.VarChar, 200)).Value = item.NombreComercial;
                    cmd.Parameters.Add(new SqlParameter("@Frase", System.Data.SqlDbType.VarChar, 200)).Value = item.Frase;
                    cmd.Parameters.Add(new SqlParameter("@ClienteXVendedorAleatorio", System.Data.SqlDbType.Int)).Value = item.ClientesxVendedorAleatorio;
                    cmd.Parameters.Add(new SqlParameter("@NumeroDiaMesEjecucionAleatorio", System.Data.SqlDbType.Int)).Value = item.NumeroDiaMesEjecucionAleatorio;

                    cmd.Parameters.Add(new SqlParameter("@NotificarDeudasXDia", System.Data.SqlDbType.Bit)).Value = item.NotificarDeudasXDia;
                    cmd.Parameters.Add(new SqlParameter("@CantDiasDeuda", System.Data.SqlDbType.Int)).Value = item.CantDiasDeuda;
                    cmd.Parameters.Add(new SqlParameter("@MostrarVentaOtros", System.Data.SqlDbType.Int)).Value = item.EstadoMostrarVentaOtros;
                    cmd.Parameters.Add(new SqlParameter("@ActivarCorreoBienvenida", System.Data.SqlDbType.Int)).Value = item.EstadoActivarCorreoBienvenida;
                    cmd.Parameters.Add(new SqlParameter("@ActivarImprimirContrato", System.Data.SqlDbType.Int)).Value = item.EstadoImprimirContrato;

                    cmd.Parameters.Add(new SqlParameter("@TiempoMarcarAsistencia", System.Data.SqlDbType.Bit)).Value = item.TiempoMarcarAsistencia;
                    cmd.Parameters.Add(new SqlParameter("@DiaCitasCaida", System.Data.SqlDbType.Int)).Value = item.DiaCitasCaida;
                    cmd.Parameters.Add(new SqlParameter("@SubDominio", System.Data.SqlDbType.VarChar, 100)).Value = item.SubDominio;
                    cmd.Parameters.Add(new SqlParameter("@FechaPago", System.Data.SqlDbType.DateTime)).Value = item.FechaPago;
                    cmd.Parameters.Add(new SqlParameter("@Ubigeo", System.Data.SqlDbType.VarChar, 50)).Value = item.Ubicaciones;
                    cmd.Parameters.Add(new SqlParameter("@MontoMensualidad", System.Data.SqlDbType.Decimal)).Value = item.MontoMensualidad;

                    cmd.Parameters.Add(new SqlParameter("@CodigoCuenta", System.Data.SqlDbType.VarChar, 100)).Value = item.CodigoCuenta;

                    cmd.Parameters.Add(new SqlParameter("@CodigoPlan", System.Data.SqlDbType.Int)).Value = item.CodigoPlan;
                    cmd.Parameters.Add(new SqlParameter("@TipoMoneda", System.Data.SqlDbType.VarChar, 10)).Value = item.TipoMoneda;

                    cmd.Parameters.Add(new SqlParameter("@TieneFacturacionElectronica", System.Data.SqlDbType.Bit)).Value = item.TieneFacturacionElectronica;
                    cmd.Parameters.Add(new SqlParameter("@UrlAPISunafact", System.Data.SqlDbType.VarChar, 200)).Value = item.UrlAPISunafact ?? String.Empty;
                    cmd.Parameters.Add(new SqlParameter("@TokenSunafact", System.Data.SqlDbType.VarChar, 200)).Value = item.TokenSunafact ?? String.Empty;


                    cmd.Parameters.Add(new SqlParameter("@NombreGerente", System.Data.SqlDbType.VarChar, 100)).Value = item.NombreGerente;
                    cmd.Parameters.Add(new SqlParameter("@ContactoCobranza", System.Data.SqlDbType.VarChar, 100)).Value = item.ContactoCobranza;
                    cmd.Parameters.Add(new SqlParameter("@CelularCobranza", System.Data.SqlDbType.VarChar, 100)).Value = item.CelularCobranza;

                    cmd.Parameters.Add(new SqlParameter("@AplicacionDisponible", System.Data.SqlDbType.Bit)).Value = item.AplicacionDisponible;
                    cmd.Parameters.Add(new SqlParameter("@TiendaAplicacion", System.Data.SqlDbType.Bit)).Value = item.TiendaAplicacion;
                    cmd.Parameters.Add(new SqlParameter("@RutinasAplicacion", System.Data.SqlDbType.Bit)).Value = item.RutinasAplicacion;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void uspActualizarConfiguracionLogo_adFitness(ConfiguracionDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspActualizarConfiguracionLogo_adFitness", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Logo", System.Data.SqlDbType.VarChar, 200)).Value = item.Logo;
                    cmd.ExecuteNonQuery();
                }
            }
        }


        public ConfiguracionDTO Buscar_RepartirClientes(ConfiguracionDTO oConfiguracion)
        {
            ConfiguracionDTO itemDTO = new ConfiguracionDTO();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspBuscar_RepartirClientes", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oConfiguracion.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oConfiguracion.CodigoSede;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new ConfiguracionDTO()
                                {
                                    Codigo = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Codigo")]),
                                    DiaDividir = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("DiaDividir")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    EstadoDividir = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("EstadoDividir")]),
                                    FechaDividir = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaDividir")])
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;
        }

        public void ActualizarDia_RepartirClientes(ConfiguracionDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspActualizarDia_RepartirClientes", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@TipoDia", System.Data.SqlDbType.Int)).Value = item.TipoDia;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void ActualizarVentaOtros(ConfiguracionDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspActualizarVentaOtros", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@EstadoMostrarVentaOtros", System.Data.SqlDbType.Int)).Value = item.EstadoMostrarVentaOtros;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void ActualizarCorreoBienvenida(ConfiguracionDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspActualizarCorreoBienvenida", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@EstadoActivarCorreoBienvenida", System.Data.SqlDbType.Int)).Value = item.EstadoActivarCorreoBienvenida;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void uspActualizarImprimirContrato(ConfiguracionDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspActualizarImprimirContrato", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@ActivarImprimirContrato", System.Data.SqlDbType.Int)).Value = item.EstadoImprimirContrato;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Actualizar(ConfiguracionDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspActualizarConfiguracion", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Codigo", System.Data.SqlDbType.Int)).Value = item.Codigo;
                    cmd.Parameters.Add(new SqlParameter("@Igv", System.Data.SqlDbType.Decimal)).Value = item.Igv;
                    cmd.Parameters.Add(new SqlParameter("@TipoDescuento", System.Data.SqlDbType.Int)).Value = item.TipoDescuento;
                    cmd.Parameters.Add(new SqlParameter("@GenerarSerie", System.Data.SqlDbType.Bit)).Value = item.GenerarSerie;
                    cmd.Parameters.Add(new SqlParameter("@GenerarComprobante", System.Data.SqlDbType.Bit)).Value = item.GenerarComprobante;

                    cmd.Parameters.Add(new SqlParameter("@UsuarioEdicion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioEdicion;
                    cmd.Parameters.Add(new SqlParameter("@ConexionDB", System.Data.SqlDbType.VarChar, 100)).Value = item.ConexionDB;
                    cmd.Parameters.Add(new SqlParameter("@RutaCarpetaImagen", System.Data.SqlDbType.VarChar, 100)).Value = item.RutaCarpetaImagen;
                    cmd.Parameters.Add(new SqlParameter("@LongitudSerie", System.Data.SqlDbType.VarChar, 100)).Value = item.LongitudSerie;
                    cmd.Parameters.Add(new SqlParameter("@NombreTiquetera", System.Data.SqlDbType.VarChar, 100)).Value = item.NombreTiquetera;

                    cmd.Parameters.Add(new SqlParameter("@RazonSocial", System.Data.SqlDbType.VarChar, 100)).Value = item.RazonSocial;
                    cmd.Parameters.Add(new SqlParameter("@Direccion", System.Data.SqlDbType.VarChar, 100)).Value = item.Direccion;
                    cmd.Parameters.Add(new SqlParameter("@Distrito", System.Data.SqlDbType.VarChar, 100)).Value = item.Distrito;
                    cmd.Parameters.Add(new SqlParameter("@Correo", System.Data.SqlDbType.VarChar, 100)).Value = item.Correo;
                    cmd.Parameters.Add(new SqlParameter("@Contrasenia", System.Data.SqlDbType.VarChar, 100)).Value = item.Contrasenia;

                    cmd.Parameters.Add(new SqlParameter("@Ruc", System.Data.SqlDbType.VarChar, 100)).Value = item.Ruc;
                    cmd.Parameters.Add(new SqlParameter("@Telefono", System.Data.SqlDbType.VarChar, 100)).Value = item.Telefono;
                    cmd.Parameters.Add(new SqlParameter("@Frase", System.Data.SqlDbType.VarChar, 100)).Value = item.Frase;
                    cmd.Parameters.Add(new SqlParameter("@NotificarDeudasXDia", System.Data.SqlDbType.Bit)).Value = item.NotificarDeudasXDia;
                    cmd.Parameters.Add(new SqlParameter("@CantDiasDeuda", System.Data.SqlDbType.Int)).Value = item.CantDiasDeuda;

                    cmd.Parameters.Add(new SqlParameter("@Tipo_Configuracion", System.Data.SqlDbType.Bit)).Value = item.Tipo_Configuracion;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateConfiguracionCitasCaidas(ConfiguracionDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("UspActualizarConfiguracionCitasCaidas", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@DiaCitasCaida", System.Data.SqlDbType.Int)).Value = item.DiaCitasCaida;

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateObligatorioIngresoDNI(ConfiguracionDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("UspActualizarConfiguracionIngresoObligatorioDNI", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@ObligatorioDNIProspectos", System.Data.SqlDbType.Bit)).Value = item.ObligatorioDNIProspectos;

                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void UpdateObligarMarcarClaseAsistencia(ConfiguracionDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("UspActualizarConfiguracionObligarMarcarClaseAsistencia", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@ObligatorioMarcarIngresoSalaClase", System.Data.SqlDbType.Bit)).Value = item.ObligatorioMarcarIngresoSalaClase;

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdatePermitirMuchasAsistenciaPordia(ConfiguracionDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("UspActualizarConfiguracionPermitirMuchasAsistenciaPordia", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@PermitirMuchasAsistenciaPordia", System.Data.SqlDbType.Int)).Value = item.PermitirMuchasAsistenciaPordia;

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateGenerarCodigoclienteAutomatico(ConfiguracionDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("UspActualizarGenerarCodigoclienteAutomatico", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoClienteAuto", System.Data.SqlDbType.Bit)).Value = item.CodigoClienteAuto;

                    cmd.ExecuteNonQuery();
                }
            }
        }
        //ActivarImprimirContrato
        public void UpdateActivarImprimirContrato(ConfiguracionDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("UspActualizarGenerarContratoAutomatico", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@ActivarGenerarContratoMembresias", System.Data.SqlDbType.Bit)).Value = item.ActivarGenerarContratoMembresias;

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateConsultasNumeroDocumentoEntidades(ConfiguracionDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("UspActualizarConfiguracionConsultasNumeroDocumentoEntidades", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Correo", System.Data.SqlDbType.VarChar, 100)).Value = item.ConsultasNumeroDocumento_Correo;
                    cmd.Parameters.Add(new SqlParameter("@Clave", System.Data.SqlDbType.VarChar, 100)).Value = item.ConsultasNumeroDocumento_Clave;
                    cmd.Parameters.Add(new SqlParameter("@ApiUrl", System.Data.SqlDbType.VarChar, 200)).Value = item.ConsultasNumeroDocumento_ApiUrl;
                    cmd.Parameters.Add(new SqlParameter("@ApiToken", System.Data.SqlDbType.VarChar, 200)).Value = item.ConsultasNumeroDocumento_ApiToken;
                    cmd.Parameters.Add(new SqlParameter("@PrecioAnual", System.Data.SqlDbType.Decimal)).Value = item.ConsultasNumeroDocumento_PrecioAnual;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.ConsultasNumeroDocumento_UsuarioCreacion;
                    cmd.Parameters.Add(new SqlParameter("@Estado", System.Data.SqlDbType.Bit)).Value = item.ConsultasNumeroDocumento_Estado;

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void ActualizarConfiguracionAsistencia(ConfiguracionDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspActualizarConfiguracionAsistencia", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@Codigo", System.Data.SqlDbType.Int)).Value = item.Codigo;
                    cmd.Parameters.Add(new SqlParameter("@PermitirMuchasAsistenciaPordia", System.Data.SqlDbType.Int)).Value = item.PermitirMuchasAsistenciaPordia;

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateConfiguracionTiempoMarcarAsistencia(ConfiguracionDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspActualizarConfiguracionAsistencia", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@Codigo", System.Data.SqlDbType.Int)).Value = item.Codigo;
                    cmd.Parameters.Add(new SqlParameter("@TiempoMarcarAsistencia", System.Data.SqlDbType.Int)).Value = item.TiempoMarcarAsistencia;

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void ActualizarConfiguracionFreezing(ConfiguracionDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspActualizarConfiguracionFreezing", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@Codigo", System.Data.SqlDbType.Int)).Value = item.Codigo;
                    cmd.Parameters.Add(new SqlParameter("@DescontarFreezingDisponiblesFlag", System.Data.SqlDbType.Int)).Value = item.DescontarFreezingDisponiblesFlag;
                    cmd.Parameters.Add(new SqlParameter("@DescontarFreezingDisponiblesNumero", System.Data.SqlDbType.Int)).Value = item.DescontarFreezingDisponiblesNumero;

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void uspActualizarControlPagoSoftware(ConfiguracionDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspActualizarControlPagoSoftware", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Usuario", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;
                    cmd.Parameters.Add(new SqlParameter("@Contrasenia", System.Data.SqlDbType.VarChar, 50)).Value = item.Contrasenia;
                    cmd.Parameters.Add(new SqlParameter("@Tipo", System.Data.SqlDbType.Int)).Value = item.Tipo;
                    cmd.Parameters.Add(new SqlParameter("@FechaVencimiento", System.Data.SqlDbType.DateTime)).Value = item.FechaVencimiento;

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public ConfiguracionDTO uspBuscarConfiguracionControlPagoSoftware(ConfiguracionDTO oConfiguracion)
        {
            ConfiguracionDTO itemDTO = new ConfiguracionDTO();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspBuscarConfiguracionControlPagoSoftware", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oConfiguracion.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oConfiguracion.CodigoSede;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new ConfiguracionDTO()
                                {
                                    Tipo = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Tipo")]),
                                    FechaVencimiento = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaVencimiento")])
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;
        }

        public ConfiguracionDTO uspBuscarConfiguracion_apfitness(ConfiguracionDTO oConfiguracion)
        {
            ConfiguracionDTO itemDTO = new ConfiguracionDTO();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspBuscarConfiguracion_apfitness", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oConfiguracion.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oConfiguracion.CodigoSede;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new ConfiguracionDTO()
                                {

                                    CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Codigo")]),
                                    RazonSocial = oIDataReader[oIDataReader.GetOrdinal("RazonSocial")].ToString(),
                                    Pais = oIDataReader[oIDataReader.GetOrdinal("Pais")].ToString(),
                                    Departamento = oIDataReader[oIDataReader.GetOrdinal("Departamento")].ToString(),
                                    Distrito = oIDataReader[oIDataReader.GetOrdinal("Distrito")].ToString(),
                                    Direccion = oIDataReader[oIDataReader.GetOrdinal("Direccion")].ToString(),
                                    Igv = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Igv")]),
                                    TipoDescuento = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("TipoDescuento")]),
                                    GenerarSerie = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("GenerarSerie")]),
                                    GenerarComprobante = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("GenerarComprobante")]),
                                    ConexionDB = oIDataReader[oIDataReader.GetOrdinal("ConexionDB")].ToString(),
                                    RutaCarpetaImagen = oIDataReader[oIDataReader.GetOrdinal("RutaCarpetaImagen")].ToString(),
                                    LongitudSerie = oIDataReader[oIDataReader.GetOrdinal("LongitudSerie")].ToString(),
                                    NombreTiquetera = oIDataReader[oIDataReader.GetOrdinal("NombreTiquetera")].ToString(),
                                    Correo = oIDataReader[oIDataReader.GetOrdinal("Correo")].ToString(),
                                    Contrasenia = oIDataReader[oIDataReader.GetOrdinal("Contrasenia")].ToString(),
                                    Estado = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Estado")]),

                                    Tipo = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Tipo")]),
                                    PermitirMuchasAsistenciaPordia = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("PermitirMuchasAsistenciaPordia")]),
                                    DescontarFreezingDisponiblesFlag = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("DescontarFreezingDisponiblesFlag")]),
                                    DescontarFreezingDisponiblesNumero = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("DescontarFreezingDisponiblesNumero")]),
                                    Ruc = oIDataReader[oIDataReader.GetOrdinal("RUC")].ToString(),
                                    Telefono = oIDataReader[oIDataReader.GetOrdinal("Telefono")].ToString(),
                                    NombreComercial = oIDataReader[oIDataReader.GetOrdinal("NombreComercial")].ToString(),
                                    Frase = oIDataReader[oIDataReader.GetOrdinal("Frase")].ToString(),
                                    ClientesxVendedorAleatorio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("ClienteXVendedorAleatorio")]),
                                    NumeroDiaMesEjecucionAleatorio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("NumeroDiaMesEjecucionAleatorio")]),
                                    NotificarDeudasXDia = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("NotificarDeudasXDia")]),

                                    CantDiasDeuda = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantDiasDeudas")]),
                                    EstadoMostrarVentaOtros = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("MostrarVentaOtros")]),
                                    EstadoActivarCorreoBienvenida = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("ActivarCorreoBienvenida")]),
                                    EstadoImprimirContrato = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("ActivarImprimirContrato")]),
                                    TiempoMarcarAsistencia = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("TiempoMarcarAsistencia")]),
                                    DiaCitasCaida = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("DiaCitasCaida")]),

                                    SubDominio = oIDataReader[oIDataReader.GetOrdinal("SubDominio")].ToString(),

                                    Ubicaciones = oIDataReader[oIDataReader.GetOrdinal("Ubigeo")].ToString(),
                                    MontoMensualidad = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("MontoMensualidad")]),

                                    FechaPago = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaPago")]),
                                    FechaVencimiento = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaVencimiento")]),

                                    str_FechaPago = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaPago")]).ToString("dd/MM/yyyy"),
                                    str_FechaVencimiento = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaVencimiento")]).ToString("dd/MM/yyyy"),

                                    //EntidadBancaria = oIDataReader[oIDataReader.GetOrdinal("EntidadBancaria")].ToString(),
                                    //NroCuenta = oIDataReader[oIDataReader.GetOrdinal("NroCuenta")].ToString(),
                                    //ResponsableCuenta = oIDataReader[oIDataReader.GetOrdinal("ResponsableCuenta")].ToString(),

                                    CodigoCuenta = oIDataReader[oIDataReader.GetOrdinal("CodigoCuenta")].ToString(),
                                    CodigoPlan = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoPlan")]),
                                    TipoMoneda = oIDataReader[oIDataReader.GetOrdinal("TipoMoneda")].ToString(),
                                    Logo = oIDataReader[oIDataReader.GetOrdinal("Logo")].ToString(),
                                    TieneFacturacionElectronica = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("TieneFacturacionElectronica")]),
                                    UrlAPISunafact = oIDataReader[oIDataReader.GetOrdinal("UrlAPISunafact")].ToString(),
                                    TokenSunafact = oIDataReader[oIDataReader.GetOrdinal("TokenSunafact")].ToString(),

                                    NombreGerente = oIDataReader[oIDataReader.GetOrdinal("NombreGerente")].ToString(),
                                    ContactoCobranza = oIDataReader[oIDataReader.GetOrdinal("ContactoCobranza")].ToString(),
                                    CelularCobranza = oIDataReader[oIDataReader.GetOrdinal("CelularCobranza")].ToString(),

                                    AplicacionDisponible = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("AplicacionDisponible")]),
                                    TiendaAplicacion = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("TiendaAplicacion")]),
                                    RutinasAplicacion = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("RutinasAplicacion")])

                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;
        }

        public void Eliminar(ConfiguracionDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspEliminarConfiguracion", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Codigo", System.Data.SqlDbType.Int)).Value = item.Codigo;

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void uspEliminarCliente_Configuracion_AdFitness(ConfiguracionDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspEliminarCliente_Configuracion_AdFitness", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public int uspValidarConfiguracionAdFitness_UnidadNegocio_Sede(int CodigoUnidadNegocio, int CodigoSede, string Dominio)
        {
            int? campoRetorno = 0;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspValidarConfiguracionAdFitness_UnidadNegocio_Sede", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@SubDominio", System.Data.SqlDbType.VarChar, 100)).Value = Dominio;
                    cmd.Parameters.AddWithValue("@Existe", campoRetorno).Direction = System.Data.ParameterDirection.Output;

                    cmd.ExecuteNonQuery();
                    campoRetorno = Convert.ToInt32(cmd.Parameters["@Existe"].Value);
                }
            }
            return Convert.ToInt32(campoRetorno);
        }

        public List<ConfiguracionDTO> uspListarSedesPorSedesPermisos(ConfiguracionDTO oConfiguracion)
        {
            List<ConfiguracionDTO> lista = new List<ConfiguracionDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarSedesPorSedesPermisos", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oConfiguracion.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoPaquete", System.Data.SqlDbType.Int)).Value = oConfiguracion.CodigoPaquete;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ConfiguracionDTO()
                                {
                                    CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    RazonSocial = oIDataReader[oIDataReader.GetOrdinal("RazonSocial")].ToString(),
                                    Estado = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Estado")]),
                                    DesEstado = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Estado")]) == 1 ? "checked" : ""
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }

        public void uspRegistrarPaqueteSedePermiso(ConfiguracionDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspRegistrarPaqueteSedePermiso", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoPermiso", System.Data.SqlDbType.Int)).Value = item.CodigoPermiso;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoPaquete", System.Data.SqlDbType.Int)).Value = item.CodigoPaquete;

                    cmd.ExecuteNonQuery();
                }
            }

        }

        public void uspEliminarPaqueteSedePermiso(ConfiguracionDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspEliminarPaqueteSedePermiso", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoPaquete", System.Data.SqlDbType.Int)).Value = item.CodigoPaquete;

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<ConfiguracionDTO> uspListarConfiguracionCuentas()
        {
            List<ConfiguracionDTO> lista = new List<ConfiguracionDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarConfiguracionCuentas", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ConfiguracionDTO()
                                {
                                    CodigoNroCuenta = oIDataReader[oIDataReader.GetOrdinal("CodigoCuenta")].ToString(),
                                    EntidadBancaria = oIDataReader[oIDataReader.GetOrdinal("Banco")].ToString(),
                                    NroCuenta = oIDataReader[oIDataReader.GetOrdinal("NroCuenta")].ToString(),
                                    ResponsableCuenta = oIDataReader[oIDataReader.GetOrdinal("Nombre")].ToString(),
                                    CCI = oIDataReader[oIDataReader.GetOrdinal("CCI")].ToString(),
                                    Estado = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Estado")]),
                                    DesEstado = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Estado")]) == 1 ? "Activo" : "Inactivo",
                                    RUC = oIDataReader[oIDataReader.GetOrdinal("RUC")].ToString(),
                                    USUARIO = oIDataReader[oIDataReader.GetOrdinal("USUARIO")].ToString(),
                                    CLAVE = oIDataReader[oIDataReader.GetOrdinal("CLAVE")].ToString()
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }

        public List<ConfiguracionDTO> uspByteFitMatriculasMensuales(ConfiguracionDTO item)
        {
            List<ConfiguracionDTO> lista = new List<ConfiguracionDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspByteFitMatriculasMensuales", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Anio", System.Data.SqlDbType.Int)).Value = item.Anio;
                    cmd.Parameters.Add(new SqlParameter("@Mes", System.Data.SqlDbType.Int)).Value = item.MesEntero;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ConfiguracionDTO()
                                {
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    MontoMatricula = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("MontoMatricula")]),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString(),
                                    NroOperacion = oIDataReader[oIDataReader.GetOrdinal("NroOperacion")].ToString(),
                                    FechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")])
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }

        public List<ConfiguracionDTO> uspByteFitVentasResumen(ConfiguracionDTO item)
        {
            List<ConfiguracionDTO> lista = new List<ConfiguracionDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspByteFitVentasResumen", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Anio", System.Data.SqlDbType.Int)).Value = item.Anio;
                    cmd.Parameters.Add(new SqlParameter("@Mes", System.Data.SqlDbType.Int)).Value = item.MesEntero;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ConfiguracionDTO()
                                {
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    SumaTotalAcuenta = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("TotalVenta")])
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }

        public List<ConfiguracionDTO> uspByteFitVentasMensuales(ConfiguracionDTO item)
        {
            List<ConfiguracionDTO> lista = new List<ConfiguracionDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspByteFitVentasMensuales", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Anio", System.Data.SqlDbType.Int)).Value = item.Anio;
                    cmd.Parameters.Add(new SqlParameter("@Mes", System.Data.SqlDbType.Int)).Value = item.MesEntero;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ConfiguracionDTO()
                                {
                                    CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    CantidadEmpresasActivas = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadGimnasios")]),
                                    FechaCreacionEmpresa = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacionEmpresa")]),
                                    NombreComercial = oIDataReader[oIDataReader.GetOrdinal("RazonSocial")].ToString(),
                                    RazonSocial = oIDataReader[oIDataReader.GetOrdinal("RazonSocial")].ToString(),
                                    RUC = oIDataReader[oIDataReader.GetOrdinal("RUC")].ToString(),
                                    FechaPago = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaPago")]),
                                    FechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")]),
                                    DiasDemora = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("DiasDemora")]),
                                    MontoMes = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("MontoMes")]),
                                    MontoAcuenta = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("MontoAcuenta")]),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString(),
                                    EntidadBancaria = oIDataReader[oIDataReader.GetOrdinal("Banco")].ToString(),
                                    NroCuenta = oIDataReader[oIDataReader.GetOrdinal("NroCuenta")].ToString(),
                                    ResponsableCuenta = oIDataReader[oIDataReader.GetOrdinal("Nombre")].ToString()
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }

        public List<ConfiguracionDTO> uspByteFitGastosMensuales(ConfiguracionDTO item)
        {
            List<ConfiguracionDTO> lista = new List<ConfiguracionDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspByteFitGastosMensuales", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Anio", System.Data.SqlDbType.Int)).Value = item.Anio;
                    cmd.Parameters.Add(new SqlParameter("@Mes", System.Data.SqlDbType.Int)).Value = item.MesEntero;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ConfiguracionDTO()
                                {
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    MontoGasto = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("MontoGasto")]),
                                    NroOperacion = oIDataReader[oIDataReader.GetOrdinal("NroOperacion")].ToString(),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString(),
                                    FechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")])
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }

        public List<ConfiguracionDTO> uspByteFitClientesNuevosDelMes(ConfiguracionDTO item)
        {
            List<ConfiguracionDTO> lista = new List<ConfiguracionDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspByteFitClientesNuevosDelMes", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Anio", System.Data.SqlDbType.Int)).Value = item.Anio;
                    cmd.Parameters.Add(new SqlParameter("@Mes", System.Data.SqlDbType.Int)).Value = item.MesEntero;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ConfiguracionDTO()
                                {
                                    NombreComercial = oIDataReader[oIDataReader.GetOrdinal("NombreComercial")].ToString(),
                                    RazonSocial = oIDataReader[oIDataReader.GetOrdinal("RazonSocial")].ToString(),
                                    RUC = oIDataReader[oIDataReader.GetOrdinal("RUC")].ToString(),
                                    Correo = oIDataReader[oIDataReader.GetOrdinal("Correo")].ToString(),
                                    Telefono = oIDataReader[oIDataReader.GetOrdinal("Telefono")].ToString(),
                                    MontoMensualidad = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("MontoMensualidad")]),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString(),
                                    FechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")]),
                                    DesEstado = oIDataReader[oIDataReader.GetOrdinal("DesEstado")].ToString()
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }


        public void uspRegistrarConfiguracionConsultaDocumentoPersonas_Log(ConfiguracionDTO item)
        {
            //int? campoRetorno = 0;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspRegistrarConfiguracionConsultaDocumentoPersonas_Log", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@ConsultaNroDocumento", System.Data.SqlDbType.VarChar, 100)).Value = item.ConsultasNumeroDocumento_ConsultaNroDocumento;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;

                    cmd.ExecuteNonQuery();
                    //campoRetorno = Convert.ToInt32(cmd.Parameters["@Existe"].Value);
                }
            }
            // return Convert.ToInt32(campoRetorno);
        }



        //actualizar config host
        public void uspActualizarConfiguracion_HostEnvioEmail(ConfiguracionDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("UspActualizarConfiguracion_HostEnvioEmail", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@email_host", System.Data.SqlDbType.VarChar)).Value = item.EmailHost;
                    cmd.Parameters.Add(new SqlParameter("@email_puerto", System.Data.SqlDbType.Int)).Value = item.EmailPort;
                    cmd.Parameters.Add(new SqlParameter("@email_usuario", System.Data.SqlDbType.VarChar)).Value = item.EmailUser;
                    cmd.Parameters.Add(new SqlParameter("@email_clave", System.Data.SqlDbType.VarChar)).Value = item.EmailKey;
                    cmd.ExecuteNonQuery();
                    //ID = cmd.Parameters["@po_CodigoProducto"].Value.ToString();
                }

            }
        }


    }
}
