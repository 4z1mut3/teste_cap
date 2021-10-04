using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using teste_cap_07.Models;
using teste_cap_07.Data;
using System.Web.Http.ModelBinding;

namespace teste_cap_07.Controllers
{
    [ApiController]
    [Route("v1/")]
    public class MovimentoProdutoController : ControllerBase
    {

        public string dataEntrega_;
        public string nomeProduto_;
        public int quantidadeProduto_;
        public decimal valorUnitario_;


        [HttpGet]
        [Route("/")]
        public async Task<ActionResult<List<MovimentoProduto>>> Get([FromServices] DataContext context)
        {
            lerExcel();
            var movimentoProdutos = await context.movimentoProdutos.ToListAsync();
            return movimentoProdutos;
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<MovimentoProduto>> Post([FromServices] DataContext context, [FromBody] MovimentoProduto model)
        {
            if (ModelState.IsValid)
            {
                context.movimentoProdutos.Add(model);
                await context.SaveChangesAsync();
                return model;
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        public void lerExcel()
        {
            var xls = new XLWorkbook(@"C:\Users\Paulo André\Documents\movimentacao.xlsx");
            var planilha = xls.Worksheets.First(w => w.Name == "movimentacao");
            var totalLinhas = planilha.Rows().Count();

            // primeira linha é o cabecalho
            for (int l = 2; l <= totalLinhas; l++)
            {
                var dataEntrega = planilha.Cell($"A{l}").Value.ToString();
                var nomeProduto = planilha.Cell($"B{l}").Value.ToString();
                var quantidade = Convert.ToInt32(planilha.Cell($"C{l}").Value.ToString());
                var ValorUnitario = Convert.ToDecimal(planilha.Cell($"D{l}").Value.ToString());

                dataEntrega_ = Convert.ToString(dataEntrega);
                nomeProduto_ = Convert.ToString(nomeProduto);
                quantidadeProduto_ = Convert.ToInt32(quantidade);
                valorUnitario_ = Convert.ToDecimal(ValorUnitario);
                teste_cap_07.Models.MovimentoProduto novoMovimentoProduto = new MovimentoProduto(Convert.ToString(dataEntrega_), Convert.ToString(nomeProduto_), Convert.ToInt32(quantidadeProduto_), Convert.ToDecimal(valorUnitario_));


                string dadosPOST = System.Text.Json.JsonSerializer.Serialize(novoMovimentoProduto);

                var dados = Encoding.UTF8.GetBytes(dadosPOST);

                var requisicaoWeb = WebRequest.CreateHttp("https://localhost:44382/v1");

                requisicaoWeb.Method = "POST";
                requisicaoWeb.ContentType = "application/x-www-form-urlencoded";
                requisicaoWeb.ContentLength = dados.Length;
                requisicaoWeb.UserAgent = "RequisicaoWebDemo";

                //precisamos escrever os dados post para o stream
                using (var stream = requisicaoWeb.GetRequestStream())
                {
                    stream.Write(dados, 0, dados.Length);
                    stream.Close();
                }
            }
        }
    }
}