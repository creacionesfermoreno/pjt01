
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using E_DataModel.Gimnasio;
using E_DataModel.Common;
using System.Data.SqlClient;

namespace E_DataLayer.Gimnasio
{
	public class LlamadaEntranteData
	{
		
        public List<LlamadaEntranteDTO> uspListarTablaLlamadaEntrante_Paginacion(LlamadaEntranteDTO oitem, Paging paging)
		{
			List<LlamadaEntranteDTO> lista = new List<LlamadaEntranteDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarTablaLlamadaEntrante_Paginacion", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oitem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oitem.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@FiltroFechaInicio", System.Data.SqlDbType.DateTime)).Value = oitem.FiltroFechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FiltroFechaFin", System.Data.SqlDbType.DateTime)).Value = oitem.FiltroFechaFin;
                    cmd.Parameters.Add(new SqlParameter("@Vendedor", System.Data.SqlDbType.VarChar, 100)).Value = oitem.Vendedor;
                    
                    cmd.Parameters.Add(new SqlParameter("@PaginaActual", System.Data.SqlDbType.Int)).Value = paging.PageNumber;
                    cmd.Parameters.Add(new SqlParameter("@TamanioPagina", System.Data.SqlDbType.Int)).Value = paging.PageRecords;
                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new LlamadaEntranteDTO()
                                {
                                    CodigoLlamadaE = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoLlamadaE")]),
                                    Nombres = oIDataReader[oIDataReader.GetOrdinal("Nombres")].ToString(),
                                    Apellidos = oIDataReader[oIDataReader.GetOrdinal("Apellidos")].ToString(),
                                    NombreCompleto = oIDataReader[oIDataReader.GetOrdinal("Nombres")].ToString() + " " + oIDataReader[oIDataReader.GetOrdinal("Apellidos")].ToString(),
                                    DNI = oIDataReader[oIDataReader.GetOrdinal("DNI")].ToString(),
                                    Telefono = oIDataReader[oIDataReader.GetOrdinal("Telefono")].ToString(),
                                    EstadoCelular = oIDataReader[oIDataReader.GetOrdinal("EstadoCelular")].ToString(),
                                    Celular = oIDataReader[oIDataReader.GetOrdinal("Celular")].ToString(),
                                    Correo = oIDataReader[oIDataReader.GetOrdinal("Correo")].ToString(),
                                    FechaNacimiento = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaNacimiento")]),
                                    ImagenUrl = oIDataReader[oIDataReader.GetOrdinal("ImagenUrl")].ToString(),
                                    Estado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Estado")]),
                                    Genero = oIDataReader[oIDataReader.GetOrdinal("Genero")].ToString(),
                                    Facebook = oIDataReader[oIDataReader.GetOrdinal("Facebook")].ToString(),
                                    Direccion = oIDataReader[oIDataReader.GetOrdinal("Direccion")].ToString(),
                                    Distrito = oIDataReader[oIDataReader.GetOrdinal("Distrito")].ToString(),
                                    Ocupacion = oIDataReader[oIDataReader.GetOrdinal("Ocupacion")].ToString(),
                                    TipoCliente = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("TipoCliente")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    Ubicaciones = oIDataReader[oIDataReader.GetOrdinal("Ubigeo")].ToString(),
                                    Vendedor = oIDataReader[oIDataReader.GetOrdinal("Vendedor")].ToString(),
                                    TipoDocumento = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("TipoDocumento")]),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString(),
                                    FechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")]),
                                    DescFechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")]).ToString("dd/MM/yyyy hh:mm tt"),
                                    CodigoPaquete = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoPaquete")]),
                                    Hijos = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Hijos")]),
                                    CantHijos = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantHijos")])
                                });
                            }
                        }

                    }
                }
            }
            return lista;           
		}
				
		public LlamadaEntranteDTO BuscarPorCodigoLlamadaEntrante(LlamadaEntranteDTO oitem)
		{
			LlamadaEntranteDTO itemDTO = null;
            
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspBuscarLlamadaEntrantePorCodigo", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoLlamadaE", System.Data.SqlDbType.VarChar, 20)).Value = oitem.CodigoLlamadaE;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oitem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oitem.CodigoSede;
                    
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new LlamadaEntranteDTO()
                                {
                                    CodigoLlamadaE = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoLlamadaE")]),
                                    Nombres = oIDataReader[oIDataReader.GetOrdinal("Nombres")].ToString(),
                                    Apellidos = oIDataReader[oIDataReader.GetOrdinal("Apellidos")].ToString(),
                                    DNI = oIDataReader[oIDataReader.GetOrdinal("DNI")].ToString(),
                                    Telefono = oIDataReader[oIDataReader.GetOrdinal("Telefono")].ToString(),
                                    Celular = oIDataReader[oIDataReader.GetOrdinal("Celular")].ToString(),
                                    Correo = oIDataReader[oIDataReader.GetOrdinal("Correo")].ToString(),
                                    FechaNacimiento = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaNacimiento")]),
                                    DesFechaNacimiento = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaNacimiento")]).ToString("dd/MM/yyyy"),
                                    ImagenUrl = oIDataReader[oIDataReader.GetOrdinal("ImagenUrl")].ToString(),
                                    Estado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Estado")]),
                                    Genero = oIDataReader[oIDataReader.GetOrdinal("Genero")].ToString(),
                                    Facebook = oIDataReader[oIDataReader.GetOrdinal("Facebook")].ToString(),
                                    Direccion = oIDataReader[oIDataReader.GetOrdinal("Direccion")].ToString(),
                                    Distrito = oIDataReader[oIDataReader.GetOrdinal("Distrito")].ToString(),
                                    Ocupacion = oIDataReader[oIDataReader.GetOrdinal("Ocupacion")].ToString(),
                                    TipoCliente = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("TipoCliente")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    Ubicaciones = oIDataReader[oIDataReader.GetOrdinal("Ubigeo")].ToString(),
                                    Vendedor = oIDataReader[oIDataReader.GetOrdinal("Vendedor")].ToString(),
                                    TipoDocumento = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("TipoDocumento")]),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString(),
                                    FechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")]),
                                    CodigoPaquete = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoPaquete")]),
                                    Hijos = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Hijos")]),
                                    CantHijos = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantHijos")]),
                                    CodigoTiempo = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoTiempo")]),
                                    Precio = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Precio")]),
                                    CodigoObjetivo = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoObjetivo")])
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;           
		}

        public LlamadaEntranteDTO uspListarTablaLlamadaEntrante_NumeroRegistros(LlamadaEntranteDTO oitem)
		{
            LlamadaEntranteDTO itemDTO = new LlamadaEntranteDTO();
		    
            int? Count = 0;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarTablaLlamadaEntrante_NumeroRegistros", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oitem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oitem.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@FiltroFechaInicio", System.Data.SqlDbType.DateTime)).Value = oitem.FiltroFechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FiltroFechaFin", System.Data.SqlDbType.DateTime)).Value = oitem.FiltroFechaFin;
                    cmd.Parameters.Add(new SqlParameter("@Vendedor", System.Data.SqlDbType.VarChar, 100)).Value = oitem.Vendedor;
                    cmd.Parameters.AddWithValue("@NumeroRegistros", Count).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new LlamadaEntranteDTO()
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

		public void Registrar(LlamadaEntranteDTO item)
		{
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspRegistrarLlamadaEntrante", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoLlamadaE", System.Data.SqlDbType.Int)).Value = item.CodigoLlamadaE;
                    cmd.Parameters.Add(new SqlParameter("@Nombres", System.Data.SqlDbType.VarChar, 100)).Value = item.Nombres;
                    cmd.Parameters.Add(new SqlParameter("@Apellidos", System.Data.SqlDbType.VarChar, 100)).Value = item.Apellidos;
                    cmd.Parameters.Add(new SqlParameter("@DNI", System.Data.SqlDbType.VarChar, 20)).Value = item.DNI;
                    cmd.Parameters.Add(new SqlParameter("@Telefono", System.Data.SqlDbType.VarChar, 20)).Value = item.Telefono;

                    cmd.Parameters.Add(new SqlParameter("@Celular", System.Data.SqlDbType.VarChar, 20)).Value = item.Celular;
                    cmd.Parameters.Add(new SqlParameter("@Correo", System.Data.SqlDbType.VarChar, 100)).Value = item.Correo;
                    cmd.Parameters.Add(new SqlParameter("@FechaNacimiento", System.Data.SqlDbType.DateTime)).Value = item.FechaNacimiento;
                    cmd.Parameters.Add(new SqlParameter("@ImagenUrl", System.Data.SqlDbType.VarChar, 200)).Value = item.ImagenUrl;
                    cmd.Parameters.Add(new SqlParameter("@Estado", System.Data.SqlDbType.Bit)).Value = item.Estado;

                    cmd.Parameters.Add(new SqlParameter("@Genero", System.Data.SqlDbType.VarChar, 1)).Value = item.Genero;
                    cmd.Parameters.Add(new SqlParameter("@Facebook", System.Data.SqlDbType.VarChar, 100)).Value = item.Facebook;
                    cmd.Parameters.Add(new SqlParameter("@Direccion", System.Data.SqlDbType.VarChar, 100)).Value = item.Direccion;
                    cmd.Parameters.Add(new SqlParameter("@Distrito", System.Data.SqlDbType.VarChar,100)).Value = item.Distrito;
                    cmd.Parameters.Add(new SqlParameter("@Ocupacion", System.Data.SqlDbType.VarChar, 100)).Value = item.Ocupacion;

                    cmd.Parameters.Add(new SqlParameter("@TipoCliente", System.Data.SqlDbType.Int)).Value = item.TipoCliente;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Ubigeo", System.Data.SqlDbType.VarChar, 50)).Value = item.Ubicaciones;
                    cmd.Parameters.Add(new SqlParameter("@Vendedor", System.Data.SqlDbType.VarChar, 100)).Value = item.Vendedor;
                    cmd.Parameters.Add(new SqlParameter("@TipoDocumento", System.Data.SqlDbType.Int)).Value = item.TipoDocumento;
                   
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;
                    cmd.Parameters.Add(new SqlParameter("@CodigoPaquete", System.Data.SqlDbType.Int)).Value = item.CodigoPaquete;
                    cmd.Parameters.Add(new SqlParameter("@Hijos", System.Data.SqlDbType.Int)).Value = item.Hijos;
                    cmd.Parameters.Add(new SqlParameter("@CantHijos", System.Data.SqlDbType.Int)).Value = item.CantHijos;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;

                    cmd.Parameters.Add(new SqlParameter("@CodigoInicioSesion", System.Data.SqlDbType.Int)).Value = item.CodigoInicioSesion;
                    cmd.Parameters.Add(new SqlParameter("@CodigoTiempo", System.Data.SqlDbType.VarChar, 200)).Value = item.CodigoTiempo;
                    cmd.Parameters.Add(new SqlParameter("@Precio", System.Data.SqlDbType.Decimal)).Value = item.Precio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoObjetivo", System.Data.SqlDbType.Int)).Value = item.CodigoObjetivo;
                    cmd.ExecuteNonQuery();
                }
            }
		}
        
        public int uspActualizarLlamadaEAInvitado(LlamadaEntranteDTO item)
        {
            int? campoRetorno = 0;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspActualizarLlamadaEAInvitado", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CodigoInvitado", 0).Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@CodigoLlamadaE", System.Data.SqlDbType.Int)).Value = item.CodigoLlamadaE;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioEdicion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioEdicion;
                    cmd.Parameters.Add(new SqlParameter("@Observacion", System.Data.SqlDbType.VarChar, 100)).Value = item.LlamadaE_Observacion;
                    cmd.Parameters.Add(new SqlParameter("@NroDias", System.Data.SqlDbType.Int)).Value = item.LlamadaE_NroDias;
                    cmd.Parameters.Add(new SqlParameter("@NroDiasActual", System.Data.SqlDbType.Int)).Value = item.LlamadaE_NroDiasActual;

                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = item.LlamadaE_FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = item.LlamadaE_FechaFin;
                    cmd.Parameters.Add(new SqlParameter("@Codigo_LlamadaEPor", System.Data.SqlDbType.Int)).Value = item.LlamadaE_CodigoLlamadaEPor;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;

                    cmd.Parameters.Add(new SqlParameter("@CodigoInicioSesion", System.Data.SqlDbType.Int)).Value = item.CodigoInicioSesion;
                    
                    cmd.ExecuteNonQuery();
                    campoRetorno = Convert.ToInt32(cmd.Parameters["@CodigoInvitado"].Value);
                }
            }
            return Convert.ToInt32(campoRetorno);
        }

        public int uspActualizarLlamadaEASocio(LlamadaEntranteDTO item)
        {
            int? campoRetorno = 0;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspActualizarLlamadaEASocio", conn))
                {                                                                                        
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CodigoSocio", 0).Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@CodigoLlamadaE", System.Data.SqlDbType.Int)).Value = item.CodigoLlamadaE;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioEdicion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioEdicion;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar,100)).Value = item.UsuarioCreacion;
                    cmd.Parameters.Add(new SqlParameter("@CodigoInicioSesion", System.Data.SqlDbType.Int)).Value = item.CodigoInicioSesion;
                    
                    cmd.ExecuteNonQuery();
                    campoRetorno = Convert.ToInt32(cmd.Parameters["@CodigoSocio"].Value);
                }
            }
            return Convert.ToInt32(campoRetorno);
        }

		public void Eliminar(LlamadaEntranteDTO item)
		{
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspEliminarLlamadaEntrante", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoLlamadaE", System.Data.SqlDbType.Int)).Value = item.CodigoLlamadaE;                    
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;
                    cmd.Parameters.Add(new SqlParameter("@CodigoInicioSesion", System.Data.SqlDbType.Int)).Value = item.CodigoInicioSesion;

                    cmd.ExecuteNonQuery();
                }
            }
		}

        //prospectos web
        
        public List<LlamadaEntranteDTO> uspListarTablaWeb_Paginacion(LlamadaEntranteDTO oitem, Paging paging)
        {
            List<LlamadaEntranteDTO> lista = new List<LlamadaEntranteDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarTablaWeb_Paginacion", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oitem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oitem.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@FiltroFechaInicio", System.Data.SqlDbType.DateTime)).Value = oitem.FiltroFechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FiltroFechaFin", System.Data.SqlDbType.DateTime)).Value = oitem.FiltroFechaFin;
                    cmd.Parameters.Add(new SqlParameter("@Vendedor", System.Data.SqlDbType.VarChar, 100)).Value = oitem.Vendedor;
                    cmd.Parameters.Add(new SqlParameter("@Buscador", System.Data.SqlDbType.VarChar, 200)).Value = oitem.Nombres;

                    cmd.Parameters.Add(new SqlParameter("@PaginaActual", System.Data.SqlDbType.Int)).Value = paging.PageNumber;
                    cmd.Parameters.Add(new SqlParameter("@TamanioPagina", System.Data.SqlDbType.Int)).Value = paging.PageRecords;
                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new LlamadaEntranteDTO()
                                {
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    CodigoLlamadaE = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoWeb")]),
                                    Nombres = oIDataReader[oIDataReader.GetOrdinal("Nombres")].ToString(),
                                    Apellidos = oIDataReader[oIDataReader.GetOrdinal("Apellidos")].ToString(),                                 
                                    Telefono = oIDataReader[oIDataReader.GetOrdinal("Telefono")].ToString(),
                                    Celular = oIDataReader[oIDataReader.GetOrdinal("Celular")].ToString(),
                                    Correo = oIDataReader[oIDataReader.GetOrdinal("Correo")].ToString(),
                                    Estado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Estado")]),
                                    Genero = oIDataReader[oIDataReader.GetOrdinal("Genero")].ToString(),
                                    Vendedor = oIDataReader[oIDataReader.GetOrdinal("Vendedor")].ToString(),
                                    TipoCliente = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("TipoCliente")]),
                                    CodigoTiempo = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoTiempo")]),
                                    Precio = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Precio")]),                                    
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString(),
                                    FechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")]),
                                    DescFechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")]).ToString("dd/MM/yyyy HH:mm:ss"),                                 
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }

        public LlamadaEntranteDTO uspListarTablaWeb_NumeroRegistros(LlamadaEntranteDTO oitem)
        {
            LlamadaEntranteDTO itemDTO = new LlamadaEntranteDTO();

            int? Count = 0;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarTablaWeb_NumeroRegistros", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oitem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oitem.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@FiltroFechaInicio", System.Data.SqlDbType.DateTime)).Value = oitem.FiltroFechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FiltroFechaFin", System.Data.SqlDbType.DateTime)).Value = oitem.FiltroFechaFin;
                    cmd.Parameters.Add(new SqlParameter("@Vendedor", System.Data.SqlDbType.VarChar, 100)).Value = oitem.Vendedor;
                    cmd.Parameters.Add(new SqlParameter("@Buscador", System.Data.SqlDbType.VarChar, 200)).Value = oitem.Nombres;

                    cmd.Parameters.AddWithValue("@NumeroRegistros", Count).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new LlamadaEntranteDTO()
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

        public LlamadaEntranteDTO uspBuscarPropectoWebPorCodigo(LlamadaEntranteDTO oitem)
        {
            LlamadaEntranteDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspBuscarPropectoWebPorCodigo", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoWeb", System.Data.SqlDbType.VarChar, 20)).Value = oitem.CodigoLlamadaE;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oitem.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oitem.CodigoUnidadNegocio;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new LlamadaEntranteDTO()
                                {
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    CodigoLlamadaE = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoWeb")]),
                                    Nombres = oIDataReader[oIDataReader.GetOrdinal("Nombres")].ToString(),
                                    Apellidos = oIDataReader[oIDataReader.GetOrdinal("Apellidos")].ToString(),
                                    Telefono = oIDataReader[oIDataReader.GetOrdinal("Telefono")].ToString(),
                                    Celular = oIDataReader[oIDataReader.GetOrdinal("Celular")].ToString(),
                                    Correo = oIDataReader[oIDataReader.GetOrdinal("Correo")].ToString(),
                                    Genero = oIDataReader[oIDataReader.GetOrdinal("Genero")].ToString(),
                                    Vendedor = oIDataReader[oIDataReader.GetOrdinal("Vendedor")].ToString(),
                                    TipoCliente = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("TipoCliente")]),
                                    CodigoTiempo = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoTiempo")]),
                                    Precio = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Precio")]),
                                    Estado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Estado")]),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString(),
                                    FechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")])
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;
        }


        public void uspRegistrarProspectoWeb(LlamadaEntranteDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspRegistrarProspectoWeb", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoWeb", System.Data.SqlDbType.Int)).Value = item.CodigoLlamadaE;                    
                    cmd.Parameters.Add(new SqlParameter("@Nombres", System.Data.SqlDbType.VarChar, 100)).Value = item.Nombres;
                    cmd.Parameters.Add(new SqlParameter("@Apellidos", System.Data.SqlDbType.VarChar, 100)).Value = item.Apellidos;
                   
                    cmd.Parameters.Add(new SqlParameter("@Telefono", System.Data.SqlDbType.VarChar, 20)).Value = item.Telefono;
                    cmd.Parameters.Add(new SqlParameter("@Celular", System.Data.SqlDbType.VarChar, 20)).Value = item.Celular;
                    cmd.Parameters.Add(new SqlParameter("@Correo", System.Data.SqlDbType.VarChar, 100)).Value = item.Correo;
                    cmd.Parameters.Add(new SqlParameter("@Genero", System.Data.SqlDbType.VarChar, 1)).Value = item.Genero;
                    cmd.Parameters.Add(new SqlParameter("@Vendedor", System.Data.SqlDbType.VarChar, 100)).Value = item.Vendedor;
                    cmd.Parameters.Add(new SqlParameter("@TipoCliente", System.Data.SqlDbType.Int)).Value = item.TipoCliente;
                    cmd.Parameters.Add(new SqlParameter("@CodigoTiempo", System.Data.SqlDbType.VarChar, 200)).Value = item.CodigoTiempo;
                    cmd.Parameters.Add(new SqlParameter("@Precio", System.Data.SqlDbType.Decimal)).Value = item.Precio;
                    cmd.Parameters.Add(new SqlParameter("@Estado", System.Data.SqlDbType.Bit)).Value = item.Estado;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;                   
                    cmd.Parameters.Add(new SqlParameter("@CodigoInicioSesion", System.Data.SqlDbType.Int)).Value = item.CodigoInicioSesion;
                    
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void uspEliminarProspectoWeb(LlamadaEntranteDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspEliminarProspectoWeb", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoWeb", System.Data.SqlDbType.Int)).Value = item.CodigoLlamadaE;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;
                    cmd.Parameters.Add(new SqlParameter("@CodigoInicioSesion", System.Data.SqlDbType.Int)).Value = item.CodigoInicioSesion;

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public int uspActualizarProspectoWebASocio(LlamadaEntranteDTO item)
        {
            int? campoRetorno = 0;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspActualizarWebASocio", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CodigoSocio", 0).Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@CodigoLlamadaE", System.Data.SqlDbType.Int)).Value = item.CodigoLlamadaE;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioEdicion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioEdicion;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;
                    cmd.Parameters.Add(new SqlParameter("@CodigoInicioSesion", System.Data.SqlDbType.Int)).Value = item.CodigoInicioSesion;

                    cmd.ExecuteNonQuery();
                    campoRetorno = Convert.ToInt32(cmd.Parameters["@CodigoSocio"].Value);
                }
            }
            return Convert.ToInt32(campoRetorno);
        }



    }
}
