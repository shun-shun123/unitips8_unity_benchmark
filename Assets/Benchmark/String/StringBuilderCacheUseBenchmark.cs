using UnityEngine;

namespace UniTips.Benchmark.String
{
    public class StringBuilderCacheUseBenchmark : BenchmarkMonoBehaviour
    {
        public override void Benchmark()
        {
            for (var i = 0; i < 10; i++)
            {
                Core();
            }
        }

        private void Core()
        {
            var sb = StringBuilderCache.Acquire();
            sb.Append("Hello");
            sb.Append(", ");
            sb.Append("World");
            sb.Append('!');
            sb.Append('!');
            StringBuilderCache.Release(sb);
        }

        public override void CleanUp()
        {
            Debug.Log($"Finish: {BenchmarkTitle}");
        }
    }
}