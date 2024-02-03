using System.ComponentModel.DataAnnotations;

namespace ApiGateway.Models.API.Entity
{
    public class OrderProductsDTO
    {
        [Required]
        public long IdProduct { get; set; }
        public int Amount { get; set; }

        public OrderProductsDTO(long idProduct, int amount)
        {
            this.IdProduct = idProduct;
            this.Amount = amount;
        }

        public static OrderProductsDTO OrderProductsDTOFactory(long idProduct, int amount)
        {
            return new OrderProductsDTO(idProduct, amount);
        }
    }
}
