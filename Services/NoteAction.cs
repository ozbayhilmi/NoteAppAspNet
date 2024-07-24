using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Deneme6.Models;

namespace Deneme6.Services
{
    public class NoteAction : INoteAction
    {
        private readonly string _noteFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "NoteApp", "notes.txt");

        public void AddNote(Note note)
        {
            note.NoteId = GetNewNoteId();
            var noteData = $"{note.NoteId}#{note.UserId}#{note.NoteText}#{note.NoteDate}";
            File.AppendAllText(_noteFilePath, noteData + Environment.NewLine);
        }

        public List<Note> ListNotes(int userId)
        {
            if (!File.Exists(_noteFilePath))
            {
                return new List<Note>();
            }

            var notes = File.ReadAllLines(_noteFilePath)
                .Select(noteLine =>
                {
                    var parts = noteLine.Split('#');
                    if (parts.Length == 4)
                    {
                        return new Note
                        {
                            NoteId = int.Parse(parts[0]),
                            UserId = int.Parse(parts[1]),
                            NoteText = parts[2],
                            NoteDate = DateTime.Parse(parts[3])
                        };
                    }
                    else
                    {
                        return null;
                    }
                })
                .Where(note => note != null && note.UserId == userId)
                .ToList();

            return notes;
        }

        public void UpdateNote(Note note)
        {
            if (!File.Exists(_noteFilePath))
            {
                return;
            }

            var notes = File.ReadAllLines(_noteFilePath).ToList();
            bool noteUpdated = false;

            for (int i = 0; i < notes.Count; i++)
            {
                var noteProps = notes[i].Split('#');
                if (noteProps.Length == 4 && int.Parse(noteProps[0]) == note.NoteId)
                {
                   
                    notes[i] = $"{note.NoteId}#{noteProps[1]}#{note.NoteText}#{note.NoteDate}"; 
                    noteUpdated = true;
                    break;
                }
            }

            if (!noteUpdated)
            { 
                return;
            }

            File.WriteAllLines(_noteFilePath, notes);
        }




        public void DeleteNote(int noteId)
        {
            if (!File.Exists(_noteFilePath))
            {
                return;
            }

            var notes = File.ReadAllLines(_noteFilePath).ToList();
            notes.RemoveAll(noteLine =>
            {
                var parts = noteLine.Split('#');
                return parts.Length == 4 && int.TryParse(parts[0], out int id) && id == noteId;
            });
            File.WriteAllLines(_noteFilePath, notes);
        }

        private int GetNewNoteId()
        {
            if (!File.Exists(_noteFilePath) || File.ReadAllLines(_noteFilePath).Length == 0)
                return 1;

            var notes = File.ReadAllLines(_noteFilePath)
                            .Where(n => !string.IsNullOrWhiteSpace(n) && n.Split('#').Length == 4)
                            .Select(n => int.Parse(n.Split('#')[0]))
                            .ToList();

            if (notes.Count == 0)
                return 1;

            return notes.Max() + 1;
        }
    }
}
