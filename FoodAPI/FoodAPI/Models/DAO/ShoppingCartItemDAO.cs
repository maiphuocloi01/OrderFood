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
    public class ShoppingCartItemDAO
    {
        private static ShoppingCartItemDAO instance;

        public static ShoppingCartItemDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ShoppingCartItemDAO();
                }
                return instance;
            }
            private set => instance = value;
        }

        FoodAppDbEntities db = new FoodAppDbEntities();

        //public async Task<List<ShoppingCartItemDTO>> GetAllShoppingCartItems(int userId)
        //{

        //    //var resultList = (await db.ShoppingCartItems
        //    //    .ToListAsync())
        //    //    .Select(b => new ShoppingCartItemDTO(b))
        //    //    .ToList();
        //    //resultList = resultList.FindAll(b => b.CustomerId == userId);
        //    //return resultList;

        //    var shoppingCartItems = from s in db.ShoppingCartItems.Where(s => s.CustomerId == userId)
        //                            join p in db.Products on s.ProductId equals p.Id

        //                            select new
        //                            {
        //                                Id = s.Id,
        //                                Price = s.Price,
        //                                TotalAmount = s.TotalAmount,
        //                                Qty = s.Qty,
        //                                ProductName = p.Name,

        //                            };
        //    return shoppingCartItems;
        //}

        public async Task<double> SubTotal(int ID)
        {
            //var subTotal = (from cart in db.ShoppingCartItems
            //                where cart.CustomerId == userId
            //                select cart.TotalAmount).Sum();

            var sum1 = (await db.ShoppingCartItems
                        .ToListAsync())
                        .Where(p => p.CustomerId == ID)
                        .Sum(p => p.TotalAmount);

            return sum1;
        }

        public async Task<int> TotalItems(int ID)
        {
           
            var sum2 = (await db.ShoppingCartItems
                        .ToListAsync())
                        .Where(p => p.CustomerId == ID)
                        .Sum(p => p.Qty);
            return sum2;
        }

        public async Task<int> AddShoppingCartItems(ShoppingCartItemDTO shoppingCartItemDTO)
        {
            var shoppingCart = db.ShoppingCartItems
                .FirstOrDefault(s => s.ProductId == shoppingCartItemDTO.ProductId &&
                s.CustomerId == shoppingCartItemDTO.CustomerId);

            try
            {

                if (shoppingCart != null)
                {
                    shoppingCart.Qty += shoppingCartItemDTO.Qty;
                    shoppingCart.TotalAmount = shoppingCart.Price * shoppingCart.Qty;
                    return shoppingCart.Id;
                }
                else
                {
                    var sCart = new ShoppingCartItem()
                    {
                        CustomerId = shoppingCartItemDTO.CustomerId,
                        ProductId = shoppingCartItemDTO.ProductId,
                        Price = shoppingCartItemDTO.Price,
                        Qty = shoppingCartItemDTO.Qty,
                        TotalAmount = shoppingCartItemDTO.TotalAmount
                    };
                    db.ShoppingCartItems.Add(sCart);
                    await db.SaveChangesAsync();
                    return sCart.Id;
                }
                
            }
            catch (Exception e)
            {
                return -1;
                throw e;
            }

        }

        public async Task<bool> UpdateQuantity(int cartId, int quantity)
        {
            var result = db.ShoppingCartItems.SingleOrDefault(c => c.Id == cartId);
            try
            {
                
                result.Qty = quantity;
                result.TotalAmount = quantity * result.Price;

                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                return false;
                throw e;
            }
        }


        public async Task<bool> DeleteCartItem(int userId)
        {
            var shoppingCart = db.ShoppingCartItems.Where(s => s.CustomerId == userId);

            try
            {
                db.ShoppingCartItems.RemoveRange(shoppingCart);
                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                return false;
                throw e;
            }
        }

        public async Task<bool> DeleteCartItemByID(int ID)
        {
            var shoppingCart = db.ShoppingCartItems.Where(s => s.Id == ID);

            try
            {
                db.ShoppingCartItems.RemoveRange(shoppingCart);
                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                return false;
                throw e;
            }
        }
    }
}