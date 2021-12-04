class GammaEpsilonPair {
  int epsilonNumber;
  int gammaNumber;

  GammaEpsilonPair(this.epsilonNumber, this.gammaNumber);

  @override
  String toString() {
    return "Epsilon = $epsilonNumber : Gamma = $gammaNumber";
  }
}
