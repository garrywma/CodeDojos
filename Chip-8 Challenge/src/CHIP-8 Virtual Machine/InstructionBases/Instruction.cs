namespace CHIP_8_Virtual_Machine.InstructionBases;

public abstract class Instruction
{
    public string Mnemonic { get; init; }
    public Tribble Arguments { get; set; }

    public virtual void Execute(VM vm)
    {
        throw new NotImplementedException();
    }

    public virtual string Disassemble(VM vm = null)
    {
        return $"{Mnemonic.PadRight(5)} 0x{Arguments.HighNibble.ToHexString()},0x{Arguments.MiddleNibble.ToHexString()},0x{Arguments.LowNibble.ToHexString()}";
    }

    protected string ValueOfRegister(VM vm, Register R)
    {
        return vm is not null ? $"[{vm.V[R].ToString("X2")}]" : "";
    }

    public Instruction(Nibble high, Nibble middle, Nibble low)
        : this(new Tribble(high, middle, low))
    { }

    public Instruction(Nibble high, byte lowByte)
        : this(new Tribble(high, lowByte.HighNibble(), lowByte.LowNibble()))
    { }

    public Instruction(Tribble arguments)
    {
        Arguments = arguments;
        Mnemonic = GetType().Name.ToUpper();
    }
}
