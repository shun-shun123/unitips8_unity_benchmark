namespace UniTipsBenchmark.Benchmark.Config
{
    public static class BenchmarkConfig
    {
        public enum PreSetupAction
        {
            None,
            ManagedGC,
            UnmanagedResourceGC,
            FullGC
        }

        public enum Iteration
        {
            Short = 1,
            Default = 10,
            Long = 100,
        }
        
        public static PreSetupAction PreSetup { get; }

        public static int IterationCount { get; }
        
        public static bool MemoryDiagnoser { get; }

        public static int[] Size = {128, 512, 1024};

        static BenchmarkConfig()
        {
            PreSetup = PreSetupAction.FullGC;
            IterationCount = (int) Iteration.Default;
            MemoryDiagnoser = true;
        }
    }
}