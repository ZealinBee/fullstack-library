using IntegrifyLibrary.Domain;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegrifyLibrary.Business;
public interface IAuthService
{
    Task<string> VerifyCredentials(LoginUserDto credentials);
}