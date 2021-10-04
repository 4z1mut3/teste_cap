using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace teste_cap_07.Models
{
    public class MovimentoProduto
    {        
        public int id { get; set; }
        
        public string dataEntrega { get; set; }
        
        public string nomeProduto { get; set; }
        
        public int quantidadeProduto { get; set; }
        
        public decimal valorUnitario { get; set; }

        public MovimentoProduto(string dataEntrega, string nomeProduto, int quantidadeProduto, decimal valorUnitario)
        {
            this.dataEntrega = dataEntrega;
            this.nomeProduto = nomeProduto;
            this.quantidadeProduto = quantidadeProduto;
            this.valorUnitario = valorUnitario;
        }
    }
}