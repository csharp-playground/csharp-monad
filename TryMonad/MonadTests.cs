using System;
using FluentAssertions;
namespace TryMonad {
	public class MonadTests {
		public void Hello() {
			(1).Should().Be(1);
		}
	}
}

