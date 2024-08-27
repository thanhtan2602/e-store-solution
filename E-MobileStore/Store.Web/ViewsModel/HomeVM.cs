using Store.WebService.ViewModels;

namespace Store.Web.ViewsModel
{
    public class HomeVM
    {
        public IEnumerable<vmCategory> ChosseCate { get; set; }
        public IEnumerable<vmCategory> ProductByCate { get; set; }
        public IEnumerable<vmBanner>  HomeSlider { get; set; }
        public IEnumerable<vmNews> TekZone { get; set; }
        public IEnumerable<vmFlashSale> FlashSale { get; set; }
        public IEnumerable<vmStore> Stores { get; set; }
    }
}
