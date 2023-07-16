using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Solutions.Days
{
    public class DayNine
    {
        public void Run()
        {
            var input = new string[]
            {
                // ".#..#",
                // ".....",
                // "#####",
                // "....#",
                // "...##"
                //
                // "......#.#.",
                // "#..#.#....",
                // "..#######.",
                // ".#.#.###..",
                // ".#..#.....",
                // "..#....#.#",
                // "#..#....#.",
                // ".##.#..###",
                // "##...#..#.",
                // ".#....####"

                // "#.#...#.#.",
                // ".###....#.",
                // ".#....#...",
                // "##.#.#.#.#",
                // "....#.#.#.",
                // ".##..###.#",
                // "..#...##..",
                // "..##....##",
                // "......#...",
                // ".####.###."

                // ".#..#..###",
                // "####.###.#",
                // "....###.#.",
                // "..###.##.#",
                // "##.##.#.#.",
                // "....###..#",
                // "..#.#..#.#",
                // "#..#.#.###",
                // ".##...##.#",
                // ".....#.#.."

                // ".#..##.###...#######",
                // "##.############..##.",
                // ".#.######.########.#",
                // ".###.#######.####.#.",
                // "#####.##.#.##.###.##",
                // "..#####..#.#########",
                // "####################",
                // "#.####....###.#.#.##",
                // "##.#################",
                // "#####.##.###..####..",
                // "..######..##.#######",
                // "####.##.####...##..#",
                // ".#####..#.######.###",
                // "##...#.##########...",
                // "#.##########.#######",
                // ".####.#.###.###.#.##",
                // "....##.##.###..#####",
                // ".#.#.###########.###",
                // "#.#.#.#####.####.###",
                // "###.##.####.##.#..##"

                "#...##.####.#.......#.##..##.#.",
                "#.##.#..#..#...##..##.##.#.....",
                "#..#####.#......#..#....#.###.#",
                "...#.#.#...#..#.....#..#..#.#..",
                ".#.....##..#...#..#.#...##.....",
                "##.....#..........##..#......##",
                ".##..##.#.#....##..##.......#..",
                "#.##.##....###..#...##...##....",
                "##.#.#............##..#...##..#",
                "###..##.###.....#.##...####....",
                "...##..#...##...##..#.#..#...#.",
                "..#.#.##.#.#.#####.#....####.#.",
                "#......###.##....#...#...#...##",
                ".....#...#.#.#.#....#...#......",
                "#..#.#.#..#....#..#...#..#..##.",
                "#.....#..##.....#...###..#..#.#",
                ".....####.#..#...##..#..#..#..#",
                "..#.....#.#........#.#.##..####",
                ".#.....##..#.##.....#...###....",
                "###.###....#..#..#.....#####...",
                "#..##.##..##.#.#....#.#......#.",
                ".#....#.##..#.#.#.......##.....",
                "##.##...#...#....###.#....#....",
                ".....#.######.#.#..#..#.#.....#",
                ".#..#.##.#....#.##..#.#...##..#",
                ".##.###..#..#..#.###...#####.#.",
                "#...#...........#.....#.......#",
                "#....##.#.#..##...#..####...#..",
                "#.####......#####.....#.##..#..",
                ".#...#....#...##..##.#.#......#",
                "#..###.....##.#.......#.##...##"
            };

            // ProblemOne(input);
            ProblemTwo(input);
        }

        public void ProblemOne(string[] input)
        {
            var map = ParseInput(input);
            
            var asteroidLocations = GetAsteroidLocations(map);
            var max = asteroidLocations
                .Max(tuple => GetAsteroidScore(tuple, map, asteroidLocations));
            
            Log(max);
        }

        public void ProblemTwo(string[] input)
        {
            var map = ParseInput(input);
            
            var asteroidLocations = GetAsteroidLocations(map);
            Log(asteroidLocations.Count);
        }

        private List<List<char>> ParseInput(string[] input)
        {
            return input.Select(s => s.ToCharArray().ToList())
                .ToList();
        }

        private List<(int k, int j)> GetAsteroidLocations(List<List<char>> map)
        {
            var rows = map.Count;
            var columns = map[0].Count;

            return Enumerable.Range(0, rows)
                .SelectMany(y => Enumerable.Range(0, columns).Select(x => (x, y)))
                .Where(tuple => map[tuple.y][tuple.x] == '#')
                .ToList();
        }

        private int GetAsteroidScore((int x, int y) currentAsteroid, List<List<char>> map,
            List<(int x, int y)> asteroidLocations)
        {
            return asteroidLocations.Count(tuple => IsOpenPath(currentAsteroid, tuple, asteroidLocations));
        }

        private bool IsOpenPath((int x, int y) currentAsteroid, (int x, int y) destinationAsteroid,
            List<(int x, int y)> asteroidLocations)
        {
            var gradient = (destinationAsteroid.y - currentAsteroid.y) /
                           (double) (destinationAsteroid.x - currentAsteroid.x);

            var c = currentAsteroid.y - (gradient * currentAsteroid.x);

            var valueTuples = Enumerable.Range(Math.Min(currentAsteroid.x, destinationAsteroid.x),
                    Math.Abs(currentAsteroid.x - destinationAsteroid.x))
                .Select(j => (j, (gradient * j) + c))
                .Where(tuple => tuple.Item2 == (int) tuple.Item2)
                .Select(tuple => (tuple.j, (int) tuple.Item2))
                .Except(new[] {destinationAsteroid});

            var any = asteroidLocations.Intersect(valueTuples).Any();

            // Log("==========================================");
            // Log(currentAsteroid);
            // Log(destinationAsteroid);
            // Log(Math.Min(currentAsteroid.x, destinationAsteroid.x));
            // Log(Math.Abs(currentAsteroid.x - destinationAsteroid.x));
            // Log($"{gradient} {c}");
            // Log(string.Join(", ", valueTuples));
            // Log(any);
            // Log("==========================================");

            return !any;
        }

        private void Log<T>(T t)
        {
            Console.WriteLine(t);
        }
    }
}