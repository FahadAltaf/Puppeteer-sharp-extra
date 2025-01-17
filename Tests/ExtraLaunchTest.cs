using System;
using System.Net;
using PuppeteerExtraSharp6.Plugins.Recaptcha.Provider.AntiCaptcha;
using Xunit;

namespace Extra.Tests
{
    public class ExtraLaunchTest: BrowserDefault
    {
        [Fact]
        public async void ShouldReturnOkPage()
        {
            var browser = await this.LaunchAsync();
            var page = await browser.NewPageAsync();
            var response = await page.GoToAsync("http://google.com");
            Assert.Equal(HttpStatusCode.OK, response.Status);
            await browser.CloseAsync();
        }
        
    }
}
