namespace ObjectDetection.DataStructures;

public class ImageNetPrediction
{
    [ColumnName("grid")]
    public float[] PredictedLabels;
    //PredictedLabel contains the dimensions, objectness score, and class probabilities for each of the bounding boxes detected in an image.
}