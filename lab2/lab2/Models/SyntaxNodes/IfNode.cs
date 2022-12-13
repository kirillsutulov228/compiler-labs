﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2.Models.SyntaxNodes
{
    public class IfNode : StatementNode
    {
        public ExpressionNode Condition;
        public StatementNode Then;
        public StatementNode? Else;
        public IfNode() { }
        public IfNode(ExpressionNode condition, StatementNode then, StatementNode? els = null)
        {
            this.Condition = condition;
            this.Then = then;
            this.Else = els;
        }
    }
}
