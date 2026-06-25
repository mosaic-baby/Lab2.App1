using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Lab2.App1
{
    public partial class MainWindow : Window
    {
        // Создаем нашего помощника для работы с базой данных
        private DatabaseHelper _db = new DatabaseHelper();

        public MainWindow()
        {
            InitializeComponent();
            RefreshGrid(); // Автоматически загружаем данные при старте приложения
        }

        // Метод для обновления таблицы на экране
        private void RefreshGrid()
        {
            try
            {
                StudentsGrid.ItemsSource = _db.GetStudents();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка чтения данных: {ex.Message}");
            }
        }

        // Кнопка ДОБАВИТЬ СТУДЕНТА
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int id = int.Parse(TxtId.Text);
                string name = TxtName.Text;
                string phone = TxtPhone.Text;
                int physics = int.Parse(TxtPhysics.Text);
                int math = int.Parse(TxtMath.Text);

                _db.AddStudent(id, name, phone, physics, math);
                RefreshGrid(); // Перерисовываем таблицу на экране
                ClearFields(); // Очищаем текстовые поля
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении: Убедитесь, что ID уникален, а оценки — это числа! ({ex.Message})");
            }
        }

        // Кнопка СОХРАНИТЬ ИЗМЕНЕНИЯ (Редактирование)
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int id = int.Parse(TxtId.Text);
                string name = TxtName.Text;
                string phone = TxtPhone.Text;
                int physics = int.Parse(TxtPhysics.Text);
                int math = int.Parse(TxtMath.Text);

                _db.UpdateStudent(id, name, phone, physics, math);
                RefreshGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении: {ex.Message}");
            }
        }

        // Кнопка УДАЛИТЬ СТУДЕНТА
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (StudentsGrid.SelectedItem is StudentModel selectedStudent)
            {
                var result = MessageBox.Show($"Удалить студента {selectedStudent.FullName}?", "Подтверждение", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    _db.DeleteStudent(selectedStudent.Id);
                    RefreshGrid();
                    ClearFields();
                }
            }
            else
            {
                MessageBox.Show("Сначала выберите строку в таблице для удаления!");
            }
        }

        // Автозаполнение текстовых полей внизу при клике по строке в таблице
        private void StudentsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (StudentsGrid.SelectedItem is StudentModel student)
            {
                TxtId.Text = student.Id.ToString();
                TxtId.IsEnabled = false; // Запрещаем менять ID при редактировании, чтобы не ломать связи
                TxtName.Text = student.FullName;
                TxtPhone.Text = student.Phone;
                TxtPhysics.Text = student.Physics.ToString();
                TxtMath.Text = student.Math.ToString();
            }
        }

        // Кнопка "Очистить поля"
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            ClearFields();
        }

        // Вспомогательный метод очистки формы ввода
        private void ClearFields()
        {
            TxtId.Text = "";
            TxtId.IsEnabled = true; // Снова разрешаем ввод ID для нового студента
            TxtName.Text = "";
            TxtPhone.Text = "";
            TxtPhysics.Text = "";
            TxtMath.Text = "";
            StudentsGrid.SelectedItem = null;
        }
    }
}