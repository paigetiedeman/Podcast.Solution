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
    public ActionResult Create(Title item, int CategoryId)
    {
      _db.Titles.Add(item);
      _db.SaveChanges();
      if (CategoryId != 0)
      {
        _db.CategoryTitle.Add(new CategoryTitle() { CategoryId = CategoryId, TitleId = item.TitleId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var thisTitle = _db.Titles
          .Include(item => item.JoinEntities)
          .ThenInclude(join => join.Category)
          .FirstOrDefault(item => item.TitleId == id);
      return View(thisTitle);
    }

    public ActionResult Edit(int id)
    {
      var thisTitle = _db.Titles.FirstOrDefault(item => item.TitleId == id);
      ViewBag.CategoryId = new SelectList(_db.Categories, "CategoryId", "Name");
      return View(thisTitle);
    }

    [HttpPost]
    public ActionResult Edit(Title item, int CategoryId)
    {
      if (CategoryId != 0)
      {
        _db.CategoryTitle.Add(new CategoryTitle() { CategoryId = CategoryId, TitleId = item.TitleId });
      }
      _db.Entry(item).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddCategory(int id)
    {
      var thisTitle = _db.Titles.FirstOrDefault(item => item.TitleId == id);
      ViewBag.CategoryId = new SelectList(_db.Categories, "CategoryId", "Name");
      return View(thisTitle);
    }

    [HttpPost]
    public ActionResult AddCategory(Title item, int CategoryId)
    {
      if (CategoryId != 0)
      {
      _db.CategoryTitle.Add(new CategoryTitle() { CategoryId = CategoryId, TitleId = item.TitleId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      var thisTitle = _db.Titles.FirstOrDefault(item => item.TitleId == id);
      return View(thisTitle);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisTitle = _db.Titles.FirstOrDefault(item => item.TitleId == id);
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