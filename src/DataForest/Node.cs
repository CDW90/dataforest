﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Dieser Code wurde von einem Tool generiert.
//     Wenn der Code neu generiert wird, gehen alle Änderungen an dieser Datei verloren
// </auto-generated>
//------------------------------------------------------------------------------
namespace Dataforest
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	public class Node
	{
		private object splitValue;

		private string name;

		private object childNodes;

		private object objects;

		private Attribut attribut;

		private Tree tree;

		public virtual Attribut Attribut
		{
			get;
			set;
		}

		public virtual object ChildNodes
		{
			get;
			set;
		}

		public virtual string Name
		{
			get;
			set;
		}

		public virtual object Objects
		{
			get;
			set;
		}

		public virtual object SplitValue
		{
			get;
			set;
		}

		public virtual Tree Tree
		{
			get;
			set;
		}

		public Node(object objs, Tree tree)
		{
		}

		public virtual void put(DataObject obj)
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

		public virtual Attribut getOptimalAttribut()
		{
			throw new System.NotImplementedException();
		}

	}
}

