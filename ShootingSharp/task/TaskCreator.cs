using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShootingSharp.interfaces;
using ShootingSharp.core;

namespace ShootingSharp.task
{
    public class TaskCreator<T> where T : ITask
    {
        public static T Create()
        {
            T t = Activator.CreateInstance<T>();

            Logger l = Logger.GetInstance();

            if (t != null)
            {
                l.Debug(t.ToString() + " is created");
            }
            else
            {
                l.Fatal("null task created");
            }
            return t;
        }
    }
}
