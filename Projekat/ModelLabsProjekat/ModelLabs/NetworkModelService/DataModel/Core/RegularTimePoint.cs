using FTN.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Core
{
    public class RegularTimePoint : IdentifiedObject
    {
		private int sequenceNumber;
		private float value1;
		private float value2;
		private long regularIntervalSchedule;

		public RegularTimePoint(long globalId) : base(globalId)
		{
		}

		public int SequenceNumber { get => sequenceNumber; set => sequenceNumber = value; }
		public float Value1 { get => value1; set => value1 = value; }
		public float Value2 { get => value2; set => value2 = value; }
		public long RegularIntervalSchedule { get => regularIntervalSchedule; set => regularIntervalSchedule = value; }

		public override bool Equals(object obj)
		{
			if (base.Equals(obj))
			{
				RegularTimePoint x = (RegularTimePoint)obj;
				return ((x.sequenceNumber == this.sequenceNumber) &&
						(x.value1 == this.value1) &&
						(x.value2 == this.value2) &&
						(x.regularIntervalSchedule == this.regularIntervalSchedule));
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
				case ModelCode.REGULARTIMEPOINT_SEQUENCE:
				case ModelCode.REGULARTIMEPOINT_VALUE1:
				case ModelCode.REGULARTIMEPOINT_VALUE2:
				case ModelCode.REGULARTIMEPOINT_RIS:

					return true;
				default:
					return base.HasProperty(property);
			}
		}

		public override void GetProperty(Property property)
		{
			switch (property.Id)
			{
				case ModelCode.REGULARTIMEPOINT_SEQUENCE:
					property.SetValue(sequenceNumber);
					break;

				case ModelCode.REGULARTIMEPOINT_VALUE1:
					property.SetValue(value1);
					break;

				case ModelCode.REGULARTIMEPOINT_VALUE2:
					property.SetValue(value2);
					break;
				case ModelCode.REGULARTIMEPOINT_RIS:
					property.SetValue(regularIntervalSchedule);
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
				case ModelCode.REGULARTIMEPOINT_SEQUENCE:
					sequenceNumber = property.AsInt();
					break;

				case ModelCode.REGULARTIMEPOINT_VALUE1:
					value1 = property.AsFloat();
					break;

				case ModelCode.REGULARTIMEPOINT_VALUE2:
					value2 = property.AsFloat();
					break;
				case ModelCode.REGULARTIMEPOINT_RIS:
					regularIntervalSchedule = property.AsReference();
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
			if (regularIntervalSchedule != 0 && (refType == TypeOfReference.Reference || refType == TypeOfReference.Both))
			{
				references[ModelCode.REGULARTIMEPOINT_RIS] = new List<long>() { regularIntervalSchedule };
			}

			base.GetReferences(references, refType);
		}

		#endregion IReference implementation
	}
}
