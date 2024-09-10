namespace CHIP_8_Virtual_Machine
{
    public class Sound
    {
        private Thread _soundThread;
        private bool _playSound;
        private int _duration;

        public void Beep(int durationInMilliseconds)
        {
            _duration = durationInMilliseconds;
            _playSound = true;
        }

        private void SoundLoop()
        {
            while (true)
            {
                if (_playSound)
                {
                    Console.Beep(440, _duration);
                    _playSound = false;
                }
            }
        }
        
        public Sound()
        {
            _soundThread = new Thread(SoundLoop);
            _soundThread.Start();
        }
    }
}
