using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lab3.Models.SyntaxNodes;
namespace lab3.Models
{
	public class IdentifiersTable
	{
		private Dictionary<string, IdentifierNode> _table;
		protected IdentifiersTable _previous;

		public IdentifiersTable(IdentifiersTable prev)
		{
			_table = new Dictionary<string, IdentifierNode>();
			_previous = prev;
		}

		public void Put(Lexeme token, IdentifierNode identifier)
		{
			_table.Add(token.Value, identifier);
		}

		public IdentifierNode? Get(Lexeme token)
		{
			for (IdentifiersTable t = this; t != null; t = t._previous)
			{
				if (t._table.ContainsKey(token.Value))
				{
					return t._table[token.Value];
				}
			}

			return null;
		}
	}
}
