using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLibrary.Abstractions;

public record Error
{
    public static readonly Error None = new(string.Empty, string.Empty, ErrorType.Failure);
    public static readonly Error NullValue = new("Error.NullValue", "Null value provided.", ErrorType.NotFound);

    private Error(string Code, string Description, ErrorType errorType)
    {

    }

    public string Code { get; }
    public string Description { get; }
    public ErrorType Type { get; }

    public static Error Failure(string code, string description) =>
        new(code, description, ErrorType.Failure);    
    public static Error Validation(string code, string description) =>
        new(code, description, ErrorType.Validation);    
    public static Error NotFound(string code, string description) =>
        new(code, description, ErrorType.NotFound);    
    public static Error Conflict(string code, string description) =>
        new(code, description, ErrorType.Conflict);
}

public enum ErrorType
{
    Failure = 0,
    Validation = 1,
    NotFound = 2,
    Conflict = 3
}

internal class Result
{
    public bool IsSuccess { get; set; }
    public bool IsFailure => !IsSuccess;
}