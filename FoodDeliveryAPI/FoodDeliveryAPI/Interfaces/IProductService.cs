using FoodDeliveryAPI.DTO;

namespace FoodDeliveryAPI.Interfaces
{
    public interface IProductService
    {
        List<ProductDTO> GetAllProducts();

        ProductDTO AddNewProduct(ProductDTO productDTO);

        void DeleteProduct(long id);

        ProductDTO GetProductById(long id);

        ProductDTO UpdateProduct(ProductDTO productDTO, long id);

    }
}
