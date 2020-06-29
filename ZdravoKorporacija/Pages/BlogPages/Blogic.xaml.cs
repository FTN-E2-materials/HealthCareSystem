using Controller.GeneralController;
using Model.Users;
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

namespace ZdravoKorporacija.Pages.Blog
{
    /// <summary>
    /// Interaction logic for Blogic.xaml
    /// </summary>
    ///     public class ArticleShow
    public class ArticleShow
    {
        public ModelHCI.ArticleHCI article { get; set; }
        public string buttonContent { get; set; }
        public ButtonTagInfo info { get; set; }
    }
    public class ButtonTagInfo
    {
        public ModelHCI.ArticleHCI article { get; set; }
    }
public partial class Blogic : Page
    {
        public static ModelHCI.ArticleHCI article;
        public static int currentPage = 1;
        public static int pageCount;
        public static List<ArticleShow> articlesToShow;

        public ArticleController articleController;

        public Blogic()
        {
            var app = Application.Current as App;
            articleController = app.articleController;

            InitializeComponent();
            if (ArticlesList != null)
            {
                ArticlesList.ItemsSource = new List<ArticleShow>();
                articlesToShow = new List<ArticleShow>();
                foreach (Article art in articleController.GetAllArticles())
                {
                    articlesToShow.Add(new ArticleShow() { article = new ModelHCI.ArticleHCI() {  article = art, content = art.PostContent.Content, title = art.PostContent.ContentTitle, articlePic = new BitmapImage(new Uri(art.Image, UriKind.Relative)) }, 
                        buttonContent = "Prikaži ceo", 
                        info = new ButtonTagInfo() { article = new ModelHCI.ArticleHCI() { article = art, content = art.PostContent.Content, title = art.PostContent.ContentTitle, articlePic = new BitmapImage(new Uri(art.Image, UriKind.RelativeOrAbsolute)) } } });
                    pageCount = articlesToShow.Count;
                    ArticlesList.ItemsSource = articlesToShow;
                    
                }
                //Pagination();
            }
        }

        private void Pagination()
        {
            List<ArticleShow> pageList = new List<ArticleShow>();

            for (int i = currentPage - 1; i <= currentPage; i++)
            {
                pageList.Add(articlesToShow[i]);
            }

            ArticlesList.ItemsSource = new List<ArticleShow>();
            ArticlesList.ItemsSource = pageList;
        }

        private void ScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {

        }

        private void ShowAll_Click(object sender, RoutedEventArgs e)
        {
            Button thisButton = (Button)sender;
            ButtonTagInfo info = (ButtonTagInfo)(thisButton.Tag);
            ShowArticle(info);
        }

        private void ShowArticle(ButtonTagInfo info)
        {
            article = info.article;
            NavigationService.Navigate(new Uri("/Pages/BlogPages/OneArticle.xaml", UriKind.Relative));
        }
    }
}
