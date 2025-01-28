
namespace ML_Integration_Pattern.model
{
    public abstract class MLModel
    {
        public string ModelVersion { get; set; }

        public abstract void LoadModel();
        public abstract object Predict(object input);
        public abstract void UpdateModel(string modelPath);
        public abstract void UpdateModel(string modelPath, string modelVersion);
    }
}
