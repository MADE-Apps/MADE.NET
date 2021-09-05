namespace MADE.Samples.Features.Samples.Data
{
    using System.Collections.Generic;

    public class SampleGroup
    {
        public string Name { get; set; }

        public ICollection<Sample> Samples { get; } = new List<Sample>();
    }
}
