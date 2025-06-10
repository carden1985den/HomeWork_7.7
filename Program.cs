abstract class Delivery
{
    public string Address;
}

class HomeDelivery : Delivery
{
    /* 
    доставка на дом. Этот тип будет подразумевать наличие курьера или передачу курьерской компании, 
    в нём будет располагаться своя, отдельная от прочих типов доставки, логика
     
     */
}

class PickPointDelivery : Delivery
{
    /*
    PickPointDelivery — доставка в пункт выдачи. Здесь будет храниться какая-то ещё логика,
    необходимая для процесса доставки в пункт выдачи, например, хранение компании и точки выдачи, 
    а также какой-то ещё информации  
    */
}

class ShopDelivery : Delivery
{
    /*
    доставка в розничный магазин. Эта доставка может выполняться внутренними средствами 
    компании и совсем не требует работы с «внешними» элементами
    */
}

class Order<TDelivery, TStruct> where TDelivery : Delivery
{
    public TDelivery Delivery;

    public int Number;

    public string Description;

    public void DisplayAddress()
    {
        Console.WriteLine(Delivery.Address);
    }

    // ... Другие поля
}
/*
 * 