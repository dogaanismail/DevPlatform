using DevPlatform.Core;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using NUnit.Framework;

namespace DevPlatform.Tests.DevPlatform.Core.Tests
{
    [TestFixture]
    public class WebHelperTests : BaseDevPlatformTest
    {
        private HttpContext _httpContext;
        private IWebHelper _webHelper;

        [OneTimeSetUp]
        public void SetUp()
        {
            _webHelper = GetService<IWebHelper>();
            _httpContext = GetService<IHttpContextAccessor>().HttpContext;

            var queryString = new QueryString(string.Empty);
            queryString = queryString.Add("Key1", "Value1");
            queryString = queryString.Add("Key2", "Value2");
            _httpContext.Request.QueryString = queryString;
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            var queryString = new QueryString(string.Empty);
            _httpContext.Request.QueryString = queryString;
        }

        [Test]
        public void CanGetStoreHostWithoutSsl()
        {
            _webHelper.GetStoreHost(false).Should().Be($"http://{DevPlatformTestsDefaults.HostIpAddress}/");
        }

        [Test]
        public void CanGetStoreHostWithSsl()
        {
            _webHelper.GetStoreHost(true).Should().Be($"https://{DevPlatformTestsDefaults.HostIpAddress}/");
        }

        [Test]
        public void CanGetStoreLocationWithoutSsl()
        {
            _webHelper.GetStoreLocation(false).Should().Be($"http://{DevPlatformTestsDefaults.HostIpAddress}/");
        }

        [Test]
        public void CanGetStoreLocationWithSsl()
        {
            _webHelper.GetStoreLocation(true).Should().Be($"https://{DevPlatformTestsDefaults.HostIpAddress}/");
        }

        [Test]
        public void CanGetStoreLocationInVirtualDirectory()
        {
            _httpContext.Request.PathBase = "/devPlatformPath";
            _webHelper.GetStoreLocation(false).Should().Be($"http://{DevPlatformTestsDefaults.HostIpAddress}/devPlatformPath/");
            _httpContext.Request.PathBase = string.Empty;
        }

        [Test]
        public void CanGetQueryString()
        {
            _webHelper.QueryString<string>("Key1").Should().Be("Value1");
            _webHelper.QueryString<string>("Key2").Should().Be("Value2");
            _webHelper.QueryString<string>("Key3").Should().Be(null);
        }

        [Test]
        public void CanRemoveQueryString()
        {
            //empty URL
            _webHelper.RemoveQueryString(null, null).Should().Be(string.Empty);
            //empty key
            _webHelper.RemoveQueryString($"http://{DevPlatformTestsDefaults.HostIpAddress}/", null).Should().Be($"http://{DevPlatformTestsDefaults.HostIpAddress}/");
            //non-existing param with fragment
            _webHelper.RemoveQueryString($"http://{DevPlatformTestsDefaults.HostIpAddress}/#fragment", "param").Should().Be($"http://{DevPlatformTestsDefaults.HostIpAddress}/#fragment");
            //first param (?)
            _webHelper.RemoveQueryString($"http://{DevPlatformTestsDefaults.HostIpAddress}/?param1=value1&param2=value1", "param1")
                .Should().Be($"http://{DevPlatformTestsDefaults.HostIpAddress}/?param2=value1");
            //second param (&)
            _webHelper.RemoveQueryString($"http://{DevPlatformTestsDefaults.HostIpAddress}/?param1=value1&param2=value1", "param2")
                .Should().Be($"http://{DevPlatformTestsDefaults.HostIpAddress}/?param1=value1");
            //non-existing param
            _webHelper.RemoveQueryString($"http://{DevPlatformTestsDefaults.HostIpAddress}/?param1=value1&param2=value1", "param3")
                .Should().Be($"http://{DevPlatformTestsDefaults.HostIpAddress}/?param1=value1&param2=value1");
            //with fragment
            _webHelper.RemoveQueryString($"http://{DevPlatformTestsDefaults.HostIpAddress}/?param1=value1&param2=value1#fragment", "param1")
                .Should().Be($"http://{DevPlatformTestsDefaults.HostIpAddress}/?param2=value1#fragment");
            //specific value
            _webHelper.RemoveQueryString($"http://{DevPlatformTestsDefaults.HostIpAddress}/?param1=value1&param1=value2&param2=value1", "param1", "value1")
                .Should().Be($"http://{DevPlatformTestsDefaults.HostIpAddress}/?param1=value2&param2=value1");
            //all values
            _webHelper.RemoveQueryString($"http://{DevPlatformTestsDefaults.HostIpAddress}/?param1=value1&param1=value2&param2=value1", "param1")
                .Should().Be($"http://{DevPlatformTestsDefaults.HostIpAddress}/?param2=value1");
        }

        [Test]
        public void CanModifyQueryString()
        {
            //empty URL
            _webHelper.ModifyQueryString(null, null).Should().Be(string.Empty);
            //empty key
            _webHelper.ModifyQueryString($"http://{DevPlatformTestsDefaults.HostIpAddress}/", null).Should().Be($"http://{DevPlatformTestsDefaults.HostIpAddress}/");
            //empty value
            _webHelper.ModifyQueryString($"http://{DevPlatformTestsDefaults.HostIpAddress}/", "param").Should().Be($"http://{DevPlatformTestsDefaults.HostIpAddress}/?param=");
            //first param (?)
            _webHelper.ModifyQueryString($"http://{DevPlatformTestsDefaults.HostIpAddress}/?param1=value1&param2=value1", "Param1", "value2")
                .Should().Be($"http://{DevPlatformTestsDefaults.HostIpAddress}/?param1=value2&param2=value1");
            //second param (&)
            _webHelper.ModifyQueryString($"http://{DevPlatformTestsDefaults.HostIpAddress}/?param1=value1&param2=value1", "param2", "value2")
                .Should().Be($"http://{DevPlatformTestsDefaults.HostIpAddress}/?param1=value1&param2=value2");
            //non-existing param
            _webHelper.ModifyQueryString($"http://{DevPlatformTestsDefaults.HostIpAddress}/?param1=value1&param2=value1", "param3", "value1")
                .Should().Be($"http://{DevPlatformTestsDefaults.HostIpAddress}/?param1=value1&param2=value1&param3=value1");
            //multiple values
            _webHelper.ModifyQueryString($"http://{DevPlatformTestsDefaults.HostIpAddress}/?param1=value1&param2=value1", "param1", "value1", "value2", "value3")
                .Should().Be($"http://{DevPlatformTestsDefaults.HostIpAddress}/?param1=value1,value2,value3&param2=value1");
            //with fragment
            _webHelper.ModifyQueryString($"http://{DevPlatformTestsDefaults.HostIpAddress}/?param1=value1&param2=value1#fragment", "param1", "value2")
                .Should().Be($"http://{DevPlatformTestsDefaults.HostIpAddress}/?param1=value2&param2=value1#fragment");
        }

        [Test]
        public void CanModifyQueryStringInVirtualDirectory()
        {
            _httpContext.Request.PathBase = "/devPlatformPath";
            _webHelper.ModifyQueryString("/devPlatformPath/Controller/Action", "param1", "value1").Should().Be("/devPlatformPath/Controller/Action?param1=value1");
            _httpContext.Request.PathBase = string.Empty;
        }
    }
}
