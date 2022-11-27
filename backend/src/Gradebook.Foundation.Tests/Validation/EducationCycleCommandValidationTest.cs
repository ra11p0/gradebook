using Gradebook.Foundation.Common.Foundation.Commands.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gradebook.Foundation.Tests.Validation
{
    internal class EducationCycleCommandValidationTest
    {
        [Test]
        public void IsValid()
        {
            var command = new EducationCycleCommand()
            {
                SchoolGuid = Guid.NewGuid(),
                Name = "Fake name",
                Stages = new List<EducationCycleStepCommand> {
                    new EducationCycleStepCommand()
                    {
                        Name="Fake name",
                        Subjects = new List<EducationCycleStepSubjectCommand>(){
                            new EducationCycleStepSubjectCommand(){
                            SubjectGuid= Guid.NewGuid(),
                            HoursNo = 40
                            }
                        }
                    }
                }
            };

            Assert.That(command.IsValid);
        }
        [Test]
        public void IsInvalid_HasEmptyName()
        {
            var command = new EducationCycleCommand()
            {
                SchoolGuid = Guid.NewGuid(),
                Name = "",
                Stages = new List<EducationCycleStepCommand> {
                    new EducationCycleStepCommand()
                    {
                        Name="Fake name",
                        Subjects = new List<EducationCycleStepSubjectCommand>(){
                            new EducationCycleStepSubjectCommand(){
                            SubjectGuid= Guid.NewGuid(),
                            HoursNo = 40
                            }
                        }
                    }
                }
            };

            Assert.That(!command.IsValid);
        }
        [Test]
        public void IsInvalid_HasNullName()
        {
            var command = new EducationCycleCommand()
            {
                SchoolGuid = Guid.NewGuid(),
                Stages = new List<EducationCycleStepCommand> {
                    new EducationCycleStepCommand()
                    {
                        Name="Fake name",
                        Subjects = new List<EducationCycleStepSubjectCommand>(){
                            new EducationCycleStepSubjectCommand(){
                            SubjectGuid= Guid.NewGuid(),
                            HoursNo = 40
                            }
                        }
                    }
                }
            };

            Assert.That(!command.IsValid);
        }
        [Test]
        public void IsInvalid_HasNoStages()
        {
            var command = new EducationCycleCommand()
            {
                SchoolGuid = Guid.NewGuid(),
                Name = "Fake name",
                Stages = new List<EducationCycleStepCommand> {}
            };

            Assert.That(!command.IsValid);
        }
        [Test]
        public void IsInvalid_HasNullStages()
        {
            var command = new EducationCycleCommand()
            {
                Name = "Fake name",
                SchoolGuid = Guid.NewGuid(),
            };

            Assert.That(!command.IsValid);
        }
        [Test]
        public void IsInvalid_HasNoSchoolGuid()
        {
            var command = new EducationCycleCommand()
            {
                Name = "Fake name",
                Stages = new List<EducationCycleStepCommand> {
                    new EducationCycleStepCommand()
                    {
                        Name="Fake name",
                        Subjects = new List<EducationCycleStepSubjectCommand>(){
                            new EducationCycleStepSubjectCommand(){
                            SubjectGuid= Guid.NewGuid(),
                            HoursNo = 40
                            }
                        }
                    }
                }
            };

            Assert.That(!command.IsValid);
        }
    }
}
