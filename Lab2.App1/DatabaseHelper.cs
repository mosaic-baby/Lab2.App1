using System;
using System.Collections.Generic;
using System.IO;

namespace Lab2.App1
{
    public class DatabaseHelper
    {
        // Вместо базы данных будем хранить всё в простом текстовом файле в папке программы
        private const string FilePath = "students_data.txt";

        public DatabaseHelper()
        {
            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            // Если файла ещё нет, просто создаем его пустым
            if (!File.Exists(FilePath))
            {
                File.Create(FilePath).Close();
            }
        }

        // 3) ЧТЕНИЕ ДАННЫХ
        public List<StudentModel> GetStudents()
        {
            var list = new List<StudentModel>();
            if (!File.Exists(FilePath)) return list;

            var lines = File.ReadAllLines(FilePath);
            foreach (var line in lines)
            {
                var parts = line.Split(';');
                if (parts.Length == 5)
                {
                    list.Add(new StudentModel
                    {
                        Id = int.Parse(parts[0]),
                        FullName = parts[1],
                        Phone = parts[2],
                        Physics = int.Parse(parts[3]),
                        Math = int.Parse(parts[4])
                    });
                }
            }
            return list;
        }

        // 2) ДОБАВЛЕНИЕ ДАННЫХ
        public void AddStudent(int id, string name, string phone, int physics, int math)
        {
            string line = $"{id};{name};{phone};{physics};{math}";
            File.AppendAllLines(FilePath, new[] { line });
        }

        // 4) РЕДАКТИРОВАНИЕ ДАННЫХ
        public void UpdateStudent(int id, string name, string phone, int physics, int math)
        {
            var students = GetStudents();
            var newLines = new List<string>();

            foreach (var s in students)
            {
                if (s.Id == id)
                {
                    newLines.Add($"{id};{name};{phone};{physics};{math}");
                }
                else
                {
                    newLines.Add($"{s.Id};{s.FullName};{s.Phone};{s.Physics};{s.Math}");
                }
            }
            File.WriteAllLines(FilePath, newLines);
        }

        // 5) УДАЛЕНИЕ ДАННЫХ
        public void DeleteStudent(int id)
        {
            var students = GetStudents();
            var newLines = new List<string>();

            foreach (var s in students)
            {
                if (s.Id != id)
                {
                    newLines.Add($"{s.Id};{s.FullName};{s.Phone};{s.Physics};{s.Math}");
                }
            }
            File.WriteAllLines(FilePath, newLines);
        }
    }
}