using System.Diagnostics.CodeAnalysis;

namespace MedNet.Application.Models;

public class Result
{
    [MemberNotNullWhen(false, nameof(Error), nameof(ErrorCode))]
    public bool IsSuccess { get; }
    public string? Error { get; }
    public string? ErrorCode { get; }

    public static Result Success() => new(true, null, null);
    public static Result Failure(string error, string errorCode = "") => new(false, error, errorCode);

    private Result(bool isSuccess, string? error, string? errorCode)
    {
        IsSuccess = isSuccess;
        Error = error;
        ErrorCode = errorCode;
    }
}

public class Result<TValue>
{
    [MemberNotNullWhen(false, nameof(Error), nameof(ErrorCode))]
    [MemberNotNullWhen(true, nameof(Value))]
    public bool IsSuccess { get; }
    public TValue? Value { get; }
    public string? Error { get; }
    public string? ErrorCode { get; }

    public static Result<TValue> Success(TValue value) => new(true, value, null, null);
    public static Result<TValue> Failure(string error, string errorCode = "") => new(false, default, error, errorCode);

    private Result(bool isSuccess, TValue? value, string? error, string? errorCode)
    {
        IsSuccess = isSuccess;
        Value = value;
        Error = error;
        ErrorCode = errorCode;
    }
}
