namespace MADE.Samples.Features.Samples.Data
{
    using System;

    public class Sample
    {
        public Sample()
        {
        }

        public Sample(string name, Type page, string description, string iconPath)
        {
            this.Name = name;
            this.Page = page;
            this.Description = description;
            this.IconPath = iconPath;
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public string IconPath { get; set; }

        public Type Page { get; set; }
    }
}