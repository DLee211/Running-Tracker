using Exercise_Tracker;
using Exercise_Tracker_v2.Models;
using Exercise_Tracker.Service;
using Microsoft.AspNetCore.Mvc;

namespace Exercise_Tracker_v2.Controllers;
[ApiController]
[Route("api/[controller]/")]
public class ExerciseController : ControllerBase
{
    private readonly ExerciseService _exerciseService;

    public ExerciseController(ExerciseService exerciseService)
    {
        _exerciseService = exerciseService;
    }

    [HttpPost]
    [Route("Create")]
    public IActionResult AddExercise(Exercise exercise)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        _exerciseService.AddExercise(exercise);
        return Ok();
    }

    [HttpPut]
    [Route("UpdateExercise/{id}")]
    public IActionResult UpdateExercise(int id, Exercise updatedExercise)
    {  
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        _exerciseService.UpdateExercise(id, updatedExercise);
        return Ok();
    }

    [HttpDelete]
    [Route("DeleteExercise/{id}")]
    public IActionResult DeleteExercise(int id)
    {
        _exerciseService.DeleteExercise(id);
        return Ok();
    }
}