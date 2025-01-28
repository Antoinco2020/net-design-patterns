
namespace ML_Integration_Pattern.monitor
{
    public class ModelMonitor
    {
        public void LogInference(object Input, object Result, long TimeTaken)
        {
            //put your monitoring system
            Console.WriteLine("Input: " + Input);
            Console.WriteLine("Result: " + Result);
            Console.WriteLine("Inference Time: " + TimeTaken + "ms");
        }
    }
}
