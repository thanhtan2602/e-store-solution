using Microsoft.AspNetCore.Html;
using Microsoft.Extensions.Hosting;
using Store.Domain.Entities;

namespace Store.Web.Utility
{
    public static class StatusUltilities
    {
        public static HtmlString GetStatusElement(bool IsActive, bool IsDeleted)
        {
            if (IsActive && !IsDeleted)
            {
                return new HtmlString("<button class='pd-setting'>Hoạt động</button>");
            }
            else if (!IsActive)
            {
                return new HtmlString("<button class='ps-setting'>Tạm dừng</button>");
            }
            else
            {
                return new HtmlString("<button class='ds-setting'> Đã xóa </ button>");
            }
        }
    }
}
