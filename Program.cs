using HomeWork_7._7;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

abstract class Courier
{
    abstract public void GetOrder(int orderNumber);
    abstract public void MoveOrder();
    abstract public void PlaceOrder();
}

class FootCourier : Courier
{
    override public void GetOrder(int orderNumber)
    {
        Console.WriteLine("Курьер взял заказ ({0})", orderNumber);
    }
    override public void MoveOrder()
    {
        Console.WriteLine("Пеший курьер доставляет заказ на дом");
    }

    override public void PlaceOrder()
    {
        Console.WriteLine("Курьер оставил заказ");
    }
}

class BikeCourier : Courier
{
    override public void GetOrder(int orderNumber)
    {
        Console.WriteLine("Курьер взял заказ ({0})", orderNumber);
    }
    override public void MoveOrder()
    {
        Console.WriteLine("Курьер на велосипеде доставит заказ в пункт выдочи");
    }

    override public void PlaceOrder()
    {
        Console.WriteLine("Курьер оставил заказ");
    }
}

class CarCourier : Courier
{
    override public void GetOrder(int orderNumber)
    {
        Console.WriteLine("Курьер взял заказ ({0})", orderNumber);
    }
    override public void MoveOrder()
    {
        Console.WriteLine("Курьер на машине доставляет заказ в магазин");
    }

    override public void PlaceOrder()
    {
        Console.WriteLine("Курьер оставил заказ");
    }
}

enum DeliveryPoint
{
    Home,
    PickPoint,
    Shop
}

abstract class Delivery
{
    abstract public void Deliver(Courier courier);
}

class HomeDelivery : Delivery
{
    private Order _order;
    public HomeDelivery(Order order)
    {
        _order = order;
    }
    override public void Deliver(Courier courier)
    {
        courier.GetOrder(_order.OrderNumber);
        courier.MoveOrder();
        courier.PlaceOrder();
    }
}

class PickPointDelivery : Delivery
{
    private Order _order;
    public PickPointDelivery(Order order)
    {
        _order = order;
    }
    override public void Deliver(Courier courier)
    {
        courier.GetOrder(_order.OrderNumber);
        courier.MoveOrder();
        courier.PlaceOrder();
    }
}

class ShopDelivery : Delivery
{
    private Order _order;
    public ShopDelivery(Order order)
    {
        _order = order;
    }
    override public void Deliver(Courier courier)
    {
        courier.GetOrder(_order.OrderNumber);
        courier.MoveOrder();
        courier.PlaceOrder();
    }
}

enum ProductType
{
    Food = 0,
    Clothes,
    Chemical,
    PetProduct,
    Other
}
class ProductCollection
{
    public string ProductName {  get; set; }
    public int ProductCount {  get; set; }
    public float ProductWeight {  get; set; }
    public ProductType ProductType {  get; set; }
}
    
class SearchOrderCollection
{
    private Order _collection;
    public SearchOrderCollection(Order order)
    {
        _collection = order;
    }
    public static void GetAllProductsInOrder(Order collection)
    {
        int count = 1;
        Console.WriteLine("\nПросмотр продуктов в заказе ({0}):", collection.OrderNumber);
        foreach (var item in collection.ProductCollection)
        {
            Console.WriteLine("{0} {1}",count ,item.ProductName);
            count++;
        }
    }
    public Order this[string searchProductName] {
    //поиск по элементам заказа
        get
        {
            Console.WriteLine("\nПоиск продукта ({1}) в заказе ({0}):", _collection.OrderNumber, searchProductName);
            for (int i = 0; i < _collection.ProductCollection.Length; i++)
            {
                if (_collection.ProductCollection[i].ProductName == searchProductName)
                {
                    Console.WriteLine("Указанный продукт ({0}), присутствует в заказе",_collection.ProductCollection[i].ProductName);
                }
            }
            return null;
        }   
    }
}

