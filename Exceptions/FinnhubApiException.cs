using System.Runtime.Serialization;

namespace StocksApp.Exceptions
{
	public class FinnhubApiException : Exception
	{
		public FinnhubApiException()
		{
		}

		public FinnhubApiException(string? message) : base(message)
		{
		}

		public FinnhubApiException(string? message, Exception? innerException) : base(message, innerException)
		{
		}

		protected FinnhubApiException(SerializationInfo info, StreamingContext context)
		{
		}
	}
}
