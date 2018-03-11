///////////////////////////////////////////////////////////
//  Prodavnica.cs
//  Implementation of the Class Prodavnica
//  Generated by Enterprise Architect
//  Created on:      11-Mar-2018 5:11:18 PM
//  Original author: Stefan
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;



public class Prodavnica {

	private string adresa;
	private string ime;
	public List<Proizvod> m_Proizvod;
	public List<Porudzbina> m_Porudzbina;

	public Prodavnica(){

	}

	~Prodavnica(){

	}

	public string Adresa{
		get{
			return adresa;
		}
		set{
			adresa = value;
		}
	}

	public string Ime{
		get{
			return ime;
		}
		set{
			ime = value;
		}
	}

}//end Prodavnica