﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using System.Reflection;
using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;

namespace FTN.Services.NetworkModelService.DataModel.Wires
{

	public class PerLengthImpedance : IdentifiedObject
    {		
        private List<long> acLineSegments = new List<long>();

		public PerLengthImpedance (long globalId) : base(globalId)
		{
		}

        public List<long> ACLineSegments
        {
            get
            {
                return acLineSegments;
            }
            set
            {
                acLineSegments = value;
            }
        }

		public override bool Equals(object obj)
		{
            if (base.Equals(obj))
            {
                PerLengthImpedance x = (PerLengthImpedance)obj;
                return (CompareHelper.CompareLists(x.acLineSegments, this.acLineSegments));
            }
            else
            {
                return false;
            }
        }
		
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		#region IAccess implementation		

		public override bool HasProperty(ModelCode property)
		{
			switch(property)
			{
				case ModelCode.PERLENIMP_ACLINESEGS:
					return true;

				default:
                    return base.HasProperty(property);
			}
		}

		public override void GetProperty(Property property)
		{
			switch(property.Id)
			{
				case ModelCode.PERLENIMP_ACLINESEGS:
					property.SetValue(acLineSegments);
					break;

                default:
                    base.GetProperty(property);
                    break;
            }
		}

		public override void SetProperty(Property property)
		{
			switch(property.Id)
			{
                default:
                    base.SetProperty(property);
                    break;
            }
		}

		#endregion IAccess implementation

		#region IReference implementation	

		public override bool IsReferenced
		{
			get
			{
                return (acLineSegments.Count > 0 || base.IsReferenced);
			}
		}

        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
		{
            if (acLineSegments != null && acLineSegments.Count != 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
            {
                references[ModelCode.TERMINAL] = acLineSegments.GetRange(0, acLineSegments.Count);
            }

            base.GetReferences(references, refType);
        }

		public override void AddReference(ModelCode referenceId, long globalId)
		{
            switch (referenceId)
            {
                case ModelCode.ACLINESEG:
                    acLineSegments.Add(globalId);
                    break;

                default:
                    base.AddReference(referenceId, globalId);
                    break;
            }
        }

		public override void RemoveReference(ModelCode referenceId, long globalId)
		{
            switch (referenceId)
            {
                case ModelCode.ACLINESEG:

                    if (acLineSegments.Contains(globalId))
                    {
                        acLineSegments.Remove(globalId);
                    }
                    else
                    {
                        CommonTrace.WriteTrace(CommonTrace.TraceWarning, "Entity (GID = 0x{0:x16}) doesn't contain reference 0x{1:x16}.", this.GlobalId, globalId);
                    }

                    break;

                default:
                    base.RemoveReference(referenceId, globalId);
                    break;
            }
        }

		#endregion IReference implementation
	}
}
