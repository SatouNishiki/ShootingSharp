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
        public Type[] NoCollitionTypes { get; set; }

        public Type BaseType { get; set; }

        public ColliderBase(Type baseType, params Type[] noCollitionTypes)
        {
            this.BaseType = baseType;
            this.NoCollitionTypes = noCollitionTypes;
        }

        public abstract SharpType GetSharpType();
    }
}
