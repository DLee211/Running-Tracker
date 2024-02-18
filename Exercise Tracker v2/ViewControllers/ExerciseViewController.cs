using Exercise_Tracker_v2.Models;
using Exercise_Tracker.Service;
using Microsoft.AspNetCore.Mvc;

namespace Exercise_Tracker_v2.Controllers;

public class ExerciseViewController : Controller
{
    private readonly ExerciseService _exerciseService;

    public ExerciseViewController(ExerciseService exerciseService)
    {
        _exerciseService = exerciseService;
    }

    public IActionResult Index()
    {
        var exercises = _exerciseService.GetAllExercises(); // Retrieve the list of exercises from the repository
        return View(exercises); // Return the view with the list of exercises
    }
}