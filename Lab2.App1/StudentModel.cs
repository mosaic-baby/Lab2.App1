using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.App1
{
    public class StudentModel
    {
        public int Id { get; set; }          // Уникальный номер
        public string FullName { get; set; }  // ФИО
        public int Physics { get; set; }     // Оценка по физике
        public int Math { get; set; }        // Оценка по математике
        public string Phone { get; set; }     // Номер телефона 
    }
}