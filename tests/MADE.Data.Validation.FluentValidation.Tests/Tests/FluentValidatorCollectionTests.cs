namespace MADE.Data.Validation.FluentValidation.Tests.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using global::FluentValidation;
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
        }
    }

    public class Person
    {
        public string Name { get; set; }

        public DateTime? DateOfBirth { get; set; }
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
}