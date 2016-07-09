using System;
using Monad;
using FluentAssertions;
using System.Linq;

namespace TryMonad {
	public class WriterTests {
		public WriterTests() {
		}

		private Writer<string, int> LogNumber(int num) {
			return () => Writer.Tell(num, "Got number: " + num);
		}

		public void Biding2() {
			var rs =
				from a in LogNumber(3)
				from b in LogNumber(5)
				from _ in Writer.Tell("Gonna multiply these two")
				select a * b;

			var memo = rs.Memo();

			memo().Value.Should().Be(15);
			memo().Output.First().Should().Be("Got number: 3");
			memo().Output.Skip(1).First().Should().Be("Got number: 5");
			memo().Output.Skip(2).First().Should().Be("Gonna multiply these two");
		}

	}
}

