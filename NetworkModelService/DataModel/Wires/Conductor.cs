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
	public class Conductor : Core.ConductingEquipment
    {
        private float length;

		public Conductor(long globalId) : base(globalId) 
		{
		}

        public float Length
        {
            get
            {
                return length;
            }

            set
            {
                length = value;
            }
        }

        public override bool Equals(object obj)
		{
            if (base.Equals(obj))
            {
                Conductor x = obj as Conductor;
                return (x.length == this.length);
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
                case ModelCode.CONDUCTOR_LEN:
                    return true;
                default:
                    return base.HasProperty(property);
            }
        }

        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.CONDUCTOR_LEN:
                    property.SetValue(length);
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
                case ModelCode.CONDUCTOR_LEN:
                    length = property.AsFloat();
                    break;

                default:
                    base.SetProperty(property);
                    break;
            }
        }

        #endregion IAccess implementation
    }
}
