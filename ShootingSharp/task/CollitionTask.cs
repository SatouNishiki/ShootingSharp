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
            if (interact.GetCollider().BaseType == typeof(Shot))
                this.shotInteractors.Add(interact);
            else
                this.interactors.Add(interact);
        }

        public void Run()
        {
            foreach (var item in this.shotInteractors)
            {
                foreach (var item2 in this.interactors)
                {
                    if (this.IsCollition(item, item2))
                    {
                        CollitionInfo info = new CollitionInfo();
                        info.CollitionObjectType = item2.GetCollider().BaseType;
                        item.OnInteract(info);

                        CollitionInfo info2 = new CollitionInfo();
                        info2.CollitionObjectType = item.GetCollider().BaseType;
                        item2.OnInteract(info2);
                    }
                }
            }

            foreach (var item in this.interactors)
            {
                foreach (var item2 in this.interactors)
                {
                    if (item != item2)
                    {
                        if (this.IsCollition(item, item2))
                        {
                            CollitionInfo info = new CollitionInfo();
                            info.CollitionObjectType = item2.GetCollider().BaseType;
                            item.OnInteract(info);
                        }
                    }
                        
                }
            }
        }

        private bool IsCollition(IInteracter obj1, IInteracter obj2)
        {
            if (obj1.GetCollider().NoCollitionTypes != null && obj1.GetCollider().NoCollitionTypes.ToList().IndexOf(obj2.GetCollider().BaseType) >= 0)
                return false;

            if (obj2.GetCollider().NoCollitionTypes != null && obj2.GetCollider().NoCollitionTypes.ToList().IndexOf(obj1.GetCollider().BaseType) >= 0)
                return false;

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
