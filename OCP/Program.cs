using System;

namespace IMJunior
{
    class Program
    {
        static void Main(string[] args)
        {
            var orderForm = new OrderForm();
            var paymentHandler = new PaymentHandler();

            var systemId = orderForm.ShowForm();

            if (systemId == "QIWI")
                Console.WriteLine("Перевод на страницу QIWI...");
            else if (systemId == "WebMoney")
                Console.WriteLine("Вызов API WebMoney...");
            else if (systemId == "Card")
                Console.WriteLine("Вызов API банка эмитера карты Card...");

            paymentHandler.ShowPaymentResult(systemId);
        }
    }

    public class OrderForm
    {
        public string ShowForm()
        {
            Console.WriteLine("Мы принимаем: QIWI, WebMoney, Card");

            //симуляция веб интерфейса
            Console.WriteLine("Какое системой вы хотите совершить оплату?");
            return Console.ReadLine();
        }
    }

    public class PaymentHandler
    {
        public void ShowPaymentResult(string systemId)
        {
            Console.WriteLine($"Вы оплатили с помощью {systemId}");

            if (systemId == "QIWI")
                Console.WriteLine("Проверка платежа через QIWI...");
            else if (systemId == "WebMoney")
                Console.WriteLine("Проверка платежа через WebMoney...");
            else if (systemId == "Card")
                Console.WriteLine("Проверка платежа через Card...");

            Console.WriteLine("Оплата прошла успешно!");
        }
    }
}