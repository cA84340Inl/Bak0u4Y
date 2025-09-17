// 代码生成时间: 2025-09-18 00:06:10
 * Author: [Your Name]
 * Date: [Date]
 */
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ApiResponseFormatterTool
{
    // Define a class to represent a standardized API response.
    public class ApiResponse<T>
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("data")]
        public T Data { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("errors")]
        public ErrorDetails[] Errors { get; set; }

        // Constructor for a successful response.
        public ApiResponse(T data, string message = "")
        {
            Success = true;
            Data = data;
            Message = message;
        }

        // Constructor for an error response.
        public ApiResponse(ErrorDetails[] errors)
        {
            Success = false;
            Errors = errors;
        }
    }

    // Define a class to represent error details.
    public class ErrorDetails
    {
        [JsonPropertyName("code")]
        public int Code { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }
    }

    // Define a class to handle API response formatting.
    public static class ApiFormatter
    {
        // Method to format a successful API response.
        public static string FormatSuccessResponse<T>(T data, string message = "")
        {
            try
            {
                var response = new ApiResponse<T>(data, message);
                return JsonSerializer.Serialize(response, new JsonSerializerOptions { WriteIndented = true });
            }
            catch (Exception ex)
            {
                // Log the exception and return an error response.
                // In a real-world scenario, you would log the exception to a logging system.
                Console.WriteLine($"Error formatting success response: {ex.Message}");
                return FormatErrorResponse(new ErrorDetails[] { new ErrorDetails { Code = 500, Message = "Internal Server Error" } });
            }
        }

        // Method to format an error API response.
        public static string FormatErrorResponse(ErrorDetails[] errors)
        {
            try
            {
                var response = new ApiResponse<object>(errors);
                return JsonSerializer.Serialize(response, new JsonSerializerOptions { WriteIndented = true });
            }
            catch (Exception ex)
            {
                // Log the exception and return a default error response.
                Console.WriteLine($"Error formatting error response: {ex.Message}");
                return "{"success":false,"errors":[{"code":500,"message":"Internal Server Error"}]}