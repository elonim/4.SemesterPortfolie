using Microsoft.ML.Data;

namespace PhoneCallsAnomalyDetection.Models;

public class PhoneCallsData
{
    [LoadColumn(0)]
    public string timestamp;

    [LoadColumn(1)]
    public double value;
}