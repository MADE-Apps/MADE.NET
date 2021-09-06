namespace MADE.Samples.Features.Samples.Data
{
    using System;

    public class Sample
    {
        public Sample()
        {
        }

        public Sample(string name, Type page, string iconPath)
        {
            this.Name = name;
            this.Page = page;
            this.IconPath = iconPath;
        }

        public string Name { get; set; }

        public string IconPath { get; set; }

        public Type Page { get; set; }
    }
}