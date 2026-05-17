using System.Diagnostics.CodeAnalysis;
using MADE.Diagnostics.Logging;
using NUnit.Framework;
using Shouldly;

namespace MADE.Diagnostics.Tests.Tests;

[ExcludeFromCodeCoverage]
[TestFixture]
public class FileEventLoggerTests
{
    public class WhenCreating
    {
        [Test]
        public void ShouldHaveEmptyLogPathByDefault()
        {
            // Arrange & Act
            using var logger = new FileEventLogger();

            // Assert
            logger.LogPath.ShouldBeEmpty();
        }

        [Test]
        public void ShouldHaveDefaultLogsFolderName()
        {
            // Arrange & Act
            using var logger = new FileEventLogger();

            // Assert
            logger.LogsFolderName.ShouldBe("Logs");
        }

        [Test]
        public void ShouldHaveDefaultLogFileNameFormat()
        {
            // Arrange & Act
            using var logger = new FileEventLogger();

            // Assert
            logger.LogFileNameFormat.ShouldContain("Log-");
        }
    }

    public class WhenWritingInfo
    {
        [Test]
        public async Task ShouldWriteInfoMessageToFile()
        {
            // Arrange
            string tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            string logFile = Path.Combine(tempDir, "test.log");
            Directory.CreateDirectory(tempDir);

            using var logger = new FileEventLogger { LogPath = logFile };

            try
            {
                // Act
                await logger.WriteInfo("Test info message");

                // Assert
                File.Exists(logFile).ShouldBeTrue();
                string content = await File.ReadAllTextAsync(logFile);
                content.ShouldContain("Level: Info");
                content.ShouldContain("Test info message");
            }
            finally
            {
                Directory.Delete(tempDir, true);
            }
        }
    }

    public class WhenWritingWarning
    {
        [Test]
        public async Task ShouldWriteWarningMessageToFile()
        {
            // Arrange
            string tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            string logFile = Path.Combine(tempDir, "test.log");
            Directory.CreateDirectory(tempDir);

            using var logger = new FileEventLogger { LogPath = logFile };

            try
            {
                // Act
                await logger.WriteWarning("Test warning message");

                // Assert
                string content = await File.ReadAllTextAsync(logFile);
                content.ShouldContain("Level: Warning");
                content.ShouldContain("Test warning message");
            }
            finally
            {
                Directory.Delete(tempDir, true);
            }
        }
    }

    public class WhenWritingError
    {
        [Test]
        public async Task ShouldWriteErrorMessageToFile()
        {
            // Arrange
            string tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            string logFile = Path.Combine(tempDir, "test.log");
            Directory.CreateDirectory(tempDir);

            using var logger = new FileEventLogger { LogPath = logFile };

            try
            {
                // Act
                await logger.WriteError("Test error message");

                // Assert
                string content = await File.ReadAllTextAsync(logFile);
                content.ShouldContain("Level: Error");
                content.ShouldContain("Test error message");
            }
            finally
            {
                Directory.Delete(tempDir, true);
            }
        }

        [Test]
        public async Task ShouldWriteErrorWithExceptionToFile()
        {
            // Arrange
            string tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            string logFile = Path.Combine(tempDir, "test.log");
            Directory.CreateDirectory(tempDir);

            using var logger = new FileEventLogger { LogPath = logFile };

            try
            {
                // Act
                await logger.WriteError("Something failed", new InvalidOperationException("bad state"));

                // Assert
                string content = await File.ReadAllTextAsync(logFile);
                content.ShouldContain("Level: Error");
                content.ShouldContain("Something failed");
                content.ShouldContain("bad state");
            }
            finally
            {
                Directory.Delete(tempDir, true);
            }
        }

        [Test]
        public async Task ShouldWriteExceptionOnlyErrorToFile()
        {
            // Arrange
            string tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            string logFile = Path.Combine(tempDir, "test.log");
            Directory.CreateDirectory(tempDir);

            using var logger = new FileEventLogger { LogPath = logFile };

            try
            {
                // Act
                await logger.WriteError(new InvalidOperationException("error only"));

                // Assert
                string content = await File.ReadAllTextAsync(logFile);
                content.ShouldContain("error only");
            }
            finally
            {
                Directory.Delete(tempDir, true);
            }
        }
    }

