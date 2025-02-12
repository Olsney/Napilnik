namespace IMJunior
{
    class Program
    {
        static void Main(string[] args)
        {
            var orderForm = new OrderForm();
            var paymentHandler = new PaymentHandler();
            
            orderForm.ShowPaymentSystemsInfo();
                
            int paymentSystemId = orderForm.GetPaymentSystemId();
            
            paymentHandler.Handle(paymentSystemId);
        }
    }

    public enum PaymentSystems
    {
        Unknown = 0,
        QIWI = 100,
        WebMoney = 200,
        Card = 300
    }

    public class OrderForm
    {
        public int GetPaymentSystemId()
        {
            Console.WriteLine("Введите номер платежной системой, которой вы хотите совершить оплату.");
            
            return int.TryParse(Console.ReadLine(), out int systemId) 
                ? systemId 
                : 0;
        }
        
        public void ShowPaymentSystemsInfo() => 
            Console.WriteLine($"Мы принимаем: " +
                              $"1 - {PaymentSystems.QIWI}, " +
                              $"2 - {PaymentSystems.Card}, " +
                              $"3 - {PaymentSystems.WebMoney}");
    }

    public class PaymentHandler
    {
        public void Handle(int systemId)
        {
            PaymentSystems paymentSystem = (PaymentSystems)systemId;
            
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

            Console.WriteLine($"Вы оплатили с помощью {paymentSystem}");
            Console.WriteLine("Оплата прошла успешно!");
        }
    }
}