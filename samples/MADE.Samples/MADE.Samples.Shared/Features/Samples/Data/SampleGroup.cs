namespace MADE.Samples.Features.Samples.Data
{
    using System.Collections.Generic;

    public class SampleGroup
    {
        public string Name { get; set; }

        public IList<Sample> Samples { get; } = new List<Sample>();
    }
}
