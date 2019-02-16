﻿using com.database;
using com.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace com.services
{
    public class ProductService
    {
        //public  static ProductService Instance
        //{
        //    get {
        //        if (Instance == null) instance = new ProductService();
        //        return instance;
        //    }
        //}

        //private static ProductService instance { get; set; }

        //private ProductService() {}

        CContext context = new CContext();

        public List<Product> GetProducts()
        { return context.Products.Include(x => x.Category).ToList(); }

        public List<Product> GetProducts (List<int> pId)
        {
            return context.Products.Where(x => pId.Contains(x.ID)).ToList();
        }

        public List<Product> GetFeaturedProducts()
        {
            return context.Products.Where(x => x.isFeatured == true && x.ImageURL != null).Take(3).ToList();
        }

        public List<Product> NewProduct()
        {
            return context.Products.OrderByDescending(x=> x.ID ).Where(x=> x.ImageURL != null).Take(4).ToList();
        }

        public List<Product> NewProductx()
        {
            return context.Products.Where(x => x.ImageURL != null).Take(8).ToList();
        }

        public Product GetProduct(int id)
        {
            return context.Products.Include(x=> x.Category).SingleOrDefault(x => x.ID == id);
        }

        public void SaveProducts(Product product)
        {
            using (context)
            {
                context.Entry(product).State = EntityState.Unchanged;
                context.Products.Add(product);
                context.SaveChanges();
            }
        }

        public void UpdateProducts(Product product)
        {
            using (context)
            {
                context.Entry(product).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void DeleteProducts(int ID)
        {
           // var pID = context.Products.Single(p => p.ID == ID);
            var product = context.Products.Where(p => p.ID.Equals(ID)).SingleOrDefault();
            context.Products.Remove(product);
            context.SaveChanges();
        }
    }
}
