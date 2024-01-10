
using DbContextClasses;
using FunctionRepository;
using Microsoft.Extensions.DependencyInjection;
using Services;
using static System.Formats.Asn1.AsnWriter;

namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        private ServiceDb<Course> _courseServise;
        private ServiceDb<GroupStudent> groupService;
        private ServiceDb<Teacher> _teacherService;
        private ServiceDb<Student> _studentService;
        public UnitTest1()
        {
            var collectionService = new ServiceCollection()
                .AddDbContext<DataBaseContext>()
                .AddScoped<IRepository<Course>, CourseRepository>()
                .AddScoped<IRepository<GroupStudent>, GroupRepository>()
                .AddScoped<IRepository<Teacher>, TeacherRepository>()
                .AddScoped<IRepository<Student>, StudentRepository>()
                .AddScoped<ServiceDb<Course>>()
                .AddScoped<ServiceDb<GroupStudent>>()
                .AddScoped<ServiceDb<Teacher>>()
                .AddScoped<ServiceDb<Student>>()
                .BuildServiceProvider();

            var _Scope = collectionService.CreateScope();
            _courseServise = _Scope.ServiceProvider.GetRequiredService<ServiceDb<Course>>();
            groupService = _Scope.ServiceProvider.GetRequiredService<ServiceDb<GroupStudent>>();
            _teacherService = _Scope.ServiceProvider.GetRequiredService<ServiceDb<Teacher>>();
            _studentService = _Scope.ServiceProvider.GetRequiredService<ServiceDb<Student>>();
        }

        //TestCourse
        [TestMethod]
        public void Add_Edit_Remove_Course()
        {
            Course test = new Course()
            {
                Course_ID = Guid.NewGuid(),
                Course_Name = "TestName",
                Course_Description = "Description",
            };
            
            _courseServise.Add(test);
            _courseServise.Save();
            Course? courseBD = _courseServise.GetId(test.Course_ID);
            Assert.IsNotNull(courseBD);

            //string EditName = "NewName";
            //string EditDesc = "NewDesc";
            //test.Course_Name = EditName;
            //test.Course_Description = EditDesc;

            //_courseServise.Update(test);
            //_courseServise.Save();
            
            //Assert.AreEqual(EditName, courseBD.Course_Name);
            //Assert.AreEqual(EditDesc, courseBD.Course_Description);

            //_courseServise.Remove(test);
            //_courseServise.Save();
            //Assert.IsNull(_courseServise.GetAll().FirstOrDefault(x => x.Course_ID == test.Course_ID));
        }
        [TestMethod]
        public void Edit_Course(Course course)
        {
            string EditName = "NewName";
            string EditDesc = "NewDesc";
            course.Course_Name = EditName;
            course.Course_Description = EditDesc;

            _courseServise.Update(course);
            _courseServise.Save();

            Assert.AreEqual(EditName, course.Course_Name);
            Assert.AreEqual(EditDesc, course.Course_Description);

        }


        //TestGroup
        [TestMethod]
        public void Add_Group()
        {

        }
        [TestMethod]
        public void Edit_Group()
        {

        }
        [TestMethod]
        public void Remove_Group()
        {

        }
        
        //TestStudent
        [TestMethod]
        public void Add_Student()
        {

        }
        [TestMethod]
        public void Edit_Student()
        {

        }
        [TestMethod]
        public void Remove_Student()
        {

        }
        
        //TestTeacher
        [TestMethod]
        public void Add_Teacher()
        {

        }
        [TestMethod]
        public void Edit_Teacher()
        {

        }
        [TestMethod]
        public void Remove_Teacher()
        {

        }
    }
}