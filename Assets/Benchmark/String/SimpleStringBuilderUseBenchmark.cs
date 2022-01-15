using System.Text;
using UnityEngine;

namespace UniTips.Benchmark.String
{
    public class SimpleStringBuilderUseBenchmark : BenchmarkMonoBehaviour
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
            var sb = new StringBuilder();
            sb.Append("Hello");
            sb.Append(", ");
            sb.Append("World");
            sb.Append('!');
            sb.Append('!');
        }

        public override void CleanUp()
        {
            Debug.Log($"Finish: {BenchmarkTitle}");
        }
    }
}