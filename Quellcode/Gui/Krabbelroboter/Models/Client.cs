using System;
using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Krabbelroboter.Commands;

namespace Krabbelroboter.Models
{
    public class Client : INotifyPropertyChanged
    {
        private Socket _socket;

        private bool _isConnected;
        public bool IsConnected
        {
            get { return _isConnected; }
            set
            {
                _isConnected = value;
                OnPropertyChanged(nameof(IsConnected));
            }
        }
        public string Ipv4String { get; set; }
        public string PortString { get; set; }
        public int Iterations { get; set; }
        public bool ShowUpdates { get; set; }
        
        private bool _isBusy;
        private string? _lastMessage;
        public ICommand ConnectCommand { get; set; }
        public ICommand SendCommand { get; set; }
        public ICommand StopCommand { get; set; }
        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if(PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private MarkovDecisionProcess _mdp;

        public Client(MarkovDecisionProcess mdp)
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Ipv4String = "192.168.178.30";
            PortString = "3333";
            Iterations = 20;
            ShowUpdates = false;
            ConnectCommand = new RelayCommand(
                param => this.Connect(),
                param => { return !_isConnected; }
            );
            SendCommand = new RelayCommand(
                param => this.Send((string)param),
                param => { return !_isBusy && _isConnected; }
            );
            StopCommand = new RelayCommand(
                param => { _lastMessage = "Stop"; },
                param => { return true; }
            );
            _mdp = mdp;
        }

        private void Connect()
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                _socket.Connect(
                    IPAddress.Parse(Ipv4String),
                    Int32.Parse(PortString)
                );
                IsConnected = true;
            }
            catch (Exception e) 
            {
                IsConnected = false;
                MessageBox.Show(e.Message);
            }

            if (IsConnected)
                Send("7");
        }

        /**
         * message
         *     0 :: Oben
         *     1 :: Rechts
         *     2 :: Unten
         *     3 :: Links
         *     4 :: Erkunden
         *     5 :: Lernen
         *     6 :: Laufen (Q-Lernen)
         *     7 :: Reset
         *     8 :: Push
         */
        public void Send(string message, bool pass = false)
        {
            _lastMessage = message;

            if (!_isBusy || pass)
            {
                _isBusy = true;

                try
                {
                    int buffer_size = 512;
                    byte[] buffer = new byte[buffer_size];
                    buffer[0] = Encoding.ASCII.GetBytes(message)[0];

                    if (message.Equals("5"))
                        buffer[1] = (byte)Iterations;

                    if (message.Equals("8"))
                        _mdp.encode(buffer, 1);

                    _socket.Send(buffer, buffer_size, 0);

                    StateObject state = new StateObject();
                    state.workSocket = _socket;
                    _socket.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                        new AsyncCallback(ReadCallback), state);
                }
                catch (Exception e)
                {
                    _isBusy = false;
                    IsConnected = false;
                    MessageBox.Show(e.Message);
                }
            }
        }

        public void ReadCallback(IAsyncResult ar)
        {
            StateObject state = (StateObject)ar.AsyncState;
            Socket handler = state.workSocket;

            int bytesRead = handler.EndReceive(ar); // das vielleicht noch in ein try-catch packen

            if (bytesRead == 0)
            {
                _isBusy = false;
                IsConnected = false;
                return;
            }

            while (bytesRead < 512)
                bytesRead += handler.Receive(state.buffer, bytesRead, StateObject.BufferSize - bytesRead, 0);
            //int messageLength = BitConverter.ToInt16(state.buffer, 0) + 2;
            //while (bytesRead < messageLength)
            //    bytesRead = handler.Receive(state.buffer, bytesRead, StateObject.BufferSize - bytesRead, 0);
            //int offset = 2;
            int offset = 0;

            //if (ShowUpdates)
            _mdp.decode(state.buffer, offset, ShowUpdates);

            if (String.Equals(_lastMessage, "4") || String.Equals(_lastMessage, "6"))
            {
                Application.Current.Dispatcher.Invoke(
                    DispatcherPriority.Background,
                    new System.Action(() => Send(_lastMessage, true))
                );
            }
            else
            {
                _isBusy = false;
                Application.Current.Dispatcher.Invoke(
                    DispatcherPriority.Background,
                    new System.Action(() => CommandManager.InvalidateRequerySuggested())
                );
            }
        }
    }
}
