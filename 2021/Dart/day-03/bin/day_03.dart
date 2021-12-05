import 'dart:io';

import 'package:day_03/gamma_epsilon_pair.dart';
import 'package:day_03/iterable_extensions.dart';
import 'package:day_03/most_least_pair.dart';
import 'package:day_03/oxygen_co2_pair.dart';
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

String filterByMost(Iterable<String> remaining, int index) {
  if (remaining.length == 1 || index >= remaining.first.length) {
    return remaining.first;
  }
  var numbers = remaining.map((e) => e[index]);
  var zeroCount = numbers.countWhere((element) => element == '0');
  var oneCount = numbers.countWhere((element) => element == '1');
  if (zeroCount <= oneCount) {
    return filterByMost(
        remaining.where((element) => element[index] == '1'), index + 1);
  } else {
    return filterByMost(
        remaining.where((element) => element[index] == '0'), index + 1);
  }
}

String filterByLeast(Iterable<String> remaining, int index) {
  if (remaining.length == 1 || index >= remaining.first.length) {
    return remaining.first;
  }
  var numbers = remaining.map((e) => e[index]);
  var zeroCount = numbers.countWhere((element) => element == '0');
  var oneCount = numbers.countWhere((element) => element == '1');
  if (zeroCount <= oneCount) {
    return filterByLeast(
        remaining.where((element) => element[index] == '0'), index + 1);
  } else {
    return filterByLeast(
        remaining.where((element) => element[index] == '1'), index + 1);
  }
}

OxygenCo2Pair getOxygenAndCo2Pair(List<String> input) {
  var oxygen = filterByMost(List.from(input), 0);
  var co2 = filterByLeast(List.from(input), 0);
  return OxygenCo2Pair(Binary(oxygen).value, Binary(co2).value);
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

  var sampleResult2 = getOxygenAndCo2Pair(sampleInput);

  print(
      "Test 2: given sample input, the output should be 230: ${sampleResult2.oxygen * sampleResult2.co2}");

  var values = readInput("./input.txt");
  var result1 = getGammaAndEpsilonRate(values, 12);
  print(
      "Day 3 - Part 1 Answer is ${result1.gammaNumber * result1.epsilonNumber}");

  var result2 = getOxygenAndCo2Pair(values);
  print("Day 3 - Part 2 Answer is ${result2.oxygen * result2.co2}");
}
