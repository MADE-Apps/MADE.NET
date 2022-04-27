namespace MADE.Data.Validation.FluentValidation.Tests.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using global::FluentValidation;
    using global::FluentValidation.Results;
    using MADE.Testing;
    using NUnit.Framework;
    using Shouldly;

    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class FluentValidatorCollectionTests
    {
        public class WhenInitializing
        {
            [Test]
            public void ShouldBeEmptyIfDefaultConstructor()
            {
                // Act
                var collection = new FluentValidatorCollection<Person>();

                // Assert
                collection.Count.ShouldBe(0);
            }

            [Test]
            public void ShouldContainItemsIfInitializedAsEnumerable()
            {
                // Arrange
                IEnumerable<AbstractValidator<Person>> validators = new List<AbstractValidator<Person>>
                {
                    new PersonValidator()
                };

                // Act
                var collection = new FluentValidatorCollection<Person>(validators);

                // Assert
                collection.Count.ShouldBe(validators.Count());
                collection.ToList().ShouldBeEquivalentTo(validators);
            }
        }

        public class WhenAddingItems
        {
            [Test]
            public void ShouldAddRangeOfItems()
            {
                // Arrange
                IEnumerable<AbstractValidator<Person>> validators = new List<AbstractValidator<Person>>
                {
                    new PersonValidator()
                };

                var collection = new FluentValidatorCollection<Person>();

                // Act
                collection.AddRange(validators);

                // Assert
                foreach (AbstractValidator<Person> item in validators)
                {
                    collection.ShouldContain(item);
                }
            }

            [Test]
            public void ShouldAddSingleItem()
            {
                // Arrange
                var objectToAdd = new PersonValidator();

                // Act
                var collection = new FluentValidatorCollection<Person> { objectToAdd };

                // Assert
                collection.ShouldContain(objectToAdd);
            }
        }

        public class WhenValidating
        {
            [Test]
            public void ShouldBeDirtyOnceValidated()
            {
                // Arrange
                var value = new Person();

                var collection = new FluentValidatorCollection<Person> { new PersonValidator() };

                // Act
                collection.Validate(value);

                // Assert
                collection.IsDirty.ShouldBe(true);
            }

            [Test]
            public void ShouldBeValidIfValidValue()
            {
                // Arrange
                var value = new Person { Name = "Joe Bloggs", DateOfBirth = new DateTime(1992, 01, 01) };

                var collection = new FluentValidatorCollection<Person> { new PersonValidator() };

                // Act
                collection.Validate(value);

                // Assert
                collection.IsInvalid.ShouldBe(false);
            }

            [Test]
            public void ShouldBeInvalidIfInvalidValue()
            {
                // Arrange
                var value = new Person();

                var collection = new FluentValidatorCollection<Person> { new PersonValidator() };

                // Act
                collection.Validate(value);

                // Assert
                collection.IsInvalid.ShouldBe(true);
            }

            [Test]
            public void ShouldHaveFeedbackMessagesIfInvalidValue()
            {
                // Arrange
                var value = new Person
                {
                    Name = "Joe Bloggs",
                    DateOfBirth = DateTime.UtcNow.AddDays(1) // Invalid birth date
                };

                var collection = new FluentValidatorCollection<Person> { new PersonValidator() };

                // Act
                collection.Validate(value);

                // Assert
                collection.FeedbackMessages.ShouldNotBeEmpty();
                collection.FeedbackMessages.Count().ShouldBe(1);
                collection.FeedbackMessages.FirstOrDefault().ShouldBe(PersonValidator.DateOfBirthValidationMessage);
            }

            [Test]
            public void ShouldValidateComplexObjectWithMultipleValidators()
            {
                // Arrange
                var value = new Staff
                {
                    Name = "Joe Bloggs",
                    JobTitle = null, // Invalid job title
                    Department = "Build",
                    DateOfBirth = DateTime.UtcNow.AddDays(1) // Invalid birth date
                };

                var collection = new FluentValidatorCollection<Person> { new PersonValidator(), new StaffValidator() };

                // Act
                collection.Validate(value);

                // Assert
                collection.FeedbackMessages.ShouldNotBeEmpty();
                collection.FeedbackMessages.Count().ShouldBe(2);
                collection.FeedbackMessages.FirstOrDefault(x => x.Equals(PersonValidator.DateOfBirthValidationMessage,
                    StringComparison.InvariantCultureIgnoreCase)).ShouldNotBeNull();
                collection.FeedbackMessages.FirstOrDefault(x => x.Equals(StaffValidator.JobTitleValidationMessage,
                    StringComparison.InvariantCultureIgnoreCase)).ShouldNotBeNull();
            }
        }
    }

    public class Person
    {
        public string Name { get; set; }

        public DateTime? DateOfBirth { get; set; }
    }

    public class Staff : Person
    {
        public string JobTitle { get; set; }

        public string Department { get; set; }
    }

    public class PersonValidator : AbstractValidator<Person>
    {
        public const string DateOfBirthValidationMessage = "Please specify a valid date of birth";

        public PersonValidator()
        {
            this.RuleFor(x => x.Name).NotEmpty();
            this.RuleFor(x => x.DateOfBirth)
                .NotNull()
                .LessThanOrEqualTo(DateTime.UtcNow)
                .WithMessage(DateOfBirthValidationMessage);
        }
    }

    public class StaffValidator : AbstractValidator<Staff>, IValidator<Person>
    {
        public const string JobTitleValidationMessage = "Please specify a job title";

        public StaffValidator()
        {
            this.RuleFor(x => x.JobTitle).NotEmpty().WithMessage(JobTitleValidationMessage);
            this.RuleFor(x => x.Department).NotEmpty();
        }

        public ValidationResult Validate(Person instance)
        {
            return base.Validate(instance as Staff);
        }

        public Task<ValidationResult> ValidateAsync(Person instance, CancellationToken cancellation = new())
        {
            return base.ValidateAsync(instance as Staff, cancellation);
        }
    }
}