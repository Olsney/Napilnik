using PaymentSystems;

namespace IMJunior
{
    class Program
    {
        static void Main(string[] args)
        {
            List<IPaymentSystemFactory> systemFactories = new List<IPaymentSystemFactory>();

            systemFactories.Add(new QiwiPaymentSystemsFactory());
            systemFactories.Add(new WebMoneyPaymentSystemsFactory());
            systemFactories.Add(new CardPaymentSystemsFactory());

            PaymentSystemsFactoryProvider provider = new PaymentSystemsFactoryProvider(systemFactories);

            var orderForm = new OrderForm(provider.Ids);

            IPaymentSystemFactory paymentSystemFactory = provider.GetPaymentSystem(orderForm.GetPaymentSystemId());

            var paymentHandler = new PaymentHandler(paymentSystemFactory);

            paymentHandler.Handle();
        }
    }

    public class OrderForm
    {
        private readonly List<string> _ids;

        public OrderForm(List<string> ids) => 
            _ids = ids;

        public string GetPaymentSystemId()
        {
            ShowPaymentSystemsInfo();
            Console.WriteLine("Введите название платежной системой, которой вы хотите совершить оплату.");

            return Console.ReadLine();
        }

        private void ShowPaymentSystemsInfo() => 
            Console.WriteLine($"Мы принимаем: {string.Join(", ", _ids)}");
    }

    internal class PaymentHandler
    {
        private readonly IPaymentSystem _paymentSystem;

        public PaymentHandler(IPaymentSystemFactory systemFactory) => 
            _paymentSystem = systemFactory.Create();

        public void Handle() => 
            _paymentSystem.Pay();
    }

    internal class QiwiPaymentSystem : IPaymentSystem
    {
        public void Pay() => 
            Console.WriteLine($"Вы успешно совершили оплату с помощью {GetType().Name}");
    }

    internal class WebMoneyPaymentSystem : IPaymentSystem
    {
        public void Pay() => 
            Console.WriteLine($"Вы успешно совершили оплату с помощью {GetType().Name}");
    }

    internal class CardPaymentSystem : IPaymentSystem
    {
        public void Pay() => 
            Console.WriteLine($"Вы успешно совершили оплату с помощью {GetType().Name}");
    }

    internal interface IPaymentSystem
    {
        void Pay();
    }


    internal interface IPaymentSystemFactory
    {
        public string Id { get; }
        IPaymentSystem Create();
    }

    class QiwiPaymentSystemsFactory : IPaymentSystemFactory
    {
        public string Id { get; } = "Qiwi";

        public IPaymentSystem Create() =>
            new QiwiPaymentSystem();
    }

    class WebMoneyPaymentSystemsFactory : IPaymentSystemFactory
    {
        public string Id { get; } = "WebMoney";

        public IPaymentSystem Create() =>
            new WebMoneyPaymentSystem();
    }

    class CardPaymentSystemsFactory : IPaymentSystemFactory
    {
        public string Id { get; } = "Card";

        public IPaymentSystem Create() =>
            new CardPaymentSystem();
    }

    class PaymentSystemsFactoryProvider
    {
        private readonly List<IPaymentSystemFactory> _systems;

        public PaymentSystemsFactoryProvider(List<IPaymentSystemFactory> systems) =>
            _systems = systems;

        public List<string> Ids => _systems.Select(x => x.Id).ToList();

        public IPaymentSystemFactory GetPaymentSystem(string id) =>
            _systems.Single(factory => factory.Id.ToLower() == id.ToLower());
    }
}