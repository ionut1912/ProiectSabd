namespace Orders.API.Models
{
    public class OrderForCreation
    {
    
        public string restaurantId { get; set; }
    
        public string address { get; set; }
        
        public string customerId { get; set; }
        
        public decimal price { get; set; }
    }
}
