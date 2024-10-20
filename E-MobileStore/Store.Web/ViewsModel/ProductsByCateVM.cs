using Store.WebService.ViewModels;

namespace Store.Web.ViewsModel
{
    public class ProductsByCateVM
    {
        public IEnumerable<vmBanner>? Banners { get; set; }
        public IEnumerable<vmProduct>? Products { get; set; }
    }
}
