namespace IMJunior
{
    class Program
    {
        static void Main(string[] args)
        {
            var orderForm = new OrderForm();
            var paymentHandler = new PaymentHandler();
            int paymentSystemId = orderForm.GetPaymentSystemId();
            
            orderForm.ShowPaymentSystemsInfo();
            paymentHandler.Handle(paymentSystemId);
        }
    }

    public enum PaymentSystems
    {
        Unknown = 0,
        QIWI = 1,
        WebMoney = 2,
        Card = 3
    }

    public class OrderForm
    {
        public int GetPaymentSystemId()
        {
            Console.WriteLine("Какой платежной системой вы хотите совершить оплату?");
            
            return int.TryParse(Console.ReadLine(), out int systemId) 
                ? systemId 
                : 0;
        }
        
        public void ShowPaymentSystemsInfo() => 
            Console.WriteLine($"Мы принимаем: {PaymentSystems.QIWI}, {PaymentSystems.Card}, {PaymentSystems.WebMoney}");
    }

    public class PaymentHandler
    {
        public void Handle(int systemId)
        {
            PaymentSystems paymentSystem = (PaymentSystems)systemId;

            Console.WriteLine($"Вы оплатили с помощью {systemId}");

            switch (paymentSystem)
            {
                case PaymentSystems.QIWI:
                    Console.WriteLine($"Проверка платежа через {PaymentSystems.QIWI}...");
                    break;
                case PaymentSystems.WebMoney:
                    Console.WriteLine($"Проверка платежа через {PaymentSystems.WebMoney}...");
                    break;
                case PaymentSystems.Card:
                    Console.WriteLine($"Проверка платежа через {PaymentSystems.Card}...");
                    break;
                default:
                    throw new Exception($"Платежная система {PaymentSystems.Unknown}. Оплата прошла неудачно.");
            }

            Console.WriteLine("Оплата прошла успешно!");
        }
    }
}