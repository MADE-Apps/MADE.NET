namespace MADE.Collections.Tests.Fakes
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class TestEqualityObject : IEquatable<TestEqualityObject>
    {
        public string Name { get; set; }

        public int Count { get; set; }

        public bool Equals(TestEqualityObject other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return this.Name == other.Name && this.Count == other.Count;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return this.Equals((TestEqualityObject)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.Name, this.Count);
        }
    }
}