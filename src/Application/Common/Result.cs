namespace Application.Common;

public class Result
{
    public bool IsSuccess { get; set; }
    public string? Error { get; set; }
    public int? Code { get; set; }

    public static Result Success() => new() { IsSuccess = true };
    public static Result Failure(string error, int? code = null) => new()
    {
        IsSuccess = false,
        Error = error,
        Code = code
    };
}

public class Result<T> : Result
{
    public T? Value { get; set; }

    public static Result<T> Success(T value) => new()
    {
        IsSuccess = true,
        Value = value
    };

    public new static Result<T> Failure(string error, int? code = null) => new()
    {
        IsSuccess = false,
        Error = error,
        Code = code
    };
}
