using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;

namespace FTN.Services.NetworkModelService.DataModel.LoadModel
{
    public class DayType : IdentifiedObject
    {
		private List<long> seasonDayTypeSchedules = new List<long>();

		public DayType(long globalId) : base(globalId)
		{
		}

		public List<long> SeasonDayTypeSchedules
		{
			get
			{
				return seasonDayTypeSchedules;
			}

			set
			{
				seasonDayTypeSchedules = value;
			}
		}

		public override bool Equals(object obj)
		{
			if (base.Equals(obj))
			{
				DayType x = (DayType)obj;
				return (CompareHelper.CompareLists(x.seasonDayTypeSchedules, this.seasonDayTypeSchedules, true));
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

		public override bool HasProperty(ModelCode t)
		{
			switch (t)
			{
				case ModelCode.DAYTYPE_SEASONDTSCHEDULES:
					return true;

				default:
					return base.HasProperty(t);
			}
		}

		public override void GetProperty(Property prop)
		{
			switch (prop.Id)
			{
				case ModelCode.DAYTYPE_SEASONDTSCHEDULES:
					prop.SetValue(seasonDayTypeSchedules);
					break;

				default:
					base.GetProperty(prop);
					break;
			}
		}

		public override void SetProperty(Property property)
		{
			base.SetProperty(property);
		}

		#endregion IAccess implementation

		#region IReference implementation

		public override bool IsReferenced
		{
			get
			{
				return seasonDayTypeSchedules.Count > 0 || base.IsReferenced;
			}
		}

		public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
		{
			if (seasonDayTypeSchedules != null && seasonDayTypeSchedules.Count > 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
			{
				references[ModelCode.DAYTYPE_SEASONDTSCHEDULES] = seasonDayTypeSchedules.GetRange(0, seasonDayTypeSchedules.Count);
			}

			base.GetReferences(references, refType);
		}

		public override void AddReference(ModelCode referenceId, long globalId)
		{
			switch (referenceId)
			{
				case ModelCode.SEASONDAYTYPESCH_DT:
					seasonDayTypeSchedules.Add(globalId);
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
				case ModelCode.SEASONDAYTYPESCH_DT:

					if (seasonDayTypeSchedules.Contains(globalId))
					{
						seasonDayTypeSchedules.Remove(globalId);
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
