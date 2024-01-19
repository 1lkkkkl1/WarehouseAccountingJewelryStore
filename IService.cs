using System.Collections.Generic;
using System.ServiceModel;
using WarehouseAccountingJewelryStoreService.Objects;

namespace WarehouseAccountingJewelryStoreService
{
    [ServiceContract]
    public interface IService
    {
        // Продукты
        [OperationContract]
        void AddProduct(Product product);

        [OperationContract]
        IEnumerable<Product> GetProducts();

        [OperationContract]
        IEnumerable<Product> GetProductsBySale(int saleId);

        [OperationContract]
        void EditProduct(Product product);

        [OperationContract]
        void RemoveProduct(int productId);

        // Поставщики
        [OperationContract]
        void AddSupplier(Supplier supplier);

        [OperationContract]
        IEnumerable<Supplier> GetSuppliers();

        [OperationContract]
        void EditSupplier(Supplier supplier);

        [OperationContract]
        void RemoveSupplier(int supplierId);

        // Складские записи
        [OperationContract]
        void AddStock(List<Product> products);

        [OperationContract]
        IEnumerable<Stock> GetStocks();

        [OperationContract]
        void EditStock(Stock stock);

        [OperationContract]
        void RemoveStock(Product product);

        // Продажи
        [OperationContract]
        void AddSale(List<Product> products, Customer customer);

        [OperationContract]
        IEnumerable<Sale> GetSales();

        [OperationContract]
        void EditSale(Sale sale);

        [OperationContract]
        void RemoveSale(int saleId);

        // Клиенты
        [OperationContract]
        void AddCustomer(Customer customer);

        [OperationContract]
        IEnumerable<Customer> GetCustomers();

        [OperationContract]
        void EditCustomer(Customer customer);

        [OperationContract]
        void RemoveCustomer(int customerId);

        // Категории
        [OperationContract]
        void AddCategory(Category category);

        [OperationContract]
        IEnumerable<Category> GetCategories();

        [OperationContract]
        IEnumerable<Category> GetCategoriesByProduct(int productId);

        [OperationContract]
        void EditCategory(Category category);

        [OperationContract]
        void RemoveCategory(int categoryId);

        // Привязка продуктов к категориям
        [OperationContract]
        void AddProductToCategory(int productId, int categoryId);

        [OperationContract]
        void RemoveProductFromCategory(int productId, int categoryId);

        // Привязка продаж к продуктам
        [OperationContract]
        void AddProductToSale(int productId, int saleId);

        [OperationContract]
        void RemoveProductFromSale(int productId, int saleId);
    }

}
