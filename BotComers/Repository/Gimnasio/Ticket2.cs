using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;

namespace BotComers
{

    public class Ticket2
    {
        ArrayList headerLines = new ArrayList();
        ArrayList subHeaderLines = new ArrayList();
        ArrayList items = new ArrayList();
        ArrayList totales = new ArrayList();
        ArrayList footerLines = new ArrayList();
        private Image headerImage = null;
        bool _DrawItemHeaders = true;
        int count = 0;

        int maxChar = 40;
        int maxCharDescription = 20;

        int imageHeight = 0;

        float leftMargin = 0;
        float topMargin = 3;

        string fontName = "Courier New";
        int fontSize = 9;

        Font printFont = null;
        SolidBrush myBrush = new SolidBrush(Color.Black);

        Graphics gfx = null;

        string line = null;

        public Ticket2()
        {

        }

        public Image HeaderImage
        {
            get { return headerImage; }
            set { if (headerImage != value) headerImage = value; }
        }

        public int MaxChar
        {
            get { return maxChar; }
            set { if (value != maxChar) maxChar = value; }
        }
        public bool DrawItemHeaders
        {
            set { _DrawItemHeaders = value; }
        }
        public int MaxCharDescription
        {
            get { return maxCharDescription; }
            set { if (value != maxCharDescription) maxCharDescription = value; }
        }

        public int FontSize
        {
            get { return fontSize; }
            set { if (value != fontSize) fontSize = value; }
        }

        public string FontName
        {
            get { return fontName; }
            set { if (value != fontName) fontName = value; }
        }

        public void AddHeaderLine(string line)
        {
            headerLines.Add(line);
        }

        public void AddSubHeaderLine(string line)
        {
            subHeaderLines.Add(line);
        }

        public void AddItem(string cantidad, string item, string price)
        {
            OrderItem newItem = new OrderItem('?');
            items.Add(newItem.GenerateItem(cantidad, item, price));
        }

        public void AddItem2(string cantidad, string item, string price, string UsuarioCreacion)
        {
            OrderItem newItem = new OrderItem('?');
            items.Add(newItem.GenerateItem2(cantidad, item, price, UsuarioCreacion));
        }

        public void AddTotal(string name, string price)
        {
            OrderTotal newTotal = new OrderTotal('?');
            totales.Add(newTotal.GenerateTotal(name, price));
        }

        public void AddFooterLine(string line)
        {
            footerLines.Add(line);
        }

        private string AlignRightText2(int lenght)
        {
            string espacios = "                                                                      ";
            int spaces = maxChar - lenght;
            for (int x = 0; x < spaces; x++)
                espacios += "";
            return espacios;
        }

        private string AlignRightText(int lenght)
        {
            string espacios = "";
            int spaces = maxChar - lenght;
            for (int x = 0; x < spaces; x++)
                espacios += "  ";
            return espacios;
        }

        private string DottedLine()
        {
            string dotted = "";
            for (int x = 0; x < maxChar; x++)
                dotted += "_";
            return dotted;
        }

        public bool PrinterExists(string impresora)
        {
            foreach (String strPrinter in PrinterSettings.InstalledPrinters)
            {
                if (impresora == strPrinter)
                    return true;
            }
            return false;
        }

        public void PrintTicket(string impresora)
        {
            if (impresora == string.Empty)
            {
                impresora = GetImpresoraDefecto();
            }
            try
            {
                printFont = new Font(fontName, fontSize, FontStyle.Regular);
                PrintDocument pr = new PrintDocument();
                pr.PrinterSettings.PrinterName = impresora;
                pr.PrintPage += new PrintPageEventHandler(pr_PrintPage);
                pr.Print();
            }
            catch (Exception)
            {


            }

        }

        public void PrintTicket2(string impresora)
        {
            if (impresora == string.Empty)
            {
                impresora = GetImpresoraDefecto();
            }

            try
            {

                printFont = new Font(fontName, fontSize, FontStyle.Regular);
                PrintDocument pr = new PrintDocument();
                pr.PrinterSettings.PrinterName = impresora;
                pr.PrintPage += new PrintPageEventHandler(pr_PrintPage2);
                pr.Print();
            }
            catch (Exception)
            {


            }

        }


        public static string GetImpresoraDefecto()
        {
            for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
            {
                PrinterSettings a = new PrinterSettings();
                a.PrinterName = PrinterSettings.InstalledPrinters[i].ToString();
                if (a.IsDefaultPrinter)
                {
                    return PrinterSettings.InstalledPrinters[i].ToString();

                }
            }
            return "";
        }

