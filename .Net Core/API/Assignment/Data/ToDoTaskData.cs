using Assignment.Entities;

namespace Assignment.Data
{
    public class ToDoTaskData
    {
        private static ToDoTaskData? _instance;
        public static ToDoTaskData Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ToDoTaskData();
                }
                return _instance;
            }
        }
        private Dictionary<int, ToDoTask> _task;
        public ToDoTaskData()
        {
            Initialize();
        }
        private void Initialize()
        {
            _task = new Dictionary<int, ToDoTask>();
            MaxKey = _task.Keys.Max();
        }
        public Dictionary<int, ToDoTask> ToDoTasks
        {
            get
            {
                return _task;
            }
            set
            {
                _task = value;
            }
        }
        public int MaxKey { get; set; }
    }
}