using System.Diagnostics.CodeAnalysis;
using MADE.Diagnostics.Exceptions;
using MADE.Diagnostics.Logging;
using Moq;
using NUnit.Framework;
using Shouldly;

namespace MADE.Diagnostics.Tests.Tests;

[ExcludeFromCodeCoverage]
[TestFixture]
public class AppDiagnosticsTests
{
    public class WhenStartingRecording
    {
        [Test]
        public async Task ShouldSetIsRecordingDiagnosticsToTrue()
        {
            // Arrange
            var mockLogger = new Mock<IEventLogger>();
            mockLogger.Setup(x => x.WriteInfo(It.IsAny<string>())).Returns(Task.CompletedTask);

            var diagnostics = new AppDiagnostics(mockLogger.Object);

            // Act
            await diagnostics.StartRecordingDiagnosticsAsync();

            // Assert
            diagnostics.IsRecordingDiagnostics.ShouldBeTrue();

            // Cleanup
            diagnostics.StopRecordingDiagnostics();
        }

        [Test]
        public async Task ShouldWriteInfoMessageWhenStarted()
        {
            // Arrange
            var mockLogger = new Mock<IEventLogger>();
            mockLogger.Setup(x => x.WriteInfo(It.IsAny<string>())).Returns(Task.CompletedTask);

            var diagnostics = new AppDiagnostics(mockLogger.Object);

            // Act
            await diagnostics.StartRecordingDiagnosticsAsync();

            // Assert
            mockLogger.Verify(x => x.WriteInfo(It.Is<string>(s => s.Contains("initialized"))), Times.Once);

            // Cleanup
            diagnostics.StopRecordingDiagnostics();
        }

        [Test]
        public async Task ShouldNotStartRecordingTwice()
        {
            // Arrange
            var mockLogger = new Mock<IEventLogger>();
            mockLogger.Setup(x => x.WriteInfo(It.IsAny<string>())).Returns(Task.CompletedTask);

            var diagnostics = new AppDiagnostics(mockLogger.Object);

            // Act
            await diagnostics.StartRecordingDiagnosticsAsync();
            await diagnostics.StartRecordingDiagnosticsAsync();

            // Assert
            mockLogger.Verify(x => x.WriteInfo(It.IsAny<string>()), Times.Once);

            // Cleanup
            diagnostics.StopRecordingDiagnostics();
        }
    }

    public class WhenStoppingRecording
    {
        [Test]
        public async Task ShouldSetIsRecordingDiagnosticsToFalse()
        {
            // Arrange
            var mockLogger = new Mock<IEventLogger>();
            mockLogger.Setup(x => x.WriteInfo(It.IsAny<string>())).Returns(Task.CompletedTask);

            var diagnostics = new AppDiagnostics(mockLogger.Object);
            await diagnostics.StartRecordingDiagnosticsAsync();

            // Act
            diagnostics.StopRecordingDiagnostics();

            // Assert
            diagnostics.IsRecordingDiagnostics.ShouldBeFalse();
        }

        [Test]
        public void ShouldNotThrowWhenNotRecording()
        {
            // Arrange
            var mockLogger = new Mock<IEventLogger>();
            var diagnostics = new AppDiagnostics(mockLogger.Object);

            // Act & Assert
            Should.NotThrow(() => diagnostics.StopRecordingDiagnostics());
        }
    }

    public class WhenCreating
    {
        [Test]
        public void ShouldExposeEventLogger()
        {
            // Arrange
            var mockLogger = new Mock<IEventLogger>();

            // Act
            var diagnostics = new AppDiagnostics(mockLogger.Object);

            // Assert
            diagnostics.EventLogger.ShouldBe(mockLogger.Object);
        }

        [Test]
        public void ShouldNotBeRecordingByDefault()
        {
            // Arrange
            var mockLogger = new Mock<IEventLogger>();

            // Act
            var diagnostics = new AppDiagnostics(mockLogger.Object);

            // Assert
            diagnostics.IsRecordingDiagnostics.ShouldBeFalse();
        }
    }
}
