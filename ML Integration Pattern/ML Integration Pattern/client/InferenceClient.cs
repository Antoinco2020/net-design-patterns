using ML_Integration_Pattern.manager;
using ML_Integration_Pattern.model;
using ML_Integration_Pattern.monitor;
using ML_Integration_Pattern.registry;

namespace ML_Integration_Pattern.client
{
    public class InferenceClient
    {
        public void start()
        {
            // Creating the registry for versioned models
            VersionedModelRegistry registry = new VersionedModelRegistry();

            // Creation and registration of models
            NeuralNetworkModel modelA_v1 = new NeuralNetworkModel("path/to/modelA_v1", "v1.0");
            NeuralNetworkModel modelA_v2 = new NeuralNetworkModel("path/to/modelA_v2", "v2.0");
            NeuralNetworkModel modelB = new NeuralNetworkModel("path/to/modelB", "v1.0");

            registry.RegisterModel("ModelA", modelA_v1.ModelVersion, modelA_v1);
            registry.RegisterModel("ModelA", modelA_v1.ModelVersion, modelA_v2);
            registry.RegisterModel("ModelB", modelA_v1.ModelVersion, modelB);

            // Upload the most recent ModelA
            string latestVersion = registry.GetLatestVersion("ModelA");
            NeuralNetworkModel latestModelA = (NeuralNetworkModel)registry.GetModel("ModelA", latestVersion);
            latestModelA.LoadModel();

            // Monitor and cache initialization
            ModelMonitor monitor = new ModelMonitor();
            InferenceCache cache = new InferenceCache();

            // Load balancing among multiple models
            ModelLoadBalancer loadBalancer = new ModelLoadBalancer([
                    latestModelA,
                    modelB
            ]);

            // A/B testing between two models
            ABTestManager abTestManager = new ABTestManager(latestModelA, modelB);

            // Example of input for inference
            object input = "Input 1";

            // Check if the result is cached
            if (cache.IsCached(input))
            {
                Console.WriteLine("Cache Hit: " + cache.GetCachedResult(input));
            }
            else
            {
                // Weather monitoring begins
                long startTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

                // Inference with load balancing
                NeuralNetworkModel model = (NeuralNetworkModel)loadBalancer.GetNextModel();
                object result = model.Predict(input);

                // Calculate the inference time
                long endTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                long timeTaken = endTime - startTime;

                // Detail logging via the monitor
                monitor.LogInference(input, result, timeTaken);

                // Save the result in the cache
                cache.AddToCache(input, result);

                Console.WriteLine("Balanced Model Result: " + result);
            }

            // Inference with A/B testing
            object inputABTest = "Input 2";
            object abResult = abTestManager.Predict(inputABTest);

            // A/B inference log
            monitor.LogInference(inputABTest, abResult, 0);
        }
    }
}
