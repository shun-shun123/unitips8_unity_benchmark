using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UniTips.Benchmark.Linq
{
    public class WhereIEnumerableBenchmark : BenchmarkMonoBehaviour
    {
        private int size;

        private IEnumerable<int> data;

        public override IEnumerator PreSetup(IBenchmarkContext context)
        {
            yield return base.PreSetup(context);
            if (context is ParamsContext paramsContext)
            {
                size = paramsContext.TryCount;
            }

            data = Enumerable.Range(0, size);
            var query = data.Where(i => i % 2 == 0);
            foreach (var i in query){}
        }

        public override void Benchmark()
        {
            var query = data.Where(i => i % 2 == 0);
            foreach (var i in query){}
        }

        public override void CleanUp()
        {
            Debug.Log($"Finish: {BenchmarkTitle}, {size}");
        }
    }
}