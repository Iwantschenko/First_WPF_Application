using DbContextClasses;
using iText.Kernel.Pdf;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualBasic.FileIO;
using Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Xceed.Document.NET;
using Xceed.Words.NET;

using iText.Layout;
using iText.Layout.Element;
namespace Task.UserControll
{
    /// <summary>
    /// Interaction logic for StudentTabControll.xaml
    /// </summary>
    public partial class StudentTabControll : UserControl
    {
        private ResourceManager _resources;
        private ServiceDb<Student> _studentService;
        private ServiceDb<Course> _courseService;
        private GroupStudent _thisGroup;
        private Course _thisCourse;
        private ObservableCollection<Student> _studentsListView;
        public StudentTabControll(IServiceScope scope)
        {
            _resources = new ResourceManager("Task.Resource2", Assembly.GetExecutingAssembly());
            _studentService = scope.ServiceProvider.GetRequiredService<ServiceDb<Student>>();
            _courseService = scope.ServiceProvider.GetRequiredService<ServiceDb<Course>>();
            _thisGroup = new GroupStudent();
            _thisCourse = new Course(); 
            _studentsListView = new ObservableCollection<Student>();
            InitializeComponent();
        }

        public TabItem CreateTabItem(GroupHierarchicalLowTree item )
        {
            _thisGroup = item.Group;
            _thisCourse = _courseService.GetAll().FirstOrDefault(x => x.Course_ID == _thisGroup.CourseId);
            _studentsListView = item.Students;

            studentListUI.ItemsSource = _studentsListView;
            
            return new TabItem()
            {
                Content = Panel
            };
        }
        private void Create_Click(object sender, RoutedEventArgs e)
        {
            IdBox.Text = Guid.NewGuid().ToString();
            IdGroupBox.Text = _thisGroup.Group_Name;
            NameBox.Text = String.Empty;
            SurnameBox.Text = String.Empty;
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (studentListUI.SelectedItem != null)
            {
                try
                {
                    Student item = (Student)studentListUI.SelectedItem;

                    IdBox.Text = item.Student_Id.ToString();
                    IdGroupBox.Text = _thisGroup.Group_Name;
                    NameBox.Text = item.First_Name;
                    SurnameBox.Text = item.Last_Name;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show(_resources.GetString("EditSelect"));
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (studentListUI.SelectedItem != null && studentListUI.SelectedItem is Student)
            {
                Student student = (Student)studentListUI.SelectedItem;
                var resultMessege = MessageBox.Show(_resources.GetString("Delete"), "Delete", MessageBoxButton.YesNo);
                if (resultMessege == MessageBoxResult.Yes)
                {
                    _studentService.Remove(student);
                    _studentsListView.Remove(student);
                    _studentService.Save();
                }
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Student student = new Student()
                {
                    Student_Id = Guid.Parse(IdBox.Text),
                    GroupId = _thisGroup.Group_Id,
                    First_Name = NameBox.Text,
                    Last_Name = SurnameBox.Text
                };
                if (MessageBox.Show(_resources.GetString("CreateChangeYes"), "GroupYesNo", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    if (_studentService.GetId(student.Student_Id) == null)
                    {
                        _studentService.Add(student);   
                        _studentsListView.Add(student);
                        _studentService.Save();
                    }
                    if (_studentService.GetId(student.Student_Id) != null)
                    {
                        _studentService.Update(student);

                        int index = _studentsListView.IndexOf(_studentsListView.FirstOrDefault(x => x.Student_Id == student.Student_Id));

                        _studentsListView[index] = student;

                        _studentService.Save();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        private void Export_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (pathBoxExport.Text == String.Empty)
                {
                    MessageBox.Show(_resources.GetString("EnterPath"));
                    return;
                }
                if (MessageBox.Show(_resources.GetString("Export"), "Export", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    DoExport(pathBoxExport.Text);
                }
            }
            catch(Exception ex )
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void DoExport(string path)
        {
            using (StreamWriter file = new StreamWriter(path))
            {
                string line = string.Empty;
                foreach (var student in _studentsListView)
                {
                    line = string.Format("{0},{1}",student.First_Name,student.Last_Name);
                    file.WriteLine(line);
                }
            }
        }
        private void Import_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_studentsListView.Count != 0)
                {
                    MessageBox.Show(_resources.GetString("ImportNotNuLL"));
                    return;
                }
                if (MessageBox.Show(_resources.GetString("Import"), "Import", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    if (pathBoxImport.Text == String.Empty)
                    {
                        MessageBox.Show(_resources.GetString("EnterPath"));
                        return;
                    }
                    DoImport(pathBoxImport.Text);
                }
                    
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private List<string[]> ReadDataFromCSV(string path)
        {
            var listElements = new List<string[]>();
            using (TextFieldParser parser = new TextFieldParser(path))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                parser.TrimWhiteSpace = true;
                while (!parser.EndOfData)
                {
                    listElements.Add(parser.ReadFields());
                }
            }
                return listElements;
        }
        private void DoImport(string path)
        {
            var importList = ReadDataFromCSV(path);
            foreach (var student in importList)
            {
                Student tempStudent = new Student()
                {
                    Student_Id = Guid.NewGuid(),
                    GroupId = _thisGroup.Group_Id,
                    First_Name = student[0],
                    Last_Name = student[1]
                };
                _studentsListView.Add(tempStudent);
                _studentService.Add(tempStudent);
            }
            _studentService.Save();
        }
        private void wordRepBut_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show(_resources.GetString("DoReport"), "REPORT", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    CreateWordRep();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void CreateWordRep()
        {
            
            using (Xceed.Words.NET.DocX document = Xceed.Words.NET.DocX.Create("GroupList.docx"))
            {
                document.InsertParagraph(_thisCourse.Course_Name).Bold().FontSize(16).Alignment = Alignment.center;
                document.InsertParagraph(_thisGroup.Group_Name).Bold().FontSize(14).Alignment = Alignment.center;

                // Adding a numbered list of students
                var list = document.AddList(listType: ListItemType.Numbered, startNumber: 1);
                foreach (var student in _studentsListView)
                {
                    document.AddListItem(list, $"{student.First_Name} {student.Last_Name}", 0);
                }
                document.InsertList(list);
                document.Save();
            }

        }
        private void PDFRepBut_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show(_resources.GetString("DoReport"), "REPORT", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    CreatePDFRep();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void CreatePDFRep()
        {
            
            using (PdfDocument pdfDoc = new PdfDocument(new PdfWriter("StudentList.pdf")))
            {
                var document = new iText.Layout.Document(pdfDoc);
                var paragraph = new iText.Layout.Element.Paragraph(_thisCourse.Course_Name + " - " + _thisGroup.Group_Name)
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                    .SetFontSize(16)
                    .SetBold();
                document.Add(paragraph);

                document.Add(new iText.Layout.Element.Paragraph("List of Students:")
                    .SetBold()
                    .SetFontSize(14)
                    );
                int studentsNumber = 1;
                foreach (var student in _studentsListView)
                {
                    document.Add(new iText.Layout.Element.Paragraph($"{studentsNumber}. {student.First_Name} {student.Last_Name}"));
                    studentsNumber++;
                }
                document.Close();
            };
        }
    }
}
