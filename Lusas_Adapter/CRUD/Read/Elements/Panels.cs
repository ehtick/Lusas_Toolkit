/*
 * This file is part of the Buildings and Habitats object Model (BHoM)
 * Copyright (c) 2015 - 2022, the respective contributors. All rights reserved.
 *
 * Each contributor holds copyright over their respective contributions.
 * The project versioning (Git) records all such contribution source information.
 *                                           
 *                                                                              
 * The BHoM is free software: you can redistribute it and/or modify         
 * it under the terms of the GNU Lesser General Public License as published by  
 * the Free Software Foundation, either version 3.0 of the License, or          
 * (at your option) any later version.                                          
 *                                                                              
 * The BHoM is distributed in the hope that it will be useful,              
 * but WITHOUT ANY WARRANTY; without even the implied warranty of               
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the                 
 * GNU Lesser General Public License for more details.                          
 *                                                                            
 * You should have received a copy of the GNU Lesser General Public License     
 * along with this code. If not, see <https://www.gnu.org/licenses/lgpl-3.0.html>.      
 */

using System.Collections.Generic;
using System.Linq;
using BH.oM.Adapters.Lusas;
using BH.oM.Adapters.Lusas.Fragments;
using BH.oM.Structure.Elements;
using BH.oM.Structure.Constraints;
using BH.oM.Structure.SurfaceProperties;
using BH.oM.Structure.MaterialFragments;
using BH.Engine.Adapter;
using Lusas.LPI;

namespace BH.Adapter.Lusas
{
#if Debug18 || Release18
    public partial class LusasV18Adapter
#elif Debug19 || Release19
    public partial class LusasV19Adapter
#else
    public partial class LusasV17Adapter
#endif
    {
        /***************************************************/
        /**** Private Methods                           ****/
        /***************************************************/

        private List<Panel> ReadPanels(List<string> ids = null)
        {
            object[] lusasSurfaces = d_LusasData.getObjects("Surface");
            List<Panel> panels = new List<Panel>();

            if (!(lusasSurfaces.Count() == 0))
            {
                IEnumerable<Edge> edgesList = ReadEdges();
                Dictionary<string, Edge> edges = edgesList.ToDictionary(
                    x => x.AdapterId<string>(typeof(LusasId)));

                HashSet<string> groupNames = ReadTags();
                IEnumerable<IMaterialFragment> materialList = ReadMaterials();
                Dictionary<string, IMaterialFragment> materials = materialList.ToDictionary(
                    x => x.Name.ToString());

                IEnumerable<ISurfaceProperty> sectionPropertiesList = Read2DProperties();
                Dictionary<string, ISurfaceProperty> sectionProperties = sectionPropertiesList.ToDictionary(
                    x => x.Name.ToString());

                IEnumerable<Constraint4DOF> supportsList = Read4DOFConstraints();
                Dictionary<string, Constraint4DOF> supports = supportsList.ToDictionary(
                    x => x.Name);

                List<MeshSettings2D> meshesList = ReadMeshSettings2D();
                Dictionary<string, MeshSettings2D> meshes = meshesList.ToDictionary(
                    x => x.Name.ToString());

                for (int i = 0; i < lusasSurfaces.Count(); i++)
                {
                    IFSurface lusasSurface = (IFSurface)lusasSurfaces[i];
                    Panel panel = Adapters.Lusas.Convert.ToPanel(lusasSurface,
                        edges,
                        groupNames,
                        sectionProperties,
                        materials,
                        supports,
                        meshes);

                    panels.Add(panel);
                }
            }

            return panels;
        }

        /***************************************************/

    }
}


