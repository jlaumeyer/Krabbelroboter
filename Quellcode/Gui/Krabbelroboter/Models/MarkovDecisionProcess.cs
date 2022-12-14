using System;
using System.Collections.ObjectModel;

namespace Krabbelroboter.Models
{
    public class MarkovDecisionProcess
    {
        private ObservableCollection<ObservableCollection<State>> _states;
        public ObservableCollection<ObservableCollection<State>> States
        { 
            get { return _states; }
        }

        public int encode(byte[] stream, int offset)
        {
            for (int i = 0; i < _states.Count; i++)
                for (int j = 0; j < _states[i].Count; j++)
                    offset = _states[i][j].encode(stream, offset);
            return offset;
        }
        public void decode(byte[] stream, int offset, bool decodeActionValues)
        {
            int row = stream[offset];
            offset++;
            int col = stream[offset];
            offset++;
            for(int i = 0; i < _states.Count; i++)
            {
                for(int j = 0; j < _states[i].Count; j++)
                {
                    State state = _states[i][j];
                    if (i == row && j == col)
                    {
                        state.IsSelected = true;
                    }
                    else
                    {
                        state.IsSelected = false;
                    }
                    offset = state.decode(stream, offset, decodeActionValues);
                }
            }
        }

        public MarkovDecisionProcess(int row_count, int col_count)
        {
            _states = new ObservableCollection<ObservableCollection<State>>();
            for (int i = 0; i < row_count; i++)
            {
                var row = new ObservableCollection<State>();
                for (int j = 0; j < col_count; j++)
                {
                    row.Add(new State());
                }
                _states.Add(row);
            }
        }
    }
}