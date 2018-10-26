﻿using System.Collections.Generic;
using System.Linq;
using System;
using BH.oM.Base;
using BH.oM.Structure.Elements;
using BH.oM.Geometry;
using BH.oM.Structure.Properties;
using BH.oM.Structure.Loads;
using BH.oM.Common.Materials;
using BH.Engine.Geometry;
using BH.Engine.Structure;
using BH.Engine.Lusas;
using Lusas.LPI;

namespace BH.Adapter.Lusas
{
    public partial class LusasAdapter
    {
        public IFLocalCoord CreateLocalCoordinate(IFLine lusasLine)
        {
            object lineXAxis = null;
            object lineYAxis = null;
            object lineZAxis = null;
            object origin = null;

            lusasLine.getAxesAtNrmCrds(0, ref origin, ref lineXAxis, ref lineYAxis, ref lineZAxis);

            double[] localXAxis = ConvertToDouble(lineXAxis);
            double[] localYAxis = ConvertToDouble(lineYAxis);
            double[] localZAxis = ConvertToDouble(lineZAxis);

            IF3dCoords barStart = lusasLine.getStartPositionCoords();

            double[] worldXAxis = new double[] { 1, 0, 0 };
            double[] worldYAxis = new double[] { 0, 1, 0 };
            double[] worldZAxis = new double[] { 0, 0, 1 };

            double[] barorigin = new double[] { barStart.getX(), barStart.getY(), barStart.getZ() };

            double[] matrixCol0 = new double[]
            {
                worldXAxis.Zip(localXAxis, (d1,d2) => d1 * d2).Sum(),
                worldYAxis.Zip(localXAxis, (d1,d2) => d1 * d2).Sum(),
                worldZAxis.Zip(localXAxis, (d1,d2) => d1 * d2).Sum(),
            };

            double[] matrixCol1 = new double[]
            {
                worldXAxis.Zip(localYAxis, (d1,d2) => d1 * d2).Sum(),
                worldYAxis.Zip(localYAxis, (d1,d2) => d1 * d2).Sum(),
                worldZAxis.Zip(localYAxis, (d1,d2) => d1 * d2).Sum(),
            };

            double[] matrixCol2 = new double[]
            {
                worldXAxis.Zip(localZAxis, (d1,d2) => d1 * d2).Sum(),
                worldYAxis.Zip(localZAxis, (d1,d2) => d1 * d2).Sum(),
                worldZAxis.Zip(localZAxis, (d1,d2) => d1 * d2).Sum(),
            };

            string customID = BH.Engine.Lusas.Convert.removePrefix(lusasLine.getName(),"L");

            string lusasAttributeName = "L" + customID + "/ Local Axis";



            IFLocalCoord barLocalAxis = d_LusasData.createLocalCartesianAttr(
            lusasAttributeName,
            barorigin,
            matrixCol0,
            matrixCol1,
            matrixCol2
            );

            return barLocalAxis;
        }
    }
}
