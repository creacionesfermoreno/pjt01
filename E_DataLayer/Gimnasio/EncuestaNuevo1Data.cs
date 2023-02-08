using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using E_DataModel.Gimnasio;
using E_DataModel.Common;
using System.Data.SqlClient;

namespace E_DataLayer.Gimnasio
{
	public class EncuestaNuevo1Data
	{
       
        public List<EncuestaNuevo1DTO> uspListarEncuestaNuevo2(EncuestaNuevo1DTO oEncuestaNuevo1DTO)
        {
            List<EncuestaNuevo1DTO> lista = new List<EncuestaNuevo1DTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarEncuestaNuevo2", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oEncuestaNuevo1DTO.CodigoUnidadNegocio;                    
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oEncuestaNuevo1DTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoOrigenProspecto", System.Data.SqlDbType.Int)).Value = oEncuestaNuevo1DTO.CodigoOrigenProspecto;
                    cmd.Parameters.Add(new SqlParameter("@CodigoProspecto", System.Data.SqlDbType.Int)).Value = oEncuestaNuevo1DTO.CodigoProspecto;
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new EncuestaNuevo1DTO()
                                {
                                    CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    CodigoProspecto = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoProspecto")]),
                                    CodigoInteres = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoInteres")]),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString(),
                                    FechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")])
                                });
                            }
                        }

                    }
                }
            }
            return lista;          
        }


        public List<EncuestaNuevo1DTO> uspListarEncuestaEstadisticaObjetivos(EncuestaNuevo1DTO oEncuestaNuevo1DTO)
        {
            List<EncuestaNuevo1DTO> lista = new List<EncuestaNuevo1DTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarEncuestaEstadisticaObjetivos", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oEncuestaNuevo1DTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oEncuestaNuevo1DTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = oEncuestaNuevo1DTO.fehaInicio;
                    cmd.Parameters.Add(new SqlParameter("@fechaFin", System.Data.SqlDbType.DateTime)).Value = oEncuestaNuevo1DTO.fehaFin;
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new EncuestaNuevo1DTO()
                                {
                                    DescripcioObjetivo = oIDataReader[oIDataReader.GetOrdinal("desObjetivo")].ToString(),
                                    porTotalObjetivo = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Total")])                                  
                                });
                            }
                        }

                    }
                }
            }
            return lista;            
        }

        public List<EncuestaNuevo1DTO> uspListarEncuestaEstadisticaComoConocioGym(EncuestaNuevo1DTO oEncuestaNuevo1DTO)
        {
            List<EncuestaNuevo1DTO> lista = new List<EncuestaNuevo1DTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarEncuestaEstadisticaComoConocioGym", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oEncuestaNuevo1DTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oEncuestaNuevo1DTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = oEncuestaNuevo1DTO.fehaInicio;
                    cmd.Parameters.Add(new SqlParameter("@fechaFin", System.Data.SqlDbType.DateTime)).Value = oEncuestaNuevo1DTO.fehaFin;
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new EncuestaNuevo1DTO()
                                {
                                    DescripcioComoConocioGym = oIDataReader[oIDataReader.GetOrdinal("desComoConocioGym")].ToString(),
                                    porTotalComoConocioGym = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Total")])
                                });
                            }
                        }

                    }
                }
            }
            return lista;            
        }

        public List<EncuestaNuevo1DTO> uspListarEncuestaEstadisticaInteres(EncuestaNuevo1DTO oEncuestaNuevo1DTO)
        {
            List<EncuestaNuevo1DTO> lista = new List<EncuestaNuevo1DTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarEncuestaEstadisticaInteres", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oEncuestaNuevo1DTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oEncuestaNuevo1DTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = oEncuestaNuevo1DTO.fehaInicio;
                    cmd.Parameters.Add(new SqlParameter("@fechaFin", System.Data.SqlDbType.DateTime)).Value = oEncuestaNuevo1DTO.fehaFin;
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new EncuestaNuevo1DTO()
                                {
                                    DescripcionInteres = oIDataReader[oIDataReader.GetOrdinal("desInteres")].ToString(),
                                    porTotalInteres = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Total")])
                                });
                            }
                        }

                    }
                }
            }
            return lista;            
        }

        public EncuestaNuevo1DTO uspBuscarEncuestaNuevo1(EncuestaNuevo1DTO oEncuestaNuevo1)
        {
            EncuestaNuevo1DTO itemDTO = new EncuestaNuevo1DTO();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspBuscarEncuestaNuevo1", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oEncuestaNuevo1.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oEncuestaNuevo1.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoProspecto", System.Data.SqlDbType.Int)).Value = oEncuestaNuevo1.CodigoProspecto;
                    
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                itemDTO = new EncuestaNuevo1DTO()
                                {
                                    CodigoUnidadNegocio = Convert.ToInt32(reader[reader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(reader[reader.GetOrdinal("CodigoSede")]),
                                    CodigoEncuestaNuevo1 = Convert.ToInt32(reader[reader.GetOrdinal("CodigoEncuestaNuevo1")]),
                                    CodigoProspecto = Convert.ToInt32(reader[reader.GetOrdinal("CodigoProspecto")]),
                                    CodigoObjetivo = Convert.ToInt32(reader[reader.GetOrdinal("CodigoObjetivo")]),
                                    CodigoComoConocioGym = Convert.ToInt32(reader[reader.GetOrdinal("CodigoComoConocioGym")]),
                                    UsuarioCreacion = reader[reader.GetOrdinal("UsuarioCreacion")].ToString(),
                                    FechaCreacion = Convert.ToDateTime(reader[reader.GetOrdinal("FechaCreacion")])
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;
        }
        
        public void Registrar(EncuestaNuevo1DTO item)
		{
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspRegistrarEncuestaNuevo1", conn))
                {                                                                                                              
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoEncuestaNuevo1", System.Data.SqlDbType.Int)).Value = item.CodigoEncuestaNuevo1;
                    cmd.Parameters.Add(new SqlParameter("@CodigoProspecto", System.Data.SqlDbType.Int)).Value = item.CodigoProspecto;
                    cmd.Parameters.Add(new SqlParameter("@CodigoObjetivo", System.Data.SqlDbType.Int)).Value = item.CodigoObjetivo;
                    cmd.Parameters.Add(new SqlParameter("@CodigoComoConocioGym", System.Data.SqlDbType.Int)).Value = item.CodigoComoConocioGym;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;
                  
                    cmd.ExecuteNonQuery();
                }
            }
		}

        public void uspRegistrarEncuestaDatos2(EncuestaNuevo1DTO item)
		{
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspRegistrarEncuestaNuevo2", conn))
                {                                                                                                              
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoEncuestaNuevo2", System.Data.SqlDbType.Int)).Value = item.CodigoEncuestaNuevo2;
                    cmd.Parameters.Add(new SqlParameter("@CodigoOrigenProspecto", System.Data.SqlDbType.Int)).Value = item.CodigoOrigenProspecto;
                    cmd.Parameters.Add(new SqlParameter("@CodigoProspecto", System.Data.SqlDbType.Int)).Value = item.CodigoProspecto;
                    cmd.Parameters.Add(new SqlParameter("@CodigoInteres", System.Data.SqlDbType.Int)).Value = item.CodigoInteres;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;

                    cmd.ExecuteNonQuery();
                }
            }
		}
        
        public void uspEliminarEncuestaDatos2(EncuestaNuevo1DTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspEliminarEncuestaNuevo2", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoProspecto", System.Data.SqlDbType.Int)).Value = item.CodigoProspecto;                    
                    cmd.ExecuteNonQuery();
                }
            }
        }
        
    }
}
