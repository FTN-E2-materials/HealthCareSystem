using Controller.GeneralController;
using MaterialDesignThemes.Wpf;
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
    /// Interaction logic for AddReply.xaml
    /// </summary>
    public partial class AddReply : Page
    {
        public QuestionController questionController;

        public AddReply()
        {
            var app = Application.Current as App;
            questionController = app.questionController;

            InitializeComponent();
            MyEvents.QuestionSelectedHandler.CustomEvent += SelectionChanged;
            MyEvents.CloseMessageBoxEventHandler.CustomEvent += CloseMessageBox;
            if (Questions.selectedQuestion != null)
            {
                title.Text = Questions.selectedQuestion.title + "\nPacijent: " + Questions.selectedQuestion.question.Author.Name + " " + Questions.selectedQuestion.question.Author.Surname;
                Content.Text = Questions.selectedQuestion.content;

                if (Questions.selectedQuestion.questionReply != null)
                {
                    AddAnswer.Visibility = Visibility.Collapsed;
                    AnswerExists.Visibility = Visibility.Visible;
                    ExistentReply.Text = Questions.selectedQuestion.questionReply;
                }


            }
        }

        public void CloseMessageBox(object sender, EventArgs e)
        {
            if (Pages.MessageBox.isOkay)
            {
                MessageBox.Children.Clear();
                Reply.Text = "";
                Replies.NavigationService.Navigate(new Uri("/Pages/BlogPages/Questions.xaml", UriKind.Relative));
            } else
            {
                MessageBox.Children.Clear();

            }
        }

        private void SelectionChanged(object sender, EventArgs e)
        {
            title.Text = Questions.selectedQuestion.title + ", Pacijent: " + Questions.selectedQuestion.question.Author.Name + " " + Questions.selectedQuestion.question.Author.Surname;
            Content.Text = Questions.selectedQuestion.content;
           
        }
        private void AddReply_Click(object sender, RoutedEventArgs e)
        {
            Replies.Navigate(new Uri("/Pages/BlogPages/Questions.xaml", UriKind.Relative));
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {

            if (!Reply.Text.Equals(""))
            {
                MessageBox.Children.Clear();
                MessageBox.Children.Add(new MessageBox());
            }
            else
            {
                Replies.Navigate(new Uri("/Pages/BlogPages/Questions.xaml", UriKind.Relative));
            }
        }


        private void Close_Click(object sender, RoutedEventArgs e)
        {
            if (!Reply.Text.Equals(""))
            {
                MessageBox.Children.Clear();
                MessageBox.Children.Add(new MessageBox());
            }
            else
            {
                Replies.Navigate(new Uri("/Pages/BlogPages/Questions.xaml", UriKind.Relative));
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (Reply != null && MoveToFAQ != null)
            {
                if ((bool) MoveToFAQ.IsChecked)
                {
                    Questions.selectedQuestion.question.FrequentlyAsked = true;
                    QuestionReply questionReply = new QuestionReply();
                    questionReply.Author = MainWindow.doctor;
                    questionReply.Content = Reply.Text;
                    questionReply.Date = DateTime.Today;
                    questionController.AnswerQuestion(Questions.selectedQuestion.question, questionReply);
                } else
                {
                    Questions.selectedQuestion.question.FrequentlyAsked = false;
                    QuestionReply questionReply = new QuestionReply();
                    questionReply.Author = MainWindow.doctor;
                    questionReply.Content = Reply.Text;
                    questionReply.Date = DateTime.Today;
                    questionController.AnswerQuestion(Questions.selectedQuestion.question, questionReply);
                }
                Replies.Navigate(new Uri("/Pages/BlogPages/Questions.xaml", UriKind.Relative));
            }
        }
    }
}
