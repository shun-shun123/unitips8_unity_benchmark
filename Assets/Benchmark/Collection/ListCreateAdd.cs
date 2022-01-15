using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UniTips.Benchmark.Collections
{
    public class ListCreateAdd : BenchmarkMonoBehaviour
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
            var list = new List<int>();
            foreach (var i in data)
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