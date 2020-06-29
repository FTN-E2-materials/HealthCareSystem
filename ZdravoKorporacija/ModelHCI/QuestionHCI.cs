using Model.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZdravoKorporacija.ModelHCI
{
    public class QuestionHCI
    {

        public string title { get; set; }

        public string content { get; set; }
        public Patient patient { get; set; }
        public string questionReply { get; set; }
        public Question question { get; set; }
        public bool FAQ { get; set; }
        public QuestionHCI() { }
    }
}
