using Exercise_Tracker.Service;
using Microsoft.AspNetCore.Mvc;

namespace Exercise_Tracker_v2.ViewControllers;

public class ExerciseController(ExerciseService exerciseService) : Controller
{
    public IActionResult Index()
    {
        var exercises = exerciseService.GetAllExercises(); // Retrieve the list of exercises from the repository
        return View(exercises); // Return the view with the list of exercises
    }
}