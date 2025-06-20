using FluentValidation;
using StudentManagement.Application.Enrollments.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Application.Enrollments.Validators
{
    public class EnrollStudentValidator : AbstractValidator<EnrollStudentCommand>
    {
        public EnrollStudentValidator()
        {
            RuleFor(x => x.StudentId).NotEmpty();
            RuleFor(x => x.SubjectId).NotEmpty();
        }
    }
}
