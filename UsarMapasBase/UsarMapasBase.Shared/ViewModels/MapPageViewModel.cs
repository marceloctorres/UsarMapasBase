using Esri.ArcGISRuntime.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Prism.Mvvm;
using Prism.Commands;
using Xamarin.Forms;
using Esri.ArcGISRuntime.Portal;

namespace UsarMapasBase.ViewModels
{
  public class MapPageViewModel : BindableBase
  {

    private Map _map = new Map(Basemap.CreateStreetsVector());
    private Boolean _isBaseMapListVisible = false;

    /// <summary>
    /// 
    /// </summary>
    public string[] BasemapChoices { get; private set; }

    /// <summary>
    /// Gets or sets the map
    /// </summary>
    public Map Map
    {
      get { return _map; }
      set { this.SetProperty<Map>(ref this._map, value);  }
    }

    /// <summary>
    /// 
    /// </summary>
    public Boolean IsBasemapsListVisible
    {
      get { return this._isBaseMapListVisible; }
      set { SetProperty<Boolean>(ref this._isBaseMapListVisible, value); }
    }

    /// <summary>
    /// /
    /// </summary>
    public DelegateCommand BaseMapCommand { get; private set; }

    /// <summary>
    /// 
    /// </summary>
    public DelegateCommand<string> ChangeBaseMapCommand { get; private set; }

    /// <summary>
    /// 
    /// </summary>
    public MapPageViewModel()
    {

      this.BaseMapCommand = new DelegateCommand(BaseMapAction);
      this.ChangeBaseMapCommand = new DelegateCommand<string>(ChangeBaseMapAction);
      this.BasemapChoices = new string[]
      {
        "Topográfico",
        "Vector Topográfico",
        "Calles",
        "Vector Calles",
        "Imágenes",
        "Océanos",
        "USGS National Map",
        "World Globe 1812"
      };
    }

    /// <summary>
    /// 
    /// </summary>
    private void BaseMapAction()
    {
      this.IsBasemapsListVisible = true;
    }


    /// <summary>
    /// 
    /// </summary>
    private async void ChangeBaseMapAction(string baseMap)
    {
      switch (baseMap)
      {
        case "Topográfico":
          _map.Basemap = Basemap.CreateTopographic();
          break;
        case "Vector Topográfico":
          _map.Basemap = Basemap.CreateTopographicVector();
          break;
        case "Topográico":
          _map.Basemap = Basemap.CreateTopographic();
          break;
        case "Calles":
          _map.Basemap = Basemap.CreateStreets();
          break;
        case "Vector Calles":
          _map.Basemap = Basemap.CreateStreetsVector();
          break;
        case "Imágenes":
          _map.Basemap = Basemap.CreateImagery();
          break;
        case "Océanos":
          _map.Basemap = Basemap.CreateOceans();
          break;
        case "USGS National Map":
          // Set the basemap to USGS National Map by opening it from ArcGIS Online
          var itemID = "809d37b42ca340a48def914df43e2c31";

          // Connect to ArcGIS Online
          ArcGISPortal agsOnline = await ArcGISPortal.CreateAsync();

          // Get the USGS webmap item
          PortalItem usgsMapItem = await PortalItem.CreateAsync(agsOnline, itemID);

          // Create a new basemap using the item
          _map.Basemap = new Basemap(usgsMapItem);
          break;
        case "World Globe 1812":
          // Create a URI that points to a map service to use in the basemap
          var uri = new Uri("https://tiles.arcgis.com/tiles/IEuSomXfi6iB7a25/arcgis/rest/services/World_Globe_1812/MapServer");

          // Create an ArcGISTiledLayer from the URI
          ArcGISTiledLayer baseLayer = new ArcGISTiledLayer(uri);

          // Create a basemap from the layer and assign it to the map
          _map.Basemap = new Basemap(baseLayer);
          break;
      }
      this.IsBasemapsListVisible = false;
    }
  }
}
