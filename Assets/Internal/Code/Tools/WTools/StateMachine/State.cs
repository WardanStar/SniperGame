namespace Tools.WTools
{
    public abstract class State<T>
    {
        protected readonly StateMachine<T> StateMachine;

        protected State(
            StateMachine<T> stateMachine
            )
        {
            StateMachine = stateMachine;
            StateMachine.ResetStatesData += ResetStatesState;
        }

        public virtual void OnEnter(){}
        public virtual void OnExit(){}
        public virtual void Tick(){}
        public virtual void FixedTick(){}
        public virtual void LateTick(){}
        
        protected virtual void ResetStatesState(){}
    }
}