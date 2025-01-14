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

using Lusas.LPI;

namespace BH.Adapter.Adapters.Lusas
{
    public static partial class Convert
    {
        /***************************************************/
        /**** Internal Methods                          ****/
        /***************************************************/

        internal static string GetName(IFAttribute lusasAttribute)
        {
            return lusasAttribute.getName();
        }

        /***************************************************/

        internal static string GetName(IFLoadcase lusasLoadcase)
        {
            string loadcaseName = "";

            if (lusasLoadcase.getName().Contains("/"))
            {
                loadcaseName = lusasLoadcase.getName().Substring(
                    lusasLoadcase.getName().LastIndexOf("/") + 1);
            }
            else
            {
                loadcaseName = lusasLoadcase.getName();
            }

            return loadcaseName;
        }

        /***************************************************/

        internal static string GetName(IFBasicCombination lusasLoadCombination)
        {
            string loadcaseName = "";

            if (lusasLoadCombination.getName().Contains("/"))
            {
                loadcaseName = lusasLoadCombination.getName().Substring(
                    lusasLoadCombination.getName().LastIndexOf("/") + 1);
            }
            else
            {
                loadcaseName = lusasLoadCombination.getName();
            }

            return loadcaseName;
        }

        /***************************************************/

        internal static string GetName(string lusasLoadName)
        {
            string loadName = "";

            if (lusasLoadName.Contains("/"))
            {
                loadName = lusasLoadName.Substring(
                    lusasLoadName.LastIndexOf("/") + 1);
            }
            else
            {
                loadName = lusasLoadName;
            }

            return loadName;
        }

        /***************************************************/

    }
}


