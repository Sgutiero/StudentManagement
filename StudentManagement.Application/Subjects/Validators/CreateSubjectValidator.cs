using FluentValidation;
using StudentManagement.Application.Subjects.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Application.Subjects.Validators
{
    public class CreateSubjectValidator : AbstractValidator<CreateSubjectCommand>
    {
        public CreateSubjectValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Code)
                .NotEmpty()
                .MaximumLength(10);

            RuleFor(x => x.Credits)
                .InclusiveBetween(1, 10);
        }
    }
}
