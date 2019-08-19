using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using System.Reflection;
using FTN.Common;

namespace FTN.Services.NetworkModelService.DataModel.Core
{

	public class Terminal : IdentifiedObject
    {		
        private long connode = 0;
        private long condeq = 0;

		public Terminal(long globalId) : base(globalId)
		{
		}

		public long Connode
        {
			get
			{				
				return connode;
			}

			set
			{
                connode = value;
			}
		}

        public long Condeq
        {
            get
            {
                return condeq;
            }

            set
            {
                condeq = value;
            }
        }

        public override bool Equals(object obj)
		{
            if (base.Equals(obj))
            {
                Terminal x = (Terminal)obj;
                return (x.connode == this.connode &&
                        x.condeq == this.condeq);
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
                case ModelCode.TERMINAL_CONNODE:
                    return true;

                case ModelCode.TERMINAL_CONDEQ:
                    return true;

                default:
                    return base.HasProperty(property);
            }
		}

		public override void GetProperty(Property property)
		{
			switch(property.Id)
			{
                case ModelCode.TERMINAL_CONNODE:
                    property.SetValue(connode);
                    break;

                case ModelCode.TERMINAL_CONDEQ:
                    property.SetValue(condeq);
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
                case ModelCode.TERMINAL_CONNODE:
                    connode = property.AsReference();
                    break;

                case ModelCode.TERMINAL_CONDEQ:
                    condeq = property.AsReference();
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
            if (connode != 0 && (refType == TypeOfReference.Reference || refType == TypeOfReference.Both))
            {
                references[ModelCode.CONNODE_TERMINALS] = new List<long>();
                references[ModelCode.CONNODE_TERMINALS].Add(connode);
            }

            if (condeq != 0 && (refType == TypeOfReference.Reference || refType == TypeOfReference.Both))
            {
                references[ModelCode.CONDEQ] = new List<long>();
                references[ModelCode.CONDEQ].Add(condeq);
            }

            base.GetReferences(references, refType);
        }

		#endregion IReference implementation
	}
}
