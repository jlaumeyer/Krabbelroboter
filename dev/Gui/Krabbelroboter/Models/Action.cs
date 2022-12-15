using System;
using System.ComponentModel;

namespace Krabbelroboter.Models
{
    public class Action : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private bool _isExecutable;
        public bool IsExecutable
        {
            get { return _isExecutable; }
            set 
            { 
                _isExecutable = value; 
                OnPropertyChanged(nameof(IsExecutable)); 
            }
        }

        private bool _isExplored;
        public bool IsExplored
        {
            get { return _isExplored; }
            set 
            { 
                _isExplored = value; 
                OnPropertyChanged(nameof(IsExplored)); 
            }
        }

        private short _reward;
        public short Reward
        {
            get { return _reward; }
            set 
            {
                _reward = value;
                OnPropertyChanged(nameof(Reward));
            }
        }
        public int encode(byte[] stream, int offset)
        {
            byte[] src = BitConverter.GetBytes(_reward);
            Array.Copy(src, 0, stream, offset, 2);
            offset += 2;
            // stream[offset++] = (byte)_reward;
            return offset;
        }
        public int decode(byte[] stream, int offset)
        {
            Reward = BitConverter.ToInt16(stream, offset);
            offset += 2;
            IsExecutable = BitConverter.ToBoolean(stream, offset++);
            IsExplored = BitConverter.ToBoolean(stream, offset++);
            return offset;
        }
        public Action()
        {
            IsExecutable = true;
            IsExplored = false;
            Reward = 0;
        }
    }
}
