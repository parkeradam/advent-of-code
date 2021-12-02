import 'dart:io';

List<String> readInput(String filePath) {
  return File(filePath).readAsLinesSync();
}

int measurementsIncreasesCount(List<int> values) {
  int count = 0;

  values.asMap().forEach((key, value) {
    if (key < values.length - 1) {
      if (value < values[key + 1]) {
        count++;
      }
    }
  });

  return count;
}

List<int> windowedSums(List<int> values, int window) {
  var newList = <int>[];
  var index = 0;
  var windowVals = values.take(window);
  while (windowVals.length == window) {
    newList.add(windowVals.reduce((a, b) => a + b));
    index += 1;
    windowVals = values.skip(index).take(window);
  }
  return newList;
}

void main(List<String> arguments) {
  //Tests
  var sampleInput = [199, 200, 208, 210, 200, 207, 240, 269, 260, 263];

  print(
      "Test 1 : given sample input, result = 7 ${measurementsIncreasesCount(sampleInput) == 7}");

  print(
      "Test 2 : Sliding scale based on example ${windowedSums(sampleInput, 3)}");

  var values = readInput("./input.txt").map(int.parse).toList();
  //part 1
  var result = measurementsIncreasesCount(values);
  print("Day 1 - Part 1 Answer is $result");

  //part 2
  var windowedVals = windowedSums(values, 3);
  var result2 = measurementsIncreasesCount(windowedVals);
  print("Day 1 - Part 2 Answer is $result2");
}
