using System.Configuration;
using System.Data;
using System.Windows;
using Syncfusion.Licensing;

namespace MyWeather
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NMaF5cXmBCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdmWX5ceHRXRWZZUUF+X0o=");

            base.OnStartup(e);
        }
    }
}
