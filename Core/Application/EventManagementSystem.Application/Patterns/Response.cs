// <copyright file="Response.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace EventManagementSystem.Application.Patterns
{
    public class Response<T>
    {
        public bool Success { get; set; }

        public string? Message { get; set; }

        public List<string>? Errors { get; set; }

        public T? Data { get; set; }

        public static Response<T> SuccessResponse(T data, string? message = null) => new () { Success = true, Data = data, Message = message };

        public static Response<T> FailureResponse(List<string> errors, string? message = null) => new () { Success = false, Errors = errors, Message = message };
    }
}
