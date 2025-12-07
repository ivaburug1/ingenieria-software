using _686DP_BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _686DP_Dal;
using _686DP_SERVICIOS;
using System.Data.SqlClient;
using System.Collections;

namespace _686DP_MPP
{
    public class _686DP_MPPDigitoVerificador
    {
        _686DPCriptoManager cm = new _686DPCriptoManager();
        _686DPDalGeneral dal = new _686DPDalGeneral();
        

        private string ObtenerPrimaryKey(string nombreTabla)
        {
            string query = $@"
        SELECT COLUMN_NAME
        FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE
        WHERE TABLE_NAME = '{nombreTabla}'";

            DataTable dt = dal._686DPConsultar(query, null);

            if (dt.Rows.Count > 0)
                return dt.Rows[0]["COLUMN_NAME"].ToString();
            else
                return null;
        }

        private string CalcularDVHFila(DataRow fila)
        {
            StringBuilder sb = new StringBuilder();

            foreach (DataColumn col in fila.Table.Columns)
            {
                if (col.ColumnName.Equals("DP686_DVH", StringComparison.OrdinalIgnoreCase))
                    continue;

                sb.Append(fila[col]?.ToString() ?? "");
            }

            return cm._686DPGetSHA256(sb.ToString());
        }


        public void ActualizarDVHFilaPorTabla(string nombreTabla)
        {
            string consulta = "";
            if(nombreTabla == "[686DP_Cliente].[686DP_Clientes]")
            {
                consulta = $"SELECT * FROM [686DP_Cliente].[686DP_Clientes]";
            }
            else
            {
                consulta = $"SELECT * FROM [{nombreTabla}]";
            }
            DataTable dt = dal._686DPConsultar(consulta, null);

            if(dt.Rows.Count > 0)
            {
                foreach (DataRow fila in dt.Rows)
                {
                    string dvh = CalcularDVHFila(fila);
                    string pk = "";
                    if (nombreTabla == "[686DP_Cliente].[686DP_Clientes]")
                    {
                        pk = ObtenerPrimaryKey("686DP_Clientes");
                    }
                    else
                    {
                        pk = ObtenerPrimaryKey(nombreTabla);
                    }


                    string valorPK = fila[pk].ToString();
                    string update = "";

                    if(nombreTabla == "[686DP_Cliente].[686DP_Clientes]")
                    {
                        update = $"UPDATE [686DP_Cliente].[686DP_Clientes] SET DP686_DVH = @DVH WHERE [{pk}] = @PK";
                    }
                    else
                    {
                        update = $"UPDATE [{nombreTabla}] SET DP686_DVH = @DVH WHERE [{pk}] = @PK";
                    }


                    ArrayList parametros = new ArrayList
                {
                    new SqlParameter("@DVH", dvh),
                    new SqlParameter("@PK", valorPK)
                };

                    dal._686DPEscribir(update, parametros);
                }
            }
            
        }

        public List<string> VerificarFilaPorTabla(string nombreTabla)
        {
            List<string> erroresFila = new List<string>();
            string consulta = "";
            if (nombreTabla == "[686DP_Cliente].[686DP_Clientes]")
            {
                consulta = $"SELECT * FROM [686DP_Cliente].[686DP_Clientes]";
            }
            else
            {
                consulta = $"SELECT * FROM [{nombreTabla}]";
            }

            erroresFila.Clear();

            DataTable dt = dal._686DPConsultar(consulta, null);
            string pk = "";
            if (nombreTabla == "[686DP_Cliente].[686DP_Clientes]")
            {
                pk = ObtenerPrimaryKey("686DP_Clientes");
            }
            else
            {
                pk = ObtenerPrimaryKey(nombreTabla);
            }
            foreach (DataRow fila in dt.Rows)
            {
                string dvhCalculado = CalcularDVHFila(fila);
                string dvhAlmacenado = fila["DP686_DVH"].ToString();

                if (dvhCalculado != dvhAlmacenado)
                {
                    string id = fila[pk].ToString();
                    erroresFila.Add($"Fila corrupta en {nombreTabla} => ID ={id}");
                }
            }
            return erroresFila;
            
        }


        public _686DP_DigitoVerificador Calcular(string consulta, string nombreTabla)
        {
            string dvh = "";
            string dvv = "";
            DataTable dt = dal._686DPConsultar(consulta, null);

            if (dt == null || dt.Rows.Count == 0)
            {
                dvh = cm._686DPGetSHA256($"{nombreTabla}_SIN_REGISTROS");
                dvv = cm._686DPGetSHA256($"{nombreTabla}_SIN_REGISTROS");
                return new _686DP_DigitoVerificador(nombreTabla, dvh, dvv);
            }

            string contenidoFilas = "";
            string contenidoColumnas = "";

            foreach (DataRow fila in dt.Rows)
            {
                foreach (var celda in fila.ItemArray)
                {
                    contenidoFilas += celda?.ToString() ?? "";

                }
            }

            for (int c = 0; c < dt.Columns.Count; c++)
            {
                foreach (DataRow fila in dt.Rows)
                {
                    contenidoColumnas += fila[c]?.ToString() ?? "";
                }
            }

             dvh = cm._686DPGetSHA256(contenidoFilas);
             dvv = cm._686DPGetSHA256(contenidoColumnas);
            _686DP_DigitoVerificador resultado = new _686DP_DigitoVerificador(nombreTabla, dvh, dvv);
            return resultado;
        }
        
