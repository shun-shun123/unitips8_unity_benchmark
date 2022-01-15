using System.Collections;
using System.Collections.Generic;

namespace UniTips.Benchmark
{
    public class ListSimpleCreateNewInstanceBenchmark : BenchmarkMonoBehaviour
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
            var list = new List<int>();
            for (var i = 0; i < size; i++)
            {
                list.Add(i);
            }
        }

        public override string ExportResult()
        {
            var result = base.ExportResult();
            result += $"Size: {size}\n";
            Exportor.Export(result);
            return result;
        }

        public override void CleanUp()
        {
        }
    }
}