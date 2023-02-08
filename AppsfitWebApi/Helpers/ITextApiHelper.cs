using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppsfitWebApi.Helpers
{
   
    public class ITextApiHelper
    {
        public static PdfPCell cellCustom(string txt, Font font, int alignment = 0, int Colspan = 0, bool border = false, int padding = 2)
        {
            PdfPCell cell = new PdfPCell(new Phrase(txt, font));
            if (border)
            {
                cell.Border = PdfPCell.BOTTOM_BORDER;
            }
            else
            {
                cell.Border = 0;
            }

            if (alignment == 0)
            {
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
            }
            else if (alignment == 1)
            {
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
            }
            else
            {
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            }


            cell.Colspan = Colspan;
            cell.BorderColor = new BaseColor(211, 211, 211);
            cell.Padding = padding;
            return cell;

        }

        public static Paragraph ParagraphCustom(string txt, Font font, int alignment = 0)
        {
            var graph = new Paragraph(txt, font);
            if (alignment == 0)
            {
                graph.Alignment = Element.ALIGN_LEFT;
            }
            else if (alignment == 1)
            {
                graph.Alignment = Element.ALIGN_CENTER;
            }
            else
            {
                graph.Alignment = Element.ALIGN_RIGHT;
            }

            return graph;

        }



    }


}