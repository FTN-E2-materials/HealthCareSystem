using Model.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace ZdravoKorporacija.ModelHCI
{
    public class ArticleHCI
    {
        public string title { get; set; }
        
        public string content { get; set; }
        public ImageSource articlePic { get; set; }

        public Article article { get; set; }
        public string category { get; set; }
        public ArticleHCI() { }
    }

    
}
