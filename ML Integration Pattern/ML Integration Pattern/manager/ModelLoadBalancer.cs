using ML_Integration_Pattern.model;

namespace ML_Integration_Pattern.manager
{
    public class ModelLoadBalancer
    {
        private List<MLModel> Models;
        private int CurrentIndex;

        public ModelLoadBalancer(List<MLModel> Models)
        {
            if (Models == null || Models.Count == 0)
            {
                throw new ArgumentException("Models list cannot be null or empty");
            }

            this.Models = Models;
            this.CurrentIndex = 0;
        }

        public MLModel GetNextModel()
        {
            int index = Interlocked.Increment(ref CurrentIndex) % Models.Count;
            return Models[index];
        }
    }
}
