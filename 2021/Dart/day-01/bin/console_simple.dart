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
      "Sample result is ${measurementsIncreasesCount(sampleInput)} which should equal 7");

  print(
      "Window values are ${windowedSums(sampleInput, 3)} which should be [607,618,618,617,647,716,769,792]");

  var values = readInput("./input.txt").map(int.parse).toList();
  //part 1
  var result = measurementsIncreasesCount(values);
  print(result);

  //part 2
  var windowedVals = windowedSums(values, 3);
  var result2 = measurementsIncreasesCount(windowedVals);
  print(result2);
}
