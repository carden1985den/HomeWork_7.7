using HomeWork_7._7;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

abstract class Courier
{
    public void GetOrder()
    {
        Console.WriteLine("Курьер взял заказ");
    }
    abstract public void MoveOrder();
    public void PlaceOrder() {
        Console.WriteLine("Курьер взял заказ");
    }
}

class FootCourier : Courier
{
    override public void MoveOrder()
    {
        Console.WriteLine("Пеший курьер доставляет заказ на дом");
    }
}

class BikeCourier : Courier
{
    override public void MoveOrder()
    {
        Console.WriteLine("Курьер на велосипеде доставляет заказ в пункт выдачи");
    }
}

class CarCourier : Courier
{
    override public void MoveOrder()
    {
        Console.WriteLine("Курьер на машине доставляет заказ в пункт магазин");
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
    public HomeDelivery(Courier courier)
    {
        courier.GetOrder();
    }
    //public string deliveredTo = "Домой";
}

class PickPointDelivery : Delivery
{
    public PickPointDelivery(Courier courier)
    {
        courier.GetOrder();
    }
    //public string deliveredTo = "ПВЗ";
}

class ShopDelivery : Delivery
{
    public ShopDelivery(Courier courier) 
    {
        courier.GetOrder();
    }
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
    private Order _collection;
    //private string productName;

    public SearchOrderCollection(Order order)
    {
        _collection = order;
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
            for (int i = 0; i < _collection.ProductCollection.Length; i++)
            {
                if (_collection.ProductCollection[i].ProductName == searchProductName)
                {
                    Console.WriteLine("Указанный продукт {0}, присутствует в заказе",_collection.ProductCollection[i].ProductName);
                }
            }
            return null;
        }   
    }
}

class Order
{
    public int OrderNumber { get; }
    public string OrderRecipientName { get; }
    public string OrderRecipientPhoneNumber { get; }
    public string OrderRecipientAddress {  get; }
    public Delivery DeliveryType {  get; set; }
    public DeliveryPoint DeliveryPoint { get; set; }
    public ProductCollection[] ProductCollection {  get; set; }
    public Order(int OrderNumber, string orderRecipientName, string orderRecipientPhoneNumber, string orderRecipientAddress, DeliveryPoint deliveryPoint, ProductCollection[] productCollection )
    {
        this.DeliveryPoint = DeliveryPoint;
        this.OrderNumber = OrderNumber;
        this.OrderRecipientName = orderRecipientName;
        this.OrderRecipientPhoneNumber = orderRecipientPhoneNumber;
        this.OrderRecipientAddress = orderRecipientAddress;
        this.ProductCollection = productCollection;
        //this.DeliveryType = deliveryPoint;
        //Присваиваем номер заказу
        switch (deliveryPoint)
        {
            case DeliveryPoint.Home:
                Courier footCourier = new FootCourier();

                Delivery HomeDelivery = new HomeDelivery(footCourier);
                this.DeliveryType = HomeDelivery;

                break;
            case DeliveryPoint.PickPoint:
                Courier bikeCourier = new FootCourier();

                Delivery PickPointDelivery = new PickPointDelivery(bikeCourier);
                this.DeliveryType = PickPointDelivery;
                break;
            case DeliveryPoint.Shop:
                Courier carCourier = new FootCourier();

                Delivery ShopDelivery = new ShopDelivery(carCourier);
                this.DeliveryType =  ShopDelivery;
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

        //Статичный метод
        SearchOrderCollection.GetAllProduct(order2);

        //Индексатор
        SearchOrderCollection collection = new SearchOrderCollection(order1);
        Order findOrderProduct = collection["Хлеб"];

        
        //Прегрузка оператора
        Order order3 = order1 + order2;
        order3.DisplayOrder();

        
    }
}