        public void PrintTicketPreview(string impresora)
        {
            //printFont = new Font(fontName, fontSize, FontStyle.Regular);
            //PrintDocument pr = new PrintDocument();
            //pr.PrinterSettings.PrinterName = impresora;
            //pr.PrintPage += new PrintPageEventHandler(pr_PrintPage);
            //PrintPreviewDialog _Preview = new PrintPreviewDialog();
            //_Preview.Document = pr;
            //_Preview.ShowDialog();
        }

        private void pr_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.PageUnit = GraphicsUnit.Millimeter;
            gfx = e.Graphics;

            DrawImage();
            DrawHeader();
            DrawSubHeader();
            DrawItems();
            DrawTotales();
            DrawFooter();

            if (headerImage != null)
            {
                HeaderImage.Dispose();
                headerImage.Dispose();
            }
        }

        private void pr_PrintPage2(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.PageUnit = GraphicsUnit.Millimeter;
            gfx = e.Graphics;

            DrawImage();
            DrawHeader();
            DrawSubHeader();
            DrawItems2();
            DrawTotales();
            DrawFooter();

            if (headerImage != null)
            {
                HeaderImage.Dispose();
                headerImage.Dispose();
            }
        }

        private float YPosition()
        {
            return topMargin + (count * printFont.GetHeight(gfx) + imageHeight);
        }

        private void DrawImage()
        {
            if (headerImage != null)
            {
                try
                {
                    gfx.DrawImage(headerImage, new Point((int)leftMargin, (int)YPosition()));
                    double height = ((double)headerImage.Height / 58) * 15;
                    imageHeight = (int)Math.Round(height) + 3;
                }
                catch (Exception)
                {
                }
            }
        }

        private void DrawHeader()
        {
            foreach (string header in headerLines)
            {
                if (header.Length > maxChar)
                {
                    int currentChar = 0;
                    int headerLenght = header.Length;

                    while (headerLenght > maxChar)
                    {
                        line = header.Substring(currentChar, maxChar);
                        gfx.DrawString(line, printFont, myBrush, leftMargin, YPosition(), new StringFormat());

                        count++;
                        currentChar += maxChar;
                        headerLenght -= maxChar;
                    }
                    line = header;
                    gfx.DrawString(line.Substring(currentChar, line.Length - currentChar), printFont, myBrush, leftMargin, YPosition(), new StringFormat());
                    count++;
                }
                else
                {
                    line = header;
                    gfx.DrawString(line, printFont, myBrush, leftMargin, YPosition(), new StringFormat());

                    count++;
                }
            }
            DrawEspacio();
        }

        private void DrawSubHeader()
        {
            foreach (string subHeader in subHeaderLines)
            {
                if (subHeader.Length > maxChar)
                {
                    int currentChar = 0;
                    int subHeaderLenght = subHeader.Length;

                    while (subHeaderLenght > maxChar)
                    {
                        line = subHeader;
                        gfx.DrawString(line.Substring(currentChar, maxChar), printFont, myBrush, leftMargin, YPosition(), new StringFormat());

                        count++;
                        currentChar += maxChar;
                        subHeaderLenght -= maxChar;
                    }
                    line = subHeader;
                    gfx.DrawString(line.Substring(currentChar, line.Length - currentChar), printFont, myBrush, leftMargin, YPosition(), new StringFormat());
                    count++;
                }
                else
                {
                    line = subHeader;

                    gfx.DrawString(line, printFont, myBrush, leftMargin, YPosition(), new StringFormat());

                    count++;

                    line = DottedLine();

                    gfx.DrawString(line, printFont, myBrush, leftMargin, YPosition(), new StringFormat());

                    count++;
                }
            }
            DrawEspacio();
        }

