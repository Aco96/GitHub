///////////////////////////////////////////////////////////
//  Stavka.cs
//  Implementation of the Class Stavka
//  Generated by Enterprise Architect
//  Created on:      13-Mar-2018 6:13:17 PM
//  Original author: Stefan
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;



using Prodavnica;
namespace Prodavnica {
	public class Stavka {

		private int kolicina;
		public Proizvod m_Proizvod;

		public Stavka(){

		}

		~Stavka(){

		}

		public int Kolicina{
			get{
				return kolicina;
			}
			set{
				kolicina = value;
			}
		}

	}//end Stavka

}//end namespace Prodavnica