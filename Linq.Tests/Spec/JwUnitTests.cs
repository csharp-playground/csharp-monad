using System;
using NUnit.Framework;
using FluentAssertions;

namespace Linq.Tests {
	public class JwUnitTests {

		public Jw<int> A() {
			return () => 1;
		}
		public Jw<int> B() {
			return () => 2;
		}

		[Test]
		public void Add() {
			var rs =
				from a in A()
				from b in B()
				select a + b;

			rs().Value.Should().Be(3);
		}
	}
}

