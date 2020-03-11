using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;

namespace FTN.Services.NetworkModelService.DataModel.Wires
{
	public class ACLineSegment : Conductor
	{
        private float b0ch;
        private float bch;
        private float g0ch;
        private float gch;
        private float r;
        private float r0;
        private float x;
        private float x0;

        private long perLenImp;

        public ACLineSegment(long globalId) : base(globalId)
        {
        }

        public float B0ch { get => b0ch; set => b0ch = value; }
        public float Bch { get => bch; set => bch = value; }
        public float G0ch { get => g0ch; set => g0ch = value; }
        public float Gch { get => gch; set => gch = value; }
        public float R { get => r; set => r = value; }
        public float R0 { get => r0; set => r0 = value; }
        public float X { get => x; set => x = value; }
        public float X0 { get => x0; set => x0 = value; }
        public long PerLenImp { get => perLenImp; set => perLenImp = value; }

	
		public override bool Equals(object obj)
		{
            if (base.Equals(obj))
            {
                ACLineSegment x = obj as ACLineSegment;
                return (x.b0ch == this.b0ch &&
                        x.bch == this.bch &&
                        x.g0ch == this.g0ch &&
                        x.gch == this.gch &&
                        x.r == this.r &&
                        x.r0 == this.r0 &&
                        x.x == this.x &&
                        x.x0 == this.x0 &&
                        x.perLenImp == this.perLenImp);
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
                case ModelCode.ACLINESEG_B0CH:
                case ModelCode.ACLINESEG_BCH:
                case ModelCode.ACLINESEG_G0CH:
                case ModelCode.ACLINESEG_GCH:
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
                case ModelCode.ACLINESEG_B0CH:
                    property.SetValue(b0ch);
                    break;
                case ModelCode.ACLINESEG_BCH:
                    property.SetValue(bch);
                    break;
                case ModelCode.ACLINESEG_G0CH:
                    property.SetValue(g0ch);
                    break;
                case ModelCode.ACLINESEG_GCH:
                    property.SetValue(gch);
                    break;
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
                case ModelCode.ACLINESEG_PERLENIMP:
                    property.SetValue(perLenImp);
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
                case ModelCode.ACLINESEG_B0CH:
                    b0ch = property.AsFloat();
                    break;
                case ModelCode.ACLINESEG_BCH:
                    bch = property.AsFloat(); ;
                    break;
                case ModelCode.ACLINESEG_G0CH:
                    g0ch = property.AsFloat();
                    break;
                case ModelCode.ACLINESEG_GCH:
                     gch = property.AsFloat();
                    break;
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
                case ModelCode.ACLINESEG_PERLENIMP:
                    perLenImp = property.AsReference();
                    break;

                default:
                    base.SetProperty(property);
                    break;
            }
        }

        #endregion IAccess implementation

        #region IReference implementation	

        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {
            if (perLenImp != 0 && (refType == TypeOfReference.Reference || refType == TypeOfReference.Both))
            {
                references[ModelCode.ACLINESEG_PERLENIMP] = new List<long>();
                references[ModelCode.ACLINESEG_PERLENIMP].Add(perLenImp);
            }

            base.GetReferences(references, refType);
        }

        #endregion IReference implementation
    }
}
