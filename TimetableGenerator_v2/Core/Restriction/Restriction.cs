using System;
using System.Reflection;
using System.Windows.Media;

namespace Generator.Core.Restriction
{
    [Serializable]
    public class Restriction
    {
        public int Number { get; set; }
        public string Expression { get; set; }
        public int WeightPozitive { get; set; }
        public int WeightNegative { get; set; }
        public string Comment { get; set; }
        
        [field: NonSerialized]
        public MethodInfo Method { get; set; }

        public Brush Color 
        { 
            get
            {
                if (CountOk == CountFail && CountFail == 0)
                {
                    return Brushes.Transparent;
                }

                if (IsRequirement && CountFail > 0)
                {
                    return Brushes.PaleVioletRed;
                }

                if (CountFail == 0 || CountFail * 1.0 / CountOk <= 0.15)
                {
                    return Brushes.LightGreen;
                }

                return Brushes.LightPink;
            } 
        }
        public int CountOk { get; set; }
        public int CountFail { get; set; }
        public bool IsRequirement { get; set; }
    }
}
