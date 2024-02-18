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
    
    public void  UpdateExercise(int id, Exercise updatedExercise)
    {
        var existingExercise = _exerciseRepository.GetById(id);

        // Update the existing exercise with the new values
        existingExercise.DateStart = updatedExercise.DateStart;
        existingExercise.DateEnd = updatedExercise.DateEnd;
        existingExercise.Duration = updatedExercise.Duration;
        existingExercise.Comments = updatedExercise.Comments;

        _exerciseRepository.Add(existingExercise);
    }
    
    public void DeleteExercise(int id)
    {
        var exerciseToDelete = _exerciseRepository.GetById(id);

        _exerciseRepository.Delete(exerciseToDelete);
    }
    
    public Exercise GetExerciseById(int id)
    {
        var exercise = _exerciseRepository.GetById(id);
        return exercise;
    }

    public IEnumerable<Exercise> GetAllExercises()
    {
        return _exerciseRepository.GetAll();
    }
}