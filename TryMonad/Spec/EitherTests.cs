using System;
using FluentAssertions;
using Monad;

namespace TryMonad {
	public class EitherTests {
		public void Hello() {
			(1).Should().Be(1);
		}

		private Either<string, int> Two() {
			return () => 2;
		}

		private Either<string, int> Error() {
			return () => "Error";
		}

		public void AddOk() {
			var r =
				from a in Two()
				from b in Two()
				select a + b;

			var option = r();
			option.IsRight.Should().Be(true);
			option.Right.Should().Be(4);
		}

		public void AddError() {
			var r =
				from a in Two()
				from b in Error()
				from c in Two()
				select a + b + c;
			var option = r();
			option.IsLeft.Should().Be(true);
			option.Right.Should().Be(0);
		}

		public void DelegateWithName() {
			var rs =
				from a in Two()
				from b in Two()
				select a + b;

			rs.Match(
				Right: r => r.Should().Be(4),
				Left: l => false.Should().Be(true));
		}

		public void DelegateWithoutName() {
			var rs =
				from a in Two()
				from b in Two()
				select a + b;
			rs.Match(
				r => r.Should().Be(4),
				l => false.Should().Be(true));
		}

		public void ProjectionWithoutName() {
			var rs =
				from a in Two()
				from b in Two()
				select a + b;

			var update =
				rs.Match(
					r => r * 2,
					l => 0
				);

			update().Should().Be(8);
		}
	}
}