        private void DrawItems()
        {
            OrderItem ordIt = new OrderItem('?');
            if (_DrawItemHeaders)
            {
                gfx.DrawString("CANT  DESCRIPCION                  IMPORTE", printFont, myBrush, leftMargin, YPosition(), new StringFormat());
            }
            count++;
            DrawEspacio();

            foreach (string item in items)
            {
                line = ordIt.GetItemCantidad(item);

                gfx.DrawString(line, printFont, myBrush, leftMargin, YPosition(), new StringFormat());

                line = ordIt.GetItemPrice(item);
                line = AlignRightText(line.Length) + line;

                gfx.DrawString(line, printFont, myBrush, leftMargin, YPosition(), new StringFormat());

                string name = ordIt.GetItemName(item);

                leftMargin = 0;
                if (name.Length > maxCharDescription)
                {
                    int currentChar = 0;
                    int itemLenght = name.Length;

                    while (itemLenght > maxCharDescription)
                    {
                        line = ordIt.GetItemName(item);
                        gfx.DrawString("      " + line.Substring(currentChar, maxCharDescription), printFont, myBrush, leftMargin, YPosition(), new StringFormat());

                        count++;
                        currentChar += maxCharDescription;
                        itemLenght -= maxCharDescription;
                    }

                    line = ordIt.GetItemName(item);
                    gfx.DrawString("      " + line.Substring(currentChar, line.Length - currentChar), printFont, myBrush, leftMargin, YPosition(), new StringFormat());
                    count++;
                }
                else
                {
                    gfx.DrawString("      " + ordIt.GetItemName(item), printFont, myBrush, leftMargin, YPosition(), new StringFormat());

                    count++;
                }
            }

            leftMargin = 0;
            DrawEspacio();
            line = DottedLine();

            gfx.DrawString(line, printFont, myBrush, leftMargin, YPosition(), new StringFormat());

            count++;
            DrawEspacio();
        }

        private void DrawItems2()
        {
            OrderItem ordIt = new OrderItem('?');
            if (_DrawItemHeaders)
            {
                gfx.DrawString("#P.  DESCRIP.                                          IMPTE.", printFont, myBrush, leftMargin, YPosition(), new StringFormat());
            }
            count++;
            DrawEspacio();

            foreach (string item in items)
            {
                line = ordIt.GetItemCantidad(item);

                gfx.DrawString(line, printFont, myBrush, leftMargin, YPosition(), new StringFormat());

                line = ordIt.GetItemPrice(item);
                line = AlignRightText2(line.Length) + line;

                gfx.DrawString(line, printFont, myBrush, leftMargin, YPosition(), new StringFormat());

                //string user = ordIt.GetItemUser(item);
                string name = ordIt.GetItemName(item);

                leftMargin = 0;
                if (name.Length > maxCharDescription)
                {
                    int currentChar = 0;
                    int itemLenght = name.Length;

                    while (itemLenght > maxCharDescription)
                    {
                        line = ordIt.GetItemName(item);
                        gfx.DrawString("      " + line.Substring(currentChar, maxCharDescription), printFont, myBrush, leftMargin, YPosition(), new StringFormat());

                        count++;
                        currentChar += maxCharDescription;
                        itemLenght -= maxCharDescription;
                    }

                    line = ordIt.GetItemName(item);
                    gfx.DrawString("      " + line.Substring(currentChar, line.Length - currentChar), printFont, myBrush, leftMargin, YPosition(), new StringFormat());
                    count++;
                }
                else
                {
                    gfx.DrawString("      " + ordIt.GetItemName(item), printFont, myBrush, leftMargin, YPosition(), new StringFormat());

                    count++;
                }
            }

            leftMargin = 0;
            DrawEspacio();
            line = DottedLine();

            gfx.DrawString(line, printFont, myBrush, leftMargin, YPosition(), new StringFormat());

            count++;
            DrawEspacio();
        }

        private void DrawTotales()
        {
            OrderTotal ordTot = new OrderTotal('?');

            foreach (string total in totales)
            {
                line = ordTot.GetTotalCantidad(total);
                line = AlignRightText(line.Length) + line;

                gfx.DrawString(line, printFont, myBrush, leftMargin, YPosition(), new StringFormat());
                leftMargin = 0;

                line = "" + ordTot.GetTotalName(total);
                gfx.DrawString(line, printFont, myBrush, leftMargin, YPosition(), new StringFormat());
                count++;
            }
            leftMargin = 0;
            DrawEspacio();
            DrawEspacio();
        }

        private void DrawFooter()
        {
            foreach (string footer in footerLines)
            {
                if (footer.Length > maxChar)
                {
                    int currentChar = 0;
                    int footerLenght = footer.Length;

                    while (footerLenght > maxChar)
                    {
                        line = footer;
                        gfx.DrawString(line.Substring(currentChar, maxChar), printFont, myBrush, leftMargin, YPosition(), new StringFormat());

                        count++;
                        currentChar += maxChar;
                        footerLenght -= maxChar;
                    }
                    line = footer;
                    gfx.DrawString(line.Substring(currentChar, line.Length - currentChar), printFont, myBrush, leftMargin, YPosition(), new StringFormat());
                    count++;
                }
                else
                {
                    line = footer;
                    gfx.DrawString(line, printFont, myBrush, leftMargin, YPosition(), new StringFormat());

                    count++;
                }
            }
            leftMargin = 0;
            DrawEspacio();
        }

