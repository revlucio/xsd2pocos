//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;


namespace Generated {
    
    
    /// <remarks/>
    public partial class dvd {
        
		public String Year { get; set; }
		public Nullable`1 Publish { get; set; }
		public String Title { get; set; }
		public Styles Style { get; set; }
		public Int32 PublishYear { get; set; }
		public dvdCover Cover { get; set; }
    }
    
    /// <remarks/>
    public enum Styles {
        
		public String Year { get; set; }
		public Nullable`1 Publish { get; set; }
		public String Title { get; set; }
		public Styles Style { get; set; }
		public Int32 PublishYear { get; set; }
		public dvdCover Cover { get; set; }
    }
    
    /// <remarks/>
    public partial class dvdCover {
        
		public String Year { get; set; }
		public Nullable`1 Publish { get; set; }
		public String Title { get; set; }
		public Styles Style { get; set; }
		public Int32 PublishYear { get; set; }
		public dvdCover Cover { get; set; }
		public String Systeme { get; set; }
		public String Value { get; set; }
    }
    
    /// <remarks/>
    public partial class mycttype {
        
		public String Year { get; set; }
		public Nullable`1 Publish { get; set; }
		public String Title { get; set; }
		public Styles Style { get; set; }
		public Int32 PublishYear { get; set; }
		public dvdCover Cover { get; set; }
		public String Systeme { get; set; }
		public String Value { get; set; }
		public Double Value { get; set; }
    }
    
    /// <remarks/>
    public partial class DvdCollection {
        
		public String Year { get; set; }
		public Nullable`1 Publish { get; set; }
		public String Title { get; set; }
		public Styles Style { get; set; }
		public Int32 PublishYear { get; set; }
		public dvdCover Cover { get; set; }
		public String Systeme { get; set; }
		public String Value { get; set; }
		public Double Value { get; set; }
		public dvd Dvds { get; set; }
    }
}
