using System;
using Deneme6.Models;

namespace Deneme6.Services
{
    public interface INoteAction
    {
        void AddNote(Note note);
        List<Note> ListNotes(int userId);
        void UpdateNote(Note note);
        void DeleteNote(int noteId);
    }
}

