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
    static class Extentions
    {
        public static Obj_AI_Base Caster(this BuffInstance buffInstance)
        {
            var caster = EntityManager.Heroes.AllHeroes.FirstOrDefault(o => o.Name == buffInstance.SourceName);
            return caster ?? Player.Instance;
        }

        public static bool HasBuff(this Obj_AI_Base t, string s)
        {
            return t.Buffs.Any(a => a.Name.ToLower().Contains(s.ToLower()));
        }
    }
}
