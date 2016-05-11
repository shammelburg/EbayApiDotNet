using eBay.Service.Core.Soap;

namespace EbayConsole
{
    public class EbayCalls
    {
        public static eBayAPIInterfaceService eBayServiceCall(string CallName)
        {
            string endpoint = "https://api.sandbox.ebay.com/wsapi";
            string siteId = "3";
            string appId = AppSettingsHelper.AppID;     // use your app ID
            string devId = AppSettingsHelper.DevID;     // use your dev ID
            string certId = AppSettingsHelper.CertID;   // use your cert ID
            string version = "965";
            // Build the request URL
            string requestURL = endpoint
            + "?callname=" + CallName
            + "&siteid=" + siteId
            + "&appid=" + appId
            + "&version=" + version
            + "&routing=default";

            eBayAPIInterfaceService service = new eBayAPIInterfaceService();
            // Assign the request URL to the service locator.
            service.Url = requestURL;
            // Set credentials
            service.RequesterCredentials = new CustomSecurityHeaderType();
            service.RequesterCredentials.eBayAuthToken = AppSettingsHelper.Token;    // use your token
            service.RequesterCredentials.Credentials = new UserIdPasswordType();
            service.RequesterCredentials.Credentials.AppId = appId;
            service.RequesterCredentials.Credentials.DevId = devId;
            service.RequesterCredentials.Credentials.AuthCert = certId;
            return service;
        }
    }
}
