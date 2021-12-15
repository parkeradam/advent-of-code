import 'package:day_14/day_14.dart';
import 'package:test/scaffolding.dart';
import 'package:test/test.dart';

void main() {
  final sampleInput = """NNCB

CH -> B
HH -> N
CB -> H
NH -> C
HB -> C
HC -> B
HN -> C
NN -> C
BH -> H
NC -> B
NB -> B
BN -> B
BB -> N
BC -> B
CC -> N
CN -> C"""
      .split('\n');

  test('Given sample input, expanding 10 times, answer should be 1588', () {
    var result = Day14().expandAndCount(sampleInput, 10);
    expect(result, 1588);
  });

  test('Given sample input, expanding 40 times, answer should be 2188189693529',
      () {
    var result = Day14().expandAndCount(sampleInput, 40);
    expect(result, 2188189693529);
  });
}
