﻿using BulkBookWeb.Data;
using BulkBookWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkBookWeb.Controllers;

public class CategoryController : Controller
{
    private readonly ApplicationDbContext _db;

    public CategoryController(ApplicationDbContext db)
    {
        _db = db;        
    }
    public IActionResult Index()
    { 
        IEnumerable<Category> objCategoryList = _db.categories;
        return View(objCategoryList); 
    }

    //Get action method
    public IActionResult Create()
    {
        return View();
    }
    //Post action method
    [HttpPost] 
    [ValidateAntiForgeryToken]
    public IActionResult Create(Category obj)
    {
        if (obj.Name == obj.DisplayOrder.ToString())
        {
            ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name");
        }
        if (ModelState.IsValid)
        {
            _db.categories.Add(obj);
            _db.SaveChanges();
            TempData["success"] = "Category created succesfully";
            return RedirectToAction("Index");
        }
        return View(obj);
    }


    //Get action method
    public IActionResult Edit(int? id)
    {
        if (id==null || id ==0)
        {
            return NotFound();
        }
        var categoryFromDb = _db.categories.Find(id);
        //var categoryFromDbFirst = _db.categories.FirstOrDefault(u=> u.Id==id)
        if (categoryFromDb == null)
        {
            return NotFound();
        }
        return View(categoryFromDb);
    }
    //Post action method
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Category obj)
    {
        if (obj.Name == obj.DisplayOrder.ToString())
        {
            ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name");
        }
        if (ModelState.IsValid)
        {
            _db.categories.Update(obj);
            _db.SaveChanges();
            TempData["success"] = "Category updated succesfully";
            return RedirectToAction("Index");
        }
        return View(obj);
    }


    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        var categoryFromDb = _db.categories.Find(id);
        //var categoryFromDbFirst = _db.categories.FirstOrDefault(u=> u.Id==id)
        if (categoryFromDb == null)
        {
            return NotFound();
        }
        return View(categoryFromDb);
    }
    //Post action method
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DeletePost(int? id)
    { 
        var obj =  _db.categories.Find(id);
        if (obj == null)
        {
         return NotFound();
        }
        _db.categories.Remove(obj);
        _db.SaveChanges();
        TempData["success"] = "Category deleted succesfully";
        return RedirectToAction("Index");
        
    }
}
