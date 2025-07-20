// <copyright file="Result.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace EventManagementSystem.Application.Patterns
{
    public class Result
    {
        public bool IsSuccess { get; set; }

        public bool IsFailure => !this.IsSuccess;

        public string? Error { get; set; }

        public static Result Success() => new () { IsSuccess = true };

        public static Result Failure(string error) => new () { IsSuccess = false, Error = error };
    }
}
