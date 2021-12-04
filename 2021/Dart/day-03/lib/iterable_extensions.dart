typedef GenericToBool<T> = bool Function(T value);

extension IterableExtension<T> on Iterable<T> {
  int countWhere(GenericToBool func) => where(func).length;
}
