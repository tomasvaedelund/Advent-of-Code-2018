using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Y2018.Day24
{
    class Solution : Solver
    {
        public string GetName() => "Day24";

        public void Test(string input) => new Test().Run(input);

        public IEnumerable<(string result, long time)> Solve(string input)
        {
            yield return PartOne(input);
            yield return PartTwo(input);
        }

        public (string result, long time) PartOne(string input)
        {
            var timer = Stopwatch.StartNew();
            return ($"result", timer.ElapsedMilliseconds);
        }

        public (string result, long time) PartTwo(string input)
        {
            var timer = Stopwatch.StartNew();
            return ($"result", timer.ElapsedMilliseconds);
        }

        public IEnumerable<(Unit attacker, Unit defender)> TargetSelectionPhase(IEnumerable<Unit> units)
        {
            units = units.OrderByDescending(u => u.EffectivePower).ThenBy(u => u.Initiative);

            foreach (var unit in units)
            {
                var targets = units
                    .Where(u => u != unit)
                    .Where(u => u.UnitType != unit.UnitType)
                    .Where(u => !u.Immunities.Contains(unit.AttackType));

                if (!targets.Any())
                {
                    continue;
                }

                var target = targets
                    .OrderBy(t => EffectiveDamage(unit, t))
                    .ThenBy(t => t.Initiative)
                    .First();

                yield return (attacker: unit, defender: target);
            }
        }

        private int EffectiveDamage(Unit unit, Unit t)
        {
            return (t.Weaknesses.Contains(unit.AttackType)) ? unit.EffectivePower * 2 : unit.EffectivePower;
        }

        public IEnumerable<Unit> ParseInput(string input)
        {
            var array = input.Split("\r\n");
            var unitType = '\0';

            foreach (var row in array)
            {
                if (string.IsNullOrEmpty(row))
                {
                    continue;
                }

                if (row.Equals("Immune System:"))
                {
                    unitType = 's';
                    continue;
                }

                if (row.Equals("Infection:"))
                {
                    unitType = 'i';
                    continue;
                }

                var numbersPattern = new Regex(@"\d+");
                var numbers = numbersPattern.Matches(row).Select(n => int.Parse(n.Value)).ToArray();

                var attackPattern = new Regex(@"(?<=(does )).+(?= damage)");
                var attack = attackPattern.Match(row).Value.Split(' ')[1];

                var weaknessPattern = new Regex(@"(?<=weak to ).*(?=\))");
                var weaknesses = weaknessPattern.Match(row).Value.Trim().Split(", ", StringSplitOptions.RemoveEmptyEntries);

                var immunityPattern = new Regex(@"(?<=immune to ).*(?=\))");
                var immunities = immunityPattern.Match(row).Value.Split("; ")[0].Split(", ", StringSplitOptions.RemoveEmptyEntries);

                yield return new Unit()
                {
                    Count = numbers[0],
                    UnitType = unitType,
                    HitPoints = numbers[1],
                    AttackDamage = numbers[2],
                    AttackType = attack,
                    Initiative = numbers[3],
                    Weaknesses = weaknesses,
                    Immunities = immunities
                };
            }
        }
    }

    public class Unit
    {
        public int Count { get; set; }
        public int AttackDamage { get; set; }
        public int HitPoints { get; set; }
        public int Initiative { get; set; }
        public char UnitType { get; set; }
        public string AttackType { get; set; }
        public string[] Weaknesses { get; set; }
        public string[] Immunities { get; set; }

        public int EffectivePower => Count * AttackDamage;
    }
}