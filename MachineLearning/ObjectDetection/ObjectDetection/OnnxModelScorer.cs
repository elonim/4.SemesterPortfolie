namespace ObjectDetection;

public class OnnxModelScorer
{
    private readonly string imagesFolder;
    private readonly string modelLocation;
    private readonly MLContext mlContext;

    private IList<YoloBoundingBox> _boundingBoxes = new List<YoloBoundingBox>();

    public OnnxModelScorer(string imagesFolder, string modelLocation, MLContext mlContext)
    {
        this.imagesFolder = imagesFolder;
        this.modelLocation = modelLocation;
        this.mlContext = mlContext;
    }
    public struct ImageNetSettings
    {
        public const int imageHeight = 416;
        public const int imageWidth = 416;
    }

    public struct TinyYoloModelSettings
    {
        // for checking Tiny yolo2 Model input and  output  parameter names,
        //you can use tools like Netron, 
        // which is installed by Visual Studio AI Tools

        // input tensor name
        public const string ModelInput = "image";

        // output tensor name
        public const string ModelOutput = "grid";
    }
    public IEnumerable<float[]> Score(IDataView data)
    {
        var model = LoadModel(modelLocation);
        return PredictDataUsingModel(data, model);
    }
    
    private ITransformer LoadModel(string modelLocation)
    {
        Console.WriteLine("Read model");
        Console.WriteLine($"Model location: {modelLocation}");
        Console.WriteLine($"Default parameters: image size=({ImageNetSettings.imageWidth},{ImageNetSettings.imageHeight})");

        var data = mlContext.Data.LoadFromEnumerable(new List<ImageNetData>());

        // Pipeline

        // LoadImages loads the image as a Bitmap.
        // ResizeImages rescales the image to the size specified (in this case, 416 x 416).
        // ExtractPixels changes the pixel representation of the image from a Bitmap to a numerical vector.
        // ApplyOnnxModel loads the ONNX model and uses it to score on the data provided.

        var pipeline = mlContext.Transforms.LoadImages(outputColumnName: "image", imageFolder: "", inputColumnName: nameof(ImageNetData.ImagePath))
                .Append(mlContext.Transforms.
                ResizeImages(outputColumnName: "image", imageWidth: ImageNetSettings.imageWidth, imageHeight: ImageNetSettings.imageHeight, inputColumnName: "image"))
                .Append(mlContext.Transforms
                .ExtractPixels(outputColumnName: "image"))
                .Append(mlContext.Transforms
                .ApplyOnnxModel(modelFile: modelLocation, outputColumnNames: new[] { TinyYoloModelSettings.ModelOutput }, inputColumnNames: new[] { TinyYoloModelSettings.ModelInput }));

        var model = pipeline.Fit(data);

        return model;
    }


    private IEnumerable<float[]> PredictDataUsingModel(IDataView testData, ITransformer model)
    {
        Console.WriteLine("");
        Console.WriteLine($"Images location: {imagesFolder}");
        Console.WriteLine("");
        Console.WriteLine("=====Identify the objects in the images=====");
        Console.WriteLine("");

        IDataView scoredData = model.Transform(testData);

        IEnumerable<float[]> probabilities = scoredData.GetColumn<float[]>(TinyYoloModelSettings.ModelOutput);

        return probabilities;
    }
}
