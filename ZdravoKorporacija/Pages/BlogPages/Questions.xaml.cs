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
    /// Interaction logic for Questions.xaml
    /// </summary>
    /// 

    public class QuestionsForShowing
    {
        public ModelHCI.QuestionHCI q { get; set; }
        public QuestionInfo tagInfo { get; set; }
    }

    public class QuestionInfo
    {
        public ModelHCI.QuestionHCI q { get; set; }
    }
    public partial class Questions : Page
    {
        public QuestionController questionController;

        public static ModelHCI.QuestionHCI selectedQuestion = null;
        public Questions()
        {
            var app = Application.Current as App;
            questionController = app.questionController;

            InitializeComponent();

            SetItemsList();

        }
        public void SetItemsList()
        {
            if (QuestionList != null) {
                List<QuestionsForShowing> listSource = new List<QuestionsForShowing>();

                QuestionList.ItemsSource = new List<ModelHCI.QuestionHCI>();
                foreach (Question question in questionController.GetAll())
                {
                    if (!question.FrequentlyAsked)
                    {
                        ModelHCI.QuestionHCI q1 = new ModelHCI.QuestionHCI()
                        {

                            content = question.PostContent.Content,
                            title = question.PostContent.ContentTitle,
                            FAQ = question.FrequentlyAsked,
                            question = question,
                            questionReply = question.QuestionReply.Content
                        };
                        listSource.Add(new QuestionsForShowing()
                        {
                            q =q1,
                            tagInfo = new QuestionInfo() { q = q1 }
                        }) ;
                    }
                }

                if (listSource.Count > 0)
                {
                    QuestionList.ItemsSource = listSource;
                } else
                {
                    NoQuestions.Visibility = Visibility.Visible;
                }

             }
        }
        private void QuestionList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


                
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            Button b = (Button)sender;

           selectedQuestion = ((QuestionInfo) b.Tag).q;
           MyEvents.QuestionSelectedHandler.RaiseMyCustomEvent(this, new MyEvents.QuestionSelected());
           NavigationService.Navigate(new Uri("/Pages/BlogPages/AddReply.xaml", UriKind.Relative));
            }
    
    }
}
