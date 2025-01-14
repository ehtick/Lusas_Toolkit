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
using System.Collections;
using System.Collections.Generic;
using BH.oM.Base;
using BH.Engine.Adapter;
using BH.oM.Analytical.Results;
using BH.oM.Data.Requests;
using BH.oM.Structure.Loads;
using BH.oM.Structure.Requests;
using System.Linq;
using BH.Engine.Adapters.Lusas;
using BH.oM.Adapters.Lusas;

namespace BH.Adapter.Lusas
{
#if Debug18 || Release18
    public partial class LusasV18Adapter : BHoMAdapter
#elif Debug19 || Release19
    public partial class LusasV19Adapter
#else
    public partial class LusasV17Adapter : BHoMAdapter
#endif
    {
        /***************************************************/
        /**** Private  Methods - Index checking         ****/
        /***************************************************/

        private List<int> GetObjectIDs(IResultRequest request)
        {
            IList ids = request.ObjectIds;

            if (ids == null || ids.Count == 0)
            {
                return GetAllIds(request as dynamic);
            }
            else
            {
                if (ids is List<string>)
                    return (ids as List<string>).Select(x => int.Parse(x)).ToList();
                else if (ids is List<int>)
                    return ids as List<int>;
                else if (ids is List<double>)
                    return (ids as List<double>).Select(x => (int)Math.Round(x)).ToList();
                else
                {
                    List<int> idsOut = new List<int>();
                    foreach (object o in ids)
                    {
                        int id;
                        if (int.TryParse(o.ToString(), out id))
                        {
                            idsOut.Add(id);
                        }
                        else if (o is IBHoMObject && (o as IBHoMObject).HasAdapterIdFragment(typeof(LusasId)))
                        {
                            int.TryParse((o as IBHoMObject).AdapterId<string>(typeof(LusasId)), out id);
                            idsOut.Add(id);
                        }
                    }
                    return idsOut;
                }
            }
        }

        private List<int> GetAllIds(NodeResultRequest request)
        {
            List<int> ids = new List<int>();
            int maxIndex = d_LusasData.getLargestNodeID();

            for (int i = 1; i < maxIndex + 1; i++)
            {
                if (d_LusasData.existsPointByID(i))
                {
                    ids.Add(
                        System.Convert.ToInt32(
                        d_LusasData.getPointByNumber(i).getID().ToString()));
                }
            }

            return ids;
        }

        /***************************************************/

        private List<int> GetAllIds(BarResultRequest request)
        {
            List<int> ids = new List<int>();
            int maxIndex = d_LusasData.getLargestLineID();

            for (int i = 1; i < maxIndex + 1; i++)
            {
                if (d_LusasData.existsLineByID(i))
                {
                    ids.Add(
                        System.Convert.ToInt32(
                        d_LusasData.getLineByNumber(i).getID().ToString()));
                }
            }

            return ids;
        }

        /***************************************************/

        private List<int> GetAllIds(MeshResultRequest request)
        {
            List<int> ids = new List<int>();
            int maxIndex = d_LusasData.getLargestSurfaceID();

            for (int i = 1; i < maxIndex + 1; i++)
            {
                if (d_LusasData.existsSurfaceByID(i))
                {
                    ids.Add(
                        System.Convert.ToInt32(
                        d_LusasData.getSurfaceByNumber(i).getID().ToString()));
                }
            }

            return ids;
        }

        /***************************************************/

        private List<int> GetLoadcaseIDs(IResultRequest request)
        {
            IList cases = request.Cases;
            List<int> caseNums = new List<int>();

            if (cases is List<string>)
                return (cases as List<string>).Select(x => int.Parse(x)).ToList();
            else if (cases is List<int>)
                return cases as List<int>;
            else if (cases is List<double>)
                return (cases as List<double>).Select(x => (int)Math.Round(x)).ToList();

            else if (cases is List<Loadcase>)
            {
                for (int i = 0; i < cases.Count; i++)
                {
                    caseNums.Add(System.Convert.ToInt32((cases[i] as Loadcase).Number));
                }
            }
            else if (cases is List<LoadCombination>)
            {
                foreach (object lComb in cases)
                {
                    foreach (Tuple<double, ICase> lCase in (lComb as LoadCombination).LoadCases)
                    {
                        caseNums.Add(System.Convert.ToInt32(lCase.Item2.Number));
                    }
                    caseNums.Add((lComb as LoadCombination).AdapterId<int>(typeof(LusasId)));
                }
            }

            else
            {
                List<int> idsOut = new List<int>();
                foreach (object o in cases)
                {
                    int id;
                    if (int.TryParse(o.ToString(), out id))
                    {
                        idsOut.Add(id);
                    }
                }
                return idsOut;
            }


            return caseNums;
        }

        /***************************************************/

    }
}


