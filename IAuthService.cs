using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WarehouseAccountingJewelryStoreService.Objects;

namespace WarehouseAccountingJewelryStoreService
{
    // ПРИМЕЧАНИЕ. Можно использовать команду "Переименовать" в меню "Рефакторинг", чтобы изменить имя интерфейса "IAuthService" в коде и файле конфигурации.
    [ServiceContract]
    public interface IAuthService
    {
        [OperationContract]
        int Auth(string username, string password);

        [OperationContract]
        bool Register(string login, string password, int level);

        [OperationContract]
        void Remove(string login);

        [OperationContract]
        bool IsAdminCreated();

        [OperationContract]
        IEnumerable<User> GetUsers();
    }
}
