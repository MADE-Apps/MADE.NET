namespace MADE.Samples.Features.Samples.Data
{
    using System.Collections.Generic;

    public class SampleGroup
    {
        public string Name { get; set; }

        public IEnumerable<Sample> Samples { get; set; }
    }
}
