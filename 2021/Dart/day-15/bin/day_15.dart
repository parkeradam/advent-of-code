import 'dart:io';

import 'package:day_15/day_15.dart' as day_15;

void main(List<String> arguments) {
  final input = File("./input.txt").readAsLinesSync();
  print("runningPart1");
  final result = day_15.calculatePath(input);
  print("Day 15 - Part 1 = $result");
}
