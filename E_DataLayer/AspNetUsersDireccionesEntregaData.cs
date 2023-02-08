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
    public class AspNetUsersDireccionesEntregaData
    {

        public List<AspNetUsersDireccionesEntregaDTO> ecommerce_uspListarAspNetUsers_DireccionesEntrega(AspNetUsersDireccionesEntregaDTO oFiltro)
        {
            List<AspNetUsersDireccionesEntregaDTO> lista = new List<AspNetUsersDireccionesEntregaDTO>();
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("ecommerce_uspListarAspNetUsers_DireccionesEntrega", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CorreoUsuario", System.Data.SqlDbType.VarChar,100)).Value = oFiltro.CorreoUsuario;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new AspNetUsersDireccionesEntregaDTO()
                                {
                                    CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    IdUser = oIDataReader[oIDataReader.GetOrdinal("IdUser")].ToString(),
                                    CodigoDireccion = oIDataReader[oIDataReader.GetOrdinal("CodigoDireccion")].ToString(),
                                    Nombres = oIDataReader[oIDataReader.GetOrdinal("Nombres")].ToString(),
                                    Apellidos = oIDataReader[oIDataReader.GetOrdinal("Apellidos")].ToString(),
                                    Celular = oIDataReader[oIDataReader.GetOrdinal("Celular")].ToString(),
                                    Telefono = oIDataReader[oIDataReader.GetOrdinal("Telefono")].ToString(),
                                    Email = oIDataReader[oIDataReader.GetOrdinal("Email")].ToString(),
                                    TipoDireccion = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("TipoDireccion")]),
                                    Direccion = oIDataReader[oIDataReader.GetOrdinal("Direccion")].ToString(),
                                    NroLote = oIDataReader[oIDataReader.GetOrdinal("NroLote")].ToString(),
                                    DeptoInt = oIDataReader[oIDataReader.GetOrdinal("DeptoInt")].ToString(),
                                    Urbanizacion = oIDataReader[oIDataReader.GetOrdinal("Urbanizacion")].ToString(),
                                    Referencia = oIDataReader[oIDataReader.GetOrdinal("Referencia")].ToString(),
                                    Ubigeo = oIDataReader[oIDataReader.GetOrdinal("Ubigeo")].ToString(),
                                    Departamento = oIDataReader[oIDataReader.GetOrdinal("Departamento")].ToString(),
                                    Provincia = oIDataReader[oIDataReader.GetOrdinal("Provincia")].ToString(),
                                    Distrito = oIDataReader[oIDataReader.GetOrdinal("Distrito")].ToString(),
                                    DesTipoDireccion = oIDataReader[oIDataReader.GetOrdinal("DesTipoDireccion")].ToString(),
                                    DireccionDefault = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("DireccionDefault")]),
                                    PrecioEnvio = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("PrecioEnvio")]),
                                    TipoTiempoEntrega = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("TipoTiempoEntrega")]),
                                    TiempoEntrega = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("TiempoEntrega")])
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }

        public string ecommerce_uspRegistrarAspNetUsers_DireccionesEntrega(AspNetUsersDireccionesEntregaDTO item)
        {
            string resultado = string.Empty;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("ecommerce_uspRegistrarAspNetUsers_DireccionesEntrega", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CorreoUsuario", System.Data.SqlDbType.VarChar,100)).Value = item.CorreoUsuario;
                    cmd.Parameters.Add(new SqlParameter("@Email", System.Data.SqlDbType.VarChar, 100)).Value = item.Email;
                    cmd.Parameters.Add(new SqlParameter("@CodigoDireccion", System.Data.SqlDbType.VarChar,100)).Direction = ParameterDirection.Output;

                    if (!string.IsNullOrEmpty(item.Nombres))
                    {
                        cmd.Parameters.Add(new SqlParameter("@Nombres", System.Data.SqlDbType.VarChar, 100)).Value = item.Nombres;
                    }
                    if (!string.IsNullOrEmpty(item.Apellidos))
                    {
                        cmd.Parameters.Add(new SqlParameter("@Apellidos", System.Data.SqlDbType.VarChar, 100)).Value = item.Apellidos;
                    }

                    cmd.Parameters.Add(new SqlParameter("@Celular", System.Data.SqlDbType.VarChar,20)).Value = item.Celular;
                    cmd.Parameters.Add(new SqlParameter("@Telefono", System.Data.SqlDbType.VarChar, 20)).Value = item.Telefono;
                    cmd.Parameters.Add(new SqlParameter("@TipoDireccion", System.Data.SqlDbType.Int)).Value = item.TipoDireccion;
                    cmd.Parameters.Add(new SqlParameter("@Direccion", System.Data.SqlDbType.VarChar, 100)).Value = item.Direccion;
                    cmd.Parameters.Add(new SqlParameter("@NroLote", System.Data.SqlDbType.VarChar, 20)).Value = item.NroLote;
                    cmd.Parameters.Add(new SqlParameter("@DeptoInt", System.Data.SqlDbType.VarChar, 20)).Value = item.DeptoInt;
                    cmd.Parameters.Add(new SqlParameter("@Urbanizacion", System.Data.SqlDbType.VarChar, 50)).Value = item.Urbanizacion;
                    cmd.Parameters.Add(new SqlParameter("@Referencia", System.Data.SqlDbType.VarChar, 100)).Value = item.Referencia;
                    cmd.Parameters.Add(new SqlParameter("@Ubigeo", System.Data.SqlDbType.VarChar, 100)).Value = item.Ubigeo;
                    cmd.Parameters.Add(new SqlParameter("@DireccionDefault", System.Data.SqlDbType.Bit)).Value = item.DireccionDefault;

                    cmd.ExecuteNonQuery();
                    resultado = cmd.Parameters["@CodigoDireccion"].Value.ToString();
                }

            }
            return resultado;
        }


    }
}
