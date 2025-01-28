using ML_Integration_Pattern.model;

namespace ML_Integration_Pattern.registry
{
    public class VersionedModelRegistry
    {
        private Dictionary<string, Dictionary<string, MLModel>> Registry;

        public VersionedModelRegistry()
        {
            Registry = new Dictionary<string, Dictionary<string, MLModel>>();
        }

        public void RegisterModel(string modelName, string version, MLModel model)
        {
            if (!Registry.ContainsKey(modelName))
            {
                Registry[modelName] = new Dictionary<string, MLModel>();
            }
            Registry[modelName][version] = model;
        }

        public MLModel GetModel(string modelName, string version)
        {
            if (Registry.TryGetValue(modelName, out var versions))
            {
                if (versions.TryGetValue(version, out var model))
                {
                    return model;
                }
            }
            return null;
        }

        public string GetLatestVersion(string modelName)
        {
            if (Registry.TryGetValue(modelName, out var versions))
            {
                return versions.Keys
                               .OrderBy(version => version) // Sort versions
                               .LastOrDefault(); // Get the latest version
            }
            return null;
        }
    }
}
