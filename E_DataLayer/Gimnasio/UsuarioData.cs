using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using E_DataModel.Gimnasio;
using E_DataModel.Common;
using System.Data.SqlClient;

namespace E_DataLayer.Gimnasio
{
	public class UsuarioData
	{   

        public List<UsuarioDTO> Listar(UsuarioDTO oItem)
		{
            List<UsuarioDTO> lista = new List<UsuarioDTO>();
            
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SEGListarUsuario", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oItem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oItem.CodigoSede;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new UsuarioDTO()
                                {
                                    CodigoUsuario = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUsuario")]),
                                    NombreCompleto = oIDataReader[oIDataReader.GetOrdinal("NombreCompleto")].ToString(),
                                    CodigoPerfil = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoPerfil")]),
                                    DesPerfil = oIDataReader[oIDataReader.GetOrdinal("DesPerfil")].ToString(),
                                    Password = oIDataReader[oIDataReader.GetOrdinal("Password")].ToString(),
                                    imagenUrl = oIDataReader[oIDataReader.GetOrdinal("imagenUrl")].ToString(),
                                    Mail = oIDataReader[oIDataReader.GetOrdinal("Mail")].ToString(),
                                    Telefono = oIDataReader[oIDataReader.GetOrdinal("Telefono")].ToString(),
                                    Estado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Estado")]),
                                    DesEstado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Estado")]) ? "Activo" : "Inactivo"                                  
                                });
                            }
                        }

                    }
                }
            }
            return lista;
          
		}

        public List<UsuarioDTO> SEGListarUsuarioVendedorPrimerDiaMesConfiguracionMetas(UsuarioDTO oItem)
		{
            List<UsuarioDTO> lista = new List<UsuarioDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SEGListarUsuarioVendedorPrimerDiaMesConfiguracionMetas", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oItem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oItem.CodigoSede;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new UsuarioDTO()
                                {
                                    CodigoUsuario = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUsuario")]),
                                    NombreCompleto = oIDataReader[oIDataReader.GetOrdinal("NombreCompleto")].ToString(),
                                    CodigoPerfil = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoPerfil")]),
                                    Estado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Estado")])
                                });
                            }
                        }

                    }
                }
            }
            return lista;
            
		}

        public List<UsuarioDTO> SEGListarUsuarioResponsableSuplementos(UsuarioDTO oItem)
		{
            List<UsuarioDTO> lista = new List<UsuarioDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SEGListarUsuarioResponsableSuplementos", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oItem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oItem.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Filtro", System.Data.SqlDbType.VarChar,100)).Value = oItem.filtro;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new UsuarioDTO()
                                {
                                    CodigoUsuario = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUsuario")]),
                                    NombreCompleto = oIDataReader[oIDataReader.GetOrdinal("NombreCompleto")].ToString(),
                                    CodigoPerfil = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoPerfil")]),
                                    DesPerfil = oIDataReader[oIDataReader.GetOrdinal("DesPerfil")].ToString(),
                                    Password = oIDataReader[oIDataReader.GetOrdinal("Password")].ToString(),
                                    imagenUrl = oIDataReader[oIDataReader.GetOrdinal("imagenUrl")].ToString(),
                                    Mail = oIDataReader[oIDataReader.GetOrdinal("Mail")].ToString(),
                                    Telefono = oIDataReader[oIDataReader.GetOrdinal("Telefono")].ToString(),
                                    Estado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Estado")]),
                                    DesEstado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Estado")]) ? "Activo" : "Inactivo"
                                });
                            }
                        }

                    }
                }
            }
            return lista;
            
		}

        public List<UsuarioDTO> SEGListarUsuarioPorPerfil(UsuarioDTO oItem)
		{
            List<UsuarioDTO> lista = new List<UsuarioDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SEGListarUsuarioPorPerfil", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oItem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oItem.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoPerfil", System.Data.SqlDbType.Int)).Value = oItem.CodigoPerfil;
                    cmd.Parameters.Add(new SqlParameter("@Estado", System.Data.SqlDbType.Bit)).Value = oItem.Estado;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new UsuarioDTO()
                                {
                                    CodigoUsuario = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUsuario")]),
                                    NombreCompleto = oIDataReader[oIDataReader.GetOrdinal("NombreCompleto")].ToString(),
                                    CodigoPerfil = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoPerfil")]),
                                    DesPerfil = oIDataReader[oIDataReader.GetOrdinal("DesPerfil")].ToString(),
                                    Nombres = oIDataReader[oIDataReader.GetOrdinal("Nombre")].ToString(),
                                    Apellidos = oIDataReader[oIDataReader.GetOrdinal("Apellido")].ToString(),
                                    Password = oIDataReader[oIDataReader.GetOrdinal("Password")].ToString(),
                                    imagenUrl = oIDataReader[oIDataReader.GetOrdinal("imagenUrl")].ToString(),
                                    Mail = oIDataReader[oIDataReader.GetOrdinal("Mail")].ToString(),
                                    Telefono = oIDataReader[oIDataReader.GetOrdinal("Telefono")].ToString(),
                                    Estado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Estado")]),
                                    DesEstado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Estado")]) ? "Activo" : "Inactivo"
                                });
                            }
                        }

                    }
                }
            }
            return lista;            
		}

        public List<UsuarioDTO> Exportar_SEGListarUsuarioPorPerfil(UsuarioDTO oItem)
        {
            List<UsuarioDTO> lista = new List<UsuarioDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("Exportar_SEGListarUsuarioPorPerfil", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oItem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oItem.CodigoSede;
                   
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new UsuarioDTO()
                                {
                                    CodigoUsuario = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUsuario")]),
                                    NombreCompleto = oIDataReader[oIDataReader.GetOrdinal("NombreCompleto")].ToString(),
                                    CodigoPerfil = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoPerfil")]),
                                    DesPerfil = oIDataReader[oIDataReader.GetOrdinal("DesPerfil")].ToString(),
                                    Password = oIDataReader[oIDataReader.GetOrdinal("Clave")].ToString(),
                                    Mail = oIDataReader[oIDataReader.GetOrdinal("Mail")].ToString(),
                                    Telefono = oIDataReader[oIDataReader.GetOrdinal("Telefono")].ToString(),
                                    Estado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Estado")]),                                   
                                    FechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")]),
                                    DesEstado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Estado")]) ? "Activo" : "Inactivo"
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }


        public List<UsuarioDTO> SEGListarUsuario_HacerContrato(UsuarioDTO oItem)
        {
            List<UsuarioDTO> lista = new List<UsuarioDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SEGListarUsuario_HacerContrato", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oItem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oItem.CodigoSede;
                   
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new UsuarioDTO()
                                {
                                    CodigoUsuario = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUsuario")]),
                                    NombreCompleto = oIDataReader[oIDataReader.GetOrdinal("NombreCompleto")].ToString(),
                                    CodigoPerfil = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoPerfil")]),
                                    DesPerfil = oIDataReader[oIDataReader.GetOrdinal("DesPerfil")].ToString(),
                                    Password = oIDataReader[oIDataReader.GetOrdinal("Password")].ToString(),
                                    imagenUrl = oIDataReader[oIDataReader.GetOrdinal("imagenUrl")].ToString(),
                                    Mail = oIDataReader[oIDataReader.GetOrdinal("Mail")].ToString(),
                                    Telefono = oIDataReader[oIDataReader.GetOrdinal("Telefono")].ToString(),
                                    Estado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Estado")]),
                                    DesEstado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Estado")]) ? "Activo" : "Inactivo"
                                });
                            }
                        }

                    }
                }
            }
            return lista;            
        }

        public List<UsuarioDTO> SEGListarUsuario_TrainnerActivos(UsuarioDTO oItem)
        {
            List<UsuarioDTO> lista = new List<UsuarioDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SEGListarUsuario_TrainnerActivos", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oItem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oItem.CodigoSede;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new UsuarioDTO()
                                {
                                    CodigoUsuario = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUsuario")]),
                                    NombreCompleto = oIDataReader[oIDataReader.GetOrdinal("NombreCompleto")].ToString(),
                                    CodigoPerfil = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoPerfil")]),
                                    DesPerfil = oIDataReader[oIDataReader.GetOrdinal("DesPerfil")].ToString(),
                                    Password = oIDataReader[oIDataReader.GetOrdinal("Password")].ToString(),
                                    imagenUrl = oIDataReader[oIDataReader.GetOrdinal("imagenUrl")].ToString(),
                                    Mail = oIDataReader[oIDataReader.GetOrdinal("Mail")].ToString(),
                                    Telefono = oIDataReader[oIDataReader.GetOrdinal("Telefono")].ToString(),
                                    Estado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Estado")]),
                                    DesEstado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Estado")]) ? "Activo" : "Inactivo"
                                });
                            }
                        }

                    }
                }
            }
            return lista;            
        }

        public List<UsuarioDTO> SEGListarUsuario_NutricionistasActivos(UsuarioDTO oItem)
        {
            List<UsuarioDTO> lista = new List<UsuarioDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SEGListarUsuario_NutricionistasActivos", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oItem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oItem.CodigoSede;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new UsuarioDTO()
                                {
                                    CodigoUsuario = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUsuario")]),
                                    NombreCompleto = oIDataReader[oIDataReader.GetOrdinal("NombreCompleto")].ToString(),
                                    CodigoPerfil = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoPerfil")]),
                                    DesPerfil = oIDataReader[oIDataReader.GetOrdinal("DesPerfil")].ToString(),
                                    Password = oIDataReader[oIDataReader.GetOrdinal("Password")].ToString(),
                                    imagenUrl = oIDataReader[oIDataReader.GetOrdinal("imagenUrl")].ToString(),
                                    Mail = oIDataReader[oIDataReader.GetOrdinal("Mail")].ToString(),
                                    Telefono = oIDataReader[oIDataReader.GetOrdinal("Telefono")].ToString(),
                                    Estado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Estado")]),
                                    DesEstado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Estado")]) ? "Activo" : "Inactivo"
                                });
                            }
                        }

                    }
                }
            }
            return lista;            
        }

        public List<UsuarioDTO> SEGListarUsuario_AgendaComercial(UsuarioDTO oItem)
        {
            List<UsuarioDTO> lista = new List<UsuarioDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SEGListarUsuario_AgendaComercial", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oItem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oItem.CodigoSede;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new UsuarioDTO()
                                {
                                    CodigoUsuario = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUsuario")]),
                                    NombreCompleto = oIDataReader[oIDataReader.GetOrdinal("NombreCompleto")].ToString(),
                                    CodigoPerfil = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoPerfil")]),
                                    DesPerfil = oIDataReader[oIDataReader.GetOrdinal("DesPerfil")].ToString(),
                                    Password = oIDataReader[oIDataReader.GetOrdinal("Password")].ToString(),
                                    imagenUrl = oIDataReader[oIDataReader.GetOrdinal("imagenUrl")].ToString(),
                                    Mail = oIDataReader[oIDataReader.GetOrdinal("Mail")].ToString(),
                                    Telefono = oIDataReader[oIDataReader.GetOrdinal("Telefono")].ToString(),
                                    Estado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Estado")]),
                                    DesEstado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Estado")]) ? "Activo" : "Inactivo"
                                });
                            }
                        }

                    }
                }
            }
            return lista;            
        }

        public List<UsuarioDTO> ListarAsesoresVentasAcuentaVentas(UsuarioDTO oItem)
		{
            List<UsuarioDTO> lista = new List<UsuarioDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarAsesoresVentasAcuentaVentas", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oItem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oItem.CodigoSede;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new UsuarioDTO()
                                {
                                    CodigoUsuario = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUsuario")]),
                                    NombreCompleto = oIDataReader[oIDataReader.GetOrdinal("NombreCompleto")].ToString(),
                                    CodigoPerfil = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoPerfil")]), 
                                });
                            }
                        }

                    }
                }
            }
            return lista;            
		}

        public List<UsuarioDTO> usplistardllCreadoPor(UsuarioDTO oItem)
		{
            List<UsuarioDTO> lista = new List<UsuarioDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("usplistardllCreadoPor", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oItem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oItem.CodigoSede;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new UsuarioDTO()
                                {
                                    CodigoUsuario = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUsuario")]),
                                    NombreCompleto = oIDataReader[oIDataReader.GetOrdinal("NombreCompleto")].ToString(),
                                    CodigoPerfil = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoPerfil")]),
                                });
                            }
                        }

                    }
                }
            }
            return lista;
		}
     
        public List<UsuarioDTO> ListarPerfilesFiltro(UsuarioDTO oUsuarioDTO)
        {
            List<UsuarioDTO> lista = new List<UsuarioDTO>();
            
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarPerfilesFiltro", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oUsuarioDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oUsuarioDTO.CodigoSede;
                   
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new UsuarioDTO()
                                {
                                    Codigo = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Codigo")]),
                                    CodigoUsuario = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUsuario")]),
                                    DescripcionPerfil = oIDataReader[oIDataReader.GetOrdinal("DescripcionPerfil")].ToString(),
                                    NombreCompleto = oIDataReader[oIDataReader.GetOrdinal("NombreCompleto")].ToString()
                                });

                            }
                        }

                    }
                }
            }
           
            return lista;
        }

        public List<UsuarioDTO> ListarVendedoresMigraciones(UsuarioDTO oUsuarioDTO)
        {
            List<UsuarioDTO> lista = new List<UsuarioDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarAsesoresMigracion", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oUsuarioDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oUsuarioDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@ind", System.Data.SqlDbType.Char,1)).Value = oUsuarioDTO.indMigracion;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new UsuarioDTO()
                                {
                                    CodigoUsuario = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUsuario")]),
                                    NombreCompleto = oIDataReader[oIDataReader.GetOrdinal("NombreCompleto")].ToString()
                                });

                            }
                        }

                    }
                }
            }

            return lista;            
        }

        public List<UsuarioDTO> ListarUsuariosConFiltroVenta(UsuarioDTO oUsuarioDTO)
        {
            List<UsuarioDTO> lista = new List<UsuarioDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarUsuariosConFiltroVenta", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oUsuarioDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oUsuarioDTO.CodigoSede;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new UsuarioDTO()
                                {
                                    CodigoUsuario = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUsuario")]),
                                    NombreCompleto = oIDataReader[oIDataReader.GetOrdinal("NombreCompleto")].ToString(),
                                    NombrePerfil = oIDataReader[oIDataReader.GetOrdinal("NombrePerfil")].ToString(),
                                    CodigoPerfil = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoPerfil")])                                    
                                });

                            }
                        }

                    }
                }
            }
            return lista;            
        }

       
        public UsuarioDTO BuscarInformacionDelUsuario(UsuarioDTO oItem)
        {
            UsuarioDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspBuscarInformacionDelUsuario", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oItem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oItem.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Usuario", System.Data.SqlDbType.VarChar,100)).Value = oItem.NombreCompleto;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new UsuarioDTO()
                                {
                                    NombreSede = oIDataReader[oIDataReader.GetOrdinal("RazonSocial")].ToString(),
                                    NombreCompleto = oIDataReader[oIDataReader.GetOrdinal("Nombre")].ToString(),
                                    NombrePerfil = oIDataReader[oIDataReader.GetOrdinal("Perfil")].ToString(),
                                    imagenUrl = oIDataReader[oIDataReader.GetOrdinal("Logo")].ToString()
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;         
        }
        
        public UsuarioDTO BuscarPorCodigoUsuario(UsuarioDTO oItem)
		{
            UsuarioDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SEGBuscarUsuarioPorCodigoUsuario", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oItem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oItem.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUsuario", System.Data.SqlDbType.Int)).Value = oItem.CodigoUsuario;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new UsuarioDTO()
                                {
                                    CodigoUsuario = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUsuario")]),
                                    CodigoPerfil = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoPerfil")]),
                                    NombreCompleto = oIDataReader[oIDataReader.GetOrdinal("NombreCompleto")].ToString(), 
                                    Password = oIDataReader[oIDataReader.GetOrdinal("Password")].ToString(),
                                    imagenUrl = oIDataReader[oIDataReader.GetOrdinal("imagenUrl")].ToString(),
                                    Mail = oIDataReader[oIDataReader.GetOrdinal("Mail")].ToString(),
                                    Telefono = oIDataReader[oIDataReader.GetOrdinal("Telefono")].ToString(), 
                                    Estado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Estado")]),
                                    Nombres = oIDataReader[oIDataReader.GetOrdinal("Nombre")].ToString(),
                                    Apellidos = oIDataReader[oIDataReader.GetOrdinal("Apellido")].ToString(),
                                    DNI = oIDataReader[oIDataReader.GetOrdinal("Dni")].ToString()                                  
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;            
		}

        public UsuarioDTO uspValidarUsuarioIngresado(UsuarioDTO oItem)
		{
            UsuarioDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspValidarUsuarioIngresado", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oItem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oItem.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Vendedor", System.Data.SqlDbType.VarChar,100)).Value = oItem.Vendedor;
                    cmd.Parameters.Add(new SqlParameter("@Clave", System.Data.SqlDbType.VarChar,50)).Value = oItem.Contrasenia;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new UsuarioDTO()
                                {
                                    ValidarExisteVendedorActivo = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("ValidarExisteVendedorActivo")]),
                                    ValidacionUsuario = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("ValidacionUsuario")])                                    
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;
		}

        public UsuarioDTO uspValidarExisteCita_Usuario_AgendaGeneral(UsuarioDTO oItem)
		{
            UsuarioDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspValidarExisteCita_Usuario_AgendaGeneral", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSocio", System.Data.SqlDbType.Int)).Value = oItem.CodigoSocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoTipoAgenda", System.Data.SqlDbType.Int)).Value = oItem.CodigoTipoAgenda;
                    cmd.Parameters.Add(new SqlParameter("@CodSede", System.Data.SqlDbType.Int)).Value = oItem.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oItem.CodigoUnidadNegocio;                    
                    cmd.Parameters.Add(new SqlParameter("@Usuario", System.Data.SqlDbType.VarChar, 100)).Value = oItem.Usuario;
                    cmd.Parameters.Add(new SqlParameter("@Clave", System.Data.SqlDbType.VarChar, 50)).Value = oItem.Contrasenia;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new UsuarioDTO()
                                {
                                    ValidacionExisteCita = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("ValidacionExisteCita")]),
                                    ValidacionUsuario = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("ValidacionUsuario")])
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;            
		}


        public UsuarioDTO ValidarUsuarioLogeo(UsuarioDTO oItem)
		{
            UsuarioDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspValidarUsuarioLogeo", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                 
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oItem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@Sede", System.Data.SqlDbType.Int)).Value = oItem.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@NombreCompleto", System.Data.SqlDbType.VarChar, 100)).Value = oItem.Usuario;
                    cmd.Parameters.Add(new SqlParameter("@Password", System.Data.SqlDbType.VarChar, 50)).Value = oItem.Contrasenia;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new UsuarioDTO()
                                {
                                    CodigoUsuario = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Codigo")]),
                                    CodigoPerfil = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoPerfil")]),
                                    CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    Nombres = oIDataReader[oIDataReader.GetOrdinal("Nombre")].ToString(),
                                    Apellidos = oIDataReader[oIDataReader.GetOrdinal("Apellido")].ToString(),
                                    DNI = oIDataReader[oIDataReader.GetOrdinal("DNI")].ToString()                                  
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;            
		}

        public UsuarioDTO uspValidarConfiguracionUsuarios(UsuarioDTO oItem)
        {
            UsuarioDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspValidarConfiguracionUsuarios", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@Usuario", System.Data.SqlDbType.VarChar, 100)).Value = oItem.Usuario;
                    cmd.Parameters.Add(new SqlParameter("@Clave", System.Data.SqlDbType.VarChar, 50)).Value = oItem.Contrasenia;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new UsuarioDTO()
                                {
                                    CodigoUsuarioConfiguracion = oIDataReader[oIDataReader.GetOrdinal("Codigo")].ToString(),                                   
                                    Nombres = oIDataReader[oIDataReader.GetOrdinal("Nombres")].ToString(),
                                    Apellidos = oIDataReader[oIDataReader.GetOrdinal("Apellidos")].ToString(),
                                    Usuario = oIDataReader[oIDataReader.GetOrdinal("Usuario")].ToString()
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;
        }

        public void Registrar(UsuarioDTO item)
		{
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SEGRegistrarUsuario", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUsuario ", System.Data.SqlDbType.Int)).Value = item.CodigoUsuario;
                    cmd.Parameters.Add(new SqlParameter("@CodigoPerfil", System.Data.SqlDbType.Int)).Value = item.CodigoPerfil;
                    cmd.Parameters.Add(new SqlParameter("@NombreCompleto ", System.Data.SqlDbType.VarChar, 100)).Value = item.NombreCompleto;
                    cmd.Parameters.Add(new SqlParameter("@Password", System.Data.SqlDbType.VarChar, 50)).Value = item.Password;
                    cmd.Parameters.Add(new SqlParameter("@Mail ", System.Data.SqlDbType.VarChar, 100)).Value = item.Mail;

                    cmd.Parameters.Add(new SqlParameter("@Telefono ", System.Data.SqlDbType.VarChar,100)).Value = item.Telefono;
                    cmd.Parameters.Add(new SqlParameter("@imagenURL", System.Data.SqlDbType.VarChar,200)).Value = item.imagenUrl;
                    cmd.Parameters.Add(new SqlParameter("@Estado ", System.Data.SqlDbType.Bit)).Value = item.Estado;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 50)).Value = item.UsuarioCreacion;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede ", System.Data.SqlDbType.Int)).Value = item.CodigoSede;

                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio ", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@Nombre", System.Data.SqlDbType.VarChar, 100)).Value = item.Nombres;
                    cmd.Parameters.Add(new SqlParameter("@Apellidos ", System.Data.SqlDbType.VarChar,100)).Value = item.Apellidos;
                    cmd.Parameters.Add(new SqlParameter("@DNI", System.Data.SqlDbType.VarChar, 20)).Value = item.DNI;
                    cmd.Parameters.Add(new SqlParameter("@CodigoInicioSesion ", System.Data.SqlDbType.Int)).Value = item.CodigoInicioSesion;


                    cmd.ExecuteNonQuery();
                }
            }            
		}

		public void Actualizar(UsuarioDTO item)
		{
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SEGActualizarUsuario", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio ", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUsuario ", System.Data.SqlDbType.Int)).Value = item.CodigoUsuario;
                    cmd.Parameters.Add(new SqlParameter("@CodigoPerfil", System.Data.SqlDbType.Int)).Value = item.CodigoPerfil;
                    cmd.Parameters.Add(new SqlParameter("@NombreCompleto ", System.Data.SqlDbType.VarChar, 100)).Value = item.NombreCompleto;

                    cmd.Parameters.Add(new SqlParameter("@Password ", System.Data.SqlDbType.VarChar, 50)).Value = item.Password;
                    cmd.Parameters.Add(new SqlParameter("@Mail", System.Data.SqlDbType.VarChar, 200)).Value = item.Mail;
                    cmd.Parameters.Add(new SqlParameter("@Telefono ", System.Data.SqlDbType.VarChar,100)).Value = item.Telefono;
                    cmd.Parameters.Add(new SqlParameter("@Estado", System.Data.SqlDbType.Bit, 50)).Value = item.Estado;
                    cmd.Parameters.Add(new SqlParameter("@imagenURL ", System.Data.SqlDbType.VarChar,200)).Value = item.imagenUrl;

                    cmd.Parameters.Add(new SqlParameter("@UsuarioEdicion ", System.Data.SqlDbType.VarChar,50)).Value = item.UsuarioEdicion;
                    cmd.Parameters.Add(new SqlParameter("@Nombre", System.Data.SqlDbType.VarChar, 100)).Value = item.Nombres;
                    cmd.Parameters.Add(new SqlParameter("@Apellidos ", System.Data.SqlDbType.VarChar, 100)).Value = item.Apellidos;
                    cmd.Parameters.Add(new SqlParameter("@DNI", System.Data.SqlDbType.VarChar, 20)).Value = item.DNI;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion ", System.Data.SqlDbType.VarChar,100)).Value = item.UsuarioCreacion;

                    cmd.Parameters.Add(new SqlParameter("@CodigoInicioSesion ", System.Data.SqlDbType.Int)).Value = item.CodigoInicioSesion;


                    cmd.ExecuteNonQuery();
                }
            }
        
		}

        public void uspActualizarDatosUsuarios(UsuarioDTO item)
		{
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspActualizarDatosUsuarios", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio ", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUsuario ", System.Data.SqlDbType.Int)).Value = item.CodigoUsuario;
                    cmd.Parameters.Add(new SqlParameter("@Nombre", System.Data.SqlDbType.VarChar,100)).Value = item.Nombres;
                    cmd.Parameters.Add(new SqlParameter("@Apellidos ", System.Data.SqlDbType.VarChar, 100)).Value = item.Apellidos;
                    cmd.Parameters.Add(new SqlParameter("@DNI ", System.Data.SqlDbType.VarChar,20)).Value = item.DNI;
                    
                    cmd.ExecuteNonQuery();
                }
            }
		}

        public int ActualizarClave(UsuarioDTO item)
        {
            int ? campoRetorno = 0;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SEGActualizarClaveUsuario", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio ", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUsuario ", System.Data.SqlDbType.Int)).Value = item.CodigoUsuario;
                    cmd.Parameters.Add(new SqlParameter("@PasswordNueva", System.Data.SqlDbType.VarChar, 50)).Value = item.PasswordNuevo;
                    cmd.Parameters.Add(new SqlParameter("@PasswordAntigua ", System.Data.SqlDbType.VarChar, 50)).Value = item.PasswordAntiguo;

                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion ", System.Data.SqlDbType.VarChar, 50)).Value = item.UsuarioCreacion;
                    cmd.Parameters.Add(new SqlParameter("@CodigoInicioSesion ", System.Data.SqlDbType.Int)).Value = 0;
                    cmd.Parameters.AddWithValue("@Estado", campoRetorno).Direction = System.Data.ParameterDirection.Output;

                    cmd.ExecuteNonQuery();

                    campoRetorno = Convert.ToInt32(cmd.Parameters["@Estado"].Value);
                }
            }
            
            return Convert.ToInt32(campoRetorno);
        }

		public void Eliminar(UsuarioDTO item)
		{
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SEGEliminarUsuario", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio ", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUsuario ", System.Data.SqlDbType.Int)).Value = item.CodigoUsuario;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 50)).Value = item.UsuarioCreacion;
                    cmd.Parameters.Add(new SqlParameter("@CodigoInicioSesion ", System.Data.SqlDbType.Int)).Value = 0;
                    cmd.Parameters.Add(new SqlParameter("@flag ", System.Data.SqlDbType.Int)).Value = item.flag;

                    cmd.ExecuteNonQuery();
                }
            }
		}

        public void uspCambiarEstadoUsuarioConfiguracionNuevoMes(UsuarioDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspCambiarEstadoUsuarioConfiguracionNuevoMes", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio ", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUsuario ", System.Data.SqlDbType.Int)).Value = item.CodigoUsuario;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 50)).Value = item.UsuarioCreacion;
                    cmd.Parameters.Add(new SqlParameter("@CodigoInicioSesion ", System.Data.SqlDbType.Int)).Value = item.CodigoInicioSesion;
                    cmd.Parameters.Add(new SqlParameter("@flag ", System.Data.SqlDbType.Int)).Value = item.flag;

                    cmd.ExecuteNonQuery();
                }
            }            
        }

        public void EliminarFiltro(UsuarioDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspEliminarUsuariosFiltroVenta", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                   
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Codigo ", System.Data.SqlDbType.Int)).Value = item.Codigo;
                  
                    cmd.ExecuteNonQuery();
                }
            }            
        }

        public int uspValidarExisteUsuario(int CodigoUnidadNegocio, int CodSede, string NombreCompleto)
        {
            int? campoRetorno = 0;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspValidarExisteUsuario", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio ", System.Data.SqlDbType.Int)).Value = CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = CodSede;
                    cmd.Parameters.Add(new SqlParameter("@NombreCompleto ", System.Data.SqlDbType.VarChar,100)).Value = NombreCompleto;
                    cmd.Parameters.AddWithValue("@Existe", campoRetorno).Direction = System.Data.ParameterDirection.Output;

                    cmd.ExecuteNonQuery();
                    campoRetorno = Convert.ToInt32(cmd.Parameters["@Existe"].Value);
                }
            }
            return Convert.ToInt32(campoRetorno);
        }


    }
}
