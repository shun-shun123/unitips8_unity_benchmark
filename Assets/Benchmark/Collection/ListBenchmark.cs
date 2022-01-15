// using System.Collections.Generic;
// using System.Linq;
// using UniTipsBenchmark.Benchmark.Config;
//
// namespace UniTipsBenchmark.Benchmark.Collection
// {
//     /// <summary>
//     /// <see cref="System.Collections.Generic.List{T}"/>のBenchmark測定
//     /// </summary>
//     public class ListBenchmark
//     {
//         /// <summary>
//         /// 新しいListのインスタンスを生成する方法のBenchmark測定
//         /// </summary>
//         [Config(typeof(BenchmarkConfig))]
//         public class CreateNewInstance
//         {
//             /// <summary>
//             /// Listの要素数をさまざまなパターンで検証
//             /// </summary>
//             [Params(128, 512, 2048, 10048, 40192)]
//             public static int Size { get; set; }
//
//             /// <summary>
//             /// 基準となる処理
//             /// capacityを設定せず、一つずつ要素を追加していく
//             /// </summary>
//             [Benchmark(Baseline = true)]
//             public void SimpleCreate()
//             {
//                 var list = new List<int>();
//                 for (var i = 0; i < Size; i++)
//                 {
//                     list.Add(i);
//                 }
//             }
//             
//             /// <summary>
//             /// capacityを明治的に設定する方法
//             /// Addで追加するサイズ分配列は確保しているため、追加のアロケーションは発生しない
//             /// </summary>
//             [Benchmark]
//             public void CreateWithCapacity()
//             {
//                 var list = new List<int>(Size);
//                 for (var i = 0; i < Size; i++)
//                 {
//                     list.Add(i);
//                 }
//             }
//         }
//         
//         /// <summary>
//         /// 新しいListのインスタンスを生成する方法のBenchmark測定
//         /// </summary>
//         [Config(typeof(BenchmarkConfig))]
//         public class CreateNewInstanceFromSource
//         {
//             /// <summary>
//             /// Listの要素数をさまざまなパターンで検証
//             /// </summary>
//             [Params(128, 512, 2048, 10048, 40192)]
//             public static int Size { get; set; }
//
//             /// <summary>
//             /// 追加する配列データ
//             /// </summary>
//             public static int[] Data;
//
//             [GlobalSetup]
//             public void GlobalSetup()
//             {
//                 Data = CreateIntArray(Size);
//             }
//             
//             /// <summary>
//             /// Addで追加する
//             /// 配列は事前に適切なサイズ分確保しているため、追加のアロケーションは発生しない
//             /// </summary>
//             [Benchmark(Baseline = true)]
//             public void SimpleCreate()
//             {
//                 var list = new List<int>(Data.Length);
//                 foreach (var i in Data)
//                 {
//                     list.Add(i);
//                 }
//             }
//
//             /// <summary>
//             /// AddRangeで追加する
//             /// 配列は事前に適切なサイズ分確保しているため、追加のアロケーションは発生しない
//             /// </summary>
//             [Benchmark]
//             public void CreateFromAddRange()
//             {
//                 var list = new List<int>(Data.Length);
//                 list.AddRange(Data);
//             }
//
//             /// <summary>
//             /// Array.Copy処理を走らせ最適なcapacityで値の初期化も行う
//             /// 配列は事前に適切なサイズ分確保しているため、追加のアロケーションは発生しない
//             /// </summary>
//             [Benchmark]
//             public void CreateFromSource()
//             {
//                 var list = new List<int>(Data);
//             }
//
//             private static int[] CreateIntArray(int size) => Enumerable.Range(1, size).ToArray();
//         }
//     }
// }