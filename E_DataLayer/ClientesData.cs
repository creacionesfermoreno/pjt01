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
    public class ClientesData
    {
        public List<ClientesDTO> ecommerce_uspBuscadorClientes(ClientesDTO oFiltro)
        {
            List<ClientesDTO> lista = new List<ClientesDTO>();
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("ecommerce_uspBuscadorClientes", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoSede;
                    if (oFiltro.Nombres == null)
                    {
                        cmd.Parameters.Add(new SqlParameter("@filterNombre", System.Data.SqlDbType.VarChar)).Value = string.Empty;
                    }
                    else
                    {
                        cmd.Parameters.Add(new SqlParameter("@filterNombre", System.Data.SqlDbType.VarChar)).Value = oFiltro.Nombres;

                    }
                    

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ClientesDTO()
                                {
                                    CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    CodigoCliente = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoCliente")]),
                                    Nombres = oIDataReader[oIDataReader.GetOrdinal("Nombres")].ToString(),
                                    Apellidos = oIDataReader[oIDataReader.GetOrdinal("Apellidos")].ToString(),
                                    Telefono = oIDataReader[oIDataReader.GetOrdinal("Telefono")].ToString(),
                                    CorreoElectronico = oIDataReader[oIDataReader.GetOrdinal("CorreoElectronico")].ToString()
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }

        public ClientesDTO ecommerce_uspBuscadorClientesPorIdentificacion(ClientesDTO oFiltro)
        {
            ClientesDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("ecommerce_uspBuscadorClientesPorIdentificacion", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Identificacion", System.Data.SqlDbType.VarChar,100)).Value = oFiltro.Identificacion;                    

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new ClientesDTO()
                                {
                                    CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    CodigoCliente = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoCliente")]),                                
                                    Nombres = oIDataReader[oIDataReader.GetOrdinal("Nombres")].ToString(),
                                    Apellidos = oIDataReader[oIDataReader.GetOrdinal("Apellidos")].ToString()                                 
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;
        }


        public int ecommerce_uspRegistrar_Clientes(ClientesDTO item)
        {
            int resultado = 0;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("ecommerce_uspRegistrar_Clientes", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoCliente", System.Data.SqlDbType.Int)).Direction = ParameterDirection.Output;

                    if (!string.IsNullOrEmpty(item.Nombres))
                    {
                        cmd.Parameters.Add(new SqlParameter("@Nombres", System.Data.SqlDbType.VarChar, 100)).Value = item.Nombres;
                    }
                    if (!string.IsNullOrEmpty(item.Apellidos))
                    {
                        cmd.Parameters.Add(new SqlParameter("@Apellidos", System.Data.SqlDbType.VarChar, 100)).Value = item.Apellidos;
                    }

                    cmd.Parameters.Add(new SqlParameter("@CodigoTipoIdentificacion", System.Data.SqlDbType.Int)).Value = item.CodigoTipoIdentificacion;
                    cmd.Parameters.Add(new SqlParameter("@Identificacion", System.Data.SqlDbType.VarChar, 100)).Value = item.Identificacion;
                    cmd.Parameters.Add(new SqlParameter("@Direccion", System.Data.SqlDbType.VarChar, 100)).Value = item.Direccion;
                    cmd.Parameters.Add(new SqlParameter("@Departamento", System.Data.SqlDbType.VarChar, 100)).Value = item.Departamento;
                    cmd.Parameters.Add(new SqlParameter("@provincia", System.Data.SqlDbType.VarChar, 100)).Value = item.provincia;
                    cmd.Parameters.Add(new SqlParameter("@Distrito", System.Data.SqlDbType.VarChar, 100)).Value = item.Distrito;
                    cmd.Parameters.Add(new SqlParameter("@Urbanizacion", System.Data.SqlDbType.VarChar, 100)).Value = item.Urbanizacion;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUbigeo", System.Data.SqlDbType.VarChar, 100)).Value = item.CodigoUbigeo;
                    cmd.Parameters.Add(new SqlParameter("@CorreoElectronico", System.Data.SqlDbType.VarChar, 100)).Value = item.CorreoElectronico;
                    cmd.Parameters.Add(new SqlParameter("@Telefono", System.Data.SqlDbType.VarChar, 100)).Value = item.Telefono;
                    cmd.Parameters.Add(new SqlParameter("@Celular", System.Data.SqlDbType.VarChar, 100)).Value = item.Celular;
                    cmd.Parameters.Add(new SqlParameter("@CodigoVendedor", System.Data.SqlDbType.Int)).Value = item.CodigoVendedor;
                    cmd.Parameters.Add(new SqlParameter("@DireccionDelivery", System.Data.SqlDbType.VarChar, 200)).Value = item.DireccionDelivery;
                    
                    if (!string.IsNullOrEmpty(item.UsuarioCreacion))
                    {
                        cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;
                    }
                    cmd.ExecuteNonQuery();
                    resultado = Convert.ToInt32(cmd.Parameters["@CodigoCliente"].Value);
                }

            }
            return resultado;
        }

        public void ecommerce_uspActualizarClientes(ClientesDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("ecommerce_uspActualizarClientes", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoCliente", System.Data.SqlDbType.Int)).Value = item.CodigoCliente;

                    if (!string.IsNullOrEmpty(item.Nombres))
                    {
                        cmd.Parameters.Add(new SqlParameter("@Nombres", System.Data.SqlDbType.VarChar, 100)).Value = item.Nombres;
                    }
                    if (!string.IsNullOrEmpty(item.Apellidos))
                    {
                        cmd.Parameters.Add(new SqlParameter("@Apellidos", System.Data.SqlDbType.VarChar, 100)).Value = item.Apellidos;
                    }

                    cmd.Parameters.Add(new SqlParameter("@CodigoTipoIdentificacion", System.Data.SqlDbType.Int)).Value = item.CodigoTipoIdentificacion;
                    cmd.Parameters.Add(new SqlParameter("@Identificacion", System.Data.SqlDbType.VarChar, 100)).Value = item.Identificacion;
                    cmd.Parameters.Add(new SqlParameter("@Direccion", System.Data.SqlDbType.VarChar, 100)).Value = item.Direccion;
                    cmd.Parameters.Add(new SqlParameter("@Departamento", System.Data.SqlDbType.VarChar, 100)).Value = item.Departamento;
                    cmd.Parameters.Add(new SqlParameter("@provincia", System.Data.SqlDbType.VarChar, 100)).Value = item.provincia;
                    cmd.Parameters.Add(new SqlParameter("@Distrito", System.Data.SqlDbType.VarChar, 100)).Value = item.Distrito;
                    cmd.Parameters.Add(new SqlParameter("@Urbanizacion", System.Data.SqlDbType.VarChar, 100)).Value = item.Urbanizacion;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUbigeo", System.Data.SqlDbType.VarChar, 100)).Value = item.CodigoUbigeo;
                    cmd.Parameters.Add(new SqlParameter("@CorreoElectronico", System.Data.SqlDbType.VarChar, 100)).Value = item.CorreoElectronico;
                    cmd.Parameters.Add(new SqlParameter("@Telefono", System.Data.SqlDbType.VarChar, 100)).Value = item.Telefono;
                    cmd.Parameters.Add(new SqlParameter("@Celular", System.Data.SqlDbType.VarChar, 100)).Value = item.Celular;
                    cmd.Parameters.Add(new SqlParameter("@CodigoVendedor", System.Data.SqlDbType.Int)).Value = item.CodigoVendedor;
                    cmd.Parameters.Add(new SqlParameter("@DireccionDelivery", System.Data.SqlDbType.VarChar, 200)).Value = item.DireccionDelivery;

                    if (!string.IsNullOrEmpty(item.UsuarioCreacion))
                    {
                        cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;
                    }
                    cmd.ExecuteNonQuery();
                   
                }

            }
          
        }




    }
}
