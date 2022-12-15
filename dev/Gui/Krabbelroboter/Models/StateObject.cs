﻿using System.Net.Sockets;
using System.Text;

namespace Krabbelroboter.Models
{
    public class StateObject
    {
        public const int BufferSize = 512;

        // Receive buffer.  
        public byte[] buffer = new byte[BufferSize];

        // Received data string.
        // public StringBuilder sb = new StringBuilder();

        // Client socket.
        public Socket workSocket = null;
    }
}
