using System;

namespace Confetti
{
    public static class Result
    {
        public static Result<T> FromValue<T>(T value)
        {
            return new Result<T>(value);
        }
    }

    public class Result<T>
    {
        public static Result<T> Failure { get; } = new Result<T>();

        // a failure
        private Result()
        {
            isFailed = true;
        }

        internal Result(T value)
        {
            this.value = value;
        }

        private readonly T value;

        public T Value
        {
            get
            {
                if (isFailed)
                {
                    throw new InvalidOperationException("Cannot get value of failed result");
                }

                return value;
            }
        }

        private readonly bool isFailed;

        public bool IsFailed => isFailed;
    }
}