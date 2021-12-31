using FoodAPI.Models.DTO;
using FoodAPI.Models.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace FoodAPI.Models.DAO
{
    public class ProductDAO
    {
        private static ProductDAO instance;

        public static ProductDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ProductDAO();
                }
                return instance;
            }
            private set => instance = value;
        }
        FoodAppDbEntities db = new FoodAppDbEntities();

        public async Task<List<ProductDTO>> GetAllProduct()
        {
            var ProductList = (await db.Products
                        .ToListAsync())
                        .Select(product => new ProductDTO(product))
                        .ToList();
            return ProductList;
        }

        public async Task<ProductDTO> GetProductByID(int ID)
        {
            var MyProduct = await db.Products.SingleOrDefaultAsync(product => product.Id == ID);

            await db.SaveChangesAsync();

            return MyProduct == null ? null : new ProductDTO(MyProduct);
        }

        public async Task<List<ProductDTO>> GetProductByCategoryID(int categoryID)
        {
            return (await db.Products
                                .Where(product => product.CategoryId == categoryID)
                                .ToListAsync())
                                .Select(product => new ProductDTO(product))
                                .ToList();
        }

        public async Task<List<ProductDTO>> GetPopularProduct()
        {
            var ProductList = (await db.Products
                        .ToListAsync())
                        .Select(product => new ProductDTO(product))
                        .ToList();
            ProductList = ProductList.FindAll(b => b.IsPopularProduct == true);
            return ProductList;
        }

        public async Task<int> AddProduct(ProductDTO productDTO)
        {
            var product = new Product()
            {
                Name = productDTO.Name,
                Price = productDTO.Price,
                ImageUrl = productDTO.ImageUrl,
                Detail = productDTO.Detail,
                IsPopularProduct = productDTO.IsPopularProduct,
                CategoryId = productDTO.CategoryId,
            };

            try
            {
                db.Products.Add(product);
                await db.SaveChangesAsync();
                return product.Id;
            }
            catch (Exception e)
            {
                return -1;
                throw e;
            }
        }

        public async Task<bool> UpdateProduct(ProductDTO productDTO)
        {
            var result = db.Products.SingleOrDefault(p => p.Id == productDTO.Id);
            try
            {
                result.Name = productDTO.Name;
                result.Price = productDTO.Price;
                result.ImageUrl = productDTO.ImageUrl;
                result.Detail = productDTO.Detail;
                result.IsPopularProduct = productDTO.IsPopularProduct;
                result.CategoryId = productDTO.CategoryId;

                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                return false;
                throw e;
            }
        }

        public async Task<bool> DeleteProduct(int ID)
        {
            var result = db.Products.SingleOrDefault(b => b.Id == ID);

            try
            {
                db.Products.Remove(result);
                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                return false;
                throw e;
            }
        }

        //public async Task<bool> RestoreAllProduct()
        //{
        //    var BrandList = (await db.Products
        //                .ToListAsync())
        //                .Select(product => new ProductDTO(product))
        //                .ToList();

        //    try
        //    {
        //        var DeletedList = db.Products.Where(b => b.IsDeleted == true).ToList();
        //        DeletedList.ForEach(b => b.IsDeleted = false);
        //        db.SaveChanges();

        //        return true;
        //    }
        //    catch (Exception e)
        //    {
        //        return false;
        //        throw e;
        //    }
        //}
    }
}