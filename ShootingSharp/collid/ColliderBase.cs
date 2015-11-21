using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShootingSharp.interfaces;


namespace ShootingSharp.collid
{
    public abstract class ColliderBase 
    {
        public enum SharpType
        {
            Circle, Square
        }

        /// <summary>
        /// あたり判定を実行しない型を記述
        /// </summary>
        public List<Type> NoCollitionTypes { get; private set; }

        public Type BaseType { get; set; }

        public ColliderBase(Type baseType, params Type[] noCollitionTypes)
        {
            this.BaseType = baseType;

            if (noCollitionTypes != null)
                this.NoCollitionTypes = noCollitionTypes.ToList();
        }

        public void AddNoCollitionTypes(params Type[] types)
        {
            if (this.NoCollitionTypes == null)
                this.NoCollitionTypes = new List<Type>();

            foreach (var item in types)
            {
                this.NoCollitionTypes.Add(item);
            }
        }

        public abstract SharpType GetSharpType();
    }
}
