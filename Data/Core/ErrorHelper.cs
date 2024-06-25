using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using Refit;

namespace productDemo.Data.Core
{
    public static class ExceptionExtensions
    {
        public static State<T> ToError<T>(this Exception exception)
        {
            const string defaultErrorMsg = "Unable to connect with server at the moment. Please try again later.";
            try
            {
                if (exception is IOException && exception.Message == "No Internet")
                    return State<T>.Error(defaultErrorMsg);

                if (exception is HttpRequestException httpException)
                {
                    // var response = httpException.Response;
                    // if (response != null)
                    // {
                    //     using (var content = response.Content)
                    //     {
                    //         var rawResponse = content.ReadAsStringAsync().Result;
                    //         var error = JsonConvert.DeserializeObject<RawHttpError>(rawResponse);
                    //         return State<T>.Error(defaultErrorMsg);
                    //     }
                    // }
                    return State<T>.Error(defaultErrorMsg);
                }
                
                try
                {
                    if (exception is ApiException apiException)
                    {
                        var content = apiException.Content;
                        var errorResponse = JsonSerializer.Deserialize<ErrorResponse>(content);

                        // Extract the error message from the ErrorResponse object
                        var errorMessage = errorResponse?.StatusMessage;

                        // If error message is available, use it. Otherwise, use default error message.
                        return State<T>.Error(errorMessage ?? defaultErrorMsg);
                    }

                    // Add more specific error handling here...

                    // If no specific error handling is provided, return a default error message
                    return State<T>.Error(defaultErrorMsg);
                }
                catch (Exception)
                {
                    // If an exception occurs during error handling, return a default error message
                    return State<T>.Error(defaultErrorMsg);
                }

                if (exception is WebException webException && webException.Status == WebExceptionStatus.Timeout)
                    return State<T>.Error(defaultErrorMsg);

                if (exception is WebException &&
                    ((WebException)exception).Status == WebExceptionStatus.NameResolutionFailure)
                    return State<T>.Error(defaultErrorMsg);

                // Add more specific error handling here...

                return State<T>.Error(defaultErrorMsg);
            }
            catch (Exception)
            {
                return State<T>.Error(defaultErrorMsg);
            }
        }
    }

    public class ErrorResponse
    {
        [JsonPropertyName("status_message")]
        public string StatusMessage { get; set; }

        [JsonPropertyName("status_code")]
        public int StatusCode { get; set; }
    }
}