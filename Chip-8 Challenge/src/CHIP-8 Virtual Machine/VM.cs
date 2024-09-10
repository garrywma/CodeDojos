using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using CHIP_8_Virtual_Machine.InstructionBases;

using Timer = System.Timers.Timer;

namespace CHIP_8_Virtual_Machine
{
    public class VM : IDisposable, IDebugVM
    {
        private RAM _ram;
        private VRegisters _vregisters;
        private Stack<Tribble> _stack;

        private Clock _clock;
        private Keypad _keypad;
        private Display _display;
        private Sound _sound;
        private SystemFont _systemFont;
        private ITimer _delayTimer;
        private ITimer _soundTimer;

        public RAM RAM => _ram;
        public Keypad Keypad => _keypad;
        public Display Display => _display;
        public Sound Sound => _sound;
        public ITimer DelayTimer => _delayTimer;
        public SystemFont SystemFont => _systemFont;

        public VRegisters V => _vregisters;

        public Tribble PC { get; set; }
        public Tribble I { get; set; }
        public byte F { get { return V[0xF]; } }

        public event EventHandler<ExecutionResult> OnAfterExecution;

        public VM(IKeypadMap keypadMap = null)
        {
            _ram = new RAM();
            _vregisters = new VRegisters();
            _stack = new Stack<Tribble>();
            _keypad = new Keypad(keypadMap);
            _display = new Display(this);
            _delayTimer = new DelayTimer();
            _sound = new Sound();

            // load system font into memory
            _systemFont = new SystemFont();
            _systemFont.InstallTo(_ram);
        }

        void IDebugVM.ReplaceTimers(ITimer delayTimer, ITimer soundTimer)
        {
            _delayTimer = delayTimer;
            _soundTimer = soundTimer;
        }

        void IDebugVM.Pause()
        {
            _clock?.Pause();
        }

        void IDebugVM.Resume()
        {
            _clock?.Resume();
        }

        public void Load(byte[] bytes)
        {
            if (bytes.Length > RAM.RAM_SIZE)
            {
                throw new ArgumentOutOfRangeException("ROM is too large to fit in RAM");
            }

            for (ushort i = 0; i < bytes.Length; i++)
            {
                PC = 0x200;
                _ram[i + PC] = bytes[i];
            }
        }

        public void Load(string path)
        {
            byte[] rom = System.IO.File.ReadAllBytes(path);
            if (rom.Length > RAM.RAM_SIZE)
            {
                throw new ArgumentOutOfRangeException("ROM is too large to fit in RAM");
            }

            for (ushort i = 0; i < rom.Length; i++)
            {
                PC = 0x200;
                _ram[i + PC] = rom[i];
            }
        }

        internal void SetFlag(bool flagState)
        {
            V[0xF] = (byte)(flagState ? 1 : 0);
        }

        internal void PushStack(Tribble value)
        {
            _stack.Push(value);
        }

        internal Tribble PopStack()
        {
            return _stack.Pop();
        }

        private void InstructionCycle()
        {
            ushort opcode = _ram.GetWord(PC);
            Instruction instruction = InstructionDecoder.DecodeInstruction(opcode);
            ushort pc = PC;
            PC += 2;

            instruction.Execute(this);
            if (OnAfterExecution is not null)
            {
                ExecutionResult result = new ExecutionResult(instruction, opcode, pc, I, F, V, _stack, _keypad.State);
                OnAfterExecution.Invoke(this, result);
            }

            if (PC >= 0xFFF)
            {
                Console.WriteLine("End of memory reached");
                _clock.Stop();
                return;
            }
        }

        public void Run(ClockMode clockMode, int cycleLengthInMilliseconds)
        {
            _clock = new Clock(clockMode, InstructionCycle, cycleLengthInMilliseconds);
            _clock.Start();
        }

        public void Stop()
        {
            _clock?.Stop();
        }

        public void Dispose()
        {
            _clock?.Stop();
        }
    }
}
