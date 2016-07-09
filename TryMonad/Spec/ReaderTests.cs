using System;
using Monad;
using FluentAssertions;

namespace TryMonad {

	public class Person {
		public string Name;
		public string Surename;
	}

	public class ReaderTests {
		public ReaderTests() {
		}

		private static Reader<Person, string> Name() {
			return env => env.Name;
		}

		private static Reader<Person, string> Surename() {
			return env => env.Surename;
		}

		public void GetName() {
			var person = new Person { Name = "wk", Surename = "kw" };
			var reader =
				from n in Name()
				from s in Surename()
				select n + " " + s;
			var rs = reader(person);
			rs.Should().Be("wk kw");
		}

		public void GenLength() {
			var person = new Person { Name = "wk", Surename = "kw" };
			var initial = Reader.Return<Person, int> (10);
			var reader =
				from x in initial
				from p in Reader.Ask<Person>()
				let nl = p.Name.Length
				let sl = p.Surename.Length
				select nl + sl + x;

			reader(person).Should().Be(14);
		}
	}
}

