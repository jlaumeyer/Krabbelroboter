using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Krabbelroboter.Models;

namespace Krabbelroboter.ViewModels
{
    public class MainViewModel
    {
        private MarkovDecisionProcess _mdp;
        public MarkovDecisionProcess MDP 
        { 
            get { return _mdp; }
        }
        public Client Robot { get; set; }
        public MainViewModel()
        {
            _mdp = new MarkovDecisionProcess(4, 4);
            Robot = new Client(_mdp);
        }
    }
}
