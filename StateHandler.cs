using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSharp;
using LeagueSharp.Common;
using Color = System.Drawing.Color;

namespace BenchTheKench
{
    class StateHandler
    {
        public static void Combo()
        {
            var target = TargetSelector.GetTarget(900, DamageType.Magical);
            if (target == null) return;

            if (BenchTheKench.ComboMenu["Combo.Q"].Cast<CheckBox>().CurrentValue && BenchTheKench.QSpell.IsReady() && target.IsValidTarget(800) && (!KenchUnbenched.ComboMenu["Combo.QOnlyStun"].Cast<CheckBox>().CurrentValue || !Player.Instance.IsInAutoAttackRange(target) || target.IsEmpowered()))
            {
                BenchTheKench.QSpell.Cast(target);
            }

            if (BenchTheKench.ComboMenu["Combo.W.Enemy"].Cast<CheckBox>().CurrentValue && !KenchCheckManager.IsSwallowed() && target.IsEmpowered())
            {
                BenchTheKench.WSpellSwallow.Cast(target);
            }

            if (BenchTheKench.ComboMenu["Combo.W.Minion"].Cast<CheckBox>().CurrentValue)
            {
                if (BenchTheKench.WSpellSpit.GetPrediction(target).HitChance >= HitChance.Medium)
                {
                    foreach (var minion in EntityManager.MinionsAndMonsters.EnemyMinions)
                    {
                        if (minion.Distance(Player.Instance) < BenchTheKench.WSpellSwallow.Range)
                        {
                            BenchTheKench.WSpellSwallow.Cast(minion);
                            break;
                        }
                    }
                }

                if (KenchCheckManager.IsSwallowed() && KenchCheckManager.WTarget != null &&
                    KenchCheckManager.WTarget.IsMinion)
                {
                    BenchTheKench.WSpellSpit.Cast(target);
                }
            }
        }

        public static void Harass()
        {
            var target = TargetSelector.GetTarget(900, DamageType.Magical);
            if (target == null) return;

            if (BenchTheKench.HarassMenu["Harass.Q"].Cast<CheckBox>().CurrentValue && BenchTheKench.QSpell.IsReady() && target.IsValidTarget(800) && (!KenchUnbenched.HarassMenu["Harass.QOnlyStun"].Cast<CheckBox>().CurrentValue || !Player.Instance.IsInAutoAttackRange(target) || target.IsEmpowered()))
            {
                BenchTheKench.QSpell.Cast(target);
            }

            if (BenchTheKench.HarassMenu["Harass.W.Enemy"].Cast<CheckBox>().CurrentValue && !KenchCheckManager.IsSwallowed() && target.IsEmpowered())
            {
                BenchTheKench.WSpellSwallow.Cast(target);
            }

            if (BenchTheKench.HarassMenu["Harass.W.Minion"].Cast<CheckBox>().CurrentValue)
            {
                if (BenchTheKench.WSpellSpit.GetPrediction(target).HitChance >= HitChance.Medium)
                {
                    foreach (var minion in EntityManager.MinionsAndMonsters.EnemyMinions)
                    {
                        if (minion.Distance(Player.Instance) < BenchTheKench.WSpellSwallow.Range)
                        {
                            BenchTheKench.WSpellSwallow.Cast(minion);
                            break;
                        }
                    }
                }

                if (KenchCheckManager.IsSwallowed() && KenchCheckManager.WTarget != null &&
                    KenchCheckManager.WTarget.IsMinion)
                {
                    BenchTheKench.WSpellSpit.Cast(target);
                }
            }
        }

        public static void LastHit()
        {
            if (BenchTheKench.FarmingMenu["LastHit.Q"].Cast<CheckBox>().CurrentValue && BenchTheKench.QSpell.IsReady())
            {
                foreach (
                    var enemies in
                        EntityManager.MinionsAndMonsters.EnemyMinions.Where(
                            a => a.Distance(Player.Instance) <= 900 && a.Health <= TahmDamage.QDamage(a)))
                {
                    if (BenchTheKench.QSpell.GetPrediction(enemies).HitChance >= HitChance.Medium)
                    {
                        BenchTheKench.QSpell.Cast(enemies);
                        break;
                    }
                }
            }
        }

