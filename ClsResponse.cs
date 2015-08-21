using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CommonCls
{
    public static class ClsResponse
    {
        public static void RedirectUser(this HttpResponse response, string url)
        {
            if (response.IsRequestBeingRedirected)
                return;
            response.Redirect(url, false);
            var context = HttpContext.Current;
            if (context != null)
            {
                context.ApplicationInstance.CompleteRequest();
            }
        }  
    }
}
