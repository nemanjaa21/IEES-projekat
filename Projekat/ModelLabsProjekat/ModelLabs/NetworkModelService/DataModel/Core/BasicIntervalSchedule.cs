using FTN.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Core
{
    public class BasicIntervalSchedule : IdentifiedObject
    {
        private DateTime startTime;
        private UnitMultiplier value1multiplier;
        private UnitSymbol value1unit;
        private UnitMultiplier value2multiplier;
        private UnitSymbol value2unit;

        //konstruktor obavezan
        public BasicIntervalSchedule(long globalId) : base(globalId)
        {
        }
        public DateTime StartTime { get => startTime; set => startTime = value; }
        public UnitMultiplier Value1multiplier { get => value1multiplier; set => value1multiplier = value; }
        public UnitSymbol Value1unit { get => value1unit; set => value1unit = value; }
        public UnitMultiplier Value2multiplier { get => value2multiplier; set => value2multiplier = value; }
        public UnitSymbol Value2unit { get => value2unit; set => value2unit = value; }

        //metoda Equals
        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                BasicIntervalSchedule x = (BasicIntervalSchedule)obj;
                return ((x.startTime == this.startTime) &&
                        (x.value1multiplier == this.value1multiplier) &&
                        (x.value1unit == this.value1unit) &&
                        (x.value2multiplier == this.value2multiplier) &&
                        (x.value2unit == this.value2unit));
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
                case ModelCode.BASICINTSCH_STARTTIME:
                case ModelCode.BASICINTSCH_VAL1MULTI:
                case ModelCode.BASICINTSCH_VAL1UNIT:
                case ModelCode.BASICINTSCH_VAL2MULTI:
                case ModelCode.BASICINTSCH_VAL2UNIT:
                    //u slucaju ovih model kodova vraca true, za sve ostale false
                    return true;
                default:
                    return base.HasProperty(property);
            }
        }

        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.BASICINTSCH_STARTTIME:
                    property.SetValue(startTime);
                    break;

                case ModelCode.BASICINTSCH_VAL1MULTI:
                    property.SetValue((short)value1multiplier);
                    break;

                case ModelCode.BASICINTSCH_VAL1UNIT:
                    property.SetValue((short)value1unit);
                    break;

                case ModelCode.BASICINTSCH_VAL2MULTI:
                    property.SetValue((short)value2multiplier);
                    break;

                case ModelCode.BASICINTSCH_VAL2UNIT:
                    property.SetValue((short)value2unit);
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
                case ModelCode.BASICINTSCH_STARTTIME:
                    startTime = property.AsDateTime();
                    break;

                case ModelCode.BASICINTSCH_VAL1MULTI:
                    value1multiplier = (UnitMultiplier)property.AsEnum(); // moramo kastovati u nasu
                    break;                                                // enumeraciju

                case ModelCode.BASICINTSCH_VAL1UNIT:
                    value1unit = (UnitSymbol)property.AsEnum();
                    break;

                case ModelCode.BASICINTSCH_VAL2MULTI:
                    value2multiplier = (UnitMultiplier)property.AsEnum();
                    break;

                case ModelCode.BASICINTSCH_VAL2UNIT:
                    value2unit = (UnitSymbol)property.AsEnum();
                    break;

                default:
                    base.SetProperty(property);
                    break;
            }
        }

        #endregion IAccess implementation
    }
}
