namespace productDemo.Data.Core
{
    public sealed class State<T>
    {
        public T Data { get; }
        public bool IsLoading { get; }
        public string ErrorMessage { get; }

        private State(T data, bool isLoading, string errorMessage)
        {
            Data = data;
            IsLoading = isLoading;
            ErrorMessage = errorMessage;
        }

        public static State<T> Success(T data) =>
            new State<T>(data, false, null);

        public static State<T> Loading() =>
            new State<T>(default(T), true, null);

        public static State<T> Error(string errorMessage) =>
            new State<T>(default(T), false, errorMessage);
    }
}