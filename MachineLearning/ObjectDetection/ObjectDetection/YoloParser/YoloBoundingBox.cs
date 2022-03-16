namespace ObjectDetection.YoloParser;

public class BoundingBoxDimensions : DimensionsBase { }
public class YoloBoundingBox
{
    public BoundingBoxDimensions Dimensions { get; set; }

    public string Label { get; set; }

    public float Confidence { get; set; }

    public RectangleF Rect
    {
        get { return new RectangleF(Dimensions.X, Dimensions.Y, Dimensions.Width, Dimensions.Height); }
    }
    public Color BoxColor { get; set; }

    /*
    Dimensions contains dimensions of the bounding box.
    Label contains the class of object detected within the bounding box.
    Confidence contains the confidence of the class.
    Rect contains the rectangle representation of the bounding box's dimensions.
    BoxColor contains the color associated with the respective class used to draw on the image.
    */
}
