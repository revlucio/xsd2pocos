namespace Generated.First
{


    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class Actor
    {

        private string firstnameField;

        private string lastnameField;

        private bool principalActorField;

        private bool principalActorFieldSpecified;

        private string nationalityField;

        public Actor()
        {
            this.nationalityField = "US";
        }

        /// <remarks/>
        public string firstname
        {
            get
            {
                return this.firstnameField;
            }
            set
            {
                this.firstnameField = value;
            }
        }

        /// <remarks/>
        public string lastname
        {
            get
            {
                return this.lastnameField;
            }
            set
            {
                this.lastnameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool PrincipalActor
        {
            get
            {
                return this.principalActorField;
            }
            set
            {
                this.principalActorField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool PrincipalActorSpecified
        {
            get
            {
                return this.principalActorFieldSpecified;
            }
            set
            {
                this.principalActorFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [System.ComponentModel.DefaultValueAttribute("US")]
        public string nationality
        {
            get
            {
                return this.nationalityField;
            }
            set
            {
                this.nationalityField = value;
            }
        }
    }
}
