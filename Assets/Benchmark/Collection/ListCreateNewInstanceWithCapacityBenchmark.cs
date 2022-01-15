using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UniTips.Benchmark.Collections
{
    public class ListCreateNewInstanceWithCapacityBenchmark : BenchmarkMonoBehaviour
    {
        private int size;

        public override IEnumerator PreSetup(IBenchmarkContext context)
        {
            yield return base.PreSetup(context);
            if (context is ParamsContext paramsContext)
            {
                size = paramsContext.TryCount;
            }
        }

        public override void Benchmark()
        {
            var list = new List<int>(size);
            for (var i = 0; i < size; i++)
            {
                list.Add(i);
            }
        }

        public override void CleanUp()
        {
            Debug.Log($"Finish: {BenchmarkTitle}, {size}");
        }
    }
}