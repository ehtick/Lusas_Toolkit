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

using System;
using System.Collections.Generic;
using BH.oM.Structure.Loads;

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

        private List<ILoad> ChooseLoad(Type type, List<string> ids = null)
        {
            List<ILoad> readLoads = null;
            string typeName = type.Name;
            switch (typeName)
            {
                case "PointLoad":
                    readLoads = ReadPointLoads(ids as dynamic);
                    break;
                case "GravityLoad":
                    readLoads = ReadGravityLoads(ids as dynamic);
                    break;
                case "BarUniformlyDistributedLoad":
                    readLoads = ReadBarUniformlyDistributedLoads(ids as dynamic);
                    break;
                case "AreaUniformlyDistributedLoad":
                    readLoads = ReadAreaUniformlyDistributedLoads(ids as dynamic);
                    break;
                case "BarUniformTemperatureLoad":
                    readLoads = ReadBarUniformTemperatureLoads(ids as dynamic);
                    break;
                case "AreaUniformTemperatureLoad":
                    readLoads = ReadAreaUniformTemperatureLoads(ids as dynamic);
                    break;
                case "PointDisplacement":
                    readLoads = ReadPointDisplacements(ids as dynamic);
                    break;
                case "BarPointLoad":
                    readLoads = ReadBarPointLoads(ids as dynamic);
                    break;
                case "BarVaryingDistributedLoad":
                    readLoads = ReadBarVaryingDistributedLoads(ids as dynamic);
                    break;
            }

            return readLoads;
        }

        /***************************************************/

    }
}



