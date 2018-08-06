﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BH.oM.Base;
using BH.oM.Geometry;
using BH.oM.Structural.Elements;
using BH.oM.Structural.Properties;
using BH.oM.Common.Materials;
using LusasM15_2;

namespace BH.Engine.Lusas
{
    public static partial class Convert
    {
        /***************************************************/
        /**** Public Methods                            ****/
        /***************************************************/

        //Add methods for converting to BHoM from the specific software types, if possible to do without any BHoM calls
        //Example:
        //public static Node ToBHoM(this LusasNode node)
        //{

        //#region Geometry Converters


        public static Bar ToBHoMObject(this IFLine lusasLine, Dictionary<string, Node> bhomNodes, 
            HashSet<String> groupNames, Dictionary<string, Constraint6DOF> constraints6DOF)
        {

            Node startNode = getNode(lusasLine, 0, bhomNodes);

            Node endNode = getNode(lusasLine, 1, bhomNodes);

            HashSet<String> tags = new HashSet<string>(isMemberOf(lusasLine, groupNames));

            List<String> supportAssignments = attributeAssignments(lusasLine, "Support");

            Constraint6DOF barConstraint = null;
            if(!(supportAssignments.Count()==0))
            {
                constraints6DOF.TryGetValue(supportAssignments[0], out barConstraint);
            }

            Bar bhomBar = new Bar { StartNode = startNode, EndNode = endNode,
                                    Tags = tags,
                                    Constraint = barConstraint};

            String lineName = removePrefix(lusasLine.getName(), "L");

            bhomBar.CustomData["Lusas_id"] = lineName;

            return bhomBar;
        }
    }
}
