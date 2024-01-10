using DbContextClasses;
using FunctionRepository;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Services;
using System.Text.RegularExpressions;

namespace UnitTest
{
    public class Tests
    {
      
        [SetUp]
        public void Setup()
        {
        
        }
        [Test]
        public void ConnectionDataBase()
        {
            string connectionString = @"Server=(LocalDB)\MSSQLLocalDB;Database=DbWPF;Trusted_Connection=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                }
                catch (Exception ex)
                {
                    Assert.Fail(ex.Message);
                }
            }
        }
        [Test]
        public void Course_AddTest()
        {
            var mockRepository = new Mock<IRepository<Course>>();
            var course = new Course
            {
                Course_ID = Guid.NewGuid(),
                Course_Name = "Course",
                Course_Description = "Description"
            };            
            mockRepository.Object.Add(course);

            mockRepository.Verify(repo => repo.Add(It.IsAny<Course>()), Times.Once);
        }
        [Test]
        public void Course_RemoveTest() 
        {
            var mockRepository = new Mock<IRepository<Course>>();
            var course = new Course
            {
                Course_ID = Guid.NewGuid(),
                Course_Name = "Course",
                Course_Description = "Description"
            };
            mockRepository.Setup(repo => repo.GetId(It.IsAny<Guid>())).Returns(course);
            mockRepository.Object.Delete(course);
            mockRepository.Verify(repo => repo.Delete(It.IsAny<Course>()), Times.Once);
        }
        [Test]
        public void Course_UpdateTest() 
        {
            var mockRepository = new Mock<IRepository<Course>>();
            var course = new Course
            {
                Course_ID = Guid.NewGuid(),
                Course_Name = "Sample Course",
                Course_Description = "Sample Description"
            };

            mockRepository.Object.Update(course);     
           
            mockRepository.Verify(repo => repo.Update(It.IsAny<Course>()), Times.Once);
            
        }
        [Test]
        public void Course_GetAllTest() 
        {
            var mockRepository = new Mock<IRepository<Course>>();
            var courses = new List<Course>
            {
                new Course { Course_ID = Guid.NewGuid(), Course_Name = "Course 1", Course_Description = "Description 1" },
                new Course { Course_ID = Guid.NewGuid(), Course_Name = "Course 2", Course_Description = "Description 2" },
                new Course { Course_ID = Guid.NewGuid(), Course_Name = "Course 3", Course_Description = "Description 3" }
            };

            mockRepository.Setup(repo => repo.GetAll()).Returns(courses);
            var result = mockRepository.Object.GetAll();

            Assert.NotNull(result);
            Assert.AreEqual(3, result.Count);
        }

