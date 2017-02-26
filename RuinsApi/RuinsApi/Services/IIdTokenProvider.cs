using System;
using System.Threading.Tasks;

namespace RuinsApi.Services
{
	public interface IIdTokenProvider
	{
		Task<string> GetIdToken();
		Task<DateTime> GetTokenExpiry();
	}
}
