//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace repository
{
    using System;
    using System.Collections.Generic;
    
    public partial class question
    {
        public question()
        {
            this.Answers = new HashSet<Answer>();
            this.exams = new HashSet<exam>();
        }
    
        public int question_id { get; set; }
        public string question_text { get; set; }
        public string option1 { get; set; }
        public string option2 { get; set; }
        public string answer { get; set; }
        public int course_id { get; set; }
    
        public virtual ICollection<Answer> Answers { get; set; }
        public virtual course course { get; set; }
        public virtual ICollection<exam> exams { get; set; }
    }
}