namespace IntervalCronGenerator.Core
{
    public interface ICronPostProcessor
    {
        string PostProcess(string cronExpression);
    }
}