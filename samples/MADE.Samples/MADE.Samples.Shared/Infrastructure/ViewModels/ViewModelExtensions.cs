namespace MADE.Samples.Infrastructure.ViewModels
{
    using MADE.Samples.Features.Home.ViewModels;
    using Microsoft.Extensions.DependencyInjection;

    public static class ViewModelExtensions
    {
        public static IServiceCollection AddViewModels(this IServiceCollection services)
        {
            services.AddSingleton<MainPageViewModel>();

            return services;
        }
    }
}
