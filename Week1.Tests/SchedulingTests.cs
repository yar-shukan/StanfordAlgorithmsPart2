using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Core;
using Core.Algorithms;
using NUnit.Framework;

namespace Week1.Tests
{
    [TestFixture]
    public class SchedulingTests
    {
        private Scheduling.Job[] _jobs;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _jobs = GetJobsFromFile().ToArray();
        }

        [Description("Question 1")]
        [Test]
        public void GetCompletionTimeWeightedSum_WeightMinusLengthComparer_CorrectResult()
        {
            long completionTime = _jobs.GetCompletionTimeWeightedSum(new WeightMinusLengthComparer());

            Assert.AreEqual(69119377652, completionTime);
        }

        [Description("Question 2")]
        [Test]
        public void GetCompletionTimeWeightedSum_WeightDivideByLengthComparer_CorrectResult()
        {
            long completionTime = _jobs.GetCompletionTimeWeightedSum(new WeightDivideByLengthComparer());

            Assert.AreEqual(67311454237, completionTime);
        }

        [Test]
        public void GetCompletionTimeWeightedSum_AllComparers_CorrectResult()
        {
            var jobs = new[] { new Scheduling.Job(3, 5), new Scheduling.Job(1, 2) };

            long result1 = jobs.GetCompletionTimeWeightedSum(new WeightMinusLengthComparer());
            long result2 = jobs.GetCompletionTimeWeightedSum(new WeightDivideByLengthComparer());

            Assert.AreEqual(23, result1);
            Assert.AreEqual(22, result2);
        }

        class WeightMinusLengthComparer : IComparer<Scheduling.Job>
        {
            public int Compare(Scheduling.Job x, Scheduling.Job y)
            {
                return (x.Weight - x.Length).CompareTo(y.Weight - y.Length);
            }
        }

        class WeightDivideByLengthComparer : IComparer<Scheduling.Job>
        {
            public int Compare(Scheduling.Job x, Scheduling.Job y)
            {
                return (x.Weight / (double)x.Length).CompareTo(y.Weight / (double)y.Length);
            }
        }

        private static IEnumerable<Scheduling.Job> GetJobsFromFile()
        {
            using (var fileStream = new FileStream("jobs.txt", FileMode.Open, FileAccess.Read))
            {
                using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
                {
                    int jobCount = int.Parse(streamReader.ReadLine());
                    while (!streamReader.EndOfStream)
                    {
                        var weightLengthTuple = streamReader.ReadLine().SplitAsInts();
                        yield return new Scheduling.Job(weightLengthTuple[0], weightLengthTuple[1]);
                    }
                }
            }
        }
    }
}
