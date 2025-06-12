enum OrderItem
{
    food = 0,
    clothes,
    perfumery,
    chemistry,
    trinkets
}

class Order //<TPhone>
{
    //элементы заказа
    public OrderItem[] orderItem;
    //номер заказа
    public int _orderNumber;
    private string _recipientAddress;

    
    public int AssignOrderNumber(ref int orderNumber)
    {
        _orderNumber = orderNumber + 1;
        return _orderNumber;
    }
    
    //адрес доставки
    public string RecipientAddress
    {
        get {
            return _recipientAddress;
        }
        set
        {
            if (!string.IsNullOrEmpty(value))
            {
                _recipientAddress = value;
            }
        }
    }

    //номер телефона получателя, может быть и 89121234556 
    public string RecipientPhone {get;set;}

    // заказ надо собрать
    public void PackOrder(params OrderItem[] item)
    {
        this.orderItem = item;
    }
    
    //Отобразить детали по заказу
    public void DisplayOrder()
    {
        Console.WriteLine("Заказ номер\t\t\t{0}\nАдрес доставки\t\t{1}\nНомер телефона получателя {2}", _orderNumber, RecipientAddress, RecipientPhone);
    }
    // отправить курьером
}

class Programm
{
    static void Main()
    {
        int orderNumber = 0000;
        var bag1 = new Order();
        bag1.PackOrder(OrderItem.food, OrderItem.chemistry);
        //List<OrderItem> itemCollection = new List<OrderItem>();
        bag1.DisplayOrder();
    }
}