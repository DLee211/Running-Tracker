using Exercise_Tracker_v2.Models;
using Exercise_Tracker.Service;
using Microsoft.AspNetCore.Mvc;

namespace Exercise_Tracker_v2.Controllers;
[ApiController]
[Route("Exercise")]
public class ExerciseController(ExerciseService exerciseService) : Controller
{
    [HttpGet]
    [Route("Index")]
    public IActionResult Index()
    {
        var exercises = exerciseService.GetAllExercises();
        return View(exercises);
    }

    [HttpGet]
    [Route("Create")]
    public IActionResult Create()
    {
        ViewData["Title"] = "Create";
        var exercise = new Exercise();
        return View(exercise);
    }
    
    [HttpGet]
    [Route("Edit/{id}")]
    public IActionResult Edit(int id)
    {
        var exercise = exerciseService.GetExerciseById(id);
        return View(exercise);
    }
    
    [HttpPost]
    [Route("AddExercise")]
    public IActionResult AddExercise([FromForm]Exercise model)
    {
        if (ModelState.IsValid)
        {
            exerciseService.AddExercise(model);
            return RedirectToAction("Index");
        }

        return View("Create", model);
    }

    
    [HttpPost]
    [Route("UpdateExercise")]
    public IActionResult UpdateExercise([FromForm]Exercise exercise)
    {  
        if (ModelState.IsValid)
        {
            exerciseService.UpdateExercise(exercise.Id, exercise);
            return RedirectToAction("Index");
        }
        return View("Edit", exercise);
    }

    [HttpPost]
    [Route("DeleteExercise/{id}")]
    public IActionResult DeleteExercise(int id)
    {
        exerciseService.DeleteExercise(id);
        return RedirectToAction("Index");
    }
}