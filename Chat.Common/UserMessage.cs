using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chat.Common
{
    [Table("UserMessage")]
    public class UserMessage 
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string Name { get; set; }

        public string Message { get; set; }

    }
}
