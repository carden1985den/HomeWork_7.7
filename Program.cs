using HomeWork_7._7;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;

abstract class Courier
{
    public void GetOrder() { 
    
    }
    abstract public void MoveOrder();
    public void PlaceOrder()
    {

    }
}

class HomeCurier : Courier
{ 
    override public void MoveOrder() {
        Console.WriteLine("Идём пешком");
    }
}

class PickPointCourier : Courier
{
    override public void MoveOrder() {
        Console.WriteLine("Едем на велосипеде");
    }
}

class ShopCourier : Courier
{
    override public void MoveOrder() {
        Console.WriteLine("Едем на машине");
    }
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
    private Order collection;
    private string productName;

    public SearchOrderCollection(Order order)
    {
        collection = order;
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
            for (int i = 0; i < collection.productCollection.Length; i++)
            {
                if (collection.productCollection[i].productName == searchProductName)
                {
                    Console.WriteLine(collection.productCollection[i].productName);
                }
            }
            return null;
        }   
    }
}

class Order
{
    private int _orderNumber;
    private string _orderRecipientName;
    private string _orderRecipientPhoneNumber;
    private string _orderRecipientAddress;
    public ProductCollection[] productCollection;
    public Order(ref int orderNumber, string orderRecipientName, string orderRecipientPhoneNumber, string orderRecipientAddress, ProductCollection[] productCollection )
    {
        _orderRecipientName = orderRecipientName;
        _orderRecipientPhoneNumber = orderRecipientPhoneNumber;
        _orderRecipientAddress = orderRecipientAddress;
        this.productCollection = productCollection;
        

        //Присваиваем номер заказу
        orderNumber++;
        _orderNumber = orderNumber;
    }

    public void DisplayOrderProduct()
    {
        foreach (var item in productCollection)
        {
            Console.WriteLine(item.productName);
        }
    }
}

/*class MyOrder : Order {
    public void NewOrder()
    {
        //create order, 
        //UserName, address, Product
    }
}*/


class Programm
{
    static void Main()
    {
        int orderNumber = 0;

        Order order1 = new Order(ref orderNumber, "Денис", "+7 912 741 82 78", "Ижевск", 
            new ProductCollection[] {
                new ProductCollection { productName = "Хлеб", productCount = 1, productType = ProductType.food},
                new ProductCollection { productName = "Колбаса", productCount = 2, productType = ProductType.food }
            }
        );
        Order order2 = new Order(ref orderNumber, "Екатерина", "+7 912 874 40 24", "Можга",
            new ProductCollection[] {
                new ProductCollection { productName = "Средство для чистки ванн", productCount = 5, productType = ProductType.chemical},
                new ProductCollection { productName = "Рубашка", productCount = 1, productType = ProductType.clothes }
            }
        );


        SearchOrderCollection.GetAllProduct(order2);
        SearchOrderCollection collection = new SearchOrderCollection(order1);
        Order findOrderProduct = collection["Хлеб"];
    }
}