
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using E_DataModel.Gimnasio;
using E_DataModel.Common;
using System.Data.SqlClient;

namespace E_DataLayer.Gimnasio
{
	public class InvitadosData
	{
        public List<InvitadosDTO> uspListarInvitadosBusqueda(InvitadosDTO oitem)
		{
			List<InvitadosDTO> lista = new List<InvitadosDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarInvitadosBusqueda", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oitem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@filterNombre", System.Data.SqlDbType.VarChar,100)).Value = oitem.Nombres;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oitem.CodigoSede;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new InvitadosDTO()
                                {                                 
                                    CodigoInvitado = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoInvitado")]),
                                    Nombres = oIDataReader[oIDataReader.GetOrdinal("Nombres")].ToString(),
                                    Apellidos = oIDataReader[oIDataReader.GetOrdinal("Apellidos")].ToString(),
                                    flagCumpleanios = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaNacimiento")]).Day == DateTime.Now.Day && Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaNacimiento")]).Month == DateTime.Now.Month ? "" : "none",
                                    DNI = oIDataReader[oIDataReader.GetOrdinal("DNI")].ToString(),
                                    Telefono = oIDataReader[oIDataReader.GetOrdinal("Telefono")].ToString(),
                                    Celular = oIDataReader[oIDataReader.GetOrdinal("Celular")].ToString(),
                                    Correo = oIDataReader[oIDataReader.GetOrdinal("Correo")].ToString(),
                                    FechaNacimiento = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaNacimiento")]),
                                    ImagenUrl = oIDataReader[oIDataReader.GetOrdinal("ImagenUrl")].ToString(),
                                    Estado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Estado")]),
                                    Facebook = oIDataReader[oIDataReader.GetOrdinal("Facebook")].ToString(),
                                    desSede = oIDataReader[oIDataReader.GetOrdinal("desSede")].ToString(),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString()
                                });
                            }
                        }

                    }
                }
            }
            return lista;            
		}
        
        public List<InvitadosDTO> uspListarTablaInvitados_Paginacion(InvitadosDTO oitem, Paging paging)
		{
			List<InvitadosDTO> lista = new List<InvitadosDTO>();
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarTablaInvitados_Paginacion", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oitem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oitem.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@FiltroFechaInicio", System.Data.SqlDbType.DateTime)).Value = oitem.FiltroFechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FiltroFechaFin", System.Data.SqlDbType.DateTime)).Value = oitem.FiltroFechaFin;
                    cmd.Parameters.Add(new SqlParameter("@Vendedor", System.Data.SqlDbType.VarChar,100)).Value = oitem.Vendedor;
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
                                lista.Add(new InvitadosDTO()
                                {
                                    CodigoInvitado = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoInvitado")]),
                                    Nombres = oIDataReader[oIDataReader.GetOrdinal("Nombres")].ToString(),
                                    Apellidos = oIDataReader[oIDataReader.GetOrdinal("Apellidos")].ToString(),
                                    NombreCompleto = oIDataReader[oIDataReader.GetOrdinal("NombreCompleto")].ToString(),
                                    DNI = oIDataReader[oIDataReader.GetOrdinal("DNI")].ToString(),
                                    Telefono = oIDataReader[oIDataReader.GetOrdinal("Telefono")].ToString(),
                                    Celular = oIDataReader[oIDataReader.GetOrdinal("Celular")].ToString(),
                                    EstadoCelular = oIDataReader[oIDataReader.GetOrdinal("EstadoCelular")].ToString(),
                                    Correo = oIDataReader[oIDataReader.GetOrdinal("Correo")].ToString(),
                                    FechaNacimiento = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaNacimiento")]),
                                    DescFechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")]).ToString("dd/MM/yyyy hh:mm tt"),
                                    ImagenUrl = oIDataReader[oIDataReader.GetOrdinal("ImagenUrl")].ToString(),
                                    Estado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Estado")]),
                                    Genero = oIDataReader[oIDataReader.GetOrdinal("Genero")].ToString(),
                                    Facebook = oIDataReader[oIDataReader.GetOrdinal("Facebook")].ToString(),
                                    CodigoSubProcedencia = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSubProcedencia")]),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString(),
                                    EstadoDiasAsistidos = oIDataReader[oIDataReader.GetOrdinal("EstadoDiasAsistidos")].ToString()                                  
                                });
                            }
                        }

                    }
                }
            }
            return lista;
		}

        public InvitadosDTO uspListarTablaInvitados_NumeroRegistros(InvitadosDTO oitem)
        {
            InvitadosDTO itemDTO = new InvitadosDTO();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarTablaInvitados_NumeroRegistros", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oitem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oitem.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@FiltroFechaInicio", System.Data.SqlDbType.DateTime)).Value = oitem.FiltroFechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FiltroFechaFin", System.Data.SqlDbType.DateTime)).Value = oitem.FiltroFechaFin;
                    cmd.Parameters.Add(new SqlParameter("@Vendedor", System.Data.SqlDbType.VarChar, 100)).Value = oitem.Vendedor;
                    cmd.Parameters.Add(new SqlParameter("@Buscador", System.Data.SqlDbType.VarChar, 200)).Value = oitem.Nombres;

                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new InvitadosDTO()
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

        public InvitadosDTO uspBuscarInfoPorCodInvitadoFiltro(InvitadosDTO oitem)
		{
			InvitadosDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspBuscarInfoPorCodInvitadoFiltro", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oitem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oitem.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@DNI", System.Data.SqlDbType.VarChar,20)).Value = oitem.DNI;
                   
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new InvitadosDTO()
                                {
                                    CodigoInvitado = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoInvitado")]),
                                    Nombres = oIDataReader[oIDataReader.GetOrdinal("Nombres")].ToString(),
                                    Apellidos = oIDataReader[oIDataReader.GetOrdinal("Apellidos")].ToString(),
                                    DNI = oIDataReader[oIDataReader.GetOrdinal("DNI")].ToString(),
                                    Telefono = oIDataReader[oIDataReader.GetOrdinal("Telefono")].ToString(),
                                    Celular = oIDataReader[oIDataReader.GetOrdinal("Celular")].ToString(),
                                    Correo = oIDataReader[oIDataReader.GetOrdinal("Correo")].ToString(),
                                    DesFechaNacimiento = oIDataReader[oIDataReader.GetOrdinal("DescFechaNacimiento")].ToString(),
                                    ImagenUrl = oIDataReader[oIDataReader.GetOrdinal("ImagenUrl")].ToString(),
                                    Genero = oIDataReader[oIDataReader.GetOrdinal("DescGenero")].ToString(),
                                    Facebook = oIDataReader[oIDataReader.GetOrdinal("Facebook")].ToString(),
                                    DesInvitadoPor = oIDataReader[oIDataReader.GetOrdinal("InvitadoPor")].ToString(),
                                    Codigo_InvitadoPor = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Codigo_InvitadoPor")]),

                                    Direccion = oIDataReader[oIDataReader.GetOrdinal("Direccion")].ToString(),
                                    Ocupacion = oIDataReader[oIDataReader.GetOrdinal("Ocupacion")].ToString(),
                                    DesTipoCliente = oIDataReader[oIDataReader.GetOrdinal("DescTipoCliente")].ToString(),
                                    Edad = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Edad")]),
                                    Observacion = oIDataReader[oIDataReader.GetOrdinal("Observacion")].ToString(),
                                    TipoDocumento = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("TipoDocumento")]),
                                    Estado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Estado")]),
                                    FechaFin = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("InvitadoFechaFin")]),
                                    NroDias = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("NroDias")]),
                                    NroDiasActual = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("NroDiasActual")]),
                                    DescFechaInicio = oIDataReader[oIDataReader.GetOrdinal("FechaInicio")].ToString(),
                                    DescFechaFin = oIDataReader[oIDataReader.GetOrdinal("FechaFin")].ToString(),
                                    HuellaStr = oIDataReader[oIDataReader.GetOrdinal("HuellaStr")].ToString(),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString(),
                                    DescFechaCreacion = oIDataReader[oIDataReader.GetOrdinal("DescFechaCreacion")].ToString(),
                                    Vendedor = oIDataReader[oIDataReader.GetOrdinal("Vendedor")].ToString(),
                                    desSede = oIDataReader[oIDataReader.GetOrdinal("desSede")].ToString(),
                                    Hoy = Convert.ToDateTime(DateTime.Now).ToString("dd/MM/yyyy")                                  
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;            
		}
        
        public InvitadosDTO uspBuscarClientesDatosInvitadosPorCodigo(InvitadosDTO oitem)
		{
			InvitadosDTO itemDTO = null;
            
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspBuscarClientesDatosInvitadosPorCodigo", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoInvitado", System.Data.SqlDbType.Int)).Value = oitem.CodigoInvitado;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oitem.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oitem.CodigoUnidadNegocio;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new InvitadosDTO()
                                {
                                    CodigoInvitado = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoInvitado")]),
                                    Nombres = oIDataReader[oIDataReader.GetOrdinal("Nombres")].ToString(),
                                    Apellidos = oIDataReader[oIDataReader.GetOrdinal("Apellidos")].ToString(),
                                    DNI = oIDataReader[oIDataReader.GetOrdinal("DNI")].ToString(),
                                    Telefono = oIDataReader[oIDataReader.GetOrdinal("Telefono")].ToString(),
                                    Celular = oIDataReader[oIDataReader.GetOrdinal("Celular")].ToString(),
                                    Correo = oIDataReader[oIDataReader.GetOrdinal("Correo")].ToString(),
                                    DesFechaNacimiento = oIDataReader[oIDataReader.GetOrdinal("DescFechaNacimiento")].ToString(),
                                    Genero = oIDataReader[oIDataReader.GetOrdinal("Genero")].ToString(),
                                    Facebook = oIDataReader[oIDataReader.GetOrdinal("Facebook")].ToString(),
                                    DesInvitadoPor = oIDataReader[oIDataReader.GetOrdinal("InvitadoPor")].ToString(),
                                    Codigo_InvitadoPor = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Codigo_InvitadoPor")]),
                                    Direccion = oIDataReader[oIDataReader.GetOrdinal("Direccion")].ToString(),
                                    Ocupacion = oIDataReader[oIDataReader.GetOrdinal("Ocupacion")].ToString(),
                                    TipoDocumento = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("TipoDocumento")]),
                                    NroDias = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("NroDias")]),
                                    NroDiasActual = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("NroDiasActual")]),
                                    DescFechaInicio = oIDataReader[oIDataReader.GetOrdinal("FechaInicio")].ToString(),
                                    DescFechaFin = oIDataReader[oIDataReader.GetOrdinal("FechaFin")].ToString(),
                                    InvitadoPor = oIDataReader[oIDataReader.GetOrdinal("InvitadoPor")].ToString(),
                                    DescFechaCreacion = oIDataReader[oIDataReader.GetOrdinal("desFechaCreacion")].ToString(),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString(),
                                    CodigoPaquete = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoPaquete")]),
                                    TipoCliente = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("TipoCliente")]),
                                    CodigoTiempo = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoTiempo")]),
                                    CodigoSubProcedencia = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSubProcedencia")]),
                                    CodigoObjetivo = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoObjetivo")]),
                                    Precio = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Precio")])
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;            
		}

		public void Registrar(InvitadosDTO item)
		{
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspRegistrarInvitados", conn))
                {    
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoInvitado", System.Data.SqlDbType.Int)).Value = item.CodigoInvitado;
                    cmd.Parameters.Add(new SqlParameter("@Nombres", System.Data.SqlDbType.VarChar, 100)).Value = item.Nombres;
                    cmd.Parameters.Add(new SqlParameter("@Apellidos", System.Data.SqlDbType.VarChar,100)).Value = item.Apellidos;
                    cmd.Parameters.Add(new SqlParameter("@DNI", System.Data.SqlDbType.VarChar, 20)).Value = item.DNI;
                    cmd.Parameters.Add(new SqlParameter("@Telefono", System.Data.SqlDbType.VarChar,20)).Value = item.Telefono;

                    cmd.Parameters.Add(new SqlParameter("@Celular", System.Data.SqlDbType.VarChar,20)).Value = item.Celular;
                    cmd.Parameters.Add(new SqlParameter("@Correo", System.Data.SqlDbType.VarChar, 100)).Value = item.Correo;
                    cmd.Parameters.Add(new SqlParameter("@FechaNacimiento", System.Data.SqlDbType.DateTime)).Value = item.FechaNacimiento;
                    cmd.Parameters.Add(new SqlParameter("@ImagenUrl", System.Data.SqlDbType.VarChar, 200)).Value = item.ImagenUrl;
                    cmd.Parameters.Add(new SqlParameter("@Estado", System.Data.SqlDbType.Bit)).Value = item.Estado;

                    cmd.Parameters.Add(new SqlParameter("@Genero", System.Data.SqlDbType.VarChar,1)).Value = item.Genero;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;
                    cmd.Parameters.Add(new SqlParameter("@Facebook", System.Data.SqlDbType.VarChar,100)).Value = item.Facebook;
                    cmd.Parameters.Add(new SqlParameter("@Codigo_InvitadoPor", System.Data.SqlDbType.Int)).Value = item.Codigo_InvitadoPor;
                    cmd.Parameters.Add(new SqlParameter("@InvitadoPor", System.Data.SqlDbType.VarChar,100)).Value = item.InvitadoPor;
                    
                    cmd.Parameters.Add(new SqlParameter("@Direccion", System.Data.SqlDbType.VarChar,100)).Value = item.Direccion;
                    cmd.Parameters.Add(new SqlParameter("@Distrito", System.Data.SqlDbType.VarChar, 100)).Value = item.Distrito;
                    cmd.Parameters.Add(new SqlParameter("@Ocupacion", System.Data.SqlDbType.VarChar,100)).Value = item.Ocupacion;
                    cmd.Parameters.Add(new SqlParameter("@TipoCliente", System.Data.SqlDbType.Int)).Value = item.TipoCliente;
                    cmd.Parameters.Add(new SqlParameter("@Ubigeo", System.Data.SqlDbType.VarChar,50)).Value = item.Ubicaciones;

                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@TipoDocumento", System.Data.SqlDbType.Int)).Value = item.TipoDocumento;
                    cmd.Parameters.Add(new SqlParameter("@Observacion", System.Data.SqlDbType.VarChar, 200)).Value = item.Observacion;
                    cmd.Parameters.Add(new SqlParameter("@NroDias", System.Data.SqlDbType.Int)).Value = item.NroDias;
                    cmd.Parameters.Add(new SqlParameter("@NroDiasActual", System.Data.SqlDbType.Int)).Value = item.NroDiasActual;

                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = item.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = item.FechaFin;
                    cmd.Parameters.Add(new SqlParameter("@CodigoPaquete", System.Data.SqlDbType.Int)).Value = item.CodigoPaquete;
                    cmd.Parameters.Add(new SqlParameter("@Vendedor", System.Data.SqlDbType.VarChar,100)).Value = item.Vendedor;
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
        
        public int uspActualizarInvitadoASocio(InvitadosDTO item)
        {
            int? campoRetorno = 0;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspActualizarInvitadoASocio", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CodigoSocio", 0).Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@CodigoInvitado", System.Data.SqlDbType.Int)).Value = item.CodigoInvitado;
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
        
		public void Eliminar(InvitadosDTO item)
		{
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspEliminarInvitados", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoInvitado", System.Data.SqlDbType.Int)).Value = item.CodigoInvitado;
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
