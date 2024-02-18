using Exercise_Tracker;
using Exercise_Tracker_v2.Models;
using Microsoft.AspNetCore.Mvc;

namespace Exercise_Tracker_v2.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ExerciseController : ControllerBase
{
    private readonly IExerciseRepository<Exercise> _exerciseRepository;

    public ExerciseController(IExerciseRepository<Exercise> exerciseRepository)
    {
        _exerciseRepository = exerciseRepository;
    }

    [HttpPost]
    [Route("AddExercise")]
    public IActionResult AddExercise(Exercise exercise)
    {
        _exerciseRepository.Add(exercise);
        return Ok();
    }
    
    [HttpPut]
    [Route("UpdateExercise/{id}")]
    public IActionResult UpdateExercise(int id, Exercise updatedExercise)
    {
        var existingExercise = _exerciseRepository.GetById(id);
        
        existingExercise.DateStart = updatedExercise.DateStart;
        existingExercise.DateEnd = updatedExercise.DateEnd;
        existingExercise.Duration = updatedExercise.Duration;
        existingExercise.Comments = updatedExercise.Comments;

        _exerciseRepository.Add(existingExercise);
        return Ok();
    }
    
    [HttpDelete]
    [Route("DeleteExercise/{id}")]
    public IActionResult DeleteExercise(int id)
    {
        var existingExercise = _exerciseRepository.GetById(id);

        _exerciseRepository.Delete(existingExercise);
        return Ok();
    }
}