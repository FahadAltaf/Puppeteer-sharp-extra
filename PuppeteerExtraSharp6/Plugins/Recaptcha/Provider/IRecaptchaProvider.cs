using System.Threading.Tasks;

namespace PuppeteerExtraSharp6.Plugins.Recaptcha.Provider
{
    public interface IRecaptchaProvider
    {
        public Task<string> GetSolution(string key, string pageUrl, string proxyStr = null);
    }
}
