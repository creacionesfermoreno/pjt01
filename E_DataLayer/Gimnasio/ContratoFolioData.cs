using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using E_DataModel.Gimnasio;
using E_DataModel.Common;
using System.Data.SqlClient;

namespace E_DataLayer.Gimnasio
{
	public class ContratoFolioData
	{
        public ContratoFolioDTO ListarContratoMembresia(ContratoFolioDTO oContratoMembresiaDTO)
		{
            ContratoFolioDTO itemDTO = new ContratoFolioDTO();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspBuscarContrato", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oContratoMembresiaDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oContratoMembresiaDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoMembresia", System.Data.SqlDbType.Int)).Value = oContratoMembresiaDTO.codigo_Membresia;
                    
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new ContratoFolioDTO()
                                {
                                    codigo_Membresia = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("codigo_Membresia")]),
                                    fechaInscripcion_Membresia = oIDataReader[oIDataReader.GetOrdinal("fechaInscripcion_Membresia")].ToString(),
                                    fechaInicio_Membresia = oIDataReader[oIDataReader.GetOrdinal("fechaInicio_Membresia")].ToString(),
                                    fechaFin_Membresia = oIDataReader[oIDataReader.GetOrdinal("fechaFin_Membresia")].ToString(),
                                    costo_Membresia = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("costo_Membresia")]),
                                    nroContrato_Membresia = oIDataReader[oIDataReader.GetOrdinal("nroContrato_Membresia")].ToString(),
                                    codigo_Socio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("codigo_Socio")]),
                                    nombre_Socio = oIDataReader[oIDataReader.GetOrdinal("nombre_Socio")].ToString(),
                                    apellido_Socio = oIDataReader[oIDataReader.GetOrdinal("apellido_Socio")].ToString(),
                                    dni_Socio = oIDataReader[oIDataReader.GetOrdinal("dni_Socio")].ToString(),
                                    telefono_Socio = oIDataReader[oIDataReader.GetOrdinal("telefono_Socio")].ToString(),
                                    celular_Socio = oIDataReader[oIDataReader.GetOrdinal("celular_Socio")].ToString(),
                                    correo_Socio = oIDataReader[oIDataReader.GetOrdinal("correo_Socio")].ToString(),
                                    fechaNacimiento_Socio = oIDataReader[oIDataReader.GetOrdinal("fechaNacimiento_Socio")].ToString(),
                                    genero_Socio = oIDataReader[oIDataReader.GetOrdinal("genero_Socio")].ToString(),
                                    facebook_Socio = oIDataReader[oIDataReader.GetOrdinal("facebook_Socio")].ToString(),
                                    referidoPor_Socio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("referidoPor_Socio")]),
                                    direccion_Socio = oIDataReader[oIDataReader.GetOrdinal("direccion_Socio")].ToString(),
                                    distrito_Socio = oIDataReader[oIDataReader.GetOrdinal("distrito_Socio")].ToString(),
                                    ocupacion_Socio = oIDataReader[oIDataReader.GetOrdinal("ocupacion_Socio")].ToString(),
                                    tipo_Socio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("tipo_Socio")]),
                                    codigo_Paquete = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("codigo_Paquete")]),
                                    nombre_Paquete = oIDataReader[oIDataReader.GetOrdinal("nombre_Paquete")].ToString(),
                                    valorDias_Paquete = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("valorDias_Paquete")]),
                                    diasFreezing_Paquete = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("diasFreezing_Paquete")]),
                                    Clausula = oIDataReader[oIDataReader.GetOrdinal("Clausula")].ToString(),
                                    Edad = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Edad")]),
                                    Apoderado = oIDataReader[oIDataReader.GetOrdinal("Apoderado")].ToString(),
                                    Parentesco_Apoderado = oIDataReader[oIDataReader.GetOrdinal("Parentesco_Apoderado")].ToString(),
                                    Apoderado_DNI = oIDataReader[oIDataReader.GetOrdinal("Apoderado_DNI")].ToString(),
                                    Apoderado_Codigo = 0,
                                    observacionTraspaso = oIDataReader[oIDataReader.GetOrdinal("ObservacionTraspaso")].ToString(),
                                    fechaTraspaso = oIDataReader[oIDataReader.GetOrdinal("FechaTraspaso")].ToString(),
                                    responsableTraspaso = oIDataReader[oIDataReader.GetOrdinal("responsableTraspaso")].ToString(),
                                    AsesorComercial_Membresia = oIDataReader[oIDataReader.GetOrdinal("AsesorComercial")].ToString(),
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
