use DbWPF;
Delete From Groups;
Delete from Courses;
Delete from Teachers;
Delete from Students;
Insert into Courses (Course_ID , Course_Name ,Course_Description)
values 
	('b265cc0c-4f2e-450a-98ef-a4f9c6289262','JavaScript','Front-end'),
	('01bc94dd-1a30-425c-919c-7bab26f82461','TypeScript' ,'Back-end'),
	('396a0259-bccf-445a-b115-67fd63a8fb55', 'C# .Net' ,'All .Net Technology');
Insert into Teachers (Teacher_Id , Teacher_Name , Teacher_Surname)
values 
		('7713545c-5ba6-4414-9a38-351853572145','Vova','Vo'),
		('445799b4-c48d-4d89-a0dd-7845eb75cad5', 'Ann','Ann' );
Insert into Groups (Group_Id , Group_Name , CourseId ,TeacherId)
VALUES 
		('c1d8cb7b-9dd4-4d7d-9318-5094983563d8','WPF' , '396a0259-bccf-445a-b115-67fd63a8fb55','7713545c-5ba6-4414-9a38-351853572145'),
		('889da2f2-5186-4ae8-97c3-206ae0d993d3','ASP.Net Core','396a0259-bccf-445a-b115-67fd63a8fb55','7713545c-5ba6-4414-9a38-351853572145'),
		('1fa8ded7-aa6c-43a8-9a1c-35902be6fcfe','Front-end Group ','b265cc0c-4f2e-450a-98ef-a4f9c6289262','445799b4-c48d-4d89-a0dd-7845eb75cad5'),
		('717a5e16-0e46-42c0-b087-4431f2053636','Back-end Group','01bc94dd-1a30-425c-919c-7bab26f82461','445799b4-c48d-4d89-a0dd-7845eb75cad5');
Insert into Students (Student_Id , GroupId , First_Name , Last_Name)
Values 
	('b5e04c82-d3a5-4091-ba3a-442170b2d40d','889da2f2-5186-4ae8-97c3-206ae0d993d3','Nazar','Ivanchenko'),
	('83b6ec4b-c319-481f-8806-f05368c2d86b','889da2f2-5186-4ae8-97c3-206ae0d993d3','Artem','Yakunenko'),
	('e5a33a22-1cd0-43b9-aa1f-ef0663e0d827','889da2f2-5186-4ae8-97c3-206ae0d993d3','Vlad','Turovskiy');