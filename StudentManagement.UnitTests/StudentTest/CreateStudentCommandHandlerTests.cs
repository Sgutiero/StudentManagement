using FluentAssertions;
using Moq;
using StudentManagement.Application.Interfaces;
using StudentManagement.Application.Students.Commands;
using StudentManagement.Application.Students.Handlers;
using StudentManagement.Domain.Entities;

namespace StudentManagement.UnitTests.StudentTest
{
    public class CreateStudentCommandHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly CreateStudentCommandHandler _handler;

        public CreateStudentCommandHandlerTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _unitOfWorkMock.Setup(u => u.Students.AddAsync(It.IsAny<Student>()))
                           .Returns(Task.CompletedTask);
            _unitOfWorkMock.Setup(u => u.CommitAsync()).ReturnsAsync(1);

            _handler = new CreateStudentCommandHandler(_unitOfWorkMock.Object);
        }

        [Fact]
        public async Task Should_Create_Student_Successfully()
        {
            var command = new CreateStudentCommand
            {
                Name = "Sebastián Gutiérrez",
                Document = "12345678",
                Email = "sgutiero@gmail.com"
            };

            var result = await _handler.Handle(command, default);

            result.Should().NotBeNull();
            result.Name.Should().Be("Sebastián Gutiérrez");

            _unitOfWorkMock.Verify(u => u.Students.AddAsync(It.IsAny<Student>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Once);
        }
    }
}
