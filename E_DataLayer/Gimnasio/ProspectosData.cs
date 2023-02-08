
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using E_DataModel.Gimnasio;
using E_DataModel.Common;
using System.Data.SqlClient;

namespace E_DataLayer.Gimnasio
{
	public class ProspectosData
	{

        public List<ProspectosTablaDTO> uspListarProspectosValidadorExisteDNI(ProspectosTablaDTO oProspectosDTO)
        {
            List<ProspectosTablaDTO> lista = new List<ProspectosTablaDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarProspectosValidadorExisteDNI", conn))
                {

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oProspectosDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oProspectosDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@DNI", System.Data.SqlDbType.VarChar, 100)).Value = oProspectosDTO.DNI;
                    
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ProspectosTablaDTO()
                                {
                                    CodigoProspecto = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoProspecto")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    Nombres = oIDataReader[oIDataReader.GetOrdinal("Nombres")].ToString(),
                                    Apellidos = oIDataReader[oIDataReader.GetOrdinal("Apellidos")].ToString(),
                                    DNI = oIDataReader[oIDataReader.GetOrdinal("DNI")].ToString(),
                                    desOrigen = oIDataReader[oIDataReader.GetOrdinal("DesOrigen")].ToString(),
                                    ColorOrigen = oIDataReader[oIDataReader.GetOrdinal("Color")].ToString(),
                                    NombreCompleto = oIDataReader[oIDataReader.GetOrdinal("Nombres")].ToString() + ", " + oIDataReader[oIDataReader.GetOrdinal("Apellidos")].ToString(),                                   
                                    Telefono = oIDataReader[oIDataReader.GetOrdinal("Telefono")].ToString(),
                                    Celular = oIDataReader[oIDataReader.GetOrdinal("Celular")].ToString(),
                                    EstadoCelular = oIDataReader[oIDataReader.GetOrdinal("EstadoCelular")].ToString(),
                                    Vendedor = oIDataReader[oIDataReader.GetOrdinal("Vendedor")].ToString(),
                                    DescFechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")]).ToString("dd/MM/yyyy")
                                });
                            }
                        }

                    }
                }
            }

            return lista;
        }


        public List<ProspectosTablaDTO> uspListarProspectosHistorialEliminadosEnviadosACliente_Paginacion(ProspectosTablaDTO oProspectosDTO, Paging paging)
        {
            List<ProspectosTablaDTO> lista = new List<ProspectosTablaDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarProspectosHistorialEliminadosEnviadosACliente_Paginacion", conn))
                {

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oProspectosDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oProspectosDTO.CodigoSede;        
                    cmd.Parameters.Add(new SqlParameter("@Buscador", System.Data.SqlDbType.VarChar, 200)).Value = oProspectosDTO.Nombres;

                    cmd.Parameters.Add(new SqlParameter("@PaginaActual", System.Data.SqlDbType.Int)).Value = paging.PageNumber;
                    cmd.Parameters.Add(new SqlParameter("@TamanioPagina", System.Data.SqlDbType.Int)).Value = paging.PageRecords;
                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ProspectosTablaDTO()
                                {
                                    CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),                                    
                                    CodigoProspecto = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoProspecto")]),
                                    Nombres = oIDataReader[oIDataReader.GetOrdinal("Nombres")].ToString(),
                                    Apellidos = oIDataReader[oIDataReader.GetOrdinal("Apellidos")].ToString(),
                                    Telefono = oIDataReader[oIDataReader.GetOrdinal("Telefono")].ToString(),
                                    Celular = oIDataReader[oIDataReader.GetOrdinal("Celular")].ToString(),
                                    Genero = oIDataReader[oIDataReader.GetOrdinal("Genero")].ToString(),                                  
                                    Vendedor = oIDataReader[oIDataReader.GetOrdinal("Vendedor")].ToString(),
                                    DNI = oIDataReader[oIDataReader.GetOrdinal("DNI")].ToString(),
                                    CodigoSocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSocio")]),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString(),
                                    DescFechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")]).ToString("dd/MM/yyyy hh:mm tt"),
                                    UsuarioEdicion = oIDataReader[oIDataReader.GetOrdinal("UsuarioEdicion")].ToString(),
                                    DescFechaEdicion = oIDataReader[oIDataReader.GetOrdinal("DesFechaEdicion")].ToString(),
                                    Observacion = oIDataReader[oIDataReader.GetOrdinal("Observacion")].ToString(),
                                });
                            }
                        }

                    }
                }
            }

            return lista;
        }

        public ProspectosTablaDTO uspListarProspectosHistorialEliminadosEnviadosACliente_NumeroRegistro(ProspectosTablaDTO oItem)
        {
            ProspectosTablaDTO itemDTO = new ProspectosTablaDTO();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarProspectosHistorialEliminadosEnviadosACliente_NumeroRegistro", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oItem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oItem.CodigoSede;               
                    cmd.Parameters.Add(new SqlParameter("@Buscador", System.Data.SqlDbType.VarChar, 200)).Value = oItem.Nombres;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                itemDTO = new ProspectosTablaDTO()
                                {
                                    CantidadTotal = Convert.ToInt32(reader[reader.GetOrdinal("CantidadRegistros")])                                 
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;
        }

        public List<ProspectosTablaDTO> UspListarProspectosSinActividadAgendaComercial(ProspectosTablaDTO oProspectosDTO, Paging paging)
        {
            List<ProspectosTablaDTO> lista = new List<ProspectosTablaDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("UspListarProspectosSinActividadAgendaComercial", conn))
                {

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oProspectosDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oProspectosDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@FiltroFechaInicio", System.Data.SqlDbType.DateTime)).Value = oProspectosDTO.FiltroFechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FiltroFechaFin", System.Data.SqlDbType.DateTime)).Value = oProspectosDTO.FiltroFechaFin;
                    cmd.Parameters.Add(new SqlParameter("@Vendedor", System.Data.SqlDbType.VarChar, 200)).Value = oProspectosDTO.Vendedor;
                    cmd.Parameters.Add(new SqlParameter("@Buscador", System.Data.SqlDbType.VarChar, 200)).Value = oProspectosDTO.Nombres;
                  
                    cmd.Parameters.Add(new SqlParameter("@PaginaActual", System.Data.SqlDbType.Int)).Value = paging.PageNumber;
                    cmd.Parameters.Add(new SqlParameter("@TamanioPagina", System.Data.SqlDbType.Int)).Value = paging.PageRecords;
                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;
                    
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ProspectosTablaDTO()
                                {
                                    CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    CodigoOrigen = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoOrigen")]), 
                                    CodigoProspecto = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoProspecto")]),                                    
                                    Nombres = oIDataReader[oIDataReader.GetOrdinal("Nombres")].ToString(),
                                    Apellidos = oIDataReader[oIDataReader.GetOrdinal("Apellidos")].ToString(),
                                    Telefono = oIDataReader[oIDataReader.GetOrdinal("Telefono")].ToString(),
                                    Celular = oIDataReader[oIDataReader.GetOrdinal("Celular")].ToString(),
                                    EstadoCelular = oIDataReader[oIDataReader.GetOrdinal("EstadoCelular")].ToString(),
                                    Precio = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Precio")]),
                                    Vendedor = oIDataReader[oIDataReader.GetOrdinal("Vendedor")].ToString(),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString(),                                    
                                    DescFechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")]).ToString("dd/MM/yyyy hh:mm tt"),
                                    desOrigen = oIDataReader[oIDataReader.GetOrdinal("DesOrigen")].ToString(),
                                    DescripcionCCG = oIDataReader[oIDataReader.GetOrdinal("DesComoConocioGym")].ToString(),
                                    DescripcionSP = oIDataReader[oIDataReader.GetOrdinal("DesObjetivo")].ToString(),
                                    ColorOrigen = oIDataReader[oIDataReader.GetOrdinal("Color")].ToString()
                                });
                            }
                        }

                    }
                }
            }

            return lista;
        }

        public ProspectosTablaDTO UspListarProspectosSinActividadAgendaComercial_NumeroRegistros(ProspectosTablaDTO oItem)
        {
            ProspectosTablaDTO itemDTO = new ProspectosTablaDTO();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("UspListarProspectosSinActividadAgendaComercial_NumeroRegistros", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oItem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oItem.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@FiltroFechaInicio", System.Data.SqlDbType.DateTime)).Value = oItem.FiltroFechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FiltroFechaFin", System.Data.SqlDbType.DateTime)).Value = oItem.FiltroFechaFin;
                    cmd.Parameters.Add(new SqlParameter("@Vendedor", System.Data.SqlDbType.VarChar, 200)).Value = oItem.Vendedor;
                    cmd.Parameters.Add(new SqlParameter("@Buscador", System.Data.SqlDbType.VarChar, 200)).Value = oItem.Nombres;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                itemDTO = new ProspectosTablaDTO()
                                {
                                    CantidadTotal = Convert.ToInt32(reader[reader.GetOrdinal("Cantidad")]),
                                    Precio = Convert.ToDecimal(reader[reader.GetOrdinal("Total")])
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;
        }

        public List<ProspectosTablaDTO> uspListarTablaPropectos_Paginacion(ProspectosTablaDTO oProspectosDTO, Paging paging)
		{
			List<ProspectosTablaDTO> lista = new List<ProspectosTablaDTO>();
            
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarTablaProspectos_Paginacion", conn))
                {
                   
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oProspectosDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Buscador", System.Data.SqlDbType.VarChar,200)).Value = oProspectosDTO.Nombres;
                    cmd.Parameters.Add(new SqlParameter("@Vendedor", System.Data.SqlDbType.VarChar, 200)).Value = oProspectosDTO.Vendedor;
                    cmd.Parameters.Add(new SqlParameter("@FiltroFechaInicio", System.Data.SqlDbType.DateTime)).Value = oProspectosDTO.FiltroFechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FiltroFechaFin", System.Data.SqlDbType.DateTime)).Value = oProspectosDTO.FiltroFechaFin;

                    cmd.Parameters.Add(new SqlParameter("@PaginaActual", System.Data.SqlDbType.Int)).Value = paging.PageNumber;
                    cmd.Parameters.Add(new SqlParameter("@TamanioPagina", System.Data.SqlDbType.Int)).Value = paging.PageRecords;
                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oProspectosDTO.CodigoUnidadNegocio;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ProspectosTablaDTO()
                                {
                                    CodigoProspecto = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoProspecto")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    Nombres = oIDataReader[oIDataReader.GetOrdinal("Nombres")].ToString(),
                                    Apellidos = oIDataReader[oIDataReader.GetOrdinal("Apellidos")].ToString(),
                                    NombreCompleto = oIDataReader[oIDataReader.GetOrdinal("Nombres")].ToString() + ", " + oIDataReader[oIDataReader.GetOrdinal("Apellidos")].ToString(),
                                    Correo = oIDataReader[oIDataReader.GetOrdinal("Correo")].ToString(),
                                    Telefono = oIDataReader[oIDataReader.GetOrdinal("Telefono")].ToString(),
                                    Celular = oIDataReader[oIDataReader.GetOrdinal("Celular")].ToString(),
                                    EstadoCelular =  oIDataReader[oIDataReader.GetOrdinal("EstadoCelular")].ToString(),
                                    Vendedor = oIDataReader[oIDataReader.GetOrdinal("Vendedor")].ToString(),
                                    DescFechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")]).ToString("dd/MM/yyyy hh:mm tt") 
                                });
                            }
                        }

                    }
                }
            }

            return lista;         
		}
        
        public ProspectosTablaDTO uspListarTablaProspectos_NumeroRegistros(ProspectosTablaDTO oItem)
        {
            ProspectosTablaDTO itemDTO = new ProspectosTablaDTO();
            
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarTablaProspectos_NumeroRegistros", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oItem.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Buscador", System.Data.SqlDbType.VarChar, 200)).Value = oItem.Nombres;
                    cmd.Parameters.Add(new SqlParameter("@Vendedor", System.Data.SqlDbType.VarChar, 200)).Value = oItem.Vendedor;
                    cmd.Parameters.Add(new SqlParameter("@FiltroFechaInicio", System.Data.SqlDbType.DateTime)).Value = oItem.FiltroFechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FiltroFechaFin", System.Data.SqlDbType.DateTime)).Value = oItem.FiltroFechaFin;

                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oItem.CodigoUnidadNegocio;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                itemDTO = new ProspectosTablaDTO()
                                {
                                    CantTotal = Convert.ToInt32(reader[reader.GetOrdinal("CantidadRegistros")])
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;          
        }

        public ProspectosTablaDTO uspBuscarClientesProspectosPorCodigo(ProspectosTablaDTO oItem)
        {
            ProspectosTablaDTO itemDTO = null;
          
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspBuscarClientesProspectosPorCodigo", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoCliente", System.Data.SqlDbType.Int)).Value = oItem.CodigoProspecto;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oItem.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.VarChar, 200)).Value = oItem.CodigoUnidadNegocio;
                   
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                itemDTO = new ProspectosTablaDTO()
                                {
                                    Codigo = Convert.ToInt32(reader[reader.GetOrdinal("CodigoCliente")]),
                                    Nombres = reader[reader.GetOrdinal("Nombres")].ToString(),
                                    Apellidos = reader[reader.GetOrdinal("Apellidos")].ToString(),
                                    Telefono = reader[reader.GetOrdinal("Telefono")].ToString(),
                                    Celular = reader[reader.GetOrdinal("Celular")].ToString(),
                                    FechaNacimiento = Convert.ToDateTime(reader[reader.GetOrdinal("FechaNacimiento")]),
                                    desFechaNacimiento = Convert.ToDateTime(reader[reader.GetOrdinal("FechaNacimiento")]).ToString("dd/MM/yyyy"),
                                    DNI = reader[reader.GetOrdinal("DNI")].ToString(),
                                    Correo = reader[reader.GetOrdinal("Correo")].ToString(),
                                    TipoCliente = Convert.ToInt32(reader[reader.GetOrdinal("TipoCliente")]),
                                    Hijos = Convert.ToInt32(reader[reader.GetOrdinal("Hijos")]),
                                    CantHijos = Convert.ToInt32(reader[reader.GetOrdinal("CantHijos")]),
                                    CodigoPaquete = Convert.ToInt32(reader[reader.GetOrdinal("CodigoPaquete")]),
                                    Genero = reader[reader.GetOrdinal("Genero")].ToString(),
                                    CodigoTiempo = Convert.ToInt32(reader[reader.GetOrdinal("CodigoTiempo")]),
                                    Precio = Convert    .ToDecimal(reader[reader.GetOrdinal("Precio")])                                   
                                };
                            }
                        }
                    }
                }
            }           
            return itemDTO;
        }
		
		public int Registrar(ProspectosTablaDTO item)
		{
            int? campoRetorno = 0;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspRegistrarProspectos", conn))
                { 
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Codigo", campoRetorno).Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@CodigoProspecto", System.Data.SqlDbType.Int)).Value = item.CodigoProspecto;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSP", System.Data.SqlDbType.Int)).Value = item.CodigoSP;
                    cmd.Parameters.Add(new SqlParameter("@CodigoAE", System.Data.SqlDbType.Int)).Value = item.CodigoAE;

                    cmd.Parameters.Add(new SqlParameter("@CodigoCCG", System.Data.SqlDbType.Int)).Value = item.CodigoCCG;
                    cmd.Parameters.Add(new SqlParameter("@Nombres", System.Data.SqlDbType.VarChar, 100)).Value = item.Nombres;
                    cmd.Parameters.Add(new SqlParameter("@Apellidos", System.Data.SqlDbType.VarChar,100)).Value = item.Apellidos;
                    cmd.Parameters.Add(new SqlParameter("@Telefono", System.Data.SqlDbType.VarChar,100)).Value = item.Telefono;
                    cmd.Parameters.Add(new SqlParameter("@Celular", System.Data.SqlDbType.VarChar, 20)).Value = item.Celular;

                    cmd.Parameters.Add(new SqlParameter("@Genero", System.Data.SqlDbType.VarChar, 1)).Value = item.Genero;
                    cmd.Parameters.Add(new SqlParameter("@Facebook", System.Data.SqlDbType.VarChar,100)).Value = item.Facebook;
                    cmd.Parameters.Add(new SqlParameter("@CodigoTipoPaquete", System.Data.SqlDbType.Int)).Value = item.CodigoTipoPaquete;
                    cmd.Parameters.Add(new SqlParameter("@CodigoPaquete", System.Data.SqlDbType.Int)).Value = item.CodigoPaquete;
                    cmd.Parameters.Add(new SqlParameter("@Vendedor", System.Data.SqlDbType.VarChar, 100)).Value = item.Vendedor;

                    cmd.Parameters.Add(new SqlParameter("@Estado", System.Data.SqlDbType.Bit)).Value = item.Estado;
                    cmd.Parameters.Add(new SqlParameter("@Correo", System.Data.SqlDbType.VarChar, 100)).Value = item.Correo;
                    cmd.Parameters.Add(new SqlParameter("@Observacion", System.Data.SqlDbType.VarChar,100)).Value = item.Observacion;
                    cmd.Parameters.Add(new SqlParameter("@Ocupacion", System.Data.SqlDbType.VarChar,100)).Value = item.Ocupacion;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;
                    
                    cmd.Parameters.Add(new SqlParameter("@TipoConversion", System.Data.SqlDbType.Int)).Value = item.TipoConversion;
                    cmd.Parameters.Add(new SqlParameter("@FechaNacimiento", System.Data.SqlDbType.DateTime)).Value = item.FechaNacimiento;
                    cmd.Parameters.Add(new SqlParameter("@Hijos", System.Data.SqlDbType.Int)).Value = item.Hijos;
                    cmd.Parameters.Add(new SqlParameter("@CantHijos", System.Data.SqlDbType.Int)).Value = item.CantHijos;
                    cmd.Parameters.Add(new SqlParameter("@DNI", System.Data.SqlDbType.VarChar, 20)).Value = item.DNI;
                    
                    cmd.Parameters.Add(new SqlParameter("@CodigoOrigen", System.Data.SqlDbType.Int)).Value = item.CodigoOrigen;
                    cmd.Parameters.Add(new SqlParameter("@TipoCliente", System.Data.SqlDbType.Int)).Value = item.TipoCliente;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoInicioSesion", System.Data.SqlDbType.Int)).Value = item.CodigoInicioSesion;
                    cmd.Parameters.Add(new SqlParameter("@CodigoTiempo", System.Data.SqlDbType.Int)).Value = item.CodigoTiempo;

                    cmd.Parameters.Add(new SqlParameter("@Precio", System.Data.SqlDbType.Decimal)).Value = item.Precio;
                    //cmd.Parameters.Add(new SqlParameter("@CodigoSocio", System.Data.SqlDbType.Int)).Value = item.CodigoSocio;

                    cmd.ExecuteNonQuery();
                    campoRetorno = Convert.ToInt32(cmd.Parameters["@Codigo"].Value);
                }
            }
            
            return Convert.ToInt32(campoRetorno);
        }

        public int uspActualizarProspectoASocio(ProspectosTablaDTO item)
        {
            int? campoRetorno = 0;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspActualizarProspectoASocio", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CodigoSocio", campoRetorno).Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@CodigoProspecto", System.Data.SqlDbType.Int)).Value = item.CodigoProspecto;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioEdicion", System.Data.SqlDbType.VarChar,100)).Value = item.UsuarioEdicion;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;

                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar,100)).Value = item.UsuarioCreacion;
                    cmd.Parameters.Add(new SqlParameter("@CodigoInicioSesion", System.Data.SqlDbType.VarChar, 100)).Value = item.CodigoInicioSesion;
                    
                    cmd.ExecuteNonQuery();
                    campoRetorno = Convert.ToInt32(cmd.Parameters["@CodigoSocio"].Value);
                }
            }
            return Convert.ToInt32(campoRetorno);
        }

        public int uspActualizarProspectoAInvitado(ProspectosTablaDTO item)
        {
            int? campoRetorno = 0;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspActualizarProspectoAInvitado", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CodigoInvitado", campoRetorno).Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@CodigoProspecto", System.Data.SqlDbType.Int)).Value = item.CodigoProspecto;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioEdicion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioEdicion;
                    cmd.Parameters.Add(new SqlParameter("@Observacion", System.Data.SqlDbType.VarChar,100)).Value = item.Invitado_Observacion;
                    cmd.Parameters.Add(new SqlParameter("@NroDias", System.Data.SqlDbType.Int)).Value = item.Invitado_NroDias;

                    cmd.Parameters.Add(new SqlParameter("@NroDiasActual", System.Data.SqlDbType.Int)).Value = item.Invitado_NroDiasActual;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = item.Invitado_FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = item.Invitado_FechaFin;
                    cmd.Parameters.Add(new SqlParameter("@Codigo_InvitadoPor", System.Data.SqlDbType.Int)).Value = item.Invitado_CodigoInvitadoPor;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar,100)).Value = item.UsuarioCreacion;
                    cmd.Parameters.Add(new SqlParameter("@CodigoInicioSesion", System.Data.SqlDbType.Int)).Value = item.CodigoInicioSesion;
                    cmd.ExecuteNonQuery();

                    campoRetorno = Convert.ToInt32(cmd.Parameters["@CodigoInvitado"].Value);
                }
            }
            
            return Convert.ToInt32(campoRetorno);
        }

		public void Eliminar(ProspectosTablaDTO item)
		{
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspEliminarProspectos", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoProspecto", System.Data.SqlDbType.Int)).Value = item.CodigoProspecto;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar,100)).Value = item.UsuarioCreacion;
                 
                    cmd.ExecuteNonQuery();
                }
            }
		}
	}
}
