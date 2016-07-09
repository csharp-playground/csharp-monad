using System;
using Monad;
using Monad.Utility;
using FluentAssertions;

namespace TryMonad {
	public class StateTests {
		public StateTests() {
		}


		public void StateM() {
			var first = State.Return<string, int>(10);
			var second = State.Return<string, int>(3);
			var third = State.Return<string, int>(5);
			var fourth = State.Return<string, int>(100);

			var sm =
				from x in first
				from t in State.Get<string>(s => s + "yyy")
				from y in second
				from s in State.Put("Hello " + (x * y) + t)
				from z in third
				from w in fourth
				from s1 in State.Get<string>()
				from s2 in State.Put(s1 + " " + (z * w))
				select x * y * z * w;

			var rs = sm(", World");
			rs.State.Should().Be("Hello 30, Worldyyy 500");
			rs.Value.Should().Be(15000);

		}
	}
}

