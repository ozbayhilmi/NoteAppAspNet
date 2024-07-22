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

            var notes = new List<Note>();
            string[] lines = File.ReadAllLines(_noteFilePath);

            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                string[] parts = line.Split('#');
                if (parts.Length == 4)
                {
                    int noteUserId;
                    int noteId;
                    DateTime noteDate;

                    if (int.TryParse(parts[0], out noteId) &&
                        int.TryParse(parts[1], out noteUserId) &&
                        DateTime.TryParse(parts[3], out noteDate))
                    {
                        if (noteUserId == userId)
                        {
                            var note = new Note
                            {
                                NoteId = noteId,
                                UserId = noteUserId,
                                NoteText = parts[2],
                                NoteDate = noteDate
                            };

                            notes.Add(note);
                        }
                    }
                }
            }

            return notes;
        }


        public void UpdateNote(Note note)
        {
            var notes = File.ReadAllLines(_noteFilePath).ToList();
            for (int i = 0; i < notes.Count; i++)
            {
                var noteProps = notes[i].Split('#');
                if (int.Parse(noteProps[0]) == note.NoteId)
                {
                    notes[i] = $"{note.NoteId}#{note.UserId}#{note.NoteText}#{note.NoteDate}";
                    break;
                }
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

            int maxId = 0;
            string[] lines = File.ReadAllLines(_noteFilePath);

            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                string[] parts = line.Split('#');
                if (parts.Length == 4 && int.TryParse(parts[0], out int noteId))
                {
                    if (noteId > maxId)
                    {
                        maxId = noteId;
                    }
                }
            }

            return maxId + 1;
        }

    }
}
