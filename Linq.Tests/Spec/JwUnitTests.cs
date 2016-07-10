using System;
using NUnit.Framework;
using FluentAssertions;
using System.Linq;
using System.Diagnostics;

namespace Linq.Tests {
	public class JwUnitTests {

		public Jw<int> A() {
			return () => 1;
		}
		public Jw<int> B() {
			return () => 2;
		}

		public Jw<int> C() {
			return () => {
				throw new Exception("Error");
			};
		}

		[Test]
		public void Add() {
			var query =
				from a in A()
				from b in B()
				select a + b;

			var rs = query();
			rs.Value.Should().Be(3);
		}

		public void AddFail() {
			var query =
				from a in A()
				from c in C()
				select a + c;

			var rs = query();
			rs.Exception.Should().Be("Error");
		}
	}
}

