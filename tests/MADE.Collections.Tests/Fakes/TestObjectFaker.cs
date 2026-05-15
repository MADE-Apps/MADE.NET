using System.Diagnostics.CodeAnalysis;
using Bogus;

namespace MADE.Collections.Tests.Fakes;

[ExcludeFromCodeCoverage]
public static class TestObjectFaker
{
    public static Faker<TestObject> Create()
    {
        return new Faker<TestObject>()
            .RuleFor(o => o.Name, faker => faker.Name.FullName())
            .RuleFor(o => o.Count, faker => faker.Random.Int(0, 10));
    }
}
