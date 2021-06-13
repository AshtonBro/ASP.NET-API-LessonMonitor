using AutoFixture;
using FluentAssertions;
using LessonMonitor.Core.Exceprions;
using LessonMonitor.Core.Models;
using LessonMonitor.Core.Repositories;
using Moq;
using NUnit.Framework;
using System;

namespace LessonMonitor.BussinesLogic.NTests
{
    public class HomeworksServiceNTests
    {
        private Mock<IHomeworksRepository> _homeworkRepositoryMock;
        private HomeworksService _service;

        public HomeworksServiceNTests() {}

        [SetUp]
        public void SetUp()
        {
            _homeworkRepositoryMock = new Mock<IHomeworksRepository>();
            _service = new HomeworksService(_homeworkRepositoryMock.Object);
        }

        // unit testing name patterns (find name methods for test in google)
        // MethodName_Conditions_Result
        [Test]
        public void Create_HomeworkIsValide_ShouldCreateNewHomework()
        {
            // arrange - �������������� ������
            var fixture = new Fixture();

            var homework = fixture.Build<Homework>()
                .Without(x => x.MentorId)
                .Create();

            //var homeworks = fixture.CreateMany<Homework>(5);

            // act - ��������� ����������� �����
            var result = _service.Create(homework);

            // assert - ���������/���������� ���������� �����
            result.Should().BeTrue();
            _homeworkRepositoryMock.Verify(x => x.Add(homework), Times.Once);
        }

        [Test]
        public void Create_HomeworkIsNull_ShouldThrowArgumentNullException()
        {
            // arrange 
            Homework homework = null;

            // act 
            bool result = false;

            var exceprtion = Assert.Throws<ArgumentNullException>(() => result = _service.Create(homework));

            // assert
            exceprtion.Should().NotBeNull()
                .And.Match<ArgumentNullException>(x => x.ParamName == "homework");

            result.Should().BeFalse();
            _homeworkRepositoryMock.Verify(x => x.Add(homework), Times.Never);
        }

        [Test]
        [TestCase(0)]
        [TestCase(-456)]
        [TestCase(-45654)]
        [TestCase(-1546454)]
        public void Create_HomeworkIsInvalide_ShouldThrowBusinessExceprion(int memberId)
        {
            // arrange
            var homework = new Homework();
            homework.MemberId = memberId;

            // act
            bool result = false;

            var exceprtion = Assert.Throws<HomeworkException>(() => result = _service.Create(homework));

            // assert
             exceprtion.Should().NotBeNull()
                .And
                .Match<HomeworkException>(x => x.Message == HomeworksService.HOMEWORK_IS_INVALID);

            result.Should().BeFalse();
            _homeworkRepositoryMock.Verify(x => x.Add(homework), Times.Never);
        }

        [Test]
        public void Delete_ShouldDeleteHomework()
        {
            // arrange
            var fixture = new Fixture();

            var homeworkId = fixture.Create<int>();

            // act
            var result = _service.Delete(homeworkId);

            // assert
            result.Should().BeTrue();
            _homeworkRepositoryMock.Verify(x => x.Delete(homeworkId), Times.Once);
        }
    }
}
