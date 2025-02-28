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
using BH.oM.Structure.Elements;
using BH.Engine.Geometry;
using BH.oM.Structure.SectionProperties;
using BH.oM.Geometry;

namespace BH.Engine.Adapters.Lusas
{
    public static partial class Query
    {
        /***************************************************/
        /**** Private Methods                           ****/
        /***************************************************/
        public static bool InvalidSectionProperty(this ISectionProperty sectionProperty)
        {
            bool isInvalid = false;

            if (sectionProperty is ExplicitSection)
            {
                isInvalid = true;
                Base.Compute.RecordWarning("Unsupported SectionProperty (ExplicitSection) assigned to Bar.");
            }

            return isInvalid;
        }
    }
}

