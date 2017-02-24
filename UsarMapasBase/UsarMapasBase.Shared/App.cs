using Esri.ArcGISRuntime;

namespace UsarMapasBase
{
  public class App : Xamarin.Forms.Application
  {
    public App()
    {
      // Deployed applications must be licensed at the Lite level or greater. 
      // See https://developers.arcgis.com/licensing for further details.

      ArcGISRuntimeEnvironment.Initialize();
      MainPage = new Views.MapPage();
    }
  }
}
