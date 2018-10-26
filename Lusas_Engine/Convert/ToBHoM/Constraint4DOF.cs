﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BH.oM.Base;
using BH.oM.Geometry;
using BH.oM.Structure.Elements;
using BH.oM.Structure.Properties;
using BH.oM.Common.Materials;
using Lusas.LPI;

namespace BH.Engine.Lusas
{
    public static partial class Convert
    {
        public static Constraint4DOF ToBHoMConstraint4DOF(this IFSupportStructural lusasAttribute)
        {
            List<string> releaseNames = new List<string> { "U", "V", "W", "THX" };

            List<DOFType> fixity = new List<DOFType>();
            List<double> stiffness = new List<double>();

            foreach (string releaseName in releaseNames)
            {
                string fixityValue = lusasAttribute.getValue(releaseName);

                if (fixityValue == "F")
                {
                    fixity.Add(DOFType.Free);
                    stiffness.Add(0.0);
                }
                else if (fixityValue == "R")
                {
                    fixity.Add(DOFType.Fixed);
                    stiffness.Add(0.0);
                }
                else if (fixityValue == "S")
                {
                    fixity.Add(DOFType.Spring);
                    double stiffnessValue = lusasAttribute.getValue(releaseName + "stiff");
                    stiffness.Add(stiffnessValue);
                }
            }

            string attributeName = GetName(lusasAttribute);

            Constraint4DOF bhomConstraint4DOF = BH.Engine.Structure.Create.Constraint4DOF(attributeName);

            bhomConstraint4DOF.TranslationX = fixity[0];
            bhomConstraint4DOF.TranslationY = fixity[1];
            bhomConstraint4DOF.TranslationZ = fixity[2];
            bhomConstraint4DOF.RotationX = fixity[3];

            bhomConstraint4DOF.RotationalStiffnessX = stiffness[0];
            bhomConstraint4DOF.TranslationalStiffnessX = stiffness[1];
            bhomConstraint4DOF.TranslationalStiffnessX = stiffness[2];
            bhomConstraint4DOF.TranslationalStiffnessX = stiffness[3];

            int bhomID = GetBHoMID(lusasAttribute, 'p');

            bhomConstraint4DOF.CustomData["Lusas_id"] = bhomID;

            return bhomConstraint4DOF;
        }
    }
}