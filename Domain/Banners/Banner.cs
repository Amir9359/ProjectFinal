using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain.Banners
{
    public class Banner
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Link { get; set; }
        public int Priority { get; set; }
        public bool IsActive { get; set; }
        public BannerPosition Position { get; set; }
    }   

    public enum BannerPosition
    {
        [Display(Name = "سلایدر")]
        slider = 0,
        [Display(Name = "سطر اول")]
        Line_1 = 1 ,
        [Display(Name = "سطر دوم")]
        Line_2 = 2 ,
        [Display(Name = "سطر سوم")]
        Line_3 = 3 ,
        [Display(Name = "سطر چهارم")]
        Line_4 = 4 ,
        [Display(Name = "سطر پنجم")]
        Line_5 = 5 ,
    }
}