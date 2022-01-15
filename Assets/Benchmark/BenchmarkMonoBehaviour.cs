using System;
using System.Collections;
using UniTipsBenchmark.Benchmark.Config;
using UnityEngine;

namespace UniTips.Benchmark
{
    /// <summary>
    /// 一つのシナリオに対するベンチマークを測定できるコンポーネント
    /// </summary>
    public abstract class BenchmarkMonoBehaviour : MonoBehaviour
    {
        public const string PROFILER_PREFIX = "[BENCHMARK_MONOBEHAVIOUR]";
        
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

        public interface IBenchmarkContext {}

        [SerializeField]
        private BenchmarkType benchmarkType;

        [SerializeField]
        protected string benchmarkTitle;

        public BenchmarkType Type => benchmarkType;
        
        public string BenchmarkTitle => PROFILER_PREFIX + benchmarkTitle;

        /// <summary>
        /// Benchmark測定直前に呼び出される
        /// </summary>
        public virtual IEnumerator PreSetup(IBenchmarkContext context)
        {
            yield return PreSetupGC();
        }

        /// <summary>
        /// Benchmark測定対象のメソッド
        /// </summary>
        public abstract void Benchmark();

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