using CHIP_8_Virtual_Machine.InstructionBases;

namespace CHIP_8_Virtual_Machine.Instructions;
public class SUBN : TwoRegistersInstruction
{
    public override void Execute(VM vm)
    {
        vm.SetFlag(vm.V[Y] > vm.V[X]);
        vm.V[X] = (byte)(vm.V[Y] - vm.V[X]);
    }

    public SUBN(Register X, Register Y)
        : base(X, Y, 0x7) { }
}