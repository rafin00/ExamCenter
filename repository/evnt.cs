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
    
    public partial class evnt
    {
        public evnt()
        {
            this.Answers = new HashSet<Answer>();
            this.exams = new HashSet<exam>();
            this.registrations = new HashSet<registration>();
            this.results = new HashSet<result>();
        }
    
        public int evnt_id { get; set; }
        public string evnt_name { get; set; }
        public Nullable<System.DateTime> evnt_sdt { get; set; }
        public Nullable<System.DateTime> evnt_edt { get; set; }
    
        public virtual ICollection<Answer> Answers { get; set; }
        public virtual ICollection<exam> exams { get; set; }
        public virtual ICollection<registration> registrations { get; set; }
        public virtual ICollection<result> results { get; set; }
    }
}
