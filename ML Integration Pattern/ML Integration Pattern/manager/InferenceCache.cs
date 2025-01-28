
namespace ML_Integration_Pattern.manager
{
    public class InferenceCache
    {
        private readonly Dictionary<object, object> Cache;

        public InferenceCache()
        {
            Cache = new Dictionary<object, object>();
        }

        public object GetCachedResult(object input)
        {
            return Cache[input];
        }

        public void AddToCache(object input, object result)
        {
            Cache.Add(input, result);
        }

        public bool IsCached(object input)
        {
            return Cache.ContainsKey(input);
        }
    }
}
