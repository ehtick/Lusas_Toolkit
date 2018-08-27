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
using Lusas.LPI;

namespace BH.Adapter.Lusas
{
    public partial class LusasAdapter
    {
        public void assignObjectSet(IFGeometry newGeometry, HashSet<String> tags)
        {
            foreach (string tag in tags)
            {
                IFObjectSet objectSet = d_LusasData.getGroupByName(tag);
                objectSet.add(newGeometry);
            }
        }

        //public void assignObjectSet(IFPoint newPoint, HashSet<String> tags)
        //{
        //    foreach (string tag in tags)
        //    {
        //        IFObjectSet objectSet = d_LusasData.getGroupByName(tag);
        //        objectSet.add(newPoint);
        //    }
        //}

        //public void assignObjectSet(IFLine newLine, HashSet<String> tags)
        //{
        //    foreach (string tag in tags)
        //    {
        //        IFObjectSet objectSet = d_LusasData.getGroupByName(tag);
        //        objectSet.add(newLine);
        //    }
        //}

        //public void assignObjectSet(IFSurface newSurface, HashSet<String> tags)
        //{
        //    foreach (string tag in tags)
        //    {
        //        IFObjectSet objectSet = d_LusasData.getGroupByName(tag);
        //        objectSet.add(newSurface);
        //    }
        //}
    }
}
