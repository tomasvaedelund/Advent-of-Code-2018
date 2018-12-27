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

        public int Solver(IEnumerable<Unit> units)
        {
            units = units.ToList();

            var hasAttackBeenDone = true;
            while (hasAttackBeenDone)
            {
                hasAttackBeenDone = false;

                var attacks = new List<(Unit attacker, Unit defender)>();
                foreach (var attacker in units.OrderByDescending(a => (a.EffectivePower, a.Initiative)))
                {
                    var defenders = units
                        .Where(d => attacker.EffectiveDamage(d) > 0)
                        .OrderByDescending(d => attacker.EffectiveDamage(d))
                        .ThenByDescending(d => d.EffectivePower)
                        .ThenByDescending(d => d.Initiative);

                    if (!defenders.Any())
                    {
                        continue;
                    }

                    foreach (var defender in defenders)
                    {
                        if (attacks.Any(a => a.attacker == attacker || a.defender == defender))
                        {
                            continue;
                        }

                        attacks.Add((attacker: attacker, defender: defender));
                        hasAttackBeenDone = true;
                    }
                }

                attacks = attacks.OrderByDescending(a => a.attacker.Initiative).ToList();

                foreach (var attack in attacks)
                {
                    var unitsLost = attack.attacker.EffectiveDamage(attack.defender) / attack.defender.HitPoints;

                    foreach (var attackB in attacks)
                    {
                        if (attackB.defender.Equals(attack.defender))
                        {
                            attackB.defender.Count -= unitsLost;
                        }
                        else if (attackB.attacker.Equals(attack.defender))
                        {
                            attackB.attacker.Count -= unitsLost;
                        }
                    }

                    units.Where(u => u.Equals(attack.defender)).ToList().ForEach(u => u.Count -= unitsLost);
                }

                units = units.Where(u => u.Count > 0);
            }

            return units.Select(u => u.Count).Sum();
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

        public int EffectiveDamage(Unit defender)
        {
            if (defender.UnitType == UnitType)
            {
                return 0;
            }

            if (defender.Immunities.Contains(AttackType))
            {
                return 0;
            }

            if (defender.Weaknesses.Contains(AttackType))
            {
                return EffectivePower * 2;
            }

            return EffectivePower;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var other = obj as Unit;
            return other.HitPoints == HitPoints && other.Initiative == Initiative;
        }

        public override int GetHashCode()
        {
            return (HitPoints.GetHashCode() ^ Initiative.GetHashCode()).GetHashCode();
        }
    }
}