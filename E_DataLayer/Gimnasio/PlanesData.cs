using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using E_DataModel.Gimnasio;
using E_DataModel.Common;
using System.Data.SqlClient;

namespace E_DataLayer.Gimnasio
{
    public class PlanesData
    {
        public List<PlanesDTO> uspListarTotalesPaquetesPorMes(PlanesDTO oitem)
        {
            List<PlanesDTO> lista = new List<PlanesDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarTotalesPaquetesPorMes", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oitem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@Anio", System.Data.SqlDbType.Int)).Value = oitem.Anio;
                    cmd.Parameters.Add(new SqlParameter("@Mes", System.Data.SqlDbType.Int)).Value = oitem.Mes;
                    cmd.Parameters.Add(new SqlParameter("@Dia", System.Data.SqlDbType.Int)).Value = oitem.Dia;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oitem.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoTipoPaquete", System.Data.SqlDbType.Int)).Value = oitem.CodigoTipoPaquete;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new PlanesDTO()
                                {
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    ImporteTotal = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("ImporteTotalMembresias")])
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }

        public List<PlanesDTO> uspListarPaquetesMenbresiasCursos_Paginacion(PlanesDTO oPaquetesDTO, Paging paging, ref uint recordCount)
        {
            List<PlanesDTO> lista = new List<PlanesDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarPaquetesMenbresiasCursos_Paginacion", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oPaquetesDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oPaquetesDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@TipoPaquete", System.Data.SqlDbType.Int)).Value = oPaquetesDTO.CodigoTipoPaquete;
                    cmd.Parameters.Add(new SqlParameter("@Busqueda", System.Data.SqlDbType.VarChar, 200)).Value = oPaquetesDTO.Busqueda;
                    cmd.Parameters.Add(new SqlParameter("@Estado", System.Data.SqlDbType.Bit)).Value = oPaquetesDTO.Estado;

                    cmd.Parameters.Add(new SqlParameter("@PaginaActual", System.Data.SqlDbType.Int)).Value = paging.PageNumber;
                    cmd.Parameters.Add(new SqlParameter("@TamanioPagina", System.Data.SqlDbType.Int)).Value = paging.PageRecords;
                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new PlanesDTO()
                                {
                                    CodigoPaquete = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoPaquete")]),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString() + " Precio: " + oIDataReader[oIDataReader.GetOrdinal("Costo")].ToString(),
                                    DesPaquete = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    valor = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("valor")]),
                                    Codigovalor = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoPaquete")]) + "|" + Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("valor")]) + "|" + Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Costo")]),
                                    Costo = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Costo")]),
                                    DesEstado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Estado")]) ? "Vigente" : "Inactivo",
                                    Estado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Estado")]),
                                    FechaVencimiento = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaVencimiento")]),
                                    desNomProfesor = oIDataReader[oIDataReader.GetOrdinal("desNomProfesor")].ToString(),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString(),
                                    DescFechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")]).ToString("dd/MM/yyyy HH:mm tt"),
                                    DesTiempoMembresia = oIDataReader[oIDataReader.GetOrdinal("DesTiempoPlan")].ToString()
                                });
                            }
                        }

                    }
                }
            }
            return lista;

        }

        //************************************************************ API******************************************
        public List<PlanesDTO> ListarPaquetesApp(PlanesDTO oPaquetesDTO)
        {
            //Paging paging, ref uint recordCount
            List<PlanesDTO> lista = new List<PlanesDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarPaquetesApp", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oPaquetesDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oPaquetesDTO.CodigoSede;


                    // cmd.Parameters.Add(new SqlParameter("@PaginaActual", System.Data.SqlDbType.Int)).Value = paging.PageNumber;
                    //cmd.Parameters.Add(new SqlParameter("@TamanioPagina", System.Data.SqlDbType.Int)).Value = paging.PageRecords;
                    //cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new PlanesDTO()
                                {
                                    CodigoPaquete = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoPaquete")]),
                                    DesTiempoMembresia = oIDataReader[oIDataReader.GetOrdinal("DesTiempoPlan")].ToString(),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    CodigoTipoPaquete = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoTipoPaquete")]),
                                    EstadoMembresiaInterdiaria = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("EstadoMembresiaInterdiaria")]),
                                    CongelamientoVigente = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CongelamientoVigente")]),
                                    ValorTiempoPlan = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("ValorTiempoPlan")]),
                                    ValorSesiones = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("valorSesiones")]),
                                    Costo = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Costo")]),
                                    NroCupo = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("NroCupo")]),
                                    FechaVencimiento = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaVencimiento")]),
                                    desNomProfesor = oIDataReader[oIDataReader.GetOrdinal("desNomProfesor")].ToString(),
                                    DesTipoPaquete = oIDataReader[oIDataReader.GetOrdinal("DesTipoPaquete")].ToString(),
                                    UrlImage = oIDataReader[oIDataReader.GetOrdinal("UrlImage")].ToString(),
                                    Suscripcion = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Suscripcion")]),

                                });
                            }
                        }

                    }
                }
            }
            return lista;

        }

        public PlanesDTO uspListarPaquetesMenbresiasCursos_NumeroRegistros(PlanesDTO oPaquetesDTO)
        {
            PlanesDTO itemDTO = new PlanesDTO();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarPaquetesMenbresiasCursos_NumeroRegistros", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oPaquetesDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oPaquetesDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@TipoPaquete", System.Data.SqlDbType.Int)).Value = oPaquetesDTO.CodigoTipoPaquete;
                    cmd.Parameters.Add(new SqlParameter("@Busqueda", System.Data.SqlDbType.VarChar, 200)).Value = oPaquetesDTO.Busqueda;
                    cmd.Parameters.Add(new SqlParameter("@Estado", System.Data.SqlDbType.Bit)).Value = oPaquetesDTO.Estado;

                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                itemDTO = new PlanesDTO()
                                {
                                    CantidadRegistros = Convert.ToInt32(reader[reader.GetOrdinal("CantidadRegistros")])
                                };
                            }
                        }
                    }
                }
            }

            return itemDTO;
        }

        public List<PlanesDTO> ListarPaquetesBusquedaFiltroSocio(PlanesDTO oPaquetesDTO)
        {
            List<PlanesDTO> lista = new List<PlanesDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarPaquetesBusquedaFiltroSocio", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oPaquetesDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oPaquetesDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Nombre", System.Data.SqlDbType.VarChar, 100)).Value = oPaquetesDTO.Descripcion;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new PlanesDTO()
                                {
                                    CodigoPaquete = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoPaquete")]),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString() + " Precio: " + oIDataReader[oIDataReader.GetOrdinal("Costo")].ToString(),
                                    CodigoTipoCliente = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoTipoCliente")]),
                                    DescripcionTipoCliente = oIDataReader[oIDataReader.GetOrdinal("DescripcionTipoCliente")].ToString(),
                                    valor = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("valor")]),
                                    Codigovalor = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoPaquete")]) + "|" + Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("valor")]) + "|" + Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Costo")]),
                                    Costo = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Costo")]),
                                    DesEstado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Estado")]) ? "Vigente" : "Inactivo",
                                    Estado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Estado")]),
                                    CongelamientoVigente = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CongelamientoVigente")]),
                                    FechaVencimiento = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaVencimiento")])
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }

        public List<PlanesDTO> ListarPaquetesTablaProspectos(PlanesDTO oPaquetesDTO)
        {
            List<PlanesDTO> lista = new List<PlanesDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarPaquetesTablaProspectos", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oPaquetesDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oPaquetesDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Nombre", System.Data.SqlDbType.VarChar, 100)).Value = oPaquetesDTO.Descripcion;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new PlanesDTO()
                                {
                                    CodigoPaquete = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoPaquete")]),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString() + " Precio: " + oIDataReader[oIDataReader.GetOrdinal("Costo")].ToString(),
                                    CodigoTipoCliente = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoTipoCliente")]),
                                    DescripcionTipoCliente = oIDataReader[oIDataReader.GetOrdinal("DescripcionTipoCliente")].ToString(),
                                    valor = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("valor")]),
                                    Codigovalor = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoPaquete")]) + "|" + Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("valor")]) + "|" + Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Costo")]),
                                    Costo = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Costo")]),
                                    DesEstado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Estado")]) ? "Vigente" : "Inactivo",
                                    Estado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Estado")]),
                                    CongelamientoVigente = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CongelamientoVigente")]),
                                    FechaVencimiento = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaVencimiento")])
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }

        public List<PlanesDTO> uspListarPaquetesPorProfesor(PlanesDTO oPaquetesDTO)
        {
            List<PlanesDTO> lista = new List<PlanesDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarPaquetesPorProfesor", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oPaquetesDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@Nombre", System.Data.SqlDbType.VarChar, 100)).Value = oPaquetesDTO.Descripcion;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oPaquetesDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoProfesor", System.Data.SqlDbType.Int)).Value = oPaquetesDTO.CodigoProfesor;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new PlanesDTO()
                                {
                                    CodigoPaquete = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoPaquete")]),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString() + " Precio: " + oIDataReader[oIDataReader.GetOrdinal("Costo")].ToString(),
                                    CodigoTipoCliente = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoTipoCliente")]),
                                    DescripcionTipoCliente = oIDataReader[oIDataReader.GetOrdinal("DescripcionTipoCliente")].ToString(),
                                    valor = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("valor")]),
                                    Codigovalor = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoPaquete")]) + "|" + Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("valor")]) + "|" + Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Costo")]),
                                    Costo = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Costo")]),
                                    DesEstado = oIDataReader[oIDataReader.GetOrdinal("DesEstado")].ToString(),
                                    ColorDesEstado = oIDataReader[oIDataReader.GetOrdinal("ColorDesEstado")].ToString(),
                                    Estado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Estado")]),
                                    CongelamientoVigente = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CongelamientoVigente")]),
                                    FechaVencimiento = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaVencimiento")]),
                                    CodigoTipoPaquete = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoTipoPaquete")]),
                                    DescTipoPaquete = oIDataReader[oIDataReader.GetOrdinal("DesTipoPaquete")].ToString(),
                                });
                            }
                        }

                    }
                }
            }
            return lista;

        }

        public PlanesDTO BuscarPorCodigoPaquetes(PlanesDTO oPaquetes)
        {
            PlanesDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspBuscarPaquetesPorCodigo", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oPaquetes.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oPaquetes.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoPaquete", System.Data.SqlDbType.Int)).Value = oPaquetes.CodigoPaquete;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                itemDTO = new PlanesDTO()
                                {
                                    CodigoPaquete = Convert.ToInt32(reader[reader.GetOrdinal("CodigoPaquete")]),
                                    Descripcion = reader[reader.GetOrdinal("Descripcion")].ToString(),
                                    valor = Convert.ToInt32(reader[reader.GetOrdinal("valor")]),
                                    Costo = Convert.ToDecimal(reader[reader.GetOrdinal("Costo")]),
                                    Estado = Convert.ToBoolean(reader[reader.GetOrdinal("Estado")]),
                                    EstadoMembresiaInterdiaria = Convert.ToBoolean(reader[reader.GetOrdinal("EstadoMembresiaInterdiaria")]),
                                    CongelamientoVigente = Convert.ToInt32(reader[reader.GetOrdinal("CongelamientoVigente")]),
                                    FechaVencimiento = Convert.ToDateTime(reader[reader.GetOrdinal("FechaVencimiento")]),
                                    CodigoTipoCliente = Convert.ToInt32(reader[reader.GetOrdinal("CodigoTipoCliente")]),
                                    CodigoTipoPaquete = Convert.ToInt32(reader[reader.GetOrdinal("CodigoTipoPaquete")]),
                                    TiempoMembresia = Convert.ToInt32(reader[reader.GetOrdinal("CodTiempoMenbresia")]),
                                    NroCupo = Convert.ToInt32(reader[reader.GetOrdinal("NroCupo")]),
                                    FechaInicio = Convert.ToDateTime(reader[reader.GetOrdinal("FechaInicio")]),
                                    EstadoFecha = Convert.ToInt32(reader[reader.GetOrdinal("EstadoFecha")]),
                                    NroIngresoDia = Convert.ToInt32(reader[reader.GetOrdinal("NroIngresoDia")]),
                                    ValorDias_Tiempo = Convert.ToInt32(reader[reader.GetOrdinal("ValorDias")]),
                                    ShowApp = Convert.ToBoolean(reader[reader.GetOrdinal("VisualizarTienda")]),
                                    UrlImage = reader[reader.GetOrdinal("UrlImage")].ToString(),
                                    Suscripcion = Convert.ToBoolean(reader[reader.GetOrdinal("Suscripcion")]),

                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;
        }

        public PlanesDTO BuscarCantidadCupoPaquetesPorCodigo(PlanesDTO oPaquetes)
        {
            PlanesDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspBuscarCantidadCupoPaquetesPorCodigo", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oPaquetes.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oPaquetes.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoPaquete", System.Data.SqlDbType.Int)).Value = oPaquetes.CodigoPaquete;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                itemDTO = new PlanesDTO()
                                {
                                    CodigoPaquete = reader[reader.GetOrdinal("CodigoPaquete")] == null ? 0 : Convert.ToInt32(reader[reader.GetOrdinal("CodigoPaquete")]),
                                    Descripcion = reader[reader.GetOrdinal("Descripcion")].ToString(),
                                    NroCupo = Convert.ToInt32(reader[reader.GetOrdinal("NroCupo")]),
                                    CantidaIngresos = Convert.ToInt32(reader[reader.GetOrdinal("CantidaIngresos")]),
                                    CantCupos = Convert.ToInt32(reader[reader.GetOrdinal("CantCupos")])
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;
        }

        public void Registrar(PlanesDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspRegistrarPaquetes", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoPaquete", System.Data.SqlDbType.Int)).Value = item.CodigoPaquete;
                    cmd.Parameters.Add(new SqlParameter("@Descripcion", System.Data.SqlDbType.VarChar, 100)).Value = item.Descripcion;
                    cmd.Parameters.Add(new SqlParameter("@valor", System.Data.SqlDbType.Int)).Value = item.valor;
                    cmd.Parameters.Add(new SqlParameter("@Costo", System.Data.SqlDbType.Decimal)).Value = item.Costo;

                    cmd.Parameters.Add(new SqlParameter("@Estado", System.Data.SqlDbType.Bit)).Value = item.Estado;
                    cmd.Parameters.Add(new SqlParameter("@EstadoMembresiaInterdiaria", System.Data.SqlDbType.Bit)).Value = item.EstadoMembresiaInterdiaria;

                    cmd.Parameters.Add(new SqlParameter("@FechaVencimiento", System.Data.SqlDbType.DateTime)).Value = item.FechaVencimiento;
                    cmd.Parameters.Add(new SqlParameter("@CongelamientoVigente", System.Data.SqlDbType.Int)).Value = item.CongelamientoVigente;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;

                    cmd.Parameters.Add(new SqlParameter("@CodigoTipoCliente", System.Data.SqlDbType.Int)).Value = item.CodigoTipoCliente;
                    cmd.Parameters.Add(new SqlParameter("@CodigoTipoPaquete", System.Data.SqlDbType.Int)).Value = item.CodigoTipoPaquete;
                    cmd.Parameters.Add(new SqlParameter("@CodTiempoMenbresia", System.Data.SqlDbType.Int)).Value = item.TiempoMembresia;
                    cmd.Parameters.Add(new SqlParameter("@NroCupo", System.Data.SqlDbType.Int)).Value = item.NroCupo;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = item.FechaInicio;

                    cmd.Parameters.Add(new SqlParameter("@EstadoFecha", System.Data.SqlDbType.Int)).Value = item.EstadoFecha;
                    cmd.Parameters.Add(new SqlParameter("@NroIngresoDia", System.Data.SqlDbType.Int)).Value = item.NroIngresoDia;
                    cmd.Parameters.Add(new SqlParameter("@CodigoInicioSesion", System.Data.SqlDbType.Int)).Value = item.CodigoInicioSesion;
                    cmd.Parameters.Add(new SqlParameter("@VisualizarTienda", System.Data.SqlDbType.Bit)).Value = item.ShowApp;
                    cmd.Parameters.Add(new SqlParameter("@UrlImage", System.Data.SqlDbType.VarChar, 100)).Value = item.UrlImage;
                    cmd.Parameters.Add(new SqlParameter("@Suscripcion", System.Data.SqlDbType.Bit)).Value = item.Suscripcion;

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Actualizar(PlanesDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspActualizarPaquetes", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoPaquete", System.Data.SqlDbType.Int)).Value = item.CodigoPaquete;
                    cmd.Parameters.Add(new SqlParameter("@Descripcion", System.Data.SqlDbType.VarChar, 100)).Value = item.Descripcion;
                    cmd.Parameters.Add(new SqlParameter("@valor", System.Data.SqlDbType.Int)).Value = item.valor;
                    cmd.Parameters.Add(new SqlParameter("@Costo", System.Data.SqlDbType.Decimal)).Value = item.Costo;

                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;
                    cmd.Parameters.Add(new SqlParameter("@Estado", System.Data.SqlDbType.Bit)).Value = item.Estado;
                    cmd.Parameters.Add(new SqlParameter("@EstadoMembresiaInterdiaria", System.Data.SqlDbType.Bit)).Value = item.EstadoMembresiaInterdiaria;
                    cmd.Parameters.Add(new SqlParameter("@FechaVencimiento", System.Data.SqlDbType.DateTime)).Value = item.FechaVencimiento;
                    cmd.Parameters.Add(new SqlParameter("@CongelamientoVigente", System.Data.SqlDbType.Int)).Value = item.CongelamientoVigente;
                    cmd.Parameters.Add(new SqlParameter("@CodigoTipoCliente", System.Data.SqlDbType.Int)).Value = item.CodigoTipoCliente;

                    cmd.Parameters.Add(new SqlParameter("@CodigoTipoPaquete", System.Data.SqlDbType.Int)).Value = item.CodigoTipoPaquete;
                    cmd.Parameters.Add(new SqlParameter("@CodTiempoMenbresia", System.Data.SqlDbType.Int)).Value = item.TiempoMembresia;
                    cmd.Parameters.Add(new SqlParameter("@NroCupo", System.Data.SqlDbType.Int)).Value = item.NroCupo;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = item.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@EstadoFecha", System.Data.SqlDbType.Int)).Value = item.EstadoFecha;

                    cmd.Parameters.Add(new SqlParameter("@NroIngresoDia", System.Data.SqlDbType.Int)).Value = item.NroIngresoDia;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoInicioSesion", System.Data.SqlDbType.Int)).Value = item.CodigoInicioSesion;
                    cmd.Parameters.Add(new SqlParameter("@VisualizarTienda", System.Data.SqlDbType.Bit)).Value = item.ShowApp;
                    cmd.Parameters.Add(new SqlParameter("@UrlImage", System.Data.SqlDbType.VarChar, 100)).Value = item.UrlImage;
                    cmd.Parameters.Add(new SqlParameter("@Suscripcion", System.Data.SqlDbType.Bit)).Value = item.Suscripcion;

                    cmd.ExecuteNonQuery();
                }
            }

        }

        public int Eliminar(PlanesDTO item)
        {
            int? cantidad = 0;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspEliminarPaquetes", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoPaquete", System.Data.SqlDbType.Int)).Value = item.CodigoPaquete;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;
                    cmd.Parameters.Add(new SqlParameter("@CodigoInicioSesion", System.Data.SqlDbType.Int)).Value = 0;

                    cmd.Parameters.AddWithValue("@Cantidad", 0).Direction = System.Data.ParameterDirection.Output;

                    cmd.ExecuteNonQuery();

                    cantidad = Convert.ToInt32(cmd.Parameters["@Cantidad"].Value);
                }
            }
            return Convert.ToInt32(cantidad);
        }

        public int ValidarBuscarDiasHorarioPaquete(int CodigoUnidadNegocio, int CodigoPaquete)
        {
            int? campoRetorno = 0;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspValidarBuscarDiasHorarioPaquete", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = CodigoUnidadNegocio;
                    cmd.Parameters.AddWithValue("@Retorno", 0).Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@CodigoPaquete", System.Data.SqlDbType.Int)).Value = CodigoPaquete;

                    cmd.ExecuteNonQuery();
                    campoRetorno = Convert.ToInt32(cmd.Parameters["@Retorno"].Value);
                }
            }
            return Convert.ToInt32(campoRetorno);
        }


        //********************************************************** SUSCRIPCIONES ****************************************************

        //list plan pasarela by paquete
        public List<PlanesDTO> ListPlanPasarelaByPaquete(PlanesDTO oitem)
        {
            List<PlanesDTO> lista = new List<PlanesDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarPaquetesSuscripcion", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oitem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oitem.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoPaquete", System.Data.SqlDbType.Int)).Value = oitem.CodigoPaquete;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new PlanesDTO()
                                {
                                    CodigoPaqueteSuscripcion = oIDataReader[oIDataReader.GetOrdinal("CodigoPaqueteSuscripcion")].ToString(),
                                    CodigoPlantillaFormaPago = oIDataReader[oIDataReader.GetOrdinal("CodigoPlantillaFormaPago")].ToString(),
                                    IdSuscripcionPasarela = oIDataReader[oIDataReader.GetOrdinal("IdSuscripcionPasarela")].ToString(),
                                    CodigoPaquete = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoPaquete")]),
                                    DesSuscripcionPlan = oIDataReader[oIDataReader.GetOrdinal("DesSuscripcionPlan")].ToString(),
                                    DesPasarelaPago = oIDataReader[oIDataReader.GetOrdinal("DesPasarelaPago")].ToString(),
                                    UrlImage = oIDataReader[oIDataReader.GetOrdinal("UrlImagenPasarelaPago")].ToString(),

                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }



        //register paquete plan - pasarela
        public void RegisterPlanPasarela(PlanesDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspRegistrarPaquetesSuscripcion", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoPaquete", System.Data.SqlDbType.Int)).Value = item.CodigoPaquete;

                    cmd.Parameters.Add(new SqlParameter("@CodigoPaqueteSuscripcion", System.Data.SqlDbType.VarChar, 100)).Value = 0;
                    cmd.Parameters.Add(new SqlParameter("@CodigoPlantillaFormaPago", System.Data.SqlDbType.VarChar, 100)).Value = item.CodigoPlantillaFormaPago;
                    cmd.Parameters.Add(new SqlParameter("@IdSuscripcionPasarela", System.Data.SqlDbType.VarChar, 100)).Value = item.IdSuscripcionPasarela;
                    cmd.Parameters.Add(new SqlParameter("@Descripcion", System.Data.SqlDbType.VarChar, 100)).Value = item.Descripcion;
                    if (!string.IsNullOrEmpty(item.UsuarioCreacion))
                    {
                        cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;
                    }
                    cmd.ExecuteNonQuery();
                }
            }
        }


        //destroy plan paquete
        public void DestroyPlanPaquete(PlanesDTO item)
        {

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspEliminarPaquetesSuscripcion", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoPaquete", System.Data.SqlDbType.Int)).Value = item.CodigoPaquete;
                    cmd.Parameters.Add(new SqlParameter("@CodigoPaqueteSuscripcion", System.Data.SqlDbType.VarChar, 100)).Value = item.CodigoPaqueteSuscripcion;
                    cmd.ExecuteNonQuery();

                }
            }
        }




        //register table idsuscription
        public void RegisterMembSuscription(PlanesDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspRegistrarMembresiasSuscripcion", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoMembresiasSuscripcion", System.Data.SqlDbType.VarChar,100)).Value = item.CodigoMembresiasSuscripcion;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSocio", System.Data.SqlDbType.Int)).Value = item.CodigoSocio;
                    cmd.Parameters.Add(new SqlParameter("@NroDocumento", System.Data.SqlDbType.VarChar,100)).Value = item.NroDocumento;
                    cmd.Parameters.Add(new SqlParameter("@DefaultKeyEmpresa", System.Data.SqlDbType.VarChar,100)).Value = item.DefaultKeyEmpresa;
                    cmd.Parameters.Add(new SqlParameter("@DefaultKeyUser", System.Data.SqlDbType.VarChar,100)).Value = item.DefaultKeyUser;
                    cmd.Parameters.Add(new SqlParameter("@CodigoPlantilaFormaPago", System.Data.SqlDbType.VarChar,100)).Value = item.CodigoPlantillaFormaPago;
                    cmd.Parameters.Add(new SqlParameter("@IdClientePasarela", System.Data.SqlDbType.VarChar,100)).Value = item.IdClientePasarela;
                    cmd.Parameters.Add(new SqlParameter("@IdSuscripcionPasarela", System.Data.SqlDbType.VarChar, 100)).Value = item.IdSuscripcionPasarela;
                    cmd.Parameters.Add(new SqlParameter("@EstadoSuscripcionPasarela", System.Data.SqlDbType.Bit)).Value = true;
                    cmd.Parameters.Add(new SqlParameter("@DataJsonPasarela", System.Data.SqlDbType.VarChar,5000)).Value = item.DataJsonPasarela;
                    cmd.Parameters.Add(new SqlParameter("@CodigoPaquete", System.Data.SqlDbType.Int)).Value = item.CodigoPaquete;

                    
                    if (!string.IsNullOrEmpty(item.UsuarioCreacion))
                    {
                        cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;
                    }
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }


        //search by IdSuscription
        public PlanesDTO SearchMemSucriptionByIdSuscription(PlanesDTO oPaquetes)
        {
            PlanesDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspBuscarMembresiasSuscripcion_PorIdSuscripcionPasarela", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@IdSuscripcionPasarela", System.Data.SqlDbType.VarChar,100)).Value = oPaquetes.IdSuscripcionPasarela;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                itemDTO = new PlanesDTO()
                                {
                                    CodigoMembresiasSuscripcion = reader[reader.GetOrdinal("CodigoMembresiasSuscripcion")].ToString(),
                                    CodigoUnidadNegocio = Convert.ToInt32(reader[reader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(reader[reader.GetOrdinal("CodigoSede")]),
                                    DefaultKeyEmpresa = reader[reader.GetOrdinal("DefaultKeyEmpresa")].ToString(),
                                    DefaultKeyUser = reader[reader.GetOrdinal("DefaultKeyUser")].ToString(),
                                    CodigoPlantillaFormaPago = reader[reader.GetOrdinal("CodigoPlantilaFormaPago")].ToString(),
                                    IdClientePasarela = reader[reader.GetOrdinal("IdClientePasarela")].ToString(),
                                    IdSuscripcionPasarela = reader[reader.GetOrdinal("IdSuscripcionPasarela")].ToString(),
                                    Estado = Convert.ToBoolean(reader[reader.GetOrdinal("EstadoSuscripcionPasarela")]),
                                    DataJsonPasarela = reader[reader.GetOrdinal("DataJsonPasarela")].ToString(),
                                    CodigoSocio = Convert.ToInt32(reader[reader.GetOrdinal("CodigoSocio")]),
                                    //CodigoMembresia = Convert.ToInt32(reader[reader.GetOrdinal("CodigoMembresia")]),
                                    CodigoPaquete = Convert.ToInt32(reader[reader.GetOrdinal("CodigoPaquete")]),
                                    NroDocumento = reader[reader.GetOrdinal("NroDocumento")].ToString(),
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;
        }



        //buscar suscriptions by defaultkeyuser
        public List<PlanesDTO> ListMemSuscriptionDkeyUser(PlanesDTO oitem)
        {
            List<PlanesDTO> lista = new List<PlanesDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspBuscarMembresiasSuscripcion_PorDefaultKeyUser", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@DefaultKeyUser", System.Data.SqlDbType.VarChar,100)).Value = oitem.DefaultKeyUser;
           
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                lista.Add(new PlanesDTO()
                                {
                                    CodigoMembresiasSuscripcion = reader[reader.GetOrdinal("CodigoMembresiasSuscripcion")].ToString(),
                                    CodigoUnidadNegocio = Convert.ToInt32(reader[reader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(reader[reader.GetOrdinal("CodigoSede")]),
                                    DefaultKeyEmpresa = reader[reader.GetOrdinal("DefaultKeyEmpresa")].ToString(),
                                    DefaultKeyUser = reader[reader.GetOrdinal("DefaultKeyUser")].ToString(),
                                    CodigoPlantillaFormaPago = reader[reader.GetOrdinal("CodigoPlantilaFormaPago")].ToString(),
                                    IdClientePasarela = reader[reader.GetOrdinal("IdClientePasarela")].ToString(),
                                    IdSuscripcionPasarela = reader[reader.GetOrdinal("IdSuscripcionPasarela")].ToString(),
                                    Estado = Convert.ToBoolean(reader[reader.GetOrdinal("EstadoSuscripcionPasarela")]),
                                    DataJsonPasarela = reader[reader.GetOrdinal("DataJsonPasarela")].ToString(),
                                    CodigoSocio = Convert.ToInt32(reader[reader.GetOrdinal("CodigoSocio")]),
                                    //CodigoMembresia = Convert.ToInt32(reader[reader.GetOrdinal("CodigoMembresia")]),
                                    CodigoPaquete = Convert.ToInt32(reader[reader.GetOrdinal("CodigoPaquete")]),
                                    NroDocumento = reader[reader.GetOrdinal("NroDocumento")].ToString(),
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }



        //********************************************************** SUSCRIPCIONES ****************************************************

    }
}
