using UnityEngine;

namespace UniTips.Benchmark.Exporter
{

    public class DefaultExporter : BenchmarkMonoBehaviour.IBenchmarkExportable
    {
        public static BenchmarkMonoBehaviour.IBenchmarkExportable Default => new DefaultExporter();
        
        public void Export(string result)
        {
            Debug.Log("<color=#FFA833>" + result + "</color>");
        }
    }
}