using Cysharp.Threading.Tasks;

namespace Rokurei.FSM {
    public interface IStateProcessEnter {
        UniTask ProcessEnter();
    }
}