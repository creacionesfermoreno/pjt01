
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using E_DataModel.Gimnasio;
using E_DataModel.Common;
using System.Data.SqlClient;

namespace E_DataLayer.Gimnasio
{
	public class ReferidoData
	{
		
        public List<ReferidoDTO> uspListarReferido_Paginacion(ReferidoDTO oItem,Paging paging)
        {
            List<ReferidoDTO> lista = new List<ReferidoDTO>();
            
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarTablaReferidos_Paginacion", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oItem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oItem.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@FiltroFechaInicio", System.Data.SqlDbType.DateTime)).Value = oItem.FiltroFechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FiltroFechaFin", System.Data.SqlDbType.DateTime)).Value = oItem.FiltroFechaFin;
                    cmd.Parameters.Add(new SqlParameter("@Vendedor", System.Data.SqlDbType.VarChar, 200)).Value = oItem.Vendedor;
                    cmd.Parameters.Add(new SqlParameter("@Buscador", System.Data.SqlDbType.VarChar, 200)).Value = oItem.Nombres;

                    cmd.Parameters.Add(new SqlParameter("@PaginaActual", System.Data.SqlDbType.Int)).Value = paging.PageNumber;
                    cmd.Parameters.Add(new SqlParameter("@TamanioPagina", System.Data.SqlDbType.Int)).Value = paging.PageRecords;
                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;
                    
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                lista.Add(new ReferidoDTO()
                                {
                                    CodigoReferido = Convert.ToInt32(reader[reader.GetOrdinal("CodigoReferido")]),
                                    Nombres = reader[reader.GetOrdinal("Nombres")].ToString(),
                                    Apellidos = reader[reader.GetOrdinal("Apellidos")].ToString(),
                                    NombreCompleto = reader[reader.GetOrdinal("Nombres")].ToString() + " " + reader[reader.GetOrdinal("Apellidos")].ToString(),
                                    DNI = reader[reader.GetOrdinal("DNI")].ToString(),
                                    Telefono = reader[reader.GetOrdinal("Telefono")].ToString(),
                                    Celular = reader[reader.GetOrdinal("Celular")].ToString(),
                                    EstadoCelular = reader[reader.GetOrdinal("EstadoCelular")].ToString(),
                                    Correo = reader[reader.GetOrdinal("Correo")].ToString(),
                                    FechaNacimiento = Convert.ToDateTime(reader[reader.GetOrdinal("FechaNacimiento")]),
                                    ImagenUrl = reader[reader.GetOrdinal("ImagenUrl")].ToString(),
                                    Estado = Convert.ToBoolean(reader[reader.GetOrdinal("Estado")]),
                                    Genero = reader[reader.GetOrdinal("Genero")].ToString(),
                                    Facebook = reader[reader.GetOrdinal("Facebook")].ToString(),
                                    ReferidoPor = reader[reader.GetOrdinal("ReferidoPor")].ToString(),
                                    Direccion = reader[reader.GetOrdinal("Direccion")].ToString(),
                                    Distrito = reader[reader.GetOrdinal("Distrito")].ToString(),
                                    Ocupacion = reader[reader.GetOrdinal("Ocupacion")].ToString(),
                                    TipoCliente = Convert.ToInt32(reader[reader.GetOrdinal("TipoCliente")]),
                                    CodigoSede = Convert.ToInt32(reader[reader.GetOrdinal("CodigoSede")]),
                                    Ubicaciones = reader[reader.GetOrdinal("Ubigeo")].ToString(),
                                    Vendedor = reader[reader.GetOrdinal("Vendedor")].ToString(),
                                    TipoDocumento = Convert.ToInt32(reader[reader.GetOrdinal("TipoDocumento")]),
                                    UsuarioCreacion = reader[reader.GetOrdinal("UsuarioCreacion")].ToString(),
                                    FechaCreacion = Convert.ToDateTime(reader[reader.GetOrdinal("FechaCreacion")]),
                                    DescFechaCreacion = Convert.ToDateTime(reader[reader.GetOrdinal("FechaCreacion")]).ToString("dd/MM/yyyy hh:mm tt"),
                                    CodigoPaquete = Convert.ToInt32(reader[reader.GetOrdinal("CodigoPaquete")]),
                                    Hijos = Convert.ToInt32(reader[reader.GetOrdinal("Hijos")]),
                                    CantHijos = Convert.ToInt32(reader[reader.GetOrdinal("CantHijos")])                                 
                                });
                            }
                        }

                    }
                }
            }
            return lista;            
        }

        public ReferidoDTO uspListarTablaReferido_NumeroRegistros(ReferidoDTO oItem)
        {
            ReferidoDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarTablaReferidos_NumeroRegistros", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oItem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oItem.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@FiltroFechaInicio", System.Data.SqlDbType.DateTime)).Value = oItem.FiltroFechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FiltroFechaFin", System.Data.SqlDbType.DateTime)).Value = oItem.FiltroFechaFin;
                    cmd.Parameters.Add(new SqlParameter("@Vendedor", System.Data.SqlDbType.VarChar, 200)).Value = oItem.Vendedor;
                    cmd.Parameters.Add(new SqlParameter("@Buscador", System.Data.SqlDbType.VarChar, 200)).Value = oItem.Nombres;

                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                itemDTO = new ReferidoDTO()
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

        public ReferidoDTO BuscarPorCodigoReferido(ReferidoDTO oItem)
		{
            ReferidoDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspBuscarReferidoPorCodigo", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoReferido", System.Data.SqlDbType.Int)).Value = oItem.CodigoReferido;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oItem.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oItem.CodigoUnidadNegocio;
                    
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                itemDTO = new ReferidoDTO()
                                {
                                    CodigoReferido = Convert.ToInt32(reader[reader.GetOrdinal("CodigoReferido")]),
                                    Nombres = reader[reader.GetOrdinal("Nombres")].ToString(),
                                    Apellidos = reader[reader.GetOrdinal("Apellidos")].ToString(),
                                    DNI = reader[reader.GetOrdinal("DNI")].ToString(),
                                    Telefono = reader[reader.GetOrdinal("Telefono")].ToString(),
                                    Celular = reader[reader.GetOrdinal("Celular")].ToString(),
                                    Correo = reader[reader.GetOrdinal("Correo")].ToString(),
                                    FechaNacimiento = Convert.ToDateTime(reader[reader.GetOrdinal("FechaNacimiento")]),
                                    DesFechaNacimiento = Convert.ToDateTime(reader[reader.GetOrdinal("FechaNacimiento")]).ToString("dd/MM/yyyy"),
                                    ImagenUrl = reader[reader.GetOrdinal("ImagenUrl")].ToString(),
                                    Estado = Convert.ToBoolean(reader[reader.GetOrdinal("Estado")]),
                                    Genero = reader[reader.GetOrdinal("Genero")].ToString(),
                                    Facebook = reader[reader.GetOrdinal("Facebook")].ToString(),
                                    ReferidoPor = reader[reader.GetOrdinal("ReferidoPor")].ToString(),
                                    Direccion = reader[reader.GetOrdinal("Direccion")].ToString(),
                                    Distrito = reader[reader.GetOrdinal("Distrito")].ToString(),
                                    Ocupacion = reader[reader.GetOrdinal("Ocupacion")].ToString(),
                                    TipoCliente = Convert.ToInt32(reader[reader.GetOrdinal("TipoCliente")]),
                                    CodigoSede = Convert.ToInt32(reader[reader.GetOrdinal("CodigoSede")]),
                                    Ubicaciones = reader[reader.GetOrdinal("Ubigeo")].ToString(),
                                    Vendedor = reader[reader.GetOrdinal("Vendedor")].ToString(),
                                    TipoDocumento = Convert.ToInt32(reader[reader.GetOrdinal("TipoDocumento")]),
                                    UsuarioCreacion = reader[reader.GetOrdinal("UsuarioCreacion")].ToString(),
                                    FechaCreacion = Convert.ToDateTime(reader[reader.GetOrdinal("FechaCreacion")]),
                                    CodigoPaquete = Convert.ToInt32(reader[reader.GetOrdinal("CodigoPaquete")]),
                                    Hijos = Convert.ToInt32(reader[reader.GetOrdinal("Hijos")]),
                                    CantHijos = Convert.ToInt32(reader[reader.GetOrdinal("CantHijos")]),
                                    CodigoReferidoPor = Convert.ToInt32(reader[reader.GetOrdinal("CodigoReferidoPor")]),
                                    CodigoTiempo = Convert.ToInt32(reader[reader.GetOrdinal("CodigoTiempo")]),
                                    Precio = Convert.ToDecimal(reader[reader.GetOrdinal("Precio")]),
                                    CodigoSubProcedencia = Convert.ToInt32(reader[reader.GetOrdinal("CodigoSubProcedencia")]),
                                    CodigoObjetivo = Convert.ToInt32(reader[reader.GetOrdinal("CodigoObjetivo")])
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;            
		}

   
		public void Registrar(ReferidoDTO item)
		{
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspRegistrarReferido", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoReferido", System.Data.SqlDbType.Int)).Value = item.CodigoReferido;
                    cmd.Parameters.Add(new SqlParameter("@Nombres", System.Data.SqlDbType.VarChar,100)).Value = item.Nombres;
                    cmd.Parameters.Add(new SqlParameter("@Apellidos", System.Data.SqlDbType.VarChar,100)).Value = item.Apellidos;
                    cmd.Parameters.Add(new SqlParameter("@DNI", System.Data.SqlDbType.VarChar,20)).Value = item.DNI;
                    cmd.Parameters.Add(new SqlParameter("@Telefono", System.Data.SqlDbType.VarChar,20)).Value = item.Telefono;

                    cmd.Parameters.Add(new SqlParameter("@Celular", System.Data.SqlDbType.VarChar,20)).Value = item.Celular;
                    cmd.Parameters.Add(new SqlParameter("@Correo", System.Data.SqlDbType.VarChar, 100)).Value = item.Correo;
                    cmd.Parameters.Add(new SqlParameter("@FechaNacimiento", System.Data.SqlDbType.DateTime)).Value = item.FechaNacimiento;
                    cmd.Parameters.Add(new SqlParameter("@ImagenUrl", System.Data.SqlDbType.VarChar, 200)).Value = item.ImagenUrl;
                    cmd.Parameters.Add(new SqlParameter("@Estado", System.Data.SqlDbType.Bit)).Value = item.Estado;

                    cmd.Parameters.Add(new SqlParameter("@Genero", System.Data.SqlDbType.VarChar, 1)).Value = item.Genero;
                    cmd.Parameters.Add(new SqlParameter("@Facebook", System.Data.SqlDbType.VarChar, 100)).Value = item.Facebook;
                    cmd.Parameters.Add(new SqlParameter("@ReferidoPor", System.Data.SqlDbType.VarChar,100)).Value = item.ReferidoPor;
                    cmd.Parameters.Add(new SqlParameter("@Direccion", System.Data.SqlDbType.VarChar,100)).Value = item.Direccion;
                    cmd.Parameters.Add(new SqlParameter("@Distrito", System.Data.SqlDbType.VarChar, 100)).Value = item.Distrito;

                    cmd.Parameters.Add(new SqlParameter("@Ocupacion", System.Data.SqlDbType.VarChar,100)).Value = item.Ocupacion;
                    cmd.Parameters.Add(new SqlParameter("@TipoCliente", System.Data.SqlDbType.Int)).Value = item.TipoCliente;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Ubigeo", System.Data.SqlDbType.VarChar, 50)).Value = item.Ubicaciones;
                    cmd.Parameters.Add(new SqlParameter("@Vendedor", System.Data.SqlDbType.VarChar, 100)).Value = item.Vendedor;

                    cmd.Parameters.Add(new SqlParameter("@TipoDocumento", System.Data.SqlDbType.Int)).Value = item.TipoDocumento;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar,100)).Value = item.UsuarioCreacion;
                    cmd.Parameters.Add(new SqlParameter("@CodigoPaquete", System.Data.SqlDbType.Int)).Value = item.CodigoPaquete;
                    cmd.Parameters.Add(new SqlParameter("@Hijos", System.Data.SqlDbType.Int)).Value = item.Hijos;
                    cmd.Parameters.Add(new SqlParameter("@CantHijos", System.Data.SqlDbType.Int)).Value = item.CantHijos;

                    cmd.Parameters.Add(new SqlParameter("@CodigoReferidoPor", System.Data.SqlDbType.Int)).Value = item.CodigoReferidoPor;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoInicioSesion", System.Data.SqlDbType.Int)).Value = item.CodigoInicioSesion;
                    cmd.Parameters.Add(new SqlParameter("@CodigoTiempo", System.Data.SqlDbType.Int)).Value = item.CodigoTiempo;
                    cmd.Parameters.Add(new SqlParameter("@Precio", System.Data.SqlDbType.Decimal)).Value = item.Precio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSubProcedencia", System.Data.SqlDbType.Int)).Value = item.CodigoSubProcedencia;
                    cmd.Parameters.Add(new SqlParameter("@CodigoObjetivo", System.Data.SqlDbType.Int)).Value = item.CodigoObjetivo;
                    cmd.ExecuteNonQuery();
                }
            }
		}

        public int uspActualizarReferidoASocio(ReferidoDTO item)
        {
            int? campoRetorno = 0;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspActualizarReferidoASocio", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CodigoSocio", 0).Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@CodigoReferido", System.Data.SqlDbType.Int)).Value = item.CodigoReferido;
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

        public int uspActualizarReferidoAInvitado(ReferidoDTO item)
        {
            int? campoRetorno = 0;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspActualizarReferidoAInvitado", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CodigoInvitado", 0).Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@CodigoReferido", System.Data.SqlDbType.Int)).Value = item.CodigoReferido;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioEdicion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioEdicion;
                    cmd.Parameters.Add(new SqlParameter("@Observacion", System.Data.SqlDbType.VarChar,100)).Value = item.Referido_Observacion;
                    cmd.Parameters.Add(new SqlParameter("@NroDias", System.Data.SqlDbType.Int)).Value = item.Referido_NroDias;

                    cmd.Parameters.Add(new SqlParameter("@NroDiasActual", System.Data.SqlDbType.Int)).Value = item.Referido_NroDiasActual;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = item.Referido_FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = item.Referido_FechaFin;
                    cmd.Parameters.Add(new SqlParameter("@Codigo_ReferidoPor", System.Data.SqlDbType.Int)).Value = item.Referido_CodigoReferidoPor;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;

                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar,100)).Value = item.UsuarioCreacion;
                    cmd.Parameters.Add(new SqlParameter("@CodigoInicioSesion", System.Data.SqlDbType.Int)).Value = item.CodigoInicioSesion;


                    cmd.ExecuteNonQuery();
                }
            }
            return Convert.ToInt32(campoRetorno);
        }
		
		public void Eliminar(ReferidoDTO item)
		{
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspEliminarReferido", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoReferido", System.Data.SqlDbType.Int)).Value = item.CodigoReferido;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;
                    cmd.Parameters.Add(new SqlParameter("@CodigoInicioSesion", System.Data.SqlDbType.Int)).Value = item.CodigoInicioSesion;
                    
                    cmd.ExecuteNonQuery();
                }
            }
		}
	}
}
