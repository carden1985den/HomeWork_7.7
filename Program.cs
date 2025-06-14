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
    chemicals,
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

class OrderCollectionSearch
{
    private static Order[] _collectionProducts;
    static void Search(Order[] collectionProducts)
    {
        _collectionProducts = collectionProducts;

        foreach (var item in _collectionProducts) {
            item;
        }
    }

    public Order this[string searchProductName] {
    //поиск по элементам заказа
        get
        {
            for (int i = 0; i < _collectionProducts.Length; i++)
            {
                if (_collectionProducts[i]._productName == searchProductName)
                {
                    
                }
            }
        }
        set
        {

        }
    }

}
class Order
{
    private int _orderNumber;
    private string _orderRecipientPhoneNumber;
    private string _orderRecipientAddress;
    private ProductCollection[] _productCollection;
    public Order(ref int orderNumber, string orderRecipientPhoneNumber, string orderRecipientAddress, ProductCollection[] productCollection )
    {
        _orderRecipientPhoneNumber = orderRecipientPhoneNumber;
        _orderRecipientAddress = orderRecipientAddress;
        _productCollection = productCollection;
        
        //Присваиваем номер заказу
        orderNumber++;
        _orderNumber = orderNumber;
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
        ProductCollection[] productCollection = new ProductCollection[] { productCount = 0 };

        Order order = new Order(ref orderNumber, "+7 912 741 82 78", "Ижевск", productCollection);
    }
}