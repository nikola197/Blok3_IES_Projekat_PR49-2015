using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using FTN.Common;

namespace FTN.Services.NetworkModelService.DataModel.Wires
{
	public class SeriesCompensator : Core.ConductingEquipment
	{
        private float r;
        private float r0;
        private float x;
        private float x0;

        public SeriesCompensator(long globalId) : base(globalId)
        {
        }

        public float R { get => r; set => r = value; }
        public float R0 { get => r0; set => r0 = value; }
        public float X { get => x; set => x = value; }
        public float X0 { get => x0; set => x0 = value; }

	
		public override bool Equals(object obj)
		{
            if (base.Equals(obj))
            {
                SeriesCompensator x = obj as SeriesCompensator;
                return (
                        x.r == this.r &&
                        x.r0 == this.r0 &&
                        x.x == this.x &&
                        x.x0 == this.x0);
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
            switch (property)
            {
                case ModelCode.ACLINESEG_R:
                case ModelCode.ACLINESEG_R0:
                case ModelCode.ACLINESEG_X:
                case ModelCode.ACLINESEG_X0:
                case ModelCode.ACLINESEG_PERLENIMP:
                    return true;
                default:
                    return base.HasProperty(property);
            }
        }

        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.ACLINESEG_R:
                    property.SetValue(r);
                    break;
                case ModelCode.ACLINESEG_R0:
                    property.SetValue(r0);
                    break;
                case ModelCode.ACLINESEG_X:
                    property.SetValue(x);
                    break;
                case ModelCode.ACLINESEG_X0:
                    property.SetValue(x0);
                    break;

                default:
                    base.GetProperty(property);
                    break;
            }
        }

        public override void SetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.ACLINESEG_R:
                    r = property.AsFloat();
                    break;
                case ModelCode.ACLINESEG_R0:
                    r0 = property.AsFloat();
                    break;
                case ModelCode.ACLINESEG_X:
                    x = property.AsFloat();
                    break;
                case ModelCode.ACLINESEG_X0:
                    x0 = property.AsFloat();
                    break;

                default:
                    base.SetProperty(property);
                    break;
            }
        }

        #endregion IAccess implementation
    }
}
