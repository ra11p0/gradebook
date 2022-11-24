using Gradebook.Foundation.Common.Foundation.Commands.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gradebook.Foundation.Tests.Validation
{
    internal class EducationCycleStepCommandValidationTest
    {
        [Test]
        public void IsValid()
        {
            var command = new EducationCycleStepCommand()
            {
                Name = "Fake name",
                Subjects = new List<EducationCycleStepSubjectCommand>(){
                    new EducationCycleStepSubjectCommand(){
                    SubjectGuid= Guid.NewGuid(),
                    HoursNo = 40
                    }
                }
            };

            Assert.That(command.IsValid);
        }
        [Test]
        public void IsInvalid_HasEmptyName()
        {
            var command = new EducationCycleStepCommand()
            {
                Name = "",
                Subjects = new List<EducationCycleStepSubjectCommand>(){
                    new EducationCycleStepSubjectCommand(){
                    SubjectGuid= Guid.NewGuid(),
                    HoursNo = 40
                    }
                }
            };

            Assert.That(!command.IsValid);
        }
        [Test]
        public void IsInvalid_HasNoSubjects()
        {
            var command = new EducationCycleStepCommand()
            {
                Name = "Fake name",
                Subjects = new List<EducationCycleStepSubjectCommand>() { }
            };

            Assert.That(!command.IsValid);
        }
        [Test]
        public void IsInvalid_HasDoubledSubjects()
        {
            var subjectGuid = Guid.NewGuid();
            var command = new EducationCycleStepCommand()
            {
                Name = "Fake name",
                Subjects = new List<EducationCycleStepSubjectCommand>(){
                    new EducationCycleStepSubjectCommand(){
                    SubjectGuid= subjectGuid,
                    HoursNo = 40
                    },
                    new EducationCycleStepSubjectCommand(){
                    SubjectGuid= subjectGuid,
                    HoursNo = 50
                    }
                }
            };

            Assert.That(!command.IsValid);
        }
    }
}
