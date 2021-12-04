import 'dart:io';

import 'package:day_03/gamma_epsilon_pair.dart';
import 'package:day_03/iterable_extensions.dart';
import 'package:day_03/most_least_pair.dart';
import 'package:quantity/number.dart';

GammaEpsilonPair getGammaAndEpsilonRate(List<String> input, int numDigits) {
  var results = List.generate(numDigits, (index) => index).map((index) {
    var numbers = input.map((e) => e[index]);
    var zeroCount = numbers.countWhere((element) => element == '0');
    var oneCount = numbers.countWhere((element) => element == '1');
    if (zeroCount < oneCount) {
      return MostLeastPair('1', '0');
    } else {
      return MostLeastPair('0', '1');
    }
  });

  var gamma = Binary(results.map((e) => e.most).join()).value;
  var epsilon = Binary(results.map((e) => e.least).join()).value;

  return GammaEpsilonPair(epsilon, gamma);
}

List<String> readInput(String filePath) {
  return File(filePath).readAsLinesSync();
}

void main(List<String> arguments) {
  var sampleInput = [
    "00100",
    "11110",
    "10110",
    "10111",
    "10101",
    "01111",
    "00111",
    "11100",
    "10000",
    "11001",
    "00010",
    "01010"
  ];

  var sampleResult1 = getGammaAndEpsilonRate(sampleInput, 5);
  print(
      "Test 1: given sample input, the output should be 198: ${sampleResult1.gammaNumber * sampleResult1.epsilonNumber}");

  var values = readInput("./input.txt");
  var result1 = getGammaAndEpsilonRate(values, 12);
  print(result1);
  print(
      "Day 3 - Part 1 Answer is ${result1.gammaNumber * result1.epsilonNumber}");
}
