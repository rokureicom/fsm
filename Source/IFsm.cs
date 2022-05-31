namespace Rokurei.FSM {
    public interface IFsm<TTrigger> {
        void AddState<TState>(TState state) where TState : IState;

        void AddTransitionBetween<TStateFrom, TStateTo>(TTrigger trigger) where TStateFrom : IState
                                                                          where TStateTo   : IState;
        void AddTransitionStart<TStateTo>(TTrigger trigger) where TStateTo : IState;

        void Fire(TTrigger trigger);
    }
}