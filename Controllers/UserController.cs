using Deneme6.Models;
using Deneme6.Services;
using Microsoft.AspNetCore.Mvc;

public class UserController : Controller
{
    private INoteAction _noteAction;
    private IUserAction _userAction;

    public UserController(INoteAction noteAction, IUserAction userAction)
    {
        _noteAction = noteAction;
        _userAction = userAction;
    }

    public IActionResult UserMenu()
    {
        return View();
    }

    public IActionResult AddNote()
    {
        return View();
    }

    [HttpPost]
    public IActionResult AddNote(Note note)
    {
        note.UserId = HttpContext.Session.GetInt32("UserId").Value;
        note.NoteDate = DateTime.Now;
        _noteAction.AddNote(note);
        return RedirectToAction("UserMenu");
    }

    public IActionResult ListNotes()
    {
        var userId = HttpContext.Session.GetInt32("UserId").Value;
        var notes = _noteAction.ListNotes(userId);
        return View(notes);
    }

    public IActionResult EditNote(int noteId)
    {
        var note = _noteAction.ListNotes(HttpContext.Session.GetInt32("UserId").Value).FirstOrDefault(n => n.NoteId == noteId);
        return View(note);
    }

    [HttpPost]
    public IActionResult EditNote(Note note)
    {
        _noteAction.UpdateNote(note);
        return RedirectToAction("ListNotes");
    }

    public IActionResult DeleteNoteConfirmation(int noteId)
    {
        var note = _noteAction.ListNotes(HttpContext.Session.GetInt32("UserId").Value).FirstOrDefault(n => n.NoteId == noteId);
        return View(note);
    }

    [HttpPost]
    public IActionResult DeleteNoteConfirmed(int noteId)
    {
        _noteAction.DeleteNote(noteId);
        return RedirectToAction("ListNotes");
    }

    public IActionResult UpdateUser()
    {
        var userId = HttpContext.Session.GetInt32("UserId").Value;
        var user = _userAction.ListUsers().FirstOrDefault(u => u.UserId == userId);
        return View(user);
    }

    [HttpPost]
    public IActionResult UpdateUser(User user)
    {
        _userAction.UpdateUser(user);
        return RedirectToAction("UserMenu");
    }
}
