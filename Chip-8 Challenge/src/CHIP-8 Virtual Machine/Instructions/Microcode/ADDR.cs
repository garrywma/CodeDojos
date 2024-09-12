using CHIP_8_Virtual_Machine.InstructionBases;

namespace CHIP_8_Virtual_Machine.Instructions;
public class ADDR : TwoRegistersInstruction
{
    public override void Execute(VM vm)
    {
        vm.SetFlag(vm.V[X] + vm.V[Y] > 0xFF);
        vm.V[X] += vm.V[Y];
    }

    public ADDR(Register X, Register Y)
        : base(X, Y, 0x4) { }
}