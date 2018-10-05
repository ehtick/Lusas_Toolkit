# Lusas Toolkit 
## System Requirements
- Lusas Modeller v17.0
- Microsoft Visual Studio Community 2015 or higher
- GitHub for Desktop (Windows)
- Microsoft .NET Framework 4.6 and above (included with Visual Studio Community 2015)
- BHoM Release 2.0

## What is supported?

Everything listed below can be pushed and pulled from Grasshopper to Lusas.

The format is as follows: Lusas Object (BHoM Object)

BHoM object are constainted within the `Structure` namespace unless specified otherwise.

### Geometry
- `Point` (`Elements.Node`)
- `Line` (`Elements.Bar`)
- `Surface` (`Elements.PanelPlanar`)

### Attributes
#### Supports
1. Point
- `Support` (`Properties.Constraint6DOF`)

#### Materials
- `Material` (`Common.Materials.Material`)

#### Loading
- `Concentrated Load` (`Loads.PointForce`)
- `Body Force` (`Loads.GravityLoad`)
- `Global Distributed Load` 
  - Bar: (`Loads.BarUniformlyDistributedLoad` - set `LoadAxis.Global`)
  - PanelPlanar: (`Loads.AreaUniformlyDistributedLoad` - set `LoadAxis.Global`)
- `Local Distributed Load` 
  - Bar: (`Loads.BarUniformlyDistributedLoad` - set `LoadAxis.Local`)
  - PanelPlanar: (`Loads.AreaUniformlyDistributedLoad` - set `LoadAxis.Local`)
- `Temperature Load` 
  - Bar: (`Loads.BarTemperatureLoad`)
  - PanelPlanar: (`Loads.AreaTemperatureLoad`)

### Post-Processing
### Load Combinations
- `Basic Combination` (`Loads.LoadCombination`)

## What is currently being developed?

Check the Cities Planning board to see what is being developed:

https://github.com/orgs/BuroHappoldEngineering/projects/7

Feel free to add any features you would like to see.
