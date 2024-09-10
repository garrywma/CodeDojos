using CHIP_8_Virtual_Machine.InstructionBases;

namespace CHIP_8_Virtual_Machine.Instructions;
public class ADDR : TwoRegistersInstruction
{
    public override void Execute(VM vm)
    {
        int x = vm.V[X];
        int y = vm.V[Y];
        vm.SetFlag(x + y > 0xFF);

        vm.V[X] += vm.V[Y];
    }

    public ADDR(Register X, Register Y)
        : base(X, Y, 0x4) { }
}