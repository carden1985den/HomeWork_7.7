using HomeWork_7._7;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;

class Courier
{
    bool isOrderDelivered = false;
    private Order _order;
    public Courier(Order order)
    {
        _order = order;
        GetOrder();
        MoveOrder();
        PlaceOrder();
    }
    private void GetOrder()
    {
        Console.WriteLine("Курьер получил заказ номер: {0}", _order.orderNumber);
    }
    private void MoveOrder() 
    {
        Console.WriteLine("Курьер доставляет заказ {0} по адресу {1}, тип доставки {2}", _order.orderNumber, _order.orderRecipientAddress, (Delivery) _order.deliveryPoint);
    }
    private void PlaceOrder()
    {
        Console.WriteLine("Курьер доставил заказ {0} по адресу {1} и вручмл получател {2}", _order.orderNumber, _order.orderRecipientAddress, _order.orderRecipientName);
    }
}

enum DeliveryType
{
    home,
    pickPoint,
    shop
}

abstract class Delivery
{
    public DeliveryType deliveredTo;
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
    food = 0,
    clothes,
    chemical,
    petProduct,
    other
}
class ProductCollection
{
    public string productName;
    public int productCount;
    public float productWeight;
    public ProductType productType;
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
        foreach (var item in collection.productCollection)
        {
            Console.WriteLine(item.productName);
        }
    }

    public Order this[string searchProductName] {
    //поиск по элементам заказа
        get
        {
            for (int i = 0; i < _collection.productCollection.Length; i++)
            {
                if (_collection.productCollection[i].productName == searchProductName)
                {
                    Console.WriteLine(_collection.productCollection[i].productName);
                }
            }
            return null;
        }   
    }
}

class Order
{
    public int orderNumber;
    public string orderRecipientName;
    public string orderRecipientPhoneNumber;
    public string orderRecipientAddress;
    public Delivery deliveryPoint;
    public ProductCollection[] productCollection;

    public Order(ref int orderNumber, string orderRecipientName, string orderRecipientPhoneNumber, string orderRecipientAddress, string deliveryPoint, ProductCollection[] productCollection )
    {
        this.orderRecipientName = orderRecipientName;
        this.orderRecipientPhoneNumber = orderRecipientPhoneNumber;
        this.orderRecipientAddress = orderRecipientAddress;
        this.productCollection = productCollection;

        //Присваиваем номер заказу
        orderNumber++;
        this.orderNumber = orderNumber;

        switch (deliveryPoint)
        {
            case "Home":
                Delivery HomeDelivery = new HomeDelivery();
                HomeDelivery.deliveredTo = DeliveryType.home;
                this.deliveryPoint = HomeDelivery;
                break;
            case "PickPoint":
                Delivery PickPointDelivery = new PickPointDelivery();
                PickPointDelivery.deliveredTo = DeliveryType.pickPoint;
                this.deliveryPoint = PickPointDelivery;
                break;
            case "Shop":
                Delivery ShopDelivery = new ShopDelivery();
                ShopDelivery.deliveredTo = DeliveryType.shop;
                this.deliveryPoint = ShopDelivery;
                break;
        }
    }

    public void DisplayOrder()
    {
        Console.WriteLine(orderNumber);
        Console.WriteLine(orderRecipientName);
        Console.WriteLine(orderRecipientPhoneNumber);
        Console.WriteLine(orderRecipientAddress);
        
        foreach (var item in productCollection)
        {
            Console.WriteLine(item.productName);
        }
    }
}

class Programm
{
    static void Main()
    {
        int orderNumber = 0;

        Order order1 = new Order(ref orderNumber, "Денис", "+7 912 741 82 78", "Ижевск", "Shop",
            new ProductCollection[] {
                new ProductCollection { productName = "Хлеб", productCount = 1, productType = ProductType.food},
                new ProductCollection { productName = "Колбаса", productCount = 2, productType = ProductType.food }
            }
        );

        Order order2 = new Order(ref orderNumber, "Екатерина", "+7 912 874 40 24", "Можга", "Home",
            new ProductCollection[] {
                new ProductCollection { productName = "Средство для чистки ванн", productCount = 5, productType = ProductType.chemical},
                new ProductCollection { productName = "Рубашка", productCount = 1, productType = ProductType.clothes }
            }
        );

        List<Order> orderList = new List<Order>();
        orderList.Add(order1);
        orderList.Add(order2);

        SearchOrderCollection.GetAllProduct(order2);

        SearchOrderCollection collection = new SearchOrderCollection(order1);
        Order findOrderProduct = collection["Хлеб"];

        foreach ( var item in orderList)
        {
            Courier courier = new Courier(item);
        }
    }
}