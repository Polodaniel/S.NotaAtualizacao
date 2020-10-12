using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using S.NotaAtualizacao.Data;
using S.NotaAtualizacao.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S.NotaAtualizacao.Controllers
{
    public class GerarPDF : Controller
    {
        private readonly ApplicationDbContext _context;

        public GerarPDF(ApplicationDbContext context)
        {
            _context = context;
        }

        public MemoryStream GerarArquivoPDF(NotaAtualizacaoItem notaAtualizacaoItem)
        {
            MemoryStream ms = new MemoryStream();

            Document document = new Document(PageSize.A4);

            document.SetMargins(0, 0, 200, 0);
            document.AddCreationDate();

            PdfWriter writer = PdfWriter.GetInstance(document, ms);
            writer.PageEvent = new PageEventHelper();

            var HTML = CriaArquivoHTML(notaAtualizacaoItem);

            System.Text.EncodingProvider ppp;
            ppp = System.Text.CodePagesEncodingProvider.Instance;
            Encoding.RegisterProvider(ppp);

            document.Open();

            XMLWorkerHelper.GetInstance().ParseXHtml(writer, document, new StringReader(HTML));

            document.Close();

            byte[] file = ms.ToArray();

            MemoryStream output = new MemoryStream();

            output.Write(file, 0, file.Length);
            output.Position = 0;

            return output;
        }

        public string CriaArquivoHTML(Models.NotaAtualizacaoItem x)
        {
            var HTML = new StringBuilder();

            HTML = HTML.Append("<html>                                                                    ");
            HTML = HTML.Append("    <head>                                                                ");
            HTML = HTML.Append("    </head>                                                               ");
            HTML = HTML.Append("    <body style=\"margin: 0; width: auto; font-family: arial, sans-serif; color: #242424;font-size: 10.5px;line-height: 1.8;\">                                                                ");
            HTML = HTML.Append("    <div style=\"padding: 50px;\">");
            HTML = HTML.Append("        <div>");
            HTML = HTML.Append("            <p style=\"font-weight: bold;\"><span>Ocorrência: </span> #NumeroOcorrencia#</p>");
            HTML = HTML.Append("            <p><span style=\"font-weight: bold;\" >Chamado: </span> #NumeroChamado#</p>");
            HTML = HTML.Append("            <p><span style=\"font-weight: bold;\">Cliente: </span> #NomeCliente#</p>");
            HTML = HTML.Append("        </div>");
            HTML = HTML.Append("        <div style=\"font-weight: bold; font-size: 14px; color: midnightblue;\">");
            HTML = HTML.Append("            V​ISÃO​ G​ERAL");
            HTML = HTML.Append("        </div>");
            HTML = HTML.Append("        <div>");
            HTML = HTML.Append("            #DescriçãoVisãoGeral#");
            HTML = HTML.Append("        </div>");
            HTML = HTML.Append("        <div style=\"font-weight: bold; font-size: 14px; color: midnightblue;\">");
            HTML = HTML.Append("            DETALHES");
            HTML = HTML.Append("        </div>");
            HTML = HTML.Append("        <div>");
            HTML = HTML.Append("            #DescriçãoDetalhes#");
            HTML = HTML.Append("        </div>");
            HTML = HTML.Append("        <div style=\"font-weight: bold; font-size: 14px; color: midnightblue;\">");
            HTML = HTML.Append("            A​NÁLISE ​​E  A​JUSTE");
            HTML = HTML.Append("        </div>");
            HTML = HTML.Append("        <div>");
            HTML = HTML.Append("            #DescriçãoAnaliseAjuste#");
            HTML = HTML.Append("        </div>");
            HTML = HTML.Append("        <div style=\"font-weight: bold; font-size: 14px; color: midnightblue;\">");
            HTML = HTML.Append("            IMAGENS");
            HTML = HTML.Append("        </div>");
            HTML = HTML.Append("        <div>");
            HTML = HTML.Append("            #PrintsAnexado#");
            HTML = HTML.Append("        </div>");
            HTML = HTML.Append("    </div>");
            HTML = HTML.Append("    </body>");
            HTML = HTML.Append("</html>");

            var cliente = _context.Clientes.Where(y => y.Codigo == x.CodigoCliente).FirstOrDefault();

            var HTML_Finalizado = HTML.ToString()
                                      .Replace("#NumeroOcorrencia#",x.Ocorrencia.ToString())
                                      .Replace("#NumeroChamado#",x.Chamado.ToString())
                                      .Replace("#NomeCliente#", !Equals(cliente,null) ? cliente.Sigla : string.Empty)
                                      .Replace("#DescriçãoVisãoGeral#", !string.IsNullOrEmpty(x.VisaoGeral) ? x.VisaoGeral.ToString() : string.Empty)
                                      .Replace("#DescriçãoDetalhes#", !string.IsNullOrEmpty(x.Detalhes) ? x.Detalhes.ToString() : string.Empty)
                                      .Replace("#DescriçãoAnaliseAjuste#", !string.IsNullOrEmpty(x.AnaliseAjuste) ? x.AnaliseAjuste.ToString() : string.Empty)
                                      .Replace("#PrintsAnexado#", "")
                                      ;

            HTML_Finalizado = HTML_Finalizado.Replace("<br>", "");

            return HTML_Finalizado;
        }
    }

    class PageEventHelper : PdfPageEventHelper
    {
        PdfContentByte cb;
        PdfTemplate template;

        public PageEventHelper()
        {
        }


        public override void OnOpenDocument(PdfWriter writer, Document document)
        {
            cb = writer.DirectContent;
            template = cb.CreateTemplate(50, 50);
        }

        public override void OnStartPage(PdfWriter writer, Document document)
        {
            base.OnStartPage(writer, document);
        }

        public override void OnEndPage(PdfWriter writer, Document document)
        {
            base.OnEndPage(writer, document);
        }

        public override void OnCloseDocument(PdfWriter writer, Document document)
        {
            base.OnCloseDocument(writer, document);
        }

    }
}
