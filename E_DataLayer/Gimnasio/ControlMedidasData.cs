using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using E_DataModel.Gimnasio;
using E_DataModel.Common;
using System.Data.SqlClient;

namespace E_DataLayer.Gimnasio
{
	public class ControlMedidasData
	{

        public List<ControlMedidasDTO> uspListarControlMedidasSinRutina_Paginacion(ControlMedidasDTO oControlMedidasDTO, Paging paging)
        {
            List<ControlMedidasDTO> lista = new List<ControlMedidasDTO>();
            
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarControlMedidasSinRutina_Paginacion", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oControlMedidasDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oControlMedidasDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar)).Value = oControlMedidasDTO.UsuarioCreacion;
                    cmd.Parameters.Add(new SqlParameter("@Sexo", System.Data.SqlDbType.VarChar,2)).Value = oControlMedidasDTO.Genero_Cliente;
                    cmd.Parameters.Add(new SqlParameter("@EdadRango1", System.Data.SqlDbType.Int)).Value = oControlMedidasDTO.EdadRango1_Cliente;

                    cmd.Parameters.Add(new SqlParameter("@EdadRango2", System.Data.SqlDbType.Int)).Value = oControlMedidasDTO.EdadRango2_Cliente;
                    cmd.Parameters.Add(new SqlParameter("@Nombre", System.Data.SqlDbType.VarChar,100)).Value = oControlMedidasDTO.Nombres_Cliente;
                    cmd.Parameters.Add(new SqlParameter("@Apellidos", System.Data.SqlDbType.VarChar,100)).Value = oControlMedidasDTO.Apellidos_Cliente;
                    cmd.Parameters.Add(new SqlParameter("@Codigo", System.Data.SqlDbType.Int)).Value = oControlMedidasDTO.CodigoCliente;
                    cmd.Parameters.Add(new SqlParameter("@Dni", System.Data.SqlDbType.VarChar,100)).Value = oControlMedidasDTO.DNI_Cliente;

                    cmd.Parameters.Add(new SqlParameter("@Telefono", System.Data.SqlDbType.VarChar,100)).Value = oControlMedidasDTO.Telefono_Cliente;
                    cmd.Parameters.Add(new SqlParameter("@Celular", System.Data.SqlDbType.VarChar,100)).Value = oControlMedidasDTO.Celular_Cliente;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = oControlMedidasDTO.FechaInicio_Cliente;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oControlMedidasDTO.FechaFin_Cliente;
                    
                    cmd.Parameters.Add(new SqlParameter("@PaginaActual", System.Data.SqlDbType.Int)).Value = paging.PageNumber;
                    cmd.Parameters.Add(new SqlParameter("@TamanioPagina", System.Data.SqlDbType.Int)).Value = paging.PageRecords;
                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ControlMedidasDTO()
                                {                                 
                                    CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    CodigoCliente = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoCliente")]),
                                    Nombres_Cliente = oIDataReader[oIDataReader.GetOrdinal("Nombres")].ToString(),
                                    Apellidos_Cliente = oIDataReader[oIDataReader.GetOrdinal("Apellidos")].ToString(),
                                    Genero_Cliente = oIDataReader[oIDataReader.GetOrdinal("Genero")].ToString(),
                                    DNI_Cliente = oIDataReader[oIDataReader.GetOrdinal("DNI")].ToString(),
                                    Celular_Cliente = oIDataReader[oIDataReader.GetOrdinal("Celular")].ToString(),
                                    DescFechaCreacion =  oIDataReader[oIDataReader.GetOrdinal("DesFechaVencimiento")].ToString(),
                                    EstadoCelular = oIDataReader[oIDataReader.GetOrdinal("Celular")].ToString() == string.Empty ? "none": "block",
                                    Correo_Cliente = oIDataReader[oIDataReader.GetOrdinal("Correo")].ToString(),
                                    FechaInicio_Cliente = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaInicio")]),
                                    FechaFin_Cliente = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaFin")]),
                                    TiempoMembresia_Cliente = oIDataReader[oIDataReader.GetOrdinal("TiempoMembresia")].ToString()
                                });

                            }
                        }

                    }
                }
            }
            return lista;

        }

        public ControlMedidasDTO uspListarControlMedidasSinRutina_NumeroRegistros(ControlMedidasDTO oControlMedidasDTO)
        {
            ControlMedidasDTO itemDTO = new ControlMedidasDTO();
            
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarControlMedidasSinRutina_NumeroRegistros", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oControlMedidasDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oControlMedidasDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar)).Value = oControlMedidasDTO.UsuarioCreacion;
                    cmd.Parameters.Add(new SqlParameter("@Sexo", System.Data.SqlDbType.VarChar, 2)).Value = oControlMedidasDTO.Genero_Cliente;
                    cmd.Parameters.Add(new SqlParameter("@EdadRango1", System.Data.SqlDbType.Int)).Value = oControlMedidasDTO.EdadRango1_Cliente;

                    cmd.Parameters.Add(new SqlParameter("@EdadRango2", System.Data.SqlDbType.Int)).Value = oControlMedidasDTO.EdadRango2_Cliente;
                    cmd.Parameters.Add(new SqlParameter("@Nombre", System.Data.SqlDbType.VarChar, 100)).Value = oControlMedidasDTO.Nombres_Cliente;
                    cmd.Parameters.Add(new SqlParameter("@Apellidos", System.Data.SqlDbType.VarChar, 100)).Value = oControlMedidasDTO.Apellidos_Cliente;
                    cmd.Parameters.Add(new SqlParameter("@Codigo", System.Data.SqlDbType.Int)).Value = oControlMedidasDTO.CodigoCliente;
                    cmd.Parameters.Add(new SqlParameter("@Dni", System.Data.SqlDbType.VarChar, 100)).Value = oControlMedidasDTO.DNI_Cliente;

                    cmd.Parameters.Add(new SqlParameter("@Telefono", System.Data.SqlDbType.VarChar, 100)).Value = oControlMedidasDTO.Telefono_Cliente;
                    cmd.Parameters.Add(new SqlParameter("@Celular", System.Data.SqlDbType.VarChar, 100)).Value = oControlMedidasDTO.Celular_Cliente;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = oControlMedidasDTO.FechaInicio_Cliente;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oControlMedidasDTO.FechaFin_Cliente;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new ControlMedidasDTO()
                                {
                                    NumeroRegistros = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("NumeroRegistros")])
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;            
        }

        public List<ControlMedidasDTO> uspListarControlMedidasRenovaciones_Paginacion(ControlMedidasDTO oControlMedidasDTO, Paging paging)
        {
            List<ControlMedidasDTO> lista = new List<ControlMedidasDTO>();
            
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarControlMedidasRenovaciones_Paginacion", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oControlMedidasDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oControlMedidasDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar)).Value = oControlMedidasDTO.UsuarioCreacion;
                    cmd.Parameters.Add(new SqlParameter("@Sexo", System.Data.SqlDbType.VarChar, 2)).Value = oControlMedidasDTO.Genero_Cliente;
                    cmd.Parameters.Add(new SqlParameter("@EdadRango1", System.Data.SqlDbType.Int)).Value = oControlMedidasDTO.EdadRango1_Cliente;

                    cmd.Parameters.Add(new SqlParameter("@EdadRango2", System.Data.SqlDbType.Int)).Value = oControlMedidasDTO.EdadRango2_Cliente;
                    cmd.Parameters.Add(new SqlParameter("@Nombre", System.Data.SqlDbType.VarChar, 100)).Value = oControlMedidasDTO.Nombres_Cliente;
                    cmd.Parameters.Add(new SqlParameter("@Apellidos", System.Data.SqlDbType.VarChar, 100)).Value = oControlMedidasDTO.Apellidos_Cliente;
                    cmd.Parameters.Add(new SqlParameter("@Codigo", System.Data.SqlDbType.Int)).Value = oControlMedidasDTO.CodigoCliente;
                    cmd.Parameters.Add(new SqlParameter("@Dni", System.Data.SqlDbType.VarChar, 100)).Value = oControlMedidasDTO.DNI_Cliente;

                    cmd.Parameters.Add(new SqlParameter("@Telefono", System.Data.SqlDbType.VarChar, 100)).Value = oControlMedidasDTO.Telefono_Cliente;
                    cmd.Parameters.Add(new SqlParameter("@Celular", System.Data.SqlDbType.VarChar, 100)).Value = oControlMedidasDTO.Celular_Cliente;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = oControlMedidasDTO.FechaInicio_Cliente;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oControlMedidasDTO.FechaFin_Cliente;

                    cmd.Parameters.Add(new SqlParameter("@PaginaActual", System.Data.SqlDbType.Int)).Value = paging.PageNumber;
                    cmd.Parameters.Add(new SqlParameter("@TamanioPagina", System.Data.SqlDbType.Int)).Value = paging.PageRecords;
                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ControlMedidasDTO()
                                {
                                    CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    CodigoCliente = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoCliente")]),
                                    Nombres_Cliente = oIDataReader[oIDataReader.GetOrdinal("Nombres")].ToString(),
                                    Apellidos_Cliente = oIDataReader[oIDataReader.GetOrdinal("Apellidos")].ToString(),
                                    Genero_Cliente = oIDataReader[oIDataReader.GetOrdinal("Genero")].ToString(),
                                    DNI_Cliente = oIDataReader[oIDataReader.GetOrdinal("DNI")].ToString(),
                                    Celular_Cliente = oIDataReader[oIDataReader.GetOrdinal("Celular")].ToString(),
                                    DescFechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaVencimiento")]).ToString("dd/MM/yyy"),
                                    EstadoCelular = oIDataReader[oIDataReader.GetOrdinal("Celular")].ToString() == string.Empty ? "none" : "block",
                                    Correo_Cliente = oIDataReader[oIDataReader.GetOrdinal("Correo")].ToString(),
                                    FechaInicio_Cliente = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaInicio")]),
                                    FechaFin_Cliente = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaFin")]),
                                    TiempoMembresia_Cliente = oIDataReader[oIDataReader.GetOrdinal("TiempoMembresia")].ToString(),
                                    Seguimiento_Cliente = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Seguimiento")])
                                });
                                
                            }
                        }

                    }
                }
            }
            return lista;
            
        }

        public ControlMedidasDTO uspListarControlMedidasRenovaciones_NumeroRegistros(ControlMedidasDTO oControlMedidasDTO)
        {
            ControlMedidasDTO itemDTO = new ControlMedidasDTO();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarControlMedidasRenovaciones_NumeroRegistros", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oControlMedidasDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oControlMedidasDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar)).Value = oControlMedidasDTO.UsuarioCreacion;
                    cmd.Parameters.Add(new SqlParameter("@Sexo", System.Data.SqlDbType.VarChar, 2)).Value = oControlMedidasDTO.Genero_Cliente;
                    cmd.Parameters.Add(new SqlParameter("@EdadRango1", System.Data.SqlDbType.Int)).Value = oControlMedidasDTO.EdadRango1_Cliente;

                    cmd.Parameters.Add(new SqlParameter("@EdadRango2", System.Data.SqlDbType.Int)).Value = oControlMedidasDTO.EdadRango2_Cliente;
                    cmd.Parameters.Add(new SqlParameter("@Nombre", System.Data.SqlDbType.VarChar, 100)).Value = oControlMedidasDTO.Nombres_Cliente;
                    cmd.Parameters.Add(new SqlParameter("@Apellidos", System.Data.SqlDbType.VarChar, 100)).Value = oControlMedidasDTO.Apellidos_Cliente;
                    cmd.Parameters.Add(new SqlParameter("@Codigo", System.Data.SqlDbType.Int)).Value = oControlMedidasDTO.CodigoCliente;
                    cmd.Parameters.Add(new SqlParameter("@Dni", System.Data.SqlDbType.VarChar, 100)).Value = oControlMedidasDTO.DNI_Cliente;

                    cmd.Parameters.Add(new SqlParameter("@Telefono", System.Data.SqlDbType.VarChar, 100)).Value = oControlMedidasDTO.Telefono_Cliente;
                    cmd.Parameters.Add(new SqlParameter("@Celular", System.Data.SqlDbType.VarChar, 100)).Value = oControlMedidasDTO.Celular_Cliente;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = oControlMedidasDTO.FechaInicio_Cliente;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oControlMedidasDTO.FechaFin_Cliente;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new ControlMedidasDTO()
                                {
                                    NumeroRegistros = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("NumeroRegistros")])
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;            
        }
        
        public List<ControlMedidasDTO> uspListarControlMedidasInactivas_Paginacion(ControlMedidasDTO oControlMedidasDTO, Paging paging)
        {
            List<ControlMedidasDTO> lista = new List<ControlMedidasDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarControlMedidasInactivas_Paginacion", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oControlMedidasDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oControlMedidasDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar)).Value = oControlMedidasDTO.UsuarioCreacion;
                    cmd.Parameters.Add(new SqlParameter("@Sexo", System.Data.SqlDbType.VarChar, 2)).Value = oControlMedidasDTO.Genero_Cliente;
                    cmd.Parameters.Add(new SqlParameter("@EdadRango1", System.Data.SqlDbType.Int)).Value = oControlMedidasDTO.EdadRango1_Cliente;

                    cmd.Parameters.Add(new SqlParameter("@EdadRango2", System.Data.SqlDbType.Int)).Value = oControlMedidasDTO.EdadRango2_Cliente;
                    cmd.Parameters.Add(new SqlParameter("@Nombre", System.Data.SqlDbType.VarChar, 100)).Value = oControlMedidasDTO.Nombres_Cliente;
                    cmd.Parameters.Add(new SqlParameter("@Apellidos", System.Data.SqlDbType.VarChar, 100)).Value = oControlMedidasDTO.Apellidos_Cliente;
                    cmd.Parameters.Add(new SqlParameter("@Codigo", System.Data.SqlDbType.Int)).Value = oControlMedidasDTO.CodigoCliente;
                    cmd.Parameters.Add(new SqlParameter("@Dni", System.Data.SqlDbType.VarChar, 100)).Value = oControlMedidasDTO.DNI_Cliente;

                    cmd.Parameters.Add(new SqlParameter("@Telefono", System.Data.SqlDbType.VarChar, 100)).Value = oControlMedidasDTO.Telefono_Cliente;
                    cmd.Parameters.Add(new SqlParameter("@Celular", System.Data.SqlDbType.VarChar, 100)).Value = oControlMedidasDTO.Celular_Cliente;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = oControlMedidasDTO.FechaInicio_Cliente;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oControlMedidasDTO.FechaFin_Cliente;

                    cmd.Parameters.Add(new SqlParameter("@PaginaActual", System.Data.SqlDbType.Int)).Value = paging.PageNumber;
                    cmd.Parameters.Add(new SqlParameter("@TamanioPagina", System.Data.SqlDbType.Int)).Value = paging.PageRecords;
                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ControlMedidasDTO()
                                {
                                    CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    CodigoCliente = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoCliente")]),
                                    Nombres_Cliente = oIDataReader[oIDataReader.GetOrdinal("Nombres")].ToString(),
                                    Apellidos_Cliente = oIDataReader[oIDataReader.GetOrdinal("Apellidos")].ToString(),
                                    Genero_Cliente = oIDataReader[oIDataReader.GetOrdinal("Genero")].ToString(),
                                    DNI_Cliente = oIDataReader[oIDataReader.GetOrdinal("DNI")].ToString(),
                                    Celular_Cliente = oIDataReader[oIDataReader.GetOrdinal("Celular")].ToString(),
                                    DescFechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaVencimiento")]).ToString("dd/MM/yyy"),
                                    EstadoCelular = oIDataReader[oIDataReader.GetOrdinal("Celular")].ToString() == string.Empty ? "none" : "block",
                                    Correo_Cliente = oIDataReader[oIDataReader.GetOrdinal("Correo")].ToString(),

                                    FechaInicio_Cliente = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaInicio")]),
                                    FechaFin_Cliente = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaFin")]),
                                    TiempoMembresia_Cliente = oIDataReader[oIDataReader.GetOrdinal("TiempoMembresia")].ToString(),
                                    Seguimiento_Cliente = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Seguimiento")])
                                });

                            }
                        }

                    }
                }
            }
            return lista;            
        }

        public ControlMedidasDTO uspListarControlMedidasInactivas_NumeroRegistros(ControlMedidasDTO oControlMedidasDTO)
        {
            ControlMedidasDTO itemDTO = new ControlMedidasDTO();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarControlMedidasInactivas_NumeroRegistros", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oControlMedidasDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oControlMedidasDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar)).Value = oControlMedidasDTO.UsuarioCreacion;
                    cmd.Parameters.Add(new SqlParameter("@Sexo", System.Data.SqlDbType.VarChar, 2)).Value = oControlMedidasDTO.Genero_Cliente;
                    cmd.Parameters.Add(new SqlParameter("@EdadRango1", System.Data.SqlDbType.Int)).Value = oControlMedidasDTO.EdadRango1_Cliente;

                    cmd.Parameters.Add(new SqlParameter("@EdadRango2", System.Data.SqlDbType.Int)).Value = oControlMedidasDTO.EdadRango2_Cliente;
                    cmd.Parameters.Add(new SqlParameter("@Nombre", System.Data.SqlDbType.VarChar, 100)).Value = oControlMedidasDTO.Nombres_Cliente;
                    cmd.Parameters.Add(new SqlParameter("@Apellidos", System.Data.SqlDbType.VarChar, 100)).Value = oControlMedidasDTO.Apellidos_Cliente;
                    cmd.Parameters.Add(new SqlParameter("@Codigo", System.Data.SqlDbType.Int)).Value = oControlMedidasDTO.CodigoCliente;
                    cmd.Parameters.Add(new SqlParameter("@Dni", System.Data.SqlDbType.VarChar, 100)).Value = oControlMedidasDTO.DNI_Cliente;

                    cmd.Parameters.Add(new SqlParameter("@Telefono", System.Data.SqlDbType.VarChar, 100)).Value = oControlMedidasDTO.Telefono_Cliente;
                    cmd.Parameters.Add(new SqlParameter("@Celular", System.Data.SqlDbType.VarChar, 100)).Value = oControlMedidasDTO.Celular_Cliente;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = oControlMedidasDTO.FechaInicio_Cliente;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oControlMedidasDTO.FechaFin_Cliente;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new ControlMedidasDTO()
                                {
                                    NumeroRegistros = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("NumeroRegistros")])
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;
        }

        public List<ControlMedidasDTO> uspListarControlMedidasActivas_Paginacion(ControlMedidasDTO oControlMedidasDTO, Paging paging)
        {
            List<ControlMedidasDTO> lista = new List<ControlMedidasDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarControlMedidasActivas_Paginacion", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oControlMedidasDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oControlMedidasDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar)).Value = oControlMedidasDTO.UsuarioCreacion;
                    cmd.Parameters.Add(new SqlParameter("@Sexo", System.Data.SqlDbType.VarChar, 2)).Value = oControlMedidasDTO.Genero_Cliente;
                    cmd.Parameters.Add(new SqlParameter("@EdadRango1", System.Data.SqlDbType.Int)).Value = oControlMedidasDTO.EdadRango1_Cliente;

                    cmd.Parameters.Add(new SqlParameter("@EdadRango2", System.Data.SqlDbType.Int)).Value = oControlMedidasDTO.EdadRango2_Cliente;
                    cmd.Parameters.Add(new SqlParameter("@Nombre", System.Data.SqlDbType.VarChar, 100)).Value = oControlMedidasDTO.Nombres_Cliente;
                    cmd.Parameters.Add(new SqlParameter("@Apellidos", System.Data.SqlDbType.VarChar, 100)).Value = oControlMedidasDTO.Apellidos_Cliente;
                    cmd.Parameters.Add(new SqlParameter("@Codigo", System.Data.SqlDbType.Int)).Value = oControlMedidasDTO.CodigoCliente;
                    cmd.Parameters.Add(new SqlParameter("@Dni", System.Data.SqlDbType.VarChar, 100)).Value = oControlMedidasDTO.DNI_Cliente;

                    cmd.Parameters.Add(new SqlParameter("@Telefono", System.Data.SqlDbType.VarChar, 100)).Value = oControlMedidasDTO.Telefono_Cliente;
                    cmd.Parameters.Add(new SqlParameter("@Celular", System.Data.SqlDbType.VarChar, 100)).Value = oControlMedidasDTO.Celular_Cliente;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = oControlMedidasDTO.FechaInicio_Cliente;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oControlMedidasDTO.FechaFin_Cliente;

                    cmd.Parameters.Add(new SqlParameter("@PaginaActual", System.Data.SqlDbType.Int)).Value = paging.PageNumber;
                    cmd.Parameters.Add(new SqlParameter("@TamanioPagina", System.Data.SqlDbType.Int)).Value = paging.PageRecords;
                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ControlMedidasDTO()
                                {
                                    CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    CodigoCliente = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoCliente")]),
                                    Nombres_Cliente = oIDataReader[oIDataReader.GetOrdinal("Nombres")].ToString(),
                                    Apellidos_Cliente = oIDataReader[oIDataReader.GetOrdinal("Apellidos")].ToString(),
                                    Genero_Cliente = oIDataReader[oIDataReader.GetOrdinal("Genero")].ToString(),
                                    DNI_Cliente = oIDataReader[oIDataReader.GetOrdinal("DNI")].ToString(),
                                    Celular_Cliente = oIDataReader[oIDataReader.GetOrdinal("Celular")].ToString(),
                                    DescFechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaVencimiento")]).ToString("dd/MM/yyy"),
                                    EstadoCelular = oIDataReader[oIDataReader.GetOrdinal("Celular")].ToString() == string.Empty ? "none" : "block",
                                    Correo_Cliente = oIDataReader[oIDataReader.GetOrdinal("Correo")].ToString(),
                                    FechaInicio_Cliente = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaInicio")]),
                                    FechaFin_Cliente = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaFin")]),
                                    TiempoMembresia_Cliente = oIDataReader[oIDataReader.GetOrdinal("TiempoMembresia")].ToString(),
                                    Seguimiento_Cliente = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Seguimiento")])
                                });

                            }
                        }

                    }
                }
            }
            return lista;            
        }

        public ControlMedidasDTO uspListarControlMedidasActivas_NumeroRegistros(ControlMedidasDTO oControlMedidasDTO)
        {
            ControlMedidasDTO itemDTO = new ControlMedidasDTO();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarControlMedidasActivas_NumeroRegistros", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oControlMedidasDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oControlMedidasDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar)).Value = oControlMedidasDTO.UsuarioCreacion;
                    cmd.Parameters.Add(new SqlParameter("@Sexo", System.Data.SqlDbType.VarChar, 2)).Value = oControlMedidasDTO.Genero_Cliente;
                    cmd.Parameters.Add(new SqlParameter("@EdadRango1", System.Data.SqlDbType.Int)).Value = oControlMedidasDTO.EdadRango1_Cliente;

                    cmd.Parameters.Add(new SqlParameter("@EdadRango2", System.Data.SqlDbType.Int)).Value = oControlMedidasDTO.EdadRango2_Cliente;
                    cmd.Parameters.Add(new SqlParameter("@Nombre", System.Data.SqlDbType.VarChar, 100)).Value = oControlMedidasDTO.Nombres_Cliente;
                    cmd.Parameters.Add(new SqlParameter("@Apellidos", System.Data.SqlDbType.VarChar, 100)).Value = oControlMedidasDTO.Apellidos_Cliente;
                    cmd.Parameters.Add(new SqlParameter("@Codigo", System.Data.SqlDbType.Int)).Value = oControlMedidasDTO.CodigoCliente;
                    cmd.Parameters.Add(new SqlParameter("@Dni", System.Data.SqlDbType.VarChar, 100)).Value = oControlMedidasDTO.DNI_Cliente;

                    cmd.Parameters.Add(new SqlParameter("@Telefono", System.Data.SqlDbType.VarChar, 100)).Value = oControlMedidasDTO.Telefono_Cliente;
                    cmd.Parameters.Add(new SqlParameter("@Celular", System.Data.SqlDbType.VarChar, 100)).Value = oControlMedidasDTO.Celular_Cliente;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = oControlMedidasDTO.FechaInicio_Cliente;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oControlMedidasDTO.FechaFin_Cliente;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new ControlMedidasDTO()
                                {
                                    NumeroRegistros = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("NumeroRegistros")])
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;            
        }
        
        public List<ControlMedidasDTO> uspListarControlMedidas_Paginacion(ControlMedidasDTO oControlMedidasDTO, Paging paging)
        {
            List<ControlMedidasDTO> lista = new List<ControlMedidasDTO>();
            
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarControlMedidas_Paginacion", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oControlMedidasDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oControlMedidasDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoCliente", System.Data.SqlDbType.VarChar)).Value = oControlMedidasDTO.CodigoCliente;

                    cmd.Parameters.Add(new SqlParameter("@PaginaActual", System.Data.SqlDbType.Int)).Value = paging.PageNumber;
                    cmd.Parameters.Add(new SqlParameter("@TamanioPagina", System.Data.SqlDbType.Int)).Value = paging.PageRecords;
                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ControlMedidasDTO()
                                {
                                    CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    CodigoCliente = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoCliente")]),
                                    CodigoMedida = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoMedida")]),
                                    FechaVencimiento = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaVencimiento")]),
                                    TiempoMedicion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("TiempoMedicion")]),
                                    AntecedentesMedicos = oIDataReader[oIDataReader.GetOrdinal("AntecedentesMedicos")].ToString(),
                                    Observacion = oIDataReader[oIDataReader.GetOrdinal("Observacion")].ToString(),
                                    ExpEntrenamiento = oIDataReader[oIDataReader.GetOrdinal("ExpEntrenamiento")].ToString(),
                                    Edad = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Edad")]),
                                    Estatura = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Estatura")]),
                                    PesoCorporal = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("PesoCorporal")]),
                                    PesoGraso = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("PesoGraso")]),
                                    PorcentajeGrasa = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("PorcentajeGrasa")]),
                                    PorcentajeAgua = oIDataReader[oIDataReader.GetOrdinal("PorcentajeAgua")].ToString(),
                                    GrasaVisceral = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("GrasaVisceral")]),
                                    IMC = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("IMC")]),
                                    Cuello = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Cuello")]),
                                    CirdelMom = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("CirdelMom")]),
                                    CirdelTorax = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("CirdelTorax")]),
                                    Cintura = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Cintura")]),
                                    CadA = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("CadA")]),
                                    CadB = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("CadB")]),
                                    MusloSuperior = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("MusloSuperior")]),
                                    MusloBajo = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("MusloBajo")]),
                                    Pantorrilla = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Pantorrilla")]),
                                    BrazoNormal = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("BrazoNormal")]),
                                    BrazoFlexionado = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("BrazoFlexionado")]),
                                    AntreBrazo = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("AntreBrazo")]),
                                    Munieca = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Munieca")]),
                                    Comentario = oIDataReader[oIDataReader.GetOrdinal("Comentario")].ToString(),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString(),
                                    FechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")]), 
                                    strFechaIngreso = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")]).ToString("dd/MM/yyyy"),
                                    strTiempoMedicion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("TiempoMedicion")]).ToString("mm:ss.ff"),
                                    strFechaVencimiento = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaVencimiento")]).ToString("dd/MM/yyyy"),
                                    Gluteos = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Gluteos")])
                                });

                            }
                        }

                    }
                }
            }
            
            return lista;
        }

        //public List<ControlMedidasDTO> uspListarControlMedidas_X_CodigoMedidas(ControlMedidasDTO oControlMedidasDTO, Paging paging)
        //{
        //    List<ControlMedidasDTO> lista = new List<ControlMedidasDTO>();

        //    using (var conn = new SqlConnection(Helper.Conexion()))
        //    {
        //        conn.Open();
        //        using (var cmd = new SqlCommand("uspListarControlMedidas_X_CodigoMedidas", conn))
        //        {
        //            cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //            cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oControlMedidasDTO.CodigoUnidadNegocio;
        //            cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oControlMedidasDTO.CodigoSede;
        //            cmd.Parameters.Add(new SqlParameter("@CodigoCliente", System.Data.SqlDbType.VarChar)).Value = oControlMedidasDTO.CodigoCliente;
        //            cmd.Parameters.Add(new SqlParameter("@CodigoMedida", System.Data.SqlDbType.Int)).Value = oControlMedidasDTO.CodigoMedida;

        //            cmd.Parameters.Add(new SqlParameter("@PaginaActual", System.Data.SqlDbType.Int)).Value = paging.PageNumber;
        //            cmd.Parameters.Add(new SqlParameter("@TamanioPagina", System.Data.SqlDbType.Int)).Value = paging.PageRecords;
        //            cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;

        //            using (SqlDataReader oIDataReader = cmd.ExecuteReader())
        //            {
        //                if (oIDataReader.HasRows)
        //                {
        //                    while (oIDataReader.Read())
        //                    {
        //                        lista.Add(new ControlMedidasDTO()
        //                        {
        //                            CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
        //                            CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
        //                            CodigoCliente = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoCliente")]),
        //                            CodigoMedida = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoMedida")]),
        //                            FechaVencimiento = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaVencimiento")]),
        //                            TiempoMedicion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("TiempoMedicion")]),
        //                            AntecedentesMedicos = oIDataReader[oIDataReader.GetOrdinal("AntecedentesMedicos")].ToString(),
        //                            Observacion = oIDataReader[oIDataReader.GetOrdinal("Observacion")].ToString(),
        //                            ExpEntrenamiento = oIDataReader[oIDataReader.GetOrdinal("ExpEntrenamiento")].ToString(),
        //                            Edad = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Edad")]),
        //                            Estatura = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Estatura")]),
        //                            PesoCorporal = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("PesoCorporal")]),
        //                            PesoGraso = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("PesoGraso")]),
        //                            PorcentajeGrasa = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("PorcentajeGrasa")]),
        //                            PorcentajeAgua = oIDataReader[oIDataReader.GetOrdinal("PorcentajeAgua")].ToString(),
        //                            GrasaVisceral = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("GrasaVisceral")]),
        //                            IMC = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("IMC")]),
        //                            Cuello = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Cuello")]),
        //                            CirdelMom = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("CirdelMom")]),
        //                            CirdelTorax = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("CirdelTorax")]),
        //                            Cintura = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Cintura")]),
        //                            CadA = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("CadA")]),
        //                            CadB = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("CadB")]),
        //                            MusloSuperior = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("MusloSuperior")]),
        //                            MusloBajo = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("MusloBajo")]),
        //                            Pantorrilla = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Pantorrilla")]),
        //                            BrazoNormal = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("BrazoNormal")]),
        //                            BrazoFlexionado = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("BrazoFlexionado")]),
        //                            AntreBrazo = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("AntreBrazo")]),
        //                            Munieca = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Munieca")]),
        //                            Comentario = oIDataReader[oIDataReader.GetOrdinal("Comentario")].ToString(),
        //                            UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString(),
        //                            FechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")]),
        //                            strFechaIngreso = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")]).ToString("dd/MM/yyyy"),
        //                            strTiempoMedicion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("TiempoMedicion")]).ToString("mm:ss.ff"),
        //                            strFechaVencimiento = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaVencimiento")]).ToString("dd/MM/yyyy"),
        //                            Gluteos = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Gluteos")])
        //                        });

        //                    }
        //                }

        //            }
        //        }
        //    }

        //    return lista;
        //}
        public ControlMedidasDTO uspListarControlMedidas_NumeroRegistros(ControlMedidasDTO oControlMedidasDTO)
        {
            ControlMedidasDTO itemDTO = new ControlMedidasDTO();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarControlMedidas_NumeroRegistros", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oControlMedidasDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oControlMedidasDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoCliente", System.Data.SqlDbType.VarChar)).Value = oControlMedidasDTO.CodigoCliente;
                    
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new ControlMedidasDTO()
                                {
                                    NumeroRegistros = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("NumeroRegistros")])
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;            
        }
      
        public List<ControlMedidasDTO> uspListarAgendaNutricionalGeneralHistorial_Paginacion(ControlMedidasDTO oControlMedidasDTO, Paging paging)
        {
            List<ControlMedidasDTO> lista = new List<ControlMedidasDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarAgendaNutricionalGeneralHistorial_Paginacion", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oControlMedidasDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oControlMedidasDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = oControlMedidasDTO.UsuarioCreacion;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = oControlMedidasDTO.FechaInicio_Filtro;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oControlMedidasDTO.FechaFin_Filtro;
                    cmd.Parameters.Add(new SqlParameter("@Buscador", System.Data.SqlDbType.VarChar)).Value = oControlMedidasDTO.Buscador;

                    cmd.Parameters.Add(new SqlParameter("@PaginaActual", System.Data.SqlDbType.Int)).Value = paging.PageNumber;
                    cmd.Parameters.Add(new SqlParameter("@TamanioPagina", System.Data.SqlDbType.Int)).Value = paging.PageRecords;
                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ControlMedidasDTO()
                                {                                 
                                    CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    CodigoSocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSocio")]),
                                    Comentario = oIDataReader[oIDataReader.GetOrdinal("Comentario")].ToString(),
                                    Observacion = oIDataReader[oIDataReader.GetOrdinal("Observacion")].ToString(),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString(),
                                    FechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")]),
                                    Nombres = oIDataReader[oIDataReader.GetOrdinal("Nombres")].ToString(),
                                    Apellidos = oIDataReader[oIDataReader.GetOrdinal("Apellidos")].ToString(),
                                    Celular = oIDataReader[oIDataReader.GetOrdinal("Celular")].ToString(),
                                    Correo = oIDataReader[oIDataReader.GetOrdinal("Correo")].ToString(),
                                    EstadoCelular = oIDataReader[oIDataReader.GetOrdinal("Celular")].ToString() == "" ? "none" : "block"
                                });

                            }
                        }

                    }
                }
            }

            return lista;
        }

        public ControlMedidasDTO uspListarAgendaNutricionalGeneralHistorial_NumeroRegistros(ControlMedidasDTO oControlMedidasDTO)
        {
            ControlMedidasDTO itemDTO = new ControlMedidasDTO();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarAgendaNutricionalGeneralHistorial_NumeroRegistros", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oControlMedidasDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oControlMedidasDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = oControlMedidasDTO.UsuarioCreacion;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = oControlMedidasDTO.FechaInicio_Filtro;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oControlMedidasDTO.FechaFin_Filtro;
                    cmd.Parameters.Add(new SqlParameter("@Buscador", System.Data.SqlDbType.VarChar)).Value = oControlMedidasDTO.Buscador;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                itemDTO = new ControlMedidasDTO()
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


        public ControlMedidasDTO uspBuscarControlMedidasPorCodigo(ControlMedidasDTO oControlMedidas)
		{
            ControlMedidasDTO itemDTO = new ControlMedidasDTO();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspBuscarControlMedidasPorCodigo", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oControlMedidas.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oControlMedidas.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoCliente", System.Data.SqlDbType.VarChar)).Value = oControlMedidas.CodigoCliente;
                    cmd.Parameters.Add(new SqlParameter("@CodigoMedida", System.Data.SqlDbType.VarChar)).Value = oControlMedidas.CodigoMedida;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new ControlMedidasDTO()
                                {
                                    CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    CodigoCliente = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoCliente")]),
                                    CodigoObjetivo = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoObjetivo")]),
                                    FechaVencimiento = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaVencimiento")]),
                                    TiempoMedicion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("TiempoMedicion")]),
                                    AntecedentesMedicos = oIDataReader[oIDataReader.GetOrdinal("AntecedentesMedicos")].ToString(),
                                    Observacion = oIDataReader[oIDataReader.GetOrdinal("Observacion")].ToString(),
                                    ExpEntrenamiento = oIDataReader[oIDataReader.GetOrdinal("ExpEntrenamiento")].ToString(),
                                    Edad = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Edad")]),
                                    Estatura = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Estatura")]),
                                    PesoCorporal = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("PesoCorporal")]),
                                    PesoGraso = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("PesoGraso")]),
                                    PorcentajeGrasa = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("PorcentajeGrasa")]),
                                    IMC = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("IMC")]),
                                    Cuello = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Cuello")]),
                                    CirdelMom = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("CirdelMom")]),
                                    CirdelTorax = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("CirdelTorax")]),
                                    Cintura = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Cintura")]),
                                    CadA = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("CadA")]),
                                    CadB = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("CadB")]),
                                    MusloSuperior = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("MusloSuperior")]),
                                    MusloBajo = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("MusloBajo")]),
                                    Pantorrilla = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Pantorrilla")]),
                                    BrazoNormal = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("BrazoNormal")]),
                                    BrazoFlexionado = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("BrazoFlexionado")]),
                                    AntreBrazo = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("AntreBrazo")]),
                                    Munieca = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Munieca")]),
                                    Comentario = oIDataReader[oIDataReader.GetOrdinal("Comentario")].ToString(),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString(),
                                    FechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")]),
                                    Gluteos = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Gluteos")])
                               
                            };
                            }
                        }
                    }
                }
            }
            return itemDTO;           
		}
		
		public void Registrar(ControlMedidasDTO item)
		{
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspRegistrarControlMedidas", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoCliente", System.Data.SqlDbType.Int)).Value = item.CodigoCliente;
                    cmd.Parameters.Add(new SqlParameter("@CodigoObjetivo", System.Data.SqlDbType.Int)).Value = item.CodigoObjetivo;
                    cmd.Parameters.Add(new SqlParameter("@CodigoMedida", System.Data.SqlDbType.Int)).Value = item.CodigoMedida;
                    cmd.Parameters.Add(new SqlParameter("@FechaVencimiento", System.Data.SqlDbType.DateTime)).Value = item.FechaVencimiento;

                    cmd.Parameters.Add(new SqlParameter("@TiempoMedicion", System.Data.SqlDbType.DateTime)).Value = item.TiempoMedicion;
                    cmd.Parameters.Add(new SqlParameter("@AntecedentesMedicos", System.Data.SqlDbType.VarChar,100)).Value = item.AntecedentesMedicos;
                    cmd.Parameters.Add(new SqlParameter("@Observacion", System.Data.SqlDbType.VarChar,100)).Value = item.Observacion;
                    cmd.Parameters.Add(new SqlParameter("@ExpEntrenamiento", System.Data.SqlDbType.VarChar,100)).Value = item.ExpEntrenamiento;
                    cmd.Parameters.Add(new SqlParameter("@Edad", System.Data.SqlDbType.Int)).Value = item.Edad;

                    cmd.Parameters.Add(new SqlParameter("@Estatura", System.Data.SqlDbType.Decimal)).Value = item.Estatura;
                    cmd.Parameters.Add(new SqlParameter("@PesoCorporal", System.Data.SqlDbType.Decimal)).Value = item.PesoCorporal;
                    cmd.Parameters.Add(new SqlParameter("@PesoGraso", System.Data.SqlDbType.Decimal)).Value = item.PesoGraso;
                    cmd.Parameters.Add(new SqlParameter("@PorcentajeGrasa", System.Data.SqlDbType.Decimal)).Value = item.PorcentajeGrasa;
                    cmd.Parameters.Add(new SqlParameter("@IMC", System.Data.SqlDbType.Decimal)).Value = item.IMC;

                    cmd.Parameters.Add(new SqlParameter("@Cuello", System.Data.SqlDbType.Decimal)).Value = item.Cuello;
                    cmd.Parameters.Add(new SqlParameter("@CirdelMom", System.Data.SqlDbType.Decimal)).Value = item.CirdelMom;
                    cmd.Parameters.Add(new SqlParameter("@CirdelTorax", System.Data.SqlDbType.Decimal)).Value = item.CirdelTorax;
                    cmd.Parameters.Add(new SqlParameter("@Cintura", System.Data.SqlDbType.Decimal)).Value = item.Cintura;
                    cmd.Parameters.Add(new SqlParameter("@CadA", System.Data.SqlDbType.Decimal)).Value = item.CadA;

                    cmd.Parameters.Add(new SqlParameter("@CadB", System.Data.SqlDbType.Decimal)).Value = item.CadB;
                    cmd.Parameters.Add(new SqlParameter("@MusloSuperior", System.Data.SqlDbType.Decimal)).Value = item.MusloSuperior;
                    cmd.Parameters.Add(new SqlParameter("@MusloBajo", System.Data.SqlDbType.Decimal)).Value = item.MusloBajo;
                    cmd.Parameters.Add(new SqlParameter("@Pantorrilla", System.Data.SqlDbType.Decimal)).Value = item.Pantorrilla;
                    cmd.Parameters.Add(new SqlParameter("@BrazoNormal", System.Data.SqlDbType.Decimal)).Value = item.BrazoNormal;
                    cmd.Parameters.Add(new SqlParameter("@GrasaVisceral", System.Data.SqlDbType.Decimal)).Value = item.GrasaVisceral;

                    cmd.Parameters.Add(new SqlParameter("@BrazoFlexionado", System.Data.SqlDbType.Decimal)).Value = item.BrazoFlexionado;
                    cmd.Parameters.Add(new SqlParameter("@AntreBrazo", System.Data.SqlDbType.Decimal)).Value = item.AntreBrazo;
                    cmd.Parameters.Add(new SqlParameter("@Munieca", System.Data.SqlDbType.Decimal)).Value = item.Munieca;
                    cmd.Parameters.Add(new SqlParameter("@Comentario", System.Data.SqlDbType.VarChar,100)).Value = item.Comentario;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar,100)).Value = item.UsuarioCreacion;
                    cmd.Parameters.Add(new SqlParameter("@Gluteos", System.Data.SqlDbType.Decimal)).Value = item.Gluteos;

                    cmd.Parameters.Add(new SqlParameter("@FechaCreacion", System.Data.SqlDbType.DateTime)).Value = item.FechaCreacion;

                    cmd.ExecuteNonQuery();
                }
            }
            
		}

		public void Actualizar(ControlMedidasDTO item)
		{
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspActualizarControlMedidas", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoCliente", System.Data.SqlDbType.Int)).Value = item.CodigoCliente;
                    cmd.Parameters.Add(new SqlParameter("@CodigoMedida", System.Data.SqlDbType.Int)).Value = item.CodigoMedida;
                    cmd.Parameters.Add(new SqlParameter("@CodigoObjetivo", System.Data.SqlDbType.Int)).Value = item.CodigoObjetivo;

                    cmd.Parameters.Add(new SqlParameter("@FechaVencimiento", System.Data.SqlDbType.DateTime)).Value = item.FechaVencimiento;

                    cmd.Parameters.Add(new SqlParameter("@TiempoMedicion", System.Data.SqlDbType.DateTime)).Value = item.TiempoMedicion;
                    cmd.Parameters.Add(new SqlParameter("@AntecedentesMedicos", System.Data.SqlDbType.VarChar, 100)).Value = item.AntecedentesMedicos;
                    cmd.Parameters.Add(new SqlParameter("@Observacion", System.Data.SqlDbType.VarChar, 100)).Value = item.Observacion;
                    cmd.Parameters.Add(new SqlParameter("@ExpEntrenamiento", System.Data.SqlDbType.VarChar, 100)).Value = item.ExpEntrenamiento;
                    cmd.Parameters.Add(new SqlParameter("@Edad", System.Data.SqlDbType.Int)).Value = item.Edad;

                    cmd.Parameters.Add(new SqlParameter("@Estatura", System.Data.SqlDbType.Decimal)).Value = item.Estatura;
                    cmd.Parameters.Add(new SqlParameter("@PesoCorporal", System.Data.SqlDbType.Decimal)).Value = item.PesoCorporal;
                    cmd.Parameters.Add(new SqlParameter("@PesoGraso", System.Data.SqlDbType.Decimal)).Value = item.PesoGraso;
                    cmd.Parameters.Add(new SqlParameter("@PorcentajeGrasa", System.Data.SqlDbType.Decimal)).Value = item.PorcentajeGrasa;
                    cmd.Parameters.Add(new SqlParameter("@IMC", System.Data.SqlDbType.Decimal)).Value = item.IMC;

                    cmd.Parameters.Add(new SqlParameter("@Cuello", System.Data.SqlDbType.Decimal)).Value = item.Cuello;
                    cmd.Parameters.Add(new SqlParameter("@CirdelMom", System.Data.SqlDbType.Decimal)).Value = item.CirdelMom;
                    cmd.Parameters.Add(new SqlParameter("@CirdelTorax", System.Data.SqlDbType.Decimal)).Value = item.CirdelTorax;
                    cmd.Parameters.Add(new SqlParameter("@Cintura", System.Data.SqlDbType.Decimal)).Value = item.Cintura;
                    cmd.Parameters.Add(new SqlParameter("@CadA", System.Data.SqlDbType.Decimal)).Value = item.CadA;

                    cmd.Parameters.Add(new SqlParameter("@CadB", System.Data.SqlDbType.Decimal)).Value = item.CadB;
                    cmd.Parameters.Add(new SqlParameter("@MusloSuperior", System.Data.SqlDbType.Decimal)).Value = item.MusloSuperior;
                    cmd.Parameters.Add(new SqlParameter("@MusloBajo", System.Data.SqlDbType.Decimal)).Value = item.MusloBajo;
                    cmd.Parameters.Add(new SqlParameter("@Pantorrilla", System.Data.SqlDbType.Decimal)).Value = item.Pantorrilla;
                    cmd.Parameters.Add(new SqlParameter("@BrazoNormal", System.Data.SqlDbType.Decimal)).Value = item.BrazoNormal;

                    cmd.Parameters.Add(new SqlParameter("@BrazoFlexionado", System.Data.SqlDbType.Decimal)).Value = item.BrazoFlexionado;
                    cmd.Parameters.Add(new SqlParameter("@AntreBrazo", System.Data.SqlDbType.Decimal)).Value = item.AntreBrazo;
                    cmd.Parameters.Add(new SqlParameter("@Munieca", System.Data.SqlDbType.Decimal)).Value = item.Munieca;
                    cmd.Parameters.Add(new SqlParameter("@Comentario", System.Data.SqlDbType.VarChar, 100)).Value = item.Comentario;
                    //cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioEdicion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioEdicion;
                    cmd.Parameters.Add(new SqlParameter("@Gluteos", System.Data.SqlDbType.Decimal)).Value = item.Gluteos;
                    cmd.Parameters.Add(new SqlParameter("@GrasaVisceral", System.Data.SqlDbType.Decimal)).Value = item.Gluteos;
                    

                    cmd.ExecuteNonQuery();
                }
            }            
		}

		public void Eliminar(ControlMedidasDTO item)
		{
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspEliminarControlMedidas_Estado", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoCliente", System.Data.SqlDbType.Int)).Value = item.CodigoCliente;
                    cmd.Parameters.Add(new SqlParameter("@CodigoMedida", System.Data.SqlDbType.Int)).Value = item.CodigoMedida;
                    
                    cmd.ExecuteNonQuery();
                }
            }
		}

	}
}
