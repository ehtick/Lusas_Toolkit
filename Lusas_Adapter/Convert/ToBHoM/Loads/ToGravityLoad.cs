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
using BH.oM.Geometry;
using BH.oM.Structure.Elements;
using BH.oM.Structure.Loads;
using BH.oM.Base;
using Lusas.LPI;
using BH.Engine.Adapter;

namespace BH.Adapter.Adapters.Lusas
{
    public static partial class Convert
    {
        /***************************************************/
        /**** Public Methods                            ****/
        /***************************************************/

        public static GravityLoad ToGravityLoad(IFLoading lusasGravityLoad,
            IEnumerable<IFAssignment> lusasAssignments,
            Dictionary<string, Node> nodes,
            Dictionary<string, Bar> bars,
            Dictionary<string, Panel> panels)
        {
            IFLoadcase assignedLoadcase = (IFLoadcase)lusasAssignments.First().getAssignmentLoadset();
            Loadcase loadcase = ToLoadcase(assignedLoadcase);
            Vector gravityVector = new Vector
            {
                X = lusasGravityLoad.getValue("accX"),
                Y = lusasGravityLoad.getValue("accY"),
                Z = lusasGravityLoad.getValue("accZ")
            };

            IEnumerable<BHoMObject> assignedObjects = GetGeometryAssignments(
                lusasAssignments, nodes, bars, panels);
            GravityLoad gravityLoad = Engine.Structure.Create.GravityLoad(
                loadcase, gravityVector, assignedObjects, GetName(lusasGravityLoad));

            int adapterNameId = lusasGravityLoad.getID();
            gravityLoad.SetAdapterId(typeof(LusasId), adapterNameId);

            return gravityLoad;
        }

        /***************************************************/
        /**** Private Methods                           ****/
        /***************************************************/

        public static IEnumerable<BHoMObject> GetGeometryAssignments(IEnumerable<IFAssignment> lusasAssignments,
            Dictionary<string, Node> nodes, Dictionary<string, Bar> bars,
            Dictionary<string, Panel> panels)
        {
            List<BHoMObject> assignedObjects = new List<BHoMObject>();

            Node node;
            Bar bar;
            Panel panel;

            foreach (IFAssignment lusasAssignment in lusasAssignments)
            {
                IFGeometry lusasGeometry = (IFGeometry)lusasAssignment.getDatabaseObject();

                if (lusasGeometry is IFPoint)
                {
                    nodes.TryGetValue(lusasGeometry.getID().ToString(), out node);
                    assignedObjects.Add(node);
                }
                else if (lusasGeometry is IFLine)
                {
                    bars.TryGetValue(lusasGeometry.getID().ToString(), out bar);
                    assignedObjects.Add(bar);
                }
                else if (lusasGeometry is IFSurface)
                {
                    panels.TryGetValue(lusasGeometry.getID().ToString(), out panel);
                    assignedObjects.Add(panel);
                }
            }

            return assignedObjects;
        }

        /***************************************************/

    }
}


