using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpTest.Models
{
    public class TopProduct
    {
        public TopProduct(int order, string? productId)
        {
            Order = order;
            ProductId = productId;
        }

        public int Order { get; set; }
        public string? ProductId { get; set; }
    }
}
