using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootingSharp.entity.shot
{
    public abstract class ShotBuilder<T> where T : Shot
    {

        protected abstract void SetBuilder(T shot);

        public T CreateShot()
        {
            Type t = typeof(T);
            T shot = Activator.CreateInstance<T>();
            this.SetBuilder(shot);

            return shot;
        }
    }
}
