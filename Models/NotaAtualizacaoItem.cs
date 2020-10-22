using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace S.NotaAtualizacao.Models
{
    [Table(nameof(NotaAtualizacaoItem))]
    public class NotaAtualizacaoItem
    {
        [Key,DisplayName("Código")]
        public int Codigo { get; set; }

        [DisplayName("Ocorrencia")]
        public int Ocorrencia { get; set; }

        [DisplayName("Chamado")]
        public int Chamado { get; set; }

        [ForeignKey(nameof(Clientes))]
        public int CodigoCliente { get; set; }
        
        public Clientes Clientes { get; set; }

        [DisplayName("Visão Geral")]
        public string VisaoGeral { get; set; }

        [DisplayName("Detalhes")]
        public string Detalhes { get; set; }

        [DisplayName("Analise e Ajuste")]
        public string AnaliseAjuste { get; set; }

        #region Imagens

        #region Imagem 1
        [NotMapped]
        public IList<IFormFile> img1 { get; set; }

        public byte[] Imagem1 { get; set; }

        public string ContentType1 { get; set; }

        [DisplayName("Legenda")]
        public string Descricao1 { get; set; }

        [NotMapped]
        public string CaminhoImagem1 { get; set; }
        #endregion

        #region Imagem 2
        [NotMapped]
        public IList<IFormFile> img2 { get; set; }

        public byte[] Imagem2 { get; set; }

        public string ContentType2 { get; set; }

        [DisplayName("Legenda")]
        public string Descricao2 { get; set; }

        [NotMapped]
        public string CaminhoImagem2 { get; set; }
        #endregion

        #region Imagem 3
        [NotMapped]
        public IList<IFormFile> img3 { get; set; }

        public byte[] Imagem3 { get; set; }

        public string ContentType3 { get; set; }

        [DisplayName("Legenda")]
        public string Descricao3 { get; set; }

        [NotMapped]
        public string CaminhoImagem3 { get; set; }
        #endregion

        #region Imagem 4
        [NotMapped]
        public IList<IFormFile> img4 { get; set; }

        public byte[] Imagem4 { get; set; }

        public string ContentType4 { get; set; }

        [DisplayName("Legenda")]
        public string Descricao4 { get; set; }

        [NotMapped]
        public string CaminhoImagem4 { get; set; }
        #endregion

        #endregion

        [NotMapped]
        public int QtdeImagens 
        {
            get 
            {
                var qtde = 0;

                if (!Equals(Imagem1, null)) qtde++;
                if (!Equals(Imagem2, null)) qtde++;
                if (!Equals(Imagem3, null)) qtde++;
                if (!Equals(Imagem4, null)) qtde++;

                return qtde;
            }
        }
    }
}
