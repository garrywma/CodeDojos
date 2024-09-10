using CHIP_8_Virtual_Machine;
using CHIP_8_Virtual_Machine.InstructionBases;

namespace CHIP_8_Disassembler
{
    public class Disassembler
    {
        private byte[] _program;

        public IList<string> Disassemble(string name, int org)
        {
            List<string> disassembly =
            [
                $"# Program: {name.ToUpper()}",
                $"",
                $"\t\tORG 0x{org:X4}",
                $"",
            ];

            for (int i = 0; i < _program.Length; i += 2)
            {
                ushort opcode = (ushort)(_program[i] << 8 | _program[i + 1]);
                Instruction instruction = InstructionDecoder.DecodeInstruction(opcode);
                string line = $"0x{org + i:X4}\t{instruction.Disassemble()}";
                disassembly.Add(line);
            }

            return disassembly;
        }

        public Disassembler(byte[] program)
        {
            _program = program;
        }
    }
}
