namespace OnewaveGames.Scripts.System.State
{
    public interface StateInterface
    {
        void OnBeginState() {}
        void OnExitState() {}
        public void Update() {}
    }
    
    public abstract class StateBase : StateInterface
    {
        private int _stateIndex = -1;
        private string _stateName;

        public virtual void Initialize(int index, string name) 
        {
            _stateIndex = index;
            _stateName = name;
        }
        
        public virtual void UnInitialize() { }
        public void OnBeginState() => Begin();
        public void OnExitState() => Exit();
        
        protected virtual void Begin() {}
        protected virtual void Exit() {}
        
        public virtual void Update() {}
    }
}