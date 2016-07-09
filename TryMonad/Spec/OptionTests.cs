using System;
using Monad;
using Monad.Utility;
using FluentAssertions;

namespace TryMonad {
	public class OptionTests {
		public OptionTests() {
		}

		private Option<int> GetInt(bool ok) {
			if (ok)
				return () => (100).ToOption();
			else
				return Option.Nothing<int>();
		}

		public void TestInt() {
			var result =
				from a in GetInt(true)
				from b in GetInt(false)
				select a;

			result().HasValue.Should().Be(false);
		}

		public void Match() {
			var result =
				from a in GetInt(true)
				from b in GetInt(true)
				select a;

			result.Match(
				ok => true.Should().Be(true),
				() => true.Should().Be(false)
			)();
		}
	}
}

