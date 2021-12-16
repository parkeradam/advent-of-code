import 'package:day_15/day_15.dart';
import 'package:test/test.dart';

final sampleIpuut = '''1163751742
1381373672
2136511328
3694931569
7463417111
1319128137
1359912421
3125421639
1293138521
2311944581'''
    .split('\n')
    .toList();

void main() {
  test('Given sample input, smallestPath should be 40', () {
    final result = calculatePath(sampleIpuut);
    expect(result, 40);
  });
}
