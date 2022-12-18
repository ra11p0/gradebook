using Gradebook.Foundation.Common.Foundation.Commands.Definitions;

namespace Gradebook.Foundation.Tests.Validation;

[Category("Unit")]
public class EducationCycleConfigurationCommandValidationTest
{
    [Test]
    public void IsValid_PartiallyNullStagesDates()
    {
        var command = new EducationCycleConfigurationCommand()
        {
            DateSince = new DateTime(2022, 12, 01),
            DateUntil = new DateTime(2022, 12, 30),
            Stages = new List<EducationCycleConfigurationStageCommand>(){
                new EducationCycleConfigurationStageCommand(){
                    DateSince = new DateTime(2022, 12, 01),
                    DateUntil = new DateTime(2022, 12, 10),
                    Order = 0,
                    Subjects = new List<EducationCycleConfigurationSubjectCommand>(){
                        new EducationCycleConfigurationSubjectCommand(),
                        new EducationCycleConfigurationSubjectCommand(),
                        new EducationCycleConfigurationSubjectCommand()
                    }
                },
                new EducationCycleConfigurationStageCommand(){
                    Order = 1,
                    Subjects = new List<EducationCycleConfigurationSubjectCommand>(){
                        new EducationCycleConfigurationSubjectCommand(),
                        new EducationCycleConfigurationSubjectCommand(),
                        new EducationCycleConfigurationSubjectCommand()
                    }
                },
                 new EducationCycleConfigurationStageCommand(){
                    Order = 2,
                    Subjects = new List<EducationCycleConfigurationSubjectCommand>(){
                        new EducationCycleConfigurationSubjectCommand(),
                        new EducationCycleConfigurationSubjectCommand(),
                        new EducationCycleConfigurationSubjectCommand()
                    }
                }
            }
        };

        Assert.That(command.IsValid);
    }
    [Test]
    public void IsValid_NullStagesDates()
    {
        var command = new EducationCycleConfigurationCommand()
        {
            DateSince = new DateTime(2022, 12, 01),
            DateUntil = new DateTime(2022, 12, 30),
            Stages = new List<EducationCycleConfigurationStageCommand>(){
                new EducationCycleConfigurationStageCommand(){
                    Order = 0,
                    Subjects = new List<EducationCycleConfigurationSubjectCommand>(){
                        new EducationCycleConfigurationSubjectCommand(),
                        new EducationCycleConfigurationSubjectCommand(),
                        new EducationCycleConfigurationSubjectCommand()
                    }
                },
                new EducationCycleConfigurationStageCommand(){
                    Order = 1,
                    Subjects = new List<EducationCycleConfigurationSubjectCommand>(){
                        new EducationCycleConfigurationSubjectCommand(),
                        new EducationCycleConfigurationSubjectCommand(),
                        new EducationCycleConfigurationSubjectCommand()
                    }
                },
                 new EducationCycleConfigurationStageCommand(){
                    Order = 2,
                    Subjects = new List<EducationCycleConfigurationSubjectCommand>(){
                        new EducationCycleConfigurationSubjectCommand(),
                        new EducationCycleConfigurationSubjectCommand(),
                        new EducationCycleConfigurationSubjectCommand()
                    }
                }
            }
        };

        Assert.That(command.IsValid);
    }
    [Test]
    public void IsValid()
    {
        var command = new EducationCycleConfigurationCommand()
        {
            DateSince = new DateTime(2022, 12, 01),
            DateUntil = new DateTime(2022, 12, 30),
            Stages = new List<EducationCycleConfigurationStageCommand>(){
                new EducationCycleConfigurationStageCommand(){
                    Order = 0,
                    DateSince = new DateTime(2022, 12, 01),
                    DateUntil = new DateTime(2022, 12, 10),
                    Subjects = new List<EducationCycleConfigurationSubjectCommand>(){
                        new EducationCycleConfigurationSubjectCommand(),
                        new EducationCycleConfigurationSubjectCommand(),
                        new EducationCycleConfigurationSubjectCommand()
                    }
                },
                new EducationCycleConfigurationStageCommand(){
                    Order = 1,
                    DateSince = new DateTime(2022, 12, 11),
                    DateUntil = new DateTime(2022, 12, 20),
                    Subjects = new List<EducationCycleConfigurationSubjectCommand>(){
                        new EducationCycleConfigurationSubjectCommand(),
                        new EducationCycleConfigurationSubjectCommand(),
                        new EducationCycleConfigurationSubjectCommand()
                    }
                },
                 new EducationCycleConfigurationStageCommand(){
                    Order = 2,
                    DateSince = new DateTime(2022, 12, 21),
                    DateUntil = new DateTime(2022, 12, 30),
                    Subjects = new List<EducationCycleConfigurationSubjectCommand>(){
                        new EducationCycleConfigurationSubjectCommand(),
                        new EducationCycleConfigurationSubjectCommand(),
                        new EducationCycleConfigurationSubjectCommand()
                    }
                }
            }
        };

        Assert.That(command.IsValid);
    }
    [Test]
    public void IsInvalid_EmptySubjectsList()
    {
        var command = new EducationCycleConfigurationCommand()
        {
            DateSince = new DateTime(2022, 12, 01),
            DateUntil = new DateTime(2022, 12, 30),
            Stages = new List<EducationCycleConfigurationStageCommand>(){
                new EducationCycleConfigurationStageCommand(){
                    Order = 0,
                    DateSince = new DateTime(2022, 12, 01),
                    DateUntil = new DateTime(2022, 12, 10),
                    Subjects = new List<EducationCycleConfigurationSubjectCommand>(){}
                },
                new EducationCycleConfigurationStageCommand(){
                    Order = 1,
                    DateSince = new DateTime(2022, 12, 11),
                    DateUntil = new DateTime(2022, 12, 20),
                    Subjects = new List<EducationCycleConfigurationSubjectCommand>(){
                        new EducationCycleConfigurationSubjectCommand(),
                        new EducationCycleConfigurationSubjectCommand(),
                        new EducationCycleConfigurationSubjectCommand()
                    }
                },
                 new EducationCycleConfigurationStageCommand(){
                    Order = 2,
                    DateSince = new DateTime(2022, 12, 21),
                    DateUntil = new DateTime(2022, 12, 30),
                    Subjects = new List<EducationCycleConfigurationSubjectCommand>(){
                        new EducationCycleConfigurationSubjectCommand(),
                        new EducationCycleConfigurationSubjectCommand(),
                        new EducationCycleConfigurationSubjectCommand()
                    }
                }
            }
        };

        Assert.That(!command.IsValid);
    }
    [Test]
    public void IsInvalid_WrongDatesOrder()
    {
        var command = new EducationCycleConfigurationCommand()
        {
            DateSince = new DateTime(2022, 12, 01),
            DateUntil = new DateTime(2022, 12, 30),
            Stages = new List<EducationCycleConfigurationStageCommand>(){
                new EducationCycleConfigurationStageCommand(){
                    Order = 1,
                    DateSince = new DateTime(2022, 12, 01),
                    DateUntil = new DateTime(2022, 12, 10),
                    Subjects = new List<EducationCycleConfigurationSubjectCommand>(){
                        new EducationCycleConfigurationSubjectCommand(),
                        new EducationCycleConfigurationSubjectCommand(),
                        new EducationCycleConfigurationSubjectCommand()
                    }
                },
                new EducationCycleConfigurationStageCommand(){
                    Order = 0,
                    DateSince = new DateTime(2022, 12, 11),
                    DateUntil = new DateTime(2022, 12, 20),
                    Subjects = new List<EducationCycleConfigurationSubjectCommand>(){
                        new EducationCycleConfigurationSubjectCommand(),
                        new EducationCycleConfigurationSubjectCommand(),
                        new EducationCycleConfigurationSubjectCommand()
                    }
                },
                 new EducationCycleConfigurationStageCommand(){
                    Order = 2,
                    DateSince = new DateTime(2022, 12, 21),
                    DateUntil = new DateTime(2022, 12, 30),
                    Subjects = new List<EducationCycleConfigurationSubjectCommand>(){
                        new EducationCycleConfigurationSubjectCommand(),
                        new EducationCycleConfigurationSubjectCommand(),
                        new EducationCycleConfigurationSubjectCommand()
                    }
                }
            }
        };

        Assert.That(!command.IsValid);
    }
    [Test]
    public void IsInvalid_OverlapingDates()
    {
        var command = new EducationCycleConfigurationCommand()
        {
            DateSince = new DateTime(2022, 12, 01),
            DateUntil = new DateTime(2022, 12, 30),
            Stages = new List<EducationCycleConfigurationStageCommand>(){
                new EducationCycleConfigurationStageCommand(){
                    Order = 0,
                    DateSince = new DateTime(2022, 12, 01),
                    DateUntil = new DateTime(2022, 12, 10),
                    Subjects = new List<EducationCycleConfigurationSubjectCommand>(){
                        new EducationCycleConfigurationSubjectCommand(),
                        new EducationCycleConfigurationSubjectCommand(),
                        new EducationCycleConfigurationSubjectCommand()
                    }
                },
                new EducationCycleConfigurationStageCommand(){
                    Order = 1,
                    DateSince = new DateTime(2022, 12, 03),
                    DateUntil = new DateTime(2022, 12, 20),
                    Subjects = new List<EducationCycleConfigurationSubjectCommand>(){
                        new EducationCycleConfigurationSubjectCommand(),
                        new EducationCycleConfigurationSubjectCommand(),
                        new EducationCycleConfigurationSubjectCommand()
                    }
                },
                 new EducationCycleConfigurationStageCommand(){
                    Order = 2,
                    DateSince = new DateTime(2022, 12, 21),
                    DateUntil = new DateTime(2022, 12, 30),
                    Subjects = new List<EducationCycleConfigurationSubjectCommand>(){
                        new EducationCycleConfigurationSubjectCommand(),
                        new EducationCycleConfigurationSubjectCommand(),
                        new EducationCycleConfigurationSubjectCommand()
                    }
                }
            }
        };

        Assert.That(!command.IsValid);
    }
    [Test]
    public void IsInalid_StageStartsBeforeCycle()
    {
        var command = new EducationCycleConfigurationCommand()
        {
            DateSince = new DateTime(2022, 12, 01),
            DateUntil = new DateTime(2022, 12, 30),
            Stages = new List<EducationCycleConfigurationStageCommand>(){
                new EducationCycleConfigurationStageCommand(){
                    Order = 0,
                    DateSince = new DateTime(2022, 11, 01),
                    DateUntil = new DateTime(2022, 12, 10),
                    Subjects = new List<EducationCycleConfigurationSubjectCommand>(){
                        new EducationCycleConfigurationSubjectCommand(),
                        new EducationCycleConfigurationSubjectCommand(),
                        new EducationCycleConfigurationSubjectCommand()
                    }
                },
                new EducationCycleConfigurationStageCommand(){
                    Order = 1,
                    DateSince = new DateTime(2022, 12, 11),
                    DateUntil = new DateTime(2022, 12, 20),
                    Subjects = new List<EducationCycleConfigurationSubjectCommand>(){
                        new EducationCycleConfigurationSubjectCommand(),
                        new EducationCycleConfigurationSubjectCommand(),
                        new EducationCycleConfigurationSubjectCommand()
                    }
                },
                 new EducationCycleConfigurationStageCommand(){
                    Order = 2,
                    DateSince = new DateTime(2022, 12, 21),
                    DateUntil = new DateTime(2022, 12, 30),
                    Subjects = new List<EducationCycleConfigurationSubjectCommand>(){
                        new EducationCycleConfigurationSubjectCommand(),
                        new EducationCycleConfigurationSubjectCommand(),
                        new EducationCycleConfigurationSubjectCommand()
                    }
                }
            }
        };

        Assert.That(!command.IsValid);
    }
    [Test]
    public void IsInvalid_StageEndsAfterCycleEnd()
    {
        var command = new EducationCycleConfigurationCommand()
        {
            DateSince = new DateTime(2022, 12, 01),
            DateUntil = new DateTime(2022, 12, 30),
            Stages = new List<EducationCycleConfigurationStageCommand>(){
                new EducationCycleConfigurationStageCommand(){
                    Order = 0,
                    DateSince = new DateTime(2022, 12, 01),
                    DateUntil = new DateTime(2022, 12, 10),
                    Subjects = new List<EducationCycleConfigurationSubjectCommand>(){
                        new EducationCycleConfigurationSubjectCommand(),
                        new EducationCycleConfigurationSubjectCommand(),
                        new EducationCycleConfigurationSubjectCommand()
                    }
                },
                new EducationCycleConfigurationStageCommand(){
                    Order = 1,
                    DateSince = new DateTime(2022, 12, 11),
                    DateUntil = new DateTime(2022, 12, 20),
                    Subjects = new List<EducationCycleConfigurationSubjectCommand>(){
                        new EducationCycleConfigurationSubjectCommand(),
                        new EducationCycleConfigurationSubjectCommand(),
                        new EducationCycleConfigurationSubjectCommand()
                    }
                },
                 new EducationCycleConfigurationStageCommand(){
                    Order = 2,
                    DateSince = new DateTime(2022, 12, 21),
                    DateUntil = new DateTime(2023, 01, 30),
                    Subjects = new List<EducationCycleConfigurationSubjectCommand>(){
                        new EducationCycleConfigurationSubjectCommand(),
                        new EducationCycleConfigurationSubjectCommand(),
                        new EducationCycleConfigurationSubjectCommand()
                    }
                }
            }
        };

        Assert.That(!command.IsValid);
    }
    [Test]
    public void IsInvalid_EmptyStages()
    {
        var command = new EducationCycleConfigurationCommand()
        {
            DateSince = new DateTime(2022, 12, 01),
            DateUntil = new DateTime(2022, 12, 30),
            Stages = new List<EducationCycleConfigurationStageCommand>() { }
        };

        Assert.That(!command.IsValid);
    }
    [Test]
    public void IsInvalid_CycleEndsBeforeBegins()
    {
        var command = new EducationCycleConfigurationCommand()
        {
            DateSince = new DateTime(2022, 12, 01),
            DateUntil = new DateTime(2022, 11, 30),
            Stages = new List<EducationCycleConfigurationStageCommand>(){
                new EducationCycleConfigurationStageCommand(){
                    Order = 0,
                    DateSince = new DateTime(2022, 12, 01),
                    DateUntil = new DateTime(2022, 12, 10),
                    Subjects = new List<EducationCycleConfigurationSubjectCommand>(){
                        new EducationCycleConfigurationSubjectCommand(),
                        new EducationCycleConfigurationSubjectCommand(),
                        new EducationCycleConfigurationSubjectCommand()
                    }
                },
                new EducationCycleConfigurationStageCommand(){
                    Order = 1,
                    DateSince = new DateTime(2022, 12, 11),
                    DateUntil = new DateTime(2022, 12, 20),
                    Subjects = new List<EducationCycleConfigurationSubjectCommand>(){
                        new EducationCycleConfigurationSubjectCommand(),
                        new EducationCycleConfigurationSubjectCommand(),
                        new EducationCycleConfigurationSubjectCommand()
                    }
                },
                 new EducationCycleConfigurationStageCommand(){
                    Order = 2,
                    DateSince = new DateTime(2022, 12, 21),
                    DateUntil = new DateTime(2022, 12, 30),
                    Subjects = new List<EducationCycleConfigurationSubjectCommand>(){
                        new EducationCycleConfigurationSubjectCommand(),
                        new EducationCycleConfigurationSubjectCommand(),
                        new EducationCycleConfigurationSubjectCommand()
                    }
                }
            }
        };

        Assert.That(!command.IsValid);
    }
    [Test]
    public void IsInvalid_CycleStartsAfterEnd()
    {
        var command = new EducationCycleConfigurationCommand()
        {
            DateSince = new DateTime(2023, 12, 01),
            DateUntil = new DateTime(2022, 12, 30),
            Stages = new List<EducationCycleConfigurationStageCommand>(){
                new EducationCycleConfigurationStageCommand(){
                    Order = 0,
                    DateSince = new DateTime(2022, 12, 01),
                    DateUntil = new DateTime(2022, 12, 10),
                    Subjects = new List<EducationCycleConfigurationSubjectCommand>(){
                        new EducationCycleConfigurationSubjectCommand(),
                        new EducationCycleConfigurationSubjectCommand(),
                        new EducationCycleConfigurationSubjectCommand()
                    }
                },
                new EducationCycleConfigurationStageCommand(){
                    Order = 1,
                    DateSince = new DateTime(2022, 12, 11),
                    DateUntil = new DateTime(2022, 12, 20),
                    Subjects = new List<EducationCycleConfigurationSubjectCommand>(){
                        new EducationCycleConfigurationSubjectCommand(),
                        new EducationCycleConfigurationSubjectCommand(),
                        new EducationCycleConfigurationSubjectCommand()
                    }
                },
                 new EducationCycleConfigurationStageCommand(){
                    Order = 2,
                    DateSince = new DateTime(2022, 12, 21),
                    DateUntil = new DateTime(2022, 12, 30),
                    Subjects = new List<EducationCycleConfigurationSubjectCommand>(){
                        new EducationCycleConfigurationSubjectCommand(),
                        new EducationCycleConfigurationSubjectCommand(),
                        new EducationCycleConfigurationSubjectCommand()
                    }
                }
            }
        };

        Assert.That(!command.IsValid);
    }
    [Test]
    public void IsInvalid_StageEndsBeforeBegins()
    {
        var command = new EducationCycleConfigurationCommand()
        {
            DateSince = new DateTime(2022, 12, 01),
            DateUntil = new DateTime(2022, 12, 30),
            Stages = new List<EducationCycleConfigurationStageCommand>(){
                new EducationCycleConfigurationStageCommand(){
                    Order = 0,
                    DateSince = new DateTime(2022, 12, 01),
                    DateUntil = new DateTime(2022, 11, 10),
                    Subjects = new List<EducationCycleConfigurationSubjectCommand>(){
                        new EducationCycleConfigurationSubjectCommand(),
                        new EducationCycleConfigurationSubjectCommand(),
                        new EducationCycleConfigurationSubjectCommand()
                    }
                },
                new EducationCycleConfigurationStageCommand(){
                    Order = 1,
                    DateSince = new DateTime(2022, 12, 11),
                    DateUntil = new DateTime(2022, 12, 20),
                    Subjects = new List<EducationCycleConfigurationSubjectCommand>(){
                        new EducationCycleConfigurationSubjectCommand(),
                        new EducationCycleConfigurationSubjectCommand(),
                        new EducationCycleConfigurationSubjectCommand()
                    }
                },
                 new EducationCycleConfigurationStageCommand(){
                    Order = 2,
                    DateSince = new DateTime(2022, 12, 21),
                    DateUntil = new DateTime(2022, 12, 30),
                    Subjects = new List<EducationCycleConfigurationSubjectCommand>(){
                        new EducationCycleConfigurationSubjectCommand(),
                        new EducationCycleConfigurationSubjectCommand(),
                        new EducationCycleConfigurationSubjectCommand()
                    }
                }
            }
        };

        Assert.That(!command.IsValid);
    }
    [Test]
    public void IsInvalid_StageBeginsAfterEnds()
    {
        var command = new EducationCycleConfigurationCommand()
        {
            DateSince = new DateTime(2022, 12, 01),
            DateUntil = new DateTime(2022, 12, 30),
            Stages = new List<EducationCycleConfigurationStageCommand>(){
                new EducationCycleConfigurationStageCommand(){
                    Order = 0,
                    DateSince = new DateTime(2023, 12, 01),
                    DateUntil = new DateTime(2022, 12, 10),
                    Subjects = new List<EducationCycleConfigurationSubjectCommand>(){
                        new EducationCycleConfigurationSubjectCommand(),
                        new EducationCycleConfigurationSubjectCommand(),
                        new EducationCycleConfigurationSubjectCommand()
                    }
                },
                new EducationCycleConfigurationStageCommand(){
                    Order = 1,
                    DateSince = new DateTime(2022, 12, 11),
                    DateUntil = new DateTime(2022, 12, 20),
                    Subjects = new List<EducationCycleConfigurationSubjectCommand>(){
                        new EducationCycleConfigurationSubjectCommand(),
                        new EducationCycleConfigurationSubjectCommand(),
                        new EducationCycleConfigurationSubjectCommand()
                    }
                },
                 new EducationCycleConfigurationStageCommand(){
                    Order = 2,
                    DateSince = new DateTime(2022, 12, 21),
                    DateUntil = new DateTime(2022, 12, 30),
                    Subjects = new List<EducationCycleConfigurationSubjectCommand>(){
                        new EducationCycleConfigurationSubjectCommand(),
                        new EducationCycleConfigurationSubjectCommand(),
                        new EducationCycleConfigurationSubjectCommand()
                    }
                }
            }
        };

        Assert.That(!command.IsValid);
    }
}
