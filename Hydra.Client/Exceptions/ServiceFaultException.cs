using System;
using System.Runtime.Serialization;

namespace Hydra.Client.Exceptions
{
    [Serializable]
    public class ServiceFaultException : ApplicationException
    {
        public static ServiceFaultException Create(int errorCode)
        {
            string message;
            switch (errorCode)
            {
                case 0:
                    message = "Success";
                    break;
                default:
                    message = $"{errorCode}";
                    break;
            }
            
            return new ServiceFaultException(errorCode, message);
        }


        public int ErrorCode { get; }

        private ServiceFaultException(int errorCode, string message)
        : base(message)
        {
            ErrorCode = errorCode;
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(ErrorCode), ErrorCode);
            base.GetObjectData(info, context);
        }
    }
}