﻿using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using RestSharp;

namespace SeatsioDotNet
{
    public class SeatsioException : Exception
    {
        public readonly List<SeatsioApiError> Errors;
        public readonly string RequestId;

        private SeatsioException(List<SeatsioApiError> errors, string requestId, IRestResponse response) : base(ExceptionMessage(errors, requestId, response))
        {
            Errors = errors;
            RequestId = requestId;
        }

        private SeatsioException(IRestResponse response) : base(ExceptionMessage(response))
        {
        }

        private static string ExceptionMessage(List<SeatsioApiError> errors, string requestId, IRestResponse response)
        {
            string exceptionMessage = ExceptionMessage(response);            
            exceptionMessage += " Reason: " + String.Join(", ", errors.Select(x => x.Message)) + ".";
            exceptionMessage += " Request ID: " + requestId;
            return exceptionMessage;
        }

        private static string ExceptionMessage(IRestResponse response)
        {
            var request = response.Request;
            return request.Method + " " + response.ResponseUri + " resulted in a " + (int) response.StatusCode + " " + response.StatusDescription + " response.";
        }

        public static SeatsioException From(IRestResponse response)
        {
            if (response.ContentType != null && response.ContentType.Contains("application/json"))
            {
                var parsedException = JsonConvert.DeserializeObject<SeatsioExceptionTo>(response.Content);
                return new SeatsioException(parsedException.Errors, parsedException.RequestId, response);
            }

            return new SeatsioException(response);
        }

        public struct SeatsioExceptionTo
        {
            public List<SeatsioApiError> Errors { get; set; }
            public string RequestId { get; set; }
        }
    }
}