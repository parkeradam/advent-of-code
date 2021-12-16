import 'package:collection_ext/iterables.dart';
import 'package:day_15/optional.dart';

import 'package:day_15/path_node.dart';

const int intMaxValue = 9223372036854775807;

Optional<PathNode> getOptional(List<List<PathNode>> graph, int posX, int posY) {
  final xInRange = !(posX < 0 || posX >= graph.length);
  final yInRange = !(posY < 0 || posY >= graph[0].length);

  if (xInRange && yInRange) {
    return Optional.some(graph[posX][posY]);
  } else {
    return Optional.none();
  }
}

Iterable<Iterable<int>> parseInput(List<String> input) =>
    input.map((e) => e.trim().split('').map((int.parse)));

int djikstra(List<List<PathNode>> graph) {
  final maxX = graph.length - 1;
  final maxY = graph[0].length - 1;

  final visited = <PathNode>[];
  graph[0][0].distance = 0;
  while (visited.length != graph.expand((element) => element).length) {
    //get current
    final current = graph
        .expand((element) => element)
        .where((element) => !visited.contains(element))
        .minBy((index, p1) => p1.distance)!;

    print("Checking $current");
    //break if current is end
    if (current.posX == maxX && current.posY == maxY) {
      break;
    }

    //get valid neighbours
    final left = getOptional(graph, current.posX - 1, current.posY);
    final right = getOptional(graph, current.posX + 1, current.posY);
    final up = getOptional(graph, current.posX, current.posY - 1);
    final down = getOptional(graph, current.posX, current.posY + 1);

    var validNeighbours = [left, right, up, down]
        .map((e) {
          if (e.isNone) {
            return null;
          } else {
            return e.value;
          }
        })
        .where((element) => element != null)
        .where((element) => !(visited.contains(element)));
    //updateNeigbours
    validNeighbours.forEach((neighbour) {
      final dist = neighbour!.risk + current.distance;
      if (neighbour.distance > dist) {
        neighbour.distance = dist;
      }
    });

    visited.add(current);
  }

  var end = graph[maxX][maxY];
  return end.distance;
}

int calculatePath(List<String> input) {
  var parsedInput = parseInput(input);
  var graph = parsedInput
      .mapIndexed((x, e) =>
          e.mapIndexed((y, e) => PathNode(e, intMaxValue, x, y)).toList())
      .toList();

  return djikstra(graph);
}

List<int> addAndCycle(int toAdd, List<int> values) => values.map((x) {
      final newNum = x + toAdd;
      if (newNum > 9) {
        return newNum % 9;
      } else {
        return newNum;
      }
    }).toList();
