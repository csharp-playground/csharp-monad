﻿
////////////////////////////////////////////////////////////////////////////////////////
// The MIT License (MIT)
// 
// Copyright (c) 2014 Paul Louth
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
// 

using System;

namespace JustTryMonad.Utility {
	/// <summary>
	/// A type with only one value, itself.
	/// This is the functional world's equivalent of Void.
	/// </summary>
	public struct Unit {
		/// <summary>
		/// Use a singleton so that ref equality works and to reduce any unnecessary
		/// thrashing of the GC from creating an object which is always the same.
		/// </summary>
		public static readonly Unit Default = new Unit();

		/// <summary>
		/// Performs an action which instead of returning void will return Unit
		/// </summary>
		/// <param name="action"></param>
		/// <returns></returns>
		public static Unit Return(Action action) {
			if (action == null) throw new ArgumentNullException("action");

			action();
			return Default;
		}
	}
}

