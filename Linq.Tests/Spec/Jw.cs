using System;

public delegate JwResult<T> Jw<T>();

public struct JwResult<T> {
	public readonly string Exception;
	public readonly T Value;

	public JwResult(T value) {
		Value = value;
		Exception = null;
	}
	public JwResult(string err) {
		Value = default(T);
		Exception = err;
	}

	public bool IsFaulted {
		get {
			return Exception != null;
		}
	}

	public static implicit operator JwResult<T>(T value) {
		return new JwResult<T>(value);
	}
}

public static class JwExt {
	public static Jw<V> SelectMany<T, U, V>(
			this Jw<T> self,
			Func<T, Jw<U>> select,
			Func<T, U, V> bind) {

		if (select == null) throw new ArgumentNullException("select");
		if (bind == null) throw new ArgumentNullException("bind");

		return new Jw<V>(
			() => {
				// ------
				JwResult<T> resT;
				try {
					resT = self();
					if (resT.IsFaulted)
						return new JwResult<V>(resT.Exception);
				} catch (Exception e) {
					return new JwResult<V>(e.Message);
				}

				// ------
				JwResult<U> resU;
				try {
					resU = select(resT.Value)();
					if (resU.IsFaulted)
						return new JwResult<V>(resU.Exception);
				} catch (Exception e) {
					return new JwResult<V>(e.Message);
				}

				// ------
				V resV;
				try {
					resV = bind(resT.Value, resU.Value);
				} catch (Exception e) {
					return new JwResult<V>(e.Message);
				}

				return new JwResult<V>(resV);
			}
		);
	}
}