using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Algorithms
{
    public static class Scheduling
    {
        public class Job
        {
            public Job(int weight, int length)
            {
                Weight = weight;
                Length = length;
                if (weight <= 0)
                {
                    throw new ArgumentOutOfRangeException("weight");
                }
                if (length <= 0)
                {
                    throw new ArgumentOutOfRangeException("length");
                }
            }

            public int Weight { get; private set; }

            public int Length { get; private set; }
        }

        public static long GetCompletionTimeWeightedSum(this IReadOnlyCollection<Job> jobs, IComparer<Job> jobComparer)
        {
            long completionTime = 0;
            long sum = 0;
            foreach (Job job in jobs.OrderByDescending(job => job, jobComparer)
                                    .ThenByDescending(job => job.Weight))
            {
                completionTime += job.Length;
                sum += completionTime * job.Weight;
            }
            return sum;
        }
    }
}