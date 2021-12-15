namespace Douanier.Abstractions
{
    /// <summary>
    /// Represents the result of an operation.
    /// </summary>
    public class DouanierResult
    {
        private static readonly DouanierResult _success = new DouanierResult(true);

        /// <summary>
        ///     Failure constructor that takes error messages
        /// </summary>
        /// <param name="errors"></param>
        public DouanierResult(params string[] errors) : this((IEnumerable<string>)errors)
        {
        }

        /// <summary>
        ///     Failure constructor that takes error messages
        /// </summary>
        /// <param name="errors"></param>
        public DouanierResult(IEnumerable<string> errors)
        {
            if (errors == null)
            {
                errors = new[] { DouanierConstants.Errors.DefaultError };
            }
            Succeeded = false;
            Errors = errors;
        }

        /// <summary>
        /// Constructor that takes whether the result is successful
        /// </summary>
        /// <param name="success"></param>
        protected DouanierResult(bool success)
        {
            Succeeded = success;
            Errors = new string[0];
        }

        /// <summary>
        ///     True if the operation was successful
        /// </summary>
        public bool Succeeded { get; private set; }

        /// <summary>
        ///     List of errors
        /// </summary>
        public IEnumerable<string> Errors { get; private set; }

        /// <summary>
        ///     Static success result
        /// </summary>
        /// <returns></returns>
        public static DouanierResult Success
        {
            get { return _success; }
        }

        /// <summary>
        ///     Failed helper method
        /// </summary>
        /// <param name="errors"></param>
        /// <returns></returns>
        public static DouanierResult Failed(params string[] errors)
        {
            return new DouanierResult(errors);
        }
    }

    public class DouanierResult<T> : DouanierResult
        where T : class
    {
        public T? Payload { get; private set; }
    }
}