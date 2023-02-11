using BlogEngineNet.Core.Domain;

namespace BlogEngineNet.Core.Models;

public class Result<T>
{
    public Result()
    {
        Value = default;
        ResultType = ResultType.ERROR;
        Message = "Something went wrong!";
    }

    public string Message { get; set; }
    public ResultType ResultType { get; set; }
    public Exception Exception { get; set; }
    public T Value { get; set; }
}
