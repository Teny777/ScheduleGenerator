namespace Generator.Core.Restriction
{
    public class RestrictionModel
    {
        public RestrictionModel(int count, int weightPositive, int weightNegative, bool isImportant)
        {
            Count = count;
            WeightPositive = weightPositive;
            WeightNegative = weightNegative;
            IsImportant = isImportant;
        }

        public int Count { get; set; }
        public int WeightPositive { get; set; }
        public int WeightNegative { get; set; }
        public bool IsImportant { get; set; }
    }
}