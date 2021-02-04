using System;
using System.Collections.Generic;
using System.Text;

namespace CollabAssist.Incoming
{
    public class Reviewer
    {
        public string Name { get; set; }
        public Vote Vote { get; set; }

        public Reviewer(string name, Vote vote)
        {
            Name = name;
            Vote = vote;
        }

        public Reviewer(string name)
        {
            Name = name;
        }

        public Reviewer()
        {
        }
    }
}
