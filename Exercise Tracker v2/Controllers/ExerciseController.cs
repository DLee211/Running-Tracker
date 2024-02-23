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
    public IActionResult AddExercise([FromForm]Exercise exercise)
    {
        if (exercise.DateEnd < exercise.DateStart)
            throw new InvalidOperationException("DateEnd cannot be before DateStart.");
        if (exercise.Duration < 0 || exercise.Duration > 24)
            throw new InvalidOperationException("Duration cannot be less than 0 or greater than 24.");
        
        if (ModelState.IsValid)
        {
            exerciseService.AddExercise(exercise);
            return RedirectToAction("Index");
        }

        return View("Create", exercise);
    }

    
    [HttpPost]
    [Route("UpdateExercise")]
    public IActionResult UpdateExercise([FromForm]Exercise exercise)
    {  
        if (exercise.DateEnd < exercise.DateStart)
            throw new InvalidOperationException("DateEnd cannot be before DateStart.");
        if (exercise.Duration < 0 || exercise.Duration > 24)
            throw new InvalidOperationException("Duration cannot be less than 0 or greater than 24.");
        
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