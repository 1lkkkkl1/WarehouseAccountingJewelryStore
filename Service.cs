using System;
using System.Collections.Generic;
using System.Linq;
using WarehouseAccountingJewelryStoreService.Objects;

namespace WarehouseAccountingJewelryStoreService
{
    public class Service : IService
    {
        public void AddCategory(Category category)
        {
            using (var db = new ApplicationContext())
            {
                category.Id = 0;
                db.Categories.Add(category);
                db.SaveChanges();
            }
        }

        public void AddCustomer(Customer customer)
        {
            using (var db = new ApplicationContext())
            {
                customer.Id = 0;
                db.Customers.Add(customer);
                db.SaveChanges();
            }
        }

        public void AddProduct(Product product)
        {
            using (var db = new ApplicationContext())
            {
                product.Id = 0;
                db.Products.Add(product);
                db.SaveChanges();
            }
        }

        public void AddProductToCategory(int productId, int categoryId)
        {
            using (var db = new ApplicationContext())
            {
                if (db.Products.ToList().Where(x => x.Id == productId).Count() == 0) return;
                if (db.Categories.ToList().Where(x => x.Id == categoryId).Count() == 0) return;
                db.ProductCategories.Add(new ProductCategory()
                {
                    ProductId = productId,
                    CategoryId = categoryId
                });
                db.SaveChanges();
            }
        }

        public void AddProductToSale(int productId, int saleId)
        {
            using (var db = new ApplicationContext())
            {
                if (db.Products.ToList().Where(x => x.Id == productId).Count() == 0) return;
                if (db.Sales.ToList().Where(x => x.Id == saleId).Count() == 0) return;
                db.SaleProducts.Add(new SaleProduct()
                {
                    ProductId = productId,
                    SaleId = saleId
                });
                db.SaveChanges();
            }
        }

        public void AddSale(List<Product> products, Customer customer)
        {
            using (var db = new ApplicationContext())
            {
                db.Sales.Add(new Sale()
                {
                    SaleDate = DateTime.Now,
                    CustomerId = customer.Id
                });
                db.SaveChanges();
                var saleId = db.Sales.ToList().Last().Id;
                foreach (var product in products)
                {
                    RemoveStock(product);
                    AddProductToSale(product.Id, saleId);
                }
            }
        }

        public void AddStock(List<Product> products)
        {
            using (var db = new ApplicationContext())
            {
                foreach(var product in products)
                {
                    if (db.Stocks.ToList().Where(x => x.ProductId == product.Id).Count() > 0)
                    {
                        db.Stocks.ToList().Where(x => x.ProductId == product.Id).First().Quantity++;
                        db.Stocks.ToList().Where(x => x.ProductId == product.Id).First().EntryDate = DateTime.Now;
                    }
                    else db.Stocks.Add(new Stock()
                    {
                        EntryDate = DateTime.Now,
                        ProductId = product.Id,
                        Quantity = 1
                    });
                    db.SaveChanges();
                }
            }
        }

        public IEnumerable<Category> GetCategories()
        {
            using (var db = new ApplicationContext())
                return db.Categories.ToList().Where(x => !x.IsDeleted).ToList();
        }

        public IEnumerable<Category> GetCategoriesByProduct(int productId)
        {
            using (var db = new ApplicationContext())
                return db.Categories.ToList().Where(x =>
                !x.IsDeleted &&
                db.ProductCategories.ToList().Where(a =>
                a.ProductId == productId).Select(a =>
                a.CategoryId).Contains(x.Id))
                    .ToList();
        }

        public IEnumerable<Customer> GetCustomers()
        {
            using (var db = new ApplicationContext())
                return db.Customers.ToList().Where(x => !x.IsDeleted).ToList();
        }

        public IEnumerable<Product> GetProducts()
        {
            using (var db = new ApplicationContext())
                return db.Products.ToList().Where(x => !x.IsDeleted).ToList();
        }

        public IEnumerable<Product> GetProductsBySale(int saleId)
        {
            using (var db = new ApplicationContext())
            {
                List<Product> products = new List<Product>();
                foreach (var x in db.Products.ToArray())
                {
                    if(!x.IsDeleted &&
                db.SaleProducts.ToList().Where(a =>
                a.SaleId == saleId).Select(a =>
                a.ProductId).Contains(x.Id))
                    {
                        for(int i = 0; i < db.SaleProducts.ToList().Where(a => a.SaleId == saleId).Count(); i++)
                            products.Add(x);
                    }
                }
                return products;
            }
        }

        public IEnumerable<Sale> GetSales()
        {
            using (var db = new ApplicationContext())
                return db.Sales.ToList().Where(x => !x.IsDeleted).ToList();
        }

        public IEnumerable<Stock> GetStocks()
        {
            using (var db = new ApplicationContext())
                return db.Stocks.ToList().Where(x => !x.IsDeleted).ToList();
        }
        public void AddSupplier(Supplier supplier)
        {
            using (var db = new ApplicationContext())
            {
                supplier.Id = 0;
                db.Supplirs.Add(supplier);
                db.SaveChanges();
            }
        }


        public void EditCategory(Category category)
        {
            using (var db = new ApplicationContext())
            {
                for (var i = 0; i < db.Categories.ToList().Count; i++)
                {
                    if (db.Categories.ToList()[i].Id == category.Id)
                    {
                        db.Categories.ToList()[i].Name = category.Name;
                        db.SaveChanges();
                        return;
                    }
                }
            }
        }

