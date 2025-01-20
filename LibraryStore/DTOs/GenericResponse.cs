namespace LibraryStore.DTOs
{
    public class GenericResponse
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public object? Data { get; set; }
        public IEnumerable<FieldError> Errors { get; set; }

        public static GenericResponse Success(object? data, string msg = "")
        {
            return new GenericResponse
            {
                IsSuccess = true,
                Message = msg,
                Data = data,
            };
        }

        public static GenericResponse Failure(string message, string field = "")
        {
            return new GenericResponse
            {
                IsSuccess = false,
                Errors = new List<FieldError>
                    {
                        new FieldError
                        {
                            Field = field,
                            Error = message
                        }
                    }
            };
        }
        public static GenericResponse Failure(List<FieldError> errors)
        {
            return new GenericResponse
            {
                IsSuccess = false,
                Errors = errors
            };
        }

        public static GenericResponse Failure()
        {
            return new GenericResponse
            {
                IsSuccess = false,
            };
        }

    }
    public class FieldError
    {
        public string Field { get; set; }
        public string Error { get; set; }
        public FieldError() { }
        public FieldError(string field, string error)
        {
            Field = field;
            Error = error;
        }
        public FieldError(string error)
        {
            Error = error;
        }
    }
}