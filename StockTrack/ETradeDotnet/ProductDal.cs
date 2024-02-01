using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeDotnet
{
    public class ProductDal
    {
        public List<Products> GetAll()
        {
            using (ETradeContext context = new ETradeContext())
            {
                return context.Products.ToList();
            }
        }
        public Products GetById(int id)
        {
            using (ETradeContext context = new ETradeContext())
            {
                var result = context.Products.FirstOrDefault(p=>p.Id==id);
                return result;
            }
        }
        public List<Products> GetByName(string key)
        {
            using (ETradeContext context = new ETradeContext())
            {
                return context.Products.Where(p=>p.Name.Contains(key)).ToList();
            }
        }
        public List<Products> GetByUnitPrice(decimal price)
        {
            using (ETradeContext context = new ETradeContext())
            {
                return context.Products.Where(p=>p.UnitPrice<=price).ToList();
            }
        }
        public List<Products> GetByUnitPrice(decimal min, decimal max)
        {
            using (ETradeContext context = new ETradeContext())
            {
                return context.Products.Where(p=>p.UnitPrice>=min && p.UnitPrice<=max).ToList();
            }
        }
        public List<Products> GetByStockAmount(int stockAmount)
        {
            using (ETradeContext context = new ETradeContext())
            {
                return context.Products.Where(p => p.StockAmount <= stockAmount).ToList();
            }
        }

        public List<Products> GetByStockAmount(int minStock, int maxStock)
        {
            using (ETradeContext context = new ETradeContext())
            {
                return context.Products.Where(p => p.StockAmount >= minStock && p.StockAmount <= maxStock).ToList();
            }
        }

        public void Add(Products products)
        {
            using (ETradeContext context = new ETradeContext())
            {
                var entity = context.Entry(products);
                entity.State = EntityState.Added; 
                context.SaveChanges();
            }

        }
        public void Update(Products products)
        {
            using (ETradeContext context = new ETradeContext())
            {
                var entity = context.Entry(products);
                entity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }
        public void Delete(Products products)
        {
            using (ETradeContext context = new ETradeContext())
            {
                var entity = context.Entry(products);
                entity.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }
    }
}
