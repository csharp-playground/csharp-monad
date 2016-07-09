using System;
using Monad;
using Monad.Utility;
using System.IO;
using FluentAssertions;

namespace TryMonad {
	public class IOTests {
		public IOTests() {
		}
		private IO<Unit> DeleteFile(string name) {
			return () => Unit.Return(() => File.Delete(name));
		}

		private IO<string> ReadFile(string name) {
			return () => File.ReadAllText(name);
		}

		private IO<Unit> WriteFile(string file, string data) {
			return () => Unit.Return(() => File.WriteAllText(file, data));
		}

		private static IO<string> GetTempFile() {
			return () => "TempFile.txt";
		}

		public void ReadWriteFile() {
			var result =
				from tempFile in GetTempFile()
				from _ in WriteFile(tempFile, "Hello")
				from dataFromFile in ReadFile(tempFile)
				from __ in DeleteFile(tempFile)
				select dataFromFile;

			result().Should().Be("Hello");
		}
	}
}

