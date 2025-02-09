using System.Security.Cryptography;
using System.Text;

namespace PaymentSystems;

class Program
{
    static void Main(string[] args)
    {
        //Выведите платёжные ссылки для трёх разных систем платежа: 
        //pay.system1.ru/order?amount=12000RUB&hash={MD5 хеш ID заказа}
        //order.system2.ru/pay?hash={MD5 хеш ID заказа + сумма заказа}
        //system3.com/pay?amount=12000&curency=RUB&hash={SHA-1 хеш сумма заказа + ID заказа + секретный ключ от системы}

        Order order = new Order(100, 1000);
        
        PaySystem paySystem = new PaySystem(new MD5Service());
        OrderSystem orderSystem = new OrderSystem(new MD5Service());
        PayWithCurrencySystem payWithCurrencySystem = new PayWithCurrencySystem(new SHAService());
        
        Console.WriteLine($"pay.system1.ru/order?amount=12000RUB&hash={paySystem.GetPayingLink(order)}");
        Console.WriteLine($"order.system2.ru/pay?hash={orderSystem.GetPayingLink(order)}");
        Console.WriteLine($"system3.com/pay?amount=12000&curency=RUB&hash={payWithCurrencySystem.GetPayingLink(order)}");
    }
}

public class Order
{
    public int Id { get; }
    public int Amount { get; }

    public Order(int id, int amount)
    {
        Id = id;
        Amount = amount;
    }
}

public interface IPaymentSystem
{
    public string GetPayingLink(Order order);
}

public interface IHashService
{
    string Hash(string stringToHash);
}

public class MD5Service : IHashService
{
    public string Hash(string stringToHash)
    {
        if (stringToHash == null)
            throw new ArgumentException(nameof(stringToHash));
        
        byte[] bytes = MD5.HashData(Encoding.UTF8.GetBytes(stringToHash));
        string hashedString = Encoding.UTF8.GetString(bytes);
        
        return hashedString;
    }
}

public class SHAService : IHashService
{
    public string Hash(string stringToHash)
    {
        if (stringToHash == null)
            throw new ArgumentException(nameof(stringToHash));
        
        byte[] bytes = SHA1.HashData(Encoding.UTF8.GetBytes(stringToHash));
        string hashedString = Encoding.UTF8.GetString(bytes);
        
        return hashedString;
    }
}

public class PaySystem : IPaymentSystem
{
    private readonly IHashService _hashService;

    public PaySystem(IHashService hashService) => 
        _hashService = hashService;

    public string GetPayingLink(Order order)
    {
        if(order == null)
            throw new ArgumentException(nameof(order));
        
        return _hashService.Hash(order.Id.ToString());
    }
}

public class OrderSystem : IPaymentSystem
{
    private readonly IHashService _hashService;

    public OrderSystem(IHashService hashService) => 
        _hashService = hashService;

    public string GetPayingLink(Order order)
    {
        if(order == null)
            throw new ArgumentException(nameof(order));
        
        string stringToHash = $"{order.Id}_{order.Amount}";
        
        return _hashService.Hash(stringToHash);
    }
}

public class PayWithCurrencySystem : IPaymentSystem
{
    private const string SystemSecretKey = "0XASDNLJsd";
    
    private readonly IHashService _hashService;

    public PayWithCurrencySystem(IHashService hashService) => 
        _hashService = hashService;

    public string GetPayingLink(Order order)
    {
        if(order == null)
            throw new ArgumentException(nameof(order));
        
        string stringToHash = $"{order.Id}_{order.Amount}_{SystemSecretKey}";
        
        return _hashService.Hash(stringToHash);
    }
}