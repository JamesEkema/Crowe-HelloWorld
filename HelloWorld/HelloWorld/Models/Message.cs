using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HelloWorld.Models
{
    public class Message
    {
        [Key]
        public int MessageID { get; set; }
        public string Text { get; set; }
    }
}
