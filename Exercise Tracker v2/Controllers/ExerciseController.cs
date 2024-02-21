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

    [HttpPut]
    [Route("UpdateExercise/{id}")]
    public IActionResult UpdateExercise(int id, Exercise updatedExercise)
    {  
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        exerciseService.UpdateExercise(id, updatedExercise);
        return Ok();
    }

    [HttpDelete]
    [Route("DeleteExercise/{id}")]
    public IActionResult DeleteExercise(int id)
    {
        exerciseService.DeleteExercise(id);
        return Ok();
    }
}