using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradeBook.GradeBooks
{
    public class RankedGradeBook:BaseGradeBook
    {
        public RankedGradeBook(string name, bool IsWeighted):base(name, IsWeighted)
        {
            Type = Enums.GradeBookType.Ranked;
        }
        public override void CalculateStatistics()
        {
            if(Students.Count < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students.");
            }
            else
            {
                base.CalculateStatistics();
            }
            
        }
        public override void CalculateStudentStatistics(string name)
        {
            if (Students.Count < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students.");
            }
            else
            {
                base.CalculateStudentStatistics(name);
            }
        }
        public override char GetLetterGrade(double averageGrade)
        {
            if(Students.Count < 5)
            {
                throw new InvalidOperationException();
            }
            var grade = Students.Select(y => y.AverageGrade).OrderByDescending(y => y).ToList();
            var minGrade = grade.Count / 5;
            var Count = 0;
            var GradeFinals = 5;
            foreach(var i in grade)
            {
                if(averageGrade >= i)
                {
                    break;
                }
                Count++;
                if(Count>= minGrade)
                {
                    Count -= minGrade;
                    GradeFinals--;
                }
            }
            return GradeFinals switch
            {
                5 => 'A',
                4 => 'B',
                3 => 'C',
                2 => 'D',
                _ => 'F'
            };
        }
    }
}
