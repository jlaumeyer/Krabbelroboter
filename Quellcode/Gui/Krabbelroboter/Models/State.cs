using System;
using System.ComponentModel;

namespace Krabbelroboter.Models
{
    public class State : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private float _value;
        public float Value
        { 
            get { return _value; }
            set 
            { 
                _value = value;
                OnPropertyChanged(nameof(Value));
            } 
        }

        private bool _isSelected;
        public bool IsSelected 
        { 
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                OnPropertyChanged(nameof(IsSelected));
            }
        }
        public Action Up { get; set; }
        public Action Right { get; set; }
        public Action Down { get; set; }
        public Action Left { get; set; }

        public int encode(byte[] stream, int offset)
        {
            byte[] src = BitConverter.GetBytes(_value);
            Array.Copy(src, 0, stream, offset, 4);
            offset += 4;
            offset = Up.encode(stream, offset);
            offset = Right.encode(stream, offset);
            offset = Down.encode(stream, offset);
            offset = Left.encode(stream, offset);
            return offset;
        }
        public int decode(byte[] stream, int offset, bool decodeActionValues)
        {
            Value = BitConverter.ToSingle(stream, offset);
            offset += 4;
            if (decodeActionValues)
            {
                offset = Up.decode(stream, offset);
                offset = Right.decode(stream, offset);
                offset = Down.decode(stream, offset);
                offset = Left.decode(stream, offset);
            } 
            else
            {
                offset += 16;
            }
            
            return offset;
        }

        public State()
        {
            Value = 0;
            IsSelected = false;
            Up = new Action();
            Right = new Action();
            Down = new Action();
            Left = new Action();
        }
    }
}
