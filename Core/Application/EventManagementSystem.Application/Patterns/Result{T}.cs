// <copyright file="Result{T}.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace EventManagementSystem.Application.Patterns
{
    public class Result<T> : Result
    {
        public T? Value { get; set; }

        public static Result<T> Success(T value) => new () { IsSuccess = true, Value = value };

        public static new Result<T> Failure(string error) => new () { IsSuccess = false, Error = error };
    }
}
