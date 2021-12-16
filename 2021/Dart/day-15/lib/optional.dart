class Optional<A> {
  A? value;
  bool isNone;

  Optional(this.value, this.isNone);

  Optional.some(A innerVal)
      : value = innerVal,
        isNone = false;
  Optional.none()
      : value = null,
        isNone = true;
}
