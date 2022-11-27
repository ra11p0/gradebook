using Gradebook.Foundation.Common.Foundation.Commands.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gradebook.Foundation.Tests.Validation
{
    [Category("Unit")]
    internal class EducationCycleStepSubjectCommandValidationTest
    {
        [Test]
        public void IsValid()
        {
            var command = new EducationCycleStepSubjectCommand()
            {
                SubjectGuid = Guid.NewGuid(),
                HoursNo = 40
            };

            Assert.That(command.IsValid);
        }
        [Test]
        public void IsInvalid_HasNoSubjectGuid()
        {
            var command = new EducationCycleStepSubjectCommand()
            {
                HoursNo = 40
            };

            Assert.That(!command.IsValid);
        }
        [Test]
        public void IsInvalid_HasNoHours()
        {
            var command = new EducationCycleStepSubjectCommand()
            {
                SubjectGuid = Guid.NewGuid(),
            };

            Assert.That(!command.IsValid);
        }
        [Test]
        public void IsInvalid_Has0Hours()
        {
            var command = new EducationCycleStepSubjectCommand()
            {
                HoursNo = 0,
                SubjectGuid = Guid.NewGuid(),
            };

            Assert.That(!command.IsValid);
        }
        [Test]
        public void IsInvalid_HasNegativeHours()
        {
            var command = new EducationCycleStepSubjectCommand()
            {
                HoursNo = -5,
                SubjectGuid = Guid.NewGuid(),
            };

            Assert.That(!command.IsValid);
        }
    }
}
