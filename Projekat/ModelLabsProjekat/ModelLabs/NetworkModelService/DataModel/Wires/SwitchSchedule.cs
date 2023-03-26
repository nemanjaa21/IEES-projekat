using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using FTN.Services.NetworkModelService.DataModel.LoadModel;

namespace FTN.Services.NetworkModelService.DataModel.Wires
{
    public class SwitchSchedule : SeasonDayTypeSchedule
    {
		private long switchRef;

		public SwitchSchedule(long globalId) : base(globalId)
		{
		}


		public long SwitchRef { get => switchRef; set => switchRef = value; }

		public override bool Equals(object obj)
		{
			if (base.Equals(obj))
			{
				SwitchSchedule x = (SwitchSchedule)obj;
				return (x.switchRef == this.switchRef);
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
				case ModelCode.SWITCHSCHEDULE_SWITCH:

					return true;
				default:
					return base.HasProperty(property);
			}
		}

		public override void GetProperty(Property property)
		{
			switch (property.Id)
			{
				case ModelCode.SWITCHSCHEDULE_SWITCH:
					property.SetValue(switchRef);
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
				case ModelCode.SWITCHSCHEDULE_SWITCH:
					switchRef = property.AsReference();
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
			if (switchRef != 0 && (refType == TypeOfReference.Reference || refType == TypeOfReference.Both))
			{
				references[ModelCode.SWITCHSCHEDULE_SWITCH] = new List<long>() { switchRef };
			}

			base.GetReferences(references, refType);
		}

		#endregion IReference implementation
	}
}
