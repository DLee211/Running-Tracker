using Exercise_Tracker_v2.Models;

namespace Exercise_Tracker.Service;

public class ExerciseService
{
    private readonly IExerciseRepository<Exercise> _exerciseRepository;
    
    public ExerciseService(IExerciseRepository<Exercise> exerciseRepository)
    {
        _exerciseRepository = exerciseRepository ?? throw new ArgumentNullException(nameof(exerciseRepository));
    }

    public void AddExercise(Exercise exercise)
    {
        if (exercise == null)
            throw new ArgumentNullException(nameof(exercise));

        _exerciseRepository.Add(exercise);
    }
    
    public void UpdateExercise(Exercise exercise)
    {
        if (exercise == null)
            throw new ArgumentNullException(nameof(exercise));

        _exerciseRepository.Add(exercise);
    }
    
    public void DeleteExercise(int id)
    {
        var exerciseToDelete = _exerciseRepository.GetById(id);

        if (exerciseToDelete == null)
            throw new ArgumentException($"Exercise with ID {id} not found.");

        _exerciseRepository.Delete(exerciseToDelete);
    }
    
    public Exercise GetExerciseById(int id)
    {
        var exercise = _exerciseRepository.GetById(id);

        if (exercise == null)
            throw new ArgumentException($"Exercise with ID {id} not found.");

        return exercise;
    }

    public IEnumerable<Exercise> GetAllExercises()
    {
        return _exerciseRepository.GetAll();
    }
}