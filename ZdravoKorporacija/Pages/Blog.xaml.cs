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
using ZdravoKorporacija.Pages.Schedules;

namespace ZdravoKorporacija.Pages
{
    /// <summary>
    /// Interaction logic for Blog.xaml
    /// </summary>
    /// 


    public partial class Blogile : Page
    {
        public Blogile()
        {
            InitializeComponent();
            MyEvents.ShowLanguage.CustomEvent += showLanguage;
            MyEvents.CloseLanguage.CustomEvent += closeLanguage;

            MyEvents.CloseArticleEventHandler.CustomEvent += CloseAddArticle;
            Content.Navigate(new Uri("/Pages/BlogPages/Blogic.xaml", UriKind.Relative));

        }

        private void closeLanguage(object sender, EventArgs e)
        {
            Language.Children.Clear();
        }

        private void showLanguage(object sender, EventArgs e)
        {
            Language.Children.Clear();
            Language.Children.Add(new Language());
        }

        private void CloseAddArticle(object sender, EventArgs e)
        {

            Content.Navigate(new Uri("/Pages/BlogPages/Blogic.xaml", UriKind.Relative));
        }

        private void Blog_Click(object sender, RoutedEventArgs e)
        {
            Content.Navigate(new Uri("/Pages/BlogPages/Blogic.xaml", UriKind.Relative));
        }

        private void AddArticle_Click(object sender, RoutedEventArgs e)
        {
            Content.Navigate(new Uri("/Pages/BlogPages/AddArticle.xaml", UriKind.Relative));
        }

        private void Questions_Click(object sender, RoutedEventArgs e)
        {
            Content.Navigate(new Uri("/Pages/BlogPages/Questions.xaml", UriKind.Relative));
        }

        private void FAQ_Click(object sender, RoutedEventArgs e)
        {
            Content.Navigate(new Uri("/Pages/BlogPages/FAQ.xaml", UriKind.Relative));
        }
    }
}