        private void DrawEspacio()
        {
            line = "";

            gfx.DrawString(line, printFont, myBrush, leftMargin, YPosition(), new StringFormat());

            count++;
        }

        public void EmitirTicket(string empresa, string direccionEmpresa, string DistritoEmpresa, string nroComprobante, List<printProductos> lista, string total, string mp, string cliente, string nombreImpresora)
        {
            Ticket2 ticket = new Ticket2();
            ticket.FontSize = 8;
            ticket.FontName = "Rounded Elegance";

            ticket.AddHeaderLine(empresa); //Nombre de la empresa
            ticket.AddHeaderLine(direccionEmpresa); //Direccion empresa
            ticket.AddHeaderLine(DistritoEmpresa); //Distrito empresa
            ticket.AddHeaderLine(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());

            ticket.AddSubHeaderLine("Ticket Nº " + nroComprobante); //Nro del comprobante

            foreach (printProductos itemProdc in lista)
            {
                ticket.AddItem(itemProdc.cantidad, itemProdc.producto, itemProdc.precio);
            }

            ticket.AddTotal("TOTAL", total); // Total
            ticket.AddTotal("", "");
            ticket.AddTotal("MP", mp); //Recibido
            //ticket.AddTotal("CAMBIO", "0"); //Cambio
            ticket.AddTotal("", "");
            if (cliente != "")
            {
                ticket.AddFooterLine("Disfrute su estadia " + cliente.Split(' ')[0].ToUpper() + ". Gracias!!"); //nombre del cliente    
            }
            else
            {
                ticket.AddFooterLine("Disfrute su estadia amig@. Gracias!!"); //nombre del cliente    
            }

            ticket.PrintTicket(nombreImpresora); //Nombre de la impresora de tickets
        }


