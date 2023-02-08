using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using E_DataModel.CentroEntrenamiento;
using E_DataModel.Common;

namespace E_DataLayer.CentroEntrenamiento
{
    public class CentroEntrenamiento_MenuPlantillaData
    {
        //PLANES
        public List<CentroEntrenamiento_MenuPlantillaDTO> CentroEntrenamiento_uspListarSEG_Planes()
        {
            List<CentroEntrenamiento_MenuPlantillaDTO> lista = new List<CentroEntrenamiento_MenuPlantillaDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("CentroEntrenamiento_uspListarSEG_Planes", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult))
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                var itemDTO = new CentroEntrenamiento_MenuPlantillaDTO();
                                itemDTO.Planes_CodigoPlan = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoPlan")]);
                                
                                itemDTO.Planes_Titulo = oIDataReader[oIDataReader.GetOrdinal("Titulo")].ToString();
                                itemDTO.Planes_Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString();
                                itemDTO.Planes_PrecioPEN = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("PrecioPEN")]);
                                itemDTO.Planes_PrecioDolares = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("PrecioDolares")]);
                                itemDTO.Planes_PrecioEuros = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("PrecioEuros")]);

                                itemDTO.Planes_PrecioPEN_Promo = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("PrecioPEN_Promo")]);
                                itemDTO.Planes_PrecioDolares_Promo = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("PrecioDolares_Promo")]);
                                itemDTO.Planes_PrecioEuros_Promo = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("PrecioEuros_Promo")]);

                                itemDTO.Planes_TituloCompleto = itemDTO.Planes_Titulo + " S/" + itemDTO.Planes_PrecioPEN + " $" + itemDTO.Planes_PrecioDolares + " EURO " + itemDTO.Planes_PrecioEuros;

                               itemDTO.Planes_CantidadUsuarios = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadUsuarios")]);
                           
                                lista.Add(itemDTO);
                            }
                        }
                    }
                }
            }

            return lista;
        }

        //PLANES CON MENU
        public List<CentroEntrenamiento_MenuPlantillaDTO> CentroEntrenamiento_uspListarMenuPlantillaPlan(CentroEntrenamiento_MenuPlantillaDTO request)
        {
            List<CentroEntrenamiento_MenuPlantillaDTO> lista = new List<CentroEntrenamiento_MenuPlantillaDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("CentroEntrenamiento_uspListarMenuPlantillaPlan", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoPlan", System.Data.SqlDbType.Int)).Value = request.Planes_CodigoPlan;
                    
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult))
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                var itemDTO = new CentroEntrenamiento_MenuPlantillaDTO();
                                itemDTO.CodigoMenu = oIDataReader[oIDataReader.GetOrdinal("CodigoMenu")].ToString();
                                itemDTO.CodigoMenuSuperior = oIDataReader[oIDataReader.GetOrdinal("CodigoMenuSuperior")].ToString();
                                itemDTO.Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString();
                                itemDTO.Observacion = oIDataReader[oIDataReader.GetOrdinal("Observacion")].ToString();
                                itemDTO.IdControl = oIDataReader[oIDataReader.GetOrdinal("IdControl")].ToString();
                                itemDTO.Url = oIDataReader[oIDataReader.GetOrdinal("Url")].ToString();
                                itemDTO.Orden = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Orden")]);
                                itemDTO.EstadoMenuPlan = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("EstadoMenuPlan")]);
                             
                                lista.Add(itemDTO);
                            }
                        }
                    }
                }
            }

            return lista;
        }

        //ELIMINAR MENU DEL PLAN
        public string CentroEntrenamiento_uspEliminarMenuPlantillaPlan(CentroEntrenamiento_MenuPlantillaDTO item)
        {
            string resultado = string.Empty;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("CentroEntrenamiento_uspEliminarMenuPlantillaPlan", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoPlan", System.Data.SqlDbType.VarChar, 100)).Value = item.Planes_CodigoPlan;                    
                    cmd.ExecuteNonQuery();
                }

            }
            return resultado;
        }

        //REGISTRAR MENU DEL PLAN
        public string CentroEntrenamiento_uspRegistrarMenuPlantillaPlan(CentroEntrenamiento_MenuPlantillaDTO item)
        {
            string resultado = string.Empty;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("CentroEntrenamiento_uspRegistrarMenuPlantillaPlan", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoPlan", System.Data.SqlDbType.Int)).Value = item.Planes_CodigoPlan;
                    cmd.Parameters.Add(new SqlParameter("@CodigoMenu", System.Data.SqlDbType.VarChar, 100)).Value = item.CodigoMenu;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;

                    cmd.ExecuteNonQuery();

                }

            }
            return resultado;
        }
        
        public List<CentroEntrenamiento_MenuPlantillaDTO> CentroEntrenamiento_uspListarMenuPlantilla(CentroEntrenamiento_MenuPlantillaDTO request)
        {
            List<CentroEntrenamiento_MenuPlantillaDTO> lista = new List<CentroEntrenamiento_MenuPlantillaDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("CentroEntrenamiento_uspListarMenuPlantilla", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult))
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                var itemDTO = new CentroEntrenamiento_MenuPlantillaDTO();
                                itemDTO.CodigoMenu = oIDataReader[oIDataReader.GetOrdinal("CodigoMenu")].ToString();
                                itemDTO.CodigoMenuSuperior = oIDataReader[oIDataReader.GetOrdinal("CodigoMenuSuperior")].ToString();
                                itemDTO.Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString();
                                itemDTO.Observacion = oIDataReader[oIDataReader.GetOrdinal("Observacion")].ToString();
                                itemDTO.IdControl = oIDataReader[oIDataReader.GetOrdinal("IdControl")].ToString();
                                itemDTO.Url = oIDataReader[oIDataReader.GetOrdinal("Url")].ToString();
                                itemDTO.Orden = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Orden")]);
                                itemDTO.Estado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Estado")]);
                                itemDTO.UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString();
                                itemDTO.FechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")]);

                                lista.Add(itemDTO);
                            }
                        }
                    }
                }
            }
            
            return lista;
        }

        public CentroEntrenamiento_MenuPlantillaDTO CentroEntrenamiento_uspBuscarMenuPlantilla(CentroEntrenamiento_MenuPlantillaDTO request)
        {
            CentroEntrenamiento_MenuPlantillaDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("CentroEntrenamiento_uspBuscarMenuPlantilla", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoMenu", System.Data.SqlDbType.VarChar, 100)).Value = request.CodigoMenu;
                  
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader(System.Data.CommandBehavior.SingleRow))
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new CentroEntrenamiento_MenuPlantillaDTO();
                                itemDTO.CodigoMenu = oIDataReader[oIDataReader.GetOrdinal("CodigoMenu")].ToString();
                                itemDTO.CodigoMenuSuperior = oIDataReader[oIDataReader.GetOrdinal("CodigoMenuSuperior")].ToString();
                                itemDTO.Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString();
                                itemDTO.Observacion = oIDataReader[oIDataReader.GetOrdinal("Observacion")].ToString();
                                itemDTO.IdControl = oIDataReader[oIDataReader.GetOrdinal("IdControl")].ToString();
                                itemDTO.Url = oIDataReader[oIDataReader.GetOrdinal("Url")].ToString();
                                itemDTO.Orden = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Orden")]);
                                itemDTO.Estado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Estado")]);
                                itemDTO.UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString();
                                itemDTO.FechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")]);
                            }
                        }
                    }
                }
            }

            return itemDTO;
        }

        public string CentroEntrenamiento_uspRegistrarMenuPlantilla(CentroEntrenamiento_MenuPlantillaDTO item)
        {
            string resultado = string.Empty;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("CentroEntrenamiento_uspRegistrarMenuPlantilla", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    if (!string.IsNullOrEmpty(item.CodigoMenu))
                    {
                        cmd.Parameters.Add(new SqlParameter("@CodigoMenu", System.Data.SqlDbType.VarChar, 100)).Value = item.CodigoMenu;
                    }
                    else
                    {
                        cmd.Parameters.Add(new SqlParameter("@CodigoMenu", System.Data.SqlDbType.VarChar, 100)).Value = DBNull.Value;
                    }

                    if (!string.IsNullOrEmpty(item.CodigoMenuSuperior))
                    {
                        cmd.Parameters.Add(new SqlParameter("@CodigoMenuSuperior", System.Data.SqlDbType.VarChar, 100)).Value = item.CodigoMenuSuperior;
                    }
                    else
                    {
                        cmd.Parameters.Add(new SqlParameter("@CodigoMenuSuperior", System.Data.SqlDbType.VarChar, 100)).Value = string.Empty;
                    }
                    
                    cmd.Parameters.Add(new SqlParameter("@Descripcion", System.Data.SqlDbType.VarChar, 100)).Value = item.Descripcion;
                    cmd.Parameters.Add(new SqlParameter("@Observacion", System.Data.SqlDbType.VarChar, 200)).Value = item.Observacion;
                    cmd.Parameters.Add(new SqlParameter("@IdControl", System.Data.SqlDbType.VarChar, 100)).Value = item.IdControl;
                    cmd.Parameters.Add(new SqlParameter("@Url", System.Data.SqlDbType.VarChar, 100)).Value = item.Url;
                    cmd.Parameters.Add(new SqlParameter("@Orden", System.Data.SqlDbType.Int)).Value = item.Orden;
                    cmd.Parameters.Add(new SqlParameter("@Estado", System.Data.SqlDbType.Bit)).Value = item.Estado;                    
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;
                    
                    cmd.ExecuteNonQuery();

                }

            }
            return resultado;
        }

        public string CentroEntrenamiento_uspActualizarMenuPlantilla(CentroEntrenamiento_MenuPlantillaDTO item)
        {
            string resultado = string.Empty;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("CentroEntrenamiento_uspActualizarMenuPlantilla", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoMenu", System.Data.SqlDbType.VarChar, 100)).Value = item.CodigoMenu;
                    cmd.Parameters.Add(new SqlParameter("@CodigoMenuSuperior", System.Data.SqlDbType.VarChar, 100)).Value = item.CodigoMenuSuperior??string.Empty;
                    cmd.Parameters.Add(new SqlParameter("@Descripcion", System.Data.SqlDbType.VarChar, 100)).Value = item.Descripcion;
                    cmd.Parameters.Add(new SqlParameter("@Observacion", System.Data.SqlDbType.VarChar, 200)).Value = item.Observacion;
                    cmd.Parameters.Add(new SqlParameter("@IdControl", System.Data.SqlDbType.VarChar, 100)).Value = item.IdControl;
                    cmd.Parameters.Add(new SqlParameter("@Url", System.Data.SqlDbType.VarChar, 100)).Value = item.Url;
                    cmd.Parameters.Add(new SqlParameter("@Orden", System.Data.SqlDbType.Int)).Value = item.Orden;
                    cmd.Parameters.Add(new SqlParameter("@Estado", System.Data.SqlDbType.Bit)).Value = item.Estado;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;

                    cmd.ExecuteNonQuery();

                }

            }
            return resultado;
        }
        public string CentroEntrenamiento_uspActualizarMenuPlantillaOrden(CentroEntrenamiento_MenuPlantillaDTO item)
        {
            string resultado = string.Empty;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("CentroEntrenamiento_uspActualizarMenuPlantillaOrden", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoMenu", System.Data.SqlDbType.VarChar, 100)).Value = item.CodigoMenu;                  
                    cmd.Parameters.Add(new SqlParameter("@Orden", System.Data.SqlDbType.Int)).Value = item.Orden;
                   
                    cmd.ExecuteNonQuery();

                }

            }
            return resultado;
        }

        //LISTAR MENU PERFIL 
        public List<CentroEntrenamiento_MenuPlantillaDTO> SEGListarPerfilMenu(CentroEntrenamiento_MenuPlantillaDTO request)
        {
            List<CentroEntrenamiento_MenuPlantillaDTO> lista = new List<CentroEntrenamiento_MenuPlantillaDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SEGListarPerfilMenu", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = request.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = request.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoPerfil", System.Data.SqlDbType.Int)).Value = request.CodigoPerfil;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult))
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                var itemDTO = new CentroEntrenamiento_MenuPlantillaDTO();
                                itemDTO.CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]);
                                itemDTO.CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]);
                                itemDTO.CodigoMenuSuperior = oIDataReader[oIDataReader.GetOrdinal("CodigoMenuSuperior")].ToString();
                                itemDTO.CodigoMenu = oIDataReader[oIDataReader.GetOrdinal("CodigoMenu")].ToString();
                                itemDTO.Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString();
                                itemDTO.Observacion = oIDataReader[oIDataReader.GetOrdinal("Observacion")].ToString();
                                itemDTO.IdControl = oIDataReader[oIDataReader.GetOrdinal("IdControl")].ToString();
                                itemDTO.Orden = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Orden")]);
                                itemDTO.Estado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Estado")]);

                                lista.Add(itemDTO);
                            }
                        }
                    }
                }
            }

            return lista;
        }

        //REGISTRAR MENU PERFIL
        public string CentroEntrenamiento_uspRegistrarPerfilMenu(CentroEntrenamiento_MenuPlantillaDTO item)
        {
            string resultado = string.Empty;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("CentroEntrenamiento_uspRegistrarPerfilMenu", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoPerfil", System.Data.SqlDbType.Int)).Value = item.CodigoPerfil;
                    cmd.Parameters.Add(new SqlParameter("@CodigoMenu", System.Data.SqlDbType.VarChar,100)).Value = item.CodigoMenu;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;

                    cmd.ExecuteNonQuery();
                }

            }
            return resultado;
        }

        //ELIMINAR MENU PERFIL
        public string CentroEntrenamiento_uspEliminarPerfilMenu(CentroEntrenamiento_MenuPlantillaDTO item)
        {
            string resultado = string.Empty;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("CentroEntrenamiento_uspEliminarPerfilMenu", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoPerfil", System.Data.SqlDbType.Int)).Value = item.CodigoPerfil;
                  
                    cmd.ExecuteNonQuery();
                }

            }
            return resultado;
        }



    }
}
