using NBench.Reporting.Targets;
using NBench.Sdk;
using NBench.Sdk.Compiler;
using System;
using System.Collections;
using System.Linq;
using NUnit.Framework;

namespace TaskManager.NBench.Test
{
    public abstract class PerformanceTestSetup<T>
    {

        [TestCaseSource(nameof(Benchmarks))]
        public void PerformanceTests(Benchmark bm)
        {
            Benchmark.PrepareForRun();
            bm.Run();
            bm.Finish();
        }

        public static IEnumerable Benchmarks()
        {
            var benchDiscover = new ReflectionDiscovery(new ActionBenchmarkOutput(report => { }, result =>
            {
                foreach (var assertion in result.AssertionResults)
                {
                    Assert.True(assertion.Passed, result.BenchmarkName + " " + assertion.Message);
                    Console.WriteLine(assertion.Message);
                }
            }));

            var benchmarks = benchDiscover.FindBenchmarks(typeof(T)).ToList();
            foreach (var bms in benchmarks)
            {
                var name = bms.BenchmarkName.Split('+')[1];
                yield return new TestCaseData(bms).SetName(name);
            }
        }

    }
}
