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
	public class PerLengthSequenceImpedance : PerLengthImpedance
    {
        private float b0ch;
        private float bch;
        private float g0ch;
        private float gch;
        private float r;
        private float r0;
        private float x;
        private float x0;

        public PerLengthSequenceImpedance(long globalId) : base(globalId)
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

	
		public override bool Equals(object obj)
		{
            if (base.Equals(obj))
            {
                PerLengthSequenceImpedance x = obj as PerLengthSequenceImpedance;
                return (x.b0ch == this.b0ch &&
                        x.bch == this.bch &&
                        x.g0ch == this.g0ch &&
                        x.gch == this.gch &&
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
                case ModelCode.PERLENSEQIMP_B0CH:
                case ModelCode.PERLENSEQIMP_BCH:
                case ModelCode.PERLENSEQIMP_G0CH:
                case ModelCode.PERLENSEQIMP_GCH:
                case ModelCode.PERLENSEQIMP_R:
                case ModelCode.PERLENSEQIMP_R0:
                case ModelCode.PERLENSEQIMP_X:
                case ModelCode.PERLENSEQIMP_X0:
                    return true;
                default:
                    return base.HasProperty(property);
            }
        }

        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.PERLENSEQIMP_B0CH:
                    property.SetValue(b0ch);
                    break;
                case ModelCode.PERLENSEQIMP_BCH:
                    property.SetValue(bch);
                    break;
                case ModelCode.PERLENSEQIMP_G0CH:
                    property.SetValue(g0ch);
                    break;
                case ModelCode.PERLENSEQIMP_GCH:
                    property.SetValue(gch);
                    break;
                case ModelCode.PERLENSEQIMP_R:
                    property.SetValue(r);
                    break;
                case ModelCode.PERLENSEQIMP_R0:
                    property.SetValue(r0);
                    break;
                case ModelCode.PERLENSEQIMP_X:
                    property.SetValue(x);
                    break;
                case ModelCode.PERLENSEQIMP_X0:
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
                case ModelCode.PERLENSEQIMP_B0CH:
                    b0ch = property.AsFloat();
                    break;
                case ModelCode.PERLENSEQIMP_BCH:
                    bch = property.AsFloat();
                    break;
                case ModelCode.PERLENSEQIMP_G0CH:
                    g0ch = property.AsFloat();
                    break;
                case ModelCode.PERLENSEQIMP_GCH:
                    gch = property.AsFloat();
                    break;
                case ModelCode.PERLENSEQIMP_R:
                    r = property.AsFloat();
                    break;
                case ModelCode.PERLENSEQIMP_R0:
                    r0 = property.AsFloat();
                    break;
                case ModelCode.PERLENSEQIMP_X:
                    x = property.AsFloat();
                    break;
                case ModelCode.PERLENSEQIMP_X0:
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
