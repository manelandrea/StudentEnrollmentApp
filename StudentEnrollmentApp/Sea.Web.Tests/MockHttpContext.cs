using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Sea.Web.Tests
{
    public class MockHttpContext : HttpContextBase
    {
        private readonly IPrincipal _user = new GenericPrincipal(new GenericIdentity("someUser"), null /* roles */);

        public override IPrincipal User
        {
            get
            {
                return _user;
            }
            set
            {
                base.User = value;
            }
        }
    }
}