        public void Teacher_AddTest()
        {
            var mockRepository = new Mock<IRepository<Teacher>>();
            var teacher = new Teacher 
            {
                Teacher_Id = Guid.NewGuid(),
                Teacher_Name = "Test",
                Teacher_Surname = "Test",
            };
            mockRepository.Object.Add(teacher);

            mockRepository.Verify(repo => repo.Add(It.IsAny<Teacher>()), Times.Once);
        }
        [Test]
        public void Teacher_RemoveTest() 
        {
            var mockRepository = new Mock<IRepository<Teacher>>();
            var teacher = new Teacher
            {
                Teacher_Id = Guid.NewGuid(),
                Teacher_Name = "Test",
                Teacher_Surname = "Test",
            };
            mockRepository.Setup(repo => repo.GetId(It.IsAny<Guid>())).Returns(teacher);
            mockRepository.Object.Delete(teacher);
            mockRepository.Verify(repo => repo.Delete(It.IsAny<Teacher>()), Times.Once);
        }
        [Test]
        public void Teacher_UpdateTest() 
        {
            var mockRepository = new Mock<IRepository<Teacher>>();
            var teacher = new Teacher
            {
                Teacher_Id = Guid.NewGuid(),
                Teacher_Name = "Test",
                Teacher_Surname = "Test",
            };
            mockRepository.Object.Update(teacher);

            mockRepository.Verify(repo => repo.Update(It.IsAny<Teacher>()), Times.Once);

        }
        [Test]
        public void Teacher_GetAllTest() 
        {
            var mockRepository = new Mock<IRepository<Teacher>>();
            var teachers = new List<Teacher>
            {
                new Teacher {},
                new Teacher {},
                new Teacher {}
            };

            mockRepository.Setup(repo => repo.GetAll()).Returns(teachers);
            var result = mockRepository.Object.GetAll();

            Assert.NotNull(result);
            Assert.AreEqual(3, result.Count);
        }
        public void Group_AddTest()
        {
            var mockRepository = new Mock<IRepository<GroupStudent>>();
            var group = new GroupStudent
            {
                Group_Id = Guid.NewGuid(),
                TeacherId = Guid.NewGuid(),
                Group_Name = "Test"
            };
            mockRepository.Object.Add(group);

            mockRepository.Verify(repo => repo.Add(It.IsAny<GroupStudent>()), Times.Once);
        }
        [Test]
        public void Group_RemoveTest() 
        {
            var mockRepository = new Mock<IRepository<GroupStudent>>();
            var group = new GroupStudent
            {
                Group_Id = Guid.NewGuid(),
                TeacherId = Guid.NewGuid(),
                Group_Name = "Test"
            };
            mockRepository.Setup(repo => repo.GetId(It.IsAny<Guid>())).Returns(group);
            mockRepository.Object.Delete(group);
            mockRepository.Verify(repo => repo.Delete(It.IsAny<GroupStudent>()), Times.Once);
        }
        [Test]
        public void Group_UpdateTest() 
        {
            var mockRepository = new Mock<IRepository<GroupStudent>>();
            var group = new GroupStudent
            {
                Group_Id = Guid.NewGuid(),
                TeacherId = Guid.NewGuid(),
                Group_Name = "Test"
            };
            mockRepository.Object.Update(group);

            mockRepository.Verify(repo => repo.Update(It.IsAny<GroupStudent>()), Times.Once);

        }
        [Test]
        public void Group_GetAllTest() 
        {
            var mockRepository = new Mock<IRepository<GroupStudent>>();
            var groups = new List<GroupStudent>
            {
                new GroupStudent {},
                new GroupStudent {},
                new GroupStudent {}
            };

            mockRepository.Setup(repo => repo.GetAll()).Returns(groups);
            var result = mockRepository.Object.GetAll();

            Assert.NotNull(result);
            Assert.AreEqual(3, result.Count);
        }
        public void Student_AddTest()
        {
            var mockRepository = new Mock<IRepository<Student>>();
            var student = new Student
            {
                Student_Id = Guid.NewGuid(),
                GroupId = Guid.NewGuid(),
                First_Name = "Test",
                Last_Name = "Test"
            };
            mockRepository.Object.Add(student);

            mockRepository.Verify(repo => repo.Add(It.IsAny<Student>()), Times.Once);
        }
        [Test]
        public void Student_RemoveTest() 
        {
            var mockRepository = new Mock<IRepository<Student>>();
            var student = new Student
            {
                Student_Id = Guid.NewGuid(),
                GroupId = Guid.NewGuid(),
                First_Name = "Test",
                Last_Name = "Test"
            };
            mockRepository.Setup(repo => repo.GetId(It.IsAny<Guid>())).Returns(student);
            mockRepository.Object.Delete(student);
            mockRepository.Verify(repo => repo.Delete(It.IsAny<Student>()), Times.Once);
        }
        [Test]
        public void Student_UpdateTest() 
        {
            var mockRepository = new Mock<IRepository<Student>>();
            var student = new Student
            {
                Student_Id = Guid.NewGuid(),
                GroupId = Guid.NewGuid(),
                First_Name = "Test",
                Last_Name = "Test"
            };
            mockRepository.Object.Update(student);

            mockRepository.Verify(repo => repo.Update(It.IsAny<Student>()), Times.Once);
        }
        [Test]
        public void Student_GetAllTest() 
        {
            var mockRepository = new Mock<IRepository<Student>>();
            var students = new List<Student>
            {
                new Student {},
                new Student {},
                new Student {}
            };

            mockRepository.Setup(repo => repo.GetAll()).Returns(students);
            var result = mockRepository.Object.GetAll();

            Assert.NotNull(result);
            Assert.AreEqual(3, result.Count);
        }



    }
}