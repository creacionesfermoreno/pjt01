using E_DataModel;
using E_DataModel.Common;
using E_DataModel.Gimnasio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace E_DataLayer
{
    public class NotificacionappData
    {
        //list paginate
        public List<NotificacionDTO> ListPagination(NotificacionDTO item, Paging paging, out uint NRegisters)
        {
            List<NotificacionDTO> lista = new List<NotificacionDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarNotificacionesApp", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;

                    cmd.Parameters.Add(new SqlParameter("@PaginaActual", System.Data.SqlDbType.Int)).Value = paging.PageNumber;
                    cmd.Parameters.Add(new SqlParameter("@TamanioPagina", System.Data.SqlDbType.Int)).Value = paging.PageRecords;


                    SqlParameter TotalRows = cmd.Parameters.Add("@NumeroRegistros", SqlDbType.Int);
                    TotalRows.Direction = ParameterDirection.Output;


                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new NotificacionDTO()
                                {
                                    CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    CodigoNotificacionesApp = oIDataReader[oIDataReader.GetOrdinal("CodigoNotificacionesApp")].ToString(),
                                    TipeNoty = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("TipoEnvio")]),
                                    Title = oIDataReader[oIDataReader.GetOrdinal("Titulo")].ToString(),
                                    Body = oIDataReader[oIDataReader.GetOrdinal("Body")].ToString(),
                                    UrlImage = oIDataReader[oIDataReader.GetOrdinal("UrlImagen")].ToString(),
                                    DescriptionHtml = oIDataReader[oIDataReader.GetOrdinal("DescripcionHtml")].ToString(),
                                    DescFechaCreacion = item.DateParse(Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")])),
                                    Recurrent = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Recurrente")]),
                                    Send = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Enviado")]),
                                    Group = oIDataReader[oIDataReader.GetOrdinal("GrupoPersonas")].ToString(),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString(),
                                });
                            }
                        }

                    }
                    NRegisters = Convert.ToUInt32(TotalRows.Value);
                }
            }

            return lista;
        }



        //search
        public NotificacionDTO SearchNotiApp(NotificacionDTO oNotificacionDTO)
        {
            NotificacionDTO itemDTO = new NotificacionDTO();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspBuscarNotificacionesApp", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oNotificacionDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oNotificacionDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoNotificacionesApp", System.Data.SqlDbType.VarChar)).Value = oNotificacionDTO.CodigoNotificacionesApp;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new NotificacionDTO()
                                {
                                    CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    CodigoNotificacionesApp = oIDataReader[oIDataReader.GetOrdinal("CodigoNotificacionesApp")].ToString(),
                                    TipeNoty = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("TipoEnvio")]),
                                    Title = oIDataReader[oIDataReader.GetOrdinal("Titulo")].ToString(),
                                    Body = oIDataReader[oIDataReader.GetOrdinal("Body")].ToString(),
                                    UrlImage = oIDataReader[oIDataReader.GetOrdinal("UrlImagen")].ToString(),
                                    DescriptionHtml = oIDataReader[oIDataReader.GetOrdinal("DescripcionHtml")].ToString(),
                                    DescFechaCreacion = oNotificacionDTO.DateParse(Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")])),
                                    Recurrent = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Recurrente")]),
                                    Send = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Enviado")]),
                                    Group = oIDataReader[oIDataReader.GetOrdinal("GrupoPersonas")].ToString(),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString(),

                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;
        }


        //register
        public string Registrar(NotificacionDTO item)
        {
            string id = "";

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspRegistrarNotificacionesApp", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoNotificacionesApp", System.Data.SqlDbType.VarChar, 100)).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@TipoEnvio", System.Data.SqlDbType.Int)).Value = item.TipeNoty;
                    cmd.Parameters.Add(new SqlParameter("@GrupoPersonas", System.Data.SqlDbType.VarChar, 50)).Value = item.Group;
                    cmd.Parameters.Add(new SqlParameter("@Titulo", System.Data.SqlDbType.VarChar, 100)).Value = item.Title;
                    cmd.Parameters.Add(new SqlParameter("@Body", System.Data.SqlDbType.VarChar, 100)).Value = item.Body;
                    cmd.Parameters.Add(new SqlParameter("@UrlImagen", System.Data.SqlDbType.VarChar, 200)).Value = item.UrlImage;
                    cmd.Parameters.Add(new SqlParameter("@DescripcionHtml", System.Data.SqlDbType.VarChar, 2000)).Value = item.DescriptionHtml;
                    cmd.Parameters.Add(new SqlParameter("@Recurrente", System.Data.SqlDbType.Bit)).Value = item.Recurrent;
                    cmd.Parameters.Add(new SqlParameter("@Enviado", System.Data.SqlDbType.Bit)).Value = item.Send;

                    if (!string.IsNullOrEmpty(item.UsuarioCreacion))
                    {
                        cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;
                    }
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    id = cmd.Parameters["@CodigoNotificacionesApp"].Value.ToString();
                }
            }
            return id;
        }


        //public void Actualizar(NotificacionDTO item)
        //{
        //    using (var conn = new SqlConnection(Helper.Conexion()))
        //    {
        //        conn.Open();
        //        using (var cmd = new SqlCommand("uspActualizarNotificacionesApp", conn))
        //        {
        //            cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //            cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
        //            cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
        //            cmd.Parameters.Add(new SqlParameter("@CodigoNotificacionesApp", System.Data.SqlDbType.VarChar)).Value = item.CodigoNotificacionesApp;
        //            cmd.Parameters.Add(new SqlParameter("@TipoEnvio", System.Data.SqlDbType.Int)).Value = item.TipoEnvio;
        //            cmd.Parameters.Add(new SqlParameter("@Asunto", System.Data.SqlDbType.VarChar)).Value = item.Asunto;
        //            cmd.Parameters.Add(new SqlParameter("@FechaHoraEnvio", System.Data.SqlDbType.DateTime)).Value = item.@FechaHoraEnvio;
        //            cmd.Parameters.Add(new SqlParameter("@Mensaje", System.Data.SqlDbType.VarChar)).Value = item.Mensaje;
        //            cmd.Parameters.Add(new SqlParameter("@Recurrente", System.Data.SqlDbType.Bit)).Value = item.Recurrente;
        //            cmd.Parameters.Add(new SqlParameter("@Estado", System.Data.SqlDbType.Bit)).Value = item.Estado;
        //            cmd.Parameters.Add(new SqlParameter("@GrupoPersonas", System.Data.SqlDbType.Int)).Value = item.GrupoPersonas;
        //            if (!string.IsNullOrEmpty(item.UsuarioEdicion))
        //            {
        //                cmd.Parameters.Add(new SqlParameter("@UsuarioEdicion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioEdicion;
        //            }
        //            cmd.ExecuteNonQuery();
        //        }
        //    }

        //}


        //update send
        public void UpdateSend(NotificacionDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspActualizarNotificacionesAppEstado", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoNotificacionesApp", System.Data.SqlDbType.VarChar)).Value = item.CodigoNotificacionesApp;
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }



        //destroy
        public void Destroy(NotificacionDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspEliminarNotificacionesApp", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoNotificacionesApp", System.Data.SqlDbType.VarChar)).Value = item.CodigoNotificacionesApp;
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }


        //****************************************************************** REGISTER ADDRESS **********************************************

        public void RegisterDetail(NotificacionDTO item)
        {
           
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspRegistrarNotificacionesAppDestinatarios", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoNotificacionesApp", System.Data.SqlDbType.VarChar, 100)).Value = item.CodigoNotificacionesApp;
                    cmd.Parameters.Add(new SqlParameter("@CodigoNotificacionesAppDestinatarios", System.Data.SqlDbType.Int, 0)).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@IdUser", System.Data.SqlDbType.VarChar,100)).Value = item.IdUser;

                    cmd.Parameters.Add(new SqlParameter("@Leido", System.Data.SqlDbType.Bit)).Value = item.Read;
                    cmd.Parameters.Add(new SqlParameter("@Enviado", System.Data.SqlDbType.Bit)).Value = item.Send;

                    if (!string.IsNullOrEmpty(item.UsuarioCreacion))
                    {
                        cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;
                    }
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    
                }
            }
            
        }



        //list address paginate
        public List<NotificacionDTO> ListPaginationAddress(NotificacionDTO item, Paging paging, out uint NroRegisters)
        {
            List<NotificacionDTO> lista = new List<NotificacionDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarNotificacionesAppDestinatarios", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoNotificacionesApp", System.Data.SqlDbType.VarChar,100)).Value = item.CodigoNotificacionesApp;

                    cmd.Parameters.Add(new SqlParameter("@PaginaActual", System.Data.SqlDbType.Int)).Value = paging.PageNumber;
                    cmd.Parameters.Add(new SqlParameter("@TamanioPagina", System.Data.SqlDbType.Int)).Value = paging.PageRecords;


                    SqlParameter TotalRows = cmd.Parameters.Add("@NumeroRegistros", SqlDbType.Int);
                    TotalRows.Direction = ParameterDirection.Output;


                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new NotificacionDTO()
                                {
                                   // CodigoNotificacionesApp = oIDataReader[oIDataReader.GetOrdinal("CodigoNotificacionesApp")].ToString(),
                                    Send = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Enviado")]),
                                    Read = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Leido")]),
                                    FullName = oIDataReader[oIDataReader.GetOrdinal("FullName")].ToString(),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString(),
                                });
                            }
                        }

                    }
                    conn.Close();
                    NroRegisters = Convert.ToUInt32(TotalRows.Value);
                }
            }

            return lista;
        }




        //****************************************************************** REGISTER ADDRESS **********************************************


        //**************************************************************    LIST USER TOKENS *************************************************

        //actives
        public List<NotificacionDTO> ListCustomerActive(NotificacionDTO oFiltro)
        {
            List<NotificacionDTO> lista = new List<NotificacionDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspAutomatizaciones_ListarClientesActivos", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Dias", System.Data.SqlDbType.Int)).Value = oFiltro.Days;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new NotificacionDTO()
                                {
                                    CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    TokenDevice = oIDataReader[oIDataReader.GetOrdinal("TokenDevice")].ToString(),
                                    IdUser = oIDataReader[oIDataReader.GetOrdinal("IdUser")].ToString(),
                                });
                            }
                        }
                    }
                }
            }
            return lista;
        }


        //x vencer
        public List<NotificacionDTO> ListCustomerByVencer(NotificacionDTO oFiltro)
        {
            List<NotificacionDTO> lista = new List<NotificacionDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspAutomatizaciones_ListarClientesPorVencer", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Dias", System.Data.SqlDbType.Int)).Value = oFiltro.Days;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new NotificacionDTO()
                                {
                                    CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    TokenDevice = oIDataReader[oIDataReader.GetOrdinal("TokenDevice")].ToString(),
                                    IdUser = oIDataReader[oIDataReader.GetOrdinal("IdUser")].ToString(),
                                });
                            }
                        }
                    }
                }
            }
            return lista;
        }


        //vencids
        public List<NotificacionDTO> ListCustomerVencids(NotificacionDTO oFiltro)
        {
            List<NotificacionDTO> lista = new List<NotificacionDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspAutomatizaciones_ListarClientesVencidos", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Dias", System.Data.SqlDbType.Int)).Value = oFiltro.Days;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new NotificacionDTO()
                                {
                                    CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    TokenDevice = oIDataReader[oIDataReader.GetOrdinal("TokenDevice")].ToString(),
                                    IdUser = oIDataReader[oIDataReader.GetOrdinal("IdUser")].ToString(),
                                });
                            }
                        }
                    }
                }
            }
            return lista;
        }


        //**************************************************************    LIST USER TOKENS *************************************************







        //******************************************** API **************************************************************************

        public List<NotificacionDTO> ListPaginationAppByUser(NotificacionDTO item)
        {
            List<NotificacionDTO> lista = new List<NotificacionDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarNotificacionesApp_PorPersona", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@DefaultKeyEmpresa", System.Data.SqlDbType.VarChar,100)).Value = item.DefaultKeyEmpresa;
                    cmd.Parameters.Add(new SqlParameter("@DefaultKeyUser", System.Data.SqlDbType.VarChar,100)).Value = item.DefaultKeyUser;

                   // cmd.Parameters.Add(new SqlParameter("@PaginaActual", System.Data.SqlDbType.Int)).Value = paging.PageNumber;
                    //cmd.Parameters.Add(new SqlParameter("@TamanioPagina", System.Data.SqlDbType.Int)).Value = paging.PageRecords;


                    //SqlParameter TotalRows = cmd.Parameters.Add("@NumeroRegistros", SqlDbType.Int);
                   // TotalRows.Direction = ParameterDirection.Output;


                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new NotificacionDTO()
                                {
                                    CodigoNotificacionesApp = oIDataReader[oIDataReader.GetOrdinal("CodigoNotificacionesApp")].ToString(),
                                    Title = oIDataReader[oIDataReader.GetOrdinal("Titulo")].ToString(),
                                    Body = oIDataReader[oIDataReader.GetOrdinal("Body")].ToString(),
                                    UrlImage = oIDataReader[oIDataReader.GetOrdinal("UrlImagen")].ToString(),
                                    DescriptionHtml = oIDataReader[oIDataReader.GetOrdinal("DescripcionHtml")].ToString(),
                                    DescFechaCreacion = item.DateParse(Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")])),
                                    Send = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Leido")]),
                                    CodigoNotificacionesAppDestinatarios  = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoNotificacionesAppDestinatarios")]),
                                    IdUser = oIDataReader[oIDataReader.GetOrdinal("IdUser")].ToString(),
                                    
                                });
                            }
                        }

                    }
                   // RNros = Convert.ToUInt32(TotalRows.Value);
                }
            }

            return lista;
        }



        //update read noti by user
        public void UpdateReadUser(NotificacionDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspActualizarNotificacionesAppDestinatariosEstadoLeido", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoNotificacionesApp", System.Data.SqlDbType.VarChar,100)).Value = item.CodigoNotificacionesApp;
                    cmd.Parameters.Add(new SqlParameter("@CodigoNotificacionesAppDestinatarios", System.Data.SqlDbType.Int)).Value = item.CodigoNotificacionesAppDestinatarios;
                    cmd.Parameters.Add(new SqlParameter("@IdUser", System.Data.SqlDbType.VarChar,100)).Value = item.IdUser;
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }


        //******************************************** API **************************************************************************






    }
}
