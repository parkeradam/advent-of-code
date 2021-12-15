import 'package:day_14/extensions.dart';
import 'dart:math';

Map<String, String> parsePairings(Iterable<String> input) {
  var elements = input.map((line) => line.split(' -> '));
  return {for (var line in elements) line[0]: line[1]};
}

class Day14 {
  String _initialString = "";
  Map<String, String> _pairings = {};
  int expandAndCount(List<String> input, int numExpansions) {
    _initialString = input[0];
    _pairings = parsePairings(input.skip(2));
    final output = _loop(numExpansions, _initialString);
    var lst = output.split('');
    final distinctChars = lst.toSet().toList();

    final counts = {
      for (var x in distinctChars) x: lst.countWhere((y) => y == x)
    };

    final minChar = counts.values.reduce(min);
    final maxChar = counts.values.reduce(max);

    return maxChar - minChar;
  }

  String _loop(int numExpansions, String str) {
    var fullStr = str;
    for (int i = 0; i < numExpansions; i++) {
      print("Iteration $i");
      final newStr = _innerLoop(fullStr);
      fullStr = newStr;
    }
    return fullStr;
  }

  String _innerLoop(String str) {
    var remainingString = str.split('').join();
    var currentFullString = "";

    while (remainingString.length >= 2) {
      final pair = remainingString.substring(0, 2);
      final toAdd = _pairings[pair];
      remainingString = remainingString.substring(1);
      currentFullString += pair[0].toString() + toAdd!;
    }
    return currentFullString + remainingString;
  }
}

List<String> parsePairingsPt2(Iterable<String> input) {
  var elements = input.map((line) => line.split(' -> '));
  var map = {for (var line in elements) line[0]: line[1]};
  var list = <String>[];
  map.forEach((key, value) =>
      list.addAll([(key[0].toString() + value), value + key[1].toString()]));

  return list;
}

class Day14Pt2 {
  static execute() { }
}
