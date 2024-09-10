namespace CHIP_8_Disassembler
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = "c:\\temp\\pong.rom"; // args[0];
            if (File.Exists(path))
            {
                byte[] program = File.ReadAllBytes(path);
                Disassembler disassembler = new Disassembler(program);
                var disassembly = disassembler.Disassemble(Path.GetFileNameWithoutExtension(path), 0x200);

                path = Path.ChangeExtension(path, ".ch8");
                if (File.Exists(path)) File.Delete(path);
                File.WriteAllLines(path, disassembly);
                foreach (var line in disassembly)
                {
                    Console.WriteLine(line);
                }

                Console.WriteLine($"\nDisassembled {path}");
            }
        }
    }
}
