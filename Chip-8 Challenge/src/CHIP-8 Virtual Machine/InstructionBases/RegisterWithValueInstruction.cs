namespace CHIP_8_Virtual_Machine.InstructionBases;

public class RegisterWithValueInstruction : Instruction
{
    public Register X => Arguments.HighNibble;
    public byte Value => Arguments.LowByte;

    public override string Disassemble(VM vm)
    {
        return $"{Mnemonic.PadRight(5)} V{X}{ValueOfRegister(vm, X)},0x{Value.ToString("X2")}";
    }

    public RegisterWithValueInstruction(Register X, byte value)
        : base(X, value)
    { }
}
