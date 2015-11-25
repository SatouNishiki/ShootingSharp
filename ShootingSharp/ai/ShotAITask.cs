using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Linq.Expressions;
using ShootingSharp.entity.shot;

namespace ShootingSharp.ai
{
    public abstract class ShotAITask<T> : AITask where T : Shot
    {
        protected Func<Shot.Builder, T> factory;
        protected int shotMetaData;

        public ShotAITask(entity.Entity entity, int priority, int frame, int shotMetaData)
            : base(entity, priority, frame)
        {
            factory = CreateInstance<Shot.Builder, T>();
            this.shotMetaData = shotMetaData;
        }

        protected Func<T1, TInstance> CreateInstance<T1, TInstance>()
        {
            var argsTypes = new[] { typeof(T1) };
            var constructor = typeof(TInstance).GetConstructor(BindingFlags.Public | BindingFlags.Instance, Type.DefaultBinder, argsTypes, null);
            var args = argsTypes.Select(Expression.Parameter).ToArray();
            return Expression.Lambda<Func<T1,TInstance>>(Expression.New(constructor, args), args).Compile();
        }

        public abstract Shot GetShot();

        protected override void RunMethod()
        {
            this.entity.Scene.AddShot(this.GetShot());
        }
    }
}
