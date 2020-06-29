using Controller.GeneralController;
using Microsoft.Win32;
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

namespace ZdravoKorporacija.Pages.BlogPages
{
    /// <summary>
    /// Interaction logic for AddArticle.xaml
    /// </summary>
    public partial class AddArticle : Page
    {
        public string imagePath = "";
        public ArticleController articleController;

        public AddArticle()
        {
            var app = Application.Current as App;
            articleController = app.articleController;

            InitializeComponent();
            List<String> categories = new List<String>();
            MyEvents.CloseMessageBoxEventHandler.CustomEvent += CloseMessageBox;

            ErrorMessage.Visibility = Visibility.Collapsed;


        }

        private void CloseMessageBox(object sender, EventArgs e)
        {
            if (Pages.MessageBox.isOkay)
            {
                MessageBox.Children.Clear();
                Content.Text = "";
                Title.Text = "";
                ImagePath.Text = "";
                MyEvents.CloseArticleEventHandler.RaiseMyCustomEvent(this, new MyEvents.CloseAddArticle());
            }
            else
            {
                MessageBox.Children.Clear();

            }
        }

        public void ClosePage()
        {

            NavigationService.Navigate(new Uri("/Pages/BlogPages/Blogic.xaml", UriKind.Relative));
        }

        private void Publish_Click(object sender, RoutedEventArgs e)
        {
            if (Title.Text.Equals("") || Content.Text.Equals(""))
            {
                ErrorMessage.Visibility = Visibility.Visible;

            } else
            {
                Article article = new Article();
                article.PostContent.ContentTitle = Title.Text;
                article.PostContent.Content = Content.Text;
                article.Author = MainWindow.doctor;
                article.Date = DateTime.Today;


                string relativePath = "";
                string[] parts = imagePath.Split("\\");
                
                for (int i = 0; i < parts.Length; i++)
                {
                    if (parts[i].Equals("Resources"))
                    {
                        relativePath = "/" + parts[i] + "/" + parts[i + 1];
                    }
                }
                article.Image = relativePath;

                articleController.AddArticle(article);

                ClosePage();
            }

            
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {

            if ((Content.Text.Equals("") || Title.Text.Equals("")))
            {
                MessageBox.Children.Clear();
                MessageBox.Children.Add(new MessageBox());
            }
            else
            {
                ClosePage();
            }
        }

        private void Upload_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            if (openFileDialog.ShowDialog() == true)
            {
                ImagePath.Text = openFileDialog.FileName;
                imagePath = openFileDialog.FileName;
            }
        }
    }
}
