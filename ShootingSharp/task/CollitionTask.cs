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
        //空間分割のためにつかう分割の幅
        private const int DivideX = 50;
        private const int DivideY = 50;

        /// <summary>
        /// 弾以外のすべてのオブジェクト
        /// </summary>
        private List<IInteracter> interactors;

        /// <summary>
        /// 弾
        /// </summary>
        private List<IInteracter> shotInteractors;

        private List<IInteracter>[] divideInteractorsList;

        private List<IInteracter>[] divideShotInteractorsList;

        private int windowSizeX;
        private int windowSizeY;

        public CollitionTask()
        {
            this.interactors = new List<IInteracter>();
            this.shotInteractors = new List<IInteracter>();

            this.windowSizeX = SSGame.GetInstance().GetWindowSize().Width;
            this.windowSizeY = SSGame.GetInstance().GetWindowSize().Height;
            this.divideInteractorsList = new List<IInteracter>[windowSizeX / DivideX + windowSizeY / DivideY * (this.windowSizeX / DivideX)];
            this.divideShotInteractorsList = new List<IInteracter>[windowSizeX / DivideX + windowSizeY / DivideY * (this.windowSizeX / DivideX)];

            for (int i = 0; i < this.divideInteractorsList.Length; i++)
                this.divideInteractorsList[i] = new List<IInteracter>();

            for (int i = 0; i < this.divideShotInteractorsList.Length; i++)
                this.divideShotInteractorsList[i] = new List<IInteracter>();
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

            foreach (var i in this.interactors)
            {
                this.divideInteractorsList[i.GetPosition().PosX / DivideX + i.GetPosition().PosY / DivideY * (this.windowSizeX / DivideX)].Add(i);
            }

            foreach (var i in this.shotInteractors)
            {
                this.divideShotInteractorsList[i.GetPosition().PosX / DivideX + i.GetPosition().PosY / DivideY * (this.windowSizeX / DivideX)].Add(i);
            }

            for (int k = 0; k < this.windowSizeX / DivideX * this.windowSizeY / DivideY; k++)
            {
                for (var i = 0; i < this.divideShotInteractorsList[k].Count; i++)
                {
                    for (var j = 0; j < this.divideInteractorsList[k].Count; j++)
                    {
                        if (this.IsCollition(this.divideShotInteractorsList[k][i], this.divideInteractorsList[k][j]))
                        {
                            CollitionInfo info = new CollitionInfo();
                            info.CollitionObjectType = this.divideInteractorsList[k][j].GetCollider().BaseType;
                            info.CollitionInteractor = this.divideInteractorsList[k][j];
                            this.divideShotInteractorsList[k][i].OnInteract(info);

                            CollitionInfo info2 = new CollitionInfo();
                            info2.CollitionObjectType = this.divideShotInteractorsList[k][i].GetCollider().BaseType;
                            info2.CollitionInteractor = this.divideShotInteractorsList[k][i];

                            this.divideInteractorsList[k][j].OnInteract(info2);

                        }
                    }
                }



                for (var i = 0; i < this.divideInteractorsList[k].Count; i++)
                {
                    for (var j = 0; j < this.divideInteractorsList[k].Count; j++)
                    {
                        if (i != j)
                        {
                            if (this.IsCollition(this.divideInteractorsList[k][i], this.divideInteractorsList[k][j]))
                            {
                                CollitionInfo info = new CollitionInfo();
                                info.CollitionObjectType = this.divideInteractorsList[k][j].GetCollider().BaseType;
                                info.CollitionInteractor = this.divideInteractorsList[k][j];
                                this.divideInteractorsList[k][i].OnInteract(info);
                            }
                        }

                    }
                }
            }

            this.shotInteractors.RemoveAll(s => !s.IsLiving());
            this.interactors.RemoveAll(i => !i.IsLiving());

            foreach (var i in this.divideInteractorsList)
                i.Clear();

            foreach (var i in this.divideShotInteractorsList)
                i.Clear();
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
