using Microsoft.AspNetCore.Html;

namespace Store.Web.Utility
{
    public static class ProductsUtilities
    {
        public static HtmlString ProductStatus(int quantity)
        {
            if (quantity > 50)
            {
                return new HtmlString("<p class='text-light t-4'>Trạng thái : Còn hàng</p>");
            }
            else if (quantity > 0)
            {
                return new HtmlString("<p class='text-warning t-4'>Trạng thái : Lượng hàng còn trong kho ít</p>");
            }
            else
            {
                return new HtmlString("<p class='text-danger t-4'>Trạng thái : Hết hàng</p>");
            }
        }
        public static string FormatPrice(decimal price)
        {
            var formatPrice = price.ToString("N0", new System.Globalization.CultureInfo("vi-VN"));
            return formatPrice;
        }
        public static string FormatToDMY(DateTime time)
        {
            var formatTime = time.ToLocalTime().ToString("dd/MM/yyyy");
            return formatTime;
        }
        public static string FormatToToD(DateTime time)
        {
            var formatTime = time.ToLocalTime().ToString("HH:mm");
            return formatTime;
        }

    }
}
