using System;
namespace Deneme6.Models
{
    public class Note
    {
        public int NoteId { get; set; }
        public int UserId { get; set; }
        public string NoteText { get; set; }
        public DateTime NoteDate { get; set; }
    }

}

