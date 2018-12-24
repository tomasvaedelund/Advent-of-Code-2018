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
            units = units
                .OrderByDescending(u => u.EffectivePower)
                .ThenBy(u => u.Initiative);

            var result = new List<(Unit attacker, Unit defender)>();

            foreach (var attacker in units)
            {
                var targets = units
                    .Where(t => t != attacker)
                    .Where(t => t.UnitType != attacker.UnitType)
                    .Where(t => !t.Immunities.Contains(attacker.AttackType))
                    .OrderByDescending(t => EffectiveDamage(attacker, t))
                    .ThenByDescending(t => t.EffectivePower)
                    .ThenByDescending(t => t.Initiative);

                foreach (var target in targets)
                {
                    if (!result.Any(r => r.attacker == attacker) && !result.Any(r => r.defender == target))
                    {
                        result.Add((attacker: attacker, defender: target));
                    }
                }
            }

            return result.OrderByDescending(r => r.attacker.Initiative);
        }

        public IEnumerable<Unit> AttackingPhase(IEnumerable<(Unit attacker, Unit defender)> attacks)
        {

            foreach (var attack in attacks)
            {
                var damage = EffectiveDamage(attack.attacker, attack.defender);
                var unitsLost = damage / attack.defender.HitPoints;

                attack.defender.Count -= unitsLost;
            }

            return attacks.Select(a => a.defender).Where(d => d.Count > 0);
        }

        private int EffectiveDamage(Unit attacker, Unit target)
        {
            return (target.Weaknesses.Contains(attacker.AttackType)) ? attacker.EffectivePower * 2 : attacker.EffectivePower;
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