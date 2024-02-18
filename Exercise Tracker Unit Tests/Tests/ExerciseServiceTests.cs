using NSubstitute;
using Exercise_Tracker;
using Exercise_Tracker_v2.Models;
using Exercise_Tracker.Service;

[TestFixture]
public class ExerciseServiceTests
{
    [Test]
    public void AddExercise_ValidExercise_ShouldCallRepositoryAdd()
    {
        // Arrange
        var exerciseRepository = Substitute.For<IExerciseRepository<Exercise>>();
        var exerciseService = new ExerciseService(exerciseRepository);
        var exerciseToAdd = new Exercise
            { Id = 1, DateStart = "2024-02-17", DateEnd = "2024-02-18", Duration = "1:00:00", Comments = "Test 1" };

        // Act
        exerciseService.AddExercise(exerciseToAdd);

        // Assert
        exerciseRepository.Received(1).Add(exerciseToAdd);
    }
    
    [Test]
    public void DeleteExercise_ExistingExerciseId_ShouldCallRepositoryDelete()
    {
        // Arrange
        var exerciseRepository = Substitute.For<IExerciseRepository<Exercise>>();
        var exerciseService = new ExerciseService(exerciseRepository);
        var exerciseIdToDelete = 1;
        var existingExercise = new Exercise { Id = exerciseIdToDelete, DateStart = "2024-02-17", DateEnd = "2024-02-18", Duration = "1:00:00", Comments = "Test 1" };
        exerciseRepository.GetById(exerciseIdToDelete).Returns(existingExercise);

        // Act
        exerciseService.DeleteExercise(exerciseIdToDelete);

        // Assert
        exerciseRepository.Received(1).Delete(existingExercise);
    }
    
    
    [Test]
    public void GetExerciseById_ExistingExerciseId_ShouldReturnExercise()
    {
        // Arrange
        var exerciseRepository = Substitute.For<IExerciseRepository<Exercise>>();
        var exerciseService = new ExerciseService(exerciseRepository);
        var exerciseId = 1;
        var expectedExercise = new Exercise { Id = exerciseId, DateStart = "2024-02-17", DateEnd = "2024-02-18", Duration = "1:00:00", Comments = "Test 1" };
        exerciseRepository.GetById(exerciseId).Returns(expectedExercise);

        // Act
        var result = exerciseService.GetExerciseById(exerciseId);

        // Assert
        Assert.That(result, Is.EqualTo(expectedExercise));
    }
    
    [Test]
    public void GetExerciseById_NonexistentExerciseId_ShouldThrowArgumentException()
    {
        // Arrange
        var exerciseRepository = Substitute.For<IExerciseRepository<Exercise>>();
        var exerciseService = new ExerciseService(exerciseRepository);
        var nonexistentExerciseId = 999; // Assuming this ID does not exist
        exerciseRepository.GetById(nonexistentExerciseId).Returns((Exercise)null);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => exerciseService.GetExerciseById(nonexistentExerciseId));
    }
    
    [Test]
    public void GetAllExercises_ShouldReturnAllExercisesFromRepository()
    {
        // Arrange
        var exerciseRepository = Substitute.For<IExerciseRepository<Exercise>>();
        var exerciseService = new ExerciseService(exerciseRepository);
        var expectedExercises = new List<Exercise>
        {
            new Exercise { Id = 1, DateStart = "2024-02-17", DateEnd = "2024-02-18", Duration = "1:00:00", Comments = "Test 1" },
            new Exercise { Id = 2, DateStart = "2024-02-18", DateEnd = "2024-02-19", Duration = "1:00:00", Comments = "Test 2" },
            new Exercise { Id = 3, DateStart = "2024-02-19", DateEnd = "2024-02-20", Duration = "1:00:00", Comments = "Test 3" }
        };
        exerciseRepository.GetAll().Returns(expectedExercises);

        // Act
        var result = exerciseService.GetAllExercises();

        // Assert
        Assert.That(result, Is.EqualTo(expectedExercises));
    }
}