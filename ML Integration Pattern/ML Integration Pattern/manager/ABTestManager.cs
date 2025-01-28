using ML_Integration_Pattern.model;

namespace ML_Integration_Pattern.manager
{
    public class ABTestManager
    {
        private MLModel ModelA;
        private MLModel ModelB;
        private Random Random;

        public ABTestManager(MLModel modelA, MLModel modelB)
        {
            ModelA = modelA;
            ModelB = modelB;
            Random = new Random();
        }

        public object Predict(object input)
        {
            // Randomly choose between ModelA and ModelB
            if (Random.Next(0, 2) == 0) // 0 or 1
            {
                Console.WriteLine("Using Model A");
                return ModelA.Predict(input);
            }
            else
            {
                Console.WriteLine("Using Model B");
                return ModelB.Predict(input);
            }
        }
    }
}