    public class WhenWritingCritical
    {
        [Test]
        public async Task ShouldWriteCriticalMessageToFile()
        {
            // Arrange
            string tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            string logFile = Path.Combine(tempDir, "test.log");
            Directory.CreateDirectory(tempDir);

            using var logger = new FileEventLogger { LogPath = logFile };

            try
            {
                // Act
                await logger.WriteCritical("Test critical message");

                // Assert
                string content = await File.ReadAllTextAsync(logFile);
                content.ShouldContain("Level: Critical");
                content.ShouldContain("Test critical message");
            }
            finally
            {
                Directory.Delete(tempDir, true);
            }
        }

        [Test]
        public async Task ShouldWriteCriticalWithExceptionToFile()
        {
            // Arrange
            string tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            string logFile = Path.Combine(tempDir, "test.log");
            Directory.CreateDirectory(tempDir);

            using var logger = new FileEventLogger { LogPath = logFile };

            try
            {
                // Act
                await logger.WriteCritical("Critical failure", new Exception("fatal"));

                // Assert
                string content = await File.ReadAllTextAsync(logFile);
                content.ShouldContain("Critical failure");
                content.ShouldContain("fatal");
            }
            finally
            {
                Directory.Delete(tempDir, true);
            }
        }

        [Test]
        public async Task ShouldWriteCriticalExceptionOnlyToFile()
        {
            // Arrange
            string tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            string logFile = Path.Combine(tempDir, "test.log");
            Directory.CreateDirectory(tempDir);

            using var logger = new FileEventLogger { LogPath = logFile };

            try
            {
                // Act
                await logger.WriteCritical(new Exception("critical ex"));

                // Assert
                string content = await File.ReadAllTextAsync(logFile);
                content.ShouldContain("critical ex");
            }
            finally
            {
                Directory.Delete(tempDir, true);
            }
        }
    }

    public class WhenWritingInfoWithException
    {
        [Test]
        public async Task ShouldWriteInfoWithExceptionToFile()
        {
            // Arrange
            string tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            string logFile = Path.Combine(tempDir, "test.log");
            Directory.CreateDirectory(tempDir);

            using var logger = new FileEventLogger { LogPath = logFile };

            try
            {
                // Act
                await logger.WriteInfo("info with error", new Exception("info ex"));

                // Assert
                string content = await File.ReadAllTextAsync(logFile);
                content.ShouldContain("Level: Info");
                content.ShouldContain("info with error");
                content.ShouldContain("info ex");
            }
            finally
            {
                Directory.Delete(tempDir, true);
            }
        }

        [Test]
        public async Task ShouldWriteInfoExceptionOnlyToFile()
        {
            // Arrange
            string tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            string logFile = Path.Combine(tempDir, "test.log");
            Directory.CreateDirectory(tempDir);

            using var logger = new FileEventLogger { LogPath = logFile };

            try
            {
                // Act
                await logger.WriteInfo(new Exception("info exception only"));

                // Assert
                string content = await File.ReadAllTextAsync(logFile);
                content.ShouldContain("info exception only");
            }
            finally
            {
                Directory.Delete(tempDir, true);
            }
        }
    }

    public class WhenWritingWarningWithException
    {
        [Test]
        public async Task ShouldWriteWarningWithExceptionToFile()
        {
            // Arrange
            string tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            string logFile = Path.Combine(tempDir, "test.log");
            Directory.CreateDirectory(tempDir);

            using var logger = new FileEventLogger { LogPath = logFile };

            try
            {
                // Act
                await logger.WriteWarning("warn with error", new Exception("warn ex"));

                // Assert
                string content = await File.ReadAllTextAsync(logFile);
                content.ShouldContain("Level: Warning");
                content.ShouldContain("warn with error");
                content.ShouldContain("warn ex");
            }
            finally
            {
                Directory.Delete(tempDir, true);
            }
        }

        [Test]
        public async Task ShouldWriteWarningExceptionOnlyToFile()
        {
            // Arrange
            string tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            string logFile = Path.Combine(tempDir, "test.log");
            Directory.CreateDirectory(tempDir);

            using var logger = new FileEventLogger { LogPath = logFile };

            try
            {
                // Act
                await logger.WriteWarning(new Exception("warning exception only"));

                // Assert
                string content = await File.ReadAllTextAsync(logFile);
                content.ShouldContain("warning exception only");
            }
            finally
            {
                Directory.Delete(tempDir, true);
            }
        }
    }

    public class WhenDisposing
    {
        [Test]
        public void ShouldNotThrowWhenDisposed()
        {
            // Arrange
            var logger = new FileEventLogger();

            // Act & Assert
            Should.NotThrow(() => logger.Dispose());
        }
    }
}
