using Interfaces;

namespace M_USE_Toxic
{
    public class PredictResult
    {
        public IDataFrame DataFrame { get; init; }
        public string Toxicity { get; init; }
    }
}
