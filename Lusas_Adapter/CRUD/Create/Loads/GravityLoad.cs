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

using BH.oM.Adapters.Lusas;
using BH.oM.Structure.Loads;
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

        private IFLoadingBody CreateGravityLoad(GravityLoad gravityLoad, IFGeometry[] lusasGeometry)
        {
            IFLoadingBody lusasGravityLoad;
            IFLoadcase assignedLoadcase = (IFLoadcase)d_LusasData.getLoadset(gravityLoad.Loadcase.AdapterId<int>(typeof(LusasId)));

            if (d_LusasData.existsAttribute("Loading", gravityLoad.Name))
            {
                lusasGravityLoad = (IFLoadingBody)d_LusasData.getAttributes("Loading", gravityLoad.Name);
            }
            else
            {
                lusasGravityLoad = d_LusasData.createLoadingBody(gravityLoad.Name);
                lusasGravityLoad.setBody(gravityLoad.GravityDirection.X, gravityLoad.GravityDirection.Y, gravityLoad.GravityDirection.Z);
            }

            IFAssignment lusasAssignment = m_LusasApplication.assignment();
            lusasAssignment.setLoadset(assignedLoadcase);
            lusasGravityLoad.assignTo(lusasGeometry, lusasAssignment);

            int adapterIdName = lusasGravityLoad.getID();
            gravityLoad.SetAdapterId(typeof(LusasId), adapterIdName);

            return lusasGravityLoad;
        }

        /***************************************************/

    }
}



