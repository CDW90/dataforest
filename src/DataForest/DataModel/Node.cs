﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Dieser Code wurde von einem Tool generiert.
//     Wenn der Code neu generiert wird, gehen alle Änderungen an dieser Datei verloren
// </auto-generated>
//------------------------------------------------------------------------------
namespace DataForest.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;

	public class Node
	{
		private double splitValue;

		private string name;

		private List<Node> childNodes;

		private DataRow objects;

		private string attribut;

		private Tree tree;
		public string Attribut
		{
            get
            {
                return attribut;
            }
            set
            {
                attribut = value;
            }
		}

		public virtual List<Node> ChildNodes
		{
			get;
			set;
		}

		public virtual string Name
		{
			get;
			set;
		}

		public virtual DataRow Objects
		{
			get;
			set;
		}

		public virtual double SplitValue
		{
			get;
			set;
		}

		public virtual Tree Tree
		{
			get;
			set;
		}

		public Node(DataRow[] row, Tree tree)
		{
		}

		public virtual void put(DataRow row)
		{
			throw new System.NotImplementedException();
		}

		public virtual void makeSubTrees()
		{
			throw new System.NotImplementedException();
		}

		public virtual Node getParentNode()
		{
			throw new System.NotImplementedException();
		}

		public string getOptimalAttribut()
		{
			throw new System.NotImplementedException();
		}

	}
}

