import 'dart:io';

import 'package:day_14/day_14.dart';

List<String> readInput(String filePath) {
  return File(filePath).readAsLinesSync();
}

void main(List<String> arguments) {
  var values = readInput("./input.txt").toList();

  var result = Day14().expandAndCount(values, 10);
  print("Day 14 - Part 1 : Result = $result");

  var result2 = Day14().expandAndCount(values, 40);
  print("Day 14 - Part 2 : Result = $result2");
}
