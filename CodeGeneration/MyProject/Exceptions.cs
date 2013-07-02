
using System;
using System.Runtime.Serialization;

namespace MyProject
{

    [Serializable]
    public sealed class EntityNotFoundException: Exception
    {
        private const string defaultMessage = "Specified entity was not found";
        
        public System.Type EntityType
        {
            get;
            private set;
        }

        public System.Guid EntityId
        {
            get;
            private set;
        }

        public EntityNotFoundException()
        {
        }

        public EntityNotFoundException(string message): base(message)
        {
        }

        public EntityNotFoundException(string message, Exception innerException): base(message, innerException)
        {
        }

        public EntityNotFoundException(Type EntityType, Guid EntityId): base(defaultMessage)
        {
            this.EntityType = EntityType;
            this.EntityId = EntityId;
        }

        public EntityNotFoundException(Type EntityType, Guid EntityId, Exception innerException): base(defaultMessage, innerException)
        {
            this.EntityType = EntityType;
            this.EntityId = EntityId;
        }

        public EntityNotFoundException(Type EntityType, Guid EntityId, string message, Exception innerException): base(message, innerException)
        {
            this.EntityType = EntityType;
            this.EntityId = EntityId;
        }
    }

    [Serializable]
    public sealed class InvalidEmailFormatException: Exception
    {
        private const string defaultMessage = "Specified e-mail in not in a correct format";
        
        public System.Guid UserId
        {
            get;
            private set;
        }

        public System.String Email
        {
            get;
            private set;
        }

        public InvalidEmailFormatException()
        {
        }

        public InvalidEmailFormatException(string message): base(message)
        {
        }

        public InvalidEmailFormatException(string message, Exception innerException): base(message, innerException)
        {
        }

        public InvalidEmailFormatException(Guid UserId, String Email): base(defaultMessage)
        {
            this.UserId = UserId;
            this.Email = Email;
        }

        public InvalidEmailFormatException(Guid UserId, String Email, Exception innerException): base(defaultMessage, innerException)
        {
            this.UserId = UserId;
            this.Email = Email;
        }

        public InvalidEmailFormatException(Guid UserId, String Email, string message, Exception innerException): base(message, innerException)
        {
            this.UserId = UserId;
            this.Email = Email;
        }
    }

}
