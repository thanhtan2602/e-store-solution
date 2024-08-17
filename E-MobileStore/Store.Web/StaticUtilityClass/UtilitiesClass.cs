namespace Store.Web.StaticUtilityClass
{
    public static class UtilitiesClass
    {
        public static string DisplayTime(DateTime createdDate)
        {
            var time = DateTime.Now - createdDate;
            if(time.TotalDays > 1)
            {
                return $"{((int)time.TotalDays)} ngày trước";
            }
            else if(time.TotalHours > 1)
            {
                return $"{((int)time.TotalHours)} giờ trước";
            }
            else if (((int)time.TotalMinutes) > 1)
            {
                return $"{((int)time.TotalMinutes)} giờ trước";
            }
            else
            {
                return "Vừa xong";
            }
        }
    }
}
