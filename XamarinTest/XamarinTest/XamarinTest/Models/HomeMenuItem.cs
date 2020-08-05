namespace XamarinTest.Models
{
    public enum MenuItemType
    {
        Browse,
        About,
        Science,
        Engineering,
        Technology,
        Math,
        Art,
        Sports,
        LogOut
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }

        public string ImagePath { get; set; }
    }
}
