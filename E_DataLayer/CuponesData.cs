using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using E_DataModel;
using E_DataModel.Common;

namespace E_DataLayer
{
    public class CuponesData
    {
        public List<CuponesDTO> ecommerce_uspListar_Cupones(CuponesDTO oFiltro)
        {
            List<CuponesDTO> lista = new List<CuponesDTO>();
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("ecommerce_uspListar_Cupones", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoSede;
                  

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new CuponesDTO()
                                {
                                    CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    CodigoCupon = oIDataReader[oIDataReader.GetOrdinal("CodigoCupon")].ToString(),
                                    CodigoPromocion = oIDataReader[oIDataReader.GetOrdinal("CodigoPromocion")].ToString(),
                                    TipoCupon = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("TipoCupon")]),
                                    DesTipoCupon = oIDataReader[oIDataReader.GetOrdinal("DesTipoCupon")].ToString(),
                                    Valor = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Valor")]),
                                    FechaInicio = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaInicio")]),
                                    FechaFin = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaFin")]),
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

        public CuponesDTO ecommerce_uspBuscar_Cupones(CuponesDTO oFiltro)
        {
            CuponesDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("ecommerce_uspBuscar_Cupones", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoCupon", System.Data.SqlDbType.VarChar,100)).Value = oFiltro.CodigoCupon;
                    
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new CuponesDTO()
                                {
                                    CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    CodigoCupon = oIDataReader[oIDataReader.GetOrdinal("CodigoCupon")].ToString(),
                                    CodigoPromocion = oIDataReader[oIDataReader.GetOrdinal("CodigoPromocion")].ToString(),
                                    TipoCupon = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("TipoCupon")]),
                                    Valor = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Valor")]),
                                    FechaInicio = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaInicio")]),
                                    FechaFin = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaFin")]),
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

        public CuponesDTO ecommerce_uspBuscar_CuponesXCodigoPromocion(CuponesDTO oFiltro)
        {
            CuponesDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("ecommerce_uspBuscar_CuponesXCodigoPromocion", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoPromocion", System.Data.SqlDbType.VarChar, 100)).Value = oFiltro.CodigoPromocion;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new CuponesDTO()
                                {
                                    CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    CodigoCupon = oIDataReader[oIDataReader.GetOrdinal("CodigoCupon")].ToString(),
                                    CodigoPromocion = oIDataReader[oIDataReader.GetOrdinal("CodigoPromocion")].ToString(),
                                    TipoCupon = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("TipoCupon")]),
                                    DesTipoCupon = oIDataReader[oIDataReader.GetOrdinal("DesTipoCupon")].ToString(),
                                    Valor = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Valor")]),
                                    FechaInicio = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaInicio")]),
                                    FechaFin = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaFin")]),
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

        public void ecommerce_uspRegistrar_Cupones(CuponesDTO oFiltro)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("ecommerce_uspRegistrar_Cupones", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoSede;                    
                    cmd.Parameters.Add(new SqlParameter("@CodigoPromocion", System.Data.SqlDbType.VarChar, 100)).Value = oFiltro.CodigoPromocion;
                    cmd.Parameters.Add(new SqlParameter("@TipoCupon", System.Data.SqlDbType.Int)).Value = oFiltro.TipoCupon;
                    cmd.Parameters.Add(new SqlParameter("@Valor", System.Data.SqlDbType.Decimal)).Value = oFiltro.Valor;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = oFiltro.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oFiltro.FechaFin;
                   
                    if (!string.IsNullOrEmpty(oFiltro.UsuarioCreacion))
                    {
                        cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = oFiltro.UsuarioCreacion;
                    }
                    cmd.ExecuteNonQuery();
                    
                }

            }
        }

        public void ecommerce_uspActualizar_Cupones(CuponesDTO oFiltro)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("ecommerce_uspActualizar_Cupones", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoCupon", System.Data.SqlDbType.VarChar, 100)).Value = oFiltro.CodigoCupon;                                       
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = oFiltro.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oFiltro.FechaFin;

                    if (!string.IsNullOrEmpty(oFiltro.UsuarioCreacion))
                    {
                        cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = oFiltro.UsuarioCreacion;
                    }
                    cmd.ExecuteNonQuery();
                }

            }
        }

    }
}
