// 代码生成时间: 2025-10-14 03:02:20
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ML;

// 定义一个模型来存储图像数据和标签
public class ImageData
{
    public byte[] ImageData { get; set; }
    public string Label { get; set; }
}

// 定义一个模型来映射预测结果
public class ImagePrediction
{
    public float[] Score { get; set; }
    public string PredictedLabel { get; set; }
}

public class ImageRecognitionService
{
    private readonly MLContext _mlContext;
    private readonly ITransformer _model;
    private readonly string _modelPath = "model.zip"; // 模型文件路径

    public ImageRecognitionService()
    {
        _mlContext = new MLContext();
        LoadModel();
    }

    // 加载模型方法
    private void LoadModel()
    {
        try
        {
            // 加载模型
            _model = _mlContext.Model.Load(_modelPath, out var modelInputSchema);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading model: {ex.Message}");
            throw;
        }
    }

    // 预测图像标签方法
    public async Task<ImagePrediction> PredictLabelAsync(byte[] imageData)
    {
        try
        {
            // 创建数据并进行预测
            var predictionEngine = _mlContext.Model.CreatePredictionEngine<ImageData, ImagePrediction>(_model);
            var prediction = predictionEngine.Predict(new ImageData { ImageData = imageData });
            return prediction;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during prediction: {ex.Message}");
            throw;
        }
    }
}
