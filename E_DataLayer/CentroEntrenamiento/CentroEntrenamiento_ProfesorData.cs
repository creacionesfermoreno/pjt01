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
    public class CentroEntrenamiento_ProfesorData
    {


        public List<CentroEntrenamiento_ProfesorDTO> CentroEntrenamiento_uspListarPresencial_uspListarPersonalClasesGrupales(CentroEntrenamiento_ProfesorDTO request)
        {
            List<CentroEntrenamiento_ProfesorDTO> lista = new List<CentroEntrenamiento_ProfesorDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarPersonalClasesGrupales", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int, 10)).Value = request.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int, 10)).Value = request.CodigoSede;
                  
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult))
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                var itemDTO = new CentroEntrenamiento_ProfesorDTO();

                              
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CodigoProfesional")))
                                {
                                    itemDTO.CodigoProfesional = oIDataReader[oIDataReader.GetOrdinal("CodigoProfesional")].ToString();
                                }                               
                              
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("ProfesionalNombre")))
                                {
                                    itemDTO.NombreCompleto = (oIDataReader[oIDataReader.GetOrdinal("ProfesionalNombre")].ToString());
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("NroDocumento")))
                                {
                                    itemDTO.NroDocumento = oIDataReader[oIDataReader.GetOrdinal("NroDocumento")].ToString();
                                }
                                                           
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("ProfesionalPhoto")))
                                {
                                    itemDTO.ImagenUrl = oIDataReader[oIDataReader.GetOrdinal("ProfesionalPhoto")].ToString();
                                }

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("EstadoCelular")))
                                {
                                    itemDTO.EstadoCelular = oIDataReader[oIDataReader.GetOrdinal("EstadoCelular")].ToString();
                                }

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("Celular")))
                                {
                                    itemDTO.Celular = oIDataReader[oIDataReader.GetOrdinal("Celular")].ToString();
                                }

                                lista.Add(itemDTO);
                            }
                        }
                    }
                }
            }

            return lista;
        }

        public CentroEntrenamiento_ProfesorDTO CentroEntrenamiento_uspBuscarProfesorPorNombres(CentroEntrenamiento_ProfesorDTO oFiltro)
        {
            CentroEntrenamiento_ProfesorDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("CentroEntrenamiento_uspBuscarProfesorPorNombres", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    if (oFiltro.NroDocumento != null)
                    {
                        cmd.Parameters.Add(new SqlParameter("@DNI", System.Data.SqlDbType.VarChar, 20)).Value = oFiltro.NroDocumento;
                    }
                    else
                    {
                        cmd.Parameters.Add(new SqlParameter("@DNI", System.Data.SqlDbType.VarChar, 20)).Value = string.Empty;
                    }

                    if (oFiltro.Nombres != null)
                    {
                        cmd.Parameters.Add(new SqlParameter("@Nombres", System.Data.SqlDbType.VarChar, 20)).Value = oFiltro.Nombres;
                    }
                    else
                    {
                        cmd.Parameters.Add(new SqlParameter("@Nombres", System.Data.SqlDbType.VarChar, 20)).Value = string.Empty;
                    }

                    if (oFiltro.Apellidos != null)
                    {
                        cmd.Parameters.Add(new SqlParameter("@Apellidos", System.Data.SqlDbType.VarChar, 20)).Value = oFiltro.Apellidos;
                    }
                    else
                    {
                        cmd.Parameters.Add(new SqlParameter("@Apellidos", System.Data.SqlDbType.VarChar, 20)).Value = string.Empty;
                    }

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new CentroEntrenamiento_ProfesorDTO()
                                {
                                    CodigoProfesional = oIDataReader[oIDataReader.GetOrdinal("CodigoProfesional")].ToString(),
                                    Nombres = oIDataReader[oIDataReader.GetOrdinal("Nombres")].ToString(),
                                    Apellidos = oIDataReader[oIDataReader.GetOrdinal("Apellidos")].ToString(),
                                    TipoDocumento = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("TipoDocumento")]),
                                    NroDocumento = oIDataReader[oIDataReader.GetOrdinal("DNI")].ToString(),
                                    Telefono = oIDataReader[oIDataReader.GetOrdinal("Telefono")].ToString(),
                                    Celular = oIDataReader[oIDataReader.GetOrdinal("Celular")].ToString(),
                                    Correo = oIDataReader[oIDataReader.GetOrdinal("Correo")].ToString(),
                                    FechaNacimiento = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaNacimiento")]),
                                    ImagenUrl = oIDataReader[oIDataReader.GetOrdinal("ImagenUrl")].ToString(),
                                    Genero = oIDataReader[oIDataReader.GetOrdinal("Genero")].ToString(),
                                    Facebook = oIDataReader[oIDataReader.GetOrdinal("Facebook")].ToString(),
                                    Direccion = oIDataReader[oIDataReader.GetOrdinal("Direccion")].ToString(),
                                    Distrito = oIDataReader[oIDataReader.GetOrdinal("Distrito")].ToString(),
                                    CostoPorClase = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("CostoPorClase")]),
                                    DescuentoPorminuto = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("DescuentoPorminuto")]),
                                    Estado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Estado")])
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;
        }

        public string CentroEntrenamiento_uspRegistrarProfesor(CentroEntrenamiento_ProfesorDTO item)
        {
            string resultado = string.Empty;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("CentroEntrenamiento_uspRegistrarProfesor", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                   
                    cmd.Parameters.Add(new SqlParameter("@CodigoProfesional", System.Data.SqlDbType.VarChar,50)).Direction = ParameterDirection.Output;

                    if (!string.IsNullOrEmpty(item.Nombres))
                    {
                        cmd.Parameters.Add(new SqlParameter("@Nombres", System.Data.SqlDbType.VarChar, 100)).Value = item.Nombres;
                    }

                    if (!string.IsNullOrEmpty(item.Nombres))
                    {
                        cmd.Parameters.Add(new SqlParameter("@Apellidos", System.Data.SqlDbType.VarChar, 100)).Value = item.Apellidos;
                    }

                    cmd.Parameters.Add(new SqlParameter("@TipoDocumento", System.Data.SqlDbType.Int)).Value = item.TipoDocumento;
                    cmd.Parameters.Add(new SqlParameter("@DNI", System.Data.SqlDbType.VarChar, 100)).Value = item.NroDocumento;
                   
                    if (item.Telefono != null)
                    {
                        cmd.Parameters.Add(new SqlParameter("@Telefono", System.Data.SqlDbType.VarChar, 20)).Value = item.Telefono;
                    }
                    else
                    {
                        cmd.Parameters.Add(new SqlParameter("@Telefono", System.Data.SqlDbType.VarChar, 20)).Value = string.Empty;
                    }
                    
                    if (item.Celular != null)
                    {
                        cmd.Parameters.Add(new SqlParameter("@Celular", System.Data.SqlDbType.VarChar, 20)).Value = item.Celular;
                    }
                    else
                    {
                        cmd.Parameters.Add(new SqlParameter("@Celular", System.Data.SqlDbType.VarChar, 20)).Value = string.Empty;
                    }

                    if (item.Correo != null)
                    {
                        cmd.Parameters.Add(new SqlParameter("@Correo", System.Data.SqlDbType.VarChar, 100)).Value = item.Correo;
                    }
                    else
                    {
                        cmd.Parameters.Add(new SqlParameter("@Correo", System.Data.SqlDbType.VarChar, 100)).Value = string.Empty;
                    }

                    if (item.ImagenUrl != null)
                    {
                        cmd.Parameters.Add(new SqlParameter("@ImagenUrl", System.Data.SqlDbType.VarChar, 200)).Value = item.ImagenUrl;
                    }
                    else
                    {
                        cmd.Parameters.Add(new SqlParameter("@ImagenUrl", System.Data.SqlDbType.VarChar, 200)).Value = string.Empty;
                    }

                    cmd.Parameters.Add(new SqlParameter("@FechaNacimiento", System.Data.SqlDbType.DateTime)).Value = item.FechaNacimiento;                    
                    cmd.Parameters.Add(new SqlParameter("@Genero", System.Data.SqlDbType.VarChar, 1)).Value = item.Genero;

                    if (item.Facebook != null)
                    {
                        cmd.Parameters.Add(new SqlParameter("@Facebook", System.Data.SqlDbType.VarChar, 100)).Value = item.Facebook;
                    }
                    else
                    {
                        cmd.Parameters.Add(new SqlParameter("@Facebook", System.Data.SqlDbType.VarChar, 100)).Value = string.Empty;
                    }

                    if (item.Ubigeo != null)
                    {
                        cmd.Parameters.Add(new SqlParameter("@Ubigeo", System.Data.SqlDbType.VarChar, 50)).Value = item.Ubigeo;
                    }
                    else
                    {
                        cmd.Parameters.Add(new SqlParameter("@Ubigeo", System.Data.SqlDbType.VarChar, 50)).Value = string.Empty;
                    }

                    if (item.Direccion != null)
                    {
                        cmd.Parameters.Add(new SqlParameter("@Direccion", System.Data.SqlDbType.VarChar, 100)).Value = item.Direccion;
                    }
                    else
                    {
                        cmd.Parameters.Add(new SqlParameter("@Direccion", System.Data.SqlDbType.VarChar, 100)).Value = string.Empty;
                    }

                    if (item.Distrito != null)
                    {
                        cmd.Parameters.Add(new SqlParameter("@Distrito", System.Data.SqlDbType.VarChar, 100)).Value = item.Distrito;
                    }
                    else
                    {
                        cmd.Parameters.Add(new SqlParameter("@Distrito", System.Data.SqlDbType.VarChar, 100)).Value = string.Empty;
                    }

                    cmd.Parameters.Add(new SqlParameter("@CostoPorClase", System.Data.SqlDbType.Decimal)).Value = item.CostoPorClase;
                    cmd.Parameters.Add(new SqlParameter("@DescuentoPorminuto", System.Data.SqlDbType.Decimal)).Value = item.DescuentoPorminuto;

                    cmd.Parameters.Add(new SqlParameter("@Estado", System.Data.SqlDbType.Bit)).Value = item.Estado;

                    if (!string.IsNullOrEmpty(item.UsuarioCreacion))
                    {
                        cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;
                    }
                    cmd.ExecuteNonQuery();
                    resultado = cmd.Parameters["@CodigoProfesional"].Value.ToString();
                }

            }
            return resultado;
        }

        public void CentroEntrenamiento_uspActualizarProfesor(CentroEntrenamiento_ProfesorDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("CentroEntrenamiento_uspActualizarProfesor", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoProfesional", System.Data.SqlDbType.VarChar, 50)).Value = item.CodigoProfesional;

                    if (!string.IsNullOrEmpty(item.Nombres))
                    {
                        cmd.Parameters.Add(new SqlParameter("@Nombres", System.Data.SqlDbType.VarChar, 100)).Value = item.Nombres;
                    }

                    if (!string.IsNullOrEmpty(item.Nombres))
                    {
                        cmd.Parameters.Add(new SqlParameter("@Apellidos", System.Data.SqlDbType.VarChar, 100)).Value = item.Apellidos;
                    }

                    cmd.Parameters.Add(new SqlParameter("@TipoDocumento", System.Data.SqlDbType.Int)).Value = item.TipoDocumento;
                    
                    if (item.NroDocumento != null)
                    {
                        cmd.Parameters.Add(new SqlParameter("@DNI", System.Data.SqlDbType.VarChar, 20)).Value = item.NroDocumento;
                    }
                    else
                    {
                        cmd.Parameters.Add(new SqlParameter("@DNI", System.Data.SqlDbType.VarChar, 20)).Value = string.Empty;
                    }
                    if (item.Telefono != null)
                    {
                        cmd.Parameters.Add(new SqlParameter("@Telefono", System.Data.SqlDbType.VarChar, 20)).Value = item.Telefono;
                    }
                    else
                    {
                        cmd.Parameters.Add(new SqlParameter("@Telefono", System.Data.SqlDbType.VarChar, 20)).Value = string.Empty;
                    }

                    if (item.Celular != null)
                    {
                        cmd.Parameters.Add(new SqlParameter("@Celular", System.Data.SqlDbType.VarChar, 20)).Value = item.Celular;
                    }
                    else
                    {
                        cmd.Parameters.Add(new SqlParameter("@Celular", System.Data.SqlDbType.VarChar, 20)).Value = string.Empty;
                    }

                    if (item.Correo != null)
                    {
                        cmd.Parameters.Add(new SqlParameter("@Correo", System.Data.SqlDbType.VarChar, 100)).Value = item.Correo;
                    }
                    else
                    {
                        cmd.Parameters.Add(new SqlParameter("@Correo", System.Data.SqlDbType.VarChar, 100)).Value = string.Empty;
                    }

                    if (item.ImagenUrl != null)
                    {
                        cmd.Parameters.Add(new SqlParameter("@ImagenUrl", System.Data.SqlDbType.VarChar, 200)).Value = item.ImagenUrl;
                    }
                    else
                    {
                        cmd.Parameters.Add(new SqlParameter("@ImagenUrl", System.Data.SqlDbType.VarChar, 200)).Value = string.Empty;
                    }

                    cmd.Parameters.Add(new SqlParameter("@FechaNacimiento", System.Data.SqlDbType.DateTime)).Value = item.FechaNacimiento;
                    cmd.Parameters.Add(new SqlParameter("@Genero", System.Data.SqlDbType.VarChar, 1)).Value = item.Genero;

                    if (item.Facebook != null)
                    {
                        cmd.Parameters.Add(new SqlParameter("@Facebook", System.Data.SqlDbType.VarChar, 100)).Value = item.Facebook;
                    }
                    else
                    {
                        cmd.Parameters.Add(new SqlParameter("@Facebook", System.Data.SqlDbType.VarChar, 100)).Value = string.Empty;
                    }                    

                    if (item.Direccion != null)
                    {
                        cmd.Parameters.Add(new SqlParameter("@Direccion", System.Data.SqlDbType.VarChar, 100)).Value = item.Direccion;
                    }
                    else
                    {
                        cmd.Parameters.Add(new SqlParameter("@Direccion", System.Data.SqlDbType.VarChar, 100)).Value = string.Empty;
                    }

                    if (item.Distrito != null)
                    {
                        cmd.Parameters.Add(new SqlParameter("@Distrito", System.Data.SqlDbType.VarChar, 100)).Value = item.Distrito;
                    }
                    else
                    {
                        cmd.Parameters.Add(new SqlParameter("@Distrito", System.Data.SqlDbType.VarChar, 100)).Value = string.Empty;
                    }

                    cmd.Parameters.Add(new SqlParameter("@CostoPorClase", System.Data.SqlDbType.Decimal)).Value = item.CostoPorClase;
                    cmd.Parameters.Add(new SqlParameter("@DescuentoPorminuto", System.Data.SqlDbType.Decimal)).Value = item.DescuentoPorminuto;

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
