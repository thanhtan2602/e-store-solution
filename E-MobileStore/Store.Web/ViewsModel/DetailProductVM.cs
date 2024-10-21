using Store.WebService.ViewModels;

namespace Store.Web.ViewsModel
{
    public class DetailProductVM
    {
        public IEnumerable<vmProduct>? SuggestProduct { get; set; }
        public vmProduct? Product { get; set; }
    }
}
