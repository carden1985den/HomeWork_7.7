namespace HomeWork_7._7
{
    class Order<TDelivery, TStruct> where TDelivery : Delivery
    {
        public TDelivery delivery;
        public string description;
        private int _ID;
        
        public int ID 
        {
            get {
                _ID = ID;
                return _ID;
            }
            set { 
            
            }
        }
        
        public void DisplayAddress()
        {
            Console.WriteLine(delivery.address);
        }

        // ... Другие поля
    }
}