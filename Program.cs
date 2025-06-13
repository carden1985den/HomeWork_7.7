enum ProductType
{
    food = 0,
    clothes,
    perfumery,
    chemistry,
    trinkets,
    none
}

class Product
{
    public string Name { get; set; }
    public ProductType type { get; set; }
    public float Weight {  get; set; }
    public int Count { get; set; }
}

abstract class Delivery
{
    public string Address;
}

class HomeDelivery : Delivery
{
    /* ... */
}

class PickPointDelivery : Delivery
{
    /* ... */
}

class ShopDelivery : Delivery
{
    /* ... */
}

class Order
{
    //элементы заказа
    public Product[] orderItems;
    //номер заказа
    public int _orderNumber;
    //адрес получателя
    private string _recipientAddress;
    //номер  для связи с получателем
    private string _recipientPhoneNumber;
    private string _recipientName;

    //конструктор 
    public Order(string recipientAddress, string recipientPhoneNumber, string recipientName)
    {
        _recipientAddress = recipientAddress;
        _recipientPhoneNumber = recipientPhoneNumber;
        _recipientName = recipientName;
    }

    //присваиваем номер заказу прибавляя на 1 изначально установленный номер orderNumber
    public void AssignOrderNumber(ref int orderNumber)
    {
        orderNumber += 1; 
        _orderNumber = orderNumber;
    }
    
    // заказ надо собрать
    public void PackOrder(params Product[] item)
    {
        this.orderItems = item;
    }
    
    //Отобразить детали по заказу
    public void DisplayOrderInformation()
    {
        Console.WriteLine("\nНомер заказа\t\t\t{0}\nАдрес доставки\t\t\t{1}\nНомер телефона получателя\t{2}\nИмя клиента\t\t\t{3}\nЗаказ:", _orderNumber, _recipientAddress, _recipientPhoneNumber, _recipientName);

        for (int i = 0; i< orderItems.Length; i++)
        {
            Console.WriteLine("\t{0}, кол-во: {1}", orderItems[i].Name,orderItems[i].Count);
        }

    }

    public SearchOrderedProduct this[string productName]
    {
        get;
        set;
    }
    // отправить курьером
}

class Programm
{
    static void Main()
    {
        int orderNumber = 0000;
        var order1 = new Order("Ижевск", "9127418278","Денис");

        //Собранный из товаров заказ
        order1.PackOrder(
            new Product { Name = "Хлеб", Count = 1, type = ProductType.food}, 
            new Product { Name = "Рубашка", type = ProductType.clothes, Count = 1 }
        );        
        order1.AssignOrderNumber(ref orderNumber);
        order1.DisplayOrderInformation();

        var order2 = new Order("Глазов", "9127418278", "Марина");
        order2.PackOrder(
            new Product() { Name = "Средство для чистки ванн", type = ProductType.chemistry, Count = 2}
            );
        order2.AssignOrderNumber(ref orderNumber);
        order2.DisplayOrderInformation();
    }
}