        public void EmitirTicketPedidos(string empresa, string direccionEmpresa, string DistritoEmpresa, string nroComprobante, List<printProductosPedidos> lista, string total, string mp, string cliente, string nombreImpresora)
        {

            if (lista.Count > 0)
            {
                Ticket2 ticket = new Ticket2();
                ticket.FontSize = 8;
                ticket.FontName = "Rounded Elegance";
                ticket.AddHeaderLine(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
                ticket.AddSubHeaderLine("Socio: " + cliente);
                ticket.AddSubHeaderLine("Nº " + nroComprobante); //Nro del comprobante
                ticket.AddSubHeaderLine("Pedido por: " + lista[0].UsuarioCreacion);

                foreach (printProductosPedidos itemProdc in lista)
                {
                    ticket.AddItem2(itemProdc.cantidad, itemProdc.producto, itemProdc.precio, itemProdc.UsuarioCreacion);
                }

                ticket.AddTotal("", "");
                ticket.PrintTicket2(nombreImpresora); //Nombre de la impresora de tickets

            }

        }


        public void EmitirBoleta(string empresa, string direccionEmpresa, string DistritoEmpresa, string nroComprobante, List<printProductos> lista, string total, string mp, string cliente, string nombreImpresora)
        {
            Ticket2 ticket = new Ticket2();
            ticket.FontSize = 8;
            ticket.FontName = "Rounded Elegance";

            ticket.AddHeaderLine(empresa); //nombre de la empresa
            ticket.AddHeaderLine(direccionEmpresa); //Direccion
            ticket.AddHeaderLine(DistritoEmpresa); //Distrito
            ticket.AddHeaderLine(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());

            ticket.AddSubHeaderLine("Boleta de Venta Nº " + nroComprobante); //Nro del comprobante

            foreach (printProductos itemProdc in lista)
            {
                ticket.AddItem(itemProdc.cantidad, itemProdc.producto, itemProdc.precio);
            }

            ticket.AddTotal("TOTAL", total); // Total
            ticket.AddTotal("", "");
            ticket.AddTotal("MP", mp); //Recibido
            // ticket.AddTotal("CAMBIO", "0"); //Cambio
            ticket.AddTotal("", "");
            if (cliente != "")
            {
                ticket.AddFooterLine("Disfrute su estadia " + cliente.Split(' ')[0].ToUpper() + ". Gracias!!"); //nombre del cliente    
            }
            else
            {
                ticket.AddFooterLine("Disfrute su estadia amig@. gracias!!"); //nombre del cliente    
            }

            ticket.PrintTicket(nombreImpresora); //Nombre de la impresora de tickets
        }

        public void EmitirFactura(string empresa, string direccionEmpresa, string distritoEmpresa, string direccionCliente, string ruc, string nroComprobante, List<printProductos> lista, string subTotal, string igv, string total, string mp, string cliente, string nombreImpresora)
        {
            Ticket2 ticket = new Ticket2();
            ticket.FontSize = 9;
            ticket.FontName = "Rounded Elegance";

            ticket.AddHeaderLine(empresa); //nombre de la empresa
            ticket.AddHeaderLine(direccionEmpresa); //direccion
            ticket.AddHeaderLine(distritoEmpresa); //distrito
            ticket.AddHeaderLine(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());

            ticket.AddHeaderLine("Cliente: " + cliente); //cliente
            ticket.AddHeaderLine("RUC: " + ruc); //ruc
            ticket.AddHeaderLine("Dirección: " + direccionCliente); //direccion

            ticket.AddSubHeaderLine("Factura Nº " + nroComprobante); //Nro del comprobante

            foreach (printProductos itemProdc in lista)
            {
                ticket.AddItem(itemProdc.cantidad, itemProdc.producto, itemProdc.precio);
            }

            ticket.AddTotal("SUB TOTAL", subTotal); // sub total
            ticket.AddTotal("IGV", igv); // igv
            ticket.AddTotal("TOTAL", total); // Total
            ticket.AddTotal("", "");
            ticket.AddTotal("MP", mp); //Recibido
            //ticket.AddTotal("CAMBIO", "0"); //Cambio
            ticket.AddTotal("", "");
            if (cliente != "")
            {
                ticket.AddFooterLine("Disfrute su estadia " + cliente.Split(' ')[0].ToUpper() + ". gracias!!"); //nombre del cliente    
            }
            else
            {
                ticket.AddFooterLine("Disfrute su estadia amig@. gracias!!"); //nombre del cliente    
            }

            ticket.PrintTicket(nombreImpresora); //Nombre de la impresora de tickets
        }

    }

    public class OrderItem
    {
        char[] delimitador = new char[] { '?' };

        public OrderItem(char delimit)
        {
            delimitador = new char[] { delimit };
        }

        public string GetItemCantidad(string orderItem)
        {
            string[] delimitado = orderItem.Split(delimitador);
            return delimitado[0];
        }

        public string GetItemName(string orderItem)
        {
            string[] delimitado = orderItem.Split(delimitador);
            return delimitado[1];
        }

        public string GetItemPrice(string orderItem)
        {
            string[] delimitado = orderItem.Split(delimitador);
            return delimitado[2];
        }

        public string GetItemUser(string orderItem)
        {
            string[] delimitado = orderItem.Split(delimitador);
            return delimitado[3];
        }

        public string GenerateItem(string cantidad, string itemName, string price)
        {
            return cantidad + delimitador[0] + itemName + delimitador[0] + price;
        }

        public string GenerateItem2(string cantidad, string itemName, string price, string UsuarioCreacion)
        {
            return cantidad + delimitador[0] + itemName + delimitador[0] + price + delimitador[0] + UsuarioCreacion;
        }
    }

    public class OrderTotal
    {
        char[] delimitador = new char[] { '?' };

        public OrderTotal(char delimit)
        {
            delimitador = new char[] { delimit };
        }

        public string GetTotalName(string totalItem)
        {
            string[] delimitado = totalItem.Split(delimitador);
            return delimitado[0];
        }

        public string GetTotalCantidad(string totalItem)
        {
            string[] delimitado = totalItem.Split(delimitador);
            return delimitado[1];
        }

        public string GenerateTotal(string totalName, string price)
        {
            return totalName + delimitador[0] + price;
        }
    }

    public class printProductos
    {
        public string cantidad { get; set; }
        public string producto { get; set; }
        public string precio { get; set; }

    }

    public class printProductosPedidos
    {
        public string fecha { get; set; }
        public string socio { get; set; }
        public string producto { get; set; }
        public string cantidad { get; set; }
        public string creadoPor { get; set; }
        public string precio { get; set; }
        public string UsuarioCreacion { get; set; }

    }

}
