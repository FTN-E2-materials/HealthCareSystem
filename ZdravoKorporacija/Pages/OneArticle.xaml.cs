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

namespace ZdravoKorporacija.Pages
{
    /// <summary>
    /// Interaction logic for OneArticle.xaml
    /// </summary>
    public partial class OneArticle : Page
    {
        public OneArticle()
        {
            InitializeComponent();
            MyEvents.ShowOneArticleEventHandler.CustomEvent += showOne;
            
            content.Text = ProfileAndNotification.Profile.article.content;
            image.Source = ProfileAndNotification.Profile.article.articlePic;
            tittle.Text = ProfileAndNotification.Profile.article.title;
        }

        private void showOne(object sender, EventArgs e)
        {
            if (e is MyEvents.ShowOneEventArgs)
            {
                var article = ((MyEvents.ShowOneEventArgs)e).article;
                content.Text = article.content;
                image.Source = article.articlePic;
                tittle.Text = article.title;
            }
        }

        private void Close_Add_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Pages/ProfileAndNotification/Profile.xaml", UriKind.Relative));
        }
    }
}
