using System.Diagnostics.CodeAnalysis;
using Bogus;

namespace MADE.Collections.Tests.Fakes;

[ExcludeFromCodeCoverage]
public static class TestObservableObjectFaker
{
    public static Faker<TestObservableObject> Create()
    {
        return new Faker<TestObservableObject>().RuleFor(o => o.Name, faker => faker.Name.FullName())
            .RuleFor(o => o.Count, faker => faker.Random.Int(0, 10));
    }
}
