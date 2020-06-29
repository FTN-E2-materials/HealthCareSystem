using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ZdravoKorporacija.Pages.Blog;

namespace ZdravoKorporacija.Pages.BlogPages
{
    /// <summary>
    /// Interaction logic for OneArticle.xaml
    /// </summary>
    public partial class OneArticle : Page
    {
        public OneArticle()
        {
            InitializeComponent();
            ModelHCI.ArticleHCI article = Blogic.article;

            content.Text = article.content;
            image.Source = article.articlePic;
            tittle.Text = article.title;
        }

        private void Close_Add_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/BlogPages/Blogic.xaml", UriKind.Relative));
        }

        private void Grid_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {

        }
    }
}
