using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShootingSharp.interfaces;
using ShootingSharp.entity.shot;
using ShootingSharp.collid;

namespace ShootingSharp.task
{
    public class CollitionTask : interfaces.ITask
    {
        private List<IInteracter> interactors;

        private List<IInteracter> shotInteractors;


        public CollitionTask()
        {
            this.interactors = new List<IInteracter>();
            this.shotInteractors = new List<IInteracter>();
        }

        public void AddInteractors(IInteracter interact)
        {
            if (typeof(Shot).IsAssignableFrom(interact.GetCollider().BaseType))
                this.shotInteractors.Add(interact);
            else
                this.interactors.Add(interact);
        }

        public void Run()
        {
            for (var i = 0; i < this.shotInteractors.Count; i++ )
            {
                for(var j = 0; j < this.interactors.Count; j++ )
                {
                    if (this.IsCollition(this.shotInteractors[i], this.interactors[j]))
                    {
                        CollitionInfo info = new CollitionInfo();
                        info.CollitionObjectType = this.interactors[j].GetCollider().BaseType;
                        info.CollitionInteractor = this.interactors[j];
                        this.shotInteractors[i].OnInteract(info);

                        CollitionInfo info2 = new CollitionInfo();
                        info2.CollitionObjectType = this.shotInteractors[i].GetCollider().BaseType;
                        info2.CollitionInteractor = this.shotInteractors[i];

                        this.interactors[j].OnInteract(info2);

                    }
                }
            }

           

            for (var i = 0; i < this.interactors.Count; i++ )
            {
                for (var j = 0; j < this.interactors.Count; j++ )
                {
                    if (i != j)
                    {
                        if (this.IsCollition(this.interactors[i], this.interactors[j]))
                        {
                            CollitionInfo info = new CollitionInfo();
                            info.CollitionObjectType = this.interactors[j].GetCollider().BaseType;
                            info.CollitionInteractor = this.interactors[j];
                            this.interactors[i].OnInteract(info);
                        }
                    }

                }
            }

            this.shotInteractors.RemoveAll(s => !s.IsLiving());
            this.interactors.RemoveAll(i => !i.IsLiving());
        }

        private bool IsCollition(IInteracter obj1, IInteracter obj2)
        {
            if (obj1.GetCollider().BaseType == obj2.GetCollider().BaseType)
                return false;

            if (obj1.GetCollider().NoCollitionTypes != null)
            {
                foreach (var noCollitionObject in obj1.GetCollider().NoCollitionTypes)
                {
                    if (noCollitionObject.IsAssignableFrom(obj2.GetCollider().BaseType))
                        return false;
                }
            }

            if (obj2.GetCollider().NoCollitionTypes != null)
            {
                foreach (var noCollitionObject in obj2.GetCollider().NoCollitionTypes)
                {
                    if (noCollitionObject.IsAssignableFrom(obj1.GetCollider().BaseType))
                        return false;
                }
            }

            if (obj1.GetCollider().GetSharpType() == ColliderBase.SharpType.Circle)
            {

                if (obj2.GetCollider().GetSharpType() == ColliderBase.SharpType.Circle)
                {
                    return CollitionCalculator.IsInteractCircleCircle(obj1, obj2);
                }
                else
                {
                    return CollitionCalculator.IsInteractCircleSquare(obj1, obj2);
                }
            }
            else
            {
                if (obj2.GetCollider().GetSharpType() == ColliderBase.SharpType.Circle)
                {
                    return CollitionCalculator.IsInteractCircleSquare(obj2, obj1);
                }
                else
                {
                    return CollitionCalculator.IsInteractSquareSquare(obj1, obj2);
                }
            }
        }
    }
}
