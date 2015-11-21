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

      //  private Dictionary<IInteracter, IInteracter> collitionList1;
      //  private Dictionary<IInteracter, IInteracter> collitionList2;

        private List<IInteracter> temp1;
        private List<IInteracter> temp2;
        private List<IInteracter> temp3;
        private List<IInteracter> temp4;


        public CollitionTask()
        {
            this.interactors = new List<IInteracter>();
            this.shotInteractors = new List<IInteracter>();
          //  this.collitionList1 = new Dictionary<IInteracter, IInteracter>();
          //  this.collitionList2 = new Dictionary<IInteracter, IInteracter>();
            this.temp1 = new List<IInteracter>();
            this.temp2 = new List<IInteracter>();
            this.temp3 = new List<IInteracter>();
            this.temp4 = new List<IInteracter>();
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
            //this.collitionList1.Clear();
          //  this.collitionList2.Clear();
         /*   this.temp1.Clear();
            this.temp2.Clear();
            this.temp3.Clear();
            this.temp4.Clear();
*/
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


                        //  this.collitionList1.Add(item, item2);
                        //        this.temp1.Add(item);
                        //       this.temp2.Add(item2);

                    }
                }
            }

           

          //  foreach (var item in this.interactors)
            for (var i = 0; i < this.interactors.Count; i++ )
            {
              //  foreach (var item2 in this.interactors)
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

                            //    this.collitionList2.Add(item, item2);
                            //    this.temp3.Add(item);
                            //     this.temp4.Add(item2);
                        }
                    }

                }
            }

      /*      //一斉に衝突を通知
            
            foreach (var item in this.temp1)
            {
                foreach (var item2 in this.temp2)
                {
                    CollitionInfo info = new CollitionInfo();
                    info.CollitionObjectType = item2.GetCollider().BaseType;
                    info.CollitionInteractor = item2;
                    item.OnInteract(info);

                    CollitionInfo info2 = new CollitionInfo();
                    info2.CollitionObjectType = item.GetCollider().BaseType;
                    info2.CollitionInteractor = item;

                    item2.OnInteract(info2);
                }
               
            }

            foreach (var item in this.temp3)
            {
                foreach (var item2 in this.temp4)
                {
                    CollitionInfo info = new CollitionInfo();
                    info.CollitionObjectType = item2.GetCollider().BaseType;
                    info.CollitionInteractor = item2;
                    item.OnInteract(info);
                }
               
            }*/
        }

        private bool IsCollition(IInteracter obj1, IInteracter obj2)
        {
            if (obj1.GetCollider().NoCollitionTypes != null)
            {
                foreach (var item in obj1.GetCollider().NoCollitionTypes)
                {
                    if (obj2.GetCollider().BaseType.IsAssignableFrom(item))
                        return false;
                }
            }

            if (obj2.GetCollider().NoCollitionTypes != null)
            {
                foreach (var item in obj2.GetCollider().NoCollitionTypes)
                {
                    if (obj1.GetCollider().BaseType.IsAssignableFrom(item))
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
