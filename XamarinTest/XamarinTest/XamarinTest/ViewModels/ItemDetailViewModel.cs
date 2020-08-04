
using XamarinTest.Models;

namespace XamarinTest.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Hobby Item { get; set; }
        public ItemDetailViewModel(Hobby item = null)
        {
            Title = item?.Text;
            Item = item;
        }
    }
}
