namespace CHIP_8_Virtual_Machine.InstructionBases;

public class TwoRegistersInstruction : Instruction
{
    public Register X => Arguments.HighNibble;
    public Register Y => Arguments.MiddleNibble;

    public override string Disassemble(VM vm)
    {
        return $"{Mnemonic.PadRight(5)} V{X}{ValueOfRegister(vm, X)},V{Y}{ValueOfRegister(vm, Y)}";
    }

    public TwoRegistersInstruction(Register X, Register Y, Nibble discriminator)
        : base(X, Y, discriminator)
    { }
}

