using Microsoft.ML.Data;

namespace DataAnalysisService.BusinessLogicLayer.NeuralModels.BERT;

public class BertInput
{
    [ColumnName("input_ids")]
    public long[] InputIds { get; init; }

    [ColumnName("attention_mask")]
    public long[] AttentionMask { get; init; }
}