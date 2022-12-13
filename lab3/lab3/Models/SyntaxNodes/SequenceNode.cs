﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3.Models.SyntaxNodes
{
    public class SequenceNode : StatementNode
    {
        public StatementNode Statement { get; set; }
        public StatementNode? Next { get; set; }

        public SequenceNode() { }
        public SequenceNode(StatementNode statement1, StatementNode? statement2 = null)
        {
            Statement = statement1;
            Next = statement2;
        }

        public override string ToPacalProgram()
        {
            return Statement.ToPacalProgram() + (Next != null ? $"\n{Next.ToPacalProgram()}" : "");
        }
    }
}