class Order
{
    public int OrderNumber { get; set; }
    public string OrderRecipientName { get; set; }
    public string OrderRecipientPhoneNumber { get; set; }
    public string OrderRecipientAddress {  get; set; }
    public DeliveryPoint DeliveryPoint { get; set; }
    public ProductCollection[] ProductCollection {  get; set; }
    public Order(int OrderNumber, string orderRecipientName, string orderRecipientPhoneNumber, string orderRecipientAddress, DeliveryPoint deliveryPoint, ProductCollection[] productCollection )
    {
        this.DeliveryPoint = deliveryPoint;
        this.OrderNumber = OrderNumber;
        this.OrderRecipientName = orderRecipientName;
        this.OrderRecipientPhoneNumber = orderRecipientPhoneNumber;
        this.OrderRecipientAddress = orderRecipientAddress;
        this.ProductCollection = productCollection;
    }

    public void DisplayOrder()
    {
        Console.WriteLine("\nИнформация о заказе ({0})", OrderNumber);
        Console.WriteLine("Номер заказа: ({0})", OrderNumber);
        Console.WriteLine("Имя получателя: ({0})", OrderRecipientName);
        Console.WriteLine("Номер телефона получателя: ({0})", OrderRecipientPhoneNumber);
        Console.WriteLine("Адрес получателя: ({0})", OrderRecipientAddress);

        Console.WriteLine("\nПродукты добавленные в заказ:");
        foreach (var item in ProductCollection)
        {
            Console.WriteLine(item.ProductName);
        }
    }

    public static Order operator +(Order order1, Order order2)
    {
        ProductCollection[] _col1 = order1.ProductCollection;
        ProductCollection[] _col2 = order2.ProductCollection;
        var _col3 = _col1.Concat(_col2).ToArray();

        return new Order(order1.OrderNumber, order1.OrderRecipientName, order1.OrderRecipientPhoneNumber, order1.OrderRecipientAddress, order1.DeliveryPoint, _col3);
    }
}

class Programm
{
     static void Main()
    {   
        Order order1 = new Order(1, "Денис", "+7 912 741 82 78", "Ижевск", DeliveryPoint.Shop,
            new ProductCollection[] {
                new ProductCollection { ProductName = "Хлеб", ProductCount = 1, ProductType = ProductType.Food},
                new ProductCollection { ProductName = "Колбаса", ProductCount = 2, ProductType = ProductType.Food }
            }
        );

        Order order2 = new Order(2, "Екатерина", "+7 912 874 40 24", "Можга", DeliveryPoint.Home,
            new ProductCollection[] {
                new ProductCollection { ProductName = "Средство для чистки ванн", ProductCount = 5, ProductType = ProductType.Chemical},
                new ProductCollection { ProductName = "Рубашка", ProductCount = 1, ProductType = ProductType.Clothes }
            }
        );

        List<Order> orderList = new List<Order>();
        orderList.Add(order1);
        orderList.Add(order2);
        
        //перебираем список заказов
        foreach ( Order order in orderList)
        {
            switch (order.DeliveryPoint)
            {
                case DeliveryPoint.Home:
                    Courier footCourier = new FootCourier();
                    Delivery HomeDelivery = new HomeDelivery(order);
                    HomeDelivery.Deliver(footCourier);
                    break;
                case DeliveryPoint.PickPoint:
                    Courier bikeCourier = new BikeCourier();
                    Delivery PickPointDelivery = new PickPointDelivery(order);
                    PickPointDelivery.Deliver(bikeCourier);
                    break;
                case DeliveryPoint.Shop:
                    Courier carCourier = new CarCourier();
                    Delivery ShopDelivery = new ShopDelivery(order);
                    ShopDelivery.Deliver(carCourier);
                    break;
            }
        }

        //Статичный метод
        SearchOrderCollection.GetAllProductsInOrder(order1);

        //Индексатор
        SearchOrderCollection collection = new SearchOrderCollection(order1);
        Order findOrderProduct = collection["Хлеб"];

        //Прегрузка оператора
        Order order3 = order1 + order2;
        order3.DisplayOrder();
        order1.DisplayOrder();
    }
}