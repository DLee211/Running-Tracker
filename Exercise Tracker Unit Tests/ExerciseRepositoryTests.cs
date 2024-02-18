using NSubstitute;
using Microsoft.EntityFrameworkCore;
using Exercise_Tracker;
using Exercise_Tracker_v2.Models;
using Assert = NUnit.Framework.Assert;

[TestFixture]
public class ExerciseRepositoryTests
{
    [Test]
    public void Constructor_WithValidContext_InitializesContextField()
    {
        // Arrange
        var mockContext = Substitute.For<ExerciseDbContext>();

        // Act
        var repository = new ExerciseRepository<Exercise>(mockContext);

        // Assert
        Assert.That(repository.GetContext(), Is.EqualTo(mockContext)); // Assuming you have a method to get the context for testing
    }
    
    
    [Test]
    public void GetById_ValidId_ShouldReturnEntity()
    {
        // Arrange
        var testData = new List<Exercise>
        {
            new Exercise
                { Id = 1, DateStart = "2024-02-17", DateEnd = "2024-02-18", Duration = "1:00:00", Comments = "Test 1" },
            new Exercise
                { Id = 2, DateStart = "2024-02-18", DateEnd = "2024-02-19", Duration = "1:30:00", Comments = "Test 2" },
            new Exercise
                { Id = 3, DateStart = "2024-02-19", DateEnd = "2024-02-20", Duration = "2:00:00", Comments = "Test 3" }
        };

        var mockDbSet = Substitute.For<DbSet<Exercise>, IQueryable<Exercise>>();

        ((IQueryable<Exercise>)mockDbSet).Provider.Returns(testData.AsQueryable().Provider);
        ((IQueryable<Exercise>)mockDbSet).Expression.Returns(testData.AsQueryable().Expression);
        ((IQueryable<Exercise>)mockDbSet).ElementType.Returns(testData.AsQueryable().ElementType);
        ((IQueryable<Exercise>)mockDbSet).GetEnumerator().Returns(testData.AsQueryable().GetEnumerator());
        mockDbSet.Find(Arg.Any<object[]>())
            .Returns(callInfo => testData.FirstOrDefault(e => e.Id == (int)callInfo.Arg<object[]>()[0]));

        var mockContext = Substitute.For<ExerciseDbContext>();
        mockContext.Set<Exercise>().Returns(mockDbSet);

        var repository = new ExerciseRepository<Exercise>(mockContext);

        // Act
        var result = repository.GetById(1);

        // Assert
        Assert.NotNull(result);
        Assert.That(result.Id, Is.EqualTo(1));

        // Act
        result = repository.GetById(2);

        // Assert
        Assert.NotNull(result);
        Assert.That(result.Id, Is.EqualTo(2));

        // Act
        result = repository.GetById(3);

        // Assert
        Assert.NotNull(result);
        Assert.That(result.Id, Is.EqualTo(3));
    }

    [Test]
    public void GetAll_ShouldReturnAllEntities()
    {
        // Arrange
        var testData = new List<Exercise>
        {
            new Exercise
                { Id = 1, DateStart = "2024-02-17", DateEnd = "2024-02-18", Duration = "1:00:00", Comments = "Test 1" },
            new Exercise
                { Id = 2, DateStart = "2024-02-18", DateEnd = "2024-02-19", Duration = "1:30:00", Comments = "Test 2" },
            new Exercise
                { Id = 3, DateStart = "2024-02-19", DateEnd = "2024-02-20", Duration = "2:00:00", Comments = "Test 3" }
        };

        var mockDbSet = Substitute.For<DbSet<Exercise>, IQueryable<Exercise>>();
        ((IQueryable<Exercise>)mockDbSet).Provider.Returns(testData.AsQueryable().Provider);
        ((IQueryable<Exercise>)mockDbSet).Expression.Returns(testData.AsQueryable().Expression);
        ((IQueryable<Exercise>)mockDbSet).ElementType.Returns(testData.AsQueryable().ElementType);
        ((IQueryable<Exercise>)mockDbSet).GetEnumerator().Returns(testData.AsQueryable().GetEnumerator());

        var mockContext = Substitute.For<ExerciseDbContext>();
        mockContext.Set<Exercise>().Returns(mockDbSet);

        var repository = new ExerciseRepository<Exercise>(mockContext);

        // Act
        var result = repository.GetAll();

        // Assert
        Assert.NotNull(result);
        Assert.That(result.Count(), Is.EqualTo(3)); // Assuming we have three entities in the test data

        Assert.Multiple(() =>
        {
            Assert.That(result.First().DateStart, Is.EqualTo("2024-02-17"));
            Assert.That(result.First().DateEnd, Is.EqualTo("2024-02-18"));
            Assert.That(result.First().Duration, Is.EqualTo("1:00:00"));
            Assert.That(result.First().Comments, Is.EqualTo("Test 1"));
        });

        Assert.Multiple(() =>
        {
            Assert.That(result.ElementAt(1).DateStart, Is.EqualTo("2024-02-18"));
            Assert.That(result.ElementAt(1).DateEnd, Is.EqualTo("2024-02-19"));
            Assert.That(result.ElementAt(1).Duration, Is.EqualTo("1:30:00"));
            Assert.That(result.ElementAt(1).Comments, Is.EqualTo("Test 2"));
        });

        Assert.Multiple(() =>
        {
            Assert.That(result.Last().DateStart, Is.EqualTo("2024-02-19"));
            Assert.That(result.Last().DateEnd, Is.EqualTo("2024-02-20"));
            Assert.That(result.Last().Duration, Is.EqualTo("2:00:00"));
            Assert.That(result.Last().Comments, Is.EqualTo("Test 3"));
        });
    }

    [Test]
    public void Add_ValidExercise_ShouldAddToRepository()
    {
        // Arrange
        var exerciseToAdd = new Exercise { Id = 1, DateStart = "2024-02-17", DateEnd = "2024-02-18", Duration = "1:00:00", Comments = "Test 1" };

        var mockDbSet = Substitute.For<DbSet<Exercise>>();

        var mockContext = Substitute.For<ExerciseDbContext>();
        mockContext.Set<Exercise>().Returns(mockDbSet); // Mock DbSet

        var repository = new ExerciseRepository<Exercise>(mockContext);

        // Act
        repository.Add(exerciseToAdd);

        // Assert
        mockDbSet.Received(1).Add(exerciseToAdd); // Ensure Add method was called
        mockContext.Received(1).SaveChanges(); // Ensure SaveChanges method was called
    }
    
    [Test]
    public void Delete_WhenEntityExists_ShouldRemoveFromRepository()
    {
        // Arrange
        var exerciseToDelete = new Exercise { Id = 1, DateStart = "2024-02-17", DateEnd = "2024-02-18", Duration = "1:00:00", Comments = "Test 1" };

        var mockDbSet = Substitute.For<DbSet<Exercise>>();

        var mockContext = Substitute.For<ExerciseDbContext>();
        mockContext.Set<Exercise>().Returns(mockDbSet); // Mock DbSet

        var repository = new ExerciseRepository<Exercise>(mockContext);

        // Act
        repository.Delete(exerciseToDelete);

        // Assert
        mockDbSet.Received(1).Remove(exerciseToDelete); // Ensure Remove method was called with the correct entity
        mockContext.Received(1).SaveChanges(); // Ensure SaveChanges method was called
    }
}