using System;
using Monad;
using FluentAssertions;

namespace TryMonad {
	public class TryTests {
		public TryTests() {
		}

		private Try<int> DoSomething(int value) {
			return () => value + 1;
		}
		private Try<int> DoSomethingError(int value) {
			return () => { throw new Exception("Whoops"); };
		}

		private Try<int> DoNotEverEnterThisFunction(int value) {
			return () => 1000;
		}

		public void Try() {
			var monad =
				from a in DoSomething(10)
				from b in DoSomethingError(10)
				from c in DoNotEverEnterThisFunction(10)
				select c;

			var rs = monad();
			rs.IsFaulted.Should().Be(true);
			rs.Exception.Message.Should().Be("Whoops");

		}
	}
}

