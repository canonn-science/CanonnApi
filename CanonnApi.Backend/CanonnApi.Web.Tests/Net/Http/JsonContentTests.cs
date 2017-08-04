using System;
using CanonnApi.Web.Net.Http;
using FluentAssertions;
using Xunit;

namespace CanonnApi.Web.Tests.Net.Http
{
	public class JsonContentTests
	{
		[Fact]
		public void NullObjectArgumentShouldCreateEmptyContent()
		{
			var sub = new JsonContent((object)null);

			sub.Should().NotBeNull();
			sub.Headers.ContentEncoding.Should().BeEmpty("UTF 8 is default");
			sub.Headers.ContentType.MediaType.Should().Be("application/json");
			var content = sub.ReadAsStringAsync().Result;
			content.Should().NotBeNullOrWhiteSpace();
			content.Should().Be("null", "We passed null in");
		}

		[Fact]
		public void NullStringArgumentShouldThrowArgumentNullException()
		{
			Action action = () =>
			{
				var sub = new JsonContent((string) null);
			};

			action.ShouldThrow<ArgumentNullException>();
		}

		[Fact]
		public void EmptyStringArgumentShouldCreateEmptyContent()
		{
			var sub = new JsonContent(String.Empty);

			sub.Should().NotBeNull();
			sub.Headers.ContentEncoding.Should().BeEmpty("UTF 8 is default");
			sub.Headers.ContentType.MediaType.Should().Be("application/json");
			var content = sub.ReadAsStringAsync().Result;
			content.Should().NotBeNull();
			content.Should().Be(String.Empty, "We passed empty string in");
		}
	}
}
