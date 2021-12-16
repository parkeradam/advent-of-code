class PathNode {
  int posX;
  int posY;
  int risk;
  int distance;

  PathNode(this.risk, this.distance, this.posX, this.posY);

  @override
  String toString() =>
      "{x: $posX ; y: $posY ; risk: $risk; distance : $distance}";
}
