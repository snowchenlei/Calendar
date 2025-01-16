using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using System.IO;
using System.Windows;
using Snow.Calendar.Common;
using Snow.Calendar.Common.Service;
using Snow.Calendar.Web.Common;

namespace Snow.Calendar.Desktop;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public IServiceProvider ServiceProvider { get; }

    public App()
    {
        ServiceProvider = ConfigureServices();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }

    private IServiceProvider ConfigureServices()
    {
        var serviceCollection = new ServiceCollection()
            .AddSingleton<IDateHelper, DateHelper>()
            .AddSingleton<IDayHelper, DayHelper>()
            .AddSingleton<IHolidayHelper, HolidayHelper>()
            .AddSingleton<Constellation>()
            .AddSingleton<Resource, LocalResource>()
            .AddSingleton<SolarTerm, SolarTerm>()
            .AddSingleton<ChineseLunisolarCalendar, ChineseLunisolarCalendar>()
            .AddSingleton<ChineseCalendarInfo>()
            .AddSingleton<ICalendarDateHelper, CalendarDateHelper>()
            .AddSingleton<MainWindow>();
        return serviceCollection.BuildServiceProvider();
    }
}