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
using BH.Engine.Adapter;
using BH.oM.Adapters.Lusas;
using BH.oM.Structure.Elements;
using BH.oM.Structure.Loads;
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
        private List<ILoad> ReadAreaUniformTemperatureLoads(List<string> ids = null)
        {
            /***************************************************/
            /**** Private Methods                           ****/
            /***************************************************/

            List<ILoad> areaUniformTemperatureLoads = new List<ILoad>();
            object[] lusasTemperatureLoads = d_LusasData.getAttributes("Temperature");

            if (!(lusasTemperatureLoads.Count() == 0))
            {
                List<Panel> panelsList = ReadPanels();
                Dictionary<string, Panel> panels = panelsList.ToDictionary(
                    x => x.AdapterId<string>(typeof(LusasId)));

                List<IFLoadcase> allLoadcases = new List<IFLoadcase>();

                for (int i = 0; i < lusasTemperatureLoads.Count(); i++)
                {
                    IFLoading lusasTemperatureLoad = (IFLoading)lusasTemperatureLoads[i];

                    IEnumerable<IGrouping<string, IFAssignment>> groupedByLoadcases =
                        GetLoadAssignments(lusasTemperatureLoad);

                    foreach (IEnumerable<IFAssignment> groupedAssignment in groupedByLoadcases)
                    {
                        List<IFAssignment> surfaceAssignments = new List<IFAssignment>();

                        foreach (IFAssignment assignment in groupedAssignment)
                        {
                            IFSurface trySurf = assignment.getDatabaseObject() as IFSurface;

                            if (trySurf != null)
                            {
                                surfaceAssignments.Add(assignment);
                            }
                        }

                        List<string> analysisName = new List<string> { lusasTemperatureLoad.getAttributeType() };

                        if (surfaceAssignments.Count != 0)
                        {
                            AreaUniformTemperatureLoad areaUniformTemperatureLoad =
                                Adapters.Lusas.Convert.ToAreaTempratureLoad(
                                    lusasTemperatureLoad, groupedAssignment, panels);

                            areaUniformTemperatureLoad.Tags = new HashSet<string>(analysisName);
                            areaUniformTemperatureLoads.Add(areaUniformTemperatureLoad);
                        }
                    }
                }
            }

            return areaUniformTemperatureLoads;
        }

        /***************************************************/

    }
}


