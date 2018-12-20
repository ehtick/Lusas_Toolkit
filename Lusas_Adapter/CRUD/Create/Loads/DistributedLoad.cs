/*
 * This file is part of the Buildings and Habitats object Model (BHoM)
 * Copyright (c) 2015 - 2018, the respective contributors. All rights reserved.
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

using BH.oM.Geometry;
using BH.oM.Structure.Loads;
using Lusas.LPI;

namespace BH.Adapter.Lusas
{
    public partial class LusasAdapter
    {
        public IFLoadingGlobalDistributed CreateGlobalDistributedLine(BarUniformlyDistributedLoad distributedLoad, IFLine[] lusasLines)
        {
            if (!CheckIllegalCharacters(distributedLoad.Name))
            {
                return null;
            }

            IFLoadcase assignedLoadcase = (IFLoadcase)d_LusasData.getLoadset("Lc" + distributedLoad.Loadcase.CustomData[AdapterId] + "/" + distributedLoad.Loadcase.Name);
            string lusasName = "Dl" + distributedLoad.CustomData[AdapterId] + "/" + distributedLoad.Name;
            NameSearch("Dl", distributedLoad.CustomData[AdapterId].ToString(), distributedLoad.Name, ref lusasName);

            IFLoadingGlobalDistributed lusasGlobalDistributed = CreateGlobalDistributed(lusasName,
                "Length", assignedLoadcase, distributedLoad.Force, distributedLoad.Moment, lusasLines);

            return lusasGlobalDistributed;
        }

        public IFLoadingGlobalDistributed CreateGlobalDistributedLoad(AreaUniformalyDistributedLoad distributedLoad, IFSurface[] lusasSurfaces)
        {
            if (!CheckIllegalCharacters(distributedLoad.Name))
            {
                return null;
            }

            IFLoadcase assignedLoadcase = (IFLoadcase)d_LusasData.getLoadset(
                "Lc" + distributedLoad.Loadcase.CustomData[AdapterId] + "/" + distributedLoad.Loadcase.Name);

            string lusasName = "Dl" + distributedLoad.CustomData[AdapterId] + "/" + distributedLoad.Name;

            IFLoadingGlobalDistributed lusasGlobalDistributed = CreateGlobalDistributed(lusasName,
                "Area", assignedLoadcase, distributedLoad.Pressure, null, lusasSurfaces);

            return lusasGlobalDistributed;
        }

        public IFLoadingLocalDistributed CreateLocalDistributedLine(BarUniformlyDistributedLoad distributedLoad, IFLine[] lusasLines)
        {
            if (!CheckIllegalCharacters(distributedLoad.Name))
            {
                return null;
            }

            IFLoadcase assignedLoadcase = (IFLoadcase)d_LusasData.getLoadset(
                "Lc" + distributedLoad.Loadcase.CustomData[AdapterId] + "/" + distributedLoad.Loadcase.Name);

            string lusasName = "Dl" + distributedLoad.CustomData[AdapterId] + "/" + distributedLoad.Name;

            IFLoadingLocalDistributed lusasLocalDistributed = CreateLocalDistributed(lusasName,
                "Line", assignedLoadcase, distributedLoad.Force, lusasLines);

            return lusasLocalDistributed;
        }

        public IFLoadingLocalDistributed CreateLocalDistributedSurface(AreaUniformalyDistributedLoad distributedLoad, IFSurface[] lusasSurfaces)
        {
            if (!CheckIllegalCharacters(distributedLoad.Name))
            {
                return null;
            }

            IFLoadcase assignedLoadcase = (IFLoadcase)d_LusasData.getLoadset(
                "Lc" + distributedLoad.Loadcase.CustomData[AdapterId] + "/" + distributedLoad.Loadcase.Name);

            string lusasName = "Dl" + distributedLoad.CustomData[AdapterId] + "/" + distributedLoad.Name;

            IFLoadingLocalDistributed lusasLocalDistributed = CreateLocalDistributed(lusasName,
                "Area", assignedLoadcase, distributedLoad.Pressure, lusasSurfaces);

            return lusasLocalDistributed;
        }

        public IFLoadingGlobalDistributed CreateGlobalDistributed(string lusasName,
            string type, IFLoadcase assignedLoadcase, Vector force, Vector moment, object[] lusasGeometry)
        {
            if (!CheckIllegalCharacters(lusasName))
            {
                return null;
            }

            IFLoadingGlobalDistributed lusasGlobalDistributed = null;

            if (d_LusasData.existsAttribute("Loading", lusasName))
            {
                lusasGlobalDistributed = (IFLoadingGlobalDistributed)d_LusasData.getAttribute("Loading",
                    lusasName);
            }
            else
            {
                lusasGlobalDistributed = d_LusasData.createLoadingGlobalDistributed(lusasName);
                if (type == "Length")
                {
                    lusasGlobalDistributed.setGlobalDistributed(type,
                        force.X, force.Y, force.Z, moment.X, moment.Y, moment.Z);
                }
                else if (type == "Area")
                {
                    lusasGlobalDistributed.setGlobalDistributed(type,
                        force.X, force.Y, force.Z);
                }
            }

            IFAssignment lusasAssignment = m_LusasApplication.assignment();
            lusasAssignment.setLoadset(assignedLoadcase);
            lusasGlobalDistributed.assignTo(lusasGeometry, lusasAssignment);

            return lusasGlobalDistributed;
        }

        public IFLoadingLocalDistributed CreateLocalDistributed(string lusasName,
            string type, IFLoadcase assignedLoadcase, Vector force, object[] lusasGeometry)
        {
            if (!CheckIllegalCharacters(lusasName))
            {
                return null;
            }

            IFLoadingLocalDistributed lusasLocalDistributed = null;

            if (d_LusasData.existsAttribute("Loading", lusasName))
            {
                lusasLocalDistributed = (IFLoadingLocalDistributed)d_LusasData.getAttribute("Loading",
                    lusasName);
            }
            else
            {
                lusasLocalDistributed = d_LusasData.createLoadingLocalDistributed(lusasName);
                lusasLocalDistributed.setLocalDistributed(force.X, force.Y, force.Z, type);
            }

            IFAssignment lusasAssignment = m_LusasApplication.assignment();
            lusasAssignment.setLoadset(assignedLoadcase);
            lusasLocalDistributed.assignTo(lusasGeometry, lusasAssignment);

            return lusasLocalDistributed;
        }
    }
}