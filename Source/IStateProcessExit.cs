using Cysharp.Threading.Tasks;

namespace Rokurei.FSM {
    public interface IStateProcessExit {
        UniTask ProcessExit();
    }
}