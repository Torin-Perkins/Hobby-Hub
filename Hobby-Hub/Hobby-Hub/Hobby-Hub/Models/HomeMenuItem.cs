using System;
using System.Collections.Generic;
using System.Text;

namespace Hobby_Hub.Models
{ //enum for available categories
    public enum MenuItemType
    {
        Home,
        About,
        Science, 
        Engineering, 
        Technology, 
        Math, 
        Art, 
        Sports
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}