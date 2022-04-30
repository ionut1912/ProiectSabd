﻿namespace MenuItems.API.Models
{
    public class MenuItemForCreation
    {
        public string ItemName { get; set; }
      
        public string Category { get; set; }

        public List<string> Ingredients { get; set; } = new List<string>();
 
        public double Price { get; set; }
    }
}
