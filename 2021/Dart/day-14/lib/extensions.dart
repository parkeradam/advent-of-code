typedef FilterFunc<T> = bool Function(T input);

extension ListExt on List {
  int countWhere(FilterFunc func) => where(func).length;
}
