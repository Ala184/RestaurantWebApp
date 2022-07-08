using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodDeliveryAPI.DTO;
using FoodDeliveryAPI.Infrastructure;
using FoodDeliveryAPI.Interfaces;
using FoodDeliveryAPI.Models;

namespace FoodDeliveryAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly FoodDeliveryDbContext _dbContext;

        public ProductService(IMapper mapper, FoodDeliveryDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }
        public List<ProductDTO> GetAllProducts()
        {
            return _mapper.Map<List<ProductDTO>>(_dbContext.Products.ToList());
        }

        public ProductDTO GetProductById(long id)
        {
            return _mapper.Map<ProductDTO>(_dbContext.Products.Find(id));
        }

        public ProductDTO AddNewProduct(ProductDTO productDTO)
        {
            Product product = _mapper.Map<Product>(productDTO);
            _dbContext.Products.Add(product);
            _dbContext.SaveChanges();

            return _mapper.Map<ProductDTO>(productDTO);
        }

        public void DeleteProduct(long id)
        {
            Product product = _dbContext.Products.Find(id);
            if (product != null)
            {
                _dbContext.Products.Remove(product);
                _dbContext.SaveChanges();
            }
        }       

        public ProductDTO UpdateProduct(ProductDTO productDTO, long id)
        {
            Product product = _dbContext.Products.Find(id);
            if (product != null)
            {
                product.Price = productDTO.Price;
                product.Ingredients = productDTO.Ingredients;
                product.Name = productDTO.Name;
                product.PhotoUrl = productDTO.PhotoUrl;
                _dbContext.SaveChanges();
            }
            return _mapper.Map<ProductDTO>(product);
        }
    }
}
