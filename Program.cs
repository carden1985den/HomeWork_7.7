using HomeWork_7._7;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;

class Courier
{
    bool isOrderDelivered = false;
    private Order _Order {  get; set; }
    
    public Courier(Order order)
    {
        _Order = order;
        GetOrder();
        MoveOrder();
        PlaceOrder();
    }
    private void GetOrder()
    {
        Console.WriteLine("Курьер получил заказ номер: {0}", _Order.OrderNumber);
    }
    private void MoveOrder() 
    {
        Console.WriteLine("Курьер доставляет заказ {0} по адресу {1}, тип доставки {2}", _Order.OrderNumber, _Order.OrderRecipientAddress, (Delivery) _Order.DeliveryPoint);
    }
    private void PlaceOrder()
    {
        Console.WriteLine("Курьер доставил заказ {0} по адресу {1} и вручмл получател {2}", _Order.OrderNumber, _Order.OrderRecipientAddress, _Order.OrderRecipientName);
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
    public DeliveryPoint deliveredTo;
    //public abstract void CourierType();
}

class HomeDelivery : Delivery
{
    //public string deliveredTo = "Домой";
}

class PickPointDelivery : Delivery
{
    //public string deliveredTo = "ПВЗ";
}

class ShopDelivery : Delivery
{
    //public string deliveredTo = "Магазин";
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
    private Order _Collection {  get; set; }
    //private string productName;

    public SearchOrderCollection(Order order)
    {
        _Collection = order;
    }

    public static void GetAllProduct(Order collection)
    {
        foreach (var item in collection.ProductCollection)
        {
            Console.WriteLine(item.ProductName);
        }
    }

    public Order this[string searchProductName] {
    //поиск по элементам заказа
        get
        {
            for (int i = 0; i < _Collection.ProductCollection.Length; i++)
            {
                if (_Collection.ProductCollection[i].ProductName == searchProductName)
                {
                    Console.WriteLine(_Collection.ProductCollection[i].ProductName);
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
    public DeliveryPoint DeliveryPoint {  get; set; }
    public ProductCollection[] ProductCollection {  get; set; }

    public Order(ref int orderNumber, string orderRecipientName, string orderRecipientPhoneNumber, string orderRecipientAddress, DeliveryPoint deliveryPoint, ProductCollection[] productCollection )
    {
        this.OrderRecipientName = orderRecipientName;
        this.OrderRecipientPhoneNumber = orderRecipientPhoneNumber;
        this.OrderRecipientAddress = orderRecipientAddress;
        this.ProductCollection = productCollection;

        //Присваиваем номер заказу
        orderNumber++;
        this.OrderNumber = orderNumber;

        switch (deliveryPoint)
        {
            case DeliveryPoint.Home:
                Delivery HomeDelivery = new HomeDelivery();
                HomeDelivery.deliveredTo = deliveryPoint;
                //this.DeliveryPoint = deliveryPoint;
                break;
            case DeliveryPoint.PickPoint:
                Delivery PickPointDelivery = new PickPointDelivery();
                PickPointDelivery.deliveredTo = deliveryPoint;
                //this.DeliveryPoint = deliveryPoint;
                break;
            case DeliveryPoint.Shop:
                Delivery ShopDelivery = new ShopDelivery();
                ShopDelivery.deliveredTo = deliveryPoint;
                //this.DeliveryPoint = deliveryPoint;
                break;
        }
    }

    public void DisplayOrder()
    {
        Console.WriteLine(OrderNumber);
        Console.WriteLine(OrderRecipientName);
        Console.WriteLine(OrderRecipientPhoneNumber);
        Console.WriteLine(OrderRecipientAddress);
        
        foreach (var item in ProductCollection)
        {
            Console.WriteLine(item.ProductName);
        }
    }
}

class Programm
{
    static void Main()
    {
        int orderNumber = 0;

        Order order1 = new Order(ref orderNumber, "Денис", "+7 912 741 82 78", "Ижевск", DeliveryPoint.Shop,
            new ProductCollection[] {
                new ProductCollection { ProductName = "Хлеб", ProductCount = 1, ProductType = ProductType.Food},
                new ProductCollection { ProductName = "Колбаса", ProductCount = 2, ProductType = ProductType.Food }
            }
        );

        Order order2 = new Order(ref orderNumber, "Екатерина", "+7 912 874 40 24", "Можга", DeliveryPoint.Home,
            new ProductCollection[] {
                new ProductCollection { ProductName = "Средство для чистки ванн", ProductCount = 5, ProductType = ProductType.Chemical},
                new ProductCollection { ProductName = "Рубашка", ProductCount = 1, ProductType = ProductType.Clothes }
            }
        );

        List<Order> orderList = new List<Order>();
        orderList.Add(order1);
        orderList.Add(order2);

        //Статичный метод
        SearchOrderCollection.GetAllProduct(order2);

        //Индексатор
        SearchOrderCollection collection = new SearchOrderCollection(order1);
        Order findOrderProduct = collection["Хлеб"];

        foreach ( var item in orderList)
        {
            Courier courier = new Courier(item);
        }
    }
}