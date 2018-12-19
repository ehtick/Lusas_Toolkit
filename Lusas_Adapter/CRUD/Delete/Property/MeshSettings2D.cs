﻿using System.Collections.Generic;

namespace BH.Adapter.Lusas
{
    public partial class LusasAdapter
    {
        public int DeleteMeshSettings2D(IEnumerable<object> ids)
        {
            int success = 1;

            if (ids != null)
            {
                //Engine.Reflection.Compute.RecordError("The deleting of individual MeshSettings2D is not supported in the Lusas_Toolkit");

                return 0;
            }
            else
            {
                d_LusasData.deleteAttributes("Surface Mesh");
            }
            return success;
        }
    }
}