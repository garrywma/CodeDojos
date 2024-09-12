using CHIP_8_Virtual_Machine.InstructionBases;
using System.Media;

namespace CHIP_8_Virtual_Machine.Instructions;
public class LOADS : RegisterInstruction
{
    public override void Execute(VM vm)
    {
        vm.Sound.Beep((1000 / 60) * vm.V[X]);
    }

    public LOADS(Register X)
        : base(X, 0x18) { }
}