        public void EditCustomer(Customer customer)
        {
            using (var db = new ApplicationContext())
            {
                for (var i = 0; i < db.Customers.ToList().Count; i++)
                {
                    if (db.Customers.ToList()[i].Id == customer.Id)
                    {
                        db.Customers.ToList()[i].Name = customer.Name;
                        db.Customers.ToList()[i].Phone = customer.Phone;
                        db.Customers.ToList()[i].Email = customer.Email;
                        db.SaveChanges();
                        return;
                    }
                }
            }
        }

        public void EditProduct(Product product)
        {
            using (var db = new ApplicationContext())
            {
                for (var i = 0; i < db.Products.ToList().Count; i++)
                {
                    if (db.Products.ToList()[i].Id == product.Id)
                    {
                        db.Products.ToList()[i].Name = product.Name;
                        db.Products.ToList()[i].Price = product.Price;
                        db.Products.ToList()[i].Materials = product.Materials;
                        db.Products.ToList()[i].SupplierId = product.SupplierId;
                        db.SaveChanges();
                        return;
                    }
                }
            }
        }

        public void EditSale(Sale sale)
        {
            using (var db = new ApplicationContext())
            {
                for (var i = 0; i < db.Sales.ToList().Count; i++)
                {
                    if (db.Sales.ToList()[i].Id == sale.Id)
                    {
                        db.Sales.ToList()[i].SaleDate = sale.SaleDate;
                        db.SaveChanges();
                        return;
                    }
                }
            }
        }

        public void EditStock(Stock stock)
        {
            using (var db = new ApplicationContext())
            {
                for (var i = 0; i < db.Stocks.ToList().Count; i++)
                {
                    if (db.Stocks.ToList()[i].Id == stock.Id)
                    {
                        db.Stocks.ToList()[i].Quantity = stock.Quantity;
                        db.Stocks.ToList()[i].EntryDate = stock.EntryDate;
                        db.Stocks.ToList()[i].ProductId = stock.ProductId;
                        db.SaveChanges();
                        return;
                    }
                }
            }
        }

        public void EditSupplier(Supplier supplier)
        {
            using (var db = new ApplicationContext())
            {
                for (var i = 0; i < db.Supplirs.ToList().Count; i++)
                {
                    if (db.Supplirs.ToList()[i].Id == supplier.Id)
                    {
                        db.Supplirs.ToList()[i].Name = supplier.Name;
                        db.Supplirs.ToList()[i].Phone = supplier.Phone;
                        db.Supplirs.ToList()[i].Address = supplier.Address;
                        db.SaveChanges();
                        return;
                    }
                }
            }
        }



        public IEnumerable<Supplier> GetSuppliers()
        {
            using (var db = new ApplicationContext())
                return db.Supplirs.ToList().Where(x => !x.IsDeleted).ToList();
        }


        public void RemoveCategory(int categoryId)
        {
            using (var db = new ApplicationContext())
            {
                if (db.Categories.ToList().Where(x => x.Id == categoryId).Count() == 0) return;
                db.Categories.ToList().Where(x => x.Id == categoryId).First().IsDeleted = true;
                db.SaveChanges();
            }
        }

        public void RemoveCustomer(int customerId)
        {
            using (var db = new ApplicationContext())
            {
                if (db.Customers.ToList().Where(x => x.Id == customerId).Count() == 0) return;
                db.Customers.ToList().Where(x => x.Id == customerId).First().IsDeleted = true;
                db.SaveChanges();
            }
        }

        public void RemoveProduct(int productId)
        {
            using (var db = new ApplicationContext())
            {
                if (db.Products.ToList().Where(x => x.Id == productId).Count() == 0) return;
                db.Products.ToList().Where(x => x.Id == productId).First().IsDeleted = true;
                db.SaveChanges();
            }
        }

        public void RemoveProductFromCategory(int productId, int categoryId)
        {
            using (var db = new ApplicationContext())
            {
                db.ProductCategories.RemoveRange(
                    db.ProductCategories.ToList().Where(x =>
                    x.CategoryId == categoryId && x.ProductId == productId));

                db.SaveChanges();
            }
        }

        public void RemoveProductFromSale(int productId, int saleId)
        {
            using (var db = new ApplicationContext())
            {
                db.SaleProducts.RemoveRange(
                    db.SaleProducts.ToList().Where(x =>
                    x.SaleId == saleId && x.ProductId == productId));

                db.SaveChanges();
            }
        }

        public void RemoveSale(int saleId)
        {
            using (var db = new ApplicationContext())
            {
                if (db.Sales.ToList().Where(x => x.Id == saleId).Count() == 0) return;
                db.Sales.ToList().Where(x => x.Id == saleId).First().IsDeleted = true;
                db.SaveChanges();
            }
        }

        public void RemoveStock(Product product)
        {
            using (var db = new ApplicationContext())
            {
                if (db.Stocks.ToList().Where(x => x.ProductId == product.Id).Count() == 0) 
                    throw new ArgumentOutOfRangeException(product.Name, "Товара нет на складе");
                else
                {
                    db.Stocks.ToList().Where(x => x.ProductId == product.Id).First().Quantity--;
                    if (db.Stocks.ToList().Where(x => x.ProductId == product.Id).First().Quantity == 0)
                        db.Stocks.RemoveRange(db.Stocks.ToList().Where(x => x.ProductId == product.Id));
                }
                db.SaveChanges();
            }
        }

        public void RemoveSupplier(int supplierId)
        {
            using (var db = new ApplicationContext())
            {
                if (db.Supplirs.ToList().Where(x => x.Id == supplierId).Count() == 0) return;
                db.Supplirs.ToList().Where(x => x.Id == supplierId).First().IsDeleted = true;
                db.SaveChanges();
            }
        }
    }
}
