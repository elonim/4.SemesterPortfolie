﻿// This file was auto-generated by ML.NET Model Builder. 
using Microsoft.ML;

namespace Iris
{
    public partial class MLModel1
    {
        public static ITransformer RetrainPipeline(MLContext context, IDataView trainData)
        {
            var pipeline = BuildPipeline(context);
            var model = pipeline.Fit(trainData);

            return model;
        }

        /// <summary>
        /// build the pipeline that is used from model builder. Use this function to retrain model.
        /// </summary>
        /// <param name="mlContext"></param>
        /// <returns></returns>
        public static IEstimator<ITransformer> BuildPipeline(MLContext mlContext)
        {
            // Data process configuration with pipeline data transformations
            var pipeline = mlContext.Transforms.ReplaceMissingValues(new[] { new InputOutputColumnPair(@"SepalLengthCm", @"SepalLengthCm"), new InputOutputColumnPair(@"SepalWidthCm", @"SepalWidthCm"), new InputOutputColumnPair(@"PetalLengthCm", @"PetalLengthCm"), new InputOutputColumnPair(@"PetalWidthCm", @"PetalWidthCm") })
                                    .Append(mlContext.Transforms.Concatenate(@"Features", new[] { @"SepalLengthCm", @"SepalWidthCm", @"PetalLengthCm", @"PetalWidthCm" }))
                                    .Append(mlContext.Transforms.Conversion.MapValueToKey(outputColumnName: @"Species", inputColumnName: @"Species"))
                                    .Append(mlContext.BinaryClassification.Trainers.FastForest(@"Species"))
                                    .Append(mlContext.Transforms.Conversion.MapKeyToValue(outputColumnName: @"PredictedLabel", inputColumnName: @"PredictedLabel"));

            return pipeline;
        }
    }
}
