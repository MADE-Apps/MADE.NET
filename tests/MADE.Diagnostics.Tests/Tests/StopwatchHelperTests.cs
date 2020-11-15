namespace MADE.Diagnostics.Tests.Tests
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using NUnit.Framework;
    using Shouldly;

    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class StopwatchHelperTests
    {
        public class WhenRunning
        {
            [Test]
            public void ShouldReturnMessageForStopwatchUsingFilePathAndMember()
            {
                // Act
                string startMessage = StopwatchHelper.Start();
                (string stopMessage, TimeSpan time) = StopwatchHelper.Stop();

                // Assert
                startMessage.ShouldContain(nameof(this.ShouldReturnMessageForStopwatchUsingFilePathAndMember));
                stopMessage.ShouldContain(nameof(this.ShouldReturnMessageForStopwatchUsingFilePathAndMember));
                stopMessage.ShouldContain(time.ToString());
                time.TotalMilliseconds.ShouldBeGreaterThan(0);
            }

            [Test]
            public void ShouldReturnMessageForStopwatchUsingProvidedPathAndMember()
            {
                // Act
                string caller = nameof(StopwatchHelperTests);
                string name = nameof(this.ShouldReturnMessageForStopwatchUsingProvidedPathAndMember);

                string startMessage = StopwatchHelper.Start(caller, name);
                (string stopMessage, TimeSpan time) = StopwatchHelper.Stop(caller, name);

                // Assert
                startMessage.ShouldContain(caller);
                startMessage.ShouldContain(name);
                stopMessage.ShouldContain(caller);
                stopMessage.ShouldContain(name);
                stopMessage.ShouldContain(time.ToString());
                time.TotalMilliseconds.ShouldBeGreaterThan(0);
            }

            [Test]
            public void ShouldNotStartMultipleStopwatchForSameKey()
            {
                // Act
                string caller = nameof(StopwatchHelperTests);
                string name = nameof(this.ShouldReturnMessageForStopwatchUsingProvidedPathAndMember);

                StopwatchHelper.Start(caller, name);
                string duplicateStartMessage = StopwatchHelper.Start(caller, name);

                StopwatchHelper.Stop(caller, name);

                // Assert
                duplicateStartMessage.ShouldBeNull();
            }

            [Test]
            public void ShouldNotStopNonExistentStopwatch()
            {
                // Act
                string caller = nameof(StopwatchHelperTests);
                string name = nameof(this.ShouldReturnMessageForStopwatchUsingProvidedPathAndMember);

                (string stopMessage, TimeSpan time) = StopwatchHelper.Stop(caller, name);

                // Assert
                stopMessage.ShouldBeNull();
                time.TotalMilliseconds.ShouldBe(0);
            }

            [Test]
            public void ShouldNotStopAlreadyStoppedStopwatch()
            {
                // Act
                string caller = nameof(StopwatchHelperTests);
                string name = nameof(this.ShouldReturnMessageForStopwatchUsingProvidedPathAndMember);

                StopwatchHelper.Start(caller, name);
                StopwatchHelper.Stop(caller, name);
                (string duplicateStopMessage, TimeSpan duplicateStopTime) = StopwatchHelper.Stop(caller, name);

                // Assert
                duplicateStopMessage.ShouldBeNull();
                duplicateStopTime.TotalMilliseconds.ShouldBe(0);
            }
        }
    }
}