        public _686DP_DigitoVerificador CalcularDVPolizas()
        {
            string consulta = @"
                SELECT [DP686_NPoliza],
                       [DP686_Estado],
                       [DP686_valorTotal],
                       [DP686_FechaVencimiento],
                       [DP686_Endoso],
                       [DP686_CodSeguro],
                       [DP686_CodPlan]
                FROM [dbo].[686DP_Poliza]";
            return Calcular(consulta, "686DP_Poliza");
        }

        public _686DP_DigitoVerificador CalcularDVCliente()
        {
            string consulta = @"
                SELECT [DP686_DNI],
                       [DP686_Nombre],
                       [DP686_Apellido],
                       [DP686_Email],
                       [DP686_Domicilio],
                       [DP686DP_CodigoPostal],
                       [DP686_Estado]
                FROM [686DP_Cliente].[686DP_Clientes]";
            //ActualizarDVHFilaPorTabla("[686DP_Cliente].[686DP_Clientes]", consulta);
            return Calcular(consulta, "686DP_Clientes");
        }

        public _686DP_DigitoVerificador CalcularDVCobertura()
        {
            string consulta = @"
                SELECT [DP686_Descripcion],
                       [DP686_SumaAsegurada],
                       [CodigoCobertura]
                FROM [dbo].[686DP_Cobertura]";
            //ActualizarDVHFilaPorTabla("[686DP_Cobertura]", consulta);
            return Calcular(consulta, "686DP_Cobertura");
        }

        public _686DP_DigitoVerificador CalcularDVPlan()
        {
            string consulta = @"
                SELECT [DP686_CodigoPlan],
                       [DP686_Franquicia],
                       [DP686_Prima]
                FROM [dbo].[686DP_Plan]";
            //ActualizarDVHFilaPorTabla("686DP_Plan", consulta);
            return Calcular(consulta, "686DP_Plan");
        }

        public _686DP_DigitoVerificador CalcularDVSeguro()
        {
            string consulta = @"
                SELECT [DP686_CodSeguro],
                       [DP686_ProductoNombre]
                FROM [dbo].[686DP_Seguro]";
            //ActualizarDVHFilaPorTabla("686DP_Seguro", consulta);
            return Calcular(consulta, "686DP_Seguro");
        }

        public _686DP_DigitoVerificador CalcularDVSiniestro()
        {
            string consulta = @"
                SELECT [CodSiniestro],
                       [Fecha],
                       [Valor],
                       [ValorDeReparacion],
                       [ValorDelBien],
                       [Estado],
                       [Descripcion]
                FROM [dbo].[686DP_Siniestro]";
            //ActualizarDVHFilaPorTabla("686DP_Siniestro", consulta);
            return Calcular(consulta, "686DP_Siniestro");
        }

        public _686DP_DigitoVerificador CalcularDVFactura()
        {
            string consulta = @"
                SELECT [CodFactura],
                       [CodSiniestro],
                       [Fecha]
                FROM [dbo].[686DP_Factura]";
            //ActualizarDVHFilaPorTabla("686DP_Factura", consulta);
            return Calcular(consulta, "686DP_Factura");
        }

        public void Grabar(_686DP_DigitoVerificador dv)
        {
            try
            {
                string query = @"
                    IF EXISTS (SELECT 1 FROM [dbo].[686DP_DigitoVerificador] WHERE DP686NombreTabla = @NombreTabla)
                        UPDATE [dbo].[686DP_DigitoVerificador]
                        SET DP686DVH = @DVH,
                            DP686DVV = @DVV
                        WHERE DP686NombreTabla = @NombreTabla;
                    ELSE
                        INSERT INTO [dbo].[686DP_DigitoVerificador]
                            (DP686NombreTabla, DP686DVH, DP686DVV)
                        VALUES (@NombreTabla, @DVH, @DVV);";

                ArrayList Parameters = new ArrayList
                {
                    new SqlParameter("@NombreTabla", dv.DP686NombreTabla),
                    new SqlParameter("@DVH", dv.DP686DVH),
                    new SqlParameter("@DVV", dv.DP686DVV)
                };

                dal._686DPEscribir(query, Parameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar el dígito verificador: " + ex.Message);
            }
        }

        public List<_686DP_DigitoVerificador> TraerDVs()
        {
            try
            {
                List<_686DP_DigitoVerificador> listaDVs = new List<_686DP_DigitoVerificador>();

                string consulta = @"
                SELECT [DP686NombreTabla],
                       [DP686DVH],
                       [DP686DVV]
                FROM [dbo].[686DP_DigitoVerificador]";

                DataTable dt = dal._686DPConsultar(consulta, null);

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow fila in dt.Rows)
                    {
                        string nombreTabla = fila["DP686NombreTabla"].ToString();
                        string dvh = fila["DP686DVH"].ToString();
                        string dvv = fila["DP686DVV"].ToString();

                        _686DP_DigitoVerificador dv = new _686DP_DigitoVerificador(nombreTabla, dvh, dvv);
                        listaDVs.Add(dv);
                    }
                }

                return listaDVs;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al traer los dígitos verificadores: " + ex.Message);
            }
        }

