using FluentAssertions;
using Moq;
using StudentManagement.Application.Enrollments.Commands;
using StudentManagement.Application.Enrollments.Handlers;
using StudentManagement.Application.Interfaces;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Exceptions;

namespace StudentManagement.UnitTests.StudentTest
{
    public class EnrollStudentCommandHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly EnrollStudentCommandHandler _handler;
        private readonly Student _student;
        private readonly Subject _subject;

        public EnrollStudentCommandHandlerTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();

            _student = new Student("Sebas", "123", "sgutiero@gmail.com");
            _subject = new Subject("Cálculo", "002", 4);

            _unitOfWorkMock.Setup(u => u.Students.GetByIdAsync(_student.Id)).ReturnsAsync(_student);
            _unitOfWorkMock.Setup(u => u.Subjects.GetByIdAsync(_subject.Id)).ReturnsAsync(_subject);
            _unitOfWorkMock.Setup(u => u.CommitAsync()).ReturnsAsync(1);

            _handler = new EnrollStudentCommandHandler(_unitOfWorkMock.Object);
        }

        [Fact]
        public async Task Should_Enroll_Student_In_Valid_Subject()
        {
            var command = new EnrollStudentCommand
            {
                StudentId = _student.Id,
                SubjectId = _subject.Id
            };

            var result = await _handler.Handle(command, default);

            result.Should().ContainSingle(e => e.SubjectId == _subject.Id);
        }

        [Fact]
        public async Task Should_Throw_When_Already_Enrolled()
        {
            _student.EnrollInSubject(_subject);

            var command = new EnrollStudentCommand
            {
                StudentId = _student.Id,
                SubjectId = _subject.Id
            };

            Func<Task> act = async () => await _handler.Handle(command, default);

            await act.Should().ThrowAsync<RuleExceptions>()
                .WithMessage("Already enrolled in this subject.");
        }
    }
}
