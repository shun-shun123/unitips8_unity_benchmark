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
        
        public static PreSetupAction PreSetup { get; }

        public static int[] Size = {128, 4096, 4096 * 16};

        static BenchmarkConfig()
        {
            PreSetup = PreSetupAction.FullGC;
        }
    }
}