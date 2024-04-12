using System.Runtime.Serialization;

namespace lab_14.Persistence
{
    [Serializable]
    internal class AssignmentNotFoundException : Exception
    {
        public AssignmentNotFoundException()
        {
        }

        public AssignmentNotFoundException(string? message) : base(message)
        {
        }

        public AssignmentNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected AssignmentNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}