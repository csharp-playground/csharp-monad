﻿using System;
using JustTryMonad;
using FluentAssertions;

public class LinqTests {
	public LinqTests() {
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
			from b in DoSomething(10)
			select a + b;

		var rs = monad();
		rs.Value.Should().Be(22);
	}

	public void TryFail() {
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

