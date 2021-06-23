using JobPortalBlazor.Shared;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobPortalBlazor.Client
{
	public interface IAuthorizeApi
	{
		Task Login(LoginParameters loginParameters);
		Task Register(RegisterParameters registerParameters);
		Task Logout();
		Task<UserInfo> GetUserInfo();
	}
}
