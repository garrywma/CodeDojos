using CHIP_8_Virtual_Machine.InstructionBases;

namespace CHIP_8_Virtual_Machine.Instructions;
public class LDSPR : RegisterInstruction
{
    public override void Execute(VM vm)
    {
        vm.I = vm.SystemFont.AddressOf((char)((byte)X));
    }

    public LDSPR(Register X)
        : base(X, 0x29) { }
}