using Podcast.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Podcast.Controllers
{
  public class TitlesController : Controller
  {
    private readonly ToDoListContext _db;

    public TitlesController(ToDoListContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      return View(_db.Titles.ToList());
    }

    public ActionResult Create()
    {
      ViewBag.CategoryId = new SelectList(_db.Categories, "CategoryId", "Name");
      return View();
    }

    [HttpPost]
    public ActionResult Create(Title title, int CategoryId)
    {
      _db.Titles.Add(title);
      _db.SaveChanges();
      if (CategoryId != 0)
      {
        _db.CategoryTitle.Add(new CategoryTitle() { CategoryId = CategoryId, TitleId = title.TitleId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var thisTitle = _db.Titles
          .Include(title => title.JoinEntities)
          .ThenInclude(join => join.Category)
          .FirstOrDefault(title => title.TitleId == id);
      return View(thisTitle);
    }

    public ActionResult Edit(int id)
    {
      var thisTitle = _db.Titles.FirstOrDefault(title => title.TitleId == id);
      ViewBag.CategoryId = new SelectList(_db.Categories, "CategoryId", "Name");
      return View(thisTitle);
    }

    [HttpPost]
    public ActionResult Edit(Title title, int CategoryId)
    {
      if (CategoryId != 0)
      {
        _db.CategoryTitle.Add(new CategoryTitle() { CategoryId = CategoryId, TitleId = title.TitleId });
      }
      _db.Entry(title).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddCategory(int id)
    {
      var thisTitle = _db.Titles.FirstOrDefault(title => title.TitleId == id);
      ViewBag.CategoryId = new SelectList(_db.Categories, "CategoryId", "Name");
      return View(thisTitle);
    }

    [HttpPost]
    public ActionResult AddCategory(Title title, int CategoryId)
    {
      if (CategoryId != 0)
      {
      _db.CategoryTitle.Add(new CategoryTitle() { CategoryId = CategoryId, TitleId = title.TitleId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      var thisTitle = _db.Titles.FirstOrDefault(title => title.TitleId == id);
      return View(thisTitle);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisTitle = _db.Titles.FirstOrDefault(title => title.TitleId == id);
      _db.Titles.Remove(thisTitle);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult DeleteCategory(int joinId)
    {
      var joinEntry = _db.CategoryTitle.FirstOrDefault(entry => entry.CategoryTitleId == joinId);
      _db.CategoryTitle.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}