        public static void WaveClear()
        {
            if (BenchTheKench.FarmingMenu["WaveClear.Q"].Cast<CheckBox>().CurrentValue && BenchTheKench.QSpell.IsReady())
            {
                foreach (
                    var enemies in
                        EntityManager.MinionsAndMonsters.EnemyMinions.Where(
                            a => a.Distance(Player.Instance) <= 900 && a.Health <= TahmDamage.QDamage(a)))
                {
                    if (BenchTheKench.QSpell.GetPrediction(enemies).HitChance >= HitChance.Medium)
                    {
                        BenchTheKench.QSpell.Cast(enemies);
                        break;
                    }
                }
            }
        }

        public static void JungleClear()
        {
            var target = EntityManager.MinionsAndMonsters.GetJungleMonsters().OrderBy(a => a.MaxHealth).FirstOrDefault(a => a.Distance(Player.Instance) <= KenchUnbenched.QSpell.Range);
            if (target == null) return;

            if (BenchTheKench.FarmingMenu["Jungle.Q"].Cast<CheckBox>().CurrentValue && KenchUnbenched.QSpell.IsReady() && KenchUnbenched.QSpell.GetPrediction(target).HitChance >= HitChance.Medium)
            {
                BenchTheKench.QSpell.Cast(target);
            }
        }

        public static void KillSteal()
        {
            foreach (var enemy in EntityManager.Heroes.Enemies)
            {
                if (enemy.IsDead || enemy.Health == 0 || enemy.IsZombie) return;
                if (BenchTheKench.QSpell.IsReady() && TahmDamage.QDamage(enemy) > enemy.Health &&
                    BenchTheKench.KillStealMenu["KillSteal.Q"].Cast<CheckBox>().CurrentValue)
                {
                    BenchTheKench.QSpell.Cast(enemy);
                    return;
                }
                if (BenchTheKench.WSpellSwallow.IsReady() && TahmDamage.WDamage(enemy) > enemy.Health && enemy.IsEmpowered() &&
                    BenchTheKench.KillStealMenu["KillSteal.W.Swallow"].Cast<CheckBox>().CurrentValue)
                {
                    BenchTheKench.QSpell.Cast(enemy);
                    return;
                }

                var pred = BenchTheKench.WSpellSpit.GetPrediction(enemy);
                if (KenchCheckManager.IsSwallowed() && BenchTheKench.KillStealMenu["KillSteal.W.Spit"].Cast<CheckBox>().CurrentValue && TahmDamage.WPDamage(enemy) > enemy.Health)
                {
                    BenchTheKench.WSpellSpit.Cast(enemy);
                    return;
                }
                if(BenchTheKench.WSpellSwallow.IsReady() && TahmDamage.WPDamage(enemy) > enemy.Health && (!pred.CollisionObjects.Any() || pred.CollisionObjects.Count() == 1 && pred.CollisionObjects[0].IsMinion && pred.CollisionObjects[0].Distance(Player.Instance) <= 250) && enemy.IsEmpowered() &&
                    BenchTheKench.KillStealMenu["KillSteal.W.Spit"].Cast<CheckBox>().CurrentValue)
                {
                    if (pred.CollisionObjects.Count() == 1 && pred.CollisionObjects[0].IsMinion)
                    {
                        BenchTheKench.WSpellSwallow.Cast(pred.CollisionObjects[0]);
                        return;
                    }
                    if (pred.CollisionObjects.Any()) continue;
                    var unit =
                        EntityManager.MinionsAndMonsters.EnemyMinions.FirstOrDefault(
                            a => a.Distance(Player.Instance) <= 250);
                    if (unit != null)
                        BenchTheKench.WSpellSwallow.Cast(unit);
                }
            }
        }
    }
}
