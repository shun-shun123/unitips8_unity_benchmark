using System.Collections;
using UniTipsBenchmark.Benchmark.Config;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.UI;

namespace UniTips.Benchmark
{

    public class BenchmarkExecutor : MonoBehaviour
    {
        [SerializeField]
        private BenchmarkMonoBehaviour[] benchmarkMonoBehaviours;

        [SerializeField]
        private Dropdown benchmarkDropdown;

        [SerializeField]
        private Button executeButton;

        [SerializeField]
        private int fpsCount;

        private void Start()
        {
            Application.targetFrameRate = fpsCount;
            
            var options = benchmarkDropdown.options;
            options.Clear();
            foreach (var benchmark in benchmarkMonoBehaviours)
            {
                options.Add(new Dropdown.OptionData(benchmark.BenchmarkTitle.Substring(BenchmarkMonoBehaviour.PROFILER_PREFIX.Length)));
            }
            benchmarkDropdown.RefreshShownValue();
            
            executeButton.onClick.AddListener(OnClickBenchmark);
        }

        private void OnClickBenchmark()
        {
            var index = benchmarkDropdown.value;
            var benchmarkMonoBehaviour = benchmarkMonoBehaviours[index];
            ExecuteBenchmark(benchmarkMonoBehaviour);
        }

        private void ExecuteBenchmark(BenchmarkMonoBehaviour benchmarkMonoBehaviour)
        {
            if (benchmarkMonoBehaviour.Type == BenchmarkMonoBehaviour.BenchmarkType.Once)
            {
                StartCoroutine(ExecuteBenchmarkOnce(benchmarkMonoBehaviour));
            }
            else if (benchmarkMonoBehaviour.Type == BenchmarkMonoBehaviour.BenchmarkType.WithParams)
            {
                StartCoroutine(ExecuteBenchmarkWithParams(benchmarkMonoBehaviour));
            }
        }

        private IEnumerator ExecuteBenchmarkOnce(BenchmarkMonoBehaviour benchmarkMonoBehaviour)
        {
            yield return benchmarkMonoBehaviour.PreSetup(null);
            Profiler.BeginSample(benchmarkMonoBehaviour.BenchmarkTitle);
            {
                benchmarkMonoBehaviour.Benchmark();
            }
            Profiler.EndSample();
            benchmarkMonoBehaviour.CleanUp();
        }

        private IEnumerator ExecuteBenchmarkWithParams(BenchmarkMonoBehaviour benchmarkMonoBehaviour)
        {
            foreach (var size in BenchmarkConfig.Size)
            {
                var paramContext = new BenchmarkMonoBehaviour.ParamsContext(size);
                yield return benchmarkMonoBehaviour.PreSetup(paramContext);
                Profiler.BeginSample(benchmarkMonoBehaviour.BenchmarkTitle + $"_{size}");
                {
                    benchmarkMonoBehaviour.Benchmark();
                }
                Profiler.EndSample();
                benchmarkMonoBehaviour.CleanUp();
            }
        }
    }
}