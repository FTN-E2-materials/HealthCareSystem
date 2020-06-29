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

namespace ZdravoKorporacija.Pages.BlogPages
{
    /// <summary>
    /// Interaction logic for FAQ.xaml
    /// </summary>
    public partial class FAQ : Page
    {
        public QuestionController questionController;

        public FAQ()
        {
            var app = Application.Current as App;
            questionController = app.questionController;

            InitializeComponent();

            SetItemsList();

        }
        public void SetItemsList()
        {
            if (FAQList != null)
            {
                List<ModelHCI.QuestionHCI> listSource = new List<ModelHCI.QuestionHCI>();

                FAQList.ItemsSource = new List<ModelHCI.QuestionHCI>();
                foreach (Question question in questionController.GetFAQ())
                {
                    ModelHCI.QuestionHCI q1 = new ModelHCI.QuestionHCI()
                    {

                        content = question.PostContent.Content,
                        title = question.PostContent.ContentTitle,
                        FAQ = question.FrequentlyAsked,
                        question = question,
                        patient = new ModelHCI.Patient() { FullName = question.Author.Name + " " + question.Author.Surname},
                        questionReply = question.QuestionReply.Content
                    };
                    if (question.FrequentlyAsked)
                    {
                        listSource.Add(q1);
                    }
                }

                if (listSource.Count > 0)
                {
                    FAQList.ItemsSource = listSource;
                }
                else
                {
                    NoQuestions.Visibility = Visibility.Visible;
                }

            }
        }
    }
}
