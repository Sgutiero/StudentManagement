using FluentAssertions;
using Moq;
using StudentManagement.Application.Enrollments.Commands;
using StudentManagement.Application.Enrollments.Handlers;
using StudentManagement.Application.Interfaces;
using StudentManagement.Domain.Entities;

namespace StudentManagement.UnitTests.StudentTest
{
    public class RemoveEnrollmentCommandHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly RemoveEnrollmentCommandHandler _handler;
        private readonly Student _student;
        private readonly Subject _subject;

        public RemoveEnrollmentCommandHandlerTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();

            _student = new Student("Sebas", "123", "sgutiero@gmal.com");
            _subject = new Subject("Física", "003", 6);

            _student.EnrollInSubject(_subject);

            _unitOfWorkMock.Setup(u => u.Students.GetByIdAsync(_student.Id)).ReturnsAsync(_student);
            _unitOfWorkMock.Setup(u => u.Subjects.GetByIdAsync(_subject.Id)).ReturnsAsync(_subject);
            _unitOfWorkMock.Setup(u => u.CommitAsync()).ReturnsAsync(1);

            _handler = new RemoveEnrollmentCommandHandler(_unitOfWorkMock.Object);
        }

        [Fact]
        public async Task Should_Remove_Enrollment_Successfully()
        {
            var command = new RemoveEnrollmentCommand(_student.Id, _subject.Id);

            var result = await _handler.Handle(command, default);

            result.Should().BeTrue();
            _student.Enrollments.Should().BeEmpty();
        }
    }
}
