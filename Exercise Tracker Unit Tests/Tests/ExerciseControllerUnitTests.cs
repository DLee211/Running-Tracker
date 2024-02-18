using Exercise_Tracker_v2.Controllers;
using Exercise_Tracker_v2.Models;
using Exercise_Tracker.Service;
using NSubstitute;
using Microsoft.AspNetCore.Mvc;

namespace Exercise_Tracker.Tests.Controllers
{
    [TestFixture]
    public class ExerciseControllerTests
    {
        private ExerciseController _controller;
        private ExerciseService _mockExerciseService;
        private IExerciseRepository<Exercise> _mockRepository;

        [SetUp]
        public void Setup()
        {
            // Create a mock repository
            _mockRepository = Substitute.For<IExerciseRepository<Exercise>>();

            // Setup mock service with the mock repository
            _mockExerciseService = new ExerciseService(_mockRepository);

            // Create the controller with the mock service
            _controller = new ExerciseController(_mockExerciseService);
        }

        // AddExercise tests
        [Test]
        public void AddExercise_ValidExercise_ShouldReturnOk()
        {
            // Arrange
            var exerciseToAdd = new Exercise { Id = 1, DateStart = "2024-02-17", DateEnd = "2024-02-18", Duration = "1:00:00", Comments = "Test 1" };

            // Act
            var result = _controller.AddExercise(exerciseToAdd) as OkResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public void AddExercise_InvalidExercise_ShouldReturnBadRequest()
        {
            // Arrange
            var invalidExercise = new Exercise { Id = 1, DateStart = null, DateEnd = "2024-02-18", Duration = "1:00:00", Comments = "Test 1" };
            _controller.ModelState.AddModelError("DateStart", "DateStart is required");

            // Act
            var result = _controller.AddExercise(invalidExercise) as BadRequestObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.StatusCode, Is.EqualTo(400));
        }

        // UpdateExercise tests
        [Test]
        public void UpdateExercise_ExistingExercise_ShouldReturnOk()
        {
            // Arrange
            var id = 1;
            var updatedExercise = new Exercise { Id = id, DateStart = "2024-02-19", DateEnd = "2024-02-20", Duration = "2:00:00", Comments = "Updated comment" };

            // Configure the mock repository to return a dummy exercise when GetById is called with the expected ID
            _mockRepository.GetById(id).Returns(new Exercise { Id = id });

            // Act
            var result = _controller.UpdateExercise(id, updatedExercise) as OkResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.StatusCode, Is.EqualTo(200));
        }

        // DeleteExercise tests
        [Test]
        public void DeleteExercise_ExistingExercise_ShouldReturnOk()
        {
            // Arrange
            var id = 1;

            // Create mock data for the exercise to be deleted
            var exerciseToDelete = new Exercise { Id = id, DateStart = "2024-01-01", DateEnd = "2024-01-02", Duration = "1:00:00", Comments = "Sample exercise" };

            // Configure the mock repository to return the mock data when GetById is called with the expected ID
            _mockRepository.GetById(id).Returns(exerciseToDelete);

            // Act
            var result = _controller.DeleteExercise(id) as OkResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.StatusCode, Is.EqualTo(200));
        }
    }
}