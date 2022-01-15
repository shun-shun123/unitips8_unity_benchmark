using System;
using System.Collections;
using UniTips.Benchmark.Exporter;
using UniTipsBenchmark.Benchmark.Config;
using UnityEngine;
using UnityEngine.Profiling;

namespace UniTips.Benchmark
{
    /// <summary>
    /// 一つのシナリオに対するベンチマークを測定できるコンポーネント
    /// </summary>
    public abstract class BenchmarkMonoBehaviour : MonoBehaviour
    {
        public enum BenchmarkType
        {
            Once,
            WithParams
        }

        public class ParamsContext : IBenchmarkContext
        {
            public readonly int TryCount;

            public ParamsContext(int tryCount)
            {
                TryCount = tryCount;
            }
        }

        public struct BenchmarkResult
        {
            public long BeforeManagedUsed;

            public float StartTime;

            public long AfterManagedUsed;

            public float FinishTime;

            public long AllocateInByte => (AfterManagedUsed - BeforeManagedUsed);

            public float ElapsedMillis => (FinishTime - StartTime) * 1000.0f;

            public override string ToString()
            {
                return $@"BeforeManagedUsed: {BeforeManagedUsed}, AfterManagedUsed: {AfterManagedUsed}, StartTime: {StartTime}, FinishTime: {FinishTime}";
            }
        }
        
        public interface IBenchmarkContext {}

        public interface IBenchmarkExportable
        {
            void Export(string result);
        }

        public static IBenchmarkExportable Exportor = DefaultExporter.Default;

        [SerializeField]
        private BenchmarkType benchmarkType;

        [SerializeField]
        private string benchmarkTitle;

        public BenchmarkType Type => benchmarkType;
        
        public string BenchmarkTitle => benchmarkTitle;

        public BenchmarkResult Result;
        
        /// <summary>
        /// Benchmark測定直前に呼び出される
        /// </summary>
        public virtual IEnumerator PreSetup(IBenchmarkContext context)
        {
            yield return PreSetupGC();
            Result.BeforeManagedUsed = Profiler.GetMonoUsedSizeLong();
            Result.StartTime = Time.realtimeSinceStartup;
            Debug.Log(Result);
        }

        /// <summary>
        /// Benchmark測定対象のメソッド
        /// </summary>
        public abstract void Benchmark();

        public virtual string ExportResult()
        {
            Result.AfterManagedUsed = Profiler.GetMonoUsedSizeLong();
            Result.FinishTime = Time.realtimeSinceStartup;
            Debug.Log(Result);
            var result = $"====={benchmarkTitle}=====\n";
            result += $"GC.Alloc: {Result.AllocateInByte}, Elapsed: {Result.ElapsedMillis}\n";
            return result;
        }

        /// <summary>
        /// Benchmark測定後の後処理で呼び出される
        /// </summary>
        public abstract void CleanUp();

        /// <summary>
        /// PreSetup時のGC処理
        /// </summary>
        private IEnumerator PreSetupGC()
        {
            switch (BenchmarkConfig.PreSetup)
            {
                case BenchmarkConfig.PreSetupAction.None:
                    break;
                case BenchmarkConfig.PreSetupAction.ManagedGC:
                    GC.Collect();
                    break;
                case BenchmarkConfig.PreSetupAction.UnmanagedResourceGC:
                    yield return Resources.UnloadUnusedAssets();
                    break;
                case BenchmarkConfig.PreSetupAction.FullGC:
                    GC.Collect();
                    yield return Resources.UnloadUnusedAssets();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}