        public _686DP_DigitoVerificador CalcularPlanesCoberturas()
        {
            string consulta = @"
               SELECT [DP686_CodigoPlan]
                    ,[CodigoCobertura]
              FROM [dbo].[686DP_PlanesCoberturas]";
            //ActualizarDVHFilaPorTabla("686DP_PlanesCoberturas", consulta);
            return Calcular(consulta, "686DP_PlanesCoberturas");
        }

        public _686DP_DigitoVerificador CalcularSeguroPlan()
        {
            string consulta = @"
            SELECT [DP686_CodSeguro]
                  ,[DP686_CodigoPlan]
              FROM [dbo].[686DP_SeguroPlan]";
            //ActualizarDVHFilaPorTabla("686DP_SeguroPlan", consulta);
            return Calcular(consulta, "686DP_SeguroPlan");
        }

        public _686DP_DigitoVerificador CalcularClientePoliza()
        {
            string consulta = @"
            SELECT [DP686_NPoliza]
                  ,[DP686_DNICliente]
              FROM [dbo].[686DPClientePoliza]";
            //ActualizarDVHFilaPorTabla("686DPClientePoliza", consulta);
            return Calcular(consulta, "686DPClientePoliza");
        }

        public _686DP_DigitoVerificador CalcularPolizaSiniestro()
        {
            string consulta = @"
            SELECT [CodSiniestro]
                  ,[DP686_NPoliza]
              FROM [dbo].[686DP_PolizaSiniestro]";
            //ActualizarDVHFilaPorTabla("686DP_PolizaSiniestro", consulta);
            return Calcular(consulta, "686DP_PolizaSiniestro");
        }

        public _686DP_DigitoVerificador CalcularPolizaCancelacion()
        {
            string consulta = @"
            SELECT [DP686_NPoliza]
                  ,[Motivo]
              FROM [dbo].[686DPPolizaCancelacion]";
            //ActualizarDVHFilaPorTabla("686DPPolizaCancelacion", consulta);
            return Calcular(consulta, "686DPPolizaCancelacion");
        }

        public void RepararDVHFaltantes(string nombreTabla)
        {
            string consulta = "";
            if (nombreTabla == "686DP_Clientes")
            {
                consulta = $"SELECT * FROM [686DP_Cliente].[686DP_Clientes] WHERE DP686_DVH IS NULL";
            }
            else
            {
                consulta = $"SELECT * FROM [{nombreTabla}] WHERE DP686_DVH IS NULL";
            }
            

            DataTable dt = dal._686DPConsultar(consulta, null);

            if (dt.Rows.Count == 0) return;

            string pk = ObtenerPrimaryKey(nombreTabla);

            foreach (DataRow fila in dt.Rows)
            {
                if (fila["DP686_DVH"]==DBNull.Value)
                {
                    string dvh = CalcularDVHFila(fila);
                    string valorPK = fila[pk].ToString();
                    string update = "";
                    if (nombreTabla == "686DP_Clientes")
                    {
                        update = $"UPDATE [686DP_Cliente].[686DP_Clientes] SET DP686_DVH = @DVH WHERE [{pk}] = @PK";
                    }
                    else
                    {
                        update = $"UPDATE [{nombreTabla}] SET DP686_DVH = @DVH WHERE [{pk}] = @PK";
                    }
                    

                    ArrayList parametros = new ArrayList
                    {
                        new SqlParameter("@DVH", dvh),
                        new SqlParameter("@PK", valorPK)
                    };

                    dal._686DPEscribir(update, parametros);
                }
            }
        }

        public void RepararDVHDeTodasLasTablas()
        {
            List<string> tablas = new List<string>
            {
                "686DP_Poliza",
                "686DP_Clientes",
                "686DP_Cobertura",
                "686DP_Plan",
                "686DP_Seguro",
                "686DP_Siniestro",
                "686DP_Factura",
                "686DP_PlanesCoberturas",
                "686DP_SeguroPlan",
                "686DPClientePoliza",
                "686DP_PolizaSiniestro",
                "686DPPolizaCancelacion"
            };

            foreach (string tabla in tablas)
            {
                RepararDVHFaltantes(tabla);
            }
        }


    }
}
