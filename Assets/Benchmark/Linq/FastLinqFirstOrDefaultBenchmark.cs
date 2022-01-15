using System.Collections;
using System.Linq;
using UnityEngine;

namespace UniTips.Benchmark.Linq
{
    public class FastLinqFirstOrDefaultBenchmark : BenchmarkMonoBehaviour
    {
        private int size;

        private int[] data;

        public override IEnumerator PreSetup(IBenchmarkContext context)
        {
            yield return base.PreSetup(context);
            if (context is ParamsContext paramsContext)
            {
                size = paramsContext.TryCount;
            }

            data = Enumerable.Range(0, size).ToArray();
        }

        public override void Benchmark()
        {
            var query = data.FirstOrDefault(i => i == size - 1);
        }

        public override void CleanUp()
        {
            Debug.Log($"Finish: {BenchmarkTitle}, {size}");
        }
    }
}