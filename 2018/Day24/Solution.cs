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
            var parsed = ParseInput(input);
            var result = Solver(parsed);

            // 22155 >
            // 22153 >
            // 22056 <
            return ($"{result}", timer.ElapsedMilliseconds);
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

                var defenders = new HashSet<Unit>(units);
                var attacks = new Dictionary<Unit, Unit>();
                foreach (var attacker in units.OrderByDescending(a => (a.EffectivePower, a.Initiative)))
                {
                    var maxDamage = defenders.Select(d => attacker.EffectiveDamage(d)).Max();

                    if (maxDamage > 0)
                    {
                        var defender = defenders
                            .Where(d => attacker.EffectiveDamage(d) == maxDamage)
                            .OrderByDescending(d => (d.EffectivePower, d.Initiative))
                            .First();

                        attacks.Add(attacker, defender);
                        defenders.Remove(defender);
                    }
                }

                foreach (var attacker in attacks.Keys.OrderByDescending(a => a.Initiative))
                {
                    if (attacker.Units > 0)
                    {
                        var defender = attacks[attacker];
                        var effectiveDamage = attacker.EffectiveDamage(defender);

                        if (effectiveDamage > 0 && defender.Units > 0)
                        {
                            var unitsLost = effectiveDamage / defender.HitPoints;
                            defender.Units = Math.Max(0, defender.Units - unitsLost);
                            if (unitsLost > 0)
                            {
                                hasAttackBeenDone = true;
                            }
                        }
                    }
                }

                units = units.Where(u => u.Units > 0).ToList();
            }

            return units.Select(u => u.Units).Sum();
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

                var specialsPattern = new Regex(@"\(.+\)");
                var specials = specialsPattern.Match(row).Value.Split(';');

                var weaknesses = new string[0];
                var immunities = new string[0];
                foreach (var special in specials)
                {
                    if (special.IndexOf("weak to") >= 0)
                    {
                        weaknesses = special.Replace("weak to", "").Replace("(", "").Replace(")", "").Split(',').Select(x => x.Trim()).ToArray();
                    }
                    else if (special.IndexOf("immune to") >= 0)
                    {
                        immunities = special.Replace("immune to", "").Replace("(", "").Replace(")", "").Split(',').Select(x => x.Trim()).ToArray();
                    }
                }

                yield return new Unit()
                {
                    Units = numbers[0],
                    UnitType = unitType,
                    HitPoints = numbers[1],
                    AttackDamage = numbers[2],
                    AttackType = attack,
                    Initiative = numbers[3],
                    Weaknesses = weaknesses.ToHashSet(),
                    Immunities = immunities.ToHashSet()
                };
            }
        }
    }

    public class Unit
    {
        public int Units { get; set; }
        public int AttackDamage { get; set; }
        public int HitPoints { get; set; }
        public int Initiative { get; set; }
        public char UnitType { get; set; }
        public string AttackType { get; set; }
        public HashSet<string> Weaknesses { get; set; } = new HashSet<string>();
        public HashSet<string> Immunities { get; set; } = new HashSet<string>();

        public int EffectivePower => Units * AttackDamage;

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

        public override string ToString()
        {
            return $"HP: {HitPoints}, D: {AttackDamage}, U: {Units}, I: {Initiative}";
        }
    }
}