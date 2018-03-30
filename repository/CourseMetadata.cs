using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace repository
{
    public class CourseMetadata
    {
        [Key]
        public int course_id { get; set; }
    }

    [MetadataType(typeof(CourseMetadata))]
    public partial class course
    {
    }
}
