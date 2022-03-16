
using Microsoft.ML.Data;

namespace PhoneCallsAnomalyDetection.Models;

public class PhoneCallsPrediction
{
    [VectorType(7)]
    public double[] Prediction { get; set; }
}
