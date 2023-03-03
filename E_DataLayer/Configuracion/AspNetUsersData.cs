using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using E_DataModel.Configuracion;
using E_DataModel.Common;

namespace E_DataLayer.Configuracion
{
    public class AspNetUsersData
    {
        public AspNetUsersDTO ecommerce_AspNetUsers_ValidarUsuarioBusiness(AspNetUsersDTO oFiltro)
        {
            AspNetUsersDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("ecommerce_AspNetUsers_ValidarUsuarioBusiness", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@UserName", System.Data.SqlDbType.VarChar)).Value = oFiltro.UserName;
                    cmd.Parameters.Add(new SqlParameter("@PasswordHash", System.Data.SqlDbType.VarChar)).Value = oFiltro.PasswordHash;
                    cmd.Parameters.Add(new SqlParameter("@Estado", System.Data.SqlDbType.Int)).Direction = ParameterDirection.Output;
                    
                    cmd.ExecuteNonQuery();
                    itemDTO = new AspNetUsersDTO();
                    itemDTO.LoginValidation = Convert.ToInt32(cmd.Parameters["@Estado"].Value);
                }

            }
            return itemDTO;
        }
       
        public AspNetUsersDTO ecommerce_AspNetUsers_ValidarUsuarioPersonaFit(AspNetUsersDTO oFiltro)
        {
            AspNetUsersDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("ecommerce_AspNetUsers_ValidarUsuarioPersonaFit", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@UserName", System.Data.SqlDbType.VarChar)).Value = oFiltro.UserName;
                   // cmd.Parameters.Add(new SqlParameter("@Email", System.Data.SqlDbType.VarChar)).Value = oFiltro.Email;
                    cmd.Parameters.Add(new SqlParameter("@PasswordHash", System.Data.SqlDbType.VarChar)).Value = oFiltro.PasswordHash;
                 
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new AspNetUsersDTO()
                                {
                                    //1 = ¡El usuario y contraseña son correctos!
                                    //2 = ¡La contraseña es incorrecta!
                                    //3 = ¡Este usuario aún no se ha registrado!
                                    LoginValidation = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("LoginValidation")]),
                                    Id = oIDataReader[oIDataReader.GetOrdinal("Id")] == null ? "sinregistro" : oIDataReader[oIDataReader.GetOrdinal("Id")].ToString(),                                    
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;
        }
        
        public AspNetUsersDTO ecommerce_AspNetUsers_ValidarUsuarioPersonaFit_AppFitness(AspNetUsersDTO oFiltro)
        {
            AspNetUsersDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("ecommerce_AspNetUsers_ValidarUsuarioPersonaFit_AppFitness", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;                    
                    cmd.Parameters.Add(new SqlParameter("@UserName", System.Data.SqlDbType.VarChar)).Value = oFiltro.UserName;
                    cmd.Parameters.Add(new SqlParameter("@Email", System.Data.SqlDbType.VarChar)).Value = oFiltro.Email;
                    cmd.Parameters.Add(new SqlParameter("@PasswordHash", System.Data.SqlDbType.VarChar)).Value = oFiltro.PasswordHash;
                    cmd.Parameters.Add(new SqlParameter("@TokenDevice", System.Data.SqlDbType.VarChar)).Value = oFiltro.TokenDevice;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new AspNetUsersDTO()
                                {
                                    //0 = ¡El usuario y contraseña son correctos!
                                    //1 = ¡La contraseña es incorrecta!
                                    //2 = ¡Este usuario aún no se ha registrado!
                                    IdValidation = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("IdValidation")]),
                                    MensajeValidation = oIDataReader[oIDataReader.GetOrdinal("MensajeValidation")].ToString(),
                                    Id = oIDataReader[oIDataReader.GetOrdinal("Id")] == null ? "" : oIDataReader[oIDataReader.GetOrdinal("Id")].ToString(),
                                    EmailConfirmed = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("EmailConfirmed")]),
                                    FullName = oIDataReader[oIDataReader.GetOrdinal("FullName")].ToString()
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;
        }

        public AspNetUsersDTO ecommerce_AspNetUsers_Buscar(AspNetUsersDTO oFiltro)
        {
            AspNetUsersDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("ecommerce_AspNetUsers_Buscar", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Id", System.Data.SqlDbType.VarChar)).Value = oFiltro.Id;
                    
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new AspNetUsersDTO()
                                {
                                    Id = oIDataReader[oIDataReader.GetOrdinal("Id")].ToString(),
                                    FullName = oIDataReader[oIDataReader.GetOrdinal("FullName")].ToString(),
                                    UserName = oIDataReader[oIDataReader.GetOrdinal("UserName")].ToString(),
                                    Photo = oIDataReader[oIDataReader.GetOrdinal("Photo")].ToString(),
                                    DefaultKey = oIDataReader[oIDataReader.GetOrdinal("DefaultKey")].ToString(),
                                    Email = oIDataReader[oIDataReader.GetOrdinal("Email")].ToString(),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString()

                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;
        }

        public List<AspNetUsersDTO> ecommerce_uspListarAspNetUsers_Paginacion(AspNetUsersDTO oFiltro, Paging paging)
        {
            List<AspNetUsersDTO> lista = new List<AspNetUsersDTO>();
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("ecommerce_uspListarAspNetUsers_Paginacion", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoSede;
                    if (oFiltro.FullName == string.Empty || oFiltro.FullName == null)
                    {
                        cmd.Parameters.Add(new SqlParameter("@Buscador", System.Data.SqlDbType.VarChar)).Value = DBNull.Value;
                    }
                    else
                    {
                        cmd.Parameters.Add(new SqlParameter("@Buscador", System.Data.SqlDbType.VarChar)).Value = oFiltro.FullName;
                    }

                    cmd.Parameters.Add(new SqlParameter("@PaginaActual", System.Data.SqlDbType.Int)).Value = paging.PageNumber;
                    cmd.Parameters.Add(new SqlParameter("@TamanioPagina", System.Data.SqlDbType.Int)).Value = paging.PageRecords;
                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;
                    
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new AspNetUsersDTO()
                                {
                                    CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    UserType = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("UserType")]),
                                    Id = oIDataReader[oIDataReader.GetOrdinal("Id")].ToString(),
                                    FullName = oIDataReader[oIDataReader.GetOrdinal("FullName")].ToString(),
                                    UserName = oIDataReader[oIDataReader.GetOrdinal("UserName")].ToString(),
                                    PasswordHash = oIDataReader[oIDataReader.GetOrdinal("PasswordHash")].ToString(),
                                    Email = oIDataReader[oIDataReader.GetOrdinal("Email")].ToString(),
                                    EmailConfirmed = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("EmailConfirmed")]),
                                    PhoneNumber = oIDataReader[oIDataReader.GetOrdinal("Email")].ToString(),
                                    PhoneNumberConfirmed = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("PhoneNumberConfirmed")]),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString(),
                                    FechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")]),
                                    DesCargo = oIDataReader[oIDataReader.GetOrdinal("DesCargo")].ToString(),
                                    CodigoCargo = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoCargo")])
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }
        
        public int ecommerce_uspRegistrar_AspNetUsers(AspNetUsersDTO item)
        {
            int LoginValidation = 0;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("ecommerce_uspRegistrar_AspNetUsers", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Validador", System.Data.SqlDbType.Int)).Direction = ParameterDirection.Output;

                    cmd.Parameters.Add(new SqlParameter("@UserType", System.Data.SqlDbType.Int)).Value = item.UserType; 
                    cmd.Parameters.Add(new SqlParameter("@FullName", System.Data.SqlDbType.VarChar)).Value = item.FullName;
                    cmd.Parameters.Add(new SqlParameter("@Nombres", System.Data.SqlDbType.VarChar)).Value = item.Nombres;
                    cmd.Parameters.Add(new SqlParameter("@Apellidos", System.Data.SqlDbType.VarChar)).Value = item.Apellidos;
                    cmd.Parameters.Add(new SqlParameter("@Photo", System.Data.SqlDbType.VarChar)).Value = item.Photo;
                    cmd.Parameters.Add(new SqlParameter("@Identificacion", System.Data.SqlDbType.VarChar)).Value = item.Identificacion;
                    cmd.Parameters.Add(new SqlParameter("@UserName", System.Data.SqlDbType.VarChar)).Value = item.UserName;   
	                cmd.Parameters.Add(new SqlParameter("@PasswordHash", System.Data.SqlDbType.VarChar)).Value = item.PasswordHash;
	                cmd.Parameters.Add(new SqlParameter("@DefaultKey", System.Data.SqlDbType.VarChar)).Value = item.DefaultKey; 
	                cmd.Parameters.Add(new SqlParameter("@Email", System.Data.SqlDbType.VarChar)).Value = item.Email; 
	                cmd.Parameters.Add(new SqlParameter("@EmailConfirmed", System.Data.SqlDbType.Bit)).Value = item.EmailConfirmed; 
                    cmd.Parameters.Add(new SqlParameter("@PhoneNumber", System.Data.SqlDbType.VarChar)).Value = item.PhoneNumber;     
	                cmd.Parameters.Add(new SqlParameter("@PhoneNumberConfirmed", System.Data.SqlDbType.Bit)).Value = item.PhoneNumberConfirmed;
                    cmd.Parameters.Add(new SqlParameter("@SecurityStamp", System.Data.SqlDbType.VarChar)).Value = item.SecurityStamp;   
	                cmd.Parameters.Add(new SqlParameter("@Estate", System.Data.SqlDbType.Int)).Value = item.Estate;    
                    cmd.Parameters.Add(new SqlParameter("@CodigoCargo", System.Data.SqlDbType.Int)).Value = item.CodigoCargo; 
                      
                    if (!string.IsNullOrEmpty(item.UsuarioCreacion))
                    {
                        cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;
                    }
                    cmd.ExecuteNonQuery();
                    LoginValidation = Convert.ToInt32(cmd.Parameters["@Validador"].Value);
                }

            }
            return LoginValidation;
        }
        public string ecommerce_uspRegistrar_AspNetUsers_AppFitness(AspNetUsersDTO item)
        {
            string LoginValidation = string.Empty;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("ecommerce_uspRegistrar_AspNetUsers_AppFitness", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUsuario", System.Data.SqlDbType.VarChar,50)).Direction = ParameterDirection.Output;
                 
                    cmd.Parameters.Add(new SqlParameter("@FullName", System.Data.SqlDbType.VarChar)).Value = item.FullName;
                    cmd.Parameters.Add(new SqlParameter("@Photo", System.Data.SqlDbType.VarChar)).Value = item.Photo;
                    cmd.Parameters.Add(new SqlParameter("@Identificacion", System.Data.SqlDbType.VarChar)).Value = item.Identificacion;
                    cmd.Parameters.Add(new SqlParameter("@UserName", System.Data.SqlDbType.VarChar)).Value = item.UserName;   
	                cmd.Parameters.Add(new SqlParameter("@PasswordHash", System.Data.SqlDbType.VarChar)).Value = item.PasswordHash;	                
	                cmd.Parameters.Add(new SqlParameter("@Email", System.Data.SqlDbType.VarChar)).Value = item.Email; 
                    cmd.Parameters.Add(new SqlParameter("@PhoneNumber", System.Data.SqlDbType.VarChar)).Value = item.PhoneNumber;

                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;

                    cmd.Parameters.Add(new SqlParameter("@TokenDevice", System.Data.SqlDbType.VarChar, 500)).Value = item.UsuarioCreacion;


                    cmd.ExecuteNonQuery();
                    LoginValidation = cmd.Parameters["@CodigoUsuario"].Value.ToString();
                }

            }
            return LoginValidation;
        }

        public void ecommerce_uspValidarCorreo_AspNetUsers_AppFitness(AspNetUsersDTO item)
        {         
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("ecommerce_uspValidarCorreo_AspNetUsers_AppFitness", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;                   
                    cmd.Parameters.Add(new SqlParameter("@IdUser", System.Data.SqlDbType.VarChar)).Value = item.Id;                   
                    cmd.ExecuteNonQuery();
                }

            }
        }

        public void ecommerce_uspRegistrar_AspNetUsersToken_AppFitness(AspNetUsersDTO item)
        {            
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("ecommerce_uspRegistrar_AspNetUsersToken_AppFitness", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                  
                    cmd.Parameters.Add(new SqlParameter("@IdUser", System.Data.SqlDbType.VarChar)).Value = item.Id;
                    cmd.Parameters.Add(new SqlParameter("@DefaultKey", System.Data.SqlDbType.VarChar)).Value = item.DefaultKey;
                                       
                    cmd.ExecuteNonQuery();
                    
                }

            }
        }

        public int ecommerce_uspRegistrar_AspNetUsersTiendaVirtual(AspNetUsersDTO item)
        {
            int LoginValidation = 0;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("ecommerce_uspRegistrar_AspNetUsersTiendaVirtual", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Validador", System.Data.SqlDbType.Int)).Direction = ParameterDirection.Output;

                    cmd.Parameters.Add(new SqlParameter("@UserType", System.Data.SqlDbType.Int)).Value = item.UserType;
                    cmd.Parameters.Add(new SqlParameter("@FullName", System.Data.SqlDbType.VarChar)).Value = item.FullName;
                    cmd.Parameters.Add(new SqlParameter("@Nombres", System.Data.SqlDbType.VarChar)).Value = item.Nombres;
                    cmd.Parameters.Add(new SqlParameter("@Apellidos", System.Data.SqlDbType.VarChar)).Value = item.Apellidos;
                    cmd.Parameters.Add(new SqlParameter("@Photo", System.Data.SqlDbType.VarChar)).Value = item.Photo;
                    cmd.Parameters.Add(new SqlParameter("@UserName", System.Data.SqlDbType.VarChar)).Value = item.UserName;
                    cmd.Parameters.Add(new SqlParameter("@PasswordHash", System.Data.SqlDbType.VarChar)).Value = item.PasswordHash;
                    cmd.Parameters.Add(new SqlParameter("@DefaultKey", System.Data.SqlDbType.VarChar)).Value = item.DefaultKey;
                    cmd.Parameters.Add(new SqlParameter("@Identificacion", System.Data.SqlDbType.VarChar, 100)).Value = item.Identificacion;
                    cmd.Parameters.Add(new SqlParameter("@Email", System.Data.SqlDbType.VarChar)).Value = item.Email;
                    cmd.Parameters.Add(new SqlParameter("@EmailConfirmed", System.Data.SqlDbType.Bit)).Value = item.EmailConfirmed;
                    cmd.Parameters.Add(new SqlParameter("@PhoneNumber", System.Data.SqlDbType.VarChar)).Value = item.PhoneNumber;
                    cmd.Parameters.Add(new SqlParameter("@PhoneNumberConfirmed", System.Data.SqlDbType.Bit)).Value = item.PhoneNumberConfirmed;
                    cmd.Parameters.Add(new SqlParameter("@SecurityStamp", System.Data.SqlDbType.VarChar)).Value = item.SecurityStamp;
                    cmd.Parameters.Add(new SqlParameter("@Estate", System.Data.SqlDbType.Int)).Value = item.Estate;
                    cmd.Parameters.Add(new SqlParameter("@CodigoCargo", System.Data.SqlDbType.Int)).Value = item.CodigoCargo;

                    if (!string.IsNullOrEmpty(item.UsuarioCreacion))
                    {
                        cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;
                    }
                    cmd.ExecuteNonQuery();
                    LoginValidation = Convert.ToInt32(cmd.Parameters["@Validador"].Value);
                }

            }
            return LoginValidation;
        }

        public int ecommerce_uspActualizarCambiarClave_AspNetUsers(AspNetUsersDTO item)
        {
            int resultado = 0;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("ecommerce_uspActualizarCambiarClave_AspNetUsers", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Id", System.Data.SqlDbType.VarChar)).Value = item.Id;
                    cmd.Parameters.Add(new SqlParameter("@PasswordHashActual", System.Data.SqlDbType.VarChar)).Value = item.PasswordHashActual;
                    cmd.Parameters.Add(new SqlParameter("@PasswordHashNueva", System.Data.SqlDbType.VarChar)).Value = item.PasswordHashNueva;
                    if (!string.IsNullOrEmpty(item.UsuarioCreacion))
                    {
                        cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;
                    }
                    cmd.Parameters.Add(new SqlParameter("@Validate", System.Data.SqlDbType.Int)).Direction = ParameterDirection.Output;
                    
                    cmd.ExecuteNonQuery();
                    resultado = Convert.ToInt32(cmd.Parameters["@Validate"].Value);
                }

            }
            return resultado;
        }

        //update pass
        public int ecommerce_uspCambiarClave_AspNetUsers_AppFitness(AspNetUsersDTO item)
        {
            int resultado = 0;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("ecommerce_uspCambiarClave_AspNetUsers_AppFitness", conn))
                { 
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@DefaultKey", System.Data.SqlDbType.VarChar)).Value = item.DefaultKey;
                    cmd.Parameters.Add(new SqlParameter("@UserName", System.Data.SqlDbType.VarChar)).Value = item.UserName;
                    cmd.Parameters.Add(new SqlParameter("@PasswordHash", System.Data.SqlDbType.VarChar)).Value = item.PasswordHash;
                    cmd.Parameters.Add(new SqlParameter("@NewPasswordHash", System.Data.SqlDbType.VarChar, 50)).Value = item.PasswordHashNueva;

                    cmd.Parameters.Add(new SqlParameter("@Validador", System.Data.SqlDbType.Int)).Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    resultado = Convert.ToInt32(cmd.Parameters["@Validador"].Value);
                }

            }
            return resultado;
        }

        //reset pass
        public AspNetUsersDTO ecommerce_uspRecuperarClave_AspNetUsers_AppFitness(AspNetUsersDTO item)
        {
            AspNetUsersDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("ecommerce_uspRecuperarClave_AspNetUsers_AppFitness", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Email", System.Data.SqlDbType.VarChar)).Value = item.Email;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new AspNetUsersDTO()
                                {
                                    UserName = oIDataReader[oIDataReader.GetOrdinal("UserName")].ToString(),
                                    FullName = oIDataReader[oIDataReader.GetOrdinal("FullName")].ToString(),
                                    PasswordHash = oIDataReader[oIDataReader.GetOrdinal("PasswordHash")].ToString(),
                                    MensajeValidation = oIDataReader[oIDataReader.GetOrdinal("MensajeValidacion")].ToString(),
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;
        }



    }
}
