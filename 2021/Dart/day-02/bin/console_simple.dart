import 'dart:io';

class Position {
  int depth;
  int horizontal;
  int aim;

  Position(this.depth, this.horizontal, this.aim);

  int multiply() => depth * horizontal;
}

List<String> readInput(String filePath) {
  return File(filePath).readAsLinesSync();
}

Position getPosition(List<String> sampleInput) {
  var position = Position(0, 0, 0);

  for (var input in sampleInput) {
    var split = input.split(" ");
    var direction = split[0];
    var amount = int.parse(split[1]);

    if (direction == "forward") {
      position.horizontal += amount;
    } else if (direction == "down") {
      position.depth += amount;
    } else if (direction == "up") {
      position.depth -= amount;
    }
  }

  return position;
}

Position getPositionTwo(List<String> sampleInput) {
  var position = Position(0, 0, 0);

  for (var input in sampleInput) {
    var split = input.split(" ");
    var direction = split[0];
    var amount = int.parse(split[1]);

    if (direction == "forward") {
      position.horizontal += amount;
      position.depth += (amount * position.aim);
    } else if (direction == "down") {
      position.aim += amount;
    } else if (direction == "up") {
      position.aim -= amount;
    }
  }

  return position;
}

void main(List<String> arguments) {
  //Test
  var sampleInput = [
    "forward 5",
    "down 5",
    "forward 8",
    "up 3",
    "down 8",
    "forward 2"
  ];

  var sampleResult = getPosition(sampleInput);
  print("Test 1 : result from sample is as expected with no aim $sampleResult");

  //Part 1
  var values = readInput("./input.txt");
  var result = getPosition(values);
  print("Day 2 - Part 1 Answer is ${result.multiply()}");
  //Part 2
  var resultTwo = getPositionTwo(values);
  print("Day 2 - Part 2 Answer is ${resultTwo.multiply()}");
}
