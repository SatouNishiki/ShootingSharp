using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootingSharp.task
{
    public class TaskManager : interfaces.ITask
    {
        private List<interfaces.ITask> taskList;

        public TaskManager()
        {
            this.taskList = new List<interfaces.ITask>();
        }

        public void AddTask(interfaces.ITask task)
        {
            this.taskList.Add(task);
        }
        
        public void Run()
        {
            foreach (var t in this.taskList)
            {
                t.Run();
            }
        }
    }
}
