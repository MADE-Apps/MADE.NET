namespace MADE.Data.EFCore.Converters
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

    /// <summary>
    /// Defines a converter to help with the storing of dates in a UTC format.
    /// </summary>
    public static class UtcDateTimeConverter
    {
        internal static readonly ValueConverter<DateTime, DateTime> UtcConverter =
            new ValueConverter<DateTime, DateTime>(
                value => value,
                value => DateTime.SpecifyKind(value, DateTimeKind.Utc));

        private const string IsUtcAnnotation = "IsUtc";

        /// <summary>
        /// Defines an annotation on a property that it should be in a UTC format.
        /// <para>
        /// The intended use for this is on properties which are a <see cref="DateTime"/> or DateTime?.
        /// </para>
        /// </summary>
        /// <param name="builder">The property builder.</param>
        /// <param name="isUtc">A value indicating whether the property value should be in UTC format.</param>
        /// <typeparam name="TProperty">The type of property.</typeparam>
        /// <returns>The configured property builder.</returns>
        public static PropertyBuilder<TProperty> IsUtc<TProperty>(
            this PropertyBuilder<TProperty> builder,
            bool isUtc = true)
        {
            return builder.HasAnnotation(IsUtcAnnotation, isUtc);
        }

        /// <summary>
        /// Determines whether the <paramref name="property"/> has the IsUtc annotation.
        /// </summary>
        /// <param name="property">The property to check.</param>
        /// <returns>A value indicating whether the property has the IsUtc annotation.</returns>
        public static bool IsUtc(this IMutableProperty property)
        {
            return (bool?)property.FindAnnotation(IsUtcAnnotation)?.Value ?? false;
        }

        /// <summary>
        /// Applies a UTC <see cref="DateTime"/> converter to the <paramref name="builder"/>.
        /// </summary>
        /// <param name="builder">The model builder to apply the converter to.</param>
        public static void ApplyUtcDateTimeConverter(this ModelBuilder builder)
        {
            foreach (IMutableEntityType entityType in builder.Model.GetEntityTypes())
            {
                foreach (IMutableProperty property in entityType.GetProperties())
                {
                    if (!property.IsUtc())
                    {
                        continue;
                    }

                    if (property.ClrType == typeof(DateTime) || property.ClrType == typeof(DateTime?))
                    {
                        property.SetValueConverter(UtcConverter);
                    }
                }
            }
        }
    }
}