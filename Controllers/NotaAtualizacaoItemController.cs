using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using S.NotaAtualizacao.Data;
using S.NotaAtualizacao.Models;

namespace S.NotaAtualizacao.Controllers
{
    public class NotaAtualizacaoItemController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NotaAtualizacaoItemController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: NotaAtualizacaoItem
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.NotaAtualizacaoItem.Include(n => n.Clientes);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: NotaAtualizacaoItem/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notaAtualizacaoItem = await _context.NotaAtualizacaoItem
                .Include(n => n.Clientes)
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (notaAtualizacaoItem == null)
            {
                return NotFound();
            }

            return View(notaAtualizacaoItem);
        }

        // GET: NotaAtualizacaoItem/Create
        public IActionResult Create()
        {
            ViewData["CodigoCliente"] = new SelectList(_context.Clientes, "Codigo", "Sigla");
            return View();
        }

        // POST: NotaAtualizacaoItem/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Codigo,Ocorrencia,Chamado,CodigoCliente,VisaoGeral,Detalhes,AnaliseAjuste,Imagem1,ContentType1,Descricao1,Imagem2,ContentType2,Descricao2,Imagem3,ContentType3,Descricao3,Imagem4,ContentType4,Descricao4")] NotaAtualizacaoItem notaAtualizacaoItem)
        {
            ViewData["CodigoCliente"] = new SelectList(_context.Clientes, "Codigo", "Sigla", notaAtualizacaoItem.CodigoCliente);
            
            
            
            
            
            
            
            
            
            return View(notaAtualizacaoItem);
        }

        [HttpPost]
        public FileStreamResult ImpressaoGeral([Bind("Codigo,Ocorrencia,Chamado,CodigoCliente,VisaoGeral,Detalhes,AnaliseAjuste,img1,Imagem1,ContentType1,Descricao1,img2,Imagem2,ContentType2,Descricao2,img3,Imagem3,ContentType3,Descricao3,img4,Imagem4,ContentType4,Descricao4")] NotaAtualizacaoItem notaAtualizacaoItem)
        {
            notaAtualizacaoItem = MontaObjetoComImagens(notaAtualizacaoItem);

            var PDF = new GerarPDF(_context).GerarArquivoPDF(notaAtualizacaoItem);

            return File(PDF, "application/pdf");
        }

        private NotaAtualizacaoItem MontaObjetoComImagens(NotaAtualizacaoItem notaAtualizacaoItem)
        {
            notaAtualizacaoItem.Imagem1 = MontaArrayImagem(notaAtualizacaoItem.img1);
            notaAtualizacaoItem.ContentType1 = MontaContentTypeImagem(notaAtualizacaoItem.img1);

            notaAtualizacaoItem.Imagem2 = MontaArrayImagem(notaAtualizacaoItem.img2);
            notaAtualizacaoItem.ContentType2 = MontaContentTypeImagem(notaAtualizacaoItem.img2);

            notaAtualizacaoItem.Imagem3 = MontaArrayImagem(notaAtualizacaoItem.img3);
            notaAtualizacaoItem.ContentType3 = MontaContentTypeImagem(notaAtualizacaoItem.img3);

            notaAtualizacaoItem.Imagem4 = MontaArrayImagem(notaAtualizacaoItem.img4);
            notaAtualizacaoItem.ContentType4 = MontaContentTypeImagem(notaAtualizacaoItem.img4);

            return notaAtualizacaoItem;
        }

        private string MontaContentTypeImagem(IList<IFormFile> img)
        {
            IFormFile imagemEnviada = img.FirstOrDefault();

            if (imagemEnviada != null || imagemEnviada.ContentType.ToLower().StartsWith("image/"))
            {
                MemoryStream ms = new MemoryStream();
                imagemEnviada.OpenReadStream().CopyTo(ms);

                return imagemEnviada.ContentType;

            }
            else
                return null;
        }

        private byte[] MontaArrayImagem(IList<IFormFile> img)
        {
            IFormFile imagemEnviada = img.FirstOrDefault();

            if (imagemEnviada != null || imagemEnviada.ContentType.ToLower().StartsWith("image/"))
            {
                MemoryStream ms = new MemoryStream();
                imagemEnviada.OpenReadStream().CopyTo(ms);

                return ms.ToArray();
            }
            else
                return null;
        }

        // GET: NotaAtualizacaoItem/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notaAtualizacaoItem = await _context.NotaAtualizacaoItem.FindAsync(id);
            if (notaAtualizacaoItem == null)
            {
                return NotFound();
            }
            ViewData["CodigoCliente"] = new SelectList(_context.Clientes, "Codigo", "Codigo", notaAtualizacaoItem.CodigoCliente);
            return View(notaAtualizacaoItem);
        }

        // POST: NotaAtualizacaoItem/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Codigo,Ocorrencia,Chamado,CodigoCliente,VisaoGeral,Detalhes,AnaliseAjuste,Imagem1,ContentType1,Descricao1,Imagem2,ContentType2,Descricao2,Imagem3,ContentType3,Descricao3,Imagem4,ContentType4,Descricao4")] NotaAtualizacaoItem notaAtualizacaoItem)
        {
            if (id != notaAtualizacaoItem.Codigo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(notaAtualizacaoItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NotaAtualizacaoItemExists(notaAtualizacaoItem.Codigo))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CodigoCliente"] = new SelectList(_context.Clientes, "Codigo", "Codigo", notaAtualizacaoItem.CodigoCliente);
            return View(notaAtualizacaoItem);
        }

        // GET: NotaAtualizacaoItem/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notaAtualizacaoItem = await _context.NotaAtualizacaoItem
                .Include(n => n.Clientes)
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (notaAtualizacaoItem == null)
            {
                return NotFound();
            }

            return View(notaAtualizacaoItem);
        }

        // POST: NotaAtualizacaoItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var notaAtualizacaoItem = await _context.NotaAtualizacaoItem.FindAsync(id);
            _context.NotaAtualizacaoItem.Remove(notaAtualizacaoItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NotaAtualizacaoItemExists(int id)
        {
            return _context.NotaAtualizacaoItem.Any(e => e.Codigo == id);
        }
    }
}
