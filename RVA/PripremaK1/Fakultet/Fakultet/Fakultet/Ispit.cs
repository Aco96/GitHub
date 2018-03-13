///////////////////////////////////////////////////////////
//  Ispit.cs
//  Implementation of the Class Ispit
//  Generated by Enterprise Architect
//  Created on:      13-Mar-2018 5:26:41 PM
//  Original author: Stefan
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;



using Fakultet;
namespace Fakultet {
	public class Ispit {

		private DateTime datum;
		private bool polozen = false;
		public IspitniRok m_IspitniRok;
		public Predmet m_Predmet;

		public Ispit(){

		}

		~Ispit(){

		}

		public DateTime Datum{
			get{
				return datum;
			}
			set{
				datum = value;
			}
		}

		public bool Polozen{
			get{
				return polozen;
			}
			set{
				polozen = value;
			}
		}

	}//end Ispit

}//end namespace